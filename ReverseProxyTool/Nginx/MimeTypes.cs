using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

//  参考
//  https://heartbeats.jp/hbblog/2012/02/nginx03.html

namespace ReverseProxyTool.Nginx
{
    [DirectiveParameter("types")]
    class MimeTypes : NginxConfigBase
    {
        [DirectiveParameter("text/html")]
        public string text_html { get; set; } = "html htm shtml";
        [DirectiveParameter("text/css")]
        public string text_css { get; set; } = "css";
        [DirectiveParameter("text/xml")]
        public string text_xml { get; set; } = "xml";
        [DirectiveParameter("image/gif")]
        public string image_gif { get; set; } = "gif";
        [DirectiveParameter("image/jpeg")]
        public string image_jpeg { get; set; } = "jpeg jpg";
        [DirectiveParameter("application/javascript")]
        public string application_javascript { get; set; } = "js";
        [DirectiveParameter("application/atom+xml")]
        public string application_atom_xml { get; set; } = "atom";
        [DirectiveParameter("application/rss+xml")]
        public string application_rss_xml { get; set; } = "rss";
        [DirectiveParameter("text/mathml")]
        public string text_mathml { get; set; } = "mml";
        [DirectiveParameter("text/plain")]
        public string text_plain { get; set; } = "txt pac";
        [DirectiveParameter("text/vnd.sun.j2me.app-descriptor")]
        public string text_vnd_sun_j2me_app_descriptor { get; set; } = "jad";
        [DirectiveParameter("text/vnd.wap.wml")]
        public string text_vnd_wap_wml { get; set; } = "wml";
        [DirectiveParameter("text/x-component")]
        public string text_x_component { get; set; } = "htc";
        [DirectiveParameter("image/png")]
        public string image_png { get; set; } = "png";
        [DirectiveParameter("image/svg+xml")]
        public string image_svg_xml { get; set; } = "svg svgz";
        [DirectiveParameter("image/tiff")]
        public string image_tiff { get; set; } = "tif tiff";
        [DirectiveParameter("image/vnd.wap.wbmp")]
        public string image_vnd_wap_wbmp { get; set; } = "wbmp";
        [DirectiveParameter("image/webp")]
        public string image_webp { get; set; } = "webp";
        [DirectiveParameter("image/x-icon")]
        public string image_x_icon { get; set; } = "ico";
        [DirectiveParameter("image/x-jng")]
        public string image_x_jng { get; set; } = "jng";
        [DirectiveParameter("image/x-ms-bmp")]
        public string image_x_ms_bmp { get; set; } = "bmp";
        [DirectiveParameter("font/woff")]
        public string font_woff { get; set; } = "woff";
        [DirectiveParameter("font/woff2")]
        public string font_woff2 { get; set; } = "woff2";
        [DirectiveParameter("application/java-archive")]
        public string application_java_archive { get; set; } = "jar war ear";
        [DirectiveParameter("application/json")]
        public string application_json { get; set; } = "json";
        [DirectiveParameter("application/mac-binhex40")]
        public string application_mac_binhex40 { get; set; } = "hqx";
        [DirectiveParameter("application/msword")]
        public string application_msword { get; set; } = "doc";
        [DirectiveParameter("application/pdf")]
        public string application_pdf { get; set; } = "pdf";
        [DirectiveParameter("application/postscript")]
        public string application_postscript { get; set; } = "ps eps ai";
        [DirectiveParameter("application/rtf")]
        public string application_rtf { get; set; } = "rtf";
        [DirectiveParameter("application/vnd.apple.mpegurl")]
        public string application_vnd_apple_mpegurl { get; set; } = "m3u8";
        [DirectiveParameter("application/vnd.google-earth.kml+xml")]
        public string application_vnd_google_earth_kml_xml { get; set; } = "kml";
        [DirectiveParameter("application/vnd.google-earth.kmz")]
        public string application_vnd_google_earth_kmz { get; set; } = "kmz";
        [DirectiveParameter("application/vnd.ms-excel")]
        public string application_vnd_ms_excel { get; set; } = "xls";
        [DirectiveParameter("application/vnd.ms-fontobject")]
        public string application_vnd_ms_fontobject { get; set; } = "eot";
        [DirectiveParameter("application/vnd.ms-powerpoint")]
        public string application_vnd_ms_powerpoint { get; set; } = "ppt";
        [DirectiveParameter("application/vnd.oasis.opendocument.graphics")]
        public string application_vnd_oasis_opendocument_graphics { get; set; } = "odg";
        [DirectiveParameter("application/vnd.oasis.opendocument.presentation")]
        public string application_vnd_oasis_opendocument_presentation { get; set; } = "odp";
        [DirectiveParameter("application/vnd.oasis.opendocument.spreadsheet")]
        public string application_vnd_oasis_opendocument_spreadsheet { get; set; } = "ods";
        [DirectiveParameter("application/vnd.oasis.opendocument.text")]
        public string application_vnd_oasis_opendocument_text { get; set; } = "odt";
        [DirectiveParameter("application/vnd.openxmlformats-officedocument.presentationml.presentation")]
        public string application_vnd_openxmlformats_officedocument_presentationml_presentation { get; set; } = "pptx";
        [DirectiveParameter("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public string application_vnd_openxmlformats_officedocument_spreadsheetml_sheet { get; set; } = "xlsx";
        [DirectiveParameter("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        public string application_vnd_openxmlformats_officedocument_wordprocessingml_document { get; set; } = "docx";
        [DirectiveParameter("application/vnd.wap.wmlc")]
        public string application_vnd_wap_wmlc { get; set; } = "wmlc";
        [DirectiveParameter("application/x-7z-compressed")]
        public string application_x_7z_compressed { get; set; } = "7z";
        [DirectiveParameter("application/x-cocoa")]
        public string application_x_cocoa { get; set; } = "cco";
        [DirectiveParameter("application/x-java-archive-diff")]
        public string application_x_java_archive_diff { get; set; } = "jardiff";
        [DirectiveParameter("application/x-java-jnlp-file")]
        public string application_x_java_jnlp_file { get; set; } = "jnlp";
        [DirectiveParameter("application/x-makeself")]
        public string application_x_makeself { get; set; } = "run";
        [DirectiveParameter("application/x-perl")]
        public string application_x_perl { get; set; } = "pl pm";
        [DirectiveParameter("application/x-pilot")]
        public string application_x_pilot { get; set; } = "prc pdb";
        [DirectiveParameter("application/x-rar-compressed")]
        public string application_x_rar_compressed { get; set; } = "rar";
        [DirectiveParameter("application/x-redhat-package-manager")]
        public string application_x_redhat_package_manager { get; set; } = "rpm";
        [DirectiveParameter("application/x-sea")]
        public string application_x_sea { get; set; } = "sea";
        [DirectiveParameter("application/x-shockwave-flash")]
        public string application_x_shockwave_flash { get; set; } = "swf";
        [DirectiveParameter("application/x-stuffit")]
        public string application_x_stuffit { get; set; } = "sit";
        [DirectiveParameter("application/x-tcl")]
        public string application_x_tcl { get; set; } = "tcl tk";
        [DirectiveParameter("application/x-x509-ca-cert")]
        public string application_x_x509_ca_cert { get; set; } = "der pem crt";
        [DirectiveParameter("application/x-xpinstall")]
        public string application_x_xpinstall { get; set; } = "xpi";
        [DirectiveParameter("application/xhtml+xml")]
        public string application_xhtml_xml { get; set; } = "xhtml";
        [DirectiveParameter("application/xspf+xml")]
        public string application_xspf_xml { get; set; } = "xspf";
        [DirectiveParameter("application/zip")]
        public string application_zip { get; set; } = "zip";
        [DirectiveParameter("application/octet-stream")]
        public string application_octet_stream { get; set; } = "bin exe dll";
        [DirectiveParameter("application/octet-stream")]
        public string application_octet_stream_1 { get; set; } = "deb";
        [DirectiveParameter("application/octet-stream")]
        public string application_octet_stream_2 { get; set; } = "dmg";
        [DirectiveParameter("application/octet-stream")]
        public string application_octet_stream_3 { get; set; } = "iso img";
        [DirectiveParameter("application/octet-stream")]
        public string application_octet_stream_4 { get; set; } = "msi msp msm";
        [DirectiveParameter("audio/midi")]
        public string audio_midi { get; set; } = "mid midi kar";
        [DirectiveParameter("audio/mpeg")]
        public string audio_mpeg { get; set; } = "mp3";
        [DirectiveParameter("audio/ogg")]
        public string audio_ogg { get; set; } = "ogg";
        [DirectiveParameter("audio/x-m4a")]
        public string audio_x_m4a { get; set; } = "m4a";
        [DirectiveParameter("audio/x-realaudio")]
        public string audio_x_realaudio { get; set; } = "ra";
        [DirectiveParameter("video/3gpp")]
        public string video_3gpp { get; set; } = "3gpp 3gp";
        [DirectiveParameter("video/mp2t")]
        public string video_mp2t { get; set; } = "ts";
        [DirectiveParameter("video/mp4")]
        public string video_mp4 { get; set; } = "mp4";
        [DirectiveParameter("video/mpeg")]
        public string video_mpeg { get; set; } = "mpeg mpg";
        [DirectiveParameter("video/quicktime")]
        public string video_quicktime { get; set; } = "mov";
        [DirectiveParameter("video/webm")]
        public string video_webm { get; set; } = "webm";
        [DirectiveParameter("video/x-flv")]
        public string video_x_flv { get; set; } = "flv";
        [DirectiveParameter("video/x-m4v")]
        public string video_x_m4v { get; set; } = "m4v";
        [DirectiveParameter("video/x-mng")]
        public string video_x_mng { get; set; } = "mng";
        [DirectiveParameter("video/x-ms-asf")]
        public string video_x_ms_asf { get; set; } = "asx asf";
        [DirectiveParameter("video/x-ms-wmv")]
        public string video_x_ms_wmv { get; set; } = "wmv";
        [DirectiveParameter("video/x-msvideo")]
        public string video_x_msvideo { get; set; } = "avi";

        public MimeTypes() { }

        public string GetConf()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("types {");
            foreach (PropertyInfo pi in
                this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
            {
                string paramValue = pi.GetValue(this) as string;
                if (!string.IsNullOrEmpty(paramValue))
                {
                    DirectiveParameterAttribute paramAttr =
                        Attribute.GetCustomAttribute(pi, typeof(DirectiveParameterAttribute)) as DirectiveParameterAttribute;
                    if (paramAttr != null)
                    {
                        sb.AppendLine(string.Format(
                            "    {0, -50}    {1};",
                            paramAttr.GetName(),
                            paramValue));
                    }
                }
            }
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
