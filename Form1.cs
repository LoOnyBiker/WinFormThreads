using System;
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
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            sim = new WorkSimulator();
            sim.progressChanged += progressChanged;
            sim.isWorkComplite += simulationCompleted;

            btnStart.Enabled = false;
            sim.Work();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void simulationCompleted(bool cancelled)
        {
            string msg = cancelled ? "Process cancelled." : "Progress completed";
            MessageBox.Show(msg);
            btnStart.Enabled = true;
        }

        private void progressChanged(int progressValue)
        {
            progressBar1.Value = progressValue;
        }
    }
}
