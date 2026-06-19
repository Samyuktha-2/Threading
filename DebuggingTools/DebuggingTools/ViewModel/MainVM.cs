using DebuggingTools.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace DebuggingTools.ViewModel
{
    class MainVM : BaseVM
    {
        public int Counter
        {
            get => _counter;
            set
            {
                _counter = value;
                OnPropertyChanged(nameof(Counter));
            }
        }

        public int taskId = 0;
        private int _counter;

        public ICommand StartWorkerCommand1 { get; }
        public ICommand StartWorkerCommand2 { get; }

        public MainVM()
        {
            StartWorkerCommand1 = new RelayCommand(StartWorker1);
            StartWorkerCommand2 = new RelayCommand(StartWorker2);
        }

        private void StartWorker1()
        {
            int counter = 0; 
            Parallel.For(0, 100000, i =>
            {
                int temp = counter;
                Thread.Yield();  //stress test
                counter = temp + 1;
            });
            Counter = counter;
        }

        private void StartWorker2()
        {
            for (int i = 0; i < 20; i++)
            {
                int taskId = i;

                Task.Run(() =>
                {
                    Debug.WriteLine($"Task: {taskId}");
                    Thread.Sleep(100);
                });
            }
        }
    }
}
