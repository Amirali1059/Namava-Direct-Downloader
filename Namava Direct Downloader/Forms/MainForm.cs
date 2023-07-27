using Microsoft.Win32;
//using Namava_Direct_Downloader.API;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Json;
using System.Net;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace Namava_Direct_Downloader
{
    public partial class MainForm : Form
    {
        JsonValue DownloadData;
        MyValues MyRegValues;
        //readonly AAM_Products_API MyAPI;
        string SavePath, VideoBaseFileName;
        bool FailedMessageShown, FFmpegExecuteNeeded, IsDownloading, btnDownloadClicked, SuccessfullDownload;
        short DownloadFailed, LastWorkerId;
        NamavaVideo MyVideo;
        public ResourceManager rm = Properties.Resources_main_en.ResourceManager;
        public string Language = "en";
        MessageBoxOptions msgboxOptions = new MessageBoxOptions();
        public MySettings mySettings;

        public MainForm()
        {
            InitializeComponent();
            //MyAPI = new AAM_Products_API("NDD", this);
        }

        public void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string CrashReport = "App Information:\n\n" + Utils.AppInformation() + "\n\nError:\n\n" + e.ExceptionObject.ToString();
            //bool crash_reported = MyAPI.CrashReport(CrashReport); // report the crach automaticaly
            bool crash_reported = false;
            if (crash_reported)
            {
                string ErrorMessage = rm.GetString("msgErrorReported");
                MsgBox.Show(ErrorMessage, rm.GetString("UnhandledError"), MessageBoxButtons.OK, MessageBoxIcon.Error, msgboxOptions);
            }
            else
            {
                string logfile = ".\\crash-report.txt";
                using (StreamWriter logWriter = new StreamWriter(File.Create(@logfile)))
                {
                    logWriter.Write($">>>> {rm.GetString("msgPleaseReport")} <<<<\n\n{CrashReport}");
                    Process.Start(new ProcessStartInfo(logfile) { WindowStyle = ProcessWindowStyle.Maximized });
                }
            }

            Environment.Exit(1);
        }
        private string GetUrl(string file)
        {
            return (string)DownloadData["avBaseUrl"] + file + "?x=" + DownloadData["avQueryParamX"];
        }
        private bool ExecuteFFmpeg(string parameters)
        {
            return Utils.Execute(parameters, "ffmpeg.exe", parent: this);
        }
        private void RunWorkers()
        {
            switch (LastWorkerId)
            {
                case 0:
                    if (cboxDownVideo.Checked)
                    {
                        bwDownVideo.RunWorkerAsync();
                    }
                    else if (cboxDownAudios.Checked)
                    {
                        bwDownAudios.RunWorkerAsync();
                    }
                    else if (cboxDownSubtitles.Checked)
                    {
                        bwDownSubtitles.RunWorkerAsync();
                    }
                    break;

                case 1:
                    if (cboxDownAudios.Checked)
                    {
                        bwDownAudios.RunWorkerAsync();
                    }
                    else if (cboxDownSubtitles.Checked)
                    {
                        bwDownSubtitles.RunWorkerAsync();
                    }
                    break;

                case 2:
                    if (cboxDownSubtitles.Checked)
                    {
                        bwDownSubtitles.RunWorkerAsync();
                    }
                    break;
            }
        }
        private void btnDownloadVideo_Click(object sender, EventArgs e)
        {

            //MyAPI.Log($"{VideoFullName}");
            btnDownloadClicked = true;
            if (Directory.Exists(SavePath))
            {
                Directory.CreateDirectory($"{SavePath}\\{MyVideo.VideoFullName}");
                lblLog.Text = rm.GetString("StartingDownload");
                DownloadEnabled(false);
                if (cboxDownVideo.Checked || cboxDownAudios.Checked || cboxDownSubtitles.Checked)
                {
                    IsDownloading = true;
                    RunWorkers();

                }
                else
                {
                    MsgBox.Show(this, rm.GetString("msgNothingSelected"), rm.GetString("NothingSelected"), MessageBoxButtons.OK, MessageBoxIcon.Warning, msgboxOptions);
                    DownloadEnabled(true);
                }
            }
            else if (!SavePath.Contains(":"))
            {
                MsgBox.Show(this, rm.GetString("msgWrongFolder"), rm.GetString("WrongFolder"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult CreateFolder = MsgBox.Show(this, rm.GetString("msgFolderNotFound"), rm.GetString("FolderNotFound"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, msgboxOptions);
                if (CreateFolder == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(@SavePath);
                    }
                    catch (NotSupportedException)
                    {
                        MsgBox.Show(this, rm.GetString("msgCantCreateFolder"), rm.GetString("CantCreateFolder"), MessageBoxButtons.OK, MessageBoxIcon.Error, msgboxOptions);
                    }
                    btnDownloadVideo_Click(sender, e);
                    return;
                }
                else
                {
                    MsgBox.Show(this, rm.GetString("msgWrongFolder"), rm.GetString("WrongFolder"), MessageBoxButtons.OK, MessageBoxIcon.Error, msgboxOptions);
                }
            }
        }
        private long Download(string url, string filename, string label, BackgroundWorker worker, bool isEncrypted = false, byte[] key = null, byte[] iv = null, bool support_range = true)
        {
            worker.ReportProgress(0, $"{rm.GetString("Downloading")} {label}...");
            long SP;
            if (File.Exists(filename))
            {
                SP = new FileInfo(filename).Length;
            }
            else
            {
                SP = 0;
            }
            if (label.Contains("Audio"))
            {
                support_range = false;
                SP = 0;
            }
            WebResponse response;
            long TotalBytesToReceive;
            bool error;
            string Status;
            try
            {
                HttpWebRequest request = null;
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;
                if (support_range)
                {
                    request.AddRange(SP);
                }
                else if (SP > 0)
                {
                    File.Delete(filename);
                }
                response = request.GetResponse();
                TotalBytesToReceive = response.ContentLength + SP;
            }
            catch (WebException e)
            {
                if (e.Message.Contains("(416)"))
                {
                    return SP;
                }
                else if (e.Message.Contains("(404)"))
                {
                    worker.ReportProgress(-3, String.Format(rm.GetString("msgFileNotFound"), label));
                    return 0;
                }
                else
                {
                    worker.ReportProgress(-2, rm.GetString("msgConnectionError"));
                    return 0;
                }
            }
            BinaryReader DownloadReader = new BinaryReader(response.GetResponseStream());
            DownloadReader.BaseStream.ReadTimeout = 5000;
            BinaryWriter DownloadWriter = new BinaryWriter(File.Open(@filename, FileMode.Append));
            int DownloadChunkSize = 1024 * 32;
            long BytesReceived = SP;
            long counter = 0;
            int x = 1024 * 1024;
            byte[] Encrypted = new byte[x];
            do
            {
                while (cboxPause.Checked)
                {
                    Thread.Sleep(50);
                }
                error = false;
                byte[] data = null;
                for (short i = 0; i < 5; i++)
                {
                    error = false;
                    try
                    {
                        data = DownloadReader.ReadBytes(DownloadChunkSize);
                    }
                    catch (IOException)
                    {
                        error = true;
                    }
                    catch (WebException)
                    {
                        error = true;
                    }
                    if (!error)
                    {
                        break;
                    }
                }
                if (error)
                {
                    DownloadReader.Close();
                    DownloadWriter.Close();
                    DownloadWriter.Dispose();
                    DownloadWriter = null;
                    DownloadReader = null;
                    worker.ReportProgress(-2, rm.GetString("msgConnectionError"));
                    return 0;
                }
                else
                {
                    if (BytesReceived <= x && isEncrypted)
                    {

                        if (BytesReceived == x)
                        {
                            DownloadWriter.Write(Utils.DecryptAES256(Encrypted, key, iv));
                            DownloadWriter.Write(data);
                            Encrypted = null;
                        }
                        else
                        {
                            Buffer.BlockCopy(data, 0, Encrypted, (int)BytesReceived, data.Length);
                        }
                    }
                    else
                    {
                        DownloadWriter.Write(data);
                    }
                    if ((counter % 64) == 0)
                    {
                        DownloadWriter.Flush();
                    }
                    BytesReceived += DownloadChunkSize;
                    double percentage = ((double)BytesReceived / (double)TotalBytesToReceive) * 100;
                    Status = string.Format("{0}: ({1:0.00}Mb of {2:0.00}Mb) {3:0.00}%",
                        $"{rm.GetString("Downloading")} {label}",
                        (double)BytesReceived / (double)(1024 * 1024),
                        (double)TotalBytesToReceive / (double)(1024 * 1024),
                        percentage
                        );
                    worker.ReportProgress(Convert.ToInt32((double)percentage * (double)10), Status);
                }
                counter++;
            }
            while (BytesReceived < TotalBytesToReceive);
            DownloadReader.Close();
            DownloadWriter.Flush();
            DownloadWriter.Dispose();
            DownloadWriter.Close();
            DownloadWriter = null;
            DownloadReader = null;
            return TotalBytesToReceive;
        }
        private void startDwonloadVideo(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            LastWorkerId = 1;
            VideoBaseFileName = $"Video-{MyVideo.VideoQuality}p.mp4";
            worker.WorkerReportsProgress = true;
            string OutVideoFilename = $"{SavePath}\\{MyVideo.VideoFullName}\\{VideoBaseFileName}";
            if (Download(MyVideo.DownloadUrl, OutVideoFilename, "Video", worker, true, MyVideo.key, MyVideo.iv) != 0)
            {
                long DownloadingLench = new FileInfo(OutVideoFilename).Length;
                if (DownloadingLench < 1)
                {
                    worker.ReportProgress(-1, rm.GetString("msgDownloadError"));
                    DownloadFailed++;
                    return;
                }
            }
            else
            {
                DownloadFailed++;
            }
        }
        public void DownloadEnabled(bool Enabled)
        {
            btnDownloadVideo.Enabled = Enabled;
            MenuStrip.Enabled = Enabled;
            cboxDownVideo.Enabled = Enabled;
            if (MyVideo.HasMoreAudios && Enabled)
            {
                cboxDownAudios.Enabled = Enabled;
            }
            else
            {
                cboxDownAudios.Enabled = false;
            }
            if (MyVideo.HasMoreSubtitles && Enabled)
            {
                cboxDownSubtitles.Enabled = Enabled;
            }
            else
            {
                cboxDownSubtitles.Enabled = false;
            }
            if (Enabled)
            {
                lblLog.Text = rm.GetString("ReadyForDownload");
                IsDownloading = false;
                LastWorkerId = 0;
            }
            pbarDownload.Value = 0;
        }
        private void DwonloadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int ProgressPercentage = e.ProgressPercentage;
            if (ProgressPercentage == -1)
            {
                MsgBox.Show(this, (string)e.UserState, rm.GetString("DownloadError"), MessageBoxButtons.OK, MessageBoxIcon.Error, msgboxOptions);
                DownloadEnabled(true);
            }
            else if (ProgressPercentage == -2)
            {
                MsgBox.Show(this, (string)e.UserState, rm.GetString("ConnectinError"), MessageBoxButtons.OK, MessageBoxIcon.Error, msgboxOptions);
                DownloadEnabled(true);
            }
            else if (ProgressPercentage == -3)
            {
                MsgBox.Show(this, (string)e.UserState, rm.GetString("FileNotFound"), MessageBoxButtons.OK, MessageBoxIcon.Error, msgboxOptions);
                DownloadEnabled(true);
            }
            else if (ProgressPercentage == -4)
            {
                MsgBox.Show(this, (string)e.UserState, rm.GetString("Done"), MessageBoxButtons.OK, MessageBoxIcon.Information, msgboxOptions);
                DownloadEnabled(true);
            }
            else if (ProgressPercentage == -5)
            {
                MsgBox.Show(this, (string)e.UserState, rm.GetString("UpdateError"), MessageBoxButtons.OK, MessageBoxIcon.Error, msgboxOptions);
                DownloadEnabled(true);
            }
            else
            {
                pbarDownload.Value = Math.Min(1000, e.ProgressPercentage);
                lblLog.Text = (string)e.UserState;
            }
        }
        private void DownloadCompeleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (DownloadFailed == 0)
            {
                if (LastWorkerId == 1 && cboxDownAudios.Checked)
                {
                    bwDownAudios.RunWorkerAsync();
                }
                else if ((LastWorkerId == 2 || LastWorkerId == 1) && cboxDownSubtitles.Checked)
                {
                    bwDownSubtitles.RunWorkerAsync();
                }
                else
                {
                    lblLog.Text = rm.GetString("DownloadCompleted");
                    SuccessfullDownload = true;
                    IsDownloading = false;
                    if (FFmpegExecuteNeeded && File.Exists($"{SavePath}\\{MyVideo.VideoFullName}\\Video-{MyVideo.VideoQuality}p.mp4") && cboxDownVideo.Checked && !mySettings.boolSettings["AllwaysMix"])
                    {
                        DialogResult Close = MsgBox.Show(this, $"\n{String.Format(rm.GetString("msgMixNeeded"), SavePath)}", rm.GetString("DownloadCompleted"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, msgboxOptions); ;
                        if (Close == DialogResult.Yes)
                        {
                            Application.Exit();
                        }
                    }
                    else
                    {
                        FFmpegExecuteNeeded = false;
                        MsgBox.Show(this, String.Format(rm.GetString("msgDownloadCompleted"), SavePath), rm.GetString("DownloadCompleted"), MessageBoxButtons.OK, MessageBoxIcon.Information, msgboxOptions);
                        Application.Exit();
                    }
                }
            }
            else if (!FailedMessageShown)
            {
                DownloadEnabled(true);
                FailedMessageShown = true;
            }
            else if (!IsDownloading)
            {
                DownloadEnabled(true);
            }
        }
        private void smbtnChangeSavePath_Click(object sender, EventArgs e)
        {
            if (fbrSavePath.ShowDialog(this) == DialogResult.OK)
            {
                SavePath = fbrSavePath.SelectedPath;
                SavePath = SavePath.Replace('/', '\\');
                if (SavePath.EndsWith("\\"))
                {
                    SavePath = SavePath.Remove(SavePath.Length - 1);
                }
                mySettings.strSettings["SavePath"] = SavePath;
                string base_dir = $"{SavePath}\\{MyVideo.VideoFullName}";
                int audio_count = MyVideo.Audios.ToArray().Length;
                int subtitle_count = MyVideo.Subtitles.ToArray().Length;
                string[] audio_languages = new string[audio_count];
                string[] subtitle_languages = new string[subtitle_count];
                for (int i = 0; i < audio_count; i++)
                {
                    audio_languages[i] = (string)MyVideo.Audios[i]["language"];
                }
                for (int i = 0; i < subtitle_count; i++)
                {
                    subtitle_languages[i] = (string)MyVideo.Subtitles[i]["label"];
                }
                int subtitle_exists = 0;
                foreach (string lang in subtitle_languages)
                {
                    if (File.Exists($"{base_dir}\\Subtitle-{lang}.srt"))
                    {
                        subtitle_exists++;
                    }
                }
                int audio_exists = 0;
                foreach (string lang in audio_languages)
                {
                    if (File.Exists($"{base_dir}\\Audio-{lang}.mp4"))
                    {
                        audio_exists++;
                    }
                }
                if (subtitle_exists == subtitle_count && MyVideo.HasMoreSubtitles)
                {
                    MyVideo.HasMoreSubtitles = false;
                    FFmpegExecuteNeeded = true;
                }
                if (audio_exists == audio_count && MyVideo.HasMoreSubtitles)
                {
                    MyVideo.HasMoreAudios = false;
                    FFmpegExecuteNeeded = true;
                }
            }
        }
        private void bwDownAudios_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            LastWorkerId = 2;
            bool download_faliled = false;
            foreach (JsonValue Audio in MyVideo.Audios)
            {
                string AudioLanguage = (string)Audio["language"];
                string AudioBaseFileName = $"Audio-{AudioLanguage}.mp4";
                string OutAudioFileame = $"{SavePath}\\{MyVideo.VideoFullName}\\{AudioBaseFileName}";
                if (Download(GetUrl(Audio["url"]).Replace("/ovod/", "/hvod/"), OutAudioFileame, $"{AudioLanguage} Audio", worker) != 0)
                {
                    long DownloadingLench = new FileInfo(OutAudioFileame).Length;
                    if (DownloadingLench < 1)
                    {
                        worker.ReportProgress(-1, rm.GetString("msgDownloadError"));
                        download_faliled = true;
                        return;
                    }
                }
                else
                {
                    download_faliled = true;
                }
            }
            if (download_faliled)
            {
                DownloadFailed++;
            }
            else
            {
                FFmpegExecuteNeeded = true;
            }
        }
        private void bwDownSubtitles_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            LastWorkerId = 3;
            bool df = false;
            foreach (JsonValue Subtitle in MyVideo.Subtitles)
            {
                string SubtitleLanguage = (string)Subtitle["label"];
                string SubtitleBaseFileName = $"Subtitle-{SubtitleLanguage}.srt";
                string OutSubtitleFileame = $"{SavePath}\\{MyVideo.VideoFullName}\\{SubtitleBaseFileName}";
                if (Download((Subtitle["url"] + "?x=" + DownloadData["avQueryParamX"]), OutSubtitleFileame, $"{SubtitleLanguage} Subtitle", worker) != 0)
                {
                    long DownloadingLench = new FileInfo(OutSubtitleFileame).Length;
                    if (DownloadingLench <= 0)
                    {
                        worker.ReportProgress(-1, rm.GetString("msgDownloadError"));
                        df = true;
                        return;
                    }
                }
                else
                {
                    df = true;
                }
            }
            if (df)
            {
                DownloadFailed++;
            }
            else
            {
                FFmpegExecuteNeeded = true;
            }
        }
        private void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mySettings.SaveSettings();
            if (smcboxRemoveNamavaIntro.Checked && cboxDownVideo.Checked)
            {
                FFmpegExecuteNeeded = true;
            }
            if (!IsDownloading)
            {
                string VideoFileName_ = Utils.CorrectFile(GetVideoFileName());
                string base_dir = Utils.CorrectPath($"{SavePath}\\{MyVideo.VideoFullName}");
                string outputvideo = Utils.CorrectPath($"{base_dir}\\{VideoFileName_}.mp4");
                string outputvideo_nf = Utils.CorrectPath($"{SavePath}\\{VideoFileName_}.mp4");
                string videofilepath = Utils.CorrectPath($"{base_dir}\\Video-{MyVideo.VideoQuality}p.mp4");
                if (FFmpegExecuteNeeded && DownloadFailed == 0 && File.Exists(videofilepath) && btnDownloadClicked)
                {
                    lblLog.Text = rm.GetString("MixingFiles");
                    Application.DoEvents();
                    int audio_count = MyVideo.Audios.ToArray().Length;
                    int subtitle_count = MyVideo.Subtitles.ToArray().Length;
                    string[] audio_languages = new string[audio_count];
                    string[] subtitle_languages = new string[subtitle_count];
                    for (int i = 0; i < audio_count; i++)
                    {
                        audio_languages[i] = (string)MyVideo.Audios[i]["language"];
                    }
                    for (int i = 0; i < subtitle_count; i++)
                    {
                        subtitle_languages[i] = (string)MyVideo.Subtitles[i]["label"];
                    }
                    string video = $"Video-{MyVideo.VideoQuality}p";
                    string MixingVideoFillPath = $"{base_dir}\\{video}.mp4";
                    string cmd = " -y -v error -stats -i ";
                    cmd += $"\"{MixingVideoFillPath}\"";
                    if (cboxDownAudios.Checked)
                    {
                        foreach (string lang in audio_languages)
                        {
                            cmd += $" -i \"{base_dir}\\Audio-{lang}.mp4\"";
                        }
                    }
                    if (cboxDownSubtitles.Checked)
                    {
                        foreach (string lang in subtitle_languages)
                        {
                            cmd += $" -i \"{base_dir}\\Subtitle-{lang}.srt\"";
                        }
                    }
                    int i_ = 0;
                    cmd += " -map 0";
                    i_++;
                    if (cboxDownAudios.Checked)
                    {
                        for (int i = 0; i < audio_count; i++)
                        {
                            cmd += " -map " + i_.ToString() + ":a";
                            i_++;
                        }
                    }
                    if (cboxDownSubtitles.Checked)
                    {
                        for (int i = 0; i < subtitle_count; i++)
                        {
                            cmd += " -map " + i_.ToString();
                            i_++;
                        }
                    }
                    i_ = 1;
                    if (cboxDownAudios.Checked)
                    {
                        for (int i = 0; i < audio_count; i++)
                        {
                            cmd += " -metadata:s:s:" + i_.ToString() + " language=" + audio_languages[i];
                            i_++;
                        }
                    }
                    if (cboxDownSubtitles.Checked)
                    {
                        for (int i = 0; i < subtitle_count; i++)
                        {
                            cmd += " -metadata:s:s:" + i_.ToString() + " language=" + subtitle_languages[i];
                            i_++;
                        }
                    }
                    cmd += (smcboxRemoveNamavaIntro.Checked ? " -ss 5 " : "") + $" -c copy -c:s mov_text \"{outputvideo}\"";
                    if (ExecuteFFmpeg(cmd))
                    {
                        Application.DoEvents();
                        if (smcboxDeleteSubtitles.Checked)
                        {
                            foreach (string lang in subtitle_languages)
                            {
                                string DFile = $"{base_dir}\\Subtitle-{lang}.srt";
                                DeleteFile(@DFile);
                            }
                        }
                        if (smcboxDeleteAudios.Checked)
                        {
                            foreach (string lang in audio_languages)
                            {
                                string DFile = $"{base_dir}\\Audio-{lang}.mp4";
                                DeleteFile(@DFile);
                            }
                        }
                        if (smcboxDeleteVideo.Checked)
                        {
                            DeleteFile(@MixingVideoFillPath);
                        }
                    }
                }
                else if (SuccessfullDownload && File.Exists(videofilepath))
                {
                    if (File.Exists(outputvideo))
                    {
                        File.Replace(videofilepath, outputvideo, null);
                    }
                    else
                    {
                        File.Move(videofilepath, outputvideo);
                    }
                }
                bool delete_folder = false;
                if (mySettings.boolSettings["DeleteVideoFolder"] && Directory.Exists(base_dir) && File.Exists(outputvideo))
                {
                    delete_folder = true;
                    foreach (string file in Directory.GetFiles(base_dir))
                    {
                        if (file != outputvideo)
                        {
                            delete_folder = false;
                            break;
                        }
                    }
                    if (delete_folder)
                    {
                        File.Move(outputvideo, outputvideo_nf);
                        Directory.Delete(base_dir, true);
                    }
                }
                if (DownloadFailed == 0 && btnDownloadClicked)
                {
                    try
                    {
                        if (delete_folder)
                        {
                            Process.Start($"{SavePath}");
                        }
                        else
                        {
                            Process.Start($"{SavePath}\\{MyVideo.VideoFullName}");
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                Environment.Exit(0);
            }
            else
            {
                DialogResult r = MsgBox.Show(this, rm.GetString("msgStillDownloading"), rm.GetString("StillDownloading"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, msgboxOptions);
                if (r == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                e.Cancel = (r == DialogResult.No);
            }
        }

        public string GetVideoFileName()
        {
            string VideoFileName_ = mySettings.strSettings["SaveFormat"];
            VideoFileName_ = VideoFileName_.Replace("$N", MyVideo.VideoBaseName);

            string episode_ = (MyVideo.Episode != "") ? Int32.Parse(MyVideo.Episode).ToString("D2") : "";
            VideoFileName_ = VideoFileName_.Replace("$E", episode_);

            string season_ = (MyVideo.Episode != "") ? Int32.Parse(MyVideo.Season).ToString("D2") : "";
            VideoFileName_ = VideoFileName_.Replace("$S", season_);

            VideoFileName_ = VideoFileName_.Replace("$Q", MyVideo.VideoQuality.ToString());
            return VideoFileName_;
        }

        private void bwInstallUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker worker = (BackgroundWorker)sender;
            //string tempfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ndd_installer.exe");
            //if (File.Exists(tempfile))
            //{
            //    File.Delete(tempfile);
            //}
            //long lenght = Download($"{AAM_Products_API.Server}/api/GetLastVersion/?product={MyAPI.product}", tempfile, rm.GetString("strUpdate"), (BackgroundWorker)sender, support_range: false);
            //if (lenght != 0)
            //{
            //    worker.ReportProgress(0, rm.GetString("strInstallingUpdate"));
            //    int error_level = Utils.InstallNsisApp(tempfile);
            //    if (error_level == 0)
            //    {
            //        worker.ReportProgress(-4, rm.GetString("msgSuccessfulUpdate"));
            //    }
            //    else if (error_level == 1)
            //    {
            //        worker.ReportProgress(-5, rm.GetString("msgCanceledUpdate"));
            //    }
            //    else if (error_level == 2)
            //    {
            //        worker.ReportProgress(-5, rm.GetString("msgFailedUpdate"));
            //    }
            //}
        }

        private void bwDownloadUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblLog.Text = rm.GetString("ReadyForDownload");
            DownloadEnabled(true);
        }

        private void bwCheckUpdates_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int UpdateAvalable = (int)e.ProgressPercentage;
            if (UpdateAvalable != 0)
            {
                (JsonValue UpdateInfo, string ReleaseNotes) = (ValueTuple<JsonValue, string>)e.UserState;
                if (smcboxAutoUpdate.Checked)
                {
                    DownloadEnabled(false);
                    bwDownloadUpdate.RunWorkerAsync();
                }
                else
                {
                    DialogResult result = MsgBox.Show(this, $"{ReleaseNotes}\n\n{rm.GetString("msgUpdateAvailable")}", rm.GetString("UpdateAvailable"), MessageBoxButtons.YesNo, MessageBoxIcon.Information, msgboxOptions);
                    if (result == DialogResult.Yes)
                    {
                        DownloadEnabled(false);
                        bwDownloadUpdate.RunWorkerAsync();
                    }
                }
            }
        }

        private void LoadSettings()
        {
            (string, ToolStripMenuItem)[] vals = {
                ("NumsWithEnglish", smcboxReplaceNumsWithEnglish),
                ("RemoveNamavaIntro",smcboxRemoveNamavaIntro),
                ("AutoUpdate",smcboxAutoUpdate),
                ("DeleteAudios",smcboxDeleteAudios),
                ("DeleteSubtitles",smcboxDeleteSubtitles),
                ("DeleteVideo",smcboxDeleteVideo),
                ("DeleteVideoFolder", smcboxDeleteVideoFolder),
                ("AllwaysMix", smcboxAllwaysMixFiles),
            };

            foreach ((string valname, ToolStripMenuItem item) in vals)
            {
                if (mySettings.boolSettings.ContainsKey(valname))
                {
                    item.Checked = mySettings.boolSettings[valname];
                }
                else
                {
                    throw new NotImplementedException($"'{valname}' is not implemented in MySettings class.");
                }
            }

            if (mySettings.strSettings.ContainsKey("SavePath"))
            {
                SavePath = mySettings.strSettings["SavePath"];
            }
            else
            {
                throw new NotImplementedException($"'SavePath' is not implemented in MySettings class.");
            }

            if (mySettings.strSettings.ContainsKey("Language"))
            {
                Language = mySettings.strSettings["Language"];
                if (Language == "fa")
                {
                    rm = Properties.Resources_main_fa.ResourceManager;
                    msgboxOptions = MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
                    LoadLanguage();
                }
            }
            else
            {
                throw new NotImplementedException($"'Language' is not implemented in MySettings class.");
            }

        }
        private void Authorize()
        {
            //string license = MyRegValues.GetValue("license").ToUpper();
            //string secret = MyRegValues.GetValue("secret");
            //bool ok = false;
            //if (!MyRegValues.Exists("License") || !MyAPI.Authorize(license, secret).Item1)
            //{
            //    do
            //    {
            //        license = MyAPI.InputLicense();
            //        if (license != null)
            //        {
            //            license = license.ToUpper();
            //            string msg;
            //            (ok, msg, secret) = MyAPI.Activate(license);
            //            if (ok)
            //            {
            //                MyRegValues.SetValue("license", license);
            //                MyRegValues.SetValue("secret", secret);
            //            }
            //            else
            //            {
            //                if (MsgBox.Show(this, String.Format(rm.GetString("msgActivationFailed"), msg), rm.GetString("ActivationFailed"), MessageBoxButtons.YesNo, MessageBoxIcon.Error, msgboxOptions) != DialogResult.Yes)
            //                {
            //                    Environment.Exit(1);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            Environment.Exit(1);
            //        }
            //    } while (!ok);
            //}
        }
        private void LoadLanguage()
        {
            (string, Control)[] vals = {
                ("desDownloadVideo",cboxDownVideo),
                ("desDownloadAudios",cboxDownAudios),
                ("desDownloadSubtitles",cboxDownSubtitles),
                ("desPause",cboxPause),
                ("desDownload",btnDownloadVideo),
                ("ReadyForDownload",lblLog),
            };

            foreach ((var text, var item) in vals)
            {
                item.Text = rm.GetString(text);
            }

            (string, ToolStripMenuItem)[] vals2 = {
                ("desFile", tsmFile),
                ("desOptions",tsmOptions),
                ("desLicense",tsmLicense),
                ("desHelp",tsmHelp),
                ("desChangeSavePath",smbtnChangeSavePath),
                ("desDeleteAfterMix",tsmiDeleteAfterMix),

                ("desChangeNumbers", smcboxReplaceNumsWithEnglish),
                ("desRemoveIntro",smcboxRemoveNamavaIntro),
                ("desAutoUpdate",smcboxAutoUpdate),
                ("desLanguage",tsmiLanguage),
                ("desDelAudios",smcboxDeleteAudios),
                ("desDelSubtitles",smcboxDeleteSubtitles),

                ("desDelVideo", smcboxDeleteVideo),
                ("desResetLicense",smbtnResetLicense),
                ("desLicenseInfo",smbtnGetLicenseInfo),
                ("desAdvanced",tsmiAdvanced),
                ("desChangeSaveFromat",smbtnChangeSaveFromat),
                ("desAllwaysMixFiles",smcboxAllwaysMixFiles),

                ("desDeleteVideoFolder",smcboxDeleteVideoFolder),

            };

            foreach ((var text, var item) in vals2)
            {
                item.Text = rm.GetString(text);
            }
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            lblLog.Text = rm.GetString("Loading");
            Application.DoEvents();
            try
            {
                string SubKey = "SOFTWARE\\Namava Direct Downloader";
                Registry.CurrentUser.CreateSubKey(@SubKey);
                RegistryKey NDDREG = Registry.CurrentUser.OpenSubKey(@SubKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                MyRegValues = new MyValues(NDDREG);
            }
            catch (UnauthorizedAccessException)
            {
                MsgBox.Show(this, rm.GetString("msgPermissionError"), rm.GetString("PermissionError"), MessageBoxButtons.OK, MessageBoxIcon.Error, msgboxOptions);
                Environment.Exit(1);
            }
            Authorize();
            bwCheckUpdates.RunWorkerAsync();
            mySettings = new MySettings(MyRegValues);
            mySettings.LoadSettings();
            LoadSettings();
            DownloadData = Utils.ParseDownloadData(this);
            MyVideo = new NamavaVideo(smcboxReplaceNumsWithEnglish.Checked, this);
            MyVideo.LoadFrom(DownloadData);
            DownloadEnabled(true);
            this.Text = $"NDD V{Application.ProductVersion} " + GetVideoFileName();
            Application.DoEvents();
        }

        private void CBoxChanged(object sender, EventArgs e)
        {
            DownloadFailed = 0;
            FailedMessageShown = false;
            SuccessfullDownload = false;
        }

        private void smbtnHelp_Click(object sender, EventArgs e)
        {
            //Process.Start("https://aamproducts.pythonanywhere.com/help");
        }

        private void smbtnGetLicenseInfo_Click(object sender, EventArgs e)
        {
            //string license = MyRegValues.GetValue("license").ToUpper();
            //string secret = MyRegValues.GetValue("secret");
            //(bool ok, JsonValue resp) = MyAPI.GetLicenseInfo(license, secret);
            //if (ok)
            //{
            //    LicenseInfoDialog mf = new LicenseInfoDialog()
            //    {
            //        created = (double)resp["created"],
            //        expire = (double)resp["expire"],
            //        license = license,
            //    };
            //    mf.ShowDialog();
            //}
            //else
            //{
            //    MsgBox.Show(this, rm.GetString("msgServerError"), rm.GetString("ServerError"), MessageBoxButtons.OK, MessageBoxIcon.Error, msgboxOptions);
            //}
        }

        private void smbtnResetLicense_Click(object sender, EventArgs e)
        {
            //DialogResult result = MsgBox.Show(this, rm.GetString("msgResetLicense"), rm.GetString("ResetLicense"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (result == DialogResult.Yes)
            //{
            //    string license = MyRegValues.GetValue("license").ToUpper();
            //    string secret = MyRegValues.GetValue("secret");
            //    (bool ok, string msg) = MyAPI.ResetLicense(license, secret);
            //    MsgBox.Show(this, msg, (ok ? "succeeded!" : "failed!"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (ok)
            //    {
            //        Application.Exit();
            //    }
            //}
        }

        private void smbtnEnglish_Click(object sender, EventArgs e)
        {
            rm = Properties.Resources_main_en.ResourceManager;
            Language = "en";
            msgboxOptions = new MessageBoxOptions();
            mySettings.strSettings["Language"] = Language;
            LoadLanguage();
        }

        private void smbtnPersian_Click(object sender, EventArgs e)
        {
            rm = Properties.Resources_main_fa.ResourceManager;
            Language = "fa";
            msgboxOptions = MessageBoxOptions.RightAlign;
            mySettings.strSettings["Language"] = Language;
            LoadLanguage();
        }

        private void smbtnChangeSaveFromat_Click(object sender, EventArgs e)
        {
            AdvancedDialog advancedDialog = new AdvancedDialog(this);
            advancedDialog.ShowDialog(this);
        }


        private void bwCheckUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker worker = (BackgroundWorker)sender;
            //bool Ok = false;
            //JsonValue UpdateInfo = null, ReleaseNotes = null;
            //ushort i = 0;
            //do
            //{
            //    (Ok, UpdateInfo, ReleaseNotes) = MyAPI.CheckUpdates(Language);
            //    i += 1;
            //} while (!Ok && i < 3);
            //if (Version.Parse(UpdateInfo["lastest-version"]) > Version.Parse(Application.ProductVersion))
            //{
            //    var data = new ValueTuple<JsonValue, string>(UpdateInfo, ReleaseNotes);
            //    worker.ReportProgress(1, data);
            //}
            //else
            //{
            //    worker.ReportProgress(0);
            //}
        }
        private void smcboxAllwaysMixFiles_Click(object sender, EventArgs e)
        {
            mySettings.boolSettings["AllwaysMix"] = smcboxAllwaysMixFiles.Checked;
        }

        private void smcboxDeleteVideoFolder_Click(object sender, EventArgs e)
        {
            mySettings.boolSettings["DeleteVideoFolder"] = smcboxDeleteVideoFolder.Checked;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsmGithub_Click(object sender, EventArgs e)
        {
            //Process.Start("https://aamproducts.pythonanywhere.com/help");
        }

        private void smcboxReplacePerNumsWithEng_Click(object sender, EventArgs e)
        {
            mySettings.boolSettings["NumsWithEnglish"] = smcboxReplaceNumsWithEnglish.Checked;
        }

        private void smcboxRemoveNamavaIntro_Click(object sender, EventArgs e)
        {
            mySettings.boolSettings["RemoveNamavaIntro"] = smcboxRemoveNamavaIntro.Checked;
        }

        private void smcboxDeleteAudios_Click(object sender, EventArgs e)
        {
            mySettings.boolSettings["DeleteAudios"] = smcboxDeleteAudios.Checked;
        }

        private void smcboxDeleteSubtitles_Click(object sender, EventArgs e)
        {
            mySettings.boolSettings["DeleteSubtitles"] = smcboxDeleteSubtitles.Checked;
        }

        private void smcboxDeleteVideo_Click(object sender, EventArgs e)
        {
            mySettings.boolSettings["DeleteVideo"] = smcboxDeleteVideo.Checked;
        }
        private void smcboxAutoUpdate_Click(object sender, EventArgs e)
        {
            mySettings.boolSettings["AutoUpdate"] = smcboxAutoUpdate.Checked;
        }
    }
}