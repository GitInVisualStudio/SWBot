namespace SummonersWarBot
{
    partial class SWBot
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbStatus = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblBot = new System.Windows.Forms.Label();
            this.statusBot = new System.Windows.Forms.Label();
            this.lblScreen = new System.Windows.Forms.Label();
            this.checkBoxShow = new System.Windows.Forms.CheckBox();
            this.offsetTop = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.offsetLeft = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.offsetRight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.offsetBottom = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.rps = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.offsetTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps)).BeginInit();
            this.SuspendLayout();
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(643, 9);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(64, 13);
            this.lbStatus.TabIndex = 0;
            this.lbStatus.Text = "BlueStacks:";
            // 
            // status
            // 
            this.status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(713, 9);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(38, 13);
            this.status.TabIndex = 1;
            this.status.Text = "closed";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(646, 60);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(105, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblBot
            // 
            this.lblBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBot.AutoSize = true;
            this.lblBot.Location = new System.Drawing.Point(643, 35);
            this.lblBot.Name = "lblBot";
            this.lblBot.Size = new System.Drawing.Size(26, 13);
            this.lblBot.TabIndex = 3;
            this.lblBot.Text = "Bot:";
            // 
            // statusBot
            // 
            this.statusBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBot.Location = new System.Drawing.Point(675, 35);
            this.statusBot.Name = "statusBot";
            this.statusBot.Size = new System.Drawing.Size(78, 13);
            this.statusBot.TabIndex = 4;
            this.statusBot.Text = "idle";
            this.statusBot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblScreen
            // 
            this.lblScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScreen.AutoSize = true;
            this.lblScreen.Location = new System.Drawing.Point(643, 103);
            this.lblScreen.Name = "lblScreen";
            this.lblScreen.Size = new System.Drawing.Size(44, 13);
            this.lblScreen.TabIndex = 5;
            this.lblScreen.Text = "Screen:";
            // 
            // checkBoxShow
            // 
            this.checkBoxShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShow.AutoSize = true;
            this.checkBoxShow.Checked = true;
            this.checkBoxShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShow.Location = new System.Drawing.Point(646, 119);
            this.checkBoxShow.Name = "checkBoxShow";
            this.checkBoxShow.Size = new System.Drawing.Size(53, 17);
            this.checkBoxShow.TabIndex = 6;
            this.checkBoxShow.Text = "Show";
            this.checkBoxShow.UseVisualStyleBackColor = true;
            this.checkBoxShow.CheckedChanged += new System.EventHandler(this.checkBoxShow_CheckedChanged);
            // 
            // offsetTop
            // 
            this.offsetTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetTop.Location = new System.Drawing.Point(646, 155);
            this.offsetTop.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.offsetTop.Name = "offsetTop";
            this.offsetTop.Size = new System.Drawing.Size(112, 20);
            this.offsetTop.TabIndex = 7;
            this.offsetTop.ValueChanged += new System.EventHandler(this.offsetChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(643, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Offset: Top";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(643, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Offset: Left";
            // 
            // offsetLeft
            // 
            this.offsetLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetLeft.Location = new System.Drawing.Point(646, 194);
            this.offsetLeft.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.offsetLeft.Name = "offsetLeft";
            this.offsetLeft.Size = new System.Drawing.Size(112, 20);
            this.offsetLeft.TabIndex = 9;
            this.offsetLeft.ValueChanged += new System.EventHandler(this.offsetChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(643, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Offset: Right";
            // 
            // offsetRight
            // 
            this.offsetRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetRight.Location = new System.Drawing.Point(646, 272);
            this.offsetRight.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.offsetRight.Name = "offsetRight";
            this.offsetRight.Size = new System.Drawing.Size(112, 20);
            this.offsetRight.TabIndex = 13;
            this.offsetRight.ValueChanged += new System.EventHandler(this.offsetChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(643, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Offset: Bottom";
            // 
            // offsetBottom
            // 
            this.offsetBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetBottom.Location = new System.Drawing.Point(646, 233);
            this.offsetBottom.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.offsetBottom.Name = "offsetBottom";
            this.offsetBottom.Size = new System.Drawing.Size(112, 20);
            this.offsetBottom.TabIndex = 11;
            this.offsetBottom.ValueChanged += new System.EventHandler(this.offsetChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(643, 295);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "refreshs per second";
            // 
            // rps
            // 
            this.rps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rps.Location = new System.Drawing.Point(646, 311);
            this.rps.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.rps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rps.Name = "rps";
            this.rps.Size = new System.Drawing.Size(112, 20);
            this.rps.TabIndex = 16;
            this.rps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rps.ValueChanged += new System.EventHandler(this.rpsChanged);
            // 
            // SWBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.offsetRight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.offsetBottom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.offsetLeft);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.offsetTop);
            this.Controls.Add(this.checkBoxShow);
            this.Controls.Add(this.lblScreen);
            this.Controls.Add(this.statusBot);
            this.Controls.Add(this.lblBot);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.status);
            this.Controls.Add(this.lbStatus);
            this.Name = "SWBot";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.offsetTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblBot;
        private System.Windows.Forms.Label statusBot;
        private System.Windows.Forms.Label lblScreen;
        private System.Windows.Forms.CheckBox checkBoxShow;
        private System.Windows.Forms.NumericUpDown offsetTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown offsetLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown offsetRight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown offsetBottom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown rps;
    }
}

