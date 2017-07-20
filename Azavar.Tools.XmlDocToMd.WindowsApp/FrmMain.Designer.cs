namespace Azavar.Tools.XmlDocToMd.WindowsApp
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.fbdOutputPath = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdInputFile = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectInputFile = new System.Windows.Forms.Button();
            this.lblInputFile = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.clbTypes = new System.Windows.Forms.CheckedListBox();
            this.lblTypesToGenerate = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnGneegrate = new System.Windows.Forms.Button();
            this.lblGitHubRepositoryRootURL = new System.Windows.Forms.Label();
            this.txtGitHubRepositoryRootURL = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ofdInputFile
            // 
            this.ofdInputFile.Filter = "XML Documentation Files | *.xml";
            // 
            // btnSelectInputFile
            // 
            this.btnSelectInputFile.Location = new System.Drawing.Point(13, 13);
            this.btnSelectInputFile.Name = "btnSelectInputFile";
            this.btnSelectInputFile.Size = new System.Drawing.Size(200, 42);
            this.btnSelectInputFile.TabIndex = 0;
            this.btnSelectInputFile.Text = "Select Input XML file";
            this.btnSelectInputFile.UseVisualStyleBackColor = true;
            this.btnSelectInputFile.Click += new System.EventHandler(this.btnSelectInputFile_Click);
            // 
            // lblInputFile
            // 
            this.lblInputFile.Location = new System.Drawing.Point(265, 13);
            this.lblInputFile.Name = "lblInputFile";
            this.lblInputFile.Size = new System.Drawing.Size(510, 42);
            this.lblInputFile.TabIndex = 1;
            this.lblInputFile.Text = "No input selected.";
            this.lblInputFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectOutputFolder
            // 
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(12, 548);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(200, 42);
            this.btnSelectOutputFolder.TabIndex = 2;
            this.btnSelectOutputFolder.Text = "Select output folder";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.Location = new System.Drawing.Point(218, 548);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(557, 42);
            this.lblOutputFolder.TabIndex = 3;
            this.lblOutputFolder.Text = "No input selected.";
            this.lblOutputFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clbTypes
            // 
            this.clbTypes.FormattingEnabled = true;
            this.clbTypes.Location = new System.Drawing.Point(12, 106);
            this.clbTypes.Name = "clbTypes";
            this.clbTypes.Size = new System.Drawing.Size(763, 436);
            this.clbTypes.TabIndex = 4;
            // 
            // lblTypesToGenerate
            // 
            this.lblTypesToGenerate.Location = new System.Drawing.Point(12, 58);
            this.lblTypesToGenerate.Name = "lblTypesToGenerate";
            this.lblTypesToGenerate.Size = new System.Drawing.Size(558, 42);
            this.lblTypesToGenerate.TabIndex = 5;
            this.lblTypesToGenerate.Text = "Types to generate";
            this.lblTypesToGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(576, 58);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(200, 42);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnGneegrate
            // 
            this.btnGneegrate.Location = new System.Drawing.Point(576, 650);
            this.btnGneegrate.Name = "btnGneegrate";
            this.btnGneegrate.Size = new System.Drawing.Size(200, 42);
            this.btnGneegrate.TabIndex = 7;
            this.btnGneegrate.Text = "Generate";
            this.btnGneegrate.UseVisualStyleBackColor = true;
            this.btnGneegrate.Click += new System.EventHandler(this.btnGneegrate_Click);
            // 
            // lblGitHubRepositoryRootURL
            // 
            this.lblGitHubRepositoryRootURL.Location = new System.Drawing.Point(13, 593);
            this.lblGitHubRepositoryRootURL.Name = "lblGitHubRepositoryRootURL";
            this.lblGitHubRepositoryRootURL.Size = new System.Drawing.Size(200, 42);
            this.lblGitHubRepositoryRootURL.TabIndex = 8;
            this.lblGitHubRepositoryRootURL.Text = "GitHub Repo Root";
            this.lblGitHubRepositoryRootURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGitHubRepositoryRootURL
            // 
            this.txtGitHubRepositoryRootURL.Location = new System.Drawing.Point(224, 599);
            this.txtGitHubRepositoryRootURL.Name = "txtGitHubRepositoryRootURL";
            this.txtGitHubRepositoryRootURL.Size = new System.Drawing.Size(551, 29);
            this.txtGitHubRepositoryRootURL.TabIndex = 9;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(219, 15);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(40, 40);
            this.lblInfo.TabIndex = 10;
            this.lblInfo.Text = "iii";
            this.lblInfo.Click += new System.EventHandler(this.lblInfo_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 700);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtGitHubRepositoryRootURL);
            this.Controls.Add(this.lblGitHubRepositoryRootURL);
            this.Controls.Add(this.btnGneegrate);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.lblTypesToGenerate);
            this.Controls.Add(this.clbTypes);
            this.Controls.Add(this.lblOutputFolder);
            this.Controls.Add(this.btnSelectOutputFolder);
            this.Controls.Add(this.lblInputFile);
            this.Controls.Add(this.btnSelectInputFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Documentation Markdown Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbdOutputPath;
        private System.Windows.Forms.OpenFileDialog ofdInputFile;
        private System.Windows.Forms.Button btnSelectInputFile;
        private System.Windows.Forms.Label lblInputFile;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.CheckedListBox clbTypes;
        private System.Windows.Forms.Label lblTypesToGenerate;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnGneegrate;
        private System.Windows.Forms.Label lblGitHubRepositoryRootURL;
        private System.Windows.Forms.TextBox txtGitHubRepositoryRootURL;
        private System.Windows.Forms.Label lblInfo;
    }
}

