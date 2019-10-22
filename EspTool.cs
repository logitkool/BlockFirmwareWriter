using System;
using System.Diagnostics;
using System.IO;

namespace BlockFirmwareWriter
{
    public class EspTool
    {
        public EspTool(string exePath)
        {
            m_esptoolPath = exePath;
        }

        public void Execute(string comPort, string args)
        {
            if (!File.Exists(m_esptoolPath))
            {
                throw new InvalidOperationException("esptool.exeのパスを設定してください。");
            }

            var p = new Process();
            p.StartInfo.FileName = m_esptoolPath;
            p.StartInfo.Arguments = $"--chip esp32 --port {comPort} --baud 921600 " + args;
            p.Start();

            p.WaitForExit();

            if (p.ExitCode != 0)
            {
                throw new InvalidOperationException("esptoolの実行に失敗しました。設定や接続を確認してください。");
            }
        }

        private string m_esptoolPath { get; set; }


    }
}
