using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using TimerExercise3.Command;
using TimerExercise3.Model;
using Timer = System.Timers.Timer;

namespace TimerExercise3.ViewModel
{
    class MainVM : BaseVM
    {
        private string _elapsedTime;
        private int _tickCount = 0;
        private int _currentThread;
        private int _threadLocalCount;
        private string _weakReference1;
        private string _employeeStatus;
        private string _gCStatus;
        private string _timerStatus;
        private string _weakReference2;

        public string ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                _elapsedTime = value;
                OnPropertyChanged(nameof(ElapsedTime));
            }
        }
        public int TickCounts
        {
            get => _tickCount;
            set
            {
                _tickCount = value;
                OnPropertyChanged(nameof(TickCounts));
            }
        }
        public int CurrentThreads
        {
            get => _currentThread;
            set
            {
                _currentThread = value;
                OnPropertyChanged(nameof(CurrentThreads));
            }
        }
        public int ThreadLocalCounts
        {
            get => _threadLocalCount;
            set
            {
                _threadLocalCount = value;
                OnPropertyChanged(nameof(ThreadLocalCounts));
            }
        }
        public string WeakReference1
        {
            get => _weakReference1;
            set
            {
                _weakReference1 = value;
                OnPropertyChanged(nameof(WeakReference1));
            }
        }
        public string WeakReference2
        {
            get => _weakReference2;
            set
            {
                _weakReference2 = value;
                OnPropertyChanged(nameof(WeakReference2));
            }
        }
        public string EmployeeStatus
        {
            get => _employeeStatus;
            set
            {
                _employeeStatus = value;
                OnPropertyChanged(nameof(EmployeeStatus));
            }
        }
        public string GCStatus
        {
            get => _gCStatus;
            set
            {
                _gCStatus = value;
                OnPropertyChanged(nameof(GCStatus));
            }
        }
        public string TimerStatus
        {
            get => _timerStatus;
            set
            {
                _timerStatus = value;
                OnPropertyChanged(nameof(TimerStatus));
            }
        }

        static Timer timer = new Timer(1000);
        static Stopwatch sw = Stopwatch.StartNew();
        static WeakReference<Employee> weakRef;
        static ThreadLocal<int> threadLocal = new ThreadLocal<int>(() => 0);
        public ObservableCollection<MainModel> TimerModel { get; set; }

        public ICommand StartCommand { get; }

        public MainVM()
        {
            TimerModel = new ObservableCollection<MainModel>();
            StartCommand = new RelayCommand(StartProcess);
        }
        private void StartProcess()
        {
            CreateNewEmployee();
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }
        private void CreateNewEmployee()
        {
            Employee emp = new Employee();
            weakRef = new WeakReference<Employee>(emp);
            EmployeeStatus = "Created";
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            TickCounts++;
            threadLocal.Value += 1;
            ThreadLocalCounts = threadLocal.Value;
            CurrentThreads = Thread.CurrentThread.ManagedThreadId;
            Application.Current.Dispatcher.Invoke(() =>
            {
                TimerModel.Add(new MainModel
                {
                    TickCount = TickCounts,
                    ElapsedTime = $"{sw.ElapsedMilliseconds} ms",
                    CurrentThread = CurrentThreads,
                    ThreadLocalCount = ThreadLocalCounts
                });
            });
            
            for (int i = 0; i < 100; i++)
            {
                byte[] buffer = new byte[1024];
            }
            if (TickCounts == 5)
            {
                GCStatus = "Forcing GC";
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GCStatus = "Garbage Collected";
                Employee emp;
                WeakReference1 = weakRef.TryGetTarget(out emp) ? "Alive" : "Dead";
            }

            if (TickCounts == 10)
            {
                timer.Stop();
                timer.Dispose();
                TimerStatus = "Timer Stopped";
                CheckWeakRef();
            }
            Thread.Sleep(1500);
        }
        private void CheckWeakRef()
        {
            Employee emp = new Employee();
            WeakReference<Employee> weak = new WeakReference<Employee>(emp);
            Employee e;
            WeakReference2 = weak.TryGetTarget(out e) ? "Alive" : "Dead";
        }
    }
    class Employee
    {
        public int Id;
        ~Employee()
        {
            Console.WriteLine($"Finaliser - {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
