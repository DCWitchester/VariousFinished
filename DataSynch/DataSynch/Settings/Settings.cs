using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSynch.Settings
{
    class Settings
    {
        #region Settings Static Properties
        //the settings path is static so no external access
        private static readonly String settingsPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\config.ini";
        /// <summary>
        /// the main instance of the program settings
        /// </summary>
        public static ProgramSettings programSettings { get; set; } = new ProgramSettings();
        /// <summary>
        /// the file settings used for retrieving settings from the file
        /// </summary>
        private static List<FileSetting> fileSettings { get; set; } = new List<FileSetting>();
        /// <summary>
        /// this settings will be set to true only if the settings have been initialized
        /// </summary>
        public static Boolean initializedSettings { get; set; } = new Boolean();
        #endregion
        #region Settings Classes
        public enum SettingTypes
        {
            none,
            clientGuid,
            workStationGuid,
            lastUpdateTimestamp,
            uploadInterval,
            downloadInterval
        }
        /// <summary>
        /// the program properties used for static readonly acces
        /// </summary>
        class ProgramProperties
        {
            /// <summary>
            /// the main password used for encrypting or decrypting the program
            /// </summary>
            private static readonly String EncrytionPassword = "D'`r@?o[m|XWEUU/S3s>N);'&I7j(iDf1{z@xPO*)Lrq7XWsl2jihmle+LKa'_^$EaZ_^]V[Tx;:VUTSLKoIHMFEiIH*)ED=aA@?87[H";
            /// <summary>
            /// the getter for the current property
            /// </summary>
            public static String getEncryptionPassword => EncrytionPassword;
        }
        /// <summary>
        /// the program settings will be contiand within an instance of this class declared in the app.Starter path
        /// </summary>
        public class ProgramSettings
        {
            /// <summary>
            /// the client GUID used for unique identification
            /// </summary>
            private String clientGUID           = String.Empty;
            /// <summary>
            /// the workstation GUID used for unique identification
            /// </summary>
            private String workStationGUID      = String.Empty;
            /// <summary>
            /// the last update timer used for file identification
            /// </summary>
            private DateTime lastUpdateTime       = new DateTime();
            /// <summary>
            /// the download timer used for function calling
            /// </summary>
            private Int32 uploadInterval        = new Int32();
            /// <summary>
            /// the download timer used for function calling
            /// </summary>
            private Int32 downloadInterval      = new Int32();

            /// <summary>
            /// the caller and setter for the clientGUID property
            /// </summary>
            public String ClientGuid
            {
                get => clientGUID;
                set => clientGUID = value;
            }
            /// <summary>
            /// the caller and setter for the workStationGUID property
            /// </summary>
            public String WorkStationGuid
            {
                get => workStationGUID;
                set => workStationGUID = value;
            }
            /// <summary>
            /// the caller and setter for the LastUpdateTime property
            /// </summary>
            public DateTime LastUpdateTime
            {
                get => lastUpdateTime;
                set => lastUpdateTime = value;
            }
            /// <summary>
            /// the caller and setter for the uploadInterval
            /// </summary>
            public Int32 UploadInterval
            {
                get => uploadInterval;
                set => uploadInterval = value;
            }
            /// <summary>
            /// the caller and setter for the downloadInterval
            /// </summary>
            public Int32 DownloadInterval
            {
                get => downloadInterval;
                set => downloadInterval = value;
            }
        }
        /// <summary>
        /// the class for containing a settings item
        /// </summary>
        private class FileSetting
        {
            public SettingTypes SettingType { get; set; } = SettingTypes.none;
            public String SettingName { get; set; } = String.Empty;
            public String SettingValue { get; set; } = String.Empty;
            public 
            FileSetting(SettingTypes settingType,String settingsNme, String settingsValue)
            {
                SettingType = settingType;
                SettingName = settingsNme;
                SettingValue = settingsValue;
            }

        }
        #endregion
        #region Auxilliary
        /// <summary>
        /// this function will turn a string with local format into a valid datetime
        /// </summary>
        /// <param name="s">the string</param>
        /// <returns>the valid DateTime</returns>
        static DateTime StringToDateTime(String s)
        {
            String[] timeType = s.Trim().Split(' ');
            String[] dMy = timeType[0].Split('.');
            String[] Hms = timeType[1].Split(':');
            return new DateTime(Int32.Parse(dMy[0]), Int32.Parse(dMy[1]), Int32.Parse(dMy[2]), Int32.Parse(Hms[0]), Int32.Parse(Hms[1]), Int32.Parse(Hms[2]));
        }
        /// <summary>
        /// this function will read the setting from the setting file
        /// </summary>
        static void ParseSettingFile()
        {
            foreach (String line in File.ReadAllLines(settingsPath))
            {
                String[] settingsLine = (Miscellaneous.Encrypter.Decrypt(line,ProgramProperties.getEncryptionPassword)).Split('=');
                switch (settingsLine[0].Trim().ToUpper())
                {
                    case "CLIENT GUID":
                        {
                            fileSettings.Add(new FileSetting(SettingTypes.clientGuid, "Client Guid", settingsLine[1]));
                            break;
                        }
                    case "WORKSTATION GUID":
                        {
                            fileSettings.Add(new FileSetting(SettingTypes.workStationGuid, "WorkStation GUID", settingsLine[1]));
                            break;
                        }
                    case "LAST UPDATE TIMESTAMP":
                        {
                            fileSettings.Add(new FileSetting(SettingTypes.lastUpdateTimestamp, "Last Update Timestamp", settingsLine[1]));
                            break;
                        }
                    case "UPLOAD INTERVAL":
                        {
                            fileSettings.Add(new FileSetting(SettingTypes.uploadInterval, "Upload Interval", settingsLine[1]));
                            break;
                        }
                    case "DOWNLOAD INTERVAL":
                        {
                            fileSettings.Add(new FileSetting(SettingTypes.downloadInterval, "Download Interval", settingsLine[1]));
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// this function will dump the setting from the List to the Settings object.
        /// </summary>
        static void SetSettingsFromList()
        {
            foreach(var element in fileSettings) 
            {
                switch (element.SettingType)
                {
                    case SettingTypes.clientGuid:
                        {
                            programSettings.ClientGuid = element.SettingValue;
                            break;
                        }
                    case SettingTypes.workStationGuid:
                        {
                            programSettings.WorkStationGuid = element.SettingValue;
                            break;
                        }
                    case SettingTypes.lastUpdateTimestamp:
                        {
                            programSettings.LastUpdateTime = StringToDateTime(element.SettingValue);
                            break;
                        }
                    case SettingTypes.uploadInterval:
                        {
                            programSettings.UploadInterval = Int32.Parse(element.SettingValue);
                            break;
                        }
                    case SettingTypes.downloadInterval:
                        {
                            programSettings.DownloadInterval = Int32.Parse(element.SettingValue);
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// this function will create the settings file and mark it as hidden
        /// </summary>
        static void CreateSettingsFile()
        {
            //the file is created on the existing path
            File.Create(settingsPath);
            //after the file creation we set the file protection by making it hidden.
            File.SetAttributes(settingsPath, File.GetAttributes(settingsPath) | FileAttributes.Hidden);
        }
        /// <summary>
        /// this function will save the settings from the program into 
        /// </summary>
        static void SaveSettingsToFile()
        {
            if (!File.Exists(settingsPath)) CreateSettingsFile();
            FileInfo localFile = new FileInfo(settingsPath);
            //we remove the hidden attribute from the file while writing into it because it might cause errors <= Happened once (Not sure why)
            localFile.Attributes &= ~FileAttributes.Hidden;
            StreamWriter streamWriter = new StreamWriter(settingsPath);
            streamWriter.WriteLine("Client Guid = "+programSettings.ClientGuid);
            streamWriter.WriteLine("WorkStation GUID = " + programSettings.WorkStationGuid);
            streamWriter.WriteLine("Last Update Timestamp = " + programSettings.LastUpdateTime);
            streamWriter.WriteLine("Upload Interval = " + programSettings.UploadInterval);
            streamWriter.WriteLine("Download Interval = " + programSettings.DownloadInterval);
            streamWriter.Close();
            streamWriter.Dispose();
            //then we hid our file once anew
            localFile.Attributes |= FileAttributes.Hidden;
        }
        /// <summary>
        /// this function will be the main path for the setting file 
        /// </summary>
        #endregion
        public static void RetrieveSettingsFromFile()
        {
            if (!File.Exists(settingsPath))
            {
                Message.Message.MissingSettingsError();
                if (Message.MessageSettings.messageFormReturn)
                {
                    SettingsForm settingsForm = new SettingsForm();
                    settingsForm.ShowDialog();
                    if (initializedSettings)
                    {
                        SaveSettingsToFile();
                        Message.Message.SettingsHaveBeenChanged();
                        String systemPath = System.Reflection.Assembly.GetEntryAssembly().Location + '/';
                        using (StreamWriter sw = File.CreateText(systemPath + "restart.bat"))
                        {
                            sw.WriteLine("timeout /t 5");
                            sw.WriteLine(systemPath + "DataSynch.exe");
                            sw.WriteLine("DEL %0");
                            sw.Close();
                        }
                        Process p = new Process();
                        p.StartInfo.FileName = systemPath + "restart.bat";
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        Miscellaneous.Miscellaneous.ProgramClose();
                    }
                    else
                    {
                        Message.Message.ProgramWillNowClose();
                        Miscellaneous.Miscellaneous.ProgramClose();
                    }
                }
                else
                {
                    Message.Message.ProgramWillNowClose();
                    Miscellaneous.Miscellaneous.ProgramClose();
                }
            }
            else
            {
                ParseSettingFile();
                SetSettingsFromList();
                initializedSettings = true;
            }
        }
        
    }
    
}
