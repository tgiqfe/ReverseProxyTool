using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using ReverseProxyTool.Nginx;
using System.Threading;

namespace ReverseProxyTool.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Start, "ReverseProxyServer")]
    public class StartReverseProxyServer : PSCmdlet
    {
        [Parameter]
        public int HttpPort { get; set; } = 6080;
        [Parameter]
        public int HttpsPort { get; set; } = 6443;
        [Parameter]
        public string CrtFile { get; set; } = "server.crt";
        [Parameter]
        public string KeyFile { get; set; } = "server.key";
        [Parameter]
        public string Transfer { get; set; } = "localhost";
        [Parameter]
        public SwitchParameter IsHttpOnly { get; set; }
        [Parameter]
        public SwitchParameter IsHttpsOnly { get; set; }

        protected override void ProcessRecord()
        {
            if (IsHttpOnly && IsHttpsOnly)
            {
                Console.WriteLine("Error! Both http and https cannot be enabled.");
                return;
            }

            using (Mutex mutex = new Mutex(false, "Global\\StartReverseProxyTool"))
            {
                if(!mutex.WaitOne(0, false))
                {
                    Console.WriteLine("Error! Multiple running is prohibited.");
                    return;
                }

                if (Item.UnassignedCancelEvent)
                {
                    Console.CancelKeyPress += (sender, e) =>
                    {
                        e.Cancel = false;
                        NginxFunction.QuitServer();
                    };
                    Item.UnassignedCancelEvent = false;
                }

                Console.WriteLine("[Ctrl + C] で終了。");

                //  リバースプロキシ実行
                NginxFunction.StartServer(HttpPort, HttpsPort, CrtFile, KeyFile, Transfer, IsHttpOnly, IsHttpsOnly);

                mutex.ReleaseMutex();
            }   
        }
    }
}
