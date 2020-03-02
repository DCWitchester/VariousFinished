using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSynch.DatabaseFunctions.DatabaseConnections
{
    /// <summary>
    /// the main Database Connection and Retrieval Functions
    /// </summary>
    class DatabaseFunctions
    {
        /// <summary>
        /// the functions for the controller database data retrieval: <= this program writes nothing in this database
        /// </summary>
        public class ControllerFunctions
        {
            /// <summary>
            /// the main database connection for the functions
            /// </summary>
            static DatabaseControl.PosgreSqlConnection connection = new DatabaseControl.PosgreSqlConnection(Settings.ServerSettings.getConnectionString);
            #region Connection Events
            /// <summary>
            /// this function will close an active connection and return true
            /// </summary>
            /// <returns>true</returns>
            static Boolean NormalConnectionClose()
            {
                connection.CloseConnection();
                return true;
            }
            /// <summary>
            /// this function will close an active connection and return false
            /// </summary>
            /// <returns>false</returns>
            static Boolean ErrorConnectionClose()
            {
                connection.CloseConnection();
                return false;
            }
            /// <summary>
            /// this function will check the connection for the program and repair it if needed
            /// </summary>
            static void CheckConnectionState()
            {
                switch (connection.connection.State)
                {
                    //if it is broken we will close it and open it
                    case System.Data.ConnectionState.Broken:
                        connection.CloseConnection();
                        if (!connection.OpenConnection()) 
                        {
                            //if the connection cannot be established we display an error message 
                            Message.Message.DatabaseConnectionError();
                            //and close the program
                            Miscellaneous.Miscellaneous.ProgramClose();
                        }
                        break;
                    //if it has been closed we will open it
                    case System.Data.ConnectionState.Closed:
                        if (!connection.OpenConnection())
                        {
                            //if the connection cannot be established we display an error message 
                            Message.Message.DatabaseConnectionError();
                            //and close the program
                            Miscellaneous.Miscellaneous.ProgramClose();
                        }
                        break;
                }
            }
            #endregion
            public class ClientFunctions
            {
                /// <summary>
                /// this function will retrieve the setting from the database based upon the guid retrieved from teh settings file
                /// </summary>
                /// <returns>wether the setting were retrieved or not</returns>
                public static Boolean RetrieveClientSettings()
                {
                    //the query string
                    String queryString = "SELECT c.id AS id, c.guid_client AS guid, c.cod_fiscal AS cod_fiscal, c.denumire AS denumire," +
                                        " cs.display_mesaj AS afisare_mesaj, cs.mesaj AS mesaj, cs.blocat AS blocat" +
                                        " FROM clienti AS c LEFT JOIN setari_client AS cs ON c.id = cs.client_id" +
                                        " WHERE c.guid_client = :p_guid_client";
                    //and the query parameter
                    NpgsqlParameter[] queryParameters =
                    {
                    new NpgsqlParameter(":p_guid_client", Settings.Settings.programSettings.ClientGuid)
                    };
                    if (!connection.OpenConnection())
                    {
                        //if the connection cannot be established we display an error message 
                        Message.Message.DatabaseConnectionError();
                        //and close the program
                        Miscellaneous.Miscellaneous.ProgramClose();
                        //for without the client settings all aditional actions are rendered mute
                    }
                    //we will only reach this point if the connection has been established
                    System.Data.DataTable result = connection.ExecuteReaderToDataTable(queryString, queryParameters);
                    //we also check if what we just retrieved is a valid set of setting
                    if (result != null && result.Rows.Count > 0)
                    {
                        Settings.ServerSettings.clientSettings.ClientID = (Int32)result.Rows[0]["ID"];
                        Settings.ServerSettings.clientSettings.ClientGUID = result.Rows[0]["GUID"].ToString();
                        Settings.ServerSettings.clientSettings.FiscalCode = result.Rows[0]["COD_FISCAL"].ToString();
                        Settings.ServerSettings.clientSettings.ClientName = result.Rows[0]["DENUMIRE"].ToString();
                        Settings.ServerSettings.clientSettings.ClientMessage = result.Rows[0]["MESAJ"].ToString();
                        Settings.ServerSettings.clientSettings.DisplayMessage = (Boolean)result.Rows[0]["AFISARE_MESAJ"];
                        Settings.ServerSettings.clientSettings.SynchBlocked = (Boolean)result.Rows[0]["BLOCAT"];
                        return NormalConnectionClose();
                    }
                    //else is useless here
                    return ErrorConnectionClose();
                }
                /// <summary>
                /// this function will retrieve the complete settings for the dataSynch
                /// </summary>
                public static void RetriveDataSynch()
                {
                    if (!connection.OpenConnection())
                    {
                        //if the connection cannot be established we display an error message 
                        Message.Message.DatabaseConnectionError();
                        //and close the program
                        Miscellaneous.Miscellaneous.ProgramClose();
                        //for without the client settings all aditional actions are rendered mute
                    }
                    //we will create a workStation structure for temporary use
                    Settings.ServerSettings.ClientSettings.WorkStationStructure workStation = new Settings.ServerSettings.ClientSettings.WorkStationStructure();
                    //we give it the GUID from the settings
                    workStation.WorkStationGUID = Settings.Settings.programSettings.WorkStationGuid;
                    //then retrieve the localServerWorkStation
                    if (retrieveWorkStationByGUID(workStation))
                    {
                        //if we are unable to retrieve any of the needed items we close the program
                        Message.Message.DataSynchSettingsError();
                        Miscellaneous.Miscellaneous.ProgramClose();
                    }
                    //we will set the global Object to our object
                    Settings.ServerSettings.dataSynch.LocalWorkStation = workStation;
                    //and reinitialize the object
                    workStation = new Settings.ServerSettings.ClientSettings.WorkStationStructure();
                    //now that our local server is done we will retrieve the settings for the global server for step 2
                    //we retrieve the id for the main server entity workStation
                    if (RetrieveWorkStationServerIDForClient(workStation))
                    {
                        //if we are unable to retrieve any of the needed items we close the program
                        Message.Message.DataSynchSettingsError();
                        Miscellaneous.Miscellaneous.ProgramClose();
                    }
                    //then we will retrieve the data for the workStation through the id
                    if (retrieveWorkStationByID(workStation))
                    {
                        //if we are unable to retrieve any of the needed items we close the program
                        Message.Message.DataSynchSettingsError();
                        Miscellaneous.Miscellaneous.ProgramClose();
                    }
                    //once we have the serverSetting we add the to the dataSynchSettings
                    Settings.ServerSettings.dataSynch.ServerWorkStation = workStation;
                    //and finally we will retrieve the complete setting
                    if (RetrieveDataSynchSettings(Settings.ServerSettings.dataSynch))
                    {
                        //if we are unable to retrieve any of the needed items we close the program
                        Message.Message.DataSynchSettingsError();
                        Miscellaneous.Miscellaneous.ProgramClose();
                    }
                    //now we finally have the entire structure needed for the data Synch
                }
                /// <summary>
                /// this function will retrieve a workStationStructure from the database based on its guid value
                /// </summary>
                /// <param name="workStation">the workStation object</param>
                /// <returns>wether the data retrieval was succesful or not</returns>
                static Boolean retrieveWorkStationByGUID(Settings.ServerSettings.ClientSettings.WorkStationStructure workStation)
                {
                    //the query String
                    String queryString = "SELECT pl.id AS id, pl.guid_punct_lucru AS guid, pl.server_mac_adress AS mac, pl.server_wan_ip AS WAN, " +
                                            "pl.server_lan_ip AS lan, pl.database_connection_string AS connection_string, pl.file_path AS file_path, " +
                                            "spl.is_server AS is_server" +
                                            "FROM puncte_lucru AS pl " +
                                            "LEFT JOIN setari_puncte_lucru AS spl ON pl.id = spl.punct_lucru_id WHERE pl.guid_punct_lucru = :p_guid_punct_lucru";
                    //the query parameters
                    NpgsqlParameter[] queryParameters = 
                    { 
                        new NpgsqlParameter(":p_guid_punct_lucru", workStation.WorkStationGUID) 
                    };
                    //we will check the connection if it is
                    CheckConnectionState();
                    //as this procedure is part of a larger one so we will just check the state of the connection for errors
                    System.Data.DataTable result = connection.ExecuteReaderToDataTable(queryString, queryParameters);
                    if (result != null && result.Rows.Count > 0)
                    {
                        workStation.WorkStationID = (Int32)result.Rows[0]["ID"];
                        workStation.WorkStationGUID = result.Rows[0]["GUID"].ToString().Trim();
                        workStation.WorkStationMAC = result.Rows[0]["MAC"].ToString().Trim();
                        workStation.WorkStationWAN = result.Rows[0]["WAN"].ToString().Trim();
                        workStation.WorkStationLAN = result.Rows[0]["LAN"].ToString().Trim();
                        workStation.WorkStationConnectionString = result.Rows[0]["CONNECTION_STRING"].ToString().Trim();
                        workStation.WorkStationFilePath = result.Rows[0]["FILE_PATH"].ToString().Trim();
                        workStation.IsServer = (Boolean)result.Rows[0]["IS_SERVER"];
                        return true;
                    };
                    return false;
                }
                /// <summary>
                /// this function will retrieve a work station based upon its id
                /// </summary>
                /// <param name="workStation">the given work station</param>
                /// <returns>the state of the query</returns>
                static Boolean retrieveWorkStationByID(Settings.ServerSettings.ClientSettings.WorkStationStructure workStation)
                {
                    //the query String
                    String queryString = "SELECT pl.id AS id, pl.guid_punct_lucru AS guid, pl.server_mac_adress AS mac, pl.server_wan_ip AS WAN, " +
                                            "pl.server_lan_ip AS lan, pl.database_connection_string AS connection_string, pl.file_path AS file_path, " +
                                            "spl.is_server AS is_server" +
                                            "FROM puncte_lucru AS pl " +
                                            "LEFT JOIN setari_puncte_lucru AS spl ON pl.id = spl.punct_lucru_id WHERE pl.id = :p_id";
                    //the query parameters
                    NpgsqlParameter[] queryParameters =
                    {
                        new NpgsqlParameter(":p_id", workStation.WorkStationGUID)
                    };
                    //we will check the connection state and repair it if needed
                    CheckConnectionState();
                    //as this procedure is part of a larger one so we will just check the state of the connection for errors
                    System.Data.DataTable result = connection.ExecuteReaderToDataTable(queryString, queryParameters);
                    if (result != null && result.Rows.Count > 0)
                    {
                        workStation.WorkStationID = (Int32)result.Rows[0]["ID"];
                        workStation.WorkStationGUID = result.Rows[0]["GUID"].ToString().Trim();
                        workStation.WorkStationMAC = result.Rows[0]["MAC"].ToString().Trim();
                        workStation.WorkStationWAN = result.Rows[0]["WAN"].ToString().Trim();
                        workStation.WorkStationLAN = result.Rows[0]["LAN"].ToString().Trim();
                        workStation.WorkStationConnectionString = result.Rows[0]["CONNECTION_STRING"].ToString().Trim();
                        workStation.WorkStationFilePath = result.Rows[0]["FILE_PATH"].ToString().Trim();
                        workStation.IsServer = (Boolean)result.Rows[0]["IS_SERVER"];
                        return true;
                    };
                    return false;
                }
                /// <summary>
                /// this function will retrieve the id of the main server workStation concept for the current client
                /// </summary>
                /// <param name="workStation">the workStationStructure</param>
                /// <returns>the state of the query</returns>
                static Boolean RetrieveWorkStationServerIDForClient(Settings.ServerSettings.ClientSettings.WorkStationStructure workStation)
                {
                    //the query String
                    String queryString = "SELECT punct_lucru_id FROM clienti WHERE id = :p_id";
                    //the query parameters
                    NpgsqlParameter[] queryParameter =
                    {
                        new NpgsqlParameter(":p_id", Settings.ServerSettings.clientSettings.ClientID)
                    };
                    //then we check the connection State once more
                    CheckConnectionState();
                    //we execute scalar the function and try to parse it to Int32 saving the state of the parsing
                    Boolean state = Int32.TryParse(connection.ExecuteScalar(queryString, queryParameter).ToString(),out Int32 result);
                    //if the parsing failed the procedure failed
                    if (!state) return false;
                    //else we set the result to the WorkStationID
                    workStation.WorkStationID = result;
                    //and return true
                    return true;
                }
                /// <summary>
                /// this is the function that will retrive the settings for the synch <= they are from the local computer
                /// </summary>
                /// <param name="dataSynch">the dataSynchStructure used for the Synch</param>
                /// <returns>the state of the query</returns>
                static Boolean RetrieveDataSynchSettings(Settings.ServerSettings.ClientSettings.DataSychStructure dataSynch)
                {
                    //the query String
                    String queryString = "SELECT * FROM setari_puncte_lucru WHERE punct_lucru_id = :p_punct_lucru_id";
                    //and parameters
                    NpgsqlParameter[] queryParameters =
                    {
                        new NpgsqlParameter(":p_punct_lucru_id",dataSynch.LocalWorkStation.WorkStationID)
                    };
                    //then we check the connection state
                    CheckConnectionState();
                    //and execute the query
                    System.Data.DataTable result = connection.ExecuteReaderToDataTable(queryString, queryParameters);
                    //we chec if there is a viable result
                    if (result != null && result.Rows.Count > 0)
                    {
                        dataSynch.RetrieveFromServer = (Boolean)result.Rows[0]["RETRIEVE_FROM_SERVER"];
                        dataSynch.RetrieveDocuments = (Boolean)result.Rows[0]["RETRIEVE_DOCUMENTS"];
                        dataSynch.RetrieveNomenclator = (Boolean)result.Rows[0]["RETRIVE_NOMENCLATORS"];
                        //dataSynch.RetrieveSpecificWork = (Boolean)result.Rows[0]["RETRIEVE_SPECIFIC"];
                        dataSynch.RetrieveSpecificWorkStations = (Boolean)result.Rows[0]["RETRIEVE_SPECIFIC"];
                        dataSynch.SpecificWorkStationIDList = result.Rows[0]["RETRIEVED_ID_LIST"].ToString();
                        dataSynch.SpecificFileList = result.Rows[0]["SPECIFIC_FILE_LIST"].ToString();
                        dataSynch.RetrieveSpecificFiles = String.IsNullOrEmpty(dataSynch.SpecificFileList);
                        return true;
                    };
                    return false;
                }
            }
        }
    }
}
