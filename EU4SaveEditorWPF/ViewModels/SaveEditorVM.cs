﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using System.Windows.Input;
using System.Windows;
using Aligres.SaveParser;
using EU4SaveEditorWPF.Commands;

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
            set 
            { 
                SetProperty(ref _listOfCountries, value); 
            }
        }

        public List<string> ListOfProvinces
        {
            get => _listOfProvinces;
            set
            {
                SetProperty(ref _listOfProvinces, value); 
            }
        }

        public string FilePath
        {
            get => _filePath;
            set 
            { 
                SetProperty(ref _filePath, value);
            }
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
            get => _currentProvince;
            set
            {
                SetProperty(ref _currentProvince, value);
            }
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
            set
            {
                SetProperty(ref _foundCountry, value);
            }
        }

        #endregion

        #region Commands

        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }

        #endregion

        public SaveEditorVM()
        {
            ListOfCountries = new List<string>();
            ListOfProvinces = new List<string>();

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

        public void SaveFile()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                MessageBox.Show("File not opened.", "Error.");
                return;
            }
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = ConfigurationManager.AppSettings.Get("FileDialogFilter")
            };

            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == string.Empty)
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
