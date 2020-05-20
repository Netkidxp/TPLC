using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Model.CfMesh
{
    public class ModelList<T> : List<T>
    {
        public event EventHandler<ModelListEventArges> OnAdd;
        public event EventHandler<ModelListEventArges> OnRemove;
        public class ModelListEventArges : EventArgs
        {
            public ModelListEventArges(object item)
            {
                Item = item;
            }
            public object Item { get; set; }
        }
        new void Add(T o)
        {
            OnAdd?.Invoke(this, new ModelListEventArges(o));
            base.Add(o);
        }
        new bool Remove(T o)
        {
            OnRemove?.Invoke(this, new ModelListEventArges(o));
            return base.Remove(o);
        }
    }
}
