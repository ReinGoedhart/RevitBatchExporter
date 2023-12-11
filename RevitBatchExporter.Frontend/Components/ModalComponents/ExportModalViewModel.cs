using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Properties;
using RevitBatchExporter.Frontend.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    public class ExportModalViewModel : ViewModelBase
    {
        public Process RevitProcess { get; set; }
        private bool _isModalOpen;
        public bool IsModalOpen
        {
            get { return _isModalOpen; }
            set
            {
                _isModalOpen = value;
                OnPropertyChanged(nameof(IsModalOpen));
            }
        }
        private int _projectCount = 0;
        public int ProjectCount
        {
            get { return _projectCount; }
            set
            {
                if (_projectCount != value)
                {
                    _projectCount = value;
                    OnPropertyChanged(nameof(ProjectCount));
                }
            }
        }
        private int _operationCount = 0;
        public int OperationCount
        {
            get { return _operationCount; }
            set
            {
                if (_operationCount != value)
                {
                    _operationCount = value;
                    OnPropertyChanged(nameof(OperationCount));
                }
            }
        }
        private int _maxProjectCount;
        public int MaxProjectCount
        {
            get
            {
                return _maxProjectCount;
            }
            set
            {
                _maxProjectCount = value;
                OnPropertyChanged(nameof(MaxProjectCount));
            }
        }
        private string _currentOperation;
        public string CurrentOperation
        {
            get
            {
                return _currentOperation;
            }
            set
            {
                _currentOperation = value;
                OnPropertyChanged(nameof(CurrentOperation));
            }
        }
        private string _currentProject;
        public string CurrentProject
        {
            get
            {
                return _currentProject;
            }
            set
            {
                _currentProject = value;
                OnPropertyChanged(nameof(CurrentProject));
            }
        }
        private string _buttonText;
        public string ButtonText
        {
            get
            {
                return _buttonText;
            }
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }
        private int _errorsOccured;
        public int ErrorsOccured
        {
            get
            {
                return _errorsOccured;
            }
            set
            {
                _errorsOccured = value;
                OnPropertyChanged(nameof(ErrorsOccured));
            }
        }

        string folderPath = @"C:\RevitBatchExporter";
        string fileName = "RevitBatchExportToken.txt";
        public ICommand CloseModalCommand { get; }

        private Configuration _configuration;
        public Configuration Configuration
        {
            get
            {
                return _configuration;
            }
            set
            {
                _configuration = value;
                OnPropertyChanged(nameof(Configuration));
            }
        }
        public ExportModalViewModel(INavigationService abortNavigationService)
        {
            IsModalOpen = false;
            CloseModalCommand = new RelayCommand(() => abortNavigationService.Navigate());
        }
        private string _error;
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }
        //public LogFile logFile { get; set; }
        public void CheckConfigurationProjects()
        {
            //ConfigurationService configurationService = new ConfigurationService();
            //List<Project> projects = configurationService.GetProjects(Configuration.Id).ToList();
            string time = DateTime.Now.ToString("yyyy_MM_dd_HHmm_ss");
            //logFile = new LogFile()
            //{
            //    ConfigurationName = Configuration.ConfigurationName,
            //    LogFilePath = Settings.Default.LogDirectory + "\\" + Configuration.ConfigurationName + "-" + time + ".txt",
            //    ErrorsOccured = 0,
            //    projectIds = string.Empty
            //};

            //ValidateProjects validator = new ValidateProjects();

            //ResetViewModelValues(projects.Count() + 1);
            //bool checkedConfigurations = validator.ValidateProjectIFCConfiguration(projects);
            //bool checkedOutputName = validator.ValidateProjectOutputName(projects);
            //if (checkedConfigurations == false || checkedOutputName == false)
            //{
            //    if (checkedConfigurations == false)
            //    {
            //        Error += "There is an issue with the configuration, make sure every project has an configurationPath\r\n";
            //    }
            //    if (checkedOutputName == false)
            //    {
            //        Error += "There is an issue with the outputnames, make sure every project has an outputname and there are no duplicates in the configuration";
            //    }
            //    ErrorsOccured++;
            //    ProjectCount = projects.Count + 1;
            //    OperationCount = 3;
            //    ButtonText = "Cancel";
            //    return;
            //}

            //StartExporting(projects);
        }
        private async Task StartExporting(List<Project> projects)
        {
            //CreateStartHandshakeForRevit();
            //
            //RevitHandler revitHandler = new RevitHandler();
            //RevitProcess = await revitHandler.LaunchRevit(Configuration, projects);
            //
            //FrontendServer frontendServer = new FrontendServer(projects, revitHandler, this);
            //frontendServer.Start();
        }
        public void ResetViewModelValues(int projectCount)
        {
            Error = string.Empty;
            ButtonText = "Abort Export";
            MaxProjectCount = 0;
            ProjectCount = 0;
            OperationCount = 0;
            CurrentOperation = string.Empty;
            CurrentProject = string.Empty;
            MaxProjectCount = projectCount;
        }
        public void CreateStartHandshakeForRevit()
        {
            string fullPath = Path.Combine(folderPath, fileName);
            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // Create and write to the text file
                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    writer.WriteLine("Your text goes here");
                }
                Console.WriteLine($"File created successfully at {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public void addOperationCount()
        {
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    OperationCount += 1;
            //});
        }
        public void addProjectCount()
        {
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    ProjectCount += 1;
            //});
        }
        public void ResetOperationCount()
        {
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    OperationCount = 0;
            //});
        }
        public void ChangeCurrenPoject(string msg)
        {
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    CurrentProject = msg;
            //});
        }
        public void ChangeCurrenOperation(string msg)
        {
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    CurrentOperation = msg;
            //});
        }
        public void OpenModal()
        {
            IsModalOpen = true;
        }
        private void CloseModal()
        {
            IsModalOpen = false;
            if (RevitProcess != null)
            {
                RevitProcess.Kill();

                string folderPath = @"C:\RevitBatchExporter";
                string fileName = "RevitBatchExportToken.txt";
                string fullPath = Path.Combine(folderPath, fileName);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }
    }
}
