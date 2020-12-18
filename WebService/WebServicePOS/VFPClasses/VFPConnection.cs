using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebServiceEvidenta.VFPClasses
{
    public class VFPConnection : IDisposable
    {
        #region Properties
#pragma warning disable IDE1006
        /// <summary>
        /// the initial connection property
        /// </summary>
        private System.Data.OleDb.OleDbConnection fileBaseConnection { get; set; } = new System.Data.OleDb.OleDbConnection();
        /// <summary>
        /// the base file path property
        /// </summary>
        private String filePath { get; set; } = Miscellaneous.Decryptor.Decrypt(Properties.Settings.Default.CaleEvidenta);
#pragma warning restore IDE1006
        #endregion

        #region Callers
        /// <summary>
        /// the caller for the check file
        /// this file should always exist
        /// </summary>
        internal String ODBCCheckFile => filePath + "\\FOXUSER.DBF";
        /// <summary>
        /// the path to the monthly files
        /// </summary>
        internal String FilesPath => filePath + "\\DBF";
        /// <summary>
        /// the path to the glossary files
        /// </summary>
        internal String GlossaryPath => filePath + "\\NOM";

        /// <summary>
        /// the protected accessor for the base connection
        /// </summary>
        protected System.Data.OleDb.OleDbConnection FileBaseConnection
        {
            get => fileBaseConnection;
        }
        #endregion

        #region Specific File Paths

        #region Glossary Files
        /// <summary>
        /// the path to the product glossary
        /// </summary>
        internal String ProductGlossary => GlossaryPath + "\\fp.dbf";
        /// <summary>
        /// the path to the partner glossary
        /// </summary>
        internal String PartnerGlossary => GlossaryPath + "\\fb.dbf";
        #endregion

        #region Document Files
        /// <summary>
        /// the path to the specific sales file for the given datetime
        /// </summary>
        /// <param name="dateTime">the given datetime</param>
        internal String SalesFile(DateTime dateTime) => FilesPath + "\\fa" + dateTime.Month.ToString().Trim().PadLeft(2, '0') + dateTime.Year.ToString().Trim() + ".dbf";
        #endregion

        #region Special Files
        /// <summary>
        /// the path to the specific quantity files
        /// </summary>
        internal String QuantityFile => FilesPath + @"\CantitatiProduse.dbf";
        #endregion

        #endregion

        #region Char Formating
        /// <summary>
        /// the line break char
        /// </summary>
        internal String newLine             = "\r\n";
        /// <summary>
        /// the bold font formatting
        /// </summary>
        internal String fontBold            = ((char)27).ToString() + ((char)33).ToString() + ((char)10).ToString();
        /// <summary>
        /// the specific font formatting for uppercase bold
        /// </summary>
        internal String fontBoldUpperCase   = ((char)27).ToString() + ((char)33).ToString() + ((char)32).ToString();
        /// <summary>
        /// the normal font formatting
        /// </summary>
        internal String fontNormal          = ((char)27).ToString() + ((char)33).ToString() + ((char)0).ToString();
        #endregion

        #region Functionality
        /// <summary>
        /// this function will open the connection and set the base functionality
        /// </summary>
        /// <returns></returns>
        protected Boolean OpenConnection()
        {
            fileBaseConnection.ConnectionString = "Provider=vfpoledb.1;Data Source=" + filePath + ";Collating Sequence=general;";
            //try to open the dbfConnection
            try
            {
                fileBaseConnection.Open();
            }
            catch
            {
                return false;
            }
            System.Data.OleDb.OleDbCommand oCmd = fileBaseConnection.CreateCommand();
            //then we set the needed presets
            oCmd.CommandText = "SET EXCLUSIVE OFF";
            oCmd.ExecuteNonQuery();
            oCmd.CommandText = "SET DELETED ON";            // excludem inreg. marcate pt stergere
            oCmd.ExecuteNonQuery();
            oCmd.CommandText = "SET NULL OFF";              // permitem valori NULL pt. update,insert....
            oCmd.ExecuteNonQuery();
            oCmd.CommandText = "SET DATE TO DMY";           // we set the date format for future update
            //oCmd.ExecuteNonQuery();
            oCmd.CommandText = "SET CENTURY ON";            // we set the century on for the date format
            //oCmd.ExecuteNonQuery();
            //oCmd.CommandText = "SET ENGINEBEHAVIOR 70";    // pentru clauza GROUP BY: sa permita gruparea fara a specifica toara campurile ( de la 80: GROUP BY clause must list every field in the SELECT list except for fields contained in an aggregate function)
            //oCmd.ExecuteNonQuery();
            return true;
        }

        /// <summary>
        /// this function will close the existing connection
        /// </summary>
        protected void CloseConnection()
        {
            if (fileBaseConnection.State == ConnectionState.Open || fileBaseConnection.State == ConnectionState.Broken)
            {
                fileBaseConnection.Close();
            }
            fileBaseConnection.Close();
        }
        /// <summary>
        /// this function will repair a broken connection
        /// </summary>
        protected void RepairConnection()
        {
            if (IsConnectionBroken)
            {
                CloseConnection();
                OpenConnection();
            }
            if (!IsConnectionOpened)
            {
                OpenConnection();
            }
        }
        #endregion

        #region Connection Properties
        /// <summary>
        /// this property contains the state of the connection
        /// </summary>
        protected Boolean IsConnectionOpened => fileBaseConnection.State == ConnectionState.Open;
        /// <summary>
        /// this property contains the state of the connection
        /// </summary>
        protected Boolean IsConnectionBroken => fileBaseConnection.State == ConnectionState.Broken;
        #endregion

        #region Table Structures
        /// <summary>
        /// the base structure for the QuantityFile
        /// </summary>
        protected String QuantityFileStructure => "(code Character(12), quantity Numeric(18,4)";
        #endregion

        #region Dispose Implementation
        /// <summary>
        /// the main dispose function on the base class
        /// </summary>
        public void Dispose()
        {
            this.CloseConnection();
        }
        #endregion
    }
}