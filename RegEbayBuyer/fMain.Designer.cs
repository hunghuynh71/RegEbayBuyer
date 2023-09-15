namespace CoreDogeTool
{
    partial class fMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tick = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DATA_INPUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proxy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonTinsoft = new System.Windows.Forms.RadioButton();
            this.radioButtonSocks5 = new System.Windows.Forms.RadioButton();
            this.radioButtonNone = new System.Windows.Forms.RadioButton();
            this.radioButtonIPPort = new System.Windows.Forms.RadioButton();
            this.radioButton911 = new System.Windows.Forms.RadioButton();
            this.radioButtonHMA = new System.Windows.Forms.RadioButton();
            this.buttonImportProxy = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonImportFile = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxGetCookie = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAdsFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownNgam = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNhaMang = new System.Windows.Forms.TextBox();
            this.checkBoxGoogle = new System.Windows.Forms.CheckBox();
            this.checkBoxAddAddress = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownMaxTuongTac = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxTuongTac = new System.Windows.Forms.CheckBox();
            this.checkBoxRegEbay = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxApiCaptcha = new System.Windows.Forms.TextBox();
            this.checkBoxVerymail = new System.Windows.Forms.CheckBox();
            this.checkBoxTmpStop = new System.Windows.Forms.CheckBox();
            this.checkBoxTurnOff = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDownThread = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelKey = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCopyKey = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel10 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelExpiredTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStripMenuControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectALLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unSelectALLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectByDragToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNgam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxTuongTac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThread)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStripMenuControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.AllowUserToAddRows = false;
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Tick,
            this.DATA_INPUT,
            this.Proxy,
            this.Result,
            this.Status});
            this.dataGridViewTable.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.RowHeadersVisible = false;
            this.dataGridViewTable.Size = new System.Drawing.Size(513, 408);
            this.dataGridViewTable.TabIndex = 0;
            // 
            // Index
            // 
            this.Index.HeaderText = "#";
            this.Index.Name = "Index";
            this.Index.Width = 30;
            // 
            // Tick
            // 
            this.Tick.HeaderText = "x";
            this.Tick.Name = "Tick";
            this.Tick.Width = 20;
            // 
            // DATA_INPUT
            // 
            this.DATA_INPUT.HeaderText = "data_input";
            this.DATA_INPUT.Name = "DATA_INPUT";
            this.DATA_INPUT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DATA_INPUT.Width = 150;
            // 
            // Proxy
            // 
            this.Proxy.HeaderText = "proxy";
            this.Proxy.Name = "Proxy";
            this.Proxy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Proxy.Width = 50;
            // 
            // Result
            // 
            this.Result.HeaderText = "result";
            this.Result.Name = "Result";
            this.Result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Result.Width = 50;
            // 
            // Status
            // 
            this.Status.HeaderText = "status";
            this.Status.Name = "Status";
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Status.Width = 450;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.buttonStop);
            this.groupBox1.Controls.Add(this.buttonStart);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Green;
            this.groupBox1.Location = new System.Drawing.Point(522, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 415);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chức năng";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonTinsoft);
            this.groupBox3.Controls.Add(this.radioButtonSocks5);
            this.groupBox3.Controls.Add(this.radioButtonNone);
            this.groupBox3.Controls.Add(this.radioButtonIPPort);
            this.groupBox3.Controls.Add(this.radioButton911);
            this.groupBox3.Controls.Add(this.radioButtonHMA);
            this.groupBox3.Controls.Add(this.buttonImportProxy);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox3.Location = new System.Drawing.Point(13, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(301, 52);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thay đổi IP";
            // 
            // radioButtonTinsoft
            // 
            this.radioButtonTinsoft.AutoSize = true;
            this.radioButtonTinsoft.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonTinsoft.ForeColor = System.Drawing.Color.Black;
            this.radioButtonTinsoft.Location = new System.Drawing.Point(181, 31);
            this.radioButtonTinsoft.Name = "radioButtonTinsoft";
            this.radioButtonTinsoft.Size = new System.Drawing.Size(61, 19);
            this.radioButtonTinsoft.TabIndex = 6;
            this.radioButtonTinsoft.TabStop = true;
            this.radioButtonTinsoft.Text = "Tinsoft";
            this.radioButtonTinsoft.UseVisualStyleBackColor = true;
            // 
            // radioButtonSocks5
            // 
            this.radioButtonSocks5.AutoSize = true;
            this.radioButtonSocks5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSocks5.ForeColor = System.Drawing.Color.Black;
            this.radioButtonSocks5.Location = new System.Drawing.Point(181, 12);
            this.radioButtonSocks5.Name = "radioButtonSocks5";
            this.radioButtonSocks5.Size = new System.Drawing.Size(61, 19);
            this.radioButtonSocks5.TabIndex = 5;
            this.radioButtonSocks5.TabStop = true;
            this.radioButtonSocks5.Text = "Socks5";
            this.radioButtonSocks5.UseVisualStyleBackColor = true;
            // 
            // radioButtonNone
            // 
            this.radioButtonNone.AutoSize = true;
            this.radioButtonNone.Checked = true;
            this.radioButtonNone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonNone.ForeColor = System.Drawing.Color.Black;
            this.radioButtonNone.Location = new System.Drawing.Point(46, 31);
            this.radioButtonNone.Name = "radioButtonNone";
            this.radioButtonNone.Size = new System.Drawing.Size(54, 19);
            this.radioButtonNone.TabIndex = 4;
            this.radioButtonNone.TabStop = true;
            this.radioButtonNone.Text = "None";
            this.radioButtonNone.UseVisualStyleBackColor = true;
            this.radioButtonNone.CheckedChanged += new System.EventHandler(this.radioButtonNone_CheckedChanged);
            // 
            // radioButtonIPPort
            // 
            this.radioButtonIPPort.AutoSize = true;
            this.radioButtonIPPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonIPPort.ForeColor = System.Drawing.Color.Black;
            this.radioButtonIPPort.Location = new System.Drawing.Point(111, 31);
            this.radioButtonIPPort.Name = "radioButtonIPPort";
            this.radioButtonIPPort.Size = new System.Drawing.Size(49, 19);
            this.radioButtonIPPort.TabIndex = 2;
            this.radioButtonIPPort.TabStop = true;
            this.radioButtonIPPort.Text = "Http";
            this.radioButtonIPPort.UseVisualStyleBackColor = true;
            // 
            // radioButton911
            // 
            this.radioButton911.AutoSize = true;
            this.radioButton911.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton911.ForeColor = System.Drawing.Color.Black;
            this.radioButton911.Location = new System.Drawing.Point(111, 12);
            this.radioButton911.Name = "radioButton911";
            this.radioButton911.Size = new System.Drawing.Size(43, 19);
            this.radioButton911.TabIndex = 1;
            this.radioButton911.TabStop = true;
            this.radioButton911.Text = "911";
            this.radioButton911.UseVisualStyleBackColor = true;
            this.radioButton911.CheckedChanged += new System.EventHandler(this.radioButton911_CheckedChanged);
            // 
            // radioButtonHMA
            // 
            this.radioButtonHMA.AutoSize = true;
            this.radioButtonHMA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonHMA.ForeColor = System.Drawing.Color.Black;
            this.radioButtonHMA.Location = new System.Drawing.Point(46, 13);
            this.radioButtonHMA.Name = "radioButtonHMA";
            this.radioButtonHMA.Size = new System.Drawing.Size(53, 19);
            this.radioButtonHMA.TabIndex = 0;
            this.radioButtonHMA.TabStop = true;
            this.radioButtonHMA.Text = "HMA";
            this.radioButtonHMA.UseVisualStyleBackColor = true;
            // 
            // buttonImportProxy
            // 
            this.buttonImportProxy.Enabled = false;
            this.buttonImportProxy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImportProxy.ForeColor = System.Drawing.SystemColors.WindowText;
            this.buttonImportProxy.Image = ((System.Drawing.Image)(resources.GetObject("buttonImportProxy.Image")));
            this.buttonImportProxy.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonImportProxy.Location = new System.Drawing.Point(246, 28);
            this.buttonImportProxy.Name = "buttonImportProxy";
            this.buttonImportProxy.Size = new System.Drawing.Size(48, 23);
            this.buttonImportProxy.TabIndex = 3;
            this.buttonImportProxy.Text = "mở";
            this.buttonImportProxy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonImportProxy.UseVisualStyleBackColor = true;
            this.buttonImportProxy.Click += new System.EventHandler(this.buttonImportProxy_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.buttonImportFile);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox4.Location = new System.Drawing.Point(13, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(301, 36);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cấu hình Tài khoản";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(150, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Nhập file dữ liệu";
            // 
            // buttonImportFile
            // 
            this.buttonImportFile.Enabled = false;
            this.buttonImportFile.ForeColor = System.Drawing.Color.Black;
            this.buttonImportFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonImportFile.Image")));
            this.buttonImportFile.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonImportFile.Location = new System.Drawing.Point(246, 13);
            this.buttonImportFile.Name = "buttonImportFile";
            this.buttonImportFile.Size = new System.Drawing.Size(48, 23);
            this.buttonImportFile.TabIndex = 1;
            this.buttonImportFile.Text = "mở";
            this.buttonImportFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonImportFile.UseVisualStyleBackColor = true;
            this.buttonImportFile.Click += new System.EventHandler(this.buttonImportFile_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.ForeColor = System.Drawing.Color.Red;
            this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
            this.buttonStop.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonStop.Location = new System.Drawing.Point(170, 378);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(145, 31);
            this.buttonStop.TabIndex = 10;
            this.buttonStop.Text = "Dừng lại";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.ForeColor = System.Drawing.Color.Green;
            this.buttonStart.Image = ((System.Drawing.Image)(resources.GetObject("buttonStart.Image")));
            this.buttonStart.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonStart.Location = new System.Drawing.Point(170, 347);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(145, 31);
            this.buttonStart.TabIndex = 9;
            this.buttonStart.Text = "Bắt đầu";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxGetCookie);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxAdsFolder);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericUpDownNgam);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxNhaMang);
            this.groupBox2.Controls.Add(this.checkBoxGoogle);
            this.groupBox2.Controls.Add(this.checkBoxAddAddress);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numericUpDownMaxTuongTac);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.checkBoxTuongTac);
            this.groupBox2.Controls.Add(this.checkBoxRegEbay);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxApiCaptcha);
            this.groupBox2.Controls.Add(this.checkBoxVerymail);
            this.groupBox2.Controls.Add(this.checkBoxTmpStop);
            this.groupBox2.Controls.Add(this.checkBoxTurnOff);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.numericUpDownThread);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox2.Location = new System.Drawing.Point(13, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 237);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cấu hình chạy";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // checkBoxGetCookie
            // 
            this.checkBoxGetCookie.AutoSize = true;
            this.checkBoxGetCookie.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxGetCookie.ForeColor = System.Drawing.Color.Black;
            this.checkBoxGetCookie.Location = new System.Drawing.Point(36, 171);
            this.checkBoxGetCookie.Name = "checkBoxGetCookie";
            this.checkBoxGetCookie.Size = new System.Drawing.Size(80, 17);
            this.checkBoxGetCookie.TabIndex = 68;
            this.checkBoxGetCookie.Text = "GetCookie";
            this.checkBoxGetCookie.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(12, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 15);
            this.label6.TabIndex = 67;
            this.label6.Text = "Thư mục profile Ads";
            // 
            // textBoxAdsFolder
            // 
            this.textBoxAdsFolder.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdsFolder.Location = new System.Drawing.Point(128, 82);
            this.textBoxAdsFolder.Name = "textBoxAdsFolder";
            this.textBoxAdsFolder.Size = new System.Drawing.Size(166, 23);
            this.textBoxAdsFolder.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(109, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 15);
            this.label3.TabIndex = 65;
            this.label3.Text = "Ngâm reg xong (giây):";
            // 
            // numericUpDownNgam
            // 
            this.numericUpDownNgam.Location = new System.Drawing.Point(237, 193);
            this.numericUpDownNgam.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.numericUpDownNgam.Name = "numericUpDownNgam";
            this.numericUpDownNgam.Size = new System.Drawing.Size(57, 23);
            this.numericUpDownNgam.TabIndex = 64;
            this.numericUpDownNgam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownNgam.Value = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(42, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 63;
            this.label1.Text = "Nhà mạng 911";
            // 
            // textBoxNhaMang
            // 
            this.textBoxNhaMang.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNhaMang.Location = new System.Drawing.Point(128, 60);
            this.textBoxNhaMang.Name = "textBoxNhaMang";
            this.textBoxNhaMang.Size = new System.Drawing.Size(166, 23);
            this.textBoxNhaMang.TabIndex = 62;
            this.textBoxNhaMang.Text = "Comcast Cable";
            // 
            // checkBoxGoogle
            // 
            this.checkBoxGoogle.AutoSize = true;
            this.checkBoxGoogle.Checked = true;
            this.checkBoxGoogle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGoogle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxGoogle.ForeColor = System.Drawing.Color.Black;
            this.checkBoxGoogle.Location = new System.Drawing.Point(91, 113);
            this.checkBoxGoogle.Name = "checkBoxGoogle";
            this.checkBoxGoogle.Size = new System.Drawing.Size(85, 17);
            this.checkBoxGoogle.TabIndex = 61;
            this.checkBoxGoogle.Text = "Vào google";
            this.checkBoxGoogle.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddAddress
            // 
            this.checkBoxAddAddress.AutoSize = true;
            this.checkBoxAddAddress.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAddAddress.ForeColor = System.Drawing.Color.Black;
            this.checkBoxAddAddress.Location = new System.Drawing.Point(170, 151);
            this.checkBoxAddAddress.Name = "checkBoxAddAddress";
            this.checkBoxAddAddress.Size = new System.Drawing.Size(84, 17);
            this.checkBoxAddAddress.TabIndex = 60;
            this.checkBoxAddAddress.Text = "Add địa chỉ";
            this.checkBoxAddAddress.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(203, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 59;
            this.label5.Text = "Max";
            // 
            // numericUpDownMaxTuongTac
            // 
            this.numericUpDownMaxTuongTac.Location = new System.Drawing.Point(237, 171);
            this.numericUpDownMaxTuongTac.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownMaxTuongTac.Name = "numericUpDownMaxTuongTac";
            this.numericUpDownMaxTuongTac.Size = new System.Drawing.Size(57, 23);
            this.numericUpDownMaxTuongTac.TabIndex = 58;
            this.numericUpDownMaxTuongTac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownMaxTuongTac.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(7, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 57;
            this.label4.Text = "Chức năng:";
            // 
            // checkBoxTuongTac
            // 
            this.checkBoxTuongTac.AutoSize = true;
            this.checkBoxTuongTac.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTuongTac.ForeColor = System.Drawing.Color.Black;
            this.checkBoxTuongTac.Location = new System.Drawing.Point(36, 151);
            this.checkBoxTuongTac.Name = "checkBoxTuongTac";
            this.checkBoxTuongTac.Size = new System.Drawing.Size(78, 17);
            this.checkBoxTuongTac.TabIndex = 56;
            this.checkBoxTuongTac.Text = "Tương tác";
            this.checkBoxTuongTac.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegEbay
            // 
            this.checkBoxRegEbay.AutoSize = true;
            this.checkBoxRegEbay.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRegEbay.ForeColor = System.Drawing.Color.Black;
            this.checkBoxRegEbay.Location = new System.Drawing.Point(91, 132);
            this.checkBoxRegEbay.Name = "checkBoxRegEbay";
            this.checkBoxRegEbay.Size = new System.Drawing.Size(73, 17);
            this.checkBoxRegEbay.TabIndex = 55;
            this.checkBoxRegEbay.Text = "Reg Ebay";
            this.checkBoxRegEbay.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(55, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "Api captcha";
            // 
            // textBoxApiCaptcha
            // 
            this.textBoxApiCaptcha.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxApiCaptcha.Location = new System.Drawing.Point(128, 39);
            this.textBoxApiCaptcha.Name = "textBoxApiCaptcha";
            this.textBoxApiCaptcha.Size = new System.Drawing.Size(166, 23);
            this.textBoxApiCaptcha.TabIndex = 51;
            this.textBoxApiCaptcha.Text = "af5c4f6ed63b4df1a98e1770cf82b3f4";
            // 
            // checkBoxVerymail
            // 
            this.checkBoxVerymail.AutoSize = true;
            this.checkBoxVerymail.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxVerymail.ForeColor = System.Drawing.Color.Black;
            this.checkBoxVerymail.Location = new System.Drawing.Point(170, 131);
            this.checkBoxVerymail.Name = "checkBoxVerymail";
            this.checkBoxVerymail.Size = new System.Drawing.Size(76, 17);
            this.checkBoxVerymail.TabIndex = 50;
            this.checkBoxVerymail.Text = "Very mail?";
            this.checkBoxVerymail.UseVisualStyleBackColor = true;
            // 
            // checkBoxTmpStop
            // 
            this.checkBoxTmpStop.AutoSize = true;
            this.checkBoxTmpStop.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTmpStop.ForeColor = System.Drawing.Color.Black;
            this.checkBoxTmpStop.Location = new System.Drawing.Point(36, 196);
            this.checkBoxTmpStop.Name = "checkBoxTmpStop";
            this.checkBoxTmpStop.Size = new System.Drawing.Size(78, 17);
            this.checkBoxTmpStop.TabIndex = 47;
            this.checkBoxTmpStop.Text = "Tạm dừng";
            this.checkBoxTmpStop.UseVisualStyleBackColor = true;
            // 
            // checkBoxTurnOff
            // 
            this.checkBoxTurnOff.AutoSize = true;
            this.checkBoxTurnOff.Checked = true;
            this.checkBoxTurnOff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTurnOff.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTurnOff.ForeColor = System.Drawing.Color.Navy;
            this.checkBoxTurnOff.Location = new System.Drawing.Point(36, 217);
            this.checkBoxTurnOff.Name = "checkBoxTurnOff";
            this.checkBoxTurnOff.Size = new System.Drawing.Size(197, 17);
            this.checkBoxTurnOff.TabIndex = 37;
            this.checkBoxTurnOff.Text = "Tắt trình duyệt sau khi chạy xong";
            this.checkBoxTurnOff.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(176, 19);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 15);
            this.label18.TabIndex = 21;
            this.label18.Text = "Số luồng:";
            // 
            // numericUpDownThread
            // 
            this.numericUpDownThread.Location = new System.Drawing.Point(237, 15);
            this.numericUpDownThread.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownThread.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownThread.Name = "numericUpDownThread";
            this.numericUpDownThread.Size = new System.Drawing.Size(57, 23);
            this.numericUpDownThread.TabIndex = 20;
            this.numericUpDownThread.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownThread.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabelKey,
            this.toolStripStatusLabelCopyKey,
            this.toolStripStatusLabel9,
            this.toolStripStatusLabel10,
            this.toolStripStatusLabelExpiredTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 414);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(848, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(139, 17);
            this.toolStripStatusLabel2.Text = "https://t.me/roremidev";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel3.Text = "|";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.Purple;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(240, 17);
            this.toolStripStatusLabel4.Spring = true;
            this.toolStripStatusLabel4.Text = "Automation for RegEbayBuyer";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel5.Text = "|";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel6.Text = "Key:";
            // 
            // toolStripStatusLabelKey
            // 
            this.toolStripStatusLabelKey.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelKey.ForeColor = System.Drawing.Color.Olive;
            this.toolStripStatusLabelKey.Name = "toolStripStatusLabelKey";
            this.toolStripStatusLabelKey.Size = new System.Drawing.Size(182, 17);
            this.toolStripStatusLabelKey.Text = "XXXXXXXXXXXXXXXXXXXXXXXXX";
            // 
            // toolStripStatusLabelCopyKey
            // 
            this.toolStripStatusLabelCopyKey.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelCopyKey.ForeColor = System.Drawing.SystemColors.Highlight;
            this.toolStripStatusLabelCopyKey.Name = "toolStripStatusLabelCopyKey";
            this.toolStripStatusLabelCopyKey.Size = new System.Drawing.Size(58, 17);
            this.toolStripStatusLabelCopyKey.Text = "Copy Key";
            this.toolStripStatusLabelCopyKey.Click += new System.EventHandler(this.toolStripStatusLabelCopyKey_Click);
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel9.Text = "|";
            this.toolStripStatusLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel10
            // 
            this.toolStripStatusLabel10.Name = "toolStripStatusLabel10";
            this.toolStripStatusLabel10.Size = new System.Drawing.Size(78, 17);
            this.toolStripStatusLabel10.Text = "Hạn sử dụng:";
            // 
            // toolStripStatusLabelExpiredTime
            // 
            this.toolStripStatusLabelExpiredTime.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelExpiredTime.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelExpiredTime.Name = "toolStripStatusLabelExpiredTime";
            this.toolStripStatusLabelExpiredTime.Size = new System.Drawing.Size(77, 17);
            this.toolStripStatusLabelExpiredTime.Text = "dd-mm-yyyy";
            // 
            // contextMenuStripMenuControl
            // 
            this.contextMenuStripMenuControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectALLToolStripMenuItem,
            this.unSelectALLToolStripMenuItem,
            this.selectByDragToolStripMenuItem});
            this.contextMenuStripMenuControl.Name = "contextMenuStripMenuControl";
            this.contextMenuStripMenuControl.Size = new System.Drawing.Size(178, 70);
            // 
            // selectALLToolStripMenuItem
            // 
            this.selectALLToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectALLToolStripMenuItem.Image")));
            this.selectALLToolStripMenuItem.Name = "selectALLToolStripMenuItem";
            this.selectALLToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.selectALLToolStripMenuItem.Text = "Chọn tất cả";
            this.selectALLToolStripMenuItem.Click += new System.EventHandler(this.selectALLToolStripMenuItem_Click);
            // 
            // unSelectALLToolStripMenuItem
            // 
            this.unSelectALLToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("unSelectALLToolStripMenuItem.Image")));
            this.unSelectALLToolStripMenuItem.Name = "unSelectALLToolStripMenuItem";
            this.unSelectALLToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.unSelectALLToolStripMenuItem.Text = "Bỏ chọn tất cả";
            this.unSelectALLToolStripMenuItem.Click += new System.EventHandler(this.unSelectALLToolStripMenuItem_Click);
            // 
            // selectByDragToolStripMenuItem
            // 
            this.selectByDragToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectByDragToolStripMenuItem.Image")));
            this.selectByDragToolStripMenuItem.Name = "selectByDragToolStripMenuItem";
            this.selectByDragToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.selectByDragToolStripMenuItem.Text = "Chọn dòng bôi đen";
            this.selectByDragToolStripMenuItem.Click += new System.EventHandler(this.selectByDragToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(13, 378);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Format dữ liệu";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(848, 436);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewTable);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(864, 475);
            this.MinimumSize = new System.Drawing.Size(864, 475);
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegEbayBuyer  V3.1";
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNgam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxTuongTac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThread)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStripMenuControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonImportFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numericUpDownThread;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelKey;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCopyKey;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelExpiredTime;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonIPPort;
        private System.Windows.Forms.RadioButton radioButton911;
        private System.Windows.Forms.Button buttonImportProxy;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton radioButtonNone;
        private System.Windows.Forms.RadioButton radioButtonHMA;
        private System.Windows.Forms.RadioButton radioButtonSocks5;
        private System.Windows.Forms.CheckBox checkBoxTurnOff;
        private System.Windows.Forms.CheckBox checkBoxTmpStop;
        private System.Windows.Forms.RadioButton radioButtonTinsoft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxTuongTac;
        private System.Windows.Forms.CheckBox checkBoxRegEbay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxApiCaptcha;
        private System.Windows.Forms.CheckBox checkBoxVerymail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxTuongTac;
        private System.Windows.Forms.CheckBox checkBoxAddAddress;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMenuControl;
        private System.Windows.Forms.ToolStripMenuItem selectALLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unSelectALLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectByDragToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Tick;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATA_INPUT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proxy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.CheckBox checkBoxGoogle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNhaMang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownNgam;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAdsFolder;
        private System.Windows.Forms.CheckBox checkBoxGetCookie;
        private System.Windows.Forms.Button button1;
    }
}

