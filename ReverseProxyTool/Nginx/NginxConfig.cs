using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace ReverseProxyTool.Nginx
{
    public enum ErrorLevel
    {
        emerg = 64,
        alert = 32,
        crit = 16,
        error = 8,
        warn = 4,
        notice = 2,
        info = 1,
        debug = 0,
    }

    class NginxConfig : NginxConfigBase
    {
        [DirectiveParameter("worker_processes")]
        public int worker_processes { get; set; } = 1;
        [DirectiveParameter("error_log")]
        public string error_log { get; set; } = string.Format("{0} {1}", "logs/error.log", ErrorLevel.notice);
        [DirectiveParameter("pid")]
        public string pid { get; set; } = "logs/nginx.pid";

        [DirectiveParameter("events")]
        public Directive_events events { get; set; } = new Directive_events();

        [DirectiveParameter("http")]
        public Directive_http http { get; set; } = new Directive_http();

        public class Directive_events : NginxConfigBase
        {
            public override string Indent { get { return "    "; } }

            [DirectiveParameter("worker_connections")]
            public int worker_connections { get; set; } = 1024;
        }
        public class Directive_http : NginxConfigBase
        {
            public override string Indent { get { return "    "; } }

            [DirectiveParameter("include")]
            public string include { get; set; } = "mime.types";
            [DirectiveParameter("default_type")]
            public string default_type { get; set; } = "application/octet-stream";
            [DirectiveParameter("log_format")]
            public string log_format { get; set; } =
                "main '$remote_addr - $remote_user [$time_local] \"$request\" $status $body_bytes_sent \"$http_referer\" \"$http_user_agent\"'";
            //public string log_format { get; set; } = "main '$remote_addr - $remote_user [$time_local] \"$request\" '\n" +
            //    "                              '$status $body_bytes_sent \"$http_referer\" '\n" +
            //    "                              '\"$http_user_agent\" \"$http_x_forwarded_for\"'";
            [DirectiveParameter("access_log")]
            public string access_log { get; set; } = "logs/access.log  main";
            [DirectiveParameter("sendfile")]
            public string sendfile { get; set; } = "on";
            [DirectiveParameter("tcp_nopush")]
            public string tcp_nopush { get; set; }
            [DirectiveParameter("keepalive_timeout")]
            public string keepalive_timeout { get; set; } = "65";
            [DirectiveParameter("gzip")]
            public string gzip { get; set; }

            [DirectiveParameter("upstream", "transfer-webserver")]
            public Directive_upstream upstream { get; set; } = new Directive_upstream();
            [DirectiveParameter("map", "$http_upgrade $connection_upgrade")]
            public Directive_map map { get; set; } = new Directive_map();
            [DirectiveParameter("server")]
            public Directive_server_https server_https { get; set; } = new Directive_server_https();
            [DirectiveParameter("server")]
            public Directive_server_http server_http { get; set; } = new Directive_server_http();
        }
        public class Directive_upstream : NginxConfigBase
        {
            public override string Indent { get { return "        "; } }

            [DirectiveParameter("server")]
            public string server { get; set; } = "localhost:8080";
        }
        public class Directive_map : NginxConfigBase
        {
            public override string Indent { get { return "        "; } }

            [DirectiveParameter("default")]
            public string mapparam_1 { get; set; } = "upgrade";
            [DirectiveParameter("''")]
            public string mapparam_2 { get; set; } = "close";
        }

        public class Directive_server_https : NginxConfigBase
        {
            public override string Indent { get { return "        "; } }

            [DirectiveParameter("listen")]
            public string listen { get; set; } = "443 ssl";
            [DirectiveParameter("server_name")]
            public string server_name { get; set; } = "_";

            //  サーバ証明書用
            [DirectiveParameter("ssl_certificate")]
            public string ssl_certificate { get; set; }
            [DirectiveParameter("ssl_certificate_key")]
            public string ssl_certificate_key { get; set; }

            //  クライアント証明書用
            [DirectiveParameter("ssl_verify_client")]
            public string ssl_verify_client { get; set; }
            [DirectiveParameter("ssl_client_certificate")]
            public string ssl_client_certificate { get; set; }

            //  SSL接続関連設定
            [DirectiveParameter("ssl_session_timeout")]
            public string ssl_session_timeout { get; set; } = "10m";
            [DirectiveParameter("ssl_protocols")]
            public string ssl_protocols { get; set; } = "TLSv1.1 TLSv1.2";
            [DirectiveParameter("ssl_ciphers")]
            public string ssl_ciphers { get; set; } = "ECDHE+RSAGCM:ECDH+AESGCM:DH+AESGCM:ECDH+AES256:DH+AES256:ECDH+AES128:DH+AES:!EXPORT:!DES:!3DES:!MD5:!DSS";
            [DirectiveParameter("ignore_invalid_headers")]
            public string ignore_invalid_headers { get; set; } = "off";
            [DirectiveParameter("real_ip_header")]
            public string real_ip_header { get; set; } = "X-Forwarded-For";

            //  その他HTTPサーバ関連設定
            [DirectiveParameter("client_max_body_size")]
            public string client_max_body_size { get; set; } = "100M";
            [DirectiveParameter("charset")]
            public string charset { get; set; }
            [DirectiveParameter("access_log")]
            public string access_log { get; set; }

            [DirectiveParameter("location", "/")]
            public Directive_location location { get; set; } = new Directive_location();
        }
        public class Directive_server_http : NginxConfigBase
        {
            public override string Indent { get { return "        "; } }

            [DirectiveParameter("listen")]
            public string listen { get; set; } = "80";
            [DirectiveParameter("server_name")]
            public string server_name { get; set; } = "_";

            //  その他HTTPサーバ関連設定
            [DirectiveParameter("client_max_body_size")]
            public string client_max_body_size { get; set; } = "100M";
            [DirectiveParameter("charset")]
            public string charset { get; set; }
            [DirectiveParameter("access_log")]
            public string access_log { get; set; }

            [DirectiveParameter("location", "/")]
            public Directive_location location { get; set; } = new Directive_location();
        }
        public class Directive_location : NginxConfigBase
        {
            public override string Indent { get { return "            "; } }

            [DirectiveParameter("root")]
            //public string root { get; set; } = "html";
            public string root { get; set; }
            [DirectiveParameter("index")]
            //public string index { get; set; } = "index.html index.htm";
            public string index { get; set; }

            [DirectiveParameter("proxy_pass")]
            public string proxy_pass { get; set; } = "http://transfer-webserver";
            [DirectiveParameter("proxy_http_version")]
            public string proxy_http_version { get; set; } = "1.1";
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_1 { get; set; } = "Host $host";
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_2 { get; set; } = "Upgrade $http_upgrade";
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_3 { get; set; } = "Connection $connection_upgrade";

            /*
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_4 { get; set; } = "X-Real-IP $remote_addr";
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_5 { get; set; } = "X-Forwarded-Host $host";
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_6 { get; set; } = "X-Forwarded-Server $host";
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_7 { get; set; } = "X-Forwarded-Proto $scheme";
            //public string proxy_set_header_4 { get; set; } = "X-Forwarded-Proto $http_x_forwarded_proto";
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_8 { get; set; } = "X-Forwarded-Port $server_port";
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_9 { get; set; } = "X-Forwarded-For $proxy_add_x_forwarded_for";
            //public string proxy_set_header_6 { get; set; } = "X-Forwarded-For $remote_addr";
            [DirectiveParameter("proxy_set_header")]
            public string proxy_set_header_10 { get; set; } = "X-Frame-Options SAMEORIGIN";
            [DirectiveParameter("proxy_buffering")]
            public string proxy_buffering { get; set; } = "on";
            [DirectiveParameter("proxy_buffers")]
            public string proxy_buffers { get; set; } = "256 16k";
            [DirectiveParameter("proxy_buffer_size")]
            public string proxy_buffer_size { get; set; } = "16k";
            */

            [DirectiveParameter("proxy_read_timeout")]
            //public string proxy_read_timeout { get; set; } = "900s";
            public string proxy_read_timeout { get; set; }
            [DirectiveParameter("add_header")]
            //public string add_header { get; set; } = "Strict-Transport-Security \"max-age=15552000\" always";
            public string add_header { get; set; }
        }

        public NginxConfig() { }

        public string GetConf()
        {
            StringBuilder sb = new StringBuilder();
            
            Action<PropertyInfo[], NginxConfigBase> getDirective = null;
            getDirective = (properties, nginxConfigBase) =>
            {
                foreach (PropertyInfo pi in properties)
                {
                    object nginxConf = pi.GetValue(nginxConfigBase);
                    if (nginxConf != null)
                    {
                        DirectiveParameterAttribute paramAttr =
                            Attribute.GetCustomAttribute(pi, typeof(DirectiveParameterAttribute)) as DirectiveParameterAttribute;
                        if (paramAttr != null)
                        {
                            if (nginxConf is NginxConfigBase)
                            {
                                sb.Append(string.Format("\n{0}{1} {{\n",
                                    nginxConfigBase.Indent,
                                    paramAttr.GetName()));
                                getDirective(
                                    nginxConf.
                                    GetType().
                                    GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly),
                                        nginxConf as NginxConfigBase);
                                sb.Append(string.Format("{0}}}\n",
                                    nginxConfigBase.Indent));
                            }
                            else
                            {
                                sb.Append(string.Format("{0}{1, -20} {2};\n", nginxConfigBase.Indent, paramAttr.GetName(), nginxConf));
                            }
                        }
                    }
                }
            };
            getDirective(this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly), this);

            return sb.ToString();
        }
    }
}
