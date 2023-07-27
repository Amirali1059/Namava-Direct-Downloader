using System;
using System.Collections.Generic;
using System.Linq;
using System.Json;
using System.Text.RegularExpressions;

namespace Namava_Direct_Downloader
{
    class NamavaVideo
    {
        public readonly List<JsonValue> Audios = new List<JsonValue>();
        public readonly List<JsonValue> Subtitles = new List<JsonValue>();
        
        public string DownloadUrl, VideoName, VideoFullName, VideoNameFile, Season, Episode, VideoBaseName;
        public byte[] key = new byte[32];
        public byte[] iv = new byte[16];
        public int VideoQuality = 0;
        public bool HasMoreAudios = false;
        public bool HasMoreSubtitles = false;
        private bool EnglishNums = false;
        // Define a regular expression for season and episode.
        Regex rx = new Regex(@"S\w*\s*(\d{1,2})\s*E\w*\s*(\d{1,2})", RegexOptions.Compiled);
        MainForm parent;

        public NamavaVideo(bool _EnglishNums, MainForm parent)
        {
            this.parent = parent;
            EnglishNums = _EnglishNums;
        }
        private (string, string, string) ParseVideoName(string VideoName)
        {
            string VideoName_ = Utils.ChangeNumbersToEnglish(VideoName.Replace("فصل", "S").Replace("قسمت", "E"));
            GroupCollection groupCollection = rx.Match(VideoName_).Groups;
            string[] group_names = rx.GetGroupNames();
            Season = groupCollection[group_names[1]].Value;
            Episode = groupCollection[group_names[2]].Value;
            if (VideoName_.Contains("-"))
            {
                VideoBaseName = VideoName_.Split('-')[0];
            }
            else if (VideoName_.Contains(":"))
            {
                VideoBaseName = VideoName_.Split(':')[0];
            }
            else
            {
                VideoBaseName = VideoName;
            }
            
            VideoBaseName = VideoBaseName.TrimEnd(' ');
            
            return (VideoBaseName, Season, Episode);
        }
        public bool LoadFrom(JsonValue DownloadData)
        {
            if (DownloadData["audios"].ToString().ToLower().Contains("per"))
            {
                foreach (JsonValue Audio in DownloadData["audios"])
                {
                    if (!((string)Audio["language"]).ToLower().StartsWith("per"))
                    {
                        Audios.Add(Audio);
                    }
                }
            }
            else if (DownloadData["audios"].ToString().ToLower().Contains("eng"))
            {
                foreach (JsonValue Audio in DownloadData["audios"])
                {
                    if (!((string)Audio["language"]).ToLower().StartsWith("eng"))
                    {
                        Audios.Add(Audio);
                    }
                }
            }
            else
            {
                foreach (JsonValue Audio in DownloadData["audios"])
                {
                    Audios.Add(Audio);
                }
                if (Audios.ToArray().Length == 1)
                {
                    Audios.Clear();
                }
            }
            foreach (JsonValue Subtitle in DownloadData["subtitles"])
            {
                Subtitles.Add(Subtitle);
            }
            DownloadUrl = (string)DownloadData["avBaseUrl"] + (string)DownloadData["videoUrl"] + "?x=" + (string)DownloadData["avQueryParamX"];
            VideoName = Uri.UnescapeDataString((string)DownloadData["title"]);
            (VideoBaseName, Season, Episode) = ParseVideoName(VideoName);
            VideoQuality = (int)DownloadData["height"];
            key = Convert.FromBase64String((string)DownloadData["encryption"][0]);
            iv = Convert.FromBase64String((string)DownloadData["encryption"][1]);
            VideoNameFile = Utils.CorrectFile(VideoName);
            VideoFullName = (string)VideoNameFile.Clone();
            if (EnglishNums)
            {
                VideoFullName = Utils.ChangeNumbersToEnglish(VideoFullName);
            }
            if (Audios.ToArray().Length != 0)
            {
                HasMoreAudios = true;
            }
            if (Subtitles.ToArray().Length != 0)
            {
                HasMoreSubtitles = true;
            }
            return true;
        }
    }
}
