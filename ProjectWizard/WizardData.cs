using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWizard
{
    class WizardData
    {
        public WizData_Type Type { get; set; }
        public WizData_Submodules Submodules { get; set; }
        public WizData_AuthorBlock Author { get; set; } 
    }

    class WizData_Type
    {
        public string ProjectTemplate { get; set; }
        public string MainLocation { get; set; }
        public string OriginLocation { get; set; }
    }

    class WizData_Submodules
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string IncludeStr { get; set; }
    }

    class WizData_AuthorBlock
    {
        public string ProjectName { get; set; }
        public string RequirementNum { get; set; }
        public string Customer { get; set; }
        public string Office { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string EmpId { get; set; }
    }
}
