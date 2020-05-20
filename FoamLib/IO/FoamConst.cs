using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace FoamLib.IO
{
    public static class FoamConst
    {
        public const string TAB = "\t";
        public const string SPACE = " ";
        public const string ENTER = "\r\n";
        public const string LBRACE = "{";
        public const string RBRACE = "}";
        public const string END = ";";
        public const string SEP = "%";
        public const string LANNO = "/*";
        public const string RANNO = "*/";
        public const string ANNO = "//";
        public const string SHARP = "#";
        public const string QUOTE = "\"";
        public const string LPARENTHESE = "(";
        public const string RPARENTHESE = ")";

        public const string PATH_CONSTANT = "constant";
        public const string PATH_POLYMESH = "polyMesh";
        public const string PATH_VXTFILE = "case.foam";
        public const string PATH_DESCFILE = "case.desc";
        public const string PATH_BOUNDARY = "boundary";
        public static string GetConstantPath(string caseRoot)
        {
            return Path.Combine(caseRoot, PATH_CONSTANT);
        }
        public static string GetPolyMeshPath(string caseRoot)
        {
            return Path.Combine(GetConstantPath(caseRoot), PATH_POLYMESH);
        }
        public static string GetDescFilePath(string caseRoot)
        {
            return Path.Combine(GetPolyMeshPath(caseRoot), PATH_DESCFILE);
        }
        public static string GetVxtFilePath(string caseRoot)
        {
            string vxt = Path.Combine(caseRoot, PATH_VXTFILE);
            if (!File.Exists(vxt))
                File.Create(vxt).Close();
            return vxt;
        }
        public static string GetCaseRootFromVxt(string vxtFileName)
        {
            return Path.GetDirectoryName(vxtFileName);
        }
        public static string GetBoundaryFileNameFromVxt(string vxt)
        {
            return Path.Combine(GetPolyMeshPath((GetCaseRootFromVxt(vxt))), PATH_BOUNDARY);
        }
        public static string GetFvOptionsPath(string caseRoot)
        {
            return GetConstantChildPathName(caseRoot, "fvOptions");
        }
        public static string ClearAnno(string src)
        {
            Regex mutiLineAnno = new Regex("/\\*(.|\\r?\\n)*\\*/");
            Regex singleLineAnno = new Regex("//[^\\n]*\\r?\\n");
            string s = mutiLineAnno.Replace(src, "");
            s = singleLineAnno.Replace(s, "");
            return s;
        }
        public static string ClearEnter(string src)
        {
            Regex twoMoreEnter = new Regex("(\\r?\\n){2,}");
            Regex firstLineEnter = new Regex("^\\r?\\n");
            Regex lastLineEnter = new Regex("\\r?\\n$");
            Regex enter = new Regex("\\r?\\n");
            Regex mutiSpace = new Regex("\\s{2,}");
            string s = twoMoreEnter.Replace(src, "\n");
            s = firstLineEnter.Replace(s, "");
            s = lastLineEnter.Replace(s, "");
            s = enter.Replace(s, FoamConst.SPACE);
            s = mutiSpace.Replace(s, FoamConst.SPACE);
            return s;
        }
        public static string ClearOutBrace(string src)
        {
            string s = new Regex("^\\s\\{").Replace(src, "");
            s = new Regex("\\}$").Replace(s, "");
            return s;
        }

        public static string ReadHeader(string src)
        {
            src = ClearAnno(src);
            src = ClearEnter(src);
            Regex r = new Regex(@"FoamFile {[^{}]+}");
            Match m = r.Match(src);
            if (m.Success)
            {
                return m.Value;
            }
            else
                return "";
        }
        public static string ClearHeader(string src)
        {
            Regex r = new Regex(@"{[^{}]+}");
            src = src.Replace("FoamFile", "");
            src = ClearAnno(src);
            src = ClearEnter(src);
            Match m = r.Match(src);
            if (m.Success)
            {
                src = src.Replace(m.Value, "");
            }
            return src;
        }

        public static string GetListContent(string src)
        {
            Regex r = new Regex(@"(?<=\().*(?=\))");
            Match m = r.Match(src);
            if (m.Success)
            {
                return m.Value;
            }
            else
                return "";
        }
        public static string GetZeroFieldFileName(string root,string fieldName)
        {
            return Path.Combine(root, "0", fieldName);
        }

        public static string GetControlDictFileName(string root)
        {
            return Path.Combine(root, "system", "controlDict");
        }
        public static string GetFvSchemesFileName(string root)
        {
            return Path.Combine(root, "system", "fvSchemes");
        }

        public static string GetFvSolutionFileName(string root)
        {
            return Path.Combine(root, "system", "fvSolution");
        }

        public static string GetDeComposeParDictFileName(string root)
        {
            return Path.Combine(root, "system", "decomposeParDict");
        }
        public static string GetSetFieldDictFileName(string root)
        {
            return Path.Combine(root, "system", "setFieldsDict");
        }

        public static string GetConstantDirName(string root)
        {
            return Path.Combine(root, "constant");
        }

        public static string GetConstantChildPathName(string root, string child)
        {
            return Path.Combine(root, "constant", child);
        }
        public static string GetSystemChildPathName(string root,string child)
        {
            return Path.Combine(root, "system", child);
        }

        public static string GetCfMeshDictFileName(string root)
        {
            return GetSystemChildPathName(root, "meshDict");
        }

        public static string GetSurfaceFileName()
        {
            return "\"surface.stl\"";
        }

        public static string GetSurfaceFileName(string root)
        {
            return Path.Combine(root, "surface.stl");
        }
    }
}
