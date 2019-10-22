using System;
using System.Windows.Forms;
using System.IO;
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

        private void TbFilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TbFilePath_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files.Length > 0)
            {
                (sender as TextBox).Text = files[0];
            }
        }

        private void 実行テストTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_avrdude.Execute(checkOnlyExeWorks: true).Success)
                {
                    MessageBox.Show("avrdudeの実行に成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("avrdudeの実行に失敗しました。設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 単体実行EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_avrdude.OpenCmd(openPromptOnly: true);
        }

        private void BtnWriteLfuse_Click(object sender, EventArgs e)
        {
            try
            {
                m_cmdBlock.WriteFuseBit(m_avrdude);
                MessageBox.Show("フューズビットの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("フューズビットの書き込みに失敗しました。接続を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ATtiny85テストAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_avrdude.Execute("-B 3").Success)
                {
                    MessageBox.Show("ATtiny85への接続が成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ATtiny85への接続が失敗しました。avrdudeのパスとUSBaspの接続等を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FuseBit確認ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fuses = m_cmdBlock.GetFuseBit(m_avrdude);
                MessageBox.Show($"efuse: {fuses.ExtFuse:X2}, hfuse: {fuses.HFuse:X2}, lfuse: {fuses.LFuse:X2}", this.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnWriteHex_Click(object sender, EventArgs e)
        {
            try
            {
                m_cmdBlock.WriteHex(m_avrdude, tbHexPath.Text);
                MessageBox.Show("ファームウェアの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("ファームウェアの書き込みに失敗しました。接続や設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            msg += $"UID: {uid_h:X2}.{uid_l:X2}\n";
            msg += "Mode: " + comboMode.SelectedItem.ToString();

            var ret = MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ret == DialogResult.Yes)
            {
                var rom = new EEPROM();
                rom.Set(0x01, role, uid_h, uid_l, mode);
                try
                {
                    m_cmdBlock.WriteRom(m_avrdude, rom);
                    MessageBox.Show("EEPROMへの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void 設定確認SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var rom = m_cmdBlock.GetRom(m_avrdude);

                var msg = "Saved: " + (rom.Saved ? "true" : "false") + "\n";
                msg += "Role: " + (rom.Role.HasValue ? Enum.GetName(typeof(FlocRole), rom.Role) : "(none)") + "\n";
                msg += $"UID: {rom.UID_H:X2}.{rom.UID_L:X2}\n";
                msg += "Mode: " + (rom.Mode.HasValue ? Enum.GetName(typeof(FlocMode), rom.Mode): "(none)");

                if (rom.Role.HasValue)
                {
                    comboRole.SelectedIndex = comboRole.Items.IndexOf(Enum.GetName(typeof(FlocRole), rom.Role));
                }
                if (rom.Mode.HasValue)
                {
                    comboMode.SelectedIndex = comboMode.Items.IndexOf(Enum.GetName(typeof(FlocMode), rom.Mode));
                }

                tbUidH.Text = rom.UID_H != 0xFF ? $"{rom.UID_H:X2}" : "";
                tbUidL.Text = rom.UID_L != 0xFF ? $"{rom.UID_L:X2}" : "";

                MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnWriteCore_Click(object sender, EventArgs e)
        {
            string comPort = comboCOM.SelectedItem.ToString();
            try
            {
                m_coreBlock.WriteBin(m_espTool, comPort, tbBinPath.Text);
                MessageBox.Show("ファームウェアの書き込みに成功しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("ファームウェアの書き込みに失敗しました。接続や設定を確認してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private const string AVRDUDE_DEFAULT_PATH = @"C:\Program Files (x86)\Arduino\hardware\tools\avr\bin\avrdude.exe"; // arduino
        private const string AVRDUDE_DEFAULT_CONF_PATH = @"C:\Program Files (x86)\Arduino\hardware\tools\avr\etc\avrdude.conf"; // arduino

        private Avrdude _avrdude;
        private Avrdude m_avrdude
        {
            get
            {
                if (_avrdude == null)
                {
                    _avrdude = new Avrdude(tbExePath.Text, tbConfPath.Text);
                }

                return _avrdude;
            }
        }

        private EspTool _espTool;
        private EspTool m_espTool
        {
            get
            {
                if (_espTool == null)
                {
                    _espTool = new EspTool(tbEsptoolPath.Text);
                }

                return _espTool;
            }
        }

        private CommandBlock m_cmdBlock = new CommandBlock();
        private CoreBlock m_coreBlock = new CoreBlock();

    }

}
