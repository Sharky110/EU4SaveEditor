using EU4SaveEditorWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EU4SaveEditorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new SaveEditorVM();
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.Text, 0));
        }

        private void lbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newSelectedItem = e.AddedItems[0];
            if (newSelectedItem != null)
                (sender as ListBox).ScrollIntoView(newSelectedItem);
        }
    }
}
