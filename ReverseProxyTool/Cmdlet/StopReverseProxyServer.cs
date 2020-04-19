using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using ReverseProxyTool.Nginx;

namespace ReverseProxyTool.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Stop, "ReverseProxyServer")]
    public class StopReverseProxyServer : PSCmdlet
    {
        [Parameter]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            if (Force)
            {
                NginxFunction.StopServer();
            }
            else
            {
                NginxFunction.QuitServer();
            }
        }
    }
}
