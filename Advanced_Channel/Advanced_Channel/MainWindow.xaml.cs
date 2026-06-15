using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
namespace Advanced_Channel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Channel<string>? _channel;
        private CancellationTokenSource? _cts;
        private Task? _task;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            lstLogs.Items.Clear();
            txtStatus.Text = "Running....";
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

            var options = new BoundedChannelOptions(1000)
            {
                SingleReader = true,
                SingleWriter = false
            };

            _chanel = Channel.CreateBounded<string>(options);
            _cts = new CancellationTokenSource();

            _task = ConsumeAsync(_chanel.Reader, _cts.Token);

            _ = Task.Run(() => ProduceAsync(_channel.Writer, _cts.Token));
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (_cts != null)
            {
                await _cts.CancelAsync();

                // Complete the writer to signal no more data is coming
                if (_channel != null)
                {
                    _channel.Writer.Complete();
                }

                // Wait for consumer to finish processing remaining items
                if (_task != null)
                {
                    await _consumerTask;
                }

                txtStatus.Text = "Stopped";
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
            }
        }

        private async Task ProduceAsync(ChannelWriter<string> writer, CancellationToken token)
        {
            try
            {
                int count = 0;
                while (!token.IsCancellationRequested)
                {
                    // Simulate heavy work or data fetching
                    await Task.Delay(100, token);

                    string message = $"Log Entry #{++count} - Timestamp: {DateTime.Now:HH:mm:ss.fff}";

                    // WriteAsync awaits if the channel is full (Backpressure)
                    // This prevents the producer from outrunning the consumer indefinitely
                    await writer.WriteAsync(message, token);
                }
            }
            catch (OperationCanceledException)
            {
                // Expected on cancellation
            }
            finally
            {
                // Ensure channel is marked complete if we exit loop
                writer.TryComplete();
            }
        }

        // CONSUMER: Runs asynchronously, updates UI directly
        private async Task ConsumeAsync(ChannelReader<string> reader, CancellationToken token)
        {
            try
            {
                // ReadAllAsync enumerates items as they arrive
                // The 'await' here captures the WPF SynchronizationContext
                // so the code inside the loop runs on the UI Thread.
                await foreach (var item in reader.ReadAllAsync(token))
                {
                    // SAFE: We are on the UI thread here
                    lstLogs.Items.Add(item);

                    // Auto-scroll to bottom
                    lstLogs.ScrollIntoView(lstLogs.Items[lstLogs.Items.Count - 1]);
                }
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation gracefully
            }
        }
    }
}


     

