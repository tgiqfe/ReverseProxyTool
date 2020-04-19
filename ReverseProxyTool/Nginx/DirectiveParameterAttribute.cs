using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxyTool.Nginx
{
    class DirectiveParameterAttribute : Attribute
    {
        private string _Name = null;
        private string _SubName = null;

        public DirectiveParameterAttribute() { }
        public DirectiveParameterAttribute(string name)
        {
            this._Name = name;
        }
        public DirectiveParameterAttribute(string name, string subName)
        {
            this._Name = name;
            this._SubName = subName;
        }

        public string GetName() { return string.IsNullOrEmpty(_SubName) ? this._Name : this._Name + " " + this._SubName; }
    }
}
