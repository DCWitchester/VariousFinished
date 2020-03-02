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
        #endregion
        /// <summary>
        /// this function will be the main path for the setting file 
        /// </summary>
        public static void RetrieveSettingsFromFile()
        {
            //first we check if the settings file exists
            if (!File.Exists(settingsPath))
            {
                //if it doesn't we display a warning 
                Message.Message.MissingSettingsError();
                if (Message.MessageSettings.messageFormReturn)
                {
                    //if they want to create a clean settings file we initialize and call the form
                    SettingsForm settingsForm = new SettingsForm();
                    settingsForm.ShowDialog();
                    //and then if the settings have been initialized
                    if (initializedSettings)
                    {
                        //we save them to the file
                        SaveSettingsToFile();
                        //we display a message that the settong have been changed
                        Message.Message.SettingsHaveBeenChanged();
                        //then we restart the program
                        String systemPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\";
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
                        //if the settings have not been initialized we close the program
                        Message.Message.ProgramWillNowClose();
                        Miscellaneous.Miscellaneous.ProgramClose();
                    }
                }
                else
                {
                    //same if they opt not to generate a settings folder
                    Message.Message.ProgramWillNowClose();
                    Miscellaneous.Miscellaneous.ProgramClose();
                }
            }
            else
            {
                //if the settings folder was found we will retrieve the settings from the file.
                ParseSettingFile();
                SetSettingsFromList();
                initializedSettings = true;
            }
        }
        
    }
    class ServerSettings
    {
        #region ConnectionSettings
        /// <summary>
        /// the main connection string for retrieving data from the database
        /// </summary>
        private static readonly String connectionString = "Host = 5.2.228.239; Port = 26662; Database = DataSynchController; User Id = postgres; Password = pgsql";
        /// <summary>
        /// this is the main getter
        /// </summary>
        public static String getConnectionString => connectionString;
        #endregion
        #region
        /// <summary>
        /// the clientSettings based upon the existing structure
        /// </summary>
        public static ClientSettings.ClientSettingsStructure clientSettings { get; set; } = new ClientSettings.ClientSettingsStructure();
        public static ClientSettings.DataSychStructure dataSynch { get; set; } = new ClientSettings.DataSychStructure();
        #endregion
        public class ClientSettings
        {
            #region Settings Static Properties

            #endregion
            /// <summary>
            /// the main structure for the ClientSettings
            /// </summary>
            public class ClientSettingsStructure
            {
                /// <summary>
                /// the client ID from the Server propery
                /// </summary>
                private Int32 clientID = new Int32();
                /// <summary>
                /// the Main GUID linked to the client property
                /// </summary>
                private String clientGUID = String.Empty;
                /// <summary>
                /// the client firm fiscalCode property
                /// </summary>
                private String fiscalCode = String.Empty;
                /// <summary>
                /// the client firm name property
                /// </summary>
                private String clientName = String.Empty;
                /// <summary>
                /// the client message property <= used to display message for our clients
                /// </summary>
                private String clientMessage = String.Empty;
                /// <summary>
                /// whether the client has seen the last message or not
                /// </summary>
                private Boolean displayMessage = new Boolean();
                /// <summary>
                /// if the dataSynch is active or not
                /// </summary>
                private Boolean synchBlocked = new Boolean();
                /// <summary>
                /// the main getter and setter for the client id property
                /// </summary>
                public Int32 ClientID
                {
                    get => clientID;
                    set => clientID = value;
                }
                /// <summary>
                /// the main getter and setter for the client guid property
                /// </summary>
                public String ClientGUID
                {
                    get => clientGUID;
                    set => clientGUID = value;
                }
                /// <summary>
                /// the main getter and setter for the fiscalCode property
                /// </summary>
                public String FiscalCode
                {
                    get => fiscalCode;
                    set => fiscalCode = value;
                }
                ///<summary>
                ///the main getter and setter for the clientName property
                ///</summary>
                public String ClientName
                {
                    get => clientName;
                    set => clientName = value;
                }
                /// <summary>
                /// the main getter and setter for the clientMessage property
                /// </summary>
                public String ClientMessage
                {
                    get => clientMessage;
                    set => clientMessage = value;
                }
                /// <summary>
                /// the main getter and setter for the clientMessage property
                /// </summary>
                public Boolean DisplayMessage
                {
                    get => displayMessage;
                    set => displayMessage = value;
                }
                /// <summary>
                /// the main getter and setter for the synchBlocked property
                /// </summary>
                public Boolean SynchBlocked
                {
                    get => synchBlocked;
                    set => synchBlocked = value;
                }
            }
            public static void RetrieveServerSettings()
            {
                if (DatabaseFunctions.DatabaseConnections.DatabaseFunctions.ControllerFunctions.ClientFunctions.RetrieveClientSettings())
                {
                    //if the data is viable for synching
                    CheckSynched();
                    //we will retrieve the settings for synching
                    DatabaseFunctions.DatabaseConnections.DatabaseFunctions.ControllerFunctions.ClientFunctions.RetriveDataSynch();
                }
                else
                {
                    //we managed to attain a connection but there was no valid client for retrieval
                    Message.Message.NoClientForGUID();
                    Miscellaneous.Miscellaneous.ProgramClose();
                }
            }
            /// <summary>
            /// this function will check if the data synch has been blocked by us and inform the client as instructed
            /// </summary>
            static void CheckSynched()
            {
                //we check if the synch is blocked
                if (clientSettings.SynchBlocked)
                {
                    //if so we display either the message given by us if it exists
                    if (clientSettings.DisplayMessage)
                    {
                        //with title
                        if (clientSettings.ClientMessage.Contains(';') && !clientSettings.ClientMessage.StartsWith(";") && !clientSettings.ClientMessage.EndsWith(";"))
                            Message.Message.DisplayMentorTitleMessage();
                        //or without title 
                        else Message.Message.DisplayMentorMessage();
                    }
                    //or a generic message
                    else Message.Message.BlockedTransferMessage();
                    //and close the program
                    Miscellaneous.Miscellaneous.ProgramClose();
                }
                else
                {
                    //if there is a message we display it even if the synch is permited
                    if (clientSettings.DisplayMessage)
                    {
                        if (clientSettings.ClientMessage.Contains(';') && !clientSettings.ClientMessage.StartsWith(";") && !clientSettings.ClientMessage.EndsWith(";"))
                            Message.Message.DisplayMentorTitleMessage();
                        else Message.Message.DisplayMentorMessage();
                    }
                }
            }
            /// <summary>
            /// the main Structure for the WorkStation
            /// </summary>
            public class WorkStationStructure
            {
                /// <summary>
                /// the workStation ID property
                /// </summary>
                private Int32 workStationID = new Int32();
                /// <summary>
                /// the workStation GUID property
                /// </summary>
                private String workStationGUID = String.Empty;
                /// <summary>
                /// the workStation MAC property
                /// </summary>
                private String workStationMAC = String.Empty;
                /// <summary>
                /// the workStation WAN property
                /// </summary>
                private String workStationWAN = String.Empty;
                /// <summary>
                /// the workStation LAN property
                /// </summary>
                private String workStationLAN = String.Empty;
                /// <summary>
                /// the workStation ConnectionString Property
                /// </summary>
                private String workStationConnectionString = String.Empty;
                /// <summary>
                /// the workStation LocalFilePath property
                /// </summary>
                private String workStationFilePath = String.Empty;
                /// <summary>
                /// the workStation isServer property
                /// </summary>
                private Boolean isServer = new Boolean();
                
                /// <summary>
                /// the getter amd setter for the workStationID property
                /// </summary>
                public Int32 WorkStationID
                {
                    get => workStationID;
                    set => workStationID = value;
                }
                /// <summary>
                /// the getter and setter for the workStationGUID property
                /// </summary>
                public String WorkStationGUID
                {
                    get => workStationGUID;
                    set => workStationGUID = value;
                }
                /// <summary>
                /// the getter and setter for the workStationMAC property
                /// </summary>
                public String WorkStationMAC
                {
                    get => workStationMAC;
                    set => workStationMAC = value;
                }
                /// <summary>
                /// the getter and setter for the workStationWAN property
                /// </summary>
                public String WorkStationWAN
                {
                    get => workStationWAN;
                    set => workStationWAN = value;
                }
                /// <summary>
                /// the getter and setter for the workStationLAN property
                /// </summary>
                public String WorkStationLAN
                {
                    get => workStationLAN;
                    set => workStationLAN = value;
                }
                /// <summary>
                /// the getter and setter for the workStationConnectionString property
                /// </summary>
                public String WorkStationConnectionString
                {
                    get => workStationConnectionString;
                    set => workStationConnectionString = value;
                }
                /// <summary>
                /// the getter and setter for the workStationFile property
                /// </summary>
                public String WorkStationFilePath 
                {
                    get => workStationFilePath;
                    set => workStationFilePath = value;
                }
                /// <summary>
                /// the getter and setter for the isServer property
                /// </summary>
                public Boolean IsServer
                {
                    get => isServer;
                    set => isServer = value;
                } 
            }
            /// <summary>
            /// the main Structure for the DataSynch
            /// </summary>
            public class DataSychStructure
            {
                /// <summary>
                /// the localWorkStation property
                /// </summary>
                private WorkStationStructure localWorkStation = new WorkStationStructure();
                /// <summary>
                /// the serverWorkStation property
                /// </summary>
                private WorkStationStructure serverWorkStation = new WorkStationStructure();
                /// <summary>
                /// the retrieveFromServer property
                /// </summary>
                private Boolean retrieveFromServer = new Boolean();
                /// <summary>
                /// the retrieveSpecificWorkStation property
                /// </summary>
                private Boolean retrieveSpecificWorkStations = new Boolean();
                /// <summary>
                /// the retrieveDocuments property
                /// </summary>
                private Boolean retrieveDocuments = new Boolean();
                /// <summary>
                /// the retrieveNomenclators property
                /// </summary>
                private Boolean retrieveNomenclators = new Boolean();
                /// <summary>
                /// the retrieveSpecificFiles property
                /// </summary>
                private Boolean retrieveSpecificFiles = new Boolean();
                /// <summary>
                /// the specificWorkStationIDList property
                /// </summary>
                private String specificWorkStationIDList = String.Empty;
                /// <summary>
                /// the specificFileList property
                /// </summary>
                private String specificFileList = String.Empty;
                /// <summary>
                /// this is the getter and setter for the localWorkStation
                /// </summary>
                public WorkStationStructure LocalWorkStation
                {
                    get => localWorkStation;
                    set => localWorkStation = value;
                }
                /// <summary>
                /// this is the getter and setter for the serverWorkStation
                /// </summary>
                public WorkStationStructure ServerWorkStation
                {
                    get => serverWorkStation;
                    set => serverWorkStation = value;
                }
                /// <summary>
                /// this is the getter and setter for the retrieveFromServer
                /// </summary>
                public Boolean RetrieveFromServer
                {
                    get => retrieveFromServer;
                    set => retrieveFromServer = value;
                }
                /// <summary>
                /// this is the getter and setter for the retrieveSpecificWorkStations
                /// </summary>
                public Boolean RetrieveSpecificWorkStations
                {
                    get => retrieveSpecificWorkStations;
                    set => retrieveSpecificWorkStations = value;
                }
                /// <summary>
                /// this is the getter and setter for the retrieveDocuments
                /// </summary>
                public Boolean RetrieveDocuments
                {
                    get => retrieveDocuments;
                    set => retrieveDocuments = value;
                }
                /// <summary>
                /// this is the getter and setter for the retrieveNomenclators
                /// </summary>
                public Boolean RetrieveNomenclator
                {
                    get => retrieveNomenclators;
                    set => retrieveNomenclators = value;
                }
                /// <summary>
                /// this is the getter and setter for the retriveSpecificFiles
                /// </summary>
                public Boolean RetrieveSpecificFiles
                {
                    get => retrieveSpecificFiles;
                    set => retrieveSpecificFiles = value;
                }
                /// <summary>
                /// this is the getter and setter for the specificWorkStationIDList
                /// </summary>
                public String SpecificWorkStationIDList
                {
                    get => specificWorkStationIDList;
                    set => specificWorkStationIDList = value;
                }
                /// <summary>
                /// this is the getter and setter for the specificFileList
                /// </summary>
                public String SpecificFileList
                {
                    get => specificFileList;
                    set => specificFileList = value;
                }
            }
        }
    }
}
