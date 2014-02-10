using System;

namespace ProjectWizard
{
    [Serializable]
    public class WizardData
    {
        public WizData_Type Type { get; set; }
        public WizData_Submodules[] SubmodulesAr { get; set; }
        public WizData_AuthorBlock Author { get; set; } 
    }

    [Serializable]
    public class WizData_Type
    {
		public ProjectType ProjectTemplate { get; set; }
		public string projectTemplateString { get; set; }
		public string projectName { get; set; }
        public string MainLocation { get; set; }
        public string OriginLocation { get; set; }
    }

    [Serializable]
    public class WizData_Submodules
    {
        public string Name { get; set; }
		public string Repo_Name { get; set; }
        public string Location { get; set; }
		public bool AddToSolution { get; set; }
        public string[] IncludeStrAr { get; set; }
		public string[] AddlIncludeDirs { get; set; }
		public string[] AddlLibDirs { get; set; }
    }

    [Serializable]
    public class WizData_AuthorBlock
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

    [Serializable]
    public class ProjectType_Data
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
		public ProjectType ProjType { get; set; }
    }

    [Serializable]
    public class Submodules_Data
    {
        public string Name { get; set; }
		public string Repo_Name { get; set; }
		public string Description { get; set; }
		public string OriginLocation { get; set; }
		public bool AddToSolution { get; set; }
        public string[] IncludeStrAr { get; set; }
		public string[] AddlIncludeDirs { get; set; }
		public string[] AddlLibDirs { get; set; }
        public string Stash { get; set; }
        public string Jira { get; set; }
        public string Confluence { get; set; }

		public bool required = false;
    }
}
