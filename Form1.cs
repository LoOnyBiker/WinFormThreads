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
        private SynchronizationContext syncContext;

        public Form1()
        {
            InitializeComponent();

            syncContext = SynchronizationContext.Current;

            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
            Load += Form1_Load;
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

            var cancelled = await Task<bool>.Factory.StartNew(sim.Work);
            simulationComplete(cancelled);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (sim != null)
                sim.Cancel();
        }

        private void simulationComplete(bool cancelled)
        {
            string msg = cancelled ? "Process cancelled." : "Progress completed";
            MessageBox.Show(msg);
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void progressChanged(int progressValue)
        {
            syncContext.Post(o => progressBar1.Value = progressValue, null);
        }
    }
}
