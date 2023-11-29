﻿using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    public class DeleteModalViewModel : ViewModelBase
    {

        public ICommand DeleteProjects { get; }
        public ICommand Cancel { get; }
        public string DeleteTitle { get; set; }
        public DeleteModalViewModel(CompositeNavigationService cancel, string deleteTitle)
        {
            DeleteTitle = deleteTitle;
            DeleteProjects = new RelayCommand(() => { cancel.Navigate(); }); // create Logic
            Cancel = new RelayCommand(() => { cancel.Navigate(); });
        }
    }
}
