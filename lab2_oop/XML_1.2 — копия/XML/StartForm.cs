using System;
using System.Windows.Forms;

namespace XML
{
    public partial class StartForm : Form
    {
        public static string PATH;
        private bool isHide;

        private string exitMessage = "Are you sure you want to quit?";
        private string helpMessage = "First of all, choose a path";
        private string attent = "Attention!";
        private string help = "Help";

        public StartForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChooseButton_Click(object sender, EventArgs e)
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
                PATH = URLlabel.Text;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            var prog = new Dorm16();
            PATH = URLlabel.Text;
            isHide = true;
            prog.Closed += (s, args) => this.Close();
            prog.Show();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
        }

        private void URLlabel_TextChanged(object sender, EventArgs e)
        {
            StartButton.Enabled = true;
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isHide)
            {
                if (MessageBox.Show(exitMessage, attent, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(helpMessage, help, MessageBoxButtons.OK);
        }

        #region Ficha
        private void FastPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PATH = @"D:\Vs_project\Files\TimeTableDataBase.xml";
            URLlabel.Text = PATH;
        }
        #endregion Ficha

        #region Language
        private void UAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpToolStripMenuItem.Text = "Допомога";
            LangToolStripMenuItem.Text = "Мова";
            StartButton.Text = "Старт";
            ChooseButton.Text = "Обрати файл";
            ExitButton.Text = "Вихід";
            GroupBox.Text = "Шлях";
            exitMessage = "Ви дійсно бажаєте завершити роботу?";
            attent = "Увага!";
            helpMessage = "Для початку роботи оберіть файл!";
            help = "Допомога!";            
        }
        private void ENGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpToolStripMenuItem.Text = "Help!";
            LangToolStripMenuItem.Text = "Language";
            StartButton.Text = "Start";
            ChooseButton.Text = "Choose";
            ExitButton.Text = "Exit";
            GroupBox.Text = "URL";
            exitMessage = "Are you sure you want to quit?";
            attent = "Attention!";
            helpMessage = "First of all, choose a file!";
            help = "Help!";
        }
        private void RUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpToolStripMenuItem.Text = "Помощь";
            LangToolStripMenuItem.Text = "Язык";
            StartButton.Text = "Старт";
            ChooseButton.Text = "Выбрать файл";
            ExitButton.Text = "Выход";
            GroupBox.Text = "Путь";
            exitMessage = "Вы хотите завершить роботу?";
            attent = "Внимание!";
            helpMessage = "Для начала роботы выберите файл!";
            help = "Помощь!";
        }
        #endregion Language       
    }
}
