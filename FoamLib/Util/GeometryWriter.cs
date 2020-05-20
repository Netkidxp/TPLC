using FoamLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public abstract class GeometryWriter
    {
        protected LadleGeometry geo;
        protected GlobalConfig config;

        public GeometryWriter(LadleGeometry geo, GlobalConfig config)
        {
            this.geo = geo;
            this.config = config;
        }
        public abstract void Write(string fileName);
    }
}
