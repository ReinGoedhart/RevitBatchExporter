using RevitBatchExporter.Frontend.Models;
using System;
using System.Collections.Generic;
using static RevitBatchExporter.Frontend.Enums.Enums;

namespace RevitBatchExporter.Frontend.Stores
{
    public class DeleteObjectsStore
    {
        public event Action OnDeleteProject;
        public event Action OnDeleteConfiguration;
        public event Action OnDeleteLog;
        public void DeleteObjects(Classes classType)
        {
            switch (classType)
            {
                case Classes.Project:
                    OnDeleteProject?.Invoke();
                    break;
                case Classes.Configuration:
                    OnDeleteConfiguration?.Invoke();
                    break;
                case Classes.Log:
                    OnDeleteLog?.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}
