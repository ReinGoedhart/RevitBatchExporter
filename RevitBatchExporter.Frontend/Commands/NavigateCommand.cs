﻿using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Commands
{

    public class NavigateCommand : CommandBase 
    {
        private readonly INavigationService _navigationService;

        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;    
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
