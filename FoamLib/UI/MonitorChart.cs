using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.IO;
using FoamLib.Util;

namespace FoamLib.UI
{
    public partial class MonitorChart : UserControl
    {
        const string rpVector = @"\s+(\S+)\s+\((\S+)\s+(\S+)\s+(\S+)\)";
        const string rpScalar = @"\s+(\S+)\s+(\S+)";
        Dictionary<string, StreamReader> readers = new Dictionary<string, StreamReader>();
        ColorPool colorPool = new ColorPool();
        string buffer = "";
        string targetDirName = "";
        uint maxPointCount = 10000;
        Timer timer = new Timer();
        public uint MaxPointCount { get => maxPointCount; set => maxPointCount = value; }

        private void FillReaders()
        {
            if (!Directory.Exists(targetDirName))
                return;
            DirectoryInfo folder = new DirectoryInfo(targetDirName);
            foreach (FileInfo fi in folder.GetFiles())
            {
                if (fi.Name != "." && fi.Name != "..")
                {
                    if (readers.ContainsKey(fi.Name))
                        continue;
                    FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader reader = new StreamReader(fs, new UTF8Encoding(false));
                    readers.Add(fi.Name, reader);
                }
            }
        }
        private Task Monite()
        {
            Task task = Task.Run(() =>
            {
                FillReaders();
                foreach (var p in readers)
                {
                    while (!p.Value.EndOfStream)
                    {
                        DecodeLine(p.Key, p.Value.ReadLine());
                    }
                }
            });
            return task;
        }

        public async Task AsyncMonite(string dirName, bool enableTimer = false)
        {
            if (timer.Enabled)
                return;
            targetDirName = dirName;
            chart.Series.Clear();
            await Monite();
            if(enableTimer)
                timer.Enabled = true;
        }

        public async Task AsyncMonite(string dirName, int interval)
        {
            if (timer.Enabled)
                return;
            targetDirName = dirName;
            chart.Series.Clear();
            await Monite();
            timer.Interval = interval;
            timer.Enabled = true;
        }

        public void StopMonite()
        {
            timer.Enabled = false;
            foreach(var r in readers.Values)
            {
                r.Close();
            }
            readers.Clear();
        }

        private void Tick(object o, EventArgs e)
        {
            FillReaders();
            foreach (var p in readers)
            {
                while (!p.Value.EndOfStream)
                {
                    DecodeLine(p.Key, p.Value.ReadLine());
                }
            }
        }

        public MonitorChart()
        {
            InitializeComponent();
            chart.Series.Clear();
            timer.Tick += new EventHandler(Tick);
            timer.Start();
            timer.Enabled = false;
        }

        private delegate void DgAddPoint(string name, double time, double value);
        private void AddPoint(string name,double time,double value)
        {
            if(chart.InvokeRequired)
            {
                DgAddPoint dg = new DgAddPoint(AddPoint);
                chart.Invoke(dg, new object[] { name, time, value });
            }
            else
            {;
                var points = GetSeries(name).Points;
                if (points.Count > 0 && points.Last().XValue > time)
                    return;
                points.AddXY(time, value);
            }
        }

        private delegate Series DgGetSeries(string name);
        private Series GetSeries(string name)
        {
            if(chart.InvokeRequired)
            {
                DgGetSeries dg = new DgGetSeries(GetSeries);
                return (Series)chart.Invoke(dg, new object[] { name });
            }
            else
            {
                Series series = chart.Series.FindByName(name);
                if (series == null)
                {
                    series = new Series(name);
                    series.Color = colorPool.Next();
                    series.ChartType = SeriesChartType.Line;
                    series.Name = name;
                    series.ChartArea = "ChartArea1";
                    chart.Series.Add(series);
                }
                return series;
            }
        }

        private void DecodeLine(string name, string str)
        {
            if (str.StartsWith("#"))
                return;
            Match m = Regex.Match(str, rpVector);
            if(m.Success)
            {
                double time = double.Parse(m.Groups[1].Value);
                double u = double.Parse(m.Groups[2].Value);
                double v = double.Parse(m.Groups[3].Value);
                double w = double.Parse(m.Groups[4].Value);
                AddPoint(name + ".u", time, u);
                AddPoint(name + ".v", time, v);
                AddPoint(name + ".w", time, w);
            }
            else
            {
                m = Regex.Match(str, rpScalar);
                if(m.Success)
                {
                    double time = double.Parse(m.Groups[1].Value);
                    double v = double.Parse(m.Groups[2].Value);
                    AddPoint(name, time, v);
                }
            }

        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            timer.Enabled = false;
            timer.Stop();
            base.OnHandleDestroyed(e);
        }

        protected override void DestroyHandle()
        {
            timer.Enabled = false;
            foreach (var r in readers.Values)
            {
                r.Close();
            }
            readers.Clear();
            base.DestroyHandle();
        }
    }
    
}
