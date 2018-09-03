using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EU4SaveEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void FileOutput(string[] FileRows )
        {
            for (int i = 0; i < 10000; i++)
            {
                label2.Text = i.ToString();
                listBox1.Items.Add(FileRows[i]);
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            string SavedFile = File.ReadAllText(@"C:/savegames/Ryazan.eu4");
            string[] FileRows = SavedFile.Split('\n');
            label1.Text = FileRows.Length.ToString();
            FileOutput(FileRows);
        }
    }
}
