using FoamLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class OpenSCadWriter : GeometryWriter
    {
        public OpenSCadWriter(LadleGeometry geo, GlobalConfig config) : base(geo, config)
        {
        }
        const string code = @"
            module ladle_body_bht(bd,height,td)
            {
                s0 = td[0] / bd[0];
                s1 = td[1] / bd[1];
                linear_extrude(height = height, scale=[s0,s1], $fn=100)
                resize(bd)
                circle(r = 1);
            }
            module plugs(ps)
            {
                for(p = ps)
                    plug(p);
            }
            module plug(p)
            {
                linear_extrude(height = 0.01, $fn=100)
                translate([p[0],p[1],-0.005])
                circle(d = p[2]);
            }
            module ladle(bd,height,td,ps)
            {
                difference()
                {
                    ladle_body_bht(bd,height,td);
                    plugs(ps);
                }
            }
            rotate(a = -90, v = [1, 0, 0])
            ladle(bd,height,td,ps);
        ";
        public override void Write(string fileName)
        {
            string tempFile = Path.GetTempFileName()+".scad";
            StreamWriter sw = new StreamWriter(tempFile, false,new UTF8Encoding(false));
            sw.Write(code);
            sw.Close();
            var ps = "[";
            for (int i = 0; i < geo.Plugs.Count; i++)
            {
                var p = geo.Plugs[i];
                if (i == 0)
                    ps += p.ToString();
                else
                    ps += "," + p.ToString();
            }
            ps += "]";
            var arg = string.Format("-D bd=[{0},{1}] -D height={2} -D td=[{3},{4}] -D ps={5} -o \"{6}\" \"{7}\"",
                geo.BottomDiameterX,
                geo.BottomDiameterZ,
                geo.Height,
                geo.TopDiameterX,
                geo.TopDiameterZ,
                ps,
                fileName,
                tempFile);
            var app = "openscad.exe";
            Runner stlRunner = new Runner(config);
            stlRunner.Run(app, arg, Directory.GetParent(fileName).FullName);
        }
    }
}
