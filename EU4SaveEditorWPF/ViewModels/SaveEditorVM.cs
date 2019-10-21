using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Controls;
using Aligres.SaveParser;
using System.IO;
using System.Windows.Input;
using EU4SaveEditorWPF.Commands;

namespace EU4SaveEditorWPF.ViewModels
{
    class SaveEditorVM : SaveEditorVMBase
    {
        private readonly SaveParser _saveParser = SaveParser.GetInstance();

        #region Private variables

        private List<string> _listOfCountries;
        private List<string> _listOfProvinces;

        private string _filePath;

        #endregion

        #region Properties
        public List<string> ListOfCountries
        {
            get => _listOfCountries;
            set { SetProperty(ref _listOfCountries, value); }
        }

        public List<string> ListOfProvinces
        {
            get => _listOfProvinces;
            set { SetProperty(ref _listOfProvinces, value); }
        }

        public string FilePath
        {
            get => _filePath;
            set { SetProperty(ref _filePath, value); }
        }
    
        #endregion

        #region Commands

        public ICommand OpenFileCommand { get; }

        #endregion

        public SaveEditorVM()
        {
            ListOfCountries = new List<string>();
            ListOfProvinces = new List<string>();

            OpenFileCommand = new RelayCommand(o => OpenFile());
        }

        public async void OpenFile()
        {
            var openFileDialog = new OpenFileDialog() 
            { 
                Filter = ConfigurationManager.AppSettings.Get("FileDialogFilter") 
            };
            
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName == string.Empty)
                return;

            FilePath = openFileDialog.FileName;
            
            var sourceFile = await Task.Run(() => File.ReadAllText(FilePath, Encoding.GetEncoding(1252)));

            _saveParser.SaveFile = sourceFile.Split('\n');

            _saveParser.ClearLists();

            await Task.Run(() => _saveParser.FindAllCountries());
            await Task.Run(() => _saveParser.FindAllProvinces());

            ListOfCountries = _saveParser.GetCountries();
        }

        public void GetProvincesOfCountry(string country)
        {
            ListOfCountries = _saveParser.GetProvincesOfContry(country);
        }

        #region Helpers

        private static void AddValsToListBox(IEnumerable<string> list, ListBox listBox)
        {
            foreach (var str in list)
            {
                listBox.Items.Add(str);
            }
        }

        #endregion
    }
}
