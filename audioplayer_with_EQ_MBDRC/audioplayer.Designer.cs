namespace audioplayer_with_EQ_MBDRC
{
    partial class audioplayer_UI_form
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.selectAudioFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAudioFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.start_playing_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Pause_button = new System.Windows.Forms.Button();
            this.stop_button = new System.Windows.Forms.Button();
            this.Label_total_time = new System.Windows.Forms.Label();
            this.Label_current_time = new System.Windows.Forms.Label();
            this.EQ_parameter = new System.Windows.Forms.Button();
            this.volume = new System.Windows.Forms.TrackBar();
            this.peak_meter = new audioplayer_with_EQ_MBDRC.verticalprogressbar();
            this.trackbar_playing_time = new audioplayer_with_EQ_MBDRC.trackbar_no_focus_cue();
            this.MBDRC = new System.Windows.Forms.Button();
            this.FFT = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar_playing_time)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAudioFileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(410, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // selectAudioFileToolStripMenuItem
            // 
            this.selectAudioFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAudioFileToolStripMenuItem});
            this.selectAudioFileToolStripMenuItem.Name = "selectAudioFileToolStripMenuItem";
            this.selectAudioFileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.selectAudioFileToolStripMenuItem.Text = "file";
            // 
            // openAudioFileToolStripMenuItem
            // 
            this.openAudioFileToolStripMenuItem.Name = "openAudioFileToolStripMenuItem";
            this.openAudioFileToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openAudioFileToolStripMenuItem.Text = "select audio file";
            this.openAudioFileToolStripMenuItem.Click += new System.EventHandler(this.openAudioFileToolStripMenuItem_Click);
            // 
            // start_playing_button
            // 
            this.start_playing_button.Location = new System.Drawing.Point(18, 135);
            this.start_playing_button.Name = "start_playing_button";
            this.start_playing_button.Size = new System.Drawing.Size(110, 30);
            this.start_playing_button.TabIndex = 2;
            this.start_playing_button.Text = "Play";
            this.start_playing_button.UseVisualStyleBackColor = true;
            this.start_playing_button.Click += new System.EventHandler(this.start_playing_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(48, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(350, 22);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(-3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "file:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pause_button
            // 
            this.Pause_button.Location = new System.Drawing.Point(152, 135);
            this.Pause_button.Name = "Pause_button";
            this.Pause_button.Size = new System.Drawing.Size(110, 30);
            this.Pause_button.TabIndex = 5;
            this.Pause_button.Text = "Pause";
            this.Pause_button.UseVisualStyleBackColor = true;
            this.Pause_button.Click += new System.EventHandler(this.Pause_Click);
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(284, 135);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(110, 30);
            this.stop_button.TabIndex = 6;
            this.stop_button.Text = "Stop";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_Click);
            // 
            // Label_total_time
            // 
            this.Label_total_time.AutoSize = true;
            this.Label_total_time.Location = new System.Drawing.Point(303, 90);
            this.Label_total_time.Name = "Label_total_time";
            this.Label_total_time.Size = new System.Drawing.Size(83, 12);
            this.Label_total_time.TabIndex = 7;
            this.Label_total_time.Text = "Label_total_time";
            // 
            // Label_current_time
            // 
            this.Label_current_time.AutoSize = true;
            this.Label_current_time.Location = new System.Drawing.Point(88, 90);
            this.Label_current_time.Name = "Label_current_time";
            this.Label_current_time.Size = new System.Drawing.Size(96, 12);
            this.Label_current_time.TabIndex = 9;
            this.Label_current_time.Text = "Label_current_time";
            // 
            // EQ_parameter
            // 
            this.EQ_parameter.Location = new System.Drawing.Point(18, 184);
            this.EQ_parameter.Name = "EQ_parameter";
            this.EQ_parameter.Size = new System.Drawing.Size(110, 35);
            this.EQ_parameter.TabIndex = 45;
            this.EQ_parameter.Text = "EQ";
            this.EQ_parameter.UseVisualStyleBackColor = true;
            this.EQ_parameter.Click += new System.EventHandler(this.EQ_parameter_Click);
            // 
            // volume
            // 
            this.volume.Location = new System.Drawing.Point(354, 64);
            this.volume.Maximum = 100;
            this.volume.Name = "volume";
            this.volume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.volume.Size = new System.Drawing.Size(45, 66);
            this.volume.TabIndex = 58;
            this.volume.Value = 100;
            this.volume.Scroll += new System.EventHandler(this.volume_Scroll);
            // 
            // peak_meter
            // 
            this.peak_meter.Location = new System.Drawing.Point(52, 75);
            this.peak_meter.Name = "peak_meter";
            this.peak_meter.Size = new System.Drawing.Size(10, 46);
            this.peak_meter.TabIndex = 59;
            // 
            // trackbar_playing_time
            // 
            this.trackbar_playing_time.Location = new System.Drawing.Point(117, 86);
            this.trackbar_playing_time.Maximum = 300;
            this.trackbar_playing_time.Name = "trackbar_playing_time";
            this.trackbar_playing_time.Size = new System.Drawing.Size(186, 45);
            this.trackbar_playing_time.SmallChange = 2;
            this.trackbar_playing_time.TabIndex = 44;
            this.trackbar_playing_time.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackbar_playing_time.Scroll += new System.EventHandler(this.trackbar_playing_time_Scroll);
            this.trackbar_playing_time.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trackbar_playing_time_KeyDown);
            this.trackbar_playing_time.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trackbar_playing_time_KeyUp);
            this.trackbar_playing_time.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackbar_playing_time_MouseDown);
            this.trackbar_playing_time.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackbar_playing_time_MouseUp);
            // 
            // MBDRC
            // 
            this.MBDRC.Location = new System.Drawing.Point(152, 184);
            this.MBDRC.Name = "MBDRC";
            this.MBDRC.Size = new System.Drawing.Size(110, 35);
            this.MBDRC.TabIndex = 62;
            this.MBDRC.Text = "DRC";
            this.MBDRC.UseVisualStyleBackColor = true;
            this.MBDRC.Click += new System.EventHandler(this.MBDRC_Click);
            // 
            // FFT
            // 
            this.FFT.Location = new System.Drawing.Point(284, 184);
            this.FFT.Name = "FFT";
            this.FFT.Size = new System.Drawing.Size(110, 35);
            this.FFT.TabIndex = 63;
            this.FFT.Text = "FFT";
            this.FFT.UseVisualStyleBackColor = true;
            this.FFT.Click += new System.EventHandler(this.FFT_Click);
            // 
            // audioplayer_UI_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(410, 247);
            this.Controls.Add(this.FFT);
            this.Controls.Add(this.MBDRC);
            this.Controls.Add(this.peak_meter);
            this.Controls.Add(this.EQ_parameter);
            this.Controls.Add(this.trackbar_playing_time);
            this.Controls.Add(this.stop_button);
            this.Controls.Add(this.Pause_button);
            this.Controls.Add(this.start_playing_button);
            this.Controls.Add(this.Label_total_time);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.Label_current_time);
            this.Controls.Add(this.volume);
            this.Controls.Add(this.label1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "audioplayer_UI_form";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "audio player with effect";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar_playing_time)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectAudioFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAudioFileToolStripMenuItem;
        private System.Windows.Forms.Button start_playing_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Pause_button;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Label Label_total_time;
        public System.Windows.Forms.Label Label_current_time;
        private trackbar_no_focus_cue trackbar_playing_time;
        private System.Windows.Forms.Button EQ_parameter;
        private System.Windows.Forms.TrackBar volume;
        private verticalprogressbar peak_meter;
        private System.Windows.Forms.Button MBDRC;
        private System.Windows.Forms.Button FFT;
    }
}

