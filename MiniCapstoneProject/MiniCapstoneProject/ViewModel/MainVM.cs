using MiniCapstoneProject.Command;
using MiniCapstoneProject.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Timer = System.Timers.Timer;

namespace MiniCapstoneProject.ViewModel
{
    class MainVM : BaseVM
    {
        private int _filesProcessed = 0;
        private int _activeThreads;
        private int _queueCount;
        private int _gCCount;
        private int _elapsed; 
        private string _scannedFiles;
        private int id = 0;
        private FileJob job;

        public int FilesProcessed
        {
            get => _filesProcessed;
            set
            {
                _filesProcessed = value;
                OnPropertyChanged(nameof(FilesProcessed));
            }
        }
        public int ActiveThreads
        {
            get => _activeThreads;
            set
            {
                _activeThreads = value;
                OnPropertyChanged(nameof(ActiveThreads));
            }
        }
        public int QueueCount
        {
            get => _queueCount;
            set
            {
                _queueCount = value;
                OnPropertyChanged(nameof(QueueCount));
            }
        }
        public int GCCount
        {
            get => _gCCount;
            set
            {
                _gCCount = value;
                OnPropertyChanged(nameof(GCCount));

            }
        }
        public int Elapsed
        {
            get => _elapsed;
            set
            {
                _elapsed = value;
                OnPropertyChanged(nameof(Elapsed));
            }
        }
        public string ScannedFiles
        {
            get => _scannedFiles;
            set
            {
                _scannedFiles = value;
                OnPropertyChanged(nameof(ScannedFiles));
            }
        }

        public ObservableCollection<FileJob> Jobs { get; } = new ObservableCollection<FileJob>();
        public ConcurrentQueue<FileJob> queue = new ConcurrentQueue<FileJob>();

        public ICommand GenerateJobCommand { get; }

        private Stopwatch sw = Stopwatch.StartNew();
        private Timer timer = new Timer(1000);

        public MainVM()
        {
            GenerateJobCommand = new RelayCommand(GenerateJob);
            timer.Elapsed += TimerElapsed;

            StartWorkers();
            timer.Start();
        }

        private void GenerateJob()
        {
            for (int i = 1; i <= 5; i++)
            {
                job = new FileJob
                {
                    Id = id + i,
                    FileName = $"File {id + i}.txt",
                    Status = "Pending"
                };
                queue.Enqueue(job);
                Jobs.Add(job);
            }
            id = job.Id;
            Task.Run(() => ParallelScan());
        }
        private void StartWorkers()
        {
            for (int i = 0; i < 4; i++)
            {
                Task.Run(ProcessJob);
            }
        }
        
        private async Task ProcessJob()
        {
            while (true)
            {
                if (queue.TryDequeue(out FileJob job))
                {
                    await ProcessFile(job);
                }
                await Task.Delay(100);
            }
        }
        private async Task ProcessFile(FileJob job)
        {
            Interlocked.Increment(ref _activeThreads);

            Application.Current.Dispatcher.Invoke(() =>
            {
                job.Status = "Processing";
                FilesProcessed++;
            });

            await Task.Delay(2000);

            Application.Current.Dispatcher.Invoke(() =>
            {
                job.Status = "Completed";
            });

            Interlocked.Decrement(ref _activeThreads);

            Application.Current.Dispatcher.Invoke(() =>
            {
                ActiveThreads = _activeThreads;
            });
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ActiveThreads =
                QueueCount = queue.Count;

                GCCount = GC.CollectionCount(0);

                Elapsed = (int)sw.Elapsed.TotalSeconds;
            });
        }
         
        private void ParallelScan()
        {
            var files = new List<FileJob>(Jobs);

            Parallel.ForEach(files, file =>
            {
                Thread.Sleep(1000); 
            });
            ScannedFiles = $"Scanned {files.Count} files";
        }
    }
}
