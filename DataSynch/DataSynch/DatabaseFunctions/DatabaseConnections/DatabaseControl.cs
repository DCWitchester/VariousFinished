using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using Npgsql;


namespace DatabaseControl
{
    class GlobalConnectionSettings
    {
        public static String connString = "Host = 5.2.228.239; Port = 26662; Database = DataSynchController; User Id = postgres; Password = pgsql";
        public static String ServerConnectionLog = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\Log";

        public static String Host = "192.168.13.130";
        public static String Port = "5432";
        public static String Database = "MentorData";
        public static String UserId = "postgres";
        public static String Password = "pgsql";

        public static void rebuildUrl()
        {
            connString = "Host=" + Host + ";Port=" + Port + ";Database=" + Database + ";User Id=" + UserId + ";Password=" + Password;
        }
        public static String shoutUrl()
        {
            return connString;
        }
    }
    public class PosgreSqlConnection : IDisposable
    {
        public NpgsqlConnection connection = new NpgsqlConnection();
        //meaningless enum, kept here because i like it
        public enum tipexecutie
        {
            /// <summary>
            /// Executes commands such as SQL INSERT, DELETE, UPDATE , and SET statements.
            /// </summary>
            ExecuteNonQuery = 1,
            /// <summary>
            /// Executes commands that return rows.
            /// </summary>
            ExecuteReader,
            /// <summary>
            /// Retrieves rows from a database and stores them to a System.DataTable
            /// </summary>
            ExecuteReaderToDataTable,
            /// <summary>
            /// Retrieves a single value and returns it as an object (for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalar,
            /// <summary>
            /// Retrieves a single value and returns it as an int(for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalarInt,
            /// <summary>
            /// Retrieves a single value and returns it in an String(for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalarString,
            /// <summary>
            /// Retrieves a single value and returns it in an double(for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalarDouble,
            /// <summary>
            /// Retrieves a single value and returns it in an char(for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalarChar,
            /// <summary>
            /// Retrieves a single value and returns it in an boolean(for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalarBool,
            /// <summary>
            /// Retrieves a single value and returns it in an long(for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalarLong,
            /// <summary>
            /// Retrieves a single value and stores it in an decimal(for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalarDecimal,
            /// <summary>
            /// Retrieves a single value and stores it in an byte(for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalarByte,
            /// <summary>
            /// Retrieves a single value and stores it in an float(for example, an aggregate value) from a database.
            /// </summary>
            ExecuteScalarFloat
            
        }
        /// <summary>
        /// Uses global connection settings. To be used for main connection.
        /// </summary>
        public PosgreSqlConnection()
        {
            connection.ConnectionString = GlobalConnectionSettings.connString;
        }
        /// <summary>
        /// Uses given connection string. To be used for alternate connections.
        /// </summary>
        /// <param name="connectionString"></param>
        public PosgreSqlConnection(String connectionString)
        {
            connection.ConnectionString = connectionString;
        }
        /// <summary>
        /// Can be used to avoid instantiation of a new object. This will close and reopen the connection with the new connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        public void setConnectionString(String connectionString)
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
            
            if(connection.State == ConnectionState.Open) CloseConnection();
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
        /// Close connection.
        /// </summary>
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
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
            ConnLog.Log("Connection disposed");
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            Dispose(true);
        }


        private NpgsqlCommand Create(String sql)
        {
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand();
            npgsqlCommand.Connection = this.connection;
            npgsqlCommand.CommandText = sql;
            return npgsqlCommand;
        }
        private NpgsqlCommand Create(String sql, NpgsqlParameter npgsqlParameter)
        {
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand();
            npgsqlCommand.Connection = this.connection;
            npgsqlCommand.CommandText = sql;
            npgsqlCommand.Parameters.Add(npgsqlParameter);
            return npgsqlCommand;
        }
        private NpgsqlCommand Create(String sql, NpgsqlParameter npgsqlParameter, NpgsqlTransaction npgsqlTransaction)
        {
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand();
            npgsqlCommand.Connection = this.connection;
            npgsqlCommand.CommandText = sql;
            npgsqlCommand.Transaction = npgsqlTransaction;
            npgsqlCommand.Parameters.Add(npgsqlParameter);
            return npgsqlCommand;
        }
        private NpgsqlCommand Create(String sql, NpgsqlParameter[] npgsqlParameters)
        {
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand();
            npgsqlCommand.Connection = this.connection;
            npgsqlCommand.CommandText = sql;
            foreach (NpgsqlParameter parameter in npgsqlParameters)
            {
                if(!(parameter.NpgsqlValue == null)) npgsqlCommand.Parameters.Add(parameter);
            }
            return npgsqlCommand;
        }
        private NpgsqlCommand Create(String sql, NpgsqlParameter[] npgsqlParameters, NpgsqlTransaction npgsqlTransaction)
        {
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand();
            npgsqlCommand.Connection = this.connection;
            npgsqlCommand.CommandText = sql;
            npgsqlCommand.Transaction = npgsqlTransaction;
            foreach (NpgsqlParameter parameter in npgsqlParameters)
            {
                if (!(parameter.NpgsqlValue == null)) npgsqlCommand.Parameters.Add(parameter);
            }
            return npgsqlCommand;
        }
    }
    //this is not used. To be replaced.
    class ConnLog
    {
        
        private static String log_path = GlobalConnectionSettings.ServerConnectionLog + ".txt";
        private static String log_folder_path = GlobalConnectionSettings.ServerConnectionLog;
        public static void Log(String text)
        {
            if (!Directory.Exists(log_folder_path))
            {
                Directory.CreateDirectory(log_folder_path);
            }
            if (!File.Exists(log_path))
            {
                File.Create(log_path).Close();
                File.SetAttributes(log_path, File.GetAttributes(log_path) | FileAttributes.Hidden);
            }
            File.AppendAllText(log_path, Environment.NewLine + DateTime.Now.ToShortTimeString() + text);
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
            if (!(connection.State == ConnectionState.Open)) connection.Open();
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
