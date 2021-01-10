using EU4SaveEditorWPF.Models;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EU4SaveEditorWPF.ViewModels
{
    class SaveEditorVM : SaveEditorVMBase
    {
        private readonly SaveParser _saveParser = SaveParser.GetInstance();

        #region Constants

        private const string DialogFilter = "FileDialogFilter";

        #endregion

        #region Private variables

        private List<string> _listOfCountries;
        private List<string> _listOfProvinces;

        private string _filePath;
        private string _currentCountry;
        private string _currentProvinceName;
        private string _foundCountry;

        private Province _currentProvince;

        #endregion

        #region Properties

        public List<string> ListOfCountries
        {
            get => _listOfCountries;
            set => SetProperty(ref _listOfCountries, value); 
        }

        public List<string> ListOfProvinces
        {
            get => _listOfProvinces;
            set => SetProperty(ref _listOfProvinces, value);
        }

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value); 
        }

        public string CurrentCountry
        {
            get => _currentCountry;
            set
            {
                SetProperty(ref _currentCountry, value);
                GetProvincesOfCountry();
            }
        }

        public Province CurrentProvince
        {
            get
            {
                SetPoints();
                return _currentProvince;
            }
            set => SetProperty(ref _currentProvince, value);
        }

        public string CurrentProvinceName
        {
            get => _currentProvinceName;
            set
            {
                SetProperty(ref _currentProvinceName, value);
                SetCurrentProvince();
            }
        }

        public string FoundCountry
        {
            get => _foundCountry;
            set => SetProperty(ref _foundCountry, value);
        }

        #endregion

        #region Commands

        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }

        #endregion

        public SaveEditorVM()
        {
            OpenFileCommand = new RelayCommand(c => OpenFile());
            SaveFileCommand = new RelayCommand(c => SaveFile());
        }

        public async void OpenFile()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = ConfigurationManager.AppSettings.Get(DialogFilter)
            };

            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName))
                return;

            FilePath = openFileDialog.FileName;

            var sourceFile = await Task.Run(() => File.ReadAllText(FilePath, Encoding.GetEncoding(1252))).ConfigureAwait(false);
           
            _saveParser.SaveFile = await Task.Run(() => sourceFile.Split('\n'));
           
            await Task.Run(() => _saveParser.FindCountriesAndProvinces());
           
            ListOfCountries = _saveParser.GetCountries();
        }

        public void SaveFile()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                MessageBox.Show("File not opened.", "Error.");
                return;
            }
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = ConfigurationManager.AppSettings.Get(DialogFilter)
            };

            saveFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(saveFileDialog.FileName))
                return;

            var saveFile = saveFileDialog.FileName;

            using (var streamWriter = new StreamWriter(saveFile, false, Encoding.GetEncoding(1252)))
            {
                foreach (var row in _saveParser.SaveFile)
                {
                    streamWriter.Write(row + "\n");
                }
            }
        }

        public void FindCountry(string text, ListBox lbCountries)
        {
            for (int i = 0; i < lbCountries.Items.Count; i++)
            {
                if (lbCountries.Items[i].ToString().StartsWith(text))
                {
                    lbCountries.SelectedIndex = i;
                    break;
                }
            }
        }

        public void GetProvincesOfCountry()
        {
            ListOfProvinces = _saveParser.GetProvincesOfContry(CurrentCountry);
        }
        public void SetCurrentProvince()
        {
            CurrentProvince = _saveParser.GetProvince(CurrentProvinceName);
        }

        public void SetPoints()
        {
            if (string.IsNullOrEmpty(FilePath) || _currentProvince.Adm == null)
                return;

            _saveParser.SaveFile[_currentProvince.AdmId] = "    base_tax=" + _currentProvince.Adm;
            _saveParser.SaveFile[_currentProvince.DipId] = "    base_production=" + _currentProvince.Dip;
            _saveParser.SaveFile[_currentProvince.MilId] = "    base_manpower=" + _currentProvince.Mil;
        }
    }
}
