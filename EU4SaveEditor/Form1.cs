using System;
using System.Windows.Forms;

namespace EU4SaveEditor
{
    public partial class Form1 : Form
    {
        readonly FormVM _formVM = new FormVM();

        public Form1()
        {
            InitializeComponent();
        }

        private void ListBoxCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            _formVM.CountryChanged(lbCountries.SelectedItem.ToString(), lbProvinces, labelProvincesCount);
        }

        private void ListBoxProvinces_SelectedIndexChanged(object sender, EventArgs e)
        {
            _formVM.ProvinceChanged(lbProvinces, gbProvProsp, tbOrigCltr, tbCltr, tbOrigRlgn, tbRlgn);
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            _formVM.OpenFile(lbCountries, lbProvinces, labelLoadedFile, labelCountriesCount);
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            _formVM.SaveFile();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            _formVM.SetPoints(sender, lbProvinces);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            _formVM.KeyPress(e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            _formVM.SaveFile();
        }

        private void tbSearchCountry_TextChanged(object sender, EventArgs e)
        {
            _formVM.FindCountry((sender as TextBox)?.Text, lbCountries);
        }
    }
}
