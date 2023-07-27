using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Json;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Namava_Direct_Downloader
{
    public class Utils
    {
        public static string[] argv = Environment.GetCommandLineArgs();
        public static string appDir = Path.GetDirectoryName(argv[0]);
        public static string ChangeNumbersToEnglish(string input)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
            string[] arabic = new string[10] { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };
            for (int j = 0; j < 10; j++)
            {
                input = input.Replace(persian[j], j.ToString());
                input = input.Replace(arabic[j], j.ToString());
            }
            return input;
        }

        public static string CorrectPath(string VideoName)
        {
            string VideoNameFile_ = VideoName;
            char newchar = '-';
            foreach (char oldchar in "@#$%^&*?\"<>|")
            {
                VideoNameFile_ = VideoNameFile_.Replace(oldchar, newchar);
            }
            return VideoNameFile_;
        }
        public static string CorrectFile(string VideoName)
        {
            string VideoNameFile_ = VideoName;
            char newchar = '-';
            foreach (char oldchar in "\\/:@#$%^&*?\"<>|")
            {
                VideoNameFile_ = VideoNameFile_.Replace(oldchar, newchar);
            }
            return VideoNameFile_;
        }

        public static string AppInformation()
        {
            string info = string.Empty;
            info += $"App Version:  {Application.ProductVersion}\n";
            info += $"Operation System:  {Environment.OSVersion}\n";
            info += $"Argv:  [\n    {String.Join(",\n    ", Utils.argv)}\n]\n";
            return info;
        }

        public static byte[] DecryptAES256(byte[] cipherData, byte[] Key, byte[] IV)
        {
            using (var ms = new MemoryStream())
            {
                Rijndael alg = Rijndael.Create();
                alg.Padding = PaddingMode.Zeros;
                alg.Key = Key;
                alg.IV = IV;
                Stream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                cs.Close();
                byte[] decryptedData = ms.ToArray();
                return decryptedData;
            }
        }

        public static byte[] EncryptAES256(byte[] cipherData, byte[] Key, byte[] IV)
        {
            using (var ms = new MemoryStream())
            {
                Rijndael alg = Rijndael.Create();
                alg.Padding = PaddingMode.Zeros;
                alg.Key = Key;
                alg.IV = IV;
                Stream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                cs.Close();
                byte[] encryptedData = ms.ToArray();
                return encryptedData;
            }
        }

        public static bool Execute(string parameters, string exec, bool createNoWindow = true, IWin32Window parent=null)
        {
            string Exectable = Path.GetDirectoryName(argv[0])+"\\"+exec;
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = createNoWindow,
                FileName = Exectable,
                Arguments = parameters,
                //Verb = "runas"
            };
            // Clipboard.SetText($"\"{ffmpeg_exe}\" {parameters}");
            if (startInfo.CreateNoWindow)
            {
                startInfo.UseShellExecute = false;
                startInfo.StandardErrorEncoding = Encoding.UTF8;
                startInfo.RedirectStandardError = true;
            }
            else
            {
                startInfo.UseShellExecute = true;
            }
            string error = "";
            int ExitCode = 0;
            try
            {
                using (Process process = Process.Start(startInfo))
                {
                    if (startInfo.CreateNoWindow)
                    {
                        do
                        {
                            Application.DoEvents();
                        } while (!process.HasExited);
                        error = process.StandardError.ReadToEnd();
                        ExitCode = process.ExitCode;
                    }
                    process.WaitForExit();
                }
            }
            catch (FileNotFoundException)
            {
                if (parent != null)
                {
                    MsgBox.Show(parent, $"Sorry, there's an error while executing \"{exec}\"!\nFile not be found! reinstalling the application may fix the problem.", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                if (parent != null)
                {
                    MsgBox.Show(parent, $"Sorry, there's an error while executing \"{exec}\"!\n{ex.Message}", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            if (ExitCode == 0)
            {
                return true;
            }
            else
            {
                if (parent != null)
                {
                    if (startInfo.CreateNoWindow)
                    {
                        MsgBox.Show(parent, $"Sorry, there's an error while executing \"{exec}\"!\n{error}", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MsgBox.Show(parent, $"Sorry, there's an error while while executing \"{exec}\"!", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return false;
            }
        }

        public static byte[] Srequest(string url, string method, NameValueCollection data, IWin32Window Parent = null)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            byte[] response = null;
            using (var wb = new WebClient())
            {
                bool error = false;
                do
                {
                    try
                    {
                        response = wb.UploadValues(url, method, data);
                    }
                    catch (WebException e)
                    {
                        try
                        {
                            using (BinaryReader br = new BinaryReader(e.Response.GetResponseStream()))
                            {
                                response = br.ReadBytes((int)br.BaseStream.Length);
                            }
                        }
                        catch (Exception)
                        {
                            error = true;
                            if (Parent != null)
                            {
                                DialogResult Retry = MsgBox.Show(Parent, $"Sorry, there's an error while sending request to AAM Products server!\n{e.Message}!\n\nDo you want to try again?", "error!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                if (Retry != DialogResult.Yes)
                                {
                                    Environment.Exit(1);
                                }
                            }
                        }
                    }
                } while (error && Parent != null);
            }
            return response;
        }
        public static JsonValue Request(string url, string method, NameValueCollection data, IWin32Window Parent)
        {
            byte[] resp = Srequest(url, method, data, Parent);
            if (!(resp == null))
            {
                try
                {
                    string responseInString = Encoding.UTF8.GetString(resp);
                    return JsonValue.Parse(responseInString);
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public static int InstallNsisApp(string filename)
        {
            string filename_ = Path.GetFullPath(@filename);
            if (!File.Exists(filename_))
            {
                return 2;
            }
            try
            {
                var proc = Process.Start(@filename_, $"/D={Utils.appDir}");
                proc.WaitForExit();
                return proc.ExitCode;
            } catch (Win32Exception e)
            {
                // the exit code 1223 is returned when the installation canceled by the user.
                if (e.NativeErrorCode == 1223) 
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            finally
            {
                File.Delete(@filename_);
            }
        }
        public static JsonValue ParseDownloadData(IWin32Window parent)
        {
            byte[] data = null;
            string database64 = argv[1].Remove(0, 10);
            try
            {
                data = Convert.FromBase64String(database64);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == new FormatException().GetType() || ex.GetType() == new ArgumentNullException().GetType())
                {
                    data = Convert.FromBase64String(database64.Remove(database64.Length - 1));
                }
                else
                {
                    MsgBox.Show(parent, "Sorry, wrong or corrupted data comes from namava! please try again later.", "wrong data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
            string decodedString = Encoding.UTF8.GetString(data);
            return JsonValue.Parse(@decodedString);
        }
        public static string getUserProfile()
        {
            return Environment.GetEnvironmentVariable("USERPROFILE");
        }
    }
}
