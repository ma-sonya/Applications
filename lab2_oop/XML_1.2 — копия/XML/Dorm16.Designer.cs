
namespace XML
{
    partial class Dorm16
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.URLlabel = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.GroupCheckBox = new System.Windows.Forms.CheckBox();
            this.FaculityComboBox = new System.Windows.Forms.ComboBox();
            this.FloorComboBox = new System.Windows.Forms.ComboBox();
            this.RoomComboBox = new System.Windows.Forms.ComboBox();
            this.GroupComboBox = new System.Windows.Forms.ComboBox();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NameComboBox = new System.Windows.Forms.ComboBox();
            this.FloorCheckBox = new System.Windows.Forms.CheckBox();
            this.RoomCheckBox = new System.Windows.Forms.CheckBox();
            this.FacultyCheckBox = new System.Windows.Forms.CheckBox();
            this.OpenHtmlButton = new System.Windows.Forms.Button();
            this.CleanButton = new System.Windows.Forms.Button();
            this.ToHtmlButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.LINQRadioButton = new System.Windows.Forms.RadioButton();
            this.DOMRadioButton = new System.Windows.Forms.RadioButton();
            this.SAXRadioButton = new System.Windows.Forms.RadioButton();
            this.NameCheckBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MyDataGridView = new System.Windows.Forms.DataGridView();
            this.FacultyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AudienceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurriculumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StudentsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YearsOfStudyingComboBox = new System.Windows.Forms.ComboBox();
            this.YearsOfStudyingCheckBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MyDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(72, 29);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // URLlabel
            // 
            this.URLlabel.AutoSize = true;
            this.URLlabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.URLlabel.Location = new System.Drawing.Point(402, 63);
            this.URLlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.URLlabel.Name = "URLlabel";
            this.URLlabel.Size = new System.Drawing.Size(73, 22);
            this.URLlabel.TabIndex = 48;
            this.URLlabel.Text = "File URL";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // GroupCheckBox
            // 
            this.GroupCheckBox.AutoSize = true;
            this.GroupCheckBox.BackColor = System.Drawing.Color.LightSlateGray;
            this.GroupCheckBox.ForeColor = System.Drawing.Color.AliceBlue;
            this.GroupCheckBox.Location = new System.Drawing.Point(24, 146);
            this.GroupCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupCheckBox.Name = "GroupCheckBox";
            this.GroupCheckBox.Size = new System.Drawing.Size(80, 24);
            this.GroupCheckBox.TabIndex = 46;
            this.GroupCheckBox.Text = "Group";
            this.GroupCheckBox.UseVisualStyleBackColor = false;
            this.GroupCheckBox.CheckedChanged += new System.EventHandler(this.GroupCheckBox_CheckedChanged);
            // 
            // FaculityComboBox
            // 
            this.FaculityComboBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.FaculityComboBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FaculityComboBox.FormattingEnabled = true;
            this.FaculityComboBox.Location = new System.Drawing.Point(195, 102);
            this.FaculityComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FaculityComboBox.Name = "FaculityComboBox";
            this.FaculityComboBox.Size = new System.Drawing.Size(180, 28);
            this.FaculityComboBox.TabIndex = 45;
            // 
            // FloorComboBox
            // 
            this.FloorComboBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.FloorComboBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FloorComboBox.FormattingEnabled = true;
            this.FloorComboBox.Location = new System.Drawing.Point(195, 272);
            this.FloorComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FloorComboBox.Name = "FloorComboBox";
            this.FloorComboBox.Size = new System.Drawing.Size(180, 28);
            this.FloorComboBox.TabIndex = 43;
            // 
            // RoomComboBox
            // 
            this.RoomComboBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.RoomComboBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.RoomComboBox.FormattingEnabled = true;
            this.RoomComboBox.Location = new System.Drawing.Point(195, 231);
            this.RoomComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RoomComboBox.Name = "RoomComboBox";
            this.RoomComboBox.Size = new System.Drawing.Size(180, 28);
            this.RoomComboBox.TabIndex = 42;
            // 
            // GroupComboBox
            // 
            this.GroupComboBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.GroupComboBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.GroupComboBox.FormattingEnabled = true;
            this.GroupComboBox.Location = new System.Drawing.Point(195, 143);
            this.GroupComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupComboBox.Name = "GroupComboBox";
            this.GroupComboBox.Size = new System.Drawing.Size(180, 28);
            this.GroupComboBox.TabIndex = 40;
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(70, 29);
            this.HelpToolStripMenuItem.Text = "Help!";
            this.HelpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // NameComboBox
            // 
            this.NameComboBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.NameComboBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.NameComboBox.FormattingEnabled = true;
            this.NameComboBox.Location = new System.Drawing.Point(195, 60);
            this.NameComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NameComboBox.Name = "NameComboBox";
            this.NameComboBox.Size = new System.Drawing.Size(180, 28);
            this.NameComboBox.TabIndex = 39;
            // 
            // FloorCheckBox
            // 
            this.FloorCheckBox.AutoSize = true;
            this.FloorCheckBox.BackColor = System.Drawing.Color.LightSlateGray;
            this.FloorCheckBox.ForeColor = System.Drawing.Color.AliceBlue;
            this.FloorCheckBox.Location = new System.Drawing.Point(24, 272);
            this.FloorCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FloorCheckBox.Name = "FloorCheckBox";
            this.FloorCheckBox.Size = new System.Drawing.Size(71, 24);
            this.FloorCheckBox.TabIndex = 38;
            this.FloorCheckBox.Text = "Floor";
            this.FloorCheckBox.UseVisualStyleBackColor = false;
            this.FloorCheckBox.CheckedChanged += new System.EventHandler(this.FloorCheckBox_CheckedChanged);
            // 
            // RoomCheckBox
            // 
            this.RoomCheckBox.AutoSize = true;
            this.RoomCheckBox.BackColor = System.Drawing.Color.LightSlateGray;
            this.RoomCheckBox.ForeColor = System.Drawing.Color.AliceBlue;
            this.RoomCheckBox.Location = new System.Drawing.Point(24, 231);
            this.RoomCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RoomCheckBox.Name = "RoomCheckBox";
            this.RoomCheckBox.Size = new System.Drawing.Size(78, 24);
            this.RoomCheckBox.TabIndex = 37;
            this.RoomCheckBox.Text = "Room";
            this.RoomCheckBox.UseVisualStyleBackColor = false;
            this.RoomCheckBox.CheckedChanged += new System.EventHandler(this.RoomCheckBox_CheckedChanged);
            // 
            // FacultyCheckBox
            // 
            this.FacultyCheckBox.AutoSize = true;
            this.FacultyCheckBox.BackColor = System.Drawing.Color.LightSlateGray;
            this.FacultyCheckBox.ForeColor = System.Drawing.Color.AliceBlue;
            this.FacultyCheckBox.Location = new System.Drawing.Point(24, 105);
            this.FacultyCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FacultyCheckBox.Name = "FacultyCheckBox";
            this.FacultyCheckBox.Size = new System.Drawing.Size(86, 24);
            this.FacultyCheckBox.TabIndex = 35;
            this.FacultyCheckBox.Text = "Faculty";
            this.FacultyCheckBox.UseVisualStyleBackColor = false;
            this.FacultyCheckBox.CheckedChanged += new System.EventHandler(this.FacultyCheckBox_CheckedChanged);
            // 
            // OpenHtmlButton
            // 
            this.OpenHtmlButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.OpenHtmlButton.Location = new System.Drawing.Point(18, 476);
            this.OpenHtmlButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OpenHtmlButton.Name = "OpenHtmlButton";
            this.OpenHtmlButton.Size = new System.Drawing.Size(176, 129);
            this.OpenHtmlButton.TabIndex = 34;
            this.OpenHtmlButton.Text = "Open HTML";
            this.OpenHtmlButton.UseVisualStyleBackColor = false;
            this.OpenHtmlButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // CleanButton
            // 
            this.CleanButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.CleanButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.CleanButton.Location = new System.Drawing.Point(18, 614);
            this.CleanButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CleanButton.Name = "CleanButton";
            this.CleanButton.Size = new System.Drawing.Size(375, 69);
            this.CleanButton.TabIndex = 33;
            this.CleanButton.Text = "Clean";
            this.CleanButton.UseVisualStyleBackColor = false;
            this.CleanButton.Click += new System.EventHandler(this.CleanButton_Click);
            // 
            // ToHtmlButton
            // 
            this.ToHtmlButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ToHtmlButton.Location = new System.Drawing.Point(217, 476);
            this.ToHtmlButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ToHtmlButton.Name = "ToHtmlButton";
            this.ToHtmlButton.Size = new System.Drawing.Size(176, 129);
            this.ToHtmlButton.TabIndex = 32;
            this.ToHtmlButton.Text = "Convert to HTML";
            this.ToHtmlButton.UseVisualStyleBackColor = false;
            this.ToHtmlButton.Click += new System.EventHandler(this.ToHtmlButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.SearchButton.ForeColor = System.Drawing.Color.White;
            this.SearchButton.Location = new System.Drawing.Point(18, 397);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(375, 69);
            this.SearchButton.TabIndex = 31;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // LINQRadioButton
            // 
            this.LINQRadioButton.AutoSize = true;
            this.LINQRadioButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.LINQRadioButton.Location = new System.Drawing.Point(240, 362);
            this.LINQRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LINQRadioButton.Name = "LINQRadioButton";
            this.LINQRadioButton.Size = new System.Drawing.Size(71, 24);
            this.LINQRadioButton.TabIndex = 30;
            this.LINQRadioButton.TabStop = true;
            this.LINQRadioButton.Text = "LINQ";
            this.LINQRadioButton.UseVisualStyleBackColor = true;
            // 
            // DOMRadioButton
            // 
            this.DOMRadioButton.AutoSize = true;
            this.DOMRadioButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.DOMRadioButton.Location = new System.Drawing.Point(156, 362);
            this.DOMRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DOMRadioButton.Name = "DOMRadioButton";
            this.DOMRadioButton.Size = new System.Drawing.Size(71, 24);
            this.DOMRadioButton.TabIndex = 29;
            this.DOMRadioButton.TabStop = true;
            this.DOMRadioButton.Text = "DOM";
            this.DOMRadioButton.UseVisualStyleBackColor = true;
            // 
            // SAXRadioButton
            // 
            this.SAXRadioButton.AutoSize = true;
            this.SAXRadioButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.SAXRadioButton.Location = new System.Drawing.Point(78, 362);
            this.SAXRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SAXRadioButton.Name = "SAXRadioButton";
            this.SAXRadioButton.Size = new System.Drawing.Size(67, 24);
            this.SAXRadioButton.TabIndex = 28;
            this.SAXRadioButton.TabStop = true;
            this.SAXRadioButton.Text = "SAX";
            this.SAXRadioButton.UseVisualStyleBackColor = true;
            // 
            // NameCheckBox
            // 
            this.NameCheckBox.AutoSize = true;
            this.NameCheckBox.BackColor = System.Drawing.Color.LightSlateGray;
            this.NameCheckBox.ForeColor = System.Drawing.Color.AliceBlue;
            this.NameCheckBox.Location = new System.Drawing.Point(24, 63);
            this.NameCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NameCheckBox.Name = "NameCheckBox";
            this.NameCheckBox.Size = new System.Drawing.Size(77, 24);
            this.NameCheckBox.TabIndex = 26;
            this.NameCheckBox.Text = "Name";
            this.NameCheckBox.UseVisualStyleBackColor = false;
            this.NameCheckBox.CheckedChanged += new System.EventHandler(this.NameCheckBox_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.HelpToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1634, 33);
            this.menuStrip1.TabIndex = 49;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MyDataGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.MyDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MyDataGridView.BackgroundColor = System.Drawing.Color.LightSlateGray;
            this.MyDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MyDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MyDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FacultyColumn,
            this.DepartmentColumn,
            this.SectionColumn,
            this.NameColumn,
            this.AudienceColumn,
            this.CurriculumColumn,
            this.StudentsColumn});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MyDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.MyDataGridView.Location = new System.Drawing.Point(402, 89);
            this.MyDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MyDataGridView.Name = "MyDataGridView";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MyDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.MyDataGridView.RowHeadersVisible = false;
            this.MyDataGridView.RowHeadersWidth = 62;
            this.MyDataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
            this.MyDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MyDataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.MyDataGridView.RowTemplate.Height = 70;
            this.MyDataGridView.Size = new System.Drawing.Size(1221, 514);
            this.MyDataGridView.TabIndex = 52;
            this.MyDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MyDataGridView_CellContentClick);
            // 
            // FacultyColumn
            // 
            this.FacultyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FacultyColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.FacultyColumn.FillWeight = 65F;
            this.FacultyColumn.HeaderText = "Faculty";
            this.FacultyColumn.MinimumWidth = 8;
            this.FacultyColumn.Name = "FacultyColumn";
            this.FacultyColumn.ReadOnly = true;
            // 
            // DepartmentColumn
            // 
            this.DepartmentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DepartmentColumn.HeaderText = "Group";
            this.DepartmentColumn.MinimumWidth = 8;
            this.DepartmentColumn.Name = "DepartmentColumn";
            this.DepartmentColumn.ReadOnly = true;
            // 
            // SectionColumn
            // 
            this.SectionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SectionColumn.HeaderText = "Years of studying";
            this.SectionColumn.MinimumWidth = 8;
            this.SectionColumn.Name = "SectionColumn";
            this.SectionColumn.ReadOnly = true;
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 8;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            // 
            // AudienceColumn
            // 
            this.AudienceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AudienceColumn.FillWeight = 55F;
            this.AudienceColumn.HeaderText = "Room";
            this.AudienceColumn.MinimumWidth = 8;
            this.AudienceColumn.Name = "AudienceColumn";
            this.AudienceColumn.ReadOnly = true;
            // 
            // CurriculumColumn
            // 
            this.CurriculumColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CurriculumColumn.HeaderText = "Floor";
            this.CurriculumColumn.MinimumWidth = 8;
            this.CurriculumColumn.Name = "CurriculumColumn";
            this.CurriculumColumn.ReadOnly = true;
            // 
            // StudentsColumn
            // 
            this.StudentsColumn.HeaderText = "Number";
            this.StudentsColumn.MinimumWidth = 8;
            this.StudentsColumn.Name = "StudentsColumn";
            this.StudentsColumn.ReadOnly = true;
            this.StudentsColumn.Width = 140;
            // 
            // YearsOfStudyingComboBox
            // 
            this.YearsOfStudyingComboBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.YearsOfStudyingComboBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.YearsOfStudyingComboBox.FormattingEnabled = true;
            this.YearsOfStudyingComboBox.Location = new System.Drawing.Point(195, 188);
            this.YearsOfStudyingComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.YearsOfStudyingComboBox.Name = "YearsOfStudyingComboBox";
            this.YearsOfStudyingComboBox.Size = new System.Drawing.Size(180, 28);
            this.YearsOfStudyingComboBox.TabIndex = 54;
            // 
            // YearsOfStudyingCheckBox
            // 
            this.YearsOfStudyingCheckBox.AutoSize = true;
            this.YearsOfStudyingCheckBox.BackColor = System.Drawing.Color.LightSlateGray;
            this.YearsOfStudyingCheckBox.ForeColor = System.Drawing.Color.AliceBlue;
            this.YearsOfStudyingCheckBox.Location = new System.Drawing.Point(24, 188);
            this.YearsOfStudyingCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.YearsOfStudyingCheckBox.Name = "YearsOfStudyingCheckBox";
            this.YearsOfStudyingCheckBox.Size = new System.Drawing.Size(158, 24);
            this.YearsOfStudyingCheckBox.TabIndex = 53;
            this.YearsOfStudyingCheckBox.Text = "Years of studying";
            this.YearsOfStudyingCheckBox.UseVisualStyleBackColor = false;
            this.YearsOfStudyingCheckBox.CheckedChanged += new System.EventHandler(this.YearsOfStudyingCheckBox_CheckedChanged_1);
            // 
            // Dorm16
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1634, 692);
            this.Controls.Add(this.YearsOfStudyingComboBox);
            this.Controls.Add(this.YearsOfStudyingCheckBox);
            this.Controls.Add(this.MyDataGridView);
            this.Controls.Add(this.URLlabel);
            this.Controls.Add(this.GroupCheckBox);
            this.Controls.Add(this.FaculityComboBox);
            this.Controls.Add(this.FloorComboBox);
            this.Controls.Add(this.RoomComboBox);
            this.Controls.Add(this.GroupComboBox);
            this.Controls.Add(this.NameComboBox);
            this.Controls.Add(this.FloorCheckBox);
            this.Controls.Add(this.RoomCheckBox);
            this.Controls.Add(this.FacultyCheckBox);
            this.Controls.Add(this.OpenHtmlButton);
            this.Controls.Add(this.CleanButton);
            this.Controls.Add(this.ToHtmlButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.LINQRadioButton);
            this.Controls.Add(this.DOMRadioButton);
            this.Controls.Add(this.SAXRadioButton);
            this.Controls.Add(this.NameCheckBox);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dorm16";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dorm 16";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyTimeTable_FormClosing);
            this.Load += new System.EventHandler(this.MyTimeTable_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MyDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.Label URLlabel;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.CheckBox GroupCheckBox;
        private System.Windows.Forms.ComboBox FaculityComboBox;
        private System.Windows.Forms.ComboBox FloorComboBox;
        private System.Windows.Forms.ComboBox RoomComboBox;
        private System.Windows.Forms.ComboBox GroupComboBox;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ComboBox NameComboBox;
        private System.Windows.Forms.CheckBox FloorCheckBox;
        private System.Windows.Forms.CheckBox RoomCheckBox;
        private System.Windows.Forms.CheckBox FacultyCheckBox;
        private System.Windows.Forms.Button OpenHtmlButton;
        private System.Windows.Forms.Button CleanButton;
        private System.Windows.Forms.Button ToHtmlButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.RadioButton LINQRadioButton;
        private System.Windows.Forms.RadioButton DOMRadioButton;
        private System.Windows.Forms.RadioButton SAXRadioButton;
        private System.Windows.Forms.CheckBox NameCheckBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView MyDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacultyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AudienceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurriculumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StudentsColumn;
        private System.Windows.Forms.ComboBox YearsOfStudyingComboBox;
        private System.Windows.Forms.CheckBox YearsOfStudyingCheckBox;
    }
}

