using System;
using System.Threading;
using System.Windows.Forms;

namespace WinFormThreads
{
    public partial class Form1 : Form
    {
        private WorkSimulator sim;

        public Form1()
        {
            InitializeComponent();

            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            sim = new WorkSimulator();
            sim.progressChanged += progressChanged;
            sim.workComplite += simulationCompleted;

            btnStart.Enabled = false;
            btnStop.Enabled = true;

            //sim.Work();
            Thread thread = new Thread(sim.Work);
            thread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (sim != null)
                sim.Cancel();
        }

        private void simulationCompleted(bool cancelled)
        {
            Action act = () =>
            {
                string msg = cancelled ? "Process cancelled." : "Progress completed";
                MessageBox.Show(msg);
                btnStart.Enabled = true;
            };

            this.isInvokeRequired(act);
        }

        private void progressChanged(int progressValue)
        {
            Action act = () => { progressBar1.Value = progressValue; };
            this.isInvokeRequired(act);
        }
    }
}
