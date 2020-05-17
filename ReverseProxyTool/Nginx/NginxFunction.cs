using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReverseProxyTool.Nginx
{
    public class NginxFunction
    {
        private static NginxPath nginxPath = null;
        private static NginxCommand command = null;

        /// <summary>
        /// Nginxプロセス開始
        /// </summary>
        public static void StartServer(
            int httpPort, int httpsPort, string crtFile, string keyFile, string transfer, bool isHttpOnly, bool isHttpsOnly)
        {
            if (nginxPath == null) { nginxPath = new NginxPath(Item.TOOLS_DIRECTORY); }
            if (command == null) { command = new NginxCommand(nginxPath); }

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
            if (nginxPath == null) { nginxPath = new NginxPath(Item.TOOLS_DIRECTORY); }
            if (command == null) { command = new NginxCommand(nginxPath); }

            command.Quit().Wait();
        }

        /// <summary>
        /// Nginxプロセス終了 (クライアント通信完了は待たない)
        /// </summary>
        public static void StopServer()
        {
            if (nginxPath == null) { nginxPath = new NginxPath(Item.TOOLS_DIRECTORY); }
            if (command == null) { command = new NginxCommand(nginxPath); }

            command.Stop().Wait();
        }
    }
}
