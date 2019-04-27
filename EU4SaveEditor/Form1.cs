using System;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace EU4SaveEditor
{
    public partial class Form1 : Form
    {
        SearchEngine searchEngine = new SearchEngine();

        public Form1()
        {
            InitializeComponent();
        }

        private void ListBoxCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchEngine.CountryChanged(lbCountries.SelectedItem.ToString(), ref lbProvinces, ref labelProvincesCount);
        }

        private void ListBoxProvinces_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchEngine.ProvinceChanged(ref lbProvinces, ref gbProvProsp, ref tbOrigCltr, ref tbCltr, ref tbOrigRlgn, ref tbRlgn);
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            searchEngine.OpenFile(ref lbCountries, ref lbProvinces, ref labelLoadedFile, ref labelCountriesCount);
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            searchEngine.SaveFile();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            searchEngine.SetPoints(ref sender, lbProvinces);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            searchEngine.KeyPress(ref e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            searchEngine.SaveFile();
        }

        private void tbSearchCountry_TextChanged(object sender, EventArgs e)
        {
            searchEngine.FindCountry((sender as TextBox).Text, ref lbCountries);
        }
    }
}
