using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class MessageManager<T>:IDisposable
    {
        ConcurrentQueue<T> queue;
        CancellationTokenSource cts;
        int delay;

        public int Delay { get => delay; set => delay = value; }

        public MessageManager()
        {
            this.queue = new ConcurrentQueue<T>();
            this.cts = new CancellationTokenSource();
            this.Delay = 5000;
        }
        public async Task AsyncStartProcesser()
        {
            await StartProcessor();
        }
        public Task StartProcessor()
        {
            Task t = Task.Run(() =>
            {
                do
                {
                    T msg;
                    bool dequeueSuccesful = queue.TryDequeue(out msg);
                    if (dequeueSuccesful)
                    {
                        Processor(msg);
                    }
                    Task.Delay(Delay);
                }
                while (!cts.Token.IsCancellationRequested);
            });
            return t;
        }
        public void StopProcessor()
        {
            cts.Cancel();
        }
        public void Push(T msg)
        {
            queue.Enqueue(msg);
        }

        public virtual void Processor(T msg)
        {
        }

        public void Dispose()
        {
            StopProcessor();
        }
    }
}
