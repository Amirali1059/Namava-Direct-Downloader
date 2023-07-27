
namespace Namava_Direct_Downloader
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnDownloadVideo = new System.Windows.Forms.Button();
            this.pbarDownload = new System.Windows.Forms.ProgressBar();
            this.lblLog = new System.Windows.Forms.Label();
            this.bwDownVideo = new System.ComponentModel.BackgroundWorker();
            this.cboxDownAudios = new System.Windows.Forms.CheckBox();
            this.cboxDownSubtitles = new System.Windows.Forms.CheckBox();
            this.fbrSavePath = new System.Windows.Forms.FolderBrowserDialog();
            this.bwDownAudios = new System.ComponentModel.BackgroundWorker();
            this.cboxDownVideo = new System.Windows.Forms.CheckBox();
            this.bwDownSubtitles = new System.ComponentModel.BackgroundWorker();
            this.cboxPause = new System.Windows.Forms.CheckBox();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.smbtnChangeSavePath = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteAfterMix = new System.Windows.Forms.ToolStripMenuItem();
            this.smcboxDeleteAudios = new System.Windows.Forms.ToolStripMenuItem();
            this.smcboxDeleteSubtitles = new System.Windows.Forms.ToolStripMenuItem();
            this.smcboxDeleteVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdvanced = new System.Windows.Forms.ToolStripMenuItem();
            this.smbtnChangeSaveFromat = new System.Windows.Forms.ToolStripMenuItem();
            this.smcboxAllwaysMixFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.smcboxDeleteVideoFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.smcboxReplaceNumsWithEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.smcboxRemoveNamavaIntro = new System.Windows.Forms.ToolStripMenuItem();
            this.smcboxAutoUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.smbtnEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.smbtnPersian = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.smbtnGetLicenseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.smbtnResetLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.bwCheckUpdates = new System.ComponentModel.BackgroundWorker();
            this.bwDownloadUpdate = new System.ComponentModel.BackgroundWorker();
            this.tsmGithub = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDownloadVideo
            // 
            resources.ApplyResources(this.btnDownloadVideo, "btnDownloadVideo");
            this.btnDownloadVideo.Name = "btnDownloadVideo";
            this.btnDownloadVideo.UseVisualStyleBackColor = true;
            this.btnDownloadVideo.Click += new System.EventHandler(this.btnDownloadVideo_Click);
            // 
            // pbarDownload
            // 
            resources.ApplyResources(this.pbarDownload, "pbarDownload");
            this.pbarDownload.Maximum = 1000;
            this.pbarDownload.Name = "pbarDownload";
            // 
            // lblLog
            // 
            resources.ApplyResources(this.lblLog, "lblLog");
            this.lblLog.Name = "lblLog";
            // 
            // bwDownVideo
            // 
            this.bwDownVideo.WorkerReportsProgress = true;
            this.bwDownVideo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.startDwonloadVideo);
            this.bwDownVideo.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DwonloadProgressChanged);
            this.bwDownVideo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DownloadCompeleted);
            // 
            // cboxDownAudios
            // 
            resources.ApplyResources(this.cboxDownAudios, "cboxDownAudios");
            this.cboxDownAudios.Name = "cboxDownAudios";
            this.cboxDownAudios.UseVisualStyleBackColor = true;
            this.cboxDownAudios.CheckedChanged += new System.EventHandler(this.CBoxChanged);
            // 
            // cboxDownSubtitles
            // 
            resources.ApplyResources(this.cboxDownSubtitles, "cboxDownSubtitles");
            this.cboxDownSubtitles.Name = "cboxDownSubtitles";
            this.cboxDownSubtitles.UseVisualStyleBackColor = true;
            this.cboxDownSubtitles.CheckedChanged += new System.EventHandler(this.CBoxChanged);
            // 
            // fbrSavePath
            // 
            resources.ApplyResources(this.fbrSavePath, "fbrSavePath");
            // 
            // bwDownAudios
            // 
            this.bwDownAudios.WorkerReportsProgress = true;
            this.bwDownAudios.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDownAudios_DoWork);
            this.bwDownAudios.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DwonloadProgressChanged);
            this.bwDownAudios.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DownloadCompeleted);
            // 
            // cboxDownVideo
            // 
            resources.ApplyResources(this.cboxDownVideo, "cboxDownVideo");
            this.cboxDownVideo.BackColor = System.Drawing.Color.Transparent;
            this.cboxDownVideo.Checked = true;
            this.cboxDownVideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxDownVideo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboxDownVideo.Name = "cboxDownVideo";
            this.cboxDownVideo.UseVisualStyleBackColor = false;
            this.cboxDownVideo.CheckedChanged += new System.EventHandler(this.CBoxChanged);
            // 
            // bwDownSubtitles
            // 
            this.bwDownSubtitles.WorkerReportsProgress = true;
            this.bwDownSubtitles.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDownSubtitles_DoWork);
            this.bwDownSubtitles.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DwonloadProgressChanged);
            this.bwDownSubtitles.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DownloadCompeleted);
            // 
            // cboxPause
            // 
            resources.ApplyResources(this.cboxPause, "cboxPause");
            this.cboxPause.Name = "cboxPause";
            this.cboxPause.UseVisualStyleBackColor = true;
            // 
            // MenuStrip
            // 
            resources.ApplyResources(this.MenuStrip, "MenuStrip");
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmOptions,
            this.tsmLicense,
            this.tsmHelp,
            this.tsmGithub});
            this.MenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuStrip.Name = "MenuStrip";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smbtnChangeSavePath,
            this.tsmiDeleteAfterMix,
            this.tsmiAdvanced});
            this.tsmFile.Name = "tsmFile";
            resources.ApplyResources(this.tsmFile, "tsmFile");
            // 
            // smbtnChangeSavePath
            // 
            this.smbtnChangeSavePath.Name = "smbtnChangeSavePath";
            resources.ApplyResources(this.smbtnChangeSavePath, "smbtnChangeSavePath");
            this.smbtnChangeSavePath.Click += new System.EventHandler(this.smbtnChangeSavePath_Click);
            // 
            // tsmiDeleteAfterMix
            // 
            this.tsmiDeleteAfterMix.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smcboxDeleteAudios,
            this.smcboxDeleteSubtitles,
            this.smcboxDeleteVideo});
            this.tsmiDeleteAfterMix.Name = "tsmiDeleteAfterMix";
            resources.ApplyResources(this.tsmiDeleteAfterMix, "tsmiDeleteAfterMix");
            // 
            // smcboxDeleteAudios
            // 
            this.smcboxDeleteAudios.Checked = true;
            this.smcboxDeleteAudios.CheckOnClick = true;
            this.smcboxDeleteAudios.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smcboxDeleteAudios.Name = "smcboxDeleteAudios";
            resources.ApplyResources(this.smcboxDeleteAudios, "smcboxDeleteAudios");
            this.smcboxDeleteAudios.Click += new System.EventHandler(this.smcboxDeleteAudios_Click);
            // 
            // smcboxDeleteSubtitles
            // 
            this.smcboxDeleteSubtitles.Checked = true;
            this.smcboxDeleteSubtitles.CheckOnClick = true;
            this.smcboxDeleteSubtitles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smcboxDeleteSubtitles.Name = "smcboxDeleteSubtitles";
            resources.ApplyResources(this.smcboxDeleteSubtitles, "smcboxDeleteSubtitles");
            this.smcboxDeleteSubtitles.Click += new System.EventHandler(this.smcboxDeleteSubtitles_Click);
            // 
            // smcboxDeleteVideo
            // 
            this.smcboxDeleteVideo.Checked = true;
            this.smcboxDeleteVideo.CheckOnClick = true;
            this.smcboxDeleteVideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smcboxDeleteVideo.Name = "smcboxDeleteVideo";
            resources.ApplyResources(this.smcboxDeleteVideo, "smcboxDeleteVideo");
            this.smcboxDeleteVideo.Click += new System.EventHandler(this.smcboxDeleteVideo_Click);
            // 
            // tsmiAdvanced
            // 
            this.tsmiAdvanced.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smbtnChangeSaveFromat,
            this.smcboxAllwaysMixFiles,
            this.smcboxDeleteVideoFolder});
            this.tsmiAdvanced.Name = "tsmiAdvanced";
            resources.ApplyResources(this.tsmiAdvanced, "tsmiAdvanced");
            // 
            // smbtnChangeSaveFromat
            // 
            this.smbtnChangeSaveFromat.Name = "smbtnChangeSaveFromat";
            resources.ApplyResources(this.smbtnChangeSaveFromat, "smbtnChangeSaveFromat");
            this.smbtnChangeSaveFromat.Click += new System.EventHandler(this.smbtnChangeSaveFromat_Click);
            // 
            // smcboxAllwaysMixFiles
            // 
            this.smcboxAllwaysMixFiles.Checked = true;
            this.smcboxAllwaysMixFiles.CheckOnClick = true;
            this.smcboxAllwaysMixFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smcboxAllwaysMixFiles.Name = "smcboxAllwaysMixFiles";
            resources.ApplyResources(this.smcboxAllwaysMixFiles, "smcboxAllwaysMixFiles");
            this.smcboxAllwaysMixFiles.Click += new System.EventHandler(this.smcboxAllwaysMixFiles_Click);
            // 
            // smcboxDeleteVideoFolder
            // 
            this.smcboxDeleteVideoFolder.CheckOnClick = true;
            this.smcboxDeleteVideoFolder.Name = "smcboxDeleteVideoFolder";
            resources.ApplyResources(this.smcboxDeleteVideoFolder, "smcboxDeleteVideoFolder");
            this.smcboxDeleteVideoFolder.Click += new System.EventHandler(this.smcboxDeleteVideoFolder_Click);
            // 
            // tsmOptions
            // 
            this.tsmOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smcboxReplaceNumsWithEnglish,
            this.smcboxRemoveNamavaIntro,
            this.smcboxAutoUpdate,
            this.tsmiLanguage});
            this.tsmOptions.Name = "tsmOptions";
            resources.ApplyResources(this.tsmOptions, "tsmOptions");
            // 
            // smcboxReplaceNumsWithEnglish
            // 
            this.smcboxReplaceNumsWithEnglish.Checked = true;
            this.smcboxReplaceNumsWithEnglish.CheckOnClick = true;
            this.smcboxReplaceNumsWithEnglish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smcboxReplaceNumsWithEnglish.Name = "smcboxReplaceNumsWithEnglish";
            resources.ApplyResources(this.smcboxReplaceNumsWithEnglish, "smcboxReplaceNumsWithEnglish");
            this.smcboxReplaceNumsWithEnglish.Click += new System.EventHandler(this.smcboxReplacePerNumsWithEng_Click);
            // 
            // smcboxRemoveNamavaIntro
            // 
            this.smcboxRemoveNamavaIntro.Checked = true;
            this.smcboxRemoveNamavaIntro.CheckOnClick = true;
            this.smcboxRemoveNamavaIntro.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smcboxRemoveNamavaIntro.Name = "smcboxRemoveNamavaIntro";
            resources.ApplyResources(this.smcboxRemoveNamavaIntro, "smcboxRemoveNamavaIntro");
            this.smcboxRemoveNamavaIntro.Click += new System.EventHandler(this.smcboxRemoveNamavaIntro_Click);
            // 
            // smcboxAutoUpdate
            // 
            this.smcboxAutoUpdate.CheckOnClick = true;
            resources.ApplyResources(this.smcboxAutoUpdate, "smcboxAutoUpdate");
            this.smcboxAutoUpdate.Name = "smcboxAutoUpdate";
            this.smcboxAutoUpdate.Click += new System.EventHandler(this.smcboxAutoUpdate_Click);
            // 
            // tsmiLanguage
            // 
            this.tsmiLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smbtnEnglish,
            this.smbtnPersian});
            this.tsmiLanguage.Name = "tsmiLanguage";
            resources.ApplyResources(this.tsmiLanguage, "tsmiLanguage");
            // 
            // smbtnEnglish
            // 
            this.smbtnEnglish.Name = "smbtnEnglish";
            resources.ApplyResources(this.smbtnEnglish, "smbtnEnglish");
            this.smbtnEnglish.Click += new System.EventHandler(this.smbtnEnglish_Click);
            // 
            // smbtnPersian
            // 
            this.smbtnPersian.Name = "smbtnPersian";
            resources.ApplyResources(this.smbtnPersian, "smbtnPersian");
            this.smbtnPersian.Click += new System.EventHandler(this.smbtnPersian_Click);
            // 
            // tsmLicense
            // 
            this.tsmLicense.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smbtnGetLicenseInfo,
            this.smbtnResetLicense});
            resources.ApplyResources(this.tsmLicense, "tsmLicense");
            this.tsmLicense.Name = "tsmLicense";
            // 
            // smbtnGetLicenseInfo
            // 
            this.smbtnGetLicenseInfo.Name = "smbtnGetLicenseInfo";
            resources.ApplyResources(this.smbtnGetLicenseInfo, "smbtnGetLicenseInfo");
            this.smbtnGetLicenseInfo.Click += new System.EventHandler(this.smbtnGetLicenseInfo_Click);
            // 
            // smbtnResetLicense
            // 
            this.smbtnResetLicense.Name = "smbtnResetLicense";
            resources.ApplyResources(this.smbtnResetLicense, "smbtnResetLicense");
            this.smbtnResetLicense.Click += new System.EventHandler(this.smbtnResetLicense_Click);
            // 
            // tsmHelp
            // 
            resources.ApplyResources(this.tsmHelp, "tsmHelp");
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Click += new System.EventHandler(this.smbtnHelp_Click);
            // 
            // bwCheckUpdates
            // 
            this.bwCheckUpdates.WorkerReportsProgress = true;
            this.bwCheckUpdates.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCheckUpdates_DoWork);
            this.bwCheckUpdates.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwCheckUpdates_ProgressChanged);
            // 
            // bwDownloadUpdate
            // 
            this.bwDownloadUpdate.WorkerReportsProgress = true;
            this.bwDownloadUpdate.WorkerSupportsCancellation = true;
            this.bwDownloadUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwInstallUpdate_DoWork);
            this.bwDownloadUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DwonloadProgressChanged);
            this.bwDownloadUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDownloadUpdate_RunWorkerCompleted);
            // 
            // tsmGithub
            // 
            this.tsmGithub.Name = "tsmGithub";
            resources.ApplyResources(this.tsmGithub, "tsmGithub");
            this.tsmGithub.Click += new System.EventHandler(this.tsmGithub_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.cboxPause);
            this.Controls.Add(this.cboxDownVideo);
            this.Controls.Add(this.cboxDownSubtitles);
            this.Controls.Add(this.cboxDownAudios);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.pbarDownload);
            this.Controls.Add(this.btnDownloadVideo);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDownloadVideo;
        private System.Windows.Forms.ProgressBar pbarDownload;
        private System.Windows.Forms.Label lblLog;
        private System.ComponentModel.BackgroundWorker bwDownVideo;
        private System.Windows.Forms.CheckBox cboxDownAudios;
        private System.Windows.Forms.CheckBox cboxDownSubtitles;
        private System.Windows.Forms.FolderBrowserDialog fbrSavePath;
        private System.ComponentModel.BackgroundWorker bwDownAudios;
        private System.Windows.Forms.CheckBox cboxDownVideo;
        private System.ComponentModel.BackgroundWorker bwDownSubtitles;
        private System.Windows.Forms.CheckBox cboxPause;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem smbtnChangeSavePath;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteAfterMix;
        private System.Windows.Forms.ToolStripMenuItem smcboxDeleteAudios;
        private System.Windows.Forms.ToolStripMenuItem smcboxDeleteSubtitles;
        private System.Windows.Forms.ToolStripMenuItem smcboxDeleteVideo;
        private System.Windows.Forms.ToolStripMenuItem tsmOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem smcboxReplaceNumsWithEnglish;
        private System.Windows.Forms.ToolStripMenuItem smcboxRemoveNamavaIntro;
        private System.Windows.Forms.ToolStripMenuItem tsmLicense;
        private System.Windows.Forms.ToolStripMenuItem smbtnGetLicenseInfo;
        private System.Windows.Forms.ToolStripMenuItem smbtnResetLicense;
        private System.Windows.Forms.ToolStripMenuItem smcboxAutoUpdate;
        private System.ComponentModel.BackgroundWorker bwCheckUpdates;
        private System.ComponentModel.BackgroundWorker bwDownloadUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiLanguage;
        private System.Windows.Forms.ToolStripMenuItem smbtnEnglish;
        private System.Windows.Forms.ToolStripMenuItem smbtnPersian;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdvanced;
        private System.Windows.Forms.ToolStripMenuItem smbtnChangeSaveFromat;
        private System.Windows.Forms.ToolStripMenuItem smcboxAllwaysMixFiles;
        private System.Windows.Forms.ToolStripMenuItem smcboxDeleteVideoFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmGithub;
    }
}

