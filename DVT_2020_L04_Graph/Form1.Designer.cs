namespace DVT_2020_L04_Graph
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_track = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveImg = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownRZ = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownZ = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.numericUpDown_Fov = new System.Windows.Forms.NumericUpDown();
            this.checkBox_perspective = new System.Windows.Forms.CheckBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.comboBoxX = new System.Windows.Forms.ComboBox();
            this.comboBoxY = new System.Windows.Forms.ComboBox();
            this.comboBoxZ = new System.Windows.Forms.ComboBox();
            this.checkBox_normalize = new System.Windows.Forms.CheckBox();
            this.checkBox_plane = new System.Windows.Forms.CheckBox();
            this.checkBox_all = new System.Windows.Forms.CheckBox();
            this.checkBox_boxPlot = new System.Windows.Forms.CheckBox();
            this.checkBoxXYZ = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Fov)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(346, 27);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(600, 400);
            this.openGLControl.TabIndex = 95;
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 25;
            // 
            // label_track
            // 
            this.label_track.AutoSize = true;
            this.label_track.Location = new System.Drawing.Point(1273, 577);
            this.label_track.Name = "label_track";
            this.label_track.Size = new System.Drawing.Size(13, 13);
            this.label_track.TabIndex = 94;
            this.label_track.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(348, 492);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 92;
            this.label6.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(348, 458);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 91;
            this.label5.Text = "X";
            // 
            // buttonSaveImg
            // 
            this.buttonSaveImg.Location = new System.Drawing.Point(491, 585);
            this.buttonSaveImg.Name = "buttonSaveImg";
            this.buttonSaveImg.Size = new System.Drawing.Size(131, 26);
            this.buttonSaveImg.TabIndex = 89;
            this.buttonSaveImg.Text = "Сохранить картинку";
            this.buttonSaveImg.UseVisualStyleBackColor = true;
            this.buttonSaveImg.Click += new System.EventHandler(this.buttonSaveImg_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(434, 433);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Направление";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 433);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "Положение";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(348, 527);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 93;
            this.label7.Text = "Z";
            // 
            // numericUpDownRZ
            // 
            this.numericUpDownRZ.DecimalPlaces = 3;
            this.numericUpDownRZ.Location = new System.Drawing.Point(437, 522);
            this.numericUpDownRZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownRZ.Name = "numericUpDownRZ";
            this.numericUpDownRZ.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownRZ.TabIndex = 85;
            // 
            // numericUpDownRY
            // 
            this.numericUpDownRY.DecimalPlaces = 3;
            this.numericUpDownRY.Location = new System.Drawing.Point(437, 490);
            this.numericUpDownRY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownRY.Name = "numericUpDownRY";
            this.numericUpDownRY.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownRY.TabIndex = 84;
            // 
            // numericUpDownRX
            // 
            this.numericUpDownRX.DecimalPlaces = 3;
            this.numericUpDownRX.Location = new System.Drawing.Point(437, 458);
            this.numericUpDownRX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownRX.Name = "numericUpDownRX";
            this.numericUpDownRX.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownRX.TabIndex = 83;
            // 
            // numericUpDownZ
            // 
            this.numericUpDownZ.DecimalPlaces = 3;
            this.numericUpDownZ.Location = new System.Drawing.Point(368, 522);
            this.numericUpDownZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownZ.Name = "numericUpDownZ";
            this.numericUpDownZ.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownZ.TabIndex = 82;
            this.numericUpDownZ.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.DecimalPlaces = 3;
            this.numericUpDownY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownY.Location = new System.Drawing.Point(368, 490);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownY.TabIndex = 81;
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.DecimalPlaces = 3;
            this.numericUpDownX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownX.Location = new System.Drawing.Point(368, 458);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownX.TabIndex = 80;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(733, 465);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(131, 26);
            this.buttonLoad.TabIndex = 78;
            this.buttonLoad.Text = "Загрузить";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // numericUpDown_Fov
            // 
            this.numericUpDown_Fov.Location = new System.Drawing.Point(448, 553);
            this.numericUpDown_Fov.Maximum = new decimal(new int[] {
            179,
            0,
            0,
            0});
            this.numericUpDown_Fov.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Fov.Name = "numericUpDown_Fov";
            this.numericUpDown_Fov.Size = new System.Drawing.Size(41, 20);
            this.numericUpDown_Fov.TabIndex = 103;
            this.numericUpDown_Fov.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            // 
            // checkBox_perspective
            // 
            this.checkBox_perspective.AutoSize = true;
            this.checkBox_perspective.Location = new System.Drawing.Point(351, 553);
            this.checkBox_perspective.Name = "checkBox_perspective";
            this.checkBox_perspective.Size = new System.Drawing.Size(93, 17);
            this.checkBox_perspective.TabIndex = 106;
            this.checkBox_perspective.Text = "Перспектива";
            this.checkBox_perspective.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(10, 17);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(330, 595);
            this.dataGridView.TabIndex = 108;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.comboBoxXYZ_SelectedIndexChanged);
            // 
            // comboBoxX
            // 
            this.comboBoxX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxX.FormattingEnabled = true;
            this.comboBoxX.Items.AddRange(new object[] {
            "2=>0",
            "1=>31",
            "31=>1",
            "57=>1",
            "25=>1"});
            this.comboBoxX.Location = new System.Drawing.Point(501, 458);
            this.comboBoxX.Name = "comboBoxX";
            this.comboBoxX.Size = new System.Drawing.Size(121, 21);
            this.comboBoxX.TabIndex = 109;
            this.comboBoxX.SelectedIndexChanged += new System.EventHandler(this.comboBoxXYZ_SelectedIndexChanged);
            // 
            // comboBoxY
            // 
            this.comboBoxY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxY.FormattingEnabled = true;
            this.comboBoxY.Items.AddRange(new object[] {
            "2=>0",
            "1=>31",
            "31=>1",
            "57=>1",
            "25=>1"});
            this.comboBoxY.Location = new System.Drawing.Point(501, 490);
            this.comboBoxY.Name = "comboBoxY";
            this.comboBoxY.Size = new System.Drawing.Size(121, 21);
            this.comboBoxY.TabIndex = 110;
            this.comboBoxY.SelectedIndexChanged += new System.EventHandler(this.comboBoxXYZ_SelectedIndexChanged);
            // 
            // comboBoxZ
            // 
            this.comboBoxZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZ.FormattingEnabled = true;
            this.comboBoxZ.Items.AddRange(new object[] {
            "2=>0",
            "1=>31",
            "31=>1",
            "57=>1",
            "25=>1"});
            this.comboBoxZ.Location = new System.Drawing.Point(501, 521);
            this.comboBoxZ.Name = "comboBoxZ";
            this.comboBoxZ.Size = new System.Drawing.Size(121, 21);
            this.comboBoxZ.TabIndex = 111;
            this.comboBoxZ.SelectedIndexChanged += new System.EventHandler(this.comboBoxXYZ_SelectedIndexChanged);
            // 
            // checkBox_normalize
            // 
            this.checkBox_normalize.AutoSize = true;
            this.checkBox_normalize.Location = new System.Drawing.Point(501, 548);
            this.checkBox_normalize.Name = "checkBox_normalize";
            this.checkBox_normalize.Size = new System.Drawing.Size(102, 17);
            this.checkBox_normalize.TabIndex = 112;
            this.checkBox_normalize.Text = "Нормализация";
            this.checkBox_normalize.UseVisualStyleBackColor = true;
            this.checkBox_normalize.CheckedChanged += new System.EventHandler(this.comboBoxXYZ_SelectedIndexChanged);
            // 
            // checkBox_plane
            // 
            this.checkBox_plane.AutoSize = true;
            this.checkBox_plane.Location = new System.Drawing.Point(645, 462);
            this.checkBox_plane.Name = "checkBox_plane";
            this.checkBox_plane.Size = new System.Drawing.Size(81, 17);
            this.checkBox_plane.TabIndex = 113;
            this.checkBox_plane.Text = "Плоскость";
            this.checkBox_plane.UseVisualStyleBackColor = true;
            // 
            // checkBox_all
            // 
            this.checkBox_all.AutoSize = true;
            this.checkBox_all.Checked = true;
            this.checkBox_all.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_all.Location = new System.Drawing.Point(346, 595);
            this.checkBox_all.Name = "checkBox_all";
            this.checkBox_all.Size = new System.Drawing.Size(110, 17);
            this.checkBox_all.TabIndex = 114;
            this.checkBox_all.Text = "Отобразить все ";
            this.checkBox_all.UseVisualStyleBackColor = true;
            this.checkBox_all.UseWaitCursor = true;
            this.checkBox_all.CheckedChanged += new System.EventHandler(this.comboBoxXYZ_SelectedIndexChanged);
            // 
            // checkBox_boxPlot
            // 
            this.checkBox_boxPlot.AutoSize = true;
            this.checkBox_boxPlot.Location = new System.Drawing.Point(645, 492);
            this.checkBox_boxPlot.Name = "checkBox_boxPlot";
            this.checkBox_boxPlot.Size = new System.Drawing.Size(70, 17);
            this.checkBox_boxPlot.TabIndex = 116;
            this.checkBox_boxPlot.Text = "Графики";
            this.checkBox_boxPlot.UseVisualStyleBackColor = true;
            // 
            // checkBoxXYZ
            // 
            this.checkBoxXYZ.AutoSize = true;
            this.checkBoxXYZ.Location = new System.Drawing.Point(645, 527);
            this.checkBoxXYZ.Name = "checkBoxXYZ";
            this.checkBoxXYZ.Size = new System.Drawing.Size(46, 17);
            this.checkBoxXYZ.TabIndex = 117;
            this.checkBoxXYZ.Text = "Оси";
            this.checkBoxXYZ.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 639);
            this.Controls.Add(this.checkBoxXYZ);
            this.Controls.Add(this.checkBox_boxPlot);
            this.Controls.Add(this.checkBox_all);
            this.Controls.Add(this.checkBox_plane);
            this.Controls.Add(this.checkBox_normalize);
            this.Controls.Add(this.comboBoxZ);
            this.Controls.Add(this.comboBoxY);
            this.Controls.Add(this.comboBoxX);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.checkBox_perspective);
            this.Controls.Add(this.numericUpDown_Fov);
            this.Controls.Add(this.openGLControl);
            this.Controls.Add(this.label_track);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonSaveImg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownRZ);
            this.Controls.Add(this.numericUpDownRY);
            this.Controls.Add(this.numericUpDownRX);
            this.Controls.Add(this.numericUpDownZ);
            this.Controls.Add(this.numericUpDownY);
            this.Controls.Add(this.numericUpDownX);
            this.Controls.Add(this.buttonLoad);
            this.Name = "Form1";
            this.Text = "Щербинин Григорий 201-325";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Fov)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_track;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSaveImg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownRZ;
        private System.Windows.Forms.NumericUpDown numericUpDownRY;
        private System.Windows.Forms.NumericUpDown numericUpDownRX;
        private System.Windows.Forms.NumericUpDown numericUpDownZ;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.NumericUpDown numericUpDown_Fov;
        private System.Windows.Forms.CheckBox checkBox_perspective;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox comboBoxX;
        private System.Windows.Forms.ComboBox comboBoxY;
        private System.Windows.Forms.ComboBox comboBoxZ;
        private System.Windows.Forms.CheckBox checkBox_normalize;
        private System.Windows.Forms.CheckBox checkBox_plane;
        private System.Windows.Forms.CheckBox checkBox_all;
        private System.Windows.Forms.CheckBox checkBox_boxPlot;
        private System.Windows.Forms.CheckBox checkBoxXYZ;
    }
}

