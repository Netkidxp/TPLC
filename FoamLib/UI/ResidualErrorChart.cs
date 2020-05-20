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
    public partial class ResidualErrorChart : UserControl
    {
        public enum ResidualType
        {
            Initial,
            Final
        }

        public enum ErrorType
        {
            Sum,
            Global,
            Cumulative
        }
        struct DoubleAndInt
        {
            public double dv;
            public double iv;
        }

        class ResDic : Dictionary<string,DoubleAndInt>
        {
            public void Set(string name,double value)
            {
                DoubleAndInt v;
                if (ContainsKey(name))
                {
                    DoubleAndInt ov = this[name];
                    v.dv = ov.dv + value;
                    v.iv = ov.iv + 1;
                    Remove(name);
                }
                else
                {
                    v.dv = value;
                    v.iv = 1;
                }
                Add(name, v);
            }

            public double GetAverage(string name)
            {
                if (!ContainsKey(name))
                    return 0;
                else
                    return this[name].dv / this[name].iv;
            }
        }
        const string rpTime = @"\sTime = (\S+)\s";
        const string rpEnd = @"\sEnd\s";
        const string rpRes = @" Solving for (\S+), Initial residual = (\S+), Final residual = (\S+), No Iterations (\S+)\s";
        const string rpErr = @"\stime step continuity errors : sum local = (\S+), global = (\S+), cumulative = (\S+)\s";

        ColorPool colorPool = new ColorPool();
        string buffer = "";
        string targetFileName = "";
        uint maxPointCount = 10000;
        Timer timer = new Timer();
        StreamReader reader = null;
        ResidualType residualType = ResidualType.Initial;
        ErrorType errorType = ErrorType.Cumulative;
        public uint MaxPointCount { get => maxPointCount; set => maxPointCount = value; }
        public ResidualType Residual { get => residualType; set => residualType = value; }
        public ErrorType Error { get => errorType; set => errorType = value; }
        

        private void DoMonite(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                reader = new StreamReader(fs, new UTF8Encoding(false));
                while (!reader.EndOfStream)
                {
                    ReceiveLogText(reader.ReadLine());
                }
            }
        }
        private Task Monite()
        {
            Task task = Task.Run(() =>
            {
                DoMonite(targetFileName);
            });
            return task;
        }

        public async Task AsyncMonite(string fileName, bool enableTimer = false)
        {
            if (timer.Enabled)
                return;
            targetFileName = fileName;
            chart.Series.Clear();
            await Monite();
            if(enableTimer)
                timer.Enabled = true;
        }

        public async Task AsyncMonite(string fileName, int interval)
        {
            if (timer.Enabled)
                return;
            targetFileName = fileName;
            chart.Series.Clear();
            await Monite();
            timer.Interval = interval;
            timer.Enabled = true;
        }

        public void StopMonite()
        {
            timer.Enabled = false;
            if (reader!=null)
            {
                reader.Close();
                reader = null;
            }
        }

        private void Tick(object o, EventArgs e)
        {
            if(reader == null && File.Exists(targetFileName))
            {
                FileStream fs = new FileStream(targetFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                reader = new StreamReader(fs, new UTF8Encoding(false));
            }
            if(reader!=null)
            {
                string line = reader.ReadToEnd();
                ReceiveLogText(line);
            }
        }

        public void Clear()
        {
            chart.Series.Clear();
        }
        public ResidualErrorChart()
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
            {
                var points = GetSeries(name).Points;
                if (points.Count > 0 && points.Last().XValue > time)
                    return;
                points.AddXY(time, Math.Log10(value));
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

        private void ReceiveLogText(string logText)
        {
            string tmp = buffer + logText + "\r\n";
            buffer = Decode(tmp);
        }

        private string Decode(string str)
        {
            var timeMatchs = Regex.Matches(str, rpTime);
            var endMatch = Regex.Match(str, rpEnd);
            if (timeMatchs.Count == 0)
            {
                return str;
            }
            else if (timeMatchs.Count == 1)
            {
                if (endMatch.Success)
                {
                    DecodeTimeStep(str);
                    return "";
                }
                return str;
            }
            else
            {
                string firstTimeLog = str.Substring(timeMatchs[0].Index, timeMatchs[1].Index - timeMatchs[0].Index);
                DecodeTimeStep(firstTimeLog);
                return Decode(str.Substring(timeMatchs[1].Index));
            }
        }
        private void DecodeTimeStep(string str)
        {
            var match = Regex.Match(str, rpTime);
            var time = double.Parse(match.Groups[1].Value);
            ResDic res = new ResDic();
            var matchs = Regex.Matches(str, rpErr);
            foreach(Match m in matchs)
            {
                double sum = double.Parse(m.Groups[1].Value);
                double global = double.Parse(m.Groups[2].Value);
                double cumulative = double.Parse(m.Groups[3].Value);
                switch(errorType)
                {
                    case ErrorType.Cumulative:
                        res.Set("continuity", sum);
                        break;
                    case ErrorType.Global:
                        res.Set("continuity", global);
                        break;
                    case ErrorType.Sum:
                        res.Set("continuity", cumulative);
                        break;
                }
            }
            matchs = Regex.Matches(str, rpRes);
            foreach (Match m in matchs)
            {
                double initial = double.Parse(m.Groups[2].Value);
                double final = double.Parse(m.Groups[3].Value);
                switch(residualType)
                {
                    case ResidualType.Final:
                        res.Set(m.Groups[1].Value , final);
                        break;
                    case ResidualType.Initial:
                        res.Set(m.Groups[1].Value , initial);
                        break;
                }
            }
            foreach(string name in res.Keys)
            {
                AddPoint(name, time, res.GetAverage(name));
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            timer.Enabled = false;
            timer.Stop();
            base.OnHandleDestroyed(e);
        }
    }
    
}
