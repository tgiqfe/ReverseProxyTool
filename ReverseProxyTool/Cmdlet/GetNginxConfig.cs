using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using ReverseProxyTool.Nginx;

namespace ReverseProxyTool.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "NginxConfig")]
    public class GetNginxConfig : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            /*
            NginxPath nginxPath = new NginxPath(Item.TOOLS_DIRECTORY);
            MimeTypes mimeTypes = new MimeTypes(nginxPath.MimeTypes);
            mimeTypes.Save();
            */
            NginxConfig confBase = new NginxConfig();
            string outStr = confBase.GetConf();
            Console.WriteLine(outStr);
        }
    }
}
