using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReverseProxyTool.Nginx
{
    class NginxPath
    {
        private string _Base = null;
        private string _Zip = null;
        private string _Dir = null;
        private string _Exe = null;
        private string _ConfDir = null;
        private string _ConfDir_def = null;
        private string _Conf = null;
        private string _MimeTypes = null;


        private string _CertFile = null;
        private string _KeyFile = null;

        public string Base { get { return this._Base; } }
        public string Zip
        {
            get
            {
                if (_Zip == null)
                {
                    if (!string.IsNullOrEmpty(_Base))
                    {
                        this._Zip = Path.Combine(_Base, "nginx.zip");
                    }
                }
                return this._Zip;
            }
        }
        public string Dir
        {
            get
            {
                if (_Dir == null)
                {
                    if (!string.IsNullOrEmpty(_Base))
                    {
                        this._Dir = Path.Combine(_Base, "nginx");
                    }
                }
                return this._Dir;
            }
        }
        public string Exe
        {
            get
            {
                if (_Exe == null)
                {
                    if (!string.IsNullOrEmpty(_Base))
                    {
                        this._Exe = Directory.GetFiles(_Base, "*.exe", SearchOption.AllDirectories).
                            FirstOrDefault(x => Path.GetFileName(x).Equals("nginx.exe"));
                    }
                }
                return this._Exe;
            }
        }
        
        public string ConfDir
        {
            get
            {
                if(_ConfDir == null)
                {
                    if (!string.IsNullOrEmpty(_Base))
                    {
                        this._ConfDir = Function.RelatedToAbsolutePath(Exe, "..\\conf");
                    }
                }
                return this._ConfDir;
            }
        }
        public string ConfDir_def
        {
            get
            {
                if (_ConfDir_def == null)
                {
                    if (!string.IsNullOrEmpty(_Base))
                    {
                        this._ConfDir_def = Function.RelatedToAbsolutePath(Exe, "..\\conf_def");
                    }
                }
                return this._ConfDir_def;
            }
        }
        public string Conf
        {
            get
            {
                if (_Conf == null)
                {
                    if (!string.IsNullOrEmpty(_Base))
                    {
                        //this._Conf = Path.Combine(Work, "nginx.conf");
                        //this._Conf = Function.RelatedToAbsolutePath(Exe, "..\\conf\\nginx.conf");
                        this._Conf = Path.Combine(ConfDir, "nginx.conf");
                    }
                }
                return this._Conf;
            }
        }
        public string MimeTypes
        {
            get
            {
                if (_MimeTypes == null)
                {
                    if (!string.IsNullOrEmpty(_Base))
                    {
                        //this._MimeTypes = Path.Combine(Work, "mime.types");
                        //this._MimeTypes = Function.RelatedToAbsolutePath(Exe, "..\\conf\\mime.types");
                        this._MimeTypes = Path.Combine(ConfDir, "mime.types");
                    }
                }
                return this._MimeTypes;
            }
        }



        public NginxPath() { }
        public NginxPath(string baseDir)
        {
            this._Base = baseDir;
        }
    }
}
