namespace BlockFirmwareWriter
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboMode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnWriteRom = new System.Windows.Forms.Button();
            this.btnWriteHex = new System.Windows.Forms.Button();
            this.btnWriteLfuse = new System.Windows.Forms.Button();
            this.comboRole = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbUidL = new System.Windows.Forms.TextBox();
            this.tbUidH = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectHex = new System.Windows.Forms.Button();
            this.tbHexPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectConf = new System.Windows.Forms.Button();
            this.tbConfPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectExe = new System.Windows.Forms.Button();
            this.tbExePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avrdudeAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.単体実行EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.実行テストTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aTtiny85ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aTtiny85テストAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fuseBit確認ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定確認SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelectToolPath = new System.Windows.Forms.Button();
            this.tbEsptoolPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSelectBinPath = new System.Windows.Forms.Button();
            this.tbBinPath = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnWriteCore = new System.Windows.Forms.Button();
            this.comboCOM = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 376);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(460, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(460, 349);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.comboMode);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.btnWriteRom);
            this.tabPage1.Controls.Add(this.btnWriteHex);
            this.tabPage1.Controls.Add(this.btnWriteLfuse);
            this.tabPage1.Controls.Add(this.comboRole);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tbUidL);
            this.tabPage1.Controls.Add(this.tbUidH);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnSelectHex);
            this.tabPage1.Controls.Add(this.tbHexPath);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnSelectConf);
            this.tabPage1.Controls.Add(this.tbConfPath);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnSelectExe);
            this.tabPage1.Controls.Add(this.tbExePath);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(452, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "動作/分岐/繰返";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(236, 232);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "L:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(122, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "H:";
            // 
            // comboMode
            // 
            this.comboMode.FormattingEnabled = true;
            this.comboMode.Location = new System.Drawing.Point(124, 258);
            this.comboMode.Name = "comboMode";
            this.comboMode.Size = new System.Drawing.Size(121, 20);
            this.comboMode.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 261);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "Mode";
            // 
            // btnWriteRom
            // 
            this.btnWriteRom.Location = new System.Drawing.Point(124, 288);
            this.btnWriteRom.Name = "btnWriteRom";
            this.btnWriteRom.Size = new System.Drawing.Size(225, 23);
            this.btnWriteRom.TabIndex = 17;
            this.btnWriteRom.Text = "[3] 設定 書き込み";
            this.btnWriteRom.UseVisualStyleBackColor = true;
            this.btnWriteRom.Click += new System.EventHandler(this.BtnWriteRom_Click);
            // 
            // btnWriteHex
            // 
            this.btnWriteHex.Location = new System.Drawing.Point(124, 166);
            this.btnWriteHex.Name = "btnWriteHex";
            this.btnWriteHex.Size = new System.Drawing.Size(225, 23);
            this.btnWriteHex.TabIndex = 16;
            this.btnWriteHex.Text = "[2] HEX 書き込み";
            this.btnWriteHex.UseVisualStyleBackColor = true;
            this.btnWriteHex.Click += new System.EventHandler(this.BtnWriteHex_Click);
            // 
            // btnWriteLfuse
            // 
            this.btnWriteLfuse.Location = new System.Drawing.Point(124, 98);
            this.btnWriteLfuse.Name = "btnWriteLfuse";
            this.btnWriteLfuse.Size = new System.Drawing.Size(225, 23);
            this.btnWriteLfuse.TabIndex = 15;
            this.btnWriteLfuse.Text = "[1] lfuse 書き込み";
            this.btnWriteLfuse.UseVisualStyleBackColor = true;
            this.btnWriteLfuse.Click += new System.EventHandler(this.BtnWriteLfuse_Click);
            // 
            // comboRole
            // 
            this.comboRole.FormattingEnabled = true;
            this.comboRole.Location = new System.Drawing.Point(124, 199);
            this.comboRole.Name = "comboRole";
            this.comboRole.Size = new System.Drawing.Size(121, 20);
            this.comboRole.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "UID";
            // 
            // tbUidL
            // 
            this.tbUidL.Location = new System.Drawing.Point(255, 229);
            this.tbUidL.Name = "tbUidL";
            this.tbUidL.Size = new System.Drawing.Size(73, 19);
            this.tbUidL.TabIndex = 12;
            // 
            // tbUidH
            // 
            this.tbUidH.Location = new System.Drawing.Point(143, 229);
            this.tbUidH.Name = "tbUidH";
            this.tbUidH.Size = new System.Drawing.Size(73, 19);
            this.tbUidH.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Role";
            // 
            // btnSelectHex
            // 
            this.btnSelectHex.Location = new System.Drawing.Point(355, 136);
            this.btnSelectHex.Name = "btnSelectHex";
            this.btnSelectHex.Size = new System.Drawing.Size(75, 23);
            this.btnSelectHex.TabIndex = 9;
            this.btnSelectHex.Text = "参照";
            this.btnSelectHex.UseVisualStyleBackColor = true;
            this.btnSelectHex.Click += new System.EventHandler(this.BtnSelectHex_Click);
            // 
            // tbHexPath
            // 
            this.tbHexPath.Location = new System.Drawing.Point(124, 138);
            this.tbHexPath.Name = "tbHexPath";
            this.tbHexPath.Size = new System.Drawing.Size(225, 19);
            this.tbHexPath.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "HEX file";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "※confがexeの位置に無い場合は選択";
            // 
            // btnSelectConf
            // 
            this.btnSelectConf.Location = new System.Drawing.Point(355, 45);
            this.btnSelectConf.Name = "btnSelectConf";
            this.btnSelectConf.Size = new System.Drawing.Size(75, 23);
            this.btnSelectConf.TabIndex = 5;
            this.btnSelectConf.Text = "参照";
            this.btnSelectConf.UseVisualStyleBackColor = true;
            this.btnSelectConf.Click += new System.EventHandler(this.BtnSelectConf_Click);
            // 
            // tbConfPath
            // 
            this.tbConfPath.Location = new System.Drawing.Point(124, 47);
            this.tbConfPath.Name = "tbConfPath";
            this.tbConfPath.Size = new System.Drawing.Size(225, 19);
            this.tbConfPath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "config file";
            // 
            // btnSelectExe
            // 
            this.btnSelectExe.Location = new System.Drawing.Point(355, 16);
            this.btnSelectExe.Name = "btnSelectExe";
            this.btnSelectExe.Size = new System.Drawing.Size(75, 23);
            this.btnSelectExe.TabIndex = 2;
            this.btnSelectExe.Text = "参照";
            this.btnSelectExe.UseVisualStyleBackColor = true;
            this.btnSelectExe.Click += new System.EventHandler(this.BtnSelectExe_Click);
            // 
            // tbExePath
            // 
            this.tbExePath.Location = new System.Drawing.Point(124, 18);
            this.tbExePath.Name = "tbExePath";
            this.tbExePath.Size = new System.Drawing.Size(225, 19);
            this.tbExePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "avrdude";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.comboCOM);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.btnWriteCore);
            this.tabPage2.Controls.Add(this.btnSelectBinPath);
            this.tabPage2.Controls.Add(this.tbBinPath);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.btnSelectToolPath);
            this.tabPage2.Controls.Add(this.tbEsptoolPath);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(452, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "コア";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.avrdudeAToolStripMenuItem,
            this.aTtiny85ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(460, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(77, 23);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
            // 
            // avrdudeAToolStripMenuItem
            // 
            this.avrdudeAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.単体実行EToolStripMenuItem,
            this.実行テストTToolStripMenuItem});
            this.avrdudeAToolStripMenuItem.Name = "avrdudeAToolStripMenuItem";
            this.avrdudeAToolStripMenuItem.Size = new System.Drawing.Size(90, 23);
            this.avrdudeAToolStripMenuItem.Text = "Avrdude(&A)";
            // 
            // 単体実行EToolStripMenuItem
            // 
            this.単体実行EToolStripMenuItem.Name = "単体実行EToolStripMenuItem";
            this.単体実行EToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.単体実行EToolStripMenuItem.Text = "単体実行(&E)";
            this.単体実行EToolStripMenuItem.Click += new System.EventHandler(this.単体実行EToolStripMenuItem_Click);
            // 
            // 実行テストTToolStripMenuItem
            // 
            this.実行テストTToolStripMenuItem.Name = "実行テストTToolStripMenuItem";
            this.実行テストTToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.実行テストTToolStripMenuItem.Text = "実行テスト(&T)";
            this.実行テストTToolStripMenuItem.Click += new System.EventHandler(this.実行テストTToolStripMenuItem_Click);
            // 
            // aTtiny85ToolStripMenuItem
            // 
            this.aTtiny85ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aTtiny85テストAToolStripMenuItem,
            this.fuseBit確認ToolStripMenuItem,
            this.設定確認SToolStripMenuItem});
            this.aTtiny85ToolStripMenuItem.Name = "aTtiny85ToolStripMenuItem";
            this.aTtiny85ToolStripMenuItem.Size = new System.Drawing.Size(91, 23);
            this.aTtiny85ToolStripMenuItem.Text = "ATtiny85(&T)";
            // 
            // aTtiny85テストAToolStripMenuItem
            // 
            this.aTtiny85テストAToolStripMenuItem.Name = "aTtiny85テストAToolStripMenuItem";
            this.aTtiny85テストAToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.aTtiny85テストAToolStripMenuItem.Text = "ATtiny85テスト(&T)";
            this.aTtiny85テストAToolStripMenuItem.Click += new System.EventHandler(this.ATtiny85テストAToolStripMenuItem_Click);
            // 
            // fuseBit確認ToolStripMenuItem
            // 
            this.fuseBit確認ToolStripMenuItem.Name = "fuseBit確認ToolStripMenuItem";
            this.fuseBit確認ToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.fuseBit確認ToolStripMenuItem.Text = "Fuse bit 確認(&F)";
            this.fuseBit確認ToolStripMenuItem.Click += new System.EventHandler(this.FuseBit確認ToolStripMenuItem_Click);
            // 
            // 設定確認SToolStripMenuItem
            // 
            this.設定確認SToolStripMenuItem.Name = "設定確認SToolStripMenuItem";
            this.設定確認SToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.設定確認SToolStripMenuItem.Text = "設定確認(&S)";
            this.設定確認SToolStripMenuItem.Click += new System.EventHandler(this.設定確認SToolStripMenuItem_Click);
            // 
            // btnSelectToolPath
            // 
            this.btnSelectToolPath.Location = new System.Drawing.Point(356, 20);
            this.btnSelectToolPath.Name = "btnSelectToolPath";
            this.btnSelectToolPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectToolPath.TabIndex = 5;
            this.btnSelectToolPath.Text = "参照";
            this.btnSelectToolPath.UseVisualStyleBackColor = true;
            this.btnSelectToolPath.Click += new System.EventHandler(this.BtnSelectToolPath_Click);
            // 
            // tbEsptoolPath
            // 
            this.tbEsptoolPath.Location = new System.Drawing.Point(125, 22);
            this.tbEsptoolPath.Name = "tbEsptoolPath";
            this.tbEsptoolPath.Size = new System.Drawing.Size(225, 19);
            this.tbEsptoolPath.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "esptool.exe";
            // 
            // btnSelectBinPath
            // 
            this.btnSelectBinPath.Location = new System.Drawing.Point(356, 58);
            this.btnSelectBinPath.Name = "btnSelectBinPath";
            this.btnSelectBinPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectBinPath.TabIndex = 8;
            this.btnSelectBinPath.Text = "参照";
            this.btnSelectBinPath.UseVisualStyleBackColor = true;
            this.btnSelectBinPath.Click += new System.EventHandler(this.BtnSelectBinPath_Click);
            // 
            // tbBinPath
            // 
            this.tbBinPath.Location = new System.Drawing.Point(125, 60);
            this.tbBinPath.Name = "tbBinPath";
            this.tbBinPath.Size = new System.Drawing.Size(225, 19);
            this.tbBinPath.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "bin";
            // 
            // btnWriteCore
            // 
            this.btnWriteCore.Location = new System.Drawing.Point(125, 139);
            this.btnWriteCore.Name = "btnWriteCore";
            this.btnWriteCore.Size = new System.Drawing.Size(225, 23);
            this.btnWriteCore.TabIndex = 16;
            this.btnWriteCore.Text = "ファームウェア書き込み";
            this.btnWriteCore.UseVisualStyleBackColor = true;
            this.btnWriteCore.Click += new System.EventHandler(this.BtnWriteCore_Click);
            // 
            // comboCOM
            // 
            this.comboCOM.FormattingEnabled = true;
            this.comboCOM.Location = new System.Drawing.Point(125, 98);
            this.comboCOM.Name = "comboCOM";
            this.comboCOM.Size = new System.Drawing.Size(121, 20);
            this.comboCOM.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "COM";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 398);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "BlockFirmwareWriter";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectConf;
        private System.Windows.Forms.TextBox tbConfPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectExe;
        private System.Windows.Forms.TextBox tbExePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectHex;
        private System.Windows.Forms.TextBox tbHexPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnWriteRom;
        private System.Windows.Forms.Button btnWriteHex;
        private System.Windows.Forms.Button btnWriteLfuse;
        private System.Windows.Forms.ComboBox comboRole;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbUidL;
        private System.Windows.Forms.TextBox tbUidH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem avrdudeAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 単体実行EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 実行テストTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aTtiny85ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aTtiny85テストAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuseBit確認ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設定確認SToolStripMenuItem;
        private System.Windows.Forms.Button btnWriteCore;
        private System.Windows.Forms.Button btnSelectBinPath;
        private System.Windows.Forms.TextBox tbBinPath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSelectToolPath;
        private System.Windows.Forms.TextBox tbEsptoolPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboCOM;
        private System.Windows.Forms.Label label12;
    }
}

