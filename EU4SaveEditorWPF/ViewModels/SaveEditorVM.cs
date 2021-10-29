using EU4SaveEditorWPF.Enums;
using EU4SaveEditorWPF.Models;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EU4SaveEditorWPF.ViewModels
{
    class SaveEditorVM : SaveEditorVMBase
    {
        private readonly SaveParser _saveParser = SaveParser.GetInstance();

        #region Constants

        private const string DialogFilter = "FileDialogFilter";

        #endregion

        #region Properties

        private List<string> _listOfCountries;
        public List<string> ListOfCountries
        {
            get => _listOfCountries;
            set => SetProperty(ref _listOfCountries, value); 
        }

        private List<string> _listOfProvinces;
        public List<string> ListOfProvinces
        {
            get => _listOfProvinces;
            set => SetProperty(ref _listOfProvinces, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value); 
        }

        private string _currentCountry;
        public string CurrentCountry
        {
            get => _currentCountry;
            set
            {
                SetProperty(ref _currentCountry, value);
                GetProvincesOfCountry();
            }
        }
        private bool _isAlreadyCalledSetPoints;
        private Province _currentProvince;
        public Province CurrentProvince
        {
            get
            {
                if (!_isAlreadyCalledSetPoints)
                {
                    _isAlreadyCalledSetPoints = true;
                    SetPoints();
                    _isAlreadyCalledSetPoints = false;
                }
                return _currentProvince;
            }
            set => SetProperty(ref _currentProvince, value);
        }

        private List<Province> _currentProvinces = new List<Province>();
        public List<Province> CurrentProvinces
        {
            get
            {
                return _currentProvinces;
            }
            set => SetProperty(ref _currentProvinces, value);
        }

        private string _currentProvinceNames;
        public string CurrentProvinceNames
        {
            get => string.Join(",", _currentProvinceNames);
            set
            {
                SetProperty(ref _currentProvinceNames, value);
                SetCurrentProvinces();
            }
        }

        private string _playerCountry;
        public string PlayerCountry
        {
            get
            {
                return Country.CountryNames.ContainsKey(_playerCountry)
                    ? Country.CountryNames[_playerCountry]
                    : _playerCountry;
            }
            set => SetProperty(ref _playerCountry, value);
        }

        private string _state = WorkingState.Ready.ToString();
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        #endregion

        #region Commands

        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand ExitCommand { get; }

        #endregion

        public SaveEditorVM()
        {
            OpenFileCommand = new RelayCommand(c => OpenFile());
            SaveFileCommand = new RelayCommand(c => SaveFile());
            ExitCommand = new RelayCommand(c => Exit());
        }

        public async void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ConfigurationManager.AppSettings.Get(DialogFilter);            
            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName))
                return;

            FilePath = openFileDialog.FileName;

            _saveParser.SaveFile = await ReadFile(FilePath).ConfigureAwait(false);
            
            await FindProvinces().ConfigureAwait(false);

            ListOfCountries = await GetCountries().ConfigureAwait(false);

            PlayerCountry = _saveParser.PlayerCountry;

            State = WorkingState.Done.ToString();
        }

        private Task<string[]> ReadFile(string path)
        {
            State = WorkingState.ReadingFile.ToString();
            return Task.Run(() => File.ReadAllLines(path, Encoding.GetEncoding(1252)));
        }

        private Task FindProvinces()
        {
            State = WorkingState.FindingCountries.ToString();
            return Task.Run(() => _saveParser.FindCountriesAndProvinces());
        }

        private Task<List<string>> GetCountries()
        {
            State = WorkingState.GettingCountries.ToString();
            return Task.Run(() => _saveParser.GetCountries());
        }

        public void SaveFile()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                MessageBox.Show("File not opened.", "Error.");
                return;
            }

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ConfigurationManager.AppSettings.Get(DialogFilter);
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

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private void GetProvincesOfCountry()
        {
            ListOfProvinces = _saveParser.GetProvincesOfContry(CurrentCountry);
        }

        private void SetCurrentProvinces()
        {
            var names = CurrentProvinceNames.Split(',');
            CurrentProvinces = _saveParser.GetProvinces(names);
            if (CurrentProvinces.Count >= 1)
                CurrentProvince = CurrentProvinces[0];
        }

        private void SetPoints()
        {
            if (string.IsNullOrEmpty(FilePath) || CurrentProvinces.Count < 1)
                return;

            foreach (var currentProvince in CurrentProvinces)
            {
                _saveParser.SaveFile[currentProvince.Points.AdmId] = $"    base_tax={CurrentProvince.Points.Adm}";
                _saveParser.SaveFile[currentProvince.Points.DipId] = $"    base_production={CurrentProvince.Points.Dip}";
                _saveParser.SaveFile[currentProvince.Points.MilId] = $"    base_manpower={CurrentProvince.Points.Mil}";
            }
        }
    }
}