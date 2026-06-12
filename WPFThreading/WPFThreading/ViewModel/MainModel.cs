using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Timers;
using WPFThreading.Command;

namespace WPFThreading.ViewModel
{
    class MainModel : BaseVM
    {
        private string _timerLabel;

        public string TimerLabel
        {
            get => _timerLabel; set
            {
                _timerLabel = value;
                OnPropertyChanged(nameof(TimerLabel));
            }
        }

        public ICommand StartCommand { get; }

        public MainModel()
        {
            StartCommand = new RelayCommand(()=> { LoadDataAsync(); });
        }
         
        private async Task LoadDataAsync()
        {
            await Task.Delay(2000);
            TimerLabel = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
