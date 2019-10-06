using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace BlockFirmwareWriter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            ofd.InitialDirectory = @"C:\Program Files (x86)\Arduino\hardware\tools\avr\bin\";
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

        }

        private void 単体実行EToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
