using System;
using System.Threading;

namespace WinFormThreads
{
    public sealed class WorkSimulator
    {

        private bool _cancelled = false;

        public void Work()
        {
            for (int i = 0; i < 100; i++)
            {
                if (_cancelled)
                    break;
                Thread.Sleep(100);
                progressChanged(i);
            }
            isWorkComplite(_cancelled);
        }

        public void Cancel()
        {
            _cancelled = true;
        }

        public event Action<int> progressChanged;
        public event Action<bool> isWorkComplite;

    }
}