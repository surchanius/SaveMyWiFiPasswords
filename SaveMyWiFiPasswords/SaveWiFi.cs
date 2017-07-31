using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SaveMyWiFiPasswords
{
    public class SaveWiFi
    {
        public string SavePath { get; set; }
        public SaveWiFi()
        {
            SavePath = Environment.CurrentDirectory;
        }
        public void SaveAllPasswords()
        {
            SavePath = Path.Combine(SavePath, "WiFiPasswords");
            string LogPass = DateTime.Now.ToString();
            string command = "netsh wlan show profiles";
            string logins = Cmd(command);
            string prefix = "Все профили пользователей     :";
            string endString= "\r\n";
            int indexer = logins.IndexOf(prefix);
            int endIndexer = logins.IndexOf(endString, indexer+1);
            while (indexer > 0)
            {
                int startLogin = indexer + prefix.Length+1;
                string login = logins.Substring(startLogin, endIndexer-startLogin);
                String password=Cmd("netsh wlan show profile name=\""+login+"\" key=clear");
                password = FindPassword(password);
                //Console.WriteLine(login +" "+ password);
                LogPass +=endString+ login + " " + password;
                indexer = logins.IndexOf(prefix, endIndexer + 1);
                endIndexer = logins.IndexOf(endString, indexer + 1);
            }
            File.WriteAllText(SavePath, LogPass);
        }
        private string Cmd(string command)
        {
            String result;
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                procStartInfo.RedirectStandardOutput = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                result = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                return result;
            }
            catch
            {
            }
            return null;
        }
        private string FindPassword(string password)
        {
            try
            {
                string prefix = "Содержимое ключа            : ";
                string endString = "\r\n";
                int indexer = password.IndexOf(prefix);
                int endIndexer = password.IndexOf(endString, indexer);
                int startLogin = indexer + prefix.Length ;
                password = password.Substring(startLogin, endIndexer - startLogin);
                return password;
            }
            catch (Exception)
            {
                //нет пароля
            }
           
            return "Нет пароля(((";
        }
    }
    
}


