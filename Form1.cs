using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace BlockFirmwareWriter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // set role and mode combo box
            comboRole.Items.AddRange(Enum.GetNames(typeof(FlocRole)));
            comboRole.SelectedIndex = 0;
            comboMode.Items.AddRange(Enum.GetNames(typeof(FlocMode)));
            comboMode.SelectedIndex = 0;

            // check existence of "avrdude with arduino" default path
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

        private ExecInfo Execute(string args = "", bool showMessage = false, bool checkOnlyExeWorks = false)
        {
            if (!File.Exists(tbExePath.Text))
            {
                MessageBox.Show("avrdudeのパスを設定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new ExecInfo(false);
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

            return new ExecInfo(ret, stdout);
        }

        private void BtnWriteLfuse_Click(object sender, EventArgs e)
        {
            var args = "-B 3 -U lfuse:w:0xE2:m";
            if (Execute(args).Success)
            {
                MessageBox.Show("フューズビットの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                MessageBox.Show("フューズビットの書き込みに失敗しました。接続を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ATtiny85テストAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Execute("-B 3").Success)
            {
                MessageBox.Show("ATtiny85への接続が成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ATtiny85への接続が失敗しました。avrdudeのパスとUSBaspの接続等を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private FuseBit GetFuseBit()
        {
            if (!Execute("-B 3").Success) throw new InvalidOperationException();

            var m = new Dictionary<string, byte>();

            foreach(var name in new[] { "efuse", "hfuse", "lfuse" })
            {
                var ret = Execute($"-B 3 -U {name}:r:-:d -q -q");
                if (!ret.Success || string.IsNullOrEmpty(ret.StdOut)) throw new InvalidOperationException();
                m[name] = Convert.ToByte(ret.StdOut.Trim(), 10);
            }

            return new FuseBit(m["efuse"], m["hfuse"], m["lfuse"]);
        }

        private void FuseBit確認ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fuses = GetFuseBit();

            MessageBox.Show($"efuse: {fuses.ExtFuse,2:X}, hfuse: {fuses.HFuse,2:X}, lfuse: {fuses.LFuse,2:X}", this.Text);
            
        }

        private void BtnSelectHex_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "HEXファイルを選択";
            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "IntelHEXファイル(*.hex)|*.hex";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbHexPath.Text = ofd.FileName;
            }
        }

        private void BtnWriteHex_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tbHexPath.Text))
            {
                MessageBox.Show("ファームウェアファイルが存在しません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var args = $"-U flash:w:\"{tbHexPath.Text}\":i";
            // TODO: 下部プログレスバーにアップロードの進捗を表示させる (#の数でも数える？)
            if (Execute(args).Success)
            {
                MessageBox.Show("ファームウェアの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ファームウェアの書き込みに失敗しました。接続や設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnWriteRom_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();

            // TODO: 実装 (Intel HEX形式を作り出したほうが楽かも？)
        }
    }

}
