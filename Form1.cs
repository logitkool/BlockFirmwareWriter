using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace BlockFirmwareWriter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (File.Exists(AVRDUDE_DEFAULT_PATH))
            {
                tbExePath.Text = AVRDUDE_DEFAULT_PATH;
            }

            if (File.Exists(AVRDUDE_DEFAULT_CONF_PATH))
            {
                tbConfPath.Text = AVRDUDE_DEFAULT_CONF_PATH;
            }
        }

        private const string AVRDUDE_DEFAULT_PATH = @"C:\Program Files (x86)\Arduino\hardware\tools\avr\bin\avrdude.exe"; // arduino
        private const string AVRDUDE_DEFAULT_CONF_PATH = @"C:\Program Files (x86)\Arduino\hardware\tools\avr\etc\avrdude.conf"; // arduino

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSelectExe_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "avrdude.exeを選択";
            ofd.FileName = "avrdude.exe";
            ofd.InitialDirectory = Path.GetDirectoryName(AVRDUDE_DEFAULT_PATH);
            ofd.Filter = "実行可能ファイル(*.exe)|*.exe";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbExePath.Text = ofd.FileName;
            }
        }

        private void BtnSelectConf_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "avrdudeの設定ファイルを選択";
            ofd.FileName = "avrdude.conf";
            ofd.InitialDirectory = @"C:\Program Files (x86)\Arduino\hardware\tools\avr\etc\";
            ofd.Filter = "avrdude設定ファイル(*.conf)|*.conf";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbConfPath.Text = ofd.FileName;
            }
        }

        private void 実行テストTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(checkIsWorking: true);
        }

        private void 単体実行EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenCmd();
        }

        private void OpenCmd()
        {
            if (!File.Exists(tbExePath.Text))
            {
                MessageBox.Show("avrdudeのパスを設定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var exePath = Environment.GetEnvironmentVariable("ComSpec");
            var avrdudePath = Path.GetDirectoryName(tbExePath.Text);
            var args = "/k cd \"" + avrdudePath + "\"";
            Process.Start(exePath, args);
        }

        private void Execute(string args = "", bool outputDebug = false, bool checkIsWorking = false, bool openCmd = false)
        {
            if (!File.Exists(tbExePath.Text))
            {
                MessageBox.Show("avrdudeのパスを設定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(tbConfPath.Text))
            {
                args += $" -C \"{tbConfPath.Text}\"";
            }

            var ret = new Process();
            var exePath = tbExePath.Text;
            if (openCmd)
            {
                exePath = Environment.GetEnvironmentVariable("ComSpec");
                var avrdudePath = Path.GetDirectoryName(tbExePath.Text);
                args = "/k cd \"" + avrdudePath + "\" && .\\avrdude.exe " + args;
            }
            if (outputDebug)
            {
                var msgbox_ret = MessageBox.Show($"cmd: {exePath}\r\nargs: {args}", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (msgbox_ret == DialogResult.Cancel)
                {
                    return;
                }
            }

            ret.StartInfo.FileName = exePath;
            ret.StartInfo.Arguments = args;
            ret.StartInfo.UseShellExecute = false;
            ret.StartInfo.RedirectStandardOutput = true;
            ret.StartInfo.RedirectStandardError = true;
            ret.Start();

            if (checkIsWorking)
            {
                ret.WaitForExit();
                string stderr = ret.StandardError.ReadToEnd();
                
                if (ret.ExitCode != 0)
                {
                    // 期待されるエラーメッセージ (1行目)
                    string expectedErrorMsg = @"avrdude.exe: no programmer has been specified on the command line or the config file";
                    string actual = stderr.Trim().Split('\n')[0].Trim();
                    if (actual == expectedErrorMsg)
                    {
                        MessageBox.Show("avrdudeの実行に成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    MessageBox.Show("avrdudeの実行に失敗しました。設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    MessageBox.Show("avrdudeの実行に成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
    }
}
