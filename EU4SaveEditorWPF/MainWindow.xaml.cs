using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EU4SaveEditorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly WindowVM _windowVM = new WindowVM();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            _windowVM.OpenFile(lbCountries, lbProvinces,lblFilePath );
        }

        private void lbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _windowVM.CountryChanged(lbCountries.SelectedItem.ToString(), lbProvinces);
        }
    }
}
