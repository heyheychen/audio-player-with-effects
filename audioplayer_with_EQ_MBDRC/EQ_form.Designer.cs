namespace audioplayer_with_EQ_MBDRC
{
    partial class EQ_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.EQ_coef_apply = new System.Windows.Forms.Button();
            this.fc7 = new System.Windows.Forms.TextBox();
            this.gain7 = new System.Windows.Forms.TextBox();
            this.Q_factor7 = new System.Windows.Forms.TextBox();
            this.fc8 = new System.Windows.Forms.TextBox();
            this.gain8 = new System.Windows.Forms.TextBox();
            this.Q_factor8 = new System.Windows.Forms.TextBox();
            this.fc9 = new System.Windows.Forms.TextBox();
            this.gain9 = new System.Windows.Forms.TextBox();
            this.Q_factor9 = new System.Windows.Forms.TextBox();
            this.fc10 = new System.Windows.Forms.TextBox();
            this.gain10 = new System.Windows.Forms.TextBox();
            this.Q_factor10 = new System.Windows.Forms.TextBox();
            this.fc6 = new System.Windows.Forms.TextBox();
            this.gain6 = new System.Windows.Forms.TextBox();
            this.Q_factor6 = new System.Windows.Forms.TextBox();
            this.fc4 = new System.Windows.Forms.TextBox();
            this.gain4 = new System.Windows.Forms.TextBox();
            this.Q_factor4 = new System.Windows.Forms.TextBox();
            this.fc5 = new System.Windows.Forms.TextBox();
            this.gain5 = new System.Windows.Forms.TextBox();
            this.Q_factor5 = new System.Windows.Forms.TextBox();
            this.fc3 = new System.Windows.Forms.TextBox();
            this.gain3 = new System.Windows.Forms.TextBox();
            this.Q_factor3 = new System.Windows.Forms.TextBox();
            this.fc2 = new System.Windows.Forms.TextBox();
            this.gain2 = new System.Windows.Forms.TextBox();
            this.Q_factor2 = new System.Windows.Forms.TextBox();
            this.filter_selection10 = new System.Windows.Forms.ComboBox();
            this.filter_selection7 = new System.Windows.Forms.ComboBox();
            this.filter_selection8 = new System.Windows.Forms.ComboBox();
            this.filter_selection9 = new System.Windows.Forms.ComboBox();
            this.filter_selection4 = new System.Windows.Forms.ComboBox();
            this.filter_selection5 = new System.Windows.Forms.ComboBox();
            this.filter_selection6 = new System.Windows.Forms.ComboBox();
            this.filter_selection3 = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.filter_selection2 = new System.Windows.Forms.ComboBox();
            this.Q_factor1 = new System.Windows.Forms.TextBox();
            this.gain1 = new System.Windows.Forms.TextBox();
            this.fc1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.filter_selection1 = new System.Windows.Forms.ComboBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // EQ_coef_apply
            // 
            this.EQ_coef_apply.Location = new System.Drawing.Point(612, 479);
            this.EQ_coef_apply.Name = "EQ_coef_apply";
            this.EQ_coef_apply.Size = new System.Drawing.Size(109, 27);
            this.EQ_coef_apply.TabIndex = 159;
            this.EQ_coef_apply.Text = "apply";
            this.EQ_coef_apply.UseVisualStyleBackColor = true;
            this.EQ_coef_apply.Click += new System.EventHandler(this.EQ_coef_apply_Click);
            // 
            // fc7
            // 
            this.fc7.Location = new System.Drawing.Point(462, 58);
            this.fc7.Name = "fc7";
            this.fc7.Size = new System.Drawing.Size(60, 22);
            this.fc7.TabIndex = 158;
            this.fc7.Text = "1000";
            this.fc7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc7.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain7
            // 
            this.gain7.Location = new System.Drawing.Point(462, 87);
            this.gain7.Name = "gain7";
            this.gain7.Size = new System.Drawing.Size(60, 22);
            this.gain7.TabIndex = 157;
            this.gain7.Text = "0";
            this.gain7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain7.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // Q_factor7
            // 
            this.Q_factor7.Location = new System.Drawing.Point(462, 114);
            this.Q_factor7.Name = "Q_factor7";
            this.Q_factor7.Size = new System.Drawing.Size(60, 22);
            this.Q_factor7.TabIndex = 156;
            this.Q_factor7.Text = "0.707";
            this.Q_factor7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor7.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // fc8
            // 
            this.fc8.Location = new System.Drawing.Point(529, 58);
            this.fc8.Name = "fc8";
            this.fc8.Size = new System.Drawing.Size(60, 22);
            this.fc8.TabIndex = 155;
            this.fc8.Text = "1000";
            this.fc8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc8.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain8
            // 
            this.gain8.Location = new System.Drawing.Point(529, 86);
            this.gain8.Name = "gain8";
            this.gain8.Size = new System.Drawing.Size(60, 22);
            this.gain8.TabIndex = 154;
            this.gain8.Text = "0";
            this.gain8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain8.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // Q_factor8
            // 
            this.Q_factor8.Location = new System.Drawing.Point(529, 113);
            this.Q_factor8.Name = "Q_factor8";
            this.Q_factor8.Size = new System.Drawing.Size(60, 22);
            this.Q_factor8.TabIndex = 153;
            this.Q_factor8.Text = "0.707";
            this.Q_factor8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor8.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // fc9
            // 
            this.fc9.Location = new System.Drawing.Point(595, 58);
            this.fc9.Name = "fc9";
            this.fc9.Size = new System.Drawing.Size(60, 22);
            this.fc9.TabIndex = 152;
            this.fc9.Text = "1000";
            this.fc9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc9.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain9
            // 
            this.gain9.Location = new System.Drawing.Point(595, 87);
            this.gain9.Name = "gain9";
            this.gain9.Size = new System.Drawing.Size(60, 22);
            this.gain9.TabIndex = 151;
            this.gain9.Text = "0";
            this.gain9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain9.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // Q_factor9
            // 
            this.Q_factor9.Location = new System.Drawing.Point(595, 114);
            this.Q_factor9.Name = "Q_factor9";
            this.Q_factor9.Size = new System.Drawing.Size(60, 22);
            this.Q_factor9.TabIndex = 150;
            this.Q_factor9.Text = "0.707";
            this.Q_factor9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor9.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // fc10
            // 
            this.fc10.Location = new System.Drawing.Point(661, 58);
            this.fc10.Name = "fc10";
            this.fc10.Size = new System.Drawing.Size(60, 22);
            this.fc10.TabIndex = 149;
            this.fc10.Text = "1000";
            this.fc10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc10.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain10
            // 
            this.gain10.Location = new System.Drawing.Point(661, 87);
            this.gain10.Name = "gain10";
            this.gain10.Size = new System.Drawing.Size(60, 22);
            this.gain10.TabIndex = 148;
            this.gain10.Text = "0";
            this.gain10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain10.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // Q_factor10
            // 
            this.Q_factor10.Location = new System.Drawing.Point(661, 114);
            this.Q_factor10.Name = "Q_factor10";
            this.Q_factor10.Size = new System.Drawing.Size(60, 22);
            this.Q_factor10.TabIndex = 147;
            this.Q_factor10.Text = "0.707";
            this.Q_factor10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor10.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // fc6
            // 
            this.fc6.Location = new System.Drawing.Point(397, 58);
            this.fc6.Name = "fc6";
            this.fc6.Size = new System.Drawing.Size(60, 22);
            this.fc6.TabIndex = 146;
            this.fc6.Text = "1000";
            this.fc6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc6.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain6
            // 
            this.gain6.Location = new System.Drawing.Point(397, 87);
            this.gain6.Name = "gain6";
            this.gain6.Size = new System.Drawing.Size(60, 22);
            this.gain6.TabIndex = 145;
            this.gain6.Text = "0";
            this.gain6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain6.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // Q_factor6
            // 
            this.Q_factor6.Location = new System.Drawing.Point(397, 114);
            this.Q_factor6.Name = "Q_factor6";
            this.Q_factor6.Size = new System.Drawing.Size(60, 22);
            this.Q_factor6.TabIndex = 144;
            this.Q_factor6.Text = "0.707";
            this.Q_factor6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor6.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // fc4
            // 
            this.fc4.Location = new System.Drawing.Point(265, 58);
            this.fc4.Name = "fc4";
            this.fc4.Size = new System.Drawing.Size(60, 22);
            this.fc4.TabIndex = 143;
            this.fc4.Text = "1000";
            this.fc4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc4.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain4
            // 
            this.gain4.Location = new System.Drawing.Point(265, 86);
            this.gain4.Name = "gain4";
            this.gain4.Size = new System.Drawing.Size(60, 22);
            this.gain4.TabIndex = 142;
            this.gain4.Text = "0";
            this.gain4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain4.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // Q_factor4
            // 
            this.Q_factor4.Location = new System.Drawing.Point(265, 113);
            this.Q_factor4.Name = "Q_factor4";
            this.Q_factor4.Size = new System.Drawing.Size(60, 22);
            this.Q_factor4.TabIndex = 141;
            this.Q_factor4.Text = "0.707";
            this.Q_factor4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor4.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // fc5
            // 
            this.fc5.Location = new System.Drawing.Point(331, 58);
            this.fc5.Name = "fc5";
            this.fc5.Size = new System.Drawing.Size(60, 22);
            this.fc5.TabIndex = 140;
            this.fc5.Text = "1000";
            this.fc5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc5.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain5
            // 
            this.gain5.Location = new System.Drawing.Point(331, 87);
            this.gain5.Name = "gain5";
            this.gain5.Size = new System.Drawing.Size(60, 22);
            this.gain5.TabIndex = 139;
            this.gain5.Text = "0";
            this.gain5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain5.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // Q_factor5
            // 
            this.Q_factor5.Location = new System.Drawing.Point(331, 114);
            this.Q_factor5.Name = "Q_factor5";
            this.Q_factor5.Size = new System.Drawing.Size(60, 22);
            this.Q_factor5.TabIndex = 138;
            this.Q_factor5.Text = "0.707";
            this.Q_factor5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor5.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // fc3
            // 
            this.fc3.Location = new System.Drawing.Point(199, 58);
            this.fc3.Name = "fc3";
            this.fc3.Size = new System.Drawing.Size(60, 22);
            this.fc3.TabIndex = 137;
            this.fc3.Text = "1000";
            this.fc3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc3.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain3
            // 
            this.gain3.Location = new System.Drawing.Point(199, 87);
            this.gain3.Name = "gain3";
            this.gain3.Size = new System.Drawing.Size(60, 22);
            this.gain3.TabIndex = 136;
            this.gain3.Text = "0";
            this.gain3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain3.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // Q_factor3
            // 
            this.Q_factor3.Location = new System.Drawing.Point(199, 114);
            this.Q_factor3.Name = "Q_factor3";
            this.Q_factor3.Size = new System.Drawing.Size(60, 22);
            this.Q_factor3.TabIndex = 135;
            this.Q_factor3.Text = "0.707";
            this.Q_factor3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor3.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // fc2
            // 
            this.fc2.Location = new System.Drawing.Point(133, 58);
            this.fc2.Name = "fc2";
            this.fc2.Size = new System.Drawing.Size(60, 22);
            this.fc2.TabIndex = 134;
            this.fc2.Text = "1000";
            this.fc2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc2.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain2
            // 
            this.gain2.Location = new System.Drawing.Point(133, 87);
            this.gain2.Name = "gain2";
            this.gain2.Size = new System.Drawing.Size(60, 22);
            this.gain2.TabIndex = 133;
            this.gain2.Text = "0";
            this.gain2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain2.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // Q_factor2
            // 
            this.Q_factor2.Location = new System.Drawing.Point(133, 114);
            this.Q_factor2.Name = "Q_factor2";
            this.Q_factor2.Size = new System.Drawing.Size(60, 22);
            this.Q_factor2.TabIndex = 132;
            this.Q_factor2.Text = "0.707";
            this.Q_factor2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor2.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // filter_selection10
            // 
            this.filter_selection10.FormattingEnabled = true;
            this.filter_selection10.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection10.Location = new System.Drawing.Point(661, 32);
            this.filter_selection10.Name = "filter_selection10";
            this.filter_selection10.Size = new System.Drawing.Size(60, 20);
            this.filter_selection10.TabIndex = 131;
            this.filter_selection10.SelectedIndexChanged += new System.EventHandler(this.filter_selection10_SelectedIndexChanged);
            // 
            // filter_selection7
            // 
            this.filter_selection7.FormattingEnabled = true;
            this.filter_selection7.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection7.Location = new System.Drawing.Point(463, 32);
            this.filter_selection7.Name = "filter_selection7";
            this.filter_selection7.Size = new System.Drawing.Size(60, 20);
            this.filter_selection7.TabIndex = 130;
            this.filter_selection7.SelectedIndexChanged += new System.EventHandler(this.filter_selection7_SelectedIndexChanged);
            // 
            // filter_selection8
            // 
            this.filter_selection8.FormattingEnabled = true;
            this.filter_selection8.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection8.Location = new System.Drawing.Point(529, 32);
            this.filter_selection8.Name = "filter_selection8";
            this.filter_selection8.Size = new System.Drawing.Size(60, 20);
            this.filter_selection8.TabIndex = 129;
            this.filter_selection8.SelectedIndexChanged += new System.EventHandler(this.filter_selection8_SelectedIndexChanged);
            // 
            // filter_selection9
            // 
            this.filter_selection9.FormattingEnabled = true;
            this.filter_selection9.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection9.Location = new System.Drawing.Point(595, 32);
            this.filter_selection9.Name = "filter_selection9";
            this.filter_selection9.Size = new System.Drawing.Size(60, 20);
            this.filter_selection9.TabIndex = 128;
            this.filter_selection9.SelectedIndexChanged += new System.EventHandler(this.filter_selection9_SelectedIndexChanged);
            // 
            // filter_selection4
            // 
            this.filter_selection4.FormattingEnabled = true;
            this.filter_selection4.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection4.Location = new System.Drawing.Point(265, 32);
            this.filter_selection4.Name = "filter_selection4";
            this.filter_selection4.Size = new System.Drawing.Size(60, 20);
            this.filter_selection4.TabIndex = 127;
            this.filter_selection4.SelectedIndexChanged += new System.EventHandler(this.filter_selection4_SelectedIndexChanged);
            // 
            // filter_selection5
            // 
            this.filter_selection5.FormattingEnabled = true;
            this.filter_selection5.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection5.Location = new System.Drawing.Point(331, 32);
            this.filter_selection5.Name = "filter_selection5";
            this.filter_selection5.Size = new System.Drawing.Size(60, 20);
            this.filter_selection5.TabIndex = 126;
            this.filter_selection5.SelectedIndexChanged += new System.EventHandler(this.filter_selection5_SelectedIndexChanged);
            // 
            // filter_selection6
            // 
            this.filter_selection6.FormattingEnabled = true;
            this.filter_selection6.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection6.Location = new System.Drawing.Point(397, 32);
            this.filter_selection6.Name = "filter_selection6";
            this.filter_selection6.Size = new System.Drawing.Size(60, 20);
            this.filter_selection6.TabIndex = 125;
            this.filter_selection6.SelectedIndexChanged += new System.EventHandler(this.filter_selection6_SelectedIndexChanged);
            // 
            // filter_selection3
            // 
            this.filter_selection3.FormattingEnabled = true;
            this.filter_selection3.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection3.Location = new System.Drawing.Point(199, 32);
            this.filter_selection3.Name = "filter_selection3";
            this.filter_selection3.Size = new System.Drawing.Size(60, 20);
            this.filter_selection3.TabIndex = 124;
            this.filter_selection3.SelectedIndexChanged += new System.EventHandler(this.filter_selection3_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // filter_selection2
            // 
            this.filter_selection2.FormattingEnabled = true;
            this.filter_selection2.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection2.Location = new System.Drawing.Point(133, 32);
            this.filter_selection2.Name = "filter_selection2";
            this.filter_selection2.Size = new System.Drawing.Size(60, 20);
            this.filter_selection2.TabIndex = 123;
            this.filter_selection2.SelectedIndexChanged += new System.EventHandler(this.filter_selection2_SelectedIndexChanged);
            // 
            // Q_factor1
            // 
            this.Q_factor1.Location = new System.Drawing.Point(66, 114);
            this.Q_factor1.Name = "Q_factor1";
            this.Q_factor1.Size = new System.Drawing.Size(60, 22);
            this.Q_factor1.TabIndex = 122;
            this.Q_factor1.Text = "0.707";
            this.Q_factor1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.Q_factor1.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // gain1
            // 
            this.gain1.Location = new System.Drawing.Point(66, 86);
            this.gain1.Name = "gain1";
            this.gain1.Size = new System.Drawing.Size(60, 22);
            this.gain1.TabIndex = 121;
            this.gain1.Text = "0";
            this.gain1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.gain1.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // fc1
            // 
            this.fc1.Location = new System.Drawing.Point(66, 58);
            this.fc1.Name = "fc1";
            this.fc1.Size = new System.Drawing.Size(60, 22);
            this.fc1.TabIndex = 120;
            this.fc1.Text = "1000";
            this.fc1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.all_parameter_key_down);
            this.fc1.Leave += new System.EventHandler(this.all_button_leave_focus);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 119;
            this.label4.Text = "Filter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 12);
            this.label3.TabIndex = 118;
            this.label3.Text = "Q";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 117;
            this.label2.Text = "Gain";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 116;
            this.label1.Text = "Fc";
            // 
            // filter_selection1
            // 
            this.filter_selection1.FormattingEnabled = true;
            this.filter_selection1.Items.AddRange(new object[] {
            "peaking",
            "high_shelf",
            "low_shelf",
            "notch",
            "lpf",
            "hpf",
            "first_lpf",
            "first_hpf",
            "bypass"});
            this.filter_selection1.Location = new System.Drawing.Point(66, 32);
            this.filter_selection1.Name = "filter_selection1";
            this.filter_selection1.Size = new System.Drawing.Size(60, 20);
            this.filter_selection1.TabIndex = 115;
            this.filter_selection1.SelectedIndexChanged += new System.EventHandler(this.filter_selection1_SelectedIndexChanged);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.IsEnableHPan = false;
            this.zedGraphControl1.IsEnableHZoom = false;
            this.zedGraphControl1.IsEnableVPan = false;
            this.zedGraphControl1.IsEnableVZoom = false;
            this.zedGraphControl1.IsEnableWheelZoom = false;
            this.zedGraphControl1.Location = new System.Drawing.Point(36, 151);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(685, 322);
            this.zedGraphControl1.TabIndex = 160;
            // 
            // EQ_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(762, 518);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.EQ_coef_apply);
            this.Controls.Add(this.fc7);
            this.Controls.Add(this.gain7);
            this.Controls.Add(this.Q_factor7);
            this.Controls.Add(this.fc8);
            this.Controls.Add(this.gain8);
            this.Controls.Add(this.Q_factor8);
            this.Controls.Add(this.fc9);
            this.Controls.Add(this.gain9);
            this.Controls.Add(this.Q_factor9);
            this.Controls.Add(this.fc10);
            this.Controls.Add(this.gain10);
            this.Controls.Add(this.Q_factor10);
            this.Controls.Add(this.fc6);
            this.Controls.Add(this.gain6);
            this.Controls.Add(this.Q_factor6);
            this.Controls.Add(this.fc4);
            this.Controls.Add(this.gain4);
            this.Controls.Add(this.Q_factor4);
            this.Controls.Add(this.fc5);
            this.Controls.Add(this.gain5);
            this.Controls.Add(this.Q_factor5);
            this.Controls.Add(this.fc3);
            this.Controls.Add(this.gain3);
            this.Controls.Add(this.Q_factor3);
            this.Controls.Add(this.fc2);
            this.Controls.Add(this.gain2);
            this.Controls.Add(this.Q_factor2);
            this.Controls.Add(this.filter_selection10);
            this.Controls.Add(this.filter_selection7);
            this.Controls.Add(this.filter_selection8);
            this.Controls.Add(this.filter_selection9);
            this.Controls.Add(this.filter_selection4);
            this.Controls.Add(this.filter_selection5);
            this.Controls.Add(this.filter_selection6);
            this.Controls.Add(this.filter_selection3);
            this.Controls.Add(this.filter_selection2);
            this.Controls.Add(this.Q_factor1);
            this.Controls.Add(this.gain1);
            this.Controls.Add(this.fc1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filter_selection1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "EQ_form";
            this.Text = "EQ";
            this.Load += new System.EventHandler(this.EQ_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EQ_coef_apply;
        private System.Windows.Forms.TextBox fc7;
        private System.Windows.Forms.TextBox gain7;
        private System.Windows.Forms.TextBox Q_factor7;
        private System.Windows.Forms.TextBox fc8;
        private System.Windows.Forms.TextBox gain8;
        private System.Windows.Forms.TextBox Q_factor8;
        private System.Windows.Forms.TextBox fc9;
        private System.Windows.Forms.TextBox gain9;
        private System.Windows.Forms.TextBox Q_factor9;
        private System.Windows.Forms.TextBox fc10;
        private System.Windows.Forms.TextBox gain10;
        private System.Windows.Forms.TextBox Q_factor10;
        private System.Windows.Forms.TextBox fc6;
        private System.Windows.Forms.TextBox gain6;
        private System.Windows.Forms.TextBox Q_factor6;
        private System.Windows.Forms.TextBox fc4;
        private System.Windows.Forms.TextBox gain4;
        private System.Windows.Forms.TextBox Q_factor4;
        private System.Windows.Forms.TextBox fc5;
        private System.Windows.Forms.TextBox gain5;
        private System.Windows.Forms.TextBox Q_factor5;
        private System.Windows.Forms.TextBox fc3;
        private System.Windows.Forms.TextBox gain3;
        private System.Windows.Forms.TextBox Q_factor3;
        private System.Windows.Forms.TextBox fc2;
        private System.Windows.Forms.TextBox gain2;
        private System.Windows.Forms.TextBox Q_factor2;
        private System.Windows.Forms.ComboBox filter_selection10;
        private System.Windows.Forms.ComboBox filter_selection7;
        private System.Windows.Forms.ComboBox filter_selection8;
        private System.Windows.Forms.ComboBox filter_selection9;
        private System.Windows.Forms.ComboBox filter_selection4;
        private System.Windows.Forms.ComboBox filter_selection5;
        private System.Windows.Forms.ComboBox filter_selection6;
        private System.Windows.Forms.ComboBox filter_selection3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox filter_selection2;
        private System.Windows.Forms.TextBox Q_factor1;
        private System.Windows.Forms.TextBox gain1;
        private System.Windows.Forms.TextBox fc1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox filter_selection1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
    }


}