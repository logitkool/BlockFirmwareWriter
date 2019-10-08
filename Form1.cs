using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.IO.Ports;

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

            // check existence of esptool.exe
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (File.Exists(documentPath + @"\Arduino\hardware\espressif\esp32\tools\esptool.exe"))
            {
                tbEsptoolPath.Text = documentPath + @"\Arduino\hardware\espressif\esp32\tools\esptool.exe";
            }
            var apppath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (File.Exists(apppath + @"\Arduino15\packages\esp32\tools\esptool_py\2.6.1\esptool.exe"))
            {
                tbEsptoolPath.Text = apppath + @"\Arduino15\packages\esp32\tools\esptool_py\2.6.1\esptool.exe";
            }

            // get com ports
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboCOM.Items.Add(port);
            }
            if (comboCOM.Items.Count > 0)
                comboCOM.SelectedIndex = 0;

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

        private bool ExecuteEspTool()
        {
            if (!File.Exists(tbEsptoolPath.Text))
            {
                MessageBox.Show("esptool.exeのパスを設定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!File.Exists(tbBinPath.Text))
            {
                MessageBox.Show(".binファイルのパスを設定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var p = new Process();
            p.StartInfo.FileName = tbEsptoolPath.Text;
            p.StartInfo.Arguments = $"-c esp32 -p {comboCOM.SelectedItem.ToString()} -b 115200 write_flash 0x100000 ${tbBinPath.Text}";
            p.Start();

            p.WaitForExit();

            return p.ExitCode == 0;
        }

        private void BtnWriteLfuse_Click(object sender, EventArgs e)
        {
            var args = "-B 3 -U lfuse:w:0xE2:m";
            if (Execute(args).Success)
            {
                MessageBox.Show("フューズビットの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
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

            foreach (var name in new[] { "efuse", "hfuse", "lfuse" })
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
            if (Execute(args).Success)
            {
                MessageBox.Show("ファームウェアの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ファームウェアの書き込みに失敗しました。接続や設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetRom()
        {
            if (!Execute("-B 3").Success) throw new InvalidOperationException();

            var ret = Execute("-U eeprom:r:-:i -q -q");
            if ((!ret.Success) || (ret.StdOut.Split('\n').Length < 1))
            {
                MessageBox.Show("EEPROMの読み込みに失敗しました。。接続や設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // :(start_code), byte count, address, record_type => 9 chars
            // flag (FF) role uidH uidL (FF) mode => 7 bytes * 2 chars = 14 chars
            // no checksum check
            var intel_hex_data = ret.StdOut.Split('\n')[0];
            var sep_hex = intel_hex_data.Skip(9).Take(14)
                .Select((v, i) => new { v, i }).GroupBy(x => x.i / 2).Select(g => g.Select(x => x.v))
                .Select(h => string.Concat(h))
                .ToArray();

            bool saved = Convert.ToByte(sep_hex[0], 16) == 0x01;
            byte role = Convert.ToByte(sep_hex[2], 16);
            byte uid_h = Convert.ToByte(sep_hex[3], 16);
            byte uid_l = Convert.ToByte(sep_hex[4], 16);
            byte mode = Convert.ToByte(sep_hex[6], 16);

            var msg = "Saved: " + (saved ? "true" : "false") + "\n";
            var _role = (FlocRole)Enum.ToObject(typeof(FlocRole), role);
            msg += "Role: " + Enum.GetName(typeof(FlocRole), _role) + "\n";
            msg += $"UID: {uid_h,2:X}.{uid_l,2:X}\n";
            var _mode = (FlocMode)Enum.ToObject(typeof(FlocMode), mode);
            msg += "Mode: " + Enum.GetName(typeof(FlocMode), _mode);

            comboRole.SelectedIndex = comboRole.Items.IndexOf(Enum.GetName(typeof(FlocRole), _role));
            comboMode.SelectedIndex = comboMode.Items.IndexOf(Enum.GetName(typeof(FlocMode), _mode));
            tbUidH.Text = $"{uid_h,2:X}";
            tbUidL.Text = $"{uid_l,2:X}";

            MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnWriteRom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUidH.Text) || string.IsNullOrEmpty(tbUidL.Text))
            {
                MessageBox.Show("UIDの設定が適切ではありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var role = (byte)(Enum.Parse(typeof(FlocRole), comboRole.SelectedItem.ToString()));
            var mode = (byte)(Enum.Parse(typeof(FlocMode), comboMode.SelectedItem.ToString()));
            var uid_h = Convert.ToByte(tbUidH.Text, 16);
            var uid_l = Convert.ToByte(tbUidL.Text, 16);

            var msg = "この情報を登録します。よろしいですか？\n";
            msg += "Role: " + comboRole.SelectedItem.ToString() + "\n";
            msg += $"UID: {uid_h,2:X}.{uid_l,2:X}\n";
            msg += "Mode: " + comboMode.SelectedItem.ToString();

            var ret = MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ret == DialogResult.Yes)
            {
                var intel_hex_data = ":0700000001FF";
                intel_hex_data += $"{role:X2}{uid_h:X2}{uid_l:X2}FF{mode:X2}";
                var sum = 0x07 + 0x01 + 0xFF + role + uid_h + uid_l + 0xFF + mode;
                intel_hex_data += $"{(byte)(~sum + 1):X2}";

                if (Execute($"-U eeprom:w:{intel_hex_data}:i").Success)
                {
                    MessageBox.Show("EEPROMへの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("EEPROMへの書き込みに失敗しました。接続や設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void 設定確認SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRom();
        }

        private void BtnSelectToolPath_Click(object sender, EventArgs e)
        {
            var apppath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var ofd = new OpenFileDialog();
            ofd.Title = "esptool.exeを選択";
            ofd.FileName = "esptool.exe";
            ofd.InitialDirectory = apppath + @"\Arduino15\packages\esp32\tools\esptool_py\2.6.1\esptool.exe";
            ofd.Filter = "実行可能ファイル(*.exe)|*.exe";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbEsptoolPath.Text = ofd.FileName;
            }
        }

        private void BtnSelectBinPath_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "ファームウェアバイナリデータを選択";
            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "バイナリファイル(*.bin)|*.bin";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbBinPath.Text = ofd.FileName;
            }
        }

        private void BtnWriteCore_Click(object sender, EventArgs e)
        {
            if (ExecuteEspTool())
            {
                MessageBox.Show("ファームウェアの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ファームウェアの書き込みに失敗しました。接続や設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
