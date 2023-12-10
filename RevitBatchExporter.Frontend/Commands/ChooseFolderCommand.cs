using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using RevitBatchExporter.Frontend.Components.ModalComponents;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitBatchExporter.Frontend.Commands
{
    class ChooseFolderCommand : CommandBase
    {
        private readonly ViewModelBase _vm;
        private int _type = 0;

        public ChooseFolderCommand(ViewModelBase vm, int type = 0)
        {
            _vm = vm;
            _type = type;
        }
        public override void Execute(object parameter)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();

            if (_vm is SettingsModalViewModel SettingViewModel)
            {
                dialog.InitialDirectory = "C:\\";
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    switch (_type)
                    {
                        case 1:
                            SettingViewModel.SaveDirectory = dialog.FileName;
                            break;
                        case 2:
                            SettingViewModel.LogDirectory = dialog.FileName;
                            break;
                        case 3:
                            SettingViewModel.CreateNewLocalDirectory = dialog.FileName;
                            break;
                    }
                }
            }
            if(_vm is EditProjectModalViewModel editProjectModalViewModel)
            {
                dialog.Filters.Add(new CommonFileDialogFilter("JSON Files", "*.json"));
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    editProjectModalViewModel.EditingProject.ConfigurationPath = dialog.FileName;
                    editProjectModalViewModel.UpdateJsonCommand();
                }
            }

        }
    }
}
