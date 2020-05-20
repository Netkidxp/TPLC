using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoamLib.Util
{
    public class ToolStripItemManager
    {
        Control father = null;
        public class ItemGroup
        {
            private int refCount = 0;
            private List<ToolStripItem> items = new List<ToolStripItem>();
            private delegate void DgSetEnable(ToolStripItem item, bool enable);
            Control father = null;
            public ItemGroup(List<ToolStripItem> items, Control father)
            {
                Items = items;
                this.father = father;
            }

            public List<ToolStripItem> Items { get => items; set => items = value; }

            public void SetCount(bool inc)
            {
                if (inc)
                    refCount++;
                else
                    refCount--;
                //if (count > 0)
                    //count = 0;
                foreach(ToolStripItem it in items)
                {
                    SetItemEnable(it, refCount >= 0);
                }
                
            }
            public void SetCount(int count)
            {
                this.refCount = count;
                foreach (ToolStripItem it in items)
                {
                    SetItemEnable(it, count >= 0);
                }
            }

            public void SetItemEnable(ToolStripItem item, bool enable)
            {
                
                if(father.InvokeRequired)
                {
                    DgSetEnable ds = new DgSetEnable(SetItemEnable);
                    father.Invoke(ds, new object[] { item, enable });
                }
                else
                    item.Enabled = enable;
            }

            public void SetItemVisible(ToolStripItem item, bool visible)
            {

                if (father.InvokeRequired)
                {
                    DgSetEnable ds = new DgSetEnable(SetItemVisible);
                    father.Invoke(ds, new object[] { item, visible });
                }
                else
                    item.Visible = visible;
            }

            public void Enable()
            {
                SetCount(true);
            }
            public void Disable()
            {
                SetCount(false);
            }
            public void Show()
            {
                foreach (ToolStripItem it in items)
                {
                    SetItemVisible(it, true);
                }
            }
            public void Hide()
            {
                foreach (ToolStripItem it in items)
                {
                    SetItemVisible(it, false);
                }
            }
        }
        private Dictionary<string, ItemGroup> groups = new Dictionary<string, ItemGroup>();

        public ToolStripItemManager(Control father)
        {
            this.father = father;
        }

        public void Add(string name, List<ToolStripItem> items)
        {
            groups.Add(name, new ItemGroup(items, father));
        }
        public void Remove(string name)
        {
            if(groups.ContainsKey(name))
                groups.Remove(name);
        }
        public void Enable(params string[] names)
        {
            foreach (string name in names)
            {
                Enable(name);
            }
        }
        public void Disable(params string[] names)
        {
            foreach (string name in names)
            {
                Disable(name);
            }
        }
        public void Enable(List<string> names)
        {
            foreach (string name in names)
            {
                Enable(name);
            }
        }
        public void Disable(List<string> names)
        {
            foreach (string name in names)
            {
                Disable(name);
            }
        }
        public void Enable(string name)
        {
            if (groups.ContainsKey(name))
            {
                ItemGroup ig = groups[name];
                ig.Enable();
            }
        }
        public void Disable(string name)
        {
            if (groups.ContainsKey(name))
            {
                ItemGroup ig = groups[name];
                ig.Disable();
            }
        }
        public void Set(string name, bool inc)
        {
            if (groups.ContainsKey(name))
            {
                ItemGroup ig = groups[name];
                ig.SetCount(inc);
            }
        }
        public void Set(string name, int count)
        {
            if (groups.ContainsKey(name))
            {
                ItemGroup ig = groups[name];
                ig.SetCount(count);
            }
        }
        public void Set(List<string> names, int count)
        {
            foreach(string name in names)
            {
                Set(name, count);
            }
        }
        public void Set(List<string> names, bool inc)
        {
            foreach (string name in names)
            {
                Set(name,inc);
            }
        }
        public void Set(string[] names, bool inc)
        {
            foreach (string name in names)
            {
                Set(name, inc);
            }
        }
        public void Set(string[] names, int count)
        {
            foreach (string name in names)
            {
                Set(name, count);
            }
        }
        public void EnbaleAll()
        {
            foreach(ItemGroup ig in groups.Values)
            {
                ig.Enable();
            }
        }
        public void DisableAll()
        {
            foreach (ItemGroup ig in groups.Values)
            {
                ig.Disable();
            }
        }
        public void Show(string name)
        {
            if (groups.ContainsKey(name))
            {
                ItemGroup ig = groups[name];
                ig.Show();
            }
        }
        public void Hide(string name)
        {
            if (groups.ContainsKey(name))
            {
                ItemGroup ig = groups[name];
                ig.Hide();
            }
        }
        public void Show(params string[] names)
        {
            foreach (string name in names)
            {
                Show(name);
            }
        }
        public void Hide(params string[] names)
        {
            foreach (string name in names)
            {
                Hide(name);
            }
        }
    }
}
