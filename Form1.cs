using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
            Execute(showMessage: true, checkOnlyExeWorks: true);
        }

        private void 単体実行EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenCmd(openPromptOnly: true);
        }

        private void OpenCmd(string _args = "", bool pause = true, bool openPromptOnly = false)
        {
            if (!File.Exists(tbExePath.Text))
            {
                MessageBox.Show("avrdudeのパスを設定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var p = new Process();
            p.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
            var avrdudePath = Path.GetDirectoryName(tbExePath.Text);
            var args = (openPromptOnly || pause) ? "/K " : "/C ";
            args += "cd \"" + avrdudePath + "\"";
            if (!openPromptOnly)
            {
                args += " .\\avrdude.exe -c usbasp -p t85 ";
                if (File.Exists(tbConfPath.Text))
                {
                    args += $" -C \"{tbConfPath.Text}\" ";
                }
                args += _args;
            }

            p.StartInfo.Arguments = args;
            p.Start();
        }

        private bool Execute(string args = "", bool showMessage = false, bool checkOnlyExeWorks = false)
        {
            if (!File.Exists(tbExePath.Text))
            {
                MessageBox.Show("avrdudeのパスを設定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!checkOnlyExeWorks)
            {
                args += " -c usbasp -p t85";
            }
            if (File.Exists(tbConfPath.Text))
            {
                args += $" -C \"{tbConfPath.Text}\"";
            }

            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = tbExePath.Text;
            p.StartInfo.Arguments = args;
            p.Start();

            string stderr = p.StandardError.ReadToEnd();
            p.WaitForExit();

            bool ret = (p.ExitCode == 0);

            if (checkOnlyExeWorks)
            {
                string expectedErrorMsg = @"avrdude.exe: no programmer has been specified on the command line or the config file";
                string actual = stderr.Trim().Split('\n')[0].Trim();
                ret |= (actual == expectedErrorMsg);
            }

            if (showMessage)
            {
                if (ret)
                {
                    MessageBox.Show("avrdudeの実行に成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("avrdudeの実行に失敗しました。設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return ret;
        }

        private void BtnWriteLfuse_Click(object sender, EventArgs e)
        {
            
        }

        private void ATtiny85テストAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Execute())
            {
                MessageBox.Show("ATtiny85への接続が成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ATtiny85への接続が失敗しました。avrdudeのパスとUSBaspの接続等を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
