using FoamLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class FoamTask<T>
    {
        public event EventHandler<TaskEventArgs<T>> OnTaskStarted;
        public event EventHandler<TaskEventArgs<T>> OnTaskFinished;

        private TaskEventArgs<T> startedArgs = new TaskEventArgs<T>();
        private TaskEventArgs<T> finishedArgs = new TaskEventArgs<T>();

        public TaskEventArgs<T> StartedArgs { get => startedArgs; }
        public TaskEventArgs<T> FinishedArgs { get => finishedArgs; }

        protected void TaskStarted(TaskEventArgs<T> e)
        {
            if(OnTaskStarted!=null)
                OnTaskStarted(this, e);
        }
        protected void TaskFinished(TaskEventArgs<T> e)
        {
            if(OnTaskFinished!=null)
                OnTaskFinished(this,e);
        }
        private Task<T> _Do(Func<T> fun)
        {
            Task<T> task = Task<T>.Run(fun);
            return task;
        }
        public async Task Do(Func<T> fun)
        {
            TaskStarted(StartedArgs);
            FinishedArgs.Result = await _Do(fun);
            TaskFinished(FinishedArgs);
            StartedArgs.Objects.Clear();
            FinishedArgs.Objects.Clear();
        }
    }
}
