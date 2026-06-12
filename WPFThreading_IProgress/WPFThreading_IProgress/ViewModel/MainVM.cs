using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFThreading_IProgress.Command;

namespace WPFThreading_IProgress.ViewModel
{
    class MainVM : BaseVM
    {
        private string _fileCount;
        private int _progressValue;
        private bool _isDownloading;

        public string FileCount
        {
            get => _fileCount;
            set
            {
                _fileCount = value;
                OnPropertyChanged(nameof(FileCount));
            }
        }
        public int ProgressValue
        {
            get => _progressValue;
            set
            {
                _progressValue = value;
                OnPropertyChanged(nameof(ProgressValue));
            }
        }
        public bool IsDownloading
        {
            get => _isDownloading;
            set
            {
                _isDownloading = value;
                OnPropertyChanged(nameof(IsDownloading));
            }
        } 
         
        public ICommand StartCommand { get; }
        public ICommand HiCommand { get; }

        public MainVM()
        {
            StartCommand = new RelayCommand(async () =>
            {
                await DownloadFilesAsync();
            });

            HiCommand = new RelayCommand(Hiii);
        }

        private async Task DownloadFilesAsync()
        {
            if (IsDownloading)
                return; 
            try
            {
                IsDownloading = true;
                IProgress<int> progress =
                new Progress<int>(value =>
                {
                    ProgressValue = value;
                    FileCount = $"File Downloaded: {value}";
                });

                await Task.Run(() =>
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        Thread.Sleep(100);

                        progress.Report(i);
                    }
                });
            }
            finally
            {
                IsDownloading = false;
            }
        }

        private void Hiii()
        {
            MessageBox.Show("Hii");
        }
    }
}
