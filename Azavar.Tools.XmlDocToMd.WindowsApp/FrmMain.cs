using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azavar.Tools.XmlDocToMd.Model;

namespace Azavar.Tools.XmlDocToMd.WindowsApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            lblInfo.Text = "\uD83D\uDEC8";
        }

        private XmlDocumentationModel _documentationModel = null;


        private void btnSelectInputFile_Click(object sender, EventArgs e)
        {
            if (ofdInputFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _documentationModel = new XmlDocumentationModel(ofdInputFile.FileName);
                    clbTypes.Items.Clear();
                    // ReSharper disable once CoVariantArrayConversion
                    clbTypes.Items.AddRange(_documentationModel.Members.Values.Where(m => (m as Model.Type) != null)
                        .Cast<Model.Type>()
                        .ToArray());
                    lblInputFile.Text = ofdInputFile.FileName.ShortFileName();
                    toolTip1.SetToolTip(lblInputFile, ofdInputFile.FileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(this, exp.Message);
                }
            }
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            if (fbdOutputPath.ShowDialog() == DialogResult.OK)
            {
                lblOutputFolder.Text = fbdOutputPath.SelectedPath.ShortFileName();
                toolTip1.SetToolTip(lblOutputFolder, fbdOutputPath.SelectedPath);
            }
        }

        private void btnGneegrate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ofdInputFile.FileName))
                {
                    MessageBox.Show(this, "Please select a documentaion file");
                    return;
                }
                if (_documentationModel == null)
                {
                    MessageBox.Show(this, "Please select a valid documentaion file");
                    return;
                }
                if (clbTypes.CheckedItems.Count == 0)
                {
                    MessageBox.Show(this, "Please select at least one type to generate documentation for");
                    return;
                }
                if (string.IsNullOrEmpty(fbdOutputPath.SelectedPath))
                {
                    MessageBox.Show(this, "Please select the output folder");
                    return;
                }
                if (string.IsNullOrEmpty(txtGitHubRepositoryRootURL.Text))
                {
                    MessageBox.Show(this, "Please enter the GitHub repository root URL");
                    return;
                }
                var markdownRenderer = new MarkdownRenderer(_documentationModel, fbdOutputPath.SelectedPath, txtGitHubRepositoryRootURL.Text);
                markdownRenderer.RenderSelectedTypes(clbTypes.CheckedItems.Cast<Model.Type>());
                MessageBox.Show(this, "Markdown files generated successfully!");
            }
            catch (Exception exp)
            {
                MessageBox.Show(this, exp.Message);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbTypes.Items.Count; i++)
            {
                clbTypes.SetItemChecked(i, true);
            }
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(this,
                    "The XML docuemntation file is generated from code with certain tags, you can use the C# compiler to generate and XML documentation file for your code, this can be done in Visual Studio by enabling \"XML documentation file\" option under \"Build\" tab of the project properties. Select \"OK\" to open a web page with more details",
                    "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments");
            }
        }
    }
}
