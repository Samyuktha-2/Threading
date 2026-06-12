using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Timers;

namespace WPFThreading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(() =>
            {
                Thread.Sleep(3000);
                Dispatcher.Invoke(() =>
                {
                    textbox1.Text = "Completed";
                });
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    btn1.Content = "Clicked";
                });

                bool canAccess = btn1.CheckAccess();
                //textblock3.Text = canAccess.ToString();
                Debug.WriteLine(canAccess);
            });

        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            btn2.Dispatcher.Invoke(() =>
            {
                btn2.Content = "Clicked";
            });

            if (btn2.CheckAccess())
            {
                bool canAccess = btn2.CheckAccess();

                textblock2.Text = "Button2 runs on UI Thread: " + canAccess.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
             

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            timer.Start();
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //Thread.Sleep(5000);  --> Adding this freezes the UI controls, as the UI thread is told to sleep for 5 seconds
            ClockLbl.Content = DateTime.Now.ToString("HH:mm:ss");
            // MessageBox.Show("Hello");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);

            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Thread.Sleep(2500);  //Doesn't freeze the ui as this runs on threadpool thread
            Dispatcher.Invoke(() =>
            {
                // Thread.Sleep(2500); --> freezes the ui as this start to run on UI thread
                ClockLbl2.Content = DateTime.Now.ToString("HH:mm:ss");
            });
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Threading.Timer timer = new System.Threading.Timer(CallBack, null, 0, 1000);
            
            //(Calls the method to execute, optional data passed to callback, time to wait before first execution, interval between execution)
        }

        private void CallBack(object sender)
        {
            Dispatcher.Invoke(() =>
            {
                ClockLbl3.Content = DateTime.Now.ToString("HH:mm:ss");
            });
        }
    }
}
