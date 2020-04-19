using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading;

namespace ReverseProxyTool.Nginx
{
    class NginxCommand
    {
        private NginxPath _nginxPath = null;

        public NginxCommand() { }
        public NginxCommand(NginxPath nginxPath) : this(nginxPath, true) { }
        public NginxCommand(NginxPath nginxPath, bool isExpand)
        {
            //  元々アセンブリのバージョンが同じ場合は埋め込みリソースの再展開は行わないので、
            //  isExpandパラメータは保険用
            this._nginxPath = nginxPath;

            //  埋め込みリソースを展開
            if (isExpand)
            {
                Function.ExpandEmbeddedResource(_nginxPath.Base);
                if (!Directory.Exists(_nginxPath.Dir))
                {
                    ZipFile.ExtractToDirectory(_nginxPath.Zip, nginxPath.Dir);
                }
            }
        }

        /// <summary>
        /// Nginxコマンドを実行する為のプライベートメソッド
        /// </summary>
        /// <param name="arguments"></param>
        private void Run(string arguments)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = _nginxPath.Exe;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(_nginxPath.Exe);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = false;
                proc.OutputDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
                proc.ErrorDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();
            }
        }

        /// <summary>
        /// Nginxコマンドを実行して、出力内容をStringBuilderに格納する
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="sb"></param>
        private void Run(string arguments, StringBuilder sb)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = _nginxPath.Exe;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(_nginxPath.Exe);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = false;
                proc.OutputDataReceived += (sender, e) => { sb.AppendLine(e.Data); };
                proc.ErrorDataReceived += (sender, e) => { sb.AppendLine(e.Data); };
                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();
            }
        }

        /// <summary>
        /// Nginx開始コマンド
        /// </summary>
        public async Task Start()
        {
            await Task.Run(() =>
            {
                Run(string.Format(
                    "-c \"{0}\"",
                    _nginxPath.Conf));
            });
        }

        /// <summary>
        /// リクエスト処理完了後に終了。(クライアント通信中の場合、完了待ち)
        /// </summary>
        public async Task Quit()
        {
            await Task.Run(() =>
            {
                Run("-s quit");
            });
        }

        /// <summary>
        /// 直ちに終了。(クライアント通信中であっても完了待ちしない)
        /// </summary>
        public async Task Stop()
        {
            await Task.Run(() =>
            {
                Run("-s stop");
            });
        }
    }
}
