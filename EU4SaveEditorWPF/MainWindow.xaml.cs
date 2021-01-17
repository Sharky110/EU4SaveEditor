using EU4SaveEditorWPF.ViewModels;
using System.Collections.Generic;
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

        private void lbProvinces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            if (list.SelectedItems.Count <= 1)
                return;

            var context = DataContext as SaveEditorVM;
            context.CurrentProvinceNames = "";

            var tempSelectedItems = new List<string>();

            foreach (var item in list.SelectedItems)
                tempSelectedItems.Add((string)item);

            context.CurrentProvinceNames = string.Join(" ", tempSelectedItems);
        }
    }
}
