using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace TasksApp.DatabaseControl
{
    public class DatabaseController
    {
        /// <summary>
        /// this class will contain the Connection String for the PosgreSqlConnection
        /// </summary>
        public class GlobalConnectionSettings
        {
            #region Properties
            /// <summary>
            /// the host property
            /// </summary>
            String host { get; set; } = "192.168.13.130";
            /// <summary>
            /// the port property
            /// </summary>
            String port { get; set; } = "5432";
            /// <summary>
            /// the database property
            /// </summary>
            String database { get; set; } = "MentorTaskController";
            /// <summary>
            /// the userID property
            /// </summary>
            String userId { get; set; } = "postgres";
            /// <summary>
            /// the password property
            /// </summary>
            String password { get; set; } = "pgsql";
            #endregion
            #region Public Callers
            /// <summary>
            /// the caller for the host property
            /// </summary>
            public String Host
            {
                get => host;
                set => host = value;
            }
            /// <summary>
            /// the caller for the port property
            /// </summary>
            public String Port
            {
                get => port;
                set => port = value;
            }
            /// <summary>
            /// the caller for the database property
            /// </summary>
            public String Database
            {
                get => database;
                set => database = value;
            }
            /// <summary>
            /// the caller for the userID property
            /// </summary>
            public String UserID
            {
                get => userId;
                set => userId = value;
            }
            /// <summary>
            /// the caller for the password property
            /// </summary>
            public String Password
            {
                get => password;
                set => password = value;
            }
            #endregion
            /// <summary>
            /// this function will reunite the class properties into a valid npgsql ConnectionString
            /// </summary>
            public void RebuiltConnectionString()
            {
                ConnectionString = "Host = " + host + ";Port = " + port + ";Database = " + database + ";User Id = " + userId + ";Password = " + password;
            }
            /// <summary>
            /// the main connection string for the class instance
            /// </summary>
            public String ConnectionString { get; private set; } = String.Empty;
            /// <summary>
            /// the main constructor for the class will initialize the connection string with the default preoperties
            /// </summary>
            public GlobalConnectionSettings()
            {
                RebuiltConnectionString();
            }

            public static String DefaultConnectionString { get; set; } = "Host = 5.2.228.239; Port = 26662; Database = MentorTaskController; User Id = postgres; Password = pgsql";
        }
        /// <summary>
        /// the main PostgreSqlConnection object for database queries
        /// </summary>
        public class PosgreSqlConnection : IDisposable
        {
            /// <summary>
            /// the main NpgsqlConnection
            /// </summary>
            public NpgsqlConnection connection = new NpgsqlConnection();

            /// <summary>
            /// The main constructor will set the connectionString to the default value
            /// </summary>
            public PosgreSqlConnection()
            {
                connection.ConnectionString = GlobalConnectionSettings.DefaultConnectionString;
            }
            /// <summary>
            /// Alternate constructor with a given connectionString
            /// </summary>
            /// <param name="connectionString"></param>
            public PosgreSqlConnection(String connectionString)
            {
                connection.ConnectionString = connectionString;
            }
            /// <summary>
            /// Alternate constructor with a GlobalConnectionSettings instance
            /// </summary>
            /// <param name="connectionSettings"></param>
            public PosgreSqlConnection(GlobalConnectionSettings connectionSettings)
            {
                //we rebuilt the connection string to make sure that the connection string is valid
                connectionSettings.RebuiltConnectionString();
                connection.ConnectionString = connectionSettings.ConnectionString;
            }

            /// <summary>
            /// Can be used to avoid instantiation of a new object. This will close and reopen the connection with the new connection string.
            /// </summary>
            /// <param name="connectionString"></param>
            public void SetConnectionString(String connectionString)
            {
                connection.ConnectionString = connectionString;
                OpenConnection();
            }
            /// <summary>
            /// Opens connection to be used.
            /// </summary>
            /// <returns>True if connection was succesful, false if not</returns>
            public bool OpenConnection()
            {
                //we will atempt to close the connection if it is open
                CloseConnection();
                try
                {
                    connection.Open();
                }
                catch
                {
                    return false;
                }
                return true;
            }
            /// <summary>
            /// this function will close the current connection
            /// </summary>
            public void CloseConnection()
            {
                if (connection.State == ConnectionState.Open || connection.State == ConnectionState.Broken)
                {
                    connection.Close();
                }
            }
            /// <summary>
            /// Executes commands as INSERT, DELETE, UPDATE , SET etc. Commands with the purpose to modify the database, not obtain data from it. Returns the number of rows.
            /// </summary>
            public int ExecuteNonQuery(String sql)
            {
                return Create(sql).ExecuteNonQuery();
            }
            /// <summary>
            /// Executes commands as INSERT, DELETE, UPDATE , SET etc. Commands with the purpose to modify the database, not obtain data from it. Returns the number of rows.
            /// </summary>
            public int ExecuteNonQuery(String sql, NpgsqlParameter npgsqlParameter)
            {
                return Create(sql, npgsqlParameter).ExecuteNonQuery();
            }
            /// <summary>
            /// Executes commands as INSERT, DELETE, UPDATE , SET etc. Commands with the purpose to modify the database, not obtain data from it. Returns the number of rows.
            /// </summary>
            public int ExecuteNonQuery(String sql, NpgsqlParameter[] npgsqlParameters)
            {
                return Create(sql, npgsqlParameters).ExecuteNonQuery();
            }
            /// <summary>
            /// Executes commands as INSERT, DELETE, UPDATE , SET etc. Commands with the purpose to modify the database, not obtain data from it. Returns the number of rows.
            /// </summary>
            public int ExecuteNonQuery(String sql, NpgsqlParameter npgsqlParameter, NpgsqlTransaction npgsqlTransaction)
            {
                return Create(sql, npgsqlParameter, npgsqlTransaction).ExecuteNonQuery();
            }
            /// <summary>
            /// Executes commands as INSERT, DELETE, UPDATE , SET etc. Commands with the purpose to modify the database, not obtain data from it. Returns the number of rows.
            /// </summary>
            public int ExecuteNonQuery(String sql, NpgsqlParameter[] npgsqlParameters, NpgsqlTransaction npgsqlTransaction)
            {
                return Create(sql, npgsqlParameters, npgsqlTransaction).ExecuteNonQuery();
            }
            /// <summary>
            /// Executes commands that retrieve rows.
            /// </summary>
            public DbDataReader ExecuteReader(String sql)
            {
                return Create(sql).ExecuteReader();
            }
            /// <summary>
            /// Executes commands that retrieve rows.
            /// </summary>
            public DbDataReader ExecuteReader(String sql, NpgsqlParameter npgsqlParameter)
            {
                return Create(sql, npgsqlParameter).ExecuteReader();
            }
            /// <summary>
            /// Executes commands that retrieve rows.
            /// </summary>
            public DbDataReader ExecuteReader(String sql, NpgsqlParameter[] npgsqlParameters)
            {
                return Create(sql, npgsqlParameters).ExecuteReader();
            }
            //Check lists.Is there a better way to read data from the DbDataReader?
            public DataTable ExecuteReaderToDataTable(String sql)
            {
                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(sql));
                return dt;
            }
            public DataTable ExecuteReaderToDataTable(String sql, NpgsqlParameter npgsqlParameter)
            {
                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(sql, npgsqlParameter));
                return dt;
            }
            public DataTable ExecuteReaderToDataTable(String sql, NpgsqlParameter[] npgsqlParameters)
            {
                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(sql, npgsqlParameters));
                return dt;
            }
            public Object ExecuteScalar(String sql)
            {
                return Create(sql).ExecuteScalar();
            }
            public Object ExecuteScalar(String sql, NpgsqlParameter npgsqlParameter)
            {
                return Create(sql, npgsqlParameter).ExecuteScalar();
            }
            public Object ExecuteScalar(String sql, NpgsqlParameter[] npgsqlParameters)
            {
                return Create(sql, npgsqlParameters).ExecuteScalar();
            }

            protected virtual void Dispose(Boolean status)
            {
                throw new NotImplementedException();
            }
            public void Dispose()
            {
                Dispose(true);
            }


            private NpgsqlCommand Create(String sql)
            {
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                {
                    Connection = this.connection,
                    CommandText = sql
                };
                return npgsqlCommand;
            }
            private NpgsqlCommand Create(String sql, NpgsqlParameter npgsqlParameter)
            {
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                {
                    Connection = this.connection,
                    CommandText = sql
                };
                npgsqlCommand.Parameters.Add(npgsqlParameter);
                return npgsqlCommand;
            }
            private NpgsqlCommand Create(String sql, NpgsqlParameter npgsqlParameter, NpgsqlTransaction npgsqlTransaction)
            {
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                {
                    Connection = this.connection,
                    CommandText = sql,
                    Transaction = npgsqlTransaction
                };
                npgsqlCommand.Parameters.Add(npgsqlParameter);
                return npgsqlCommand;
            }
            private NpgsqlCommand Create(String sql, NpgsqlParameter[] npgsqlParameters)
            {
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                {
                    Connection = this.connection,
                    CommandText = sql
                };
                SetCommandParameters(npgsqlCommand, npgsqlParameters);
                return npgsqlCommand;
            }
            private NpgsqlCommand Create(String sql, NpgsqlParameter[] npgsqlParameters, NpgsqlTransaction npgsqlTransaction)
            {
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                {
                    Connection = this.connection,
                    CommandText = sql,
                    Transaction = npgsqlTransaction
                };
                SetCommandParameters(npgsqlCommand, npgsqlParameters);
                return npgsqlCommand;
            }
            /// <summary>
            /// this function will add the parameter array to the commands collection
            /// </summary>
            /// <param name="command">the query command</param>
            /// <param name="npgsqlParameters">the query parameters</param>
            private void SetCommandParameters(NpgsqlCommand command, NpgsqlParameter[] npgsqlParameters)
            {
                foreach (NpgsqlParameter parameter in npgsqlParameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
        }

        public static class Transactions
        {
            /// <summary>
            /// this function will create a transaction 
            /// </summary>
            /// <param name="connection"></param>
            /// <returns>we return the transaction</returns>
            public static NpgsqlTransaction CreateTransaction(NpgsqlConnection connection)
            {
                if (connection.State == ConnectionState.Broken) connection.Close();
                if (connection.State == ConnectionState.Closed) connection.Open();
                NpgsqlTransaction transaction = connection.BeginTransaction();
                return transaction;
            }
            /// <summary>
            /// this function will try to Commit the transaction and on error will Rollback the transaction
            /// </summary>
            /// <param name="transaction"></param>
            /// <returns>True if the Commit was succesful; false otherwise</returns>
            public static Boolean CommitTransaction(NpgsqlTransaction transaction)
            {
                try
                {
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
            /// <summary>
            /// this will end the transaction and close an open connection
            /// </summary>
            /// <param name="connection"></param>
            public static void EndTransaction(NpgsqlConnection connection)
            {
                connection.Close();
            }
        }
    }
}
