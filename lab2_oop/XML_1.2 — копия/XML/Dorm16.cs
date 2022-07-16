using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Xsl;
using System.IO;

namespace XML
{
    public partial class Dorm16 : Form
    {
        private string path = StartForm.PATH; // FILE
        private readonly string pathPartXml = @"PartXml.xml"; // Part Xml FILE

        private readonly string pathXsl = @"XSL.xsl"; // XSL for converting to HTML

        private readonly string pathHtml = @"HTML.html"; // All HTML file
        private readonly string pathPartHtml = @"PartHTML.html"; // Part HTML file

        private bool isToHtmlPressed = false;
        private bool isToPartHtmlPressed = false;
        private bool isNew;


        public Dorm16()
        {
            InitializeComponent();

            BuildBox(NameComboBox, GroupComboBox, YearsOfStudyingComboBox, RoomComboBox, FloorComboBox, FaculityComboBox);
                        
        }


        #region Functions

        private void BuildBox(ComboBox Name, ComboBox Group, ComboBox YearsOfStudying, ComboBox Room, ComboBox Floor, ComboBox Faculty)
        {
            if (path != "")
            {
                Name.Items.Clear();
                Group.Items.Clear();
                YearsOfStudying.Items.Clear();
                Room.Items.Clear();
                Floor.Items.Clear();
                Faculty.Items.Clear();

                IParse p = new LINQ();
                List<Search> res = p.AnalizeFile(new Search(), path);
                List<string> name = new List<string>();
                List<string> faculty = new List<string>();
                List<string> group = new List<string>();
                List<string> yearsofstudying = new List<string>();
                List<string> room = new List<string>();
                List<string> floor = new List<string>();

                foreach (Search elem in res)
                {
                    if (!faculty.Contains(elem.faculty))
                    {
                        faculty.Add(elem.faculty);
                    }
                    if (!name.Contains(elem.name))
                    {
                        name.Add(elem.name);
                    }
                    if (!group.Contains(elem.group))
                    {
                        group.Add(elem.group);
                    }
                    if (!yearsofstudying.Contains(elem.yearsofstudying))
                    {
                        yearsofstudying.Add(elem.yearsofstudying);
                    }
                    if (!room.Contains(elem.audience))
                    {
                        room.Add(elem.audience);
                    }
                    if (!floor.Contains(elem.floor))
                    {
                        floor.Add(elem.floor);
                    }
                }

                name = name.OrderBy(x => x).ToList();
                faculty = faculty.OrderBy(x => x).ToList();
                group = group.OrderBy(x => x).ToList();
                yearsofstudying = yearsofstudying.OrderBy(x => x).ToList();
                room = room.OrderBy(x => x).ToList();
                floor = floor.OrderBy(x => x).ToList();

                Name.Items.AddRange(name.ToArray());
                Faculty.Items.AddRange(faculty.ToArray());
                Group.Items.AddRange(group.ToArray());
                YearsOfStudying.Items.AddRange(yearsofstudying.ToArray());
                Room.Items.AddRange(room.ToArray());
                Floor.Items.AddRange(floor.ToArray());
            }
        }

        private void ParseNames(object sender)
        {
            CheckBox temp = sender as CheckBox;
            if (path != "")
            {
                switch (temp.Text)
                {
                    case "Name":
                        NameComboBox.Text = "";
                        if (temp.CheckState == CheckState.Checked) NameComboBox.Enabled = true;
                        else NameComboBox.Enabled = false;
                        break;
                    case "Faculty":
                        FaculityComboBox.Text = "";
                        if (temp.CheckState == CheckState.Checked) FaculityComboBox.Enabled = true;
                        else FaculityComboBox.Enabled = false;
                        break;
                    case "Group":
                        GroupComboBox.Text = "";
                        if (temp.CheckState == CheckState.Checked) GroupComboBox.Enabled = true;
                        else GroupComboBox.Enabled = false;
                        break;
                    case "Years of studying":
                        YearsOfStudyingComboBox.Text = "";
                        if (temp.CheckState == CheckState.Checked) YearsOfStudyingComboBox.Enabled = true;
                        else YearsOfStudyingComboBox.Enabled = false;
                        break;
                    case "Room":
                        RoomComboBox.Text = "";
                        if (temp.CheckState == CheckState.Checked) RoomComboBox.Enabled = true;
                        else RoomComboBox.Enabled = false;
                        break;
                    case "Floor":
                        FloorComboBox.Text = "";
                        if (temp.CheckState == CheckState.Checked) FloorComboBox.Enabled = true;
                        else FloorComboBox.Enabled = false;
                        break;
                }
            }
        }

        private Search OurSearch()
        {
            string[] info = new string[7];
            if (NameCheckBox.Checked) info[0] = Convert.ToString(NameComboBox.Text);
            if (GroupCheckBox.Checked) info[1] = Convert.ToString(GroupComboBox.Text);
            if (YearsOfStudyingCheckBox.Checked) info[2] = Convert.ToString(YearsOfStudyingComboBox.Text);
            if (RoomCheckBox.Checked) info[4] = Convert.ToString(RoomComboBox.Text);
            if (FloorCheckBox.Checked) info[5] = Convert.ToString(FloorComboBox.Text);
            if (FacultyCheckBox.Checked) info[6] = Convert.ToString(FaculityComboBox.Text);
            Search IdealSearch = new Search(info);
            return IdealSearch;
        }

        private void Parse4XML()
        {
            Search myTemplate = OurSearch();
            XmlWriters XW = new XmlWriters();
            MyDataGridView.Rows.Clear();
            List<Search> res;

            if (SAXRadioButton.Checked)
            {
                IParse parser = new SAX();
                res = parser.AnalizeFile(myTemplate, path);
                XW.WriteToXml(res);
                Output(res);                
            }
            if (DOMRadioButton.Checked)
            {
                IParse parser = new DOM();
                res = parser.AnalizeFile(myTemplate, path);
                XW.WriteToXml(res);
                Output(res);
            }
            if (LINQRadioButton.Checked)
            {
                IParse parser = new LINQ();
                res = parser.AnalizeFile(myTemplate, path);
                XW.WriteToXml(res);
                Output(res);
            }
        }

        private void Output(List<Search> res)
        {
            this.MyDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            int count = (File.ReadAllLines(pathPartXml).Length - 4) / 5;
            if (count > 1) MyDataGridView.Rows.Add(count - 1);

            int indexCell = 0;

            foreach (Search n in res)
            {
                bool firstName = true;                

                for (int j = 0; j < MyDataGridView.Columns.Count; j++)
                {                    
                    MyDataGridView.Rows[indexCell].Cells[0].Value = n.faculty;
                    MyDataGridView.Rows[indexCell].Cells[1].Value = n.group;
                    MyDataGridView.Rows[indexCell].Cells[2].Value = n.yearsofstudying;
                    MyDataGridView.Rows[indexCell].Cells[3].Value = n.name;
                    MyDataGridView.Rows[indexCell].Cells[4].Value = n.audience;
                    MyDataGridView.Rows[indexCell].Cells[5].Value = n.floor;                    
                }
                foreach (var item in n.students)
                {
                    MyDataGridView.Rows[indexCell].Cells[6].Value += item + " ";
                }


                indexCell++;
            }
        }

        private void IntoHTML(string XSL, string PATH, string HTML)
        {
            XslCompiledTransform xlst = new XslCompiledTransform();
            xlst.Load(XSL);
            string input = PATH;
            string result = HTML;
            xlst.Transform(input, result);
            MessageBox.Show("Done!");
        }

        private void OpenDoc()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                InitialDirectory = "D:",
                Filter = "xml files(*.xml)|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                URLlabel.Text = OpenFileDialog.FileName;
                isNew = true;
                path = URLlabel.Text;
                Clear();
                BuildBox(NameComboBox, GroupComboBox, YearsOfStudyingComboBox, RoomComboBox, FloorComboBox, FaculityComboBox);
            }
            if (!isNew)
            {
                path = "";
                isNew = false;
            }
        }

        private void Clear()
        {
            isToHtmlPressed = false;
            isToPartHtmlPressed = false;

            NameCheckBox.Checked = false;
            FacultyCheckBox.Checked = false;
            GroupCheckBox.Checked = false;
            YearsOfStudyingCheckBox.Checked = false;
            RoomCheckBox.Checked = false;
            FloorCheckBox.Checked = false;

            NameComboBox.Text = null;
            GroupComboBox.Text = null;
            YearsOfStudyingComboBox.Text = null;
            RoomComboBox.Text = null;
            FloorComboBox.Text = null;

            SAXRadioButton.Checked = false;
            DOMRadioButton.Checked = false;
            LINQRadioButton.Checked = false;

            File.WriteAllText(pathPartXml, String.Empty);
            MyDataGridView.Rows.Clear();
        }

        #endregion Functions

        #region Events

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (path == "")
            {
                MessageBox.Show("Please select a file");
            }
            else
            {
                if (!SAXRadioButton.Checked && !DOMRadioButton.Checked && !LINQRadioButton.Checked)
                {
                    MessageBox.Show("Choose a method!");
                }
                Parse4XML();
            }
        }

        private void ToHtmlButton_Click(object sender, EventArgs e)
        {
            if (path == "")
            {
                MessageBox.Show("Please select a file");
            }
            else
            {
                IntoHTML(pathXsl, path, pathHtml);
                isToHtmlPressed = true;
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (isToHtmlPressed)
            {
                System.Diagnostics.Process.Start(pathHtml);
            }
            else
                MessageBox.Show("Please, create a document");
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void OpenPartButton_Click(object sender, EventArgs e)
        {
            if (isToPartHtmlPressed)
            {
                System.Diagnostics.Process.Start(pathPartHtml);
            }
            else
                MessageBox.Show("Please, create a document");
        }

        

        private void MyTimeTable_Load(object sender, EventArgs e)
        {
            FaculityComboBox.Enabled = false;
            NameComboBox.Enabled = false;
            GroupComboBox.Enabled = false;
            YearsOfStudyingComboBox.Enabled = false;
            RoomComboBox.Enabled = false;
            FloorComboBox.Enabled = false;
            URLlabel.Text = StartForm.PATH;
            isNew = true;
        }

        private void MyTimeTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Attention!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void NameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ParseNames(NameCheckBox);
        }

        private void FacultyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ParseNames(FacultyCheckBox);
        }

        private void RoomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ParseNames(RoomCheckBox);
        }

        private void FloorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ParseNames(FloorCheckBox);
        }

        private void GroupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ParseNames(GroupCheckBox);
        }

        private void YearsOfStudyingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ParseNames(YearsOfStudyingCheckBox);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDoc();
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Оберіть файл, з яким бажаєте працювати \n" +
               "2. Оберіть метод\n" +
               "3. Натисніть на кнопку \"Search\" для здійснення пошуку за всіма категоріями\n" +
               "4. Якщо бажаєте здійснити пошук за якоюсь категорією, або за декількома -" +
               " оберіть їх та їх значення і натисніть на \"Search\" \n" +
               "5. Якщо ви бажаєте сконвертувати файл в HTML формат, натисніть кнопку \"To HTML\"\n" +
               "6. Натисніть кнопку \"Clear\", щоб очистити вікно \n" +
               "7. Натисніть кнопку \"Help!\" для виклику допомоги.", "Help!", MessageBoxButtons.OK);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Лабораторну роботу виконала \n" +
                "Нікітіна Софія К-24 \n" +
                "Варіант №5 \n", "Help!", MessageBoxButtons.OK);

        }

        #endregion Events

        private void MyDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void YearsOfStudyingCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            ParseNames(YearsOfStudyingCheckBox);
        }
    }
}