using Freezable.Command;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Freezable.ViewModel
{
    class MainVM : BaseVM
    {
        private SolidColorBrush _buttonBackground;

        public SolidColorBrush ButtonBackground
        {
            get => _buttonBackground;
            set
            {
                _buttonBackground = value;
                OnPropertyChanged(nameof(ButtonBackground));
            }
        }

        public ICommand ButtonCommand { get; }

        public MainVM()
        {
            ButtonCommand = new RelayCommand(BtnStart);
        }

        private void BtnStart()
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.Red); 
            brush.Color = Colors.Magenta;
            brush.Freeze();
            //MessageBox.Show($"Is Frozen: {brush.IsFrozen}");  //--> true 
            //brush.Color = Colors.Blue;  //--> Raise error
            ButtonBackground = brush; 
        }
    }
}
