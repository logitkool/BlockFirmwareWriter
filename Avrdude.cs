using System;
using System.Diagnostics;
using System.IO;

namespace BlockFirmwareWriter
{
    public class Avrdude
    {
        public Avrdude(string exePath, string confPath = null)
        {
            m_AvrdudePath = exePath;
            m_ConfigPath = confPath;
        }

        public void OpenCmd(string _args = "", bool pause = true, bool openPromptOnly = false)
        {
            if (!File.Exists(m_AvrdudePath))
            {
                throw new InvalidOperationException("avrdudeのパスを設定してください。");
            }

            var p = new Process();
            p.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
            var avrdudePath = Path.GetDirectoryName(m_AvrdudePath);
            var args = (openPromptOnly || pause) ? "/K " : "/C ";
            args += "cd \"" + avrdudePath + "\"";
            if (!openPromptOnly)
            {
                args += " .\\avrdude.exe -c usbasp -p t85 ";
                if (File.Exists(m_ConfigPath))
                {
                    args += $" -C \"{m_ConfigPath}\" ";
                }
                args += _args;
            }

            p.StartInfo.Arguments = args;
            p.Start();
        }

        public ExecInfo Execute(string args = "", bool checkOnlyExeWorks = false)
        {
            if (!File.Exists(m_AvrdudePath))
            {
                throw new InvalidOperationException("avrdudeのパスを設定してください。");
            }

            if (!checkOnlyExeWorks)
            {
                args += " -c usbasp -p t85";
            }
            if (File.Exists(m_ConfigPath))
            {
                args += $" -C \"{m_ConfigPath}\"";
            }

            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = m_AvrdudePath;
            p.StartInfo.Arguments = args;
            p.Start();

            string stdout = p.StandardOutput.ReadToEnd();
            string stderr = p.StandardError.ReadToEnd();
            p.WaitForExit();

            bool ret = (p.ExitCode == 0);

            if (checkOnlyExeWorks)
            {
                string expectedErrorMsg = @"avrdude.exe: no programmer has been specified on the command line or the config file";
                string actual = stderr.Trim().Split('\n')[0].Trim();
                ret |= (actual == expectedErrorMsg);
            }

            return new ExecInfo(ret, stdout);
        }

        private string m_AvrdudePath { get; set; }
        private string m_ConfigPath { get; set; }

    }

    /// <summary>
    /// 実行時情報
    /// </summary>
    public class ExecInfo
    {
        public ExecInfo(bool success, string stdout = null)
        {
            Success = success;
            StdOut = stdout;
        }

        public bool Success { get; }
        public string StdOut { get; }
    }

}
