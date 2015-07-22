using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormThreads
{
    public partial class Form1 : Form
    {
        private WorkSimulator sim;
        private TaskScheduler sheduler;

        public Form1()
        {
            InitializeComponent();

            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
            btnStop.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            sim = new WorkSimulator();
            sim.progressChanged += progressChanged;

            btnStart.Enabled = false;
            btnStop.Enabled = true;

            //sim.Work();   // One thread version

            //Thread thread = new Thread(sim.Work);
            //thread.Start();

            var cancelled = await Task<bool>.Factory.StartNew(sim.Work);

            this.isInvokeRequired(() =>
            {
                string msg = cancelled ? "Process cancelled." : "Progress completed";
                MessageBox.Show(msg);
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (sim != null)
                sim.Cancel();
        }

        private void progressChanged(int progressValue)
        {
            this.isInvokeRequired(() =>
            {
                progressBar1.Value = progressValue;
            });
        }
    }
}
