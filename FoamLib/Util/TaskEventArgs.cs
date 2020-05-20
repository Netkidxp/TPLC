using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class TaskEventArgs<T> : EventArgs
    {
        private Dictionary<string, Object> objects = new Dictionary<string, object>();
        public TaskEventArgs()
        {
        }

        public TaskEventArgs(T result)
        {
            Result = result;
        }

        public Dictionary<string, object> Objects { get => objects; set => objects = value; }
        public T Result { get; set; }
    }
}
