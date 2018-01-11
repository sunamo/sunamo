using sunamo.Attributes;
using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sunamo
{
    public class AllExtensions
    {

        [TypeOfExtension(TypeOfExtension.archive)]
        public const string zip = ".zip";
        [TypeOfExtension(TypeOfExtension.image)]
        public const string gif = ".gif";
        [TypeOfExtension(TypeOfExtension.archive)]
        public const string rar = ".rar";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string cs = ".cs";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string aspx = ".aspx";
        [TypeOfExtension(TypeOfExtension.archive)]
        public const string _7z = ".7z";
        [TypeOfExtension(TypeOfExtension.image)]
        public const string jpg = ".jpg";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string js = ".js";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string asmx = ".asmx";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string htm = ".htm";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string html = ".html";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string master = ".master";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string txt = ".txt";
        [TypeOfExtension(TypeOfExtension.binary)]
        public const string dll = ".dll";
        [TypeOfExtension(TypeOfExtension.data)]
        public const string xml = ".xml";
        [TypeOfExtension(TypeOfExtension.database)]
        public const string db = ".db";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string whtt = ".whtt";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string config = ".config";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string gitattributes = ".gitattributes";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string gitignore = ".gitignore";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string sln = ".sln";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string sample = ".sample";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string idx = ".idx";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pack = ".pack";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string suo = ".suo";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string csproj = ".csproj";
        [TypeOfExtension(TypeOfExtension.image)]
        public const string ico = ".ico";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string xaml = ".xaml";
        [TypeOfExtension(TypeOfExtension.image)]
        public const string bmp = ".bmp";
        [TypeOfExtension(TypeOfExtension.image)]
        public const string png = ".png";
        [TypeOfExtension(TypeOfExtension.executable)]
        public const string exe = ".exe";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string pdb = ".pdb";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string manifest = ".manifest";
        [TypeOfExtension(TypeOfExtension.settings)]
        public const string ini = ".ini";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string cache = ".cache";
        [TypeOfExtension(TypeOfExtension.resource)]
        public const string resources = ".resources";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string lref = ".lref";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string baml = ".baml";
        [TypeOfExtension(TypeOfExtension.resource)]
        public const string resx = ".resx";
        [TypeOfExtension(TypeOfExtension.settings)]
        public const string settings = ".settings";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string nupkg = ".nupkg";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string nuspec = ".nuspec";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pri = ".pri";
        [TypeOfExtension(TypeOfExtension.script)]
        public const string ps1 = ".ps1";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string rtf = ".rtf";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string css = ".css";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string recovery = ".recovery";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pfx = ".pfx";
        [TypeOfExtension(TypeOfExtension.data)]
        public const string json = ".json";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string targets = ".targets";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string user = ".user";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string def = ".def";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string props = ".props";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xdt = ".xdt";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string asax = ".asax";
        [TypeOfExtension(TypeOfExtension.font)]
        public const string eot = ".eot";
        [TypeOfExtension(TypeOfExtension.image)]
        public const string svg = ".svg";
        [TypeOfExtension(TypeOfExtension.font)]
        public const string ttf = ".ttf";
        [TypeOfExtension(TypeOfExtension.font)]
        public const string woff = ".woff";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string map = ".map";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string cshtml = ".cshtml";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string xproj = ".xproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string bowerrc = ".bowerrc";
        [TypeOfExtension(TypeOfExtension.executable)]
        public const string cmd = ".cmd";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string patch = ".patch";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string md = ".md";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string npmignore = ".npmignore";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string yml = ".yml";
        [TypeOfExtension(TypeOfExtension.script)]
        public const string sh = ".sh";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string markdown = ".markdown";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string jsm = ".jsm";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string jshintrc = ".jshintrc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string editorconfig = ".editorconfig";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string gnu = ".gnu";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string c = ".c";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string opts = ".opts";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string name = ".name";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string iml = ".iml";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string hbs = ".hbs";
        [TypeOfExtension(TypeOfExtension.font)]
        public const string woff2 = ".woff2";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string snk = ".snk";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string xslt = ".xslt";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string transform = ".transform";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xamarin = ".xamarin";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string axml = ".axml";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string htaccess = ".htaccess";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string _lock = ".lock" ;
        [TypeOfExtension(TypeOfExtension.other)]
        public const string phar = ".phar";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string php = ".php";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string phpproj = ".phpproj";
        [TypeOfExtension(TypeOfExtension.font)]
        public const string otf = ".otf";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string less = ".less";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string scss = ".scss";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string mdnobuild = ".md-nobuild";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string pdf = ".pdf";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string swf = ".swf";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string csslintrc = ".csslintrc";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string rb = ".rb";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string mo = ".mo";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string po = ".po";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pot = ".pot";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pem = ".pem";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string crt = ".crt";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xap = ".xap";
        [TypeOfExtension(TypeOfExtension.archive)]
        public const string gz = ".gz";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string ppt = ".ppt";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string ashx = ".ashx";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string ascx = ".ascx";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string psd1 = ".psd1";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string psm1 = ".psm1";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pp = ".pp";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string py = ".py";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string chm = ".chm";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string log2 = ".log2";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string mte = ".mte";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string src = ".src";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string mta = ".mta";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string application = ".application";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string tlog = ".tlog";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string index = ".index";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string log = ".log";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string lnk = ".lnk";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string cz = ".cz";
        [TypeOfExtension(TypeOfExtension.executable)]
        public const string msi = ".msi";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string dict = ".dict";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string done = ".done";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string lst = ".lst";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string cxx = ".cxx";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string readme = ".readme";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string mk = ".mk";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string hxx = ".hxx";
        [TypeOfExtension(TypeOfExtension.settings)]
        public const string dat = ".dat";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pl = ".pl";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string scc = ".scc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string atlas = ".atlas";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string cd = ".cd";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string datasource = ".datasource";
        [TypeOfExtension(TypeOfExtension.database)]
        public const string mdf = ".mdf";
        [TypeOfExtension(TypeOfExtension.database)]
        public const string ldf = ".ldf";
        [TypeOfExtension(TypeOfExtension.database)]
        public const string sdf = ".sdf";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string njsproj = ".njsproj";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string vb = ".vb";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string svcinfo = ".svcinfo";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string svcmap = ".svcmap";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string disco = ".disco";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string wsdl = ".wsdl";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string svnwork = ".svn-work";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string svnbase = ".svn-base";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string old = ".old";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string svc = ".svc";
        [TypeOfExtension(TypeOfExtension.executable)]
        public const string bat = ".bat";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string csdproj = ".csdproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string prx = ".prx";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xsc = ".xsc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xsd = ".xsd";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xss = ".xss";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string vssscc = ".vssscc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xsx = ".xsx";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string vspscc = ".vspscc";
        [TypeOfExtension(TypeOfExtension.script)]
        public const string sql = ".sql";
        [TypeOfExtension(TypeOfExtension.image)]
        public const string jpeg = ".jpeg";
        [TypeOfExtension(TypeOfExtension.multimedia)]
        public const string mp3 = ".mp3";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pak = ".pak";
        [TypeOfExtension(TypeOfExtension.temporary)]
        public const string tmp = ".tmp";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string force = ".force";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string chk = ".chk";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string jrs = ".jrs";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string ide = ".ide";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string ts = ".ts";
        [TypeOfExtension(TypeOfExtension.multimedia)]
        public const string wav = ".wav";
        [TypeOfExtension(TypeOfExtension.image)]
        public const string psd = ".psd";
        [TypeOfExtension(TypeOfExtension.archive)]
        public const string gzip = ".gzip";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string gpx = ".gpx";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string sbf = ".sbf";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string jpg2 = ".jpg2";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string analysisoptions = ".analysis_options";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string packages = ".packages";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string yaml = ".yaml";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string dart = ".dart";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string buildlog = ".buildlog";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string _1 = ".1";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string _2 = ".2";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string snapshot = ".snapshot";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string bak = ".bak";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string sitemap = ".sitemap";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string deploy = ".deploy";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string reg = ".reg";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string command = ".command";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string rss = ".rss";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string htc = ".htc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string net = ".net";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string inc = ".inc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string mid = ".mid";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string mht = ".mht";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string projdata = ".projdata";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string incr = ".incr";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string projdata1 = ".projdata1";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string hhc = ".hhc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string hhk = ".hhk";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string hhp = ".hhp";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string appxmanifest = ".appxmanifest";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string projitems = ".projitems";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string shproj = ".shproj";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string proj = ".proj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string addin = ".addin";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string template = ".template";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xfrm = ".xfrm";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string cur = ".cur";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string doc = ".doc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string wxs = ".wxs";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string wixproj = ".wixproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string url = ".url";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string vbproj = ".vbproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string build = ".build";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string atg = ".atg";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string frame = ".frame";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string wixlib = ".wixlib";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string wxl = ".wxl";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string appx = ".appx";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string appxupload = ".appxupload";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string appxbundle = ".appxbundle";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string cer = ".cer";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string p12 = ".p12";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string refresh = ".refresh";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pubxml = ".pubxml";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string syn = ".syn";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string config2 = ".config2";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string skin = ".skin";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string pptx = ".pptx";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string docx = ".docx";
        [TypeOfExtension(TypeOfExtension.multimedia)]
        public const string mp4 = ".mp4";
        [TypeOfExtension(TypeOfExtension.multimedia)]
        public const string wmv = ".wmv";
        [TypeOfExtension(TypeOfExtension.document)]
        public const string pps = ".pps";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string rsp = ".rsp";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string xlf = ".xlf";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string h = ".h";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string lib = ".lib";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string glade = ".glade";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string dsw = ".dsw";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string myapp = ".myapp";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pidb = ".pidb";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string userprefs = ".userprefs";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string usertasks = ".usertasks";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string cpp = ".cpp";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string cproj = ".cproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string tfsbase = ".tfs-base";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string dbml = ".dbml";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string layout = ".layout";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string appxrecipe = ".appxrecipe";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pckgdep = ".pckgdep";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string intermediate = ".intermediate";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string resfiles = ".resfiles";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string backup = ".backup";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string jsproj = ".jsproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string resjson = ".resjson";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string vcxproj = ".vcxproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string filters = ".filters";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string idl = ".idl";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string jxr = ".jxr";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string vtt = ".vtt";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string hlsl = ".hlsl";
        [TypeOfExtension(TypeOfExtension.multimedia)]
        public const string mpg = ".mpg";
        [TypeOfExtension(TypeOfExtension.resource)]
        public const string resw = ".resw";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xbf = ".xbf";
        [TypeOfExtension(TypeOfExtension.binary)]
        public const string winmd = ".winmd";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string state = ".state";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string winmdobj = ".winmdobj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string appcontentms = ".appcontent-ms";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string fxlib = ".fxlib";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string fzz = ".fzz";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string ino = ".ino";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string dds = ".dds";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string rc = ".rc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string srt = ".srt";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string hlsli = ".hlsli";
        [TypeOfExtension(TypeOfExtension.multimedia)]
        public const string wma = ".wma";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string vdproj = ".vdproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string dsp = ".dsp";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string rc2 = ".rc2";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string tlb = ".tlb";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string m4 = ".m4";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string _in = ".in" ;
        [TypeOfExtension(TypeOfExtension.other)]
        public const string ac = ".ac";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string win32 = ".win32";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string wince = ".wince";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string am = ".am";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string guess = ".guess";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string rpath = ".rpath";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string sub = ".sub";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string sgml = ".sgml";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string help = ".help";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string lircrc = ".lircrc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string bas = ".bas";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string frm = ".frm";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string frx = ".frx";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string vbp = ".vbp";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string vbw = ".vbw";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string init = ".init";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string suppressions = ".suppressions";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string vim = ".vim";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string mak = ".mak";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string diff = ".diff";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string gpe = ".gpe";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string h3600 = ".h3600";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string opie = ".opie";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string zaurus = ".zaurus";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string desktop = ".desktop";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string plist = ".plist";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string icns = ".icns";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string rsrc = ".rsrc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string scpt = ".scpt";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string strings = ".strings";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string pbxproj = ".pbxproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string scriptsuite = ".scriptsuite";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string scriptterminology = ".scriptterminology";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string nib = ".nib";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string spec = ".spec";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string nsh = ".nsh";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string s = ".s";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string m = ".m";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string hpp = ".hpp";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string gladep = ".gladep";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string qrc = ".qrc";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xpm = ".xpm";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string ui = ".ui";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string gmo = ".gmo";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string sed = ".sed";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string header = ".header";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string sin = ".sin";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string r = ".r";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string hosts = ".hosts";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string access = ".access";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string lua = ".lua";
        [TypeOfExtension(TypeOfExtension.settings)]
        public const string cfg = ".cfg";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xcf = ".xcf";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string catalog = ".catalog";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string dtd = ".dtd";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string sym = ".sym";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string vsdisco = ".vsdisco";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string kdevses = ".kdevses~";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string xls = ".xls";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string vsk = ".vsk";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string web = ".web";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string sol = ".sol";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string fx = ".fx";
        [TypeOfExtension(TypeOfExtension.visual_studio)]
        public const string pyproj = ".pyproj";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pyd = ".pyd";
        [TypeOfExtension(TypeOfExtension.image)]
        public const string tiff = ".tiff";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string clientconfig = ".clientconfig";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string vs10x = ".vs10x";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string exclude = ".exclude";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string mno = ".mno";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pfb = ".pfb";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string pfm = ".pfm";
        [TypeOfExtension(TypeOfExtension.other)]
        public const string fla = ".fla";
        [TypeOfExtension(TypeOfExtension.source_code)]
        public const string java = ".java";


    }
}
