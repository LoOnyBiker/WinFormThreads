using System;
using System.Threading;

namespace WinFormThreads
{
    public sealed class WorkSimulator
    {

        private bool _cancelled = false;

        public bool Work()
        {
            for (int i = 0; i < 100; i++)
            {
                if (_cancelled)
                    break;

                Thread.Sleep(100);
                OnProgressChange(i);
            }
            return _cancelled;
        }

        public void Cancel()
        {
            _cancelled = true;
        }

        public void OnProgressChange(object i)
        {
            if (progressChanged != null)
                progressChanged( (int) i );
        }

        public event Action<int> progressChanged;

    }
}