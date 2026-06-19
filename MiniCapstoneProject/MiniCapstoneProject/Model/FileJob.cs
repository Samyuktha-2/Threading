using MiniCapstoneProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCapstoneProject.Model
{
    class FileJob : BaseVM
    {
        private string _status;

        public int Id { get; set; }
        public string FileName { get; set; }
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
    }
}
