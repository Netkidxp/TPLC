using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoamLib.IO
{
    [Serializable]
    public class FoamDictionary : List<KeyValuePair<string, FoamDictionary>>
    {
        const string pnValue = @"\S[^;]*;";
        const string pnDict = @"\{(((?<dict>\{)|(?<-dict>\})|[^{}])*(?(dict)(?!)))\}";
        const string pnList = @"\((((?<list>\()|(?<-list>\))|[^()])*(?(list)(?!)))\);";
        const string pnInnerLittleBrace = @"\((((?<list>\()|(?<-list>\))|[^()])*(?(list)(?!)))\)";
        const string pnFirstWordHead = @"\S";
        const string pnSecondWordHead = @"\S\s+\S";
        const string pnFunction = @"#\S+\b";
        const string pnSimple = @"\S+\b";//
        const string pnInDQuot = "\"[^\"]+\"";
        const string pnInSQuot = "\'[^\']+\'";
        const string pnInLittleBrace = @"\([^)]+\)";


        private string data = "";
        private uint level = 0;
        protected string prefix = "";
        private IMonitor monitor = null;
        private static FoamDictionary Null = new FoamDictionary();
        private bool isNull = false;
        private bool isList = false;
        public uint Level
        {
            get => level;
            set
            {
                level = value;
                prefix = "";
                for (uint i = 0; i < level; i++)
                    prefix += FoamConst.TAB;
                foreach(KeyValuePair<string,FoamDictionary> p in this)
                {
                    p.Value.Level = value + 1;
                }
            }
                
        }
        public string Data
        {
            get => data;
            set
            {
                Clear();
                /*
                string s = value;
                s = FoamConst.ClearAnno(s);
                s = FoamConst.ClearEnter(s);
                int pos = 0, len = 0;
                uint lev = level;
                pos = FindSubDictionary(s, ref len);
                if(pos == -1)
                {
                    s = s.Replace(FoamConst.END, "");
                    data = s;
                }
                else
                {
                    string subdicstr = s.Substring(pos + 1, len - 1);
                    subdicstr = subdicstr.Trim();
                    FoamDictionary subdic = Decode(subdicstr);
                    if(subdic.IsNull)
                    {
                        string err = string.Format("Dictionary::setData error:\n[type:\t{0}\nsrc:\t{1}\n]", "data is null dictionary", subdicstr);
                        Error(err);
                        this.isNull = true;
                    }
                    else
                    {
                        this.CopyFrom(subdic);
                        Level = lev;
                    }
                }
                */
                data = value;
            }
        }
        protected IMonitor Monitor { get => monitor; set => monitor = value; }
        public FoamDictionary()
        {
            this.Monitor = null;
            this.data = "";
            this.level = 0;
            this.prefix = "";
            this.isNull = false;
            this.isList = false;
        }
        public FoamDictionary(IMonitor mon = null):base()
        {
            this.Monitor = mon;
            this.data = "";
            this.level = 0;
            this.prefix = "";
            this.isNull = false;
            this.isList = false;
        }
        public FoamDictionary(bool isList,IMonitor mon = null)
        {
            this.Monitor = mon;
            this.data = "";
            this.level = 0;
            this.prefix = "";
            this.isNull = false;
            this.isList = isList;
        }
        public FoamDictionary(FoamDictionary dic)
        {
            CopyFrom(dic);
        }
        public void CopyFrom(FoamDictionary dic)
        {
            this.Clear();
            this.monitor = dic.monitor;
            this.data = dic.data;
            //this.level = dic.level;
            this.isNull = dic.IsNull;
            this.prefix = dic.prefix;
            this.isList = dic.isList;
            foreach (KeyValuePair<string, FoamDictionary> it in dic)
            {
                this.SetChild(it.Key,it.Value);
            }
        }
        public FoamDictionary Duplicate()
        {
            FoamDictionary d = new FoamDictionary(Monitor);
            d.data = this.data;
            //d.level = this.level;
            d.isNull = this.IsNull;
            d.prefix = this.prefix;
            d.isList = this.isList;
            foreach (KeyValuePair<string, FoamDictionary> it in this)
            {
                d.SetChild(it.Key, it.Value);
            }
            return d;
        }
        protected void Error(string err)
        {
            if (Monitor != null)
                Monitor.ErrorLine(err);
        }
        protected void Log(string inf)
        {
            if(Monitor != null)
                Monitor.LogLine(inf);
        }
        protected void Progress(float point)
        {
            if (Monitor != null)
                Monitor.Progress(point);
        }
        private static FoamDictionary ExtractFunction(string funName, string parStr, IMonitor mon = null)
        {
            FoamDictionary d = new FoamDictionary(mon);
            return d;
        }
        private static string CaptureFirstChar(string src)
        {
            Match m = Regex.Match(src, pnFirstWordHead);
            if (!m.Success)
                return "";
            else
                return m.Captures[0].Value;
        }
        private static Capture CaptureName(string src)
        {
            Match matchName = null;
            string firstNameChar = CaptureFirstChar(src);
            if (firstNameChar == "")
            {
                return null;
            }
            switch (firstNameChar)
            {
                case "(":
                    matchName = Regex.Match(src, pnInLittleBrace);
                    break;
                case "\"":
                    matchName = Regex.Match(src, pnInDQuot);
                    break;
                case "\'":
                    matchName = Regex.Match(src, pnInSQuot);
                    break;
                //case "#":
                default:
                    matchName = Regex.Match(src, pnSimple);
                    break;
            }
            if (!matchName.Success)
            {
                return null;
            }
            return matchName.Captures[0];
        }
        private static FoamDictionary DecodeList(string src, bool isExtractFun, IMonitor mon = null)
        {
            string s = src;
            s = FoamConst.ClearAnno(s);
            s = FoamConst.ClearEnter(s);
            FoamDictionary d = new FoamDictionary(mon);
            d.isList = true;
            while(!IsEmpty(s))
            {
                Capture capturefirst = CaptureName(s);
                if (capturefirst == null)
                    break;
                string firstStr = capturefirst.Value;
                s = s.Substring(capturefirst.Index + capturefirst.Length);
                string secondChar = CaptureFirstChar(s);
                if(secondChar == "{")
                {
                    Match matchDict = Regex.Match(s, pnDict);
                    if (!matchDict.Success)
                    {
                        if (mon != null)
                        {
                            mon.ErrorLine("decode.decode_list.child_dict failed");
                            mon.ErrorLine(string.Format("src: {0}", s));
                        }
                        return NULL;
                    }
                    Capture captureDict = matchDict.Captures[0];
                    string dictBody = captureDict.Value;
                    dictBody = dictBody.Substring(1, dictBody.Length - 2);
                    FoamDictionary dDict = FoamDictionary.DecodeDictionary(dictBody, isExtractFun, mon);
                    if (dDict.isNull)
                    {
                        if (mon != null)
                        {
                            mon.ErrorLine("decode.match_body.decode failed");
                            mon.ErrorLine(string.Format("src: {0}", s));
                        }
                        return NULL;
                    }
                    d.SetChild(firstStr, dDict);
                    s = s.Substring(captureDict.Index + captureDict.Length);
                }
                else
                {
                    d.SetChild(d.Count.ToString(), firstStr);
                }
            }
            return d;
        }
        public static FoamDictionary DecodeDictionary(string src, bool isExtractFun,IMonitor mon = null)
        {

            string s = src;
            s = FoamConst.ClearAnno(s);
            s = FoamConst.ClearEnter(s);
            FoamDictionary d = new FoamDictionary(mon);
            while (!IsEmpty(s))
            {
                Capture captureName = CaptureName(s);
                if(captureName==null)
                {
                    if (mon != null)
                    {
                        mon.ErrorLine("decode.capture_name failed, captured name is empty");
                        mon.ErrorLine(string.Format("src: {0}", s));
                    }
                    return NULL;
                }
                string entryName = captureName.Value;
                s = s.Substring(captureName.Index + captureName.Length);
                if(entryName.StartsWith("#"))
                {
                    string functionFirstChar = CaptureFirstChar(s);
                    if(functionFirstChar=="")
                    {
                        if (mon != null)
                        {
                            mon.ErrorLine("decode.extract function failed, body first char is not \"");
                            mon.ErrorLine(string.Format("src: {0}", s));
                        }
                        return NULL;
                    }
                    Match matchFunctionPar = Regex.Match(s, pnInDQuot);
                    if(!matchFunctionPar.Success)
                    {
                        if (mon != null)
                        {
                            mon.ErrorLine("decode.extract function failed, can not match body in \"\"");
                            mon.ErrorLine(string.Format("src: {0}", s));
                        }
                        return NULL;
                    }
                    Capture captureFunPar = matchFunctionPar.Captures[0];
                    if(isExtractFun)
                    {
                        FoamDictionary dfun = ExtractFunction(entryName, captureFunPar.Value, mon);
                        if (dfun.IsNull)
                        {
                            if (mon != null)
                            {
                                mon.ErrorLine("decode.extract function failed, extracted directionary is null");
                                mon.ErrorLine(string.Format("src: {0}", s));
                            }
                            return NULL;
                        }
                        foreach (KeyValuePair<string, FoamDictionary> item in dfun)
                        {
                            d.SetChild(item.Key, item.Value);
                        }
                    }
                    else
                    {
                        d.SetChild(entryName, captureFunPar.Value);
                    }
                    s = s.Substring(captureFunPar.Index + captureFunPar.Length);
                }
                else if(entryName == ";")
                {
                    s = s.Substring(s.IndexOf(";") + 1);
                }
                else
                {
                    string firstBodyChar = CaptureFirstChar(s);
                    if (firstBodyChar == "")
                    {
                        if (mon != null)
                        {
                            mon.ErrorLine("decode.match_body failed");
                            mon.ErrorLine(string.Format("src: {0}", s));
                        }
                        return NULL;
                    }
                    if (firstBodyChar == "{")
                    {
                        Match matchDict = Regex.Match(s, pnDict);
                        if (!matchDict.Success)
                        {
                            if (mon != null)
                            {
                                mon.ErrorLine("decode.match_body.dictionary failed");
                                mon.ErrorLine(string.Format("src: {0}", s));
                            }
                            return NULL;
                        }
                        Capture captureDict = matchDict.Captures[0];
                        string dictBody = captureDict.Value;
                        dictBody = dictBody.Substring(1, dictBody.Length - 2);
                        FoamDictionary dDict = FoamDictionary.DecodeDictionary(dictBody,isExtractFun,mon);
                        if (dDict.isNull)
                        {
                            if (mon != null)
                            {
                                mon.ErrorLine("decode.match_body.decode failed");
                                mon.ErrorLine(string.Format("src: {0}", s));
                            }
                            return NULL;
                        }
                        d.SetChild(entryName, dDict);
                        s = s.Substring(captureDict.Index + captureDict.Length);
                    }
                    else if (firstBodyChar == "(")
                    {
                        Match matchList = Regex.Match(s, pnList);
                        if (!matchList.Success)
                        {
                            if (mon != null)
                            {
                                mon.ErrorLine("decode.match_body.list failed");
                                mon.ErrorLine(string.Format("src: {0}", s));
                            }
                            return NULL;
                        }
                        Capture captureList = matchList.Captures[0];
                        string listBody = captureList.Value;
                        listBody = listBody.Substring(1, listBody.LastIndexOf(')')-1);
                        FoamDictionary dlist = FoamDictionary.DecodeList(listBody, isExtractFun, mon);
                        if (dlist.isNull)
                        {
                            if (mon != null)
                            {
                                mon.ErrorLine("decode.match_body.decode_list failed");
                                mon.ErrorLine(string.Format("src: {0}", s));
                            }
                            return NULL;
                        }
                        dlist.isList = true;
                        d.SetChild(entryName, dlist);
                        s = s.Substring(captureList.Index + captureList.Length);
                    }
                    else
                    {
                        Match matchValue = Regex.Match(s, pnValue);
                        if (!matchValue.Success)
                        {
                            if (mon != null)
                            {
                                mon.ErrorLine("decode.match_body.value failed");
                                mon.ErrorLine(string.Format("src: {0}", s));
                            }
                            return NULL;
                        }
                        Capture captureValue = matchValue.Captures[0];
                        string valueText = captureValue.Value;
                        valueText = valueText.Substring(0, valueText.Length - 1);
                        d.SetChild(entryName, valueText);
                        s = s.Substring(captureValue.Index + captureValue.Length);
                    }
                }
            }
            return d;

        }
        public static FoamDictionary Decode(string src, IMonitor mon = null)
        {
            FoamDictionary dic = new FoamDictionary();
            string s = src;
            s = FoamConst.ClearAnno(s);
            s = FoamConst.ClearEnter(s);
            Regex r_name = new Regex("\\s?([^\\s\\{\\}]+)\\s");
            if(mon != null)
            {
                mon.Progress(0);
            }
            while(!IsEmpty(s))
            {
                Match m = r_name.Match(s);
                if (!m.Success)
                {
                    if(mon !=null)
                    {
                        mon.ErrorLine("decode entry name error");
                        mon.ErrorLine("src:\t" + s);
                    }
                    dic = NULL;
                    break;
                }
                else
                {
                    int pos = m.Index;
                    int len = 0;
                    string name = m.Captures[0].Value.Trim();
                    s = s.Substring(pos + m.Length, s.Length - pos - m.Length);
                    if(name.StartsWith(FoamConst.SHARP))
                    {
                        pos = FindFunctionValue(s, ref len);
                        if(pos == -1)
                        {
                            if (mon != null)
                            {
                                mon.ErrorLine("decode function value error");
                                mon.ErrorLine("value src:\t" + s);
                            }
                            dic = NULL;
                            break;
                        }
                        else
                        {
                            string vs = s.Substring(pos, len);
                            vs.Replace(FoamConst.END, "");
                            dic.SetChild(name, vs);
                            s = s.Substring(pos + len, s.Length - pos - len);
                        }
                    }
                    else
                    {
                        pos = FindValue(s, ref len);
                        if(pos == -1)
                        {
                            pos = FindSubDictionary(s, ref len);
                            if(pos == -1)
                            {
                                if (mon != null)
                                {
                                    mon.ErrorLine("decode entry value error");
                                    mon.ErrorLine("entry name:\t" + name);
                                    mon.ErrorLine("value src:\t" + s);
                                }
                                dic = NULL;
                                break;
                            }
                            else
                            {
                                string subdicstr = s.Substring(pos + 1, len - 2).Trim();
                                FoamDictionary subdic = Decode(subdicstr);
                                if(subdic.IsNull)
                                {
                                    if (mon != null)
                                    {
                                        mon.ErrorLine("decode sub dictionary error");
                                        mon.ErrorLine("entry name:\t" + name);
                                        mon.ErrorLine("sub dict src:\t" + subdicstr);
                                    }
                                    dic = NULL;
                                    break;
                                }
                                else
                                {
                                    dic.SetChild(name, subdic);
                                    s = s.Substring(pos + len + 1, s.Length - pos - len -1);
                                    if(mon != null )
                                    {
                                        mon.Progress(s.Length / src.Length);
                                    }
                                }

                            }
                        }
                        else
                        {
                            string vs = s.Substring(pos, len);
                            vs = vs.Replace(FoamConst.END, "");
                            dic.SetChild(name, vs);
                            s = s.Substring(pos + len, s.Length - pos - len).Trim();
                            if (mon != null)
                            {
                                mon.Progress(s.Length / src.Length);
                            }
                        }
                    }
                }
            }
            if (mon != null)
            {
                mon.Progress(1.0f);
            }
            dic.Monitor = mon;
            return dic;
        }
        private static int FindSubDictionary(string src, ref int len)
        {
            int l = -1;
            int r = -1;
            int level = 0;
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == FoamConst.LBRACE[0])
                {
                    if (level == 0)
                        l = i;
                    level++;
                }
                else if (src[i] == FoamConst.RBRACE[0])
                {
                    level--;
                    if (level == 0)
                        r = i;
                }
                if (level == 0 && r > l)
                    break;
            }
            if (level == 0 && r > l)
            {
                len = r - l;
                return l;
            }
            else
            {
                return -1;
            }
        }
        private static int FindValue(string src, ref int len)
        {
            int l = -1;
            int r = -1;
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == FoamConst.LBRACE[0] || src[i] == FoamConst.RBRACE[0])
                    return -1;
                else if (src[i] == FoamConst.END[0])
                {
                    r = i;
                    break;
                }
                else if (src[i] == FoamConst.SPACE[0])
                {
                    continue;
                }
                else
                {
                    if (l == -1)
                        l = i;
                }
            }
            len = r - l + 1;
            if (len >= 1)
                return l;
            else
            {
                return -1;
            }
        }
        private static int FindFunctionValue(string src,ref  int len)
        {
            int l = -1;
            int r = -1;
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == FoamConst.QUOTE[0])
                {
                    if (l == -1)
                    {
                        l = i;
                    }
                    else
                    {
                        r = i;
                        break;
                    }
                }
            }
            len = r - l + 1;
            if (len >= 2)
                return l;
            else
            {
                return -1;
            }
        }
        private static bool IsEmpty(string src)
        {
            /*
            if (src.Length == 0)
                return true;
            else
            {
                Regex r = new Regex("^[\\s]*$");
                return r.IsMatch(src);
            }
            */
            return string.IsNullOrWhiteSpace(src);
        }
        override public string ToString()
        {
            if (IsNull)
                return "";
            string s = "";
            if(IsValue)
            {
                s = FoamConst.TAB + data + FoamConst.END + FoamConst.ENTER;
            }
            else if(IsList)
            {
                if(this.Count==0)
                {
                    s = FoamConst.SPACE +FoamConst.LPARENTHESE + FoamConst.RPARENTHESE + FoamConst.END + FoamConst.ENTER;
                }
                else
                {
                    if (this.First().Value.IsValue)
                    {
                        s = FoamConst.SPACE + FoamConst.LPARENTHESE;
                        foreach (KeyValuePair<string, FoamDictionary> item in this)
                        {
                            string istr = item.Value.Data;
                            s += istr + " ";
                        }
                        s = s.Substring(0, s.Length - 1);
                        s += FoamConst.RPARENTHESE + FoamConst.END + FoamConst.ENTER;
                    }
                    else
                    {
                        s = FoamConst.ENTER + prefix + FoamConst.LPARENTHESE + FoamConst.ENTER;
                        foreach (KeyValuePair<string, FoamDictionary> p in this)
                        {
                            s += prefix + FoamConst.TAB + p.Key + p.Value.ToString();
                        }
                        s += prefix + FoamConst.RPARENTHESE + FoamConst.END + FoamConst.ENTER;
                    }
                }
            }
            else
            {
                if (IsRoot)
                {
                    foreach (KeyValuePair<string, FoamDictionary> p in this)
                    {
                        s += FoamConst.ENTER + prefix + FoamConst.TAB + p.Key + p.Value.ToString();
                    }
                }
                else
                {
                    s = FoamConst.ENTER + prefix + FoamConst.LBRACE + FoamConst.ENTER;
                    foreach (KeyValuePair<string, FoamDictionary> p in this)
                    {
                        s += prefix + FoamConst.TAB + p.Key + p.Value.ToString();
                    }
                    s += prefix + FoamConst.RBRACE + FoamConst.ENTER;
                }
            }
            return s;
        }
        public FoamDictionary SetChild(string name, FoamDictionary child)
        {
            if(name == "")
            {
                Error("FoamDictionary.SetChild error: child name is empty");
                return NULL;
            }
            if(child.IsNull)
            {
                Error("FoamDictionary.SetChild error: child name is Null, child name is" + name);
                return NULL;
            }
            FoamDictionary d = child.Duplicate();
            d.Level = Level + 1;
            int i = IndexOf(name);
            if(i==-1)
            {
                this.Add(new KeyValuePair<string, FoamDictionary>(name, d));
            }
            else
            {
                this[i] = new KeyValuePair<string, FoamDictionary>(name, d);
            }
            data = "";
            return Child(name);
        }
        public FoamDictionary SetChild(string name, string dat)
        {
            FoamDictionary d = SetChild(name, new FoamDictionary(Monitor));
            d.Data = dat;
            return d;
        }
        public FoamDictionary SetChild(string name, double v)
        {
            return SetChild(name, v.ToString());
        }
        public FoamDictionary SetChild(string name, int v)
        {
            return SetChild(name, v.ToString());
        }
        public FoamDictionary SetChild(string name, float v)
        {
            return SetChild(name, v.ToString());
        }
        public FoamDictionary SetChild(string name, uint v)
        {
            return SetChild(name, v.ToString());
        }
        public FoamDictionary AddChild(string name)
        {
            return SetChild(name, new FoamDictionary(Monitor));
        }
        public bool Equals(FoamDictionary dic)
        {
            if (Data != dic.Data)
                return false;
            if (Count != dic.Count)
                return false;
            for(int i=0;i<Count;i++)
            {
                if (this[i].Key != dic[i].Key)
                    return false;
                if (!this[i].Value.Equals(dic[i].Value))
                    return false;
            }
            return true;
        }
        public FoamDictionary GetByUrl(string url)
        {
            if (url == "")
                return NULL;
            int fir = url.IndexOf(FoamConst.SEP);
            if(fir == -1)
            {
                if (Contains(url))
                    return Child(url);
                else
                {
                    return AddChild(url);
                }
            }
            else
            {
                string name1 = url.Substring(0, fir);
                string name2 = url.Substring(fir + FoamConst.SEP.Length, url.Length - fir - FoamConst.SEP.Length);
                if(Contains(name1))
                {
                    return Child(name1).GetByUrl(name2);
                }
                else
                {
                    return AddChild(name1).GetByUrl(name2);
                }
            }
        }
        public FoamDictionary LookupByUrl(string url)
        {
            if (url == "")
                return NULL;
            int fir = url.IndexOf(FoamConst.SEP);
            if (fir == -1)
            {
                if (Contains(url))
                    return Child(url);
                else
                {
                    return NULL;
                }
            }
            else
            {
                string name1 = url.Substring(0, fir);
                string name2 = url.Substring(fir + FoamConst.SEP.Length, url.Length - fir - FoamConst.SEP.Length);
                if (Contains(name1))
                {
                    return Child(name1).LookupByUrl(name2);
                }
                else
                {
                    return NULL;
                }
            }
        }
        public List<string> GetChildenNames()
        {
            List<string> names = new List<string>();
            foreach(KeyValuePair<string,FoamDictionary> p in this)
            {
                names.Add(p.Key);
            }
            return names;
        }
        public void RemoveByUrl(string url)
        {
            int last = url.LastIndexOf(FoamConst.SEP);
            if(last == -1)
            {
                if (Contains(url))
                    RemoveChild(url);

            }
            else
            {
                string name1 = url.Substring(0, last);
                string name2 = url.Substring(last + FoamConst.SEP.Length, url.Length - last - FoamConst.SEP.Length);
                FoamDictionary d = LookupByUrl(name1);
                if (!d.IsNull)
                    d.RemoveChild(name2);
            }
        }
        public bool IsRoot
        {
            get => Level == 0;
        }
        public bool IsValue
        {
            get => Data != "";
        }
        public FoamDictionary Child(string name)
        {
            foreach(KeyValuePair<string,FoamDictionary> p in this)
            {
                if (p.Key == name)
                    return p.Value;
            }
            return NULL;
        }
        public FoamDictionary Child(int index)
        {
            if (index >= this.Count)
                return NULL;
            else
                return this[index].Value;
        }
        public int IndexOf(string name)
        {
            int res = -1;
            for (int i = 0; i < Count; i++)
                if (this[i].Key == name)
                    res = i;
            return res;
        }
        public bool Contains(string name)
        {
            return IndexOf(name) != -1;
        }
        public void RemoveChild(string name)
        {
            int i = IndexOf(name);
            if (i == -1)
                return;
            else
                RemoveAt(i);
        }
        public bool IsNull
        {
            get => (this == NULL || isNull);
        }
        public static FoamDictionary NULL { get => Null; }
        public bool IsList { get => isList;}
        public bool IsSubDictionary
        {
            get
            {
                return !IsList && !IsValue;
            }
        }
    }
}
