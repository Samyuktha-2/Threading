using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        } 
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for(int i = 1; i <= 100; i++)
            {

                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Thread.Sleep(500);
                backgroundWorker1.ReportProgress(i); 
            }
            Thread.Sleep(2000);
        } 
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = $"Progress: {e.ProgressPercentage} %";
        }  
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "Progress cancelled";
            }
            else
            {
                label1.Text = "Progress Completed";
            }
        } 
        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
    }
}
