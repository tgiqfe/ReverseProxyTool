using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ReverseProxyTool.Nginx
{
    public class NginxFunction
    {
        private static NginxCommand command = null;

        /// <summary>
        /// 初期実行時処理
        /// </summary>
        private static void Init()
        {
            if (Item.NginxPath == null)
            {
                //nginxPath = new NginxPath(Item.TOOLS_DIRECTORY);
                Item.NginxPath = new NginxPath(Item.TOOLS_DIRECTORY);
            }
            if (command == null)
            {
                //command = new NginxCommand(nginxPath);
                command = new NginxCommand();
            }

            Function.ExpandEmbeddedResource(Item.NginxPath.Base);
            if (!Directory.Exists(Item.NginxPath.Dir))
            {
                ZipFile.ExtractToDirectory(Item.NginxPath.Zip, Item.NginxPath.Dir);
            }
        }

        /// <summary>
        /// Nginxプロセス開始
        /// </summary>
        public static void StartServer(
            int httpPort, int httpsPort, string crtFile, string keyFile, string transfer, bool isHttpOnly, bool isHttpsOnly)
        {
            Init();
            //if (nginxPath == null) { nginxPath = new NginxPath(Item.TOOLS_DIRECTORY); }
            //if (command == null) { command = new NginxCommand(nginxPath); }

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

            if (Directory.Exists(Item.NginxPath.ConfDir) && !Directory.Exists(Item.NginxPath.ConfDir_def))
            {
                Directory.Move(Item.NginxPath.ConfDir, Item.NginxPath.ConfDir_def);
                Directory.CreateDirectory(Item.NginxPath.ConfDir);
            }
            using (StreamWriter sw = new StreamWriter(Item.NginxPath.Conf, false, new UTF8Encoding(false)))
            {
                sw.WriteLine(config.GetConf());
            }

            //  Mime設定ファイルをセット
            MimeTypes mime = new MimeTypes();
            using (StreamWriter sw = new StreamWriter(Item.NginxPath.MimeTypes, false, new UTF8Encoding(false)))
            {
                sw.WriteLine(mime.GetConf());
            }

            command.Start().Wait();
        }

        /// <summary>
        /// 非同期でNginxプロセスを開始
        /// ※同期実行する場合と比べて、最後の command.Start().Wait() が、awaitするかどうかしか違いが無いので、どうにか処理共通化ができるとベター
        /// </summary>
        /// <param name="httpPort"></param>
        /// <param name="httpsPort"></param>
        /// <param name="crtFile"></param>
        /// <param name="keyFile"></param>
        /// <param name="transfer"></param>
        /// <param name="isHttpOnly"></param>
        /// <param name="isHttpsOnly"></param>
        /// <returns></returns>
        public static async Task StartServerAsync(
            int httpPort, int httpsPort, string crtFile, string keyFile, string transfer, bool isHttpOnly, bool isHttpsOnly)
        {
            Init();
            //if (nginxPath == null) { nginxPath = new NginxPath(Item.TOOLS_DIRECTORY); }
            //if (command == null) { command = new NginxCommand(nginxPath); }

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

            if (Directory.Exists(Item.NginxPath.ConfDir) && !Directory.Exists(Item.NginxPath.ConfDir_def))
            {
                Directory.Move(Item.NginxPath.ConfDir, Item.NginxPath.ConfDir_def);
                Directory.CreateDirectory(Item.NginxPath.ConfDir);
            }
            using (StreamWriter sw = new StreamWriter(Item.NginxPath.Conf, false, new UTF8Encoding(false)))
            {
                sw.WriteLine(config.GetConf());
            }

            //  Mime設定ファイルをセット
            MimeTypes mime = new MimeTypes();
            using (StreamWriter sw = new StreamWriter(Item.NginxPath.MimeTypes, false, new UTF8Encoding(false)))
            {
                sw.WriteLine(mime.GetConf());
            }

            await command.Start();
        }

        /// <summary>
        /// Nginxプロセス終了 (クライアント通信完了待ち)
        /// </summary>
        public static void QuitServer()
        {
            Init();
            //if (nginxPath == null) { nginxPath = new NginxPath(Item.TOOLS_DIRECTORY); }
            //if (command == null) { command = new NginxCommand(nginxPath); }

            command.Quit().Wait();
        }

        /// <summary>
        /// Nginxプロセス終了 (クライアント通信完了は待たない)
        /// </summary>
        public static void StopServer()
        {
            Init();
            //if (nginxPath == null) { nginxPath = new NginxPath(Item.TOOLS_DIRECTORY); }
            //if (command == null) { command = new NginxCommand(nginxPath); }

            command.Stop().Wait();
        }
    }
}
