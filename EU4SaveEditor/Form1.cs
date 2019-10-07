using System;
using System.Windows.Forms;

namespace EU4SaveEditor
{
    public partial class Form1 : Form
    {
        readonly FormVM _formViewModel = new FormVM();

        public Form1()
        {
            InitializeComponent();
        }

        private void ListBoxCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            _formViewModel.CountryChanged(lbCountries.SelectedItem.ToString(), lbProvinces, labelProvincesCount);
        }

        private void ListBoxProvinces_SelectedIndexChanged(object sender, EventArgs e)
        {
            _formViewModel.ProvinceChanged(lbProvinces, gbProvProsp, tbOrigCltr, tbCltr, tbOrigRlgn, tbRlgn);
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            _formViewModel.OpenFile(lbCountries, lbProvinces, labelLoadedFile, labelCountriesCount);
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            _formViewModel.SaveFile();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            _formViewModel.SetPoints(ref sender, lbProvinces);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            _formViewModel.KeyPress(ref e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            _formViewModel.SaveFile();
        }

        private void tbSearchCountry_TextChanged(object sender, EventArgs e)
        {
            _formViewModel.FindCountry((sender as TextBox)?.Text, ref lbCountries);
        }
    }
}
