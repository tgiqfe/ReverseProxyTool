﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ReverseProxyTool.Nginx;

namespace ReverseProxyTool
{
    internal class Item
    {
        public static readonly string WORK_DIRECTORY =
            Path.Combine(Environment.ExpandEnvironmentVariables("%TEMP%"), "ReverseProxyTool");
        public static readonly string TOOLS_DIRECTORY =
            Path.Combine(WORK_DIRECTORY, "Tools");

        /// <summary>
        /// CancelKeyPressイベント未割当/割当済
        /// </summary>
        public static bool UnassignedCancelEvent = true;

        /// <summary>
        /// Nginx関連パスを格納
        /// </summary>
        public static NginxPath NginxPath = null;
    }
}
