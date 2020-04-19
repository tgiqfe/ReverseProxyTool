using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReverseProxyTool.Nginx
{
    class NginxFunction
    {
        /// <summary>
        /// Nginxプロセス開始
        /// </summary>
        public static void StartServer(
            int httpPort, int httpsPort, string crtFile, string keyFile, string transfer, bool isHttpOnly, bool isHttpsOnly)
        {
            NginxPath nginxPath = new NginxPath(Item.TOOLS_DIRECTORY);
            NginxCommand command = new NginxCommand(nginxPath);

            //  設定ファイルをセット
            NginxConfig config = new NginxConfig();
            config.http.server_http.listen = httpPort.ToString();
            config.http.server_https.listen = string.Format("{0} ssl", httpsPort);
            config.http.server_https.ssl_certificate = Path.GetFullPath(crtFile).Replace("\\", "/");
            config.http.server_https.ssl_certificate_key = Path.GetFullPath(keyFile).Replace("\\", "/");
            config.http.upstream.server = transfer.Contains("://") ?
                transfer.Substring(transfer.IndexOf("://") + 3) :
                transfer;
            if (isHttpOnly) { config.http.server_https = null; }
            if (isHttpsOnly) { config.http.server_http = null; }

            //config.error_log = string.Format("'{0}/error.log' {1}", nginxPath.Logs.Replace("\\", "/"), ErrorLevel.notice);
            //config.pid = string.Format("'{0}/nginx.pid'", nginxPath.Logs.Replace("\\", "/"));
            //config.http.access_log = string.Format("'{0}/access.log' main", nginxPath.Logs.Replace("\\", "/"));

            if (Directory.Exists(nginxPath.ConfDir) && !Directory.Exists(nginxPath.ConfDir_def))
            {
                Directory.Move(nginxPath.ConfDir, nginxPath.ConfDir_def);
                Directory.CreateDirectory(nginxPath.ConfDir);
            }
            using (StreamWriter sw = new StreamWriter(nginxPath.Conf, false, new UTF8Encoding(false)))
            {
                sw.WriteLine(config.GetConf());
            }

            //  Mime設定ファイルをセット
            MimeTypes mime = new MimeTypes();
            using (StreamWriter sw = new StreamWriter(nginxPath.MimeTypes, false, new UTF8Encoding(false)))
            {
                sw.WriteLine(mime.GetConf());
            }

            command.Start().Wait();
        }

        /// <summary>
        /// Nginxプロセス終了 (クライアント通信完了待ち)
        /// </summary>
        public static void QuitServer()
        {
            NginxPath nginxPath = new NginxPath(Item.TOOLS_DIRECTORY);
            NginxCommand command = new NginxCommand(nginxPath, false);

            command.Quit().Wait();
        }

        /// <summary>
        /// Nginxプロセス終了 (クライアント通信完了は待たない)
        /// </summary>
        public static void StopServer()
        {
            NginxPath nginxPath = new NginxPath(Item.TOOLS_DIRECTORY);
            NginxCommand command = new NginxCommand(nginxPath, false);

            command.Stop().Wait();
        }
    }
}
