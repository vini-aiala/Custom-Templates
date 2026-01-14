using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UseCaseItemWizard
{
    public class WizardImplementation : IWizard
    {
        // This method is called before opening any item that
        // has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        // This method is only called for item templates,
        // not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }


        // This method is only called for item templates,
        // not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem
            projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is called after the project is created.
        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            try
            {
                using (var form = new UseCaseNameForm())
                {
                    if (form.ShowDialog() != DialogResult.OK)
                        throw new WizardCancelledException();

                    replacementsDictionary.Add("$usecasename$",
                        form.BaseName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    public class UseCaseNameForm : Form
    {
        private TextBox txtBaseName;
        private Button btnOk;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblHint;

        public string BaseName => txtBaseName.Text.Trim();

        public UseCaseNameForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form
            Text = "Create Use Case";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(360, 170);

            // Title
            lblTitle = new Label
            {
                Text = "Use case base name",
                Font = new Font(Font, FontStyle.Bold),
                Location = new Point(12, 12),
                AutoSize = true
            };

            // Hint
            lblHint = new Label
            {
                Text = "This name will be used to generate the use case classes.\n" +
                       "Example: CreateOrder, UpdateUser",
                Location = new Point(12, 40),
                Size = new Size(330, 34)
            };

            // TextBox
            txtBaseName = new TextBox
            {
                Location = new Point(12, 82),
                Width = 330
            };

            // OK button
            btnOk = new Button
            {
                Text = "Create",
                Location = new Point(186, 120),
                DialogResult = DialogResult.OK
            };
            btnOk.Click += OnOkClicked;

            // Cancel button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(267, 120),
                DialogResult = DialogResult.Cancel
            };

            AcceptButton = btnOk;
            CancelButton = btnCancel;

            Controls.AddRange(new Control[]
            {
            lblTitle,
            lblHint,
            txtBaseName,
            btnOk,
            btnCancel
            });
        }

        private void OnOkClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BaseName))
            {
                MessageBox.Show(
                    "Please enter a valid base name for the use case.",
                    "Invalid name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                DialogResult = DialogResult.None; // Keep dialog open
            }
        }
    }
}