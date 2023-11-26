using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Enums
{
    public class Enums
    {
        public enum RevitRelease
        {
            Revit2019 = 2019,
            Revit2020 = 2020,
            Revit2021 = 2021,
            Revit2022 = 2022,
            Revit2023 = 2023,
            Revit2024 = 2024
        }
        public enum RevitExportType
        {
            IFC = 1,
            SYNCONLY = 2,
        }
        public enum Region
        {
            EMEA = 1,
            US = 2,
            LOCAL = 3,
        }
    }
}
