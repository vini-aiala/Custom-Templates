using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UseCase.TemplateWizard
{
    public class FolderItemWizard : IWizard
    {
        private DTE _dte;
        private string _name;

        public void RunStarted(
            object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams)
        {
            _dte = (DTE)automationObject;
            _name = replacementsDictionary["$safeitemname$"];
            replacementsDictionary["$name$"] = _name;
        }

        public void ProjectFinishedGenerating(Project project)
        {
            var parentItems = project.ProjectItems;
            var folder = parentItems.AddFolder(_name);

            var files = parentItems
                .Cast<ProjectItem>()
                .Where(i => i.Name.EndsWith(".cs"))
                .ToList();

            foreach (var file in files)
            {
                var src = file.FileNames[1];
                var dest = Path.Combine(
                    Path.GetDirectoryName(src),
                    _name,
                    file.Name);

                file.Remove();
                File.Move(src, dest);
                folder.ProjectItems.AddFromFile(dest);
            }
        }

        public bool ShouldAddProjectItem(string filePath) => true;
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }
        public void BeforeOpeningFile(ProjectItem projectItem) { }
        public void RunFinished() { }
    }
}