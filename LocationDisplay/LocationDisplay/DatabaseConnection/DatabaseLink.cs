using GoogleMapsComponents.Maps;
using LocationDisplay.PageControllers;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDisplay.DatabaseConnection
{
    public class DatabaseLink
    {
        /// <summary>
        /// the central npgSqlConnection that will be used by all functions
        /// </summary>
        public static DatabaseController.PosgreSqlConnection npgSqlConnection = new DatabaseController.PosgreSqlConnection(PublicObjects.Settings.connectionSettings);
        #region Connection Close Function
        /// <summary>
        /// the main function for closing a connection on success
        /// </summary>
        /// <returns>true</returns>
        public static Boolean NormalConnectionClose()
        {
            npgSqlConnection.CloseConnection();
            return true;
        }
        /// <summary>
        /// the main function dor closing a connection on error
        /// </summary>
        /// <returns>false</returns>
        public static Boolean ErrorConnectionClose()
        {
            npgSqlConnection.CloseConnection();
            return false;
        }
        #endregion
        public class LocationFunctions
        {
            public static void RetrieveLocationHistory(List<DatabaseObjects.ServerLocation> locationList) 
            {
                String sqlCommand = "SELECT * FROM mentorgps.mentorgps WHERE last_update::date = now()::date";
                if (!npgSqlConnection.OpenConnection()) return ;
                DataTable result = npgSqlConnection.ExecuteReaderToDataTable(sqlCommand);
                foreach (DataRow item in result.Rows)
                {
                    locationList.Add(new DatabaseObjects.ServerLocation
                    {
                        LocationId  = (Int32)item["ID"],
                        AgentCode   = item["AGENT_CODE"].ToString(),
                        Longitude   = (Double)item["AGENT_LONGITUDE"],
                        Latitude    = (Double)item["AGENT_LATITUDE"],
                        Altitude    = (Double)item["AGENT_ALTITUDE"],
                        Accuaracy   = (Double)item["AGENT_ACCUARACY"],
                        Bearing     = (Double)item["AGENT_BEARING"],
                        Speed       = (Double)item["AGENT_SPEED"],
                        LastUpdate  = (DateTime)item["LAST_UPDATE"]

                    });
                }
            }
            public static void RetrieveSpecificLocationHistory(List<DatabaseObjects.ServerLocation> locationList, HistoryController historyController)
            {
                String sqlCommand = "SELECT * FROM mentorgps.mentorgps WHERE last_update BETWEEN :p_initial_date AND :p_final_date";
                NpgsqlParameter[] commandParameters =
                {
                    new NpgsqlParameter(":p_initial_date",historyController.initialDateTime.dateTime),
                    new NpgsqlParameter(":p_final_date",historyController.finalDateTime.dateTime)
                };
                if (!String.IsNullOrWhiteSpace(historyController.AgentFilter))
                {
                    String filterCondition = String.Join(",", historyController.AgentFilter.Split(",").ToList().Select(x => "'" + x + "'"));
                    sqlCommand += String.Format(" AND agent_code IN ({0})",filterCondition);
                }
                if (!npgSqlConnection.OpenConnection()) return;
                DataTable result = npgSqlConnection.ExecuteReaderToDataTable(sqlCommand,commandParameters);
                foreach (DataRow item in result.Rows)
                {
                    locationList.Add(new DatabaseObjects.ServerLocation
                    {
                        LocationId = (Int32)item["ID"],
                        AgentCode = item["AGENT_CODE"].ToString(),
                        Longitude = (Double)item["AGENT_LONGITUDE"],
                        Latitude = (Double)item["AGENT_LATITUDE"],
                        Altitude = (Double)item["AGENT_ALTITUDE"],
                        Accuaracy = (Double)item["AGENT_ACCURACY"],
                        Bearing = (Double)item["AGENT_BEARING"],
                        Speed = (Double)item["AGENT_SPEED"],
                        LastUpdate = (DateTime)item["LAST_UPDATE"]
                    });
                }
            }
            public static void RetrieveCurrentLocation(LocationController locationController, List<DatabaseObjects.CurrentAgentLocation> agentLocation)
            {
                String sqlCommand = @"SELECT agent_code,agent_latitude,agent_longitude
                                        FROM mentorgps.mentorgps 
                                        WHERE last_update IN 
                                        ( SELECT MAX(last_update) 
                                            FROM mentorgps.mentorgps 
                                            GROUP BY agent_code) ";
                if (!String.IsNullOrWhiteSpace(locationController.AgentFilter))
                {
                    String filterCondition = String.Join(",", locationController.AgentFilter.Split(",").ToList().Select(x => "'" + x + "'"));
                    sqlCommand += String.Format(" AND agent_code IN ({0})", filterCondition);
                }
                if (!npgSqlConnection.OpenConnection()) return;
                DataTable result = npgSqlConnection.ExecuteReaderToDataTable(sqlCommand);
                foreach (DataRow item in result.Rows)
                {
                    agentLocation.Add(new DatabaseObjects.CurrentAgentLocation
                    {
                        AgentCode = item["AGENT_CODE"].ToString(),
                        Longitude = (Double)item["AGENT_LONGITUDE"],
                        Latitude = (Double)item["AGENT_LATITUDE"]
                    });
                }
            }
            public static LatLngLiteral RetrieveCurrentLocation(String agent_code)
            {
                String sqlCommand = @"SELECT agent_latitude,agent_longitude
                                        FROM mentorgps.mentorgps 
                                        WHERE last_update IN 
                                        ( SELECT MAX(last_update) 
                                            FROM mentorgps.mentorgps 
                                            GROUP BY agent_code) AND agent_code = :p_agent_code";
                NpgsqlParameter[] commnadParameters =
                {
                    new NpgsqlParameter(":p_agent_code",agent_code)
                };
                if (!npgSqlConnection.OpenConnection()) return new LatLngLiteral();
                DataTable result = npgSqlConnection.ExecuteReaderToDataTable(sqlCommand);
                return new LatLngLiteral()
                {
                    Lat = (Double)result.Rows[0]["AGENT_LATITUDE"],
                    Lng = (Double)result.Rows[0]["AGENT_LONGITUDE"]
                };
            }
        }
    }
}
