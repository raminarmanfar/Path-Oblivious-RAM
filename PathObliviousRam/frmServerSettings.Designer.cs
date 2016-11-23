namespace PathObliviousRam
{
    partial class frmServerSettings
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLoadRecords = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.nudKeySizeInBits = new System.Windows.Forms.NumericUpDown();
            this.nudRecordsPerBlock = new System.Windows.Forms.NumericUpDown();
            this.nudTreeHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudChildsPerNode = new System.Windows.Forms.NumericUpDown();
            this.nudBlocksPerBucket = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbKeyInitialValue = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.nudRandNameCount = new System.Windows.Forms.NumericUpDown();
            this.nudNoOfRecordsToGenerate = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTotalRecords = new System.Windows.Forms.TextBox();
            this.txtTotalBlocks = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotalNodes = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.nudRandFamilyCount = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.nudRandCityCount = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKeySizeInBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecordsPerBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTreeHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChildsPerNode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlocksPerBucket)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandNameCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoOfRecordsToGenerate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandFamilyCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandCityCount)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 293F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(758, 605);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = global::PathObliviousRam.Properties.Resources.advancedsettings;
            this.pictureBox2.Location = new System.Drawing.Point(465, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(287, 593);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 232F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(450, 593);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.btnLoadRecords, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(5, 504);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(440, 84);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // btnLoadRecords
            // 
            this.btnLoadRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadRecords.Image = global::PathObliviousRam.Properties.Resources.open_file_icon__1_;
            this.btnLoadRecords.Location = new System.Drawing.Point(4, 4);
            this.btnLoadRecords.Name = "btnLoadRecords";
            this.btnLoadRecords.Size = new System.Drawing.Size(139, 76);
            this.btnLoadRecords.TabIndex = 0;
            this.btnLoadRecords.Text = "Load Records";
            this.btnLoadRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoadRecords.UseVisualStyleBackColor = true;
            this.btnLoadRecords.Click += new System.EventHandler(this.btnLoadRecords_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Image = global::PathObliviousRam.Properties.Resources.red_cross_icon;
            this.btnCancel.Location = new System.Drawing.Point(296, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 76);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Image = global::PathObliviousRam.Properties.Resources.Ok_icon;
            this.btnSave.Location = new System.Drawing.Point(150, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 76);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 226);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.nudKeySizeInBits, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.nudRecordsPerBlock, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.nudTreeHeight, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.nudChildsPerNode, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.nudBlocksPerBucket, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label6, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.cbKeyInitialValue, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.label12, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(434, 201);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // nudKeySizeInBits
            // 
            this.nudKeySizeInBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudKeySizeInBits.Location = new System.Drawing.Point(236, 136);
            this.nudKeySizeInBits.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudKeySizeInBits.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudKeySizeInBits.Name = "nudKeySizeInBits";
            this.nudKeySizeInBits.Size = new System.Drawing.Size(151, 26);
            this.nudKeySizeInBits.TabIndex = 9;
            this.nudKeySizeInBits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudKeySizeInBits.Value = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.nudKeySizeInBits.ValueChanged += new System.EventHandler(this.nudTreeHeight_ValueChanged);
            this.nudKeySizeInBits.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudTreeHeight_KeyUp);
            // 
            // nudRecordsPerBlock
            // 
            this.nudRecordsPerBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudRecordsPerBlock.Location = new System.Drawing.Point(236, 103);
            this.nudRecordsPerBlock.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudRecordsPerBlock.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRecordsPerBlock.Name = "nudRecordsPerBlock";
            this.nudRecordsPerBlock.Size = new System.Drawing.Size(151, 26);
            this.nudRecordsPerBlock.TabIndex = 8;
            this.nudRecordsPerBlock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRecordsPerBlock.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRecordsPerBlock.ValueChanged += new System.EventHandler(this.nudTreeHeight_ValueChanged);
            this.nudRecordsPerBlock.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudTreeHeight_KeyUp);
            // 
            // nudTreeHeight
            // 
            this.nudTreeHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudTreeHeight.Location = new System.Drawing.Point(236, 4);
            this.nudTreeHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTreeHeight.Name = "nudTreeHeight";
            this.nudTreeHeight.Size = new System.Drawing.Size(151, 26);
            this.nudTreeHeight.TabIndex = 5;
            this.nudTreeHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudTreeHeight.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTreeHeight.ValueChanged += new System.EventHandler(this.nudTreeHeight_ValueChanged);
            this.nudTreeHeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudTreeHeight_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tree Height (L):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "No. of Childs Per Node:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "No. of Blocks Per Block (Z):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(225, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "No. of Records Per Block (R):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 32);
            this.label5.TabIndex = 4;
            this.label5.Text = "Key Length:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudChildsPerNode
            // 
            this.nudChildsPerNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudChildsPerNode.Location = new System.Drawing.Point(236, 37);
            this.nudChildsPerNode.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudChildsPerNode.Name = "nudChildsPerNode";
            this.nudChildsPerNode.Size = new System.Drawing.Size(151, 26);
            this.nudChildsPerNode.TabIndex = 6;
            this.nudChildsPerNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudChildsPerNode.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudChildsPerNode.ValueChanged += new System.EventHandler(this.nudTreeHeight_ValueChanged);
            this.nudChildsPerNode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudTreeHeight_KeyUp);
            // 
            // nudBlocksPerBucket
            // 
            this.nudBlocksPerBucket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudBlocksPerBucket.Location = new System.Drawing.Point(236, 70);
            this.nudBlocksPerBucket.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBlocksPerBucket.Name = "nudBlocksPerBucket";
            this.nudBlocksPerBucket.Size = new System.Drawing.Size(151, 26);
            this.nudBlocksPerBucket.TabIndex = 7;
            this.nudBlocksPerBucket.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudBlocksPerBucket.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudBlocksPerBucket.ValueChanged += new System.EventHandler(this.nudTreeHeight_ValueChanged);
            this.nudBlocksPerBucket.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudTreeHeight_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(394, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 32);
            this.label6.TabIndex = 10;
            this.label6.Text = "Bits";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(4, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(225, 34);
            this.label7.TabIndex = 11;
            this.label7.Text = "Key Initial Value in Generation:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbKeyInitialValue
            // 
            this.cbKeyInitialValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbKeyInitialValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbKeyInitialValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbKeyInitialValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKeyInitialValue.FormattingEnabled = true;
            this.cbKeyInitialValue.Items.AddRange(new object[] {
            "Zero",
            "One",
            "Random Value"});
            this.cbKeyInitialValue.Location = new System.Drawing.Point(236, 169);
            this.cbKeyInitialValue.Name = "cbKeyInitialValue";
            this.cbKeyInitialValue.Size = new System.Drawing.Size(151, 28);
            this.cbKeyInitialValue.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(394, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 32);
            this.label12.TabIndex = 13;
            this.label12.Text = "+1";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(5, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 257);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label15, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.nudRandNameCount, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.nudNoOfRecordsToGenerate, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.label11, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtTotalRecords, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtTotalBlocks, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtTotalNodes, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label13, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.label14, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.nudRandFamilyCount, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.nudRandCityCount, 1, 6);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 7;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(434, 232);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // nudRandNameCount
            // 
            this.nudRandNameCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudRandNameCount.Location = new System.Drawing.Point(203, 136);
            this.nudRandNameCount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudRandNameCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRandNameCount.Name = "nudRandNameCount";
            this.nudRandNameCount.Size = new System.Drawing.Size(227, 26);
            this.nudRandNameCount.TabIndex = 11;
            this.nudRandNameCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRandNameCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // nudNoOfRecordsToGenerate
            // 
            this.nudNoOfRecordsToGenerate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudNoOfRecordsToGenerate.Location = new System.Drawing.Point(203, 103);
            this.nudNoOfRecordsToGenerate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNoOfRecordsToGenerate.Name = "nudNoOfRecordsToGenerate";
            this.nudNoOfRecordsToGenerate.Size = new System.Drawing.Size(227, 26);
            this.nudNoOfRecordsToGenerate.TabIndex = 7;
            this.nudNoOfRecordsToGenerate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudNoOfRecordsToGenerate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(4, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(192, 32);
            this.label11.TabIndex = 6;
            this.label11.Text = "Number of Initial Records:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalRecords
            // 
            this.txtTotalRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTotalRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTotalRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtTotalRecords.Location = new System.Drawing.Point(203, 70);
            this.txtTotalRecords.Name = "txtTotalRecords";
            this.txtTotalRecords.ReadOnly = true;
            this.txtTotalRecords.Size = new System.Drawing.Size(227, 26);
            this.txtTotalRecords.TabIndex = 5;
            this.txtTotalRecords.TabStop = false;
            this.txtTotalRecords.Text = "0";
            this.txtTotalRecords.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalRecords.TextChanged += new System.EventHandler(this.txtTotalRecords_TextChanged);
            // 
            // txtTotalBlocks
            // 
            this.txtTotalBlocks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTotalBlocks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTotalBlocks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtTotalBlocks.Location = new System.Drawing.Point(203, 37);
            this.txtTotalBlocks.Name = "txtTotalBlocks";
            this.txtTotalBlocks.ReadOnly = true;
            this.txtTotalBlocks.Size = new System.Drawing.Size(227, 26);
            this.txtTotalBlocks.TabIndex = 4;
            this.txtTotalBlocks.TabStop = false;
            this.txtTotalBlocks.Text = "0";
            this.txtTotalBlocks.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(4, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(192, 32);
            this.label9.TabIndex = 2;
            this.label9.Text = "Total Blocks:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(4, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(192, 32);
            this.label8.TabIndex = 0;
            this.label8.Text = "Total Nodes (Bukets):";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalNodes
            // 
            this.txtTotalNodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTotalNodes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTotalNodes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtTotalNodes.Location = new System.Drawing.Point(203, 4);
            this.txtTotalNodes.Name = "txtTotalNodes";
            this.txtTotalNodes.ReadOnly = true;
            this.txtTotalNodes.Size = new System.Drawing.Size(227, 26);
            this.txtTotalNodes.TabIndex = 1;
            this.txtTotalNodes.TabStop = false;
            this.txtTotalNodes.Text = "0";
            this.txtTotalNodes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(4, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(192, 32);
            this.label10.TabIndex = 3;
            this.label10.Text = "Total Records:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(4, 133);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(192, 32);
            this.label13.TabIndex = 8;
            this.label13.Text = "Number of random name:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(4, 166);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(192, 32);
            this.label14.TabIndex = 12;
            this.label14.Text = "Number of random family:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudRandFamilyCount
            // 
            this.nudRandFamilyCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudRandFamilyCount.Location = new System.Drawing.Point(203, 169);
            this.nudRandFamilyCount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudRandFamilyCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRandFamilyCount.Name = "nudRandFamilyCount";
            this.nudRandFamilyCount.Size = new System.Drawing.Size(227, 26);
            this.nudRandFamilyCount.TabIndex = 13;
            this.nudRandFamilyCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRandFamilyCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(4, 199);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(192, 32);
            this.label15.TabIndex = 14;
            this.label15.Text = "Number of random city:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudRandCityCount
            // 
            this.nudRandCityCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudRandCityCount.Location = new System.Drawing.Point(203, 202);
            this.nudRandCityCount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudRandCityCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRandCityCount.Name = "nudRandCityCount";
            this.nudRandCityCount.Size = new System.Drawing.Size(227, 26);
            this.nudRandCityCount.TabIndex = 15;
            this.nudRandCityCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRandCityCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // frmServerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(758, 605);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServerSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Server Settings";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKeySizeInBits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecordsPerBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTreeHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChildsPerNode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlocksPerBucket)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandNameCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoOfRecordsToGenerate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandFamilyCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandCityCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nudTreeHeight;
        private System.Windows.Forms.NumericUpDown nudChildsPerNode;
        private System.Windows.Forms.NumericUpDown nudBlocksPerBucket;
        private System.Windows.Forms.NumericUpDown nudRecordsPerBlock;
        private System.Windows.Forms.NumericUpDown nudKeySizeInBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbKeyInitialValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotalNodes;
        private System.Windows.Forms.TextBox txtTotalRecords;
        private System.Windows.Forms.TextBox txtTotalBlocks;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudNoOfRecordsToGenerate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnLoadRecords;
        private System.Windows.Forms.NumericUpDown nudRandNameCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown nudRandFamilyCount;
        private System.Windows.Forms.NumericUpDown nudRandCityCount;
    }
}