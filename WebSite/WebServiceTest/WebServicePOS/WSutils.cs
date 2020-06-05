using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Xml;
using FirebirdSql.Data.FirebirdClient;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Xml.Serialization;

namespace WebServicePOS
{
    public static class StringExtensions
    {
        public static string PadCenter(this string str, int length, char paddingChar)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft, paddingChar).PadRight(length, paddingChar);
        }
    }

    public enum tipGetprod
    {
        toate,
        vanzareRapida
    }

    public static class Setari
    {
        public static string caleEcashFDB = @"C:\Programe\Ecash\Ecash.FDB";
        public static string userFirebird = "SYSDBA";
        public static string passFirebird = "masterkey";
        public static string calePOS = @"C:\Programe\Vmentor\DATE";
        public static bool tokenInSetariIni = false;
        public static string token = "token";
        public static string notificationURL = "";

        public static bool tokenIsOk(string tk)
        {
            return ((token + "42x").CompareTo(tk.Trim()) == 0);
        }
        public static void citeste()
        {
            string setari = HttpContext.Current.Server.MapPath("setari.ini");
            if (File.Exists(setari))
            {
                using (StreamReader sr = new StreamReader(setari))
                {
                    Codificator navajo = new Codificator();
                    Setari.caleEcashFDB = navajo.decripteaza(sr.ReadLine());
                    Setari.userFirebird = navajo.decripteaza(sr.ReadLine());
                    Setari.passFirebird = navajo.decripteaza(sr.ReadLine());
                    Setari.calePOS = navajo.decripteaza(sr.ReadLine());
                    if (!sr.EndOfStream)
                    {
                        Setari.token = navajo.decripteaza(sr.ReadLine());                        
                        Setari.token = (Setari.tokenInSetariIni=Setari.token.Contains("=")) ? Setari.token.Substring(Setari.token.IndexOf("=") + 1).Trim() : "token";
                    }
                    if (!Setari.tokenInSetariIni) { using (EPay epay = new EPay()) { Setari.token = epay.getToken(); } }
                }
            }
            string notificari = HttpContext.Current.Server.MapPath("notificari.ini");
            if (File.Exists(notificari)) { using (StreamReader sr = new StreamReader(notificari)) { Setari.notificationURL = sr.ReadLine(); } }
        }
    }

    public class Codificator
    {
        private TripleDESCryptoServiceProvider x3des = new TripleDESCryptoServiceProvider();
        private UTF8Encoding xutf8 = new UTF8Encoding();
        public string cripteaza(string text)
        {
            # region generare cheie si vector de lungimi cunoscute
            // ma asigur ca key si iv au exact lungimile 24 respectiv 8
            byte[] key = new byte[24];
            byte[] iv = new byte[8];
            RNGCryptoServiceProvider randgen = new RNGCryptoServiceProvider();
            randgen.GetBytes(key);
            randgen.GetBytes(iv);
            x3des.Key = key;
            x3des.IV = iv;

            // tot codul de mai sus e semiinutil , dar acum nu mai depind de extensii viitoare ale clasei 
            //TripleDESCryptoServiceProvider ( care ar putea modifica 24 si 8)
            #endregion
            return Convert.ToBase64String(x3des.Key) + Convert.ToBase64String(Transform(xutf8.GetBytes(text),
                                          x3des.CreateEncryptor(x3des.Key, x3des.IV))) + Convert.ToBase64String(x3des.IV);
        }
        public string decripteaza(string text)
        {
            try
            {
                return xutf8.GetString(Transform(Convert.FromBase64String(text.Substring(32, text.Length - 44)),
                       x3des.CreateDecryptor(Convert.FromBase64String(text.Substring(0, 32)),
                       Convert.FromBase64String(text.Substring(text.Length - 12)))));
            }
            catch (Exception)
            {
                return null;
            }
        }
        private byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            if (input.Length > 0)
            {
                MemoryStream memStream = new MemoryStream();
                CryptoStream cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write);
                cryptStream.Write(input, 0, input.Length);
                cryptStream.FlushFinalBlock();
                memStream.Position = 0;
                byte[] result = memStream.ToArray();
                memStream.Close();
                cryptStream.Close();
                return result;
            }
            else
            { return new byte[] { 0 }; }
        }
    }

    public class firebird
    {
        public enum tipexecutie { FbCommand_ExecuteNonQuery = 1, FbCommand_ExecuteReader, FbCommand_ExecuteScalar, FbDataAdapter_Fill }

        static public RezultatBSO ExecutaSQL(FirebirdSql.Data.FirebirdClient.FbConnection conexiune, string sirsql, tipexecutie tipexec, string tabela, bool iesire, string mesaj, FirebirdSql.Data.FirebirdClient.FbParameter[] ppar)
        {
            FirebirdSql.Data.FirebirdClient.FbTransaction tranzactie = null;
            return ExecutaSQL(conexiune, sirsql, tipexec, tabela, iesire, mesaj, ppar, tranzactie);
        }
        static public RezultatBSO ExecutaSQL(FirebirdSql.Data.FirebirdClient.FbConnection conexiune, string sirsql, tipexecutie tipexec, string tabela, bool iesire, string mesaj)
        {
            FirebirdSql.Data.FirebirdClient.FbParameter[] par = { };
            return ExecutaSQL(conexiune, sirsql, tipexec, tabela, iesire, mesaj, par);
        }
        static public RezultatBSO ExecutaSQL(FirebirdSql.Data.FirebirdClient.FbConnection conexiune, string sirsql, tipexecutie tipexec, string tabela, bool iesire, string mesaj, FirebirdSql.Data.FirebirdClient.FbParameter[] ppar, FirebirdSql.Data.FirebirdClient.FbTransaction tranzactie)
        {
            /// singura ( ? ) functie care va comunica DIRECT cu serverul firebird , pentru a putea centraliza urmarirea erorilor
            /// conexiune   = conexiunea prin care comunica cu serverul firebird - exista si este deschisa
            ///               practic , este conexiuneSecurity sau conexiuneDate
            /// sirsql      = sirul sql ce trebuie a fi executat
            /// tipexecutie = 1 : FirebirdSql.Data.FirebirdClient.FbCommand.ExecuteNonQuery
            ///               2 : FirebirdSql.Data.FirebirdClient.FbCommand.ExecuteReader
            ///               3 : FirebirdSql.Data.FirebirdClient.FbCommand.ExecuteScalar
            ///               4 : FirebirdSql.Data.FirebirdClient.FbDataAdapter.Fill   
            /// tabela      = numele tabelei care sufera transformari ( din necesitati de jurnalizare a actiunilor )
            /// iesire      = daca e true , in cazul esuarii executiei sql-ului se iese din program
            /// mesaj       = daca e nevid va fi aratat in caz de eroare
            /// tranzactie  = numele tranzactiei in interiorul careia dorim sa executam sql-ul sau null
            /// in caz de reusita functia intoarce rezultatul executie - e treaba apelantului sa converteasca la tipul dorit Object-ul primit.

            RezultatBSO rez = new RezultatBSO();
            //Object rez = new Object();
            DataTable dt = new DataTable();
            FirebirdSql.Data.FirebirdClient.FbCommand fbcommand;
            if (tranzactie != null)
            {
                fbcommand = new FirebirdSql.Data.FirebirdClient.FbCommand(sirsql, conexiune, tranzactie);
            }
            else
            {
                fbcommand = new FirebirdSql.Data.FirebirdClient.FbCommand(sirsql, conexiune);
            }
            FirebirdSql.Data.FirebirdClient.FbDataAdapter fbDA = new FirebirdSql.Data.FirebirdClient.FbDataAdapter(sirsql, conexiune);
            if (tipexec != tipexecutie.FbDataAdapter_Fill) { for (int i = 0; i < ppar.GetLength(0); i++) { fbcommand.Parameters.Add(ppar[i]); } }
            else { for (int i = 0; i < ppar.GetLength(0); i++) { fbDA.SelectCommand.Parameters.Add(ppar[i]); } }
            {
                try
                {
                    switch (tipexec)
                    {
                        case tipexecutie.FbCommand_ExecuteNonQuery:
                            //Executes commands such as SQL INSERT, DELETE, UPDATE , and SET statements.
                            //fbcommand.Transaction = tranzactie;
                            rez.obiect = fbcommand.ExecuteNonQuery();
                            break;
                        case tipexecutie.FbCommand_ExecuteReader:
                            //Executes commands that return rows.
                            //fbcommand.Transaction = tranzactie;
                            rez.obiect = fbcommand.ExecuteReader();
                            break;
                        case tipexecutie.FbCommand_ExecuteScalar:
                            //Retrieves a single value (for example, an aggregate value) from a database.
                            //fbcommand.Transaction = tranzactie;
                            rez.obiect = fbcommand.ExecuteScalar();
                            break;
                        case tipexecutie.FbDataAdapter_Fill:
                            if (tranzactie != null)
                            {
                                fbDA.SelectCommand.Transaction = tranzactie;
                            }
                            fbDA.Fill(dt);
                            rez.obiect = dt;
                            break;
                    }
                    rez.succes = true;
                }
                catch (FirebirdSql.Data.FirebirdClient.FbException exc)
                {
                    jurnal.scrie("firebird.executa " + exc.ErrorCode.ToString() + " " + exc.Message);
                    jurnal.scrie("fbcommand: " + fbcommand.CommandText);
                    rez.succes = false;
                    rez.obiect = null;
                    rez.mesaj = exc.ErrorCode.ToString() + " " + exc.Message;
                }
            }
            fbDA.Dispose();
            fbcommand.Dispose();
            //if (rez is System.DBNull) { rez = null; }
            return rez;
        }
        static public FirebirdSql.Data.FirebirdClient.FbTransaction BeginTranzactie(FirebirdSql.Data.FirebirdClient.FbConnection conexiune, string nume)
        {
            try
            {
                FirebirdSql.Data.FirebirdClient.FbTransaction myTransaction = conexiune.BeginTransaction(nume);
                return myTransaction;
            }
            catch (Exception exc)
            {
                if (exc == null)
                    return null;
                return null;
            }
        }
        static public bool ComiteTranzactia(FirebirdSql.Data.FirebirdClient.FbTransaction tranzactie)
        {
            tranzactie.Commit();
            return true;
        }
        static public bool EndTranzactie(FirebirdSql.Data.FirebirdClient.FbTransaction tranzactie)
        {
            tranzactie.Commit();
            return true;
        }
        static public bool conectareBD(FirebirdSql.Data.FirebirdClient.FbConnection fbconexiune, string caleServer, string userServer, string passServer, ref string mesaj)
        {
            fbconexiune.ConnectionString = "User=" + userServer + ";Password=" + passServer +
                ";Database=" + caleServer + ";Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=0;Connection timeout=15;Pooling=True;Packet Size=8192;Server Type=0";
            fbconexiune.Close();
            try
            {
                fbconexiune.Open();
            }
            catch (FirebirdSql.Data.FirebirdClient.FbException exceptie)
            {
                mesaj = "Conexiune esuata";
                // DialogResult retry=DialogResult.None;
                //switch (exceptie.Errors[0].Number)
                switch (exceptie.ErrorCode)
                {
                    case 335544344:
                        mesaj = "Nu ma pot conecta la baza de date!   Verificati setarile de conectare, serverul firebird...";
                        break;
                    case 335544721:
                        mesaj = "Nu ma pot conecta la baza de date!   Verificati setarile de conectare, serverul firebird...";
                        break;
                    case 335544472:
                        mesaj = "Nu ma pot conecta la baza de date!   Verificati setarile de conectare ( username si parola )";
                        break;
                    default:
                        mesaj = exceptie.ToString();
                        break;
                }
                return false;
            }
            mesaj = "";
            return true;
        }
        static public void deconectareBD(FirebirdSql.Data.FirebirdClient.FbConnection fbconexiune)
        {
            fbconexiune.Close();
        }
    }

    public struct RezultatBSO
    {
        public bool succes;
        public string mesaj;
        public object obiect;
        public string toText()
        {
            return "[succes=" + succes.ToString() + "]" +
                    "[mesaj=" + (mesaj == null ? "null" : mesaj) + "]" +
                    "[obiect=" + (obiect == null ? "null" : obiect.ToString()) + "]";
        }
    }

    public static class jurnal
    {
        public static string tempfile = System.IO.Path.GetTempPath() + "\\WS_MentorPOS_log.txt";
        public static void scrie(string mesaj)
        {
            StreamWriter log;
            if (!File.Exists(tempfile))
            {
                log = new StreamWriter(tempfile);
            }
            else
            {
                log = File.AppendText(tempfile);
            }
            log.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fffffff") + ">> " + mesaj);
            log.Close();
        }
        public static void eroare(string mesaj)
        {
            string tempfile = System.IO.Path.GetTempPath() + "\\WS_MentorPOS_erori.txt";
            StreamWriter log;
            if (!File.Exists(tempfile))
            {
                log = new StreamWriter(tempfile);
            }
            else
            {
                log = File.AppendText(tempfile);
            }
            log.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fffffff") + ">> " + mesaj);
            log.Close();
        }
    }

    public class WSPOS : IDisposable
    {
        /* IDisposable */
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed resources                    
                }
                // Call the appropriate methods to clean up unmanaged resources here
                disposed = true;
            }
        }
        ~WSPOS()
        {
            Dispose(false);
        }
        /* END IDisposable */

        private string apkPath = HttpContext.Current.Server.MapPath("~\\apk");

        public Byte[] getApkFile()
        {
            Byte[] documentContents = null;
            String theFile = apkPath + @"\mentorPOS.apk";
            if (File.Exists(theFile))
            {
                FileStream objfilestream = new FileStream(theFile, FileMode.Open, FileAccess.Read);
                int len = (int)objfilestream.Length;
                documentContents = new Byte[len];
                objfilestream.Read(documentContents, 0, len);
                objfilestream.Close();
            }
            return documentContents;
        }
        public string getApkVersion()
        {
            string version = "";
            String theFile = apkPath + @"\mentorPOS.txt";
            try {
                version = System.IO.File.ReadAllText(theFile).Trim();
            } catch (Exception e)
            {
                version = "ERR : " + e.Message;
            }
            return version;
        }
    }

    public class vfpPOS : IDisposable
    {
        System.Data.OleDb.OleDbConnection dbfConn = new System.Data.OleDb.OleDbConnection();
        System.Data.OleDb.OleDbCommand oCmd;
        string eroare = "";
        string fisDBC = Setari.calePOS + @"\v.dbc";
        string fisVanzari = Setari.calePOS + @"\vanz.dbf";
        string fisPers = Setari.calePOS + @"\pers.dbf";
        string fisGest = Setari.calePOS + @"\gest.dbf";
        string fisCateg = Setari.calePOS + @"\categ.dbf";
        string fisProd = Setari.calePOS + @"\prod.dbf";
        string fisMese = Setari.calePOS + @"\mese.dbf";
        string fisSetari = Setari.calePOS + @"\setari.dbf";
        string fisIstoric = Setari.calePOS + @"\istoric.dbf";
        string fisFeluri = Setari.calePOS + @"\feluri.dbf";
        string fisTipProd = Setari.calePOS + @"\tipprod.dbf";
        string fisJSONVANZ = Setari.calePOS + @"\jsonvanz.dbf";
        string fisCoteTva = Setari.calePOS + @"\cote_tva.dbf";
        string fisStariVanzari = Setari.calePOS + @"\stari_vanz.dbf";

        //
        public string newLine = "\r\n";
        public string fontBold = ((char)27).ToString() + ((char)33).ToString() + ((char)10).ToString();
        public string fontBoldMare = ((char)27).ToString() + ((char)33).ToString() + ((char)32).ToString();
        public string fontNormal = ((char)27).ToString() + ((char)33).ToString() + ((char)0).ToString();

        /* IDisposable */
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed resources                    
                }
                // Call the appropriate methods to clean up unmanaged resources here
                dbfConn.Close();
                disposed = true;
            }
        }
        ~vfpPOS()
        {
            Dispose(false);
        }
        /* END IDisposable */

        public vfpPOS()
        {
            #region initializare vfpoledb
            //dbfConn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + Setari.calePOS + ";Collating Sequence=general;";
            //dbfConn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + fisDBC + ";Collating Sequence=general;";
            Decryptor decryptor = new Decryptor();
            String spath = decryptor.Decrypt(WebConfigurationManager.AppSettings["DatabasePath"]);
            dbfConn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + decryptor.Decrypt(WebConfigurationManager.AppSettings["DatabasePath"]) + ";Collating Sequence=general;";
            try
            {
                dbfConn.Open();
            }
            catch (Exception ee)
            {
                eroare = "Eroare: nu ma pot conecta la baza de date pentru comunicare: " + fisVanzari + " " + ee.Message;
                jurnal.scrie(eroare);
                // trebuie sa si trimit eroarea asta la o pagina de facut Erori.aspx?text=eroare;
                return;
            }
            oCmd = dbfConn.CreateCommand();
            oCmd.CommandText = "SET EXCLUSIVE OFF";
            oCmd.ExecuteNonQuery();
            oCmd.CommandText = "SET DELETED ON";            // excludem inreg. marcate pt stergere
            oCmd.ExecuteNonQuery();
            oCmd.CommandText = "SET NULL OFF";              // permitem valori NULL pt. update,insert....
            oCmd.ExecuteNonQuery();
            oCmd.CommandText = "SET ENGINEBEHAVIOR 70";     // pentru clauza GROUP BY: sa permita gruparea fara a specifica toara campurile ( de la 80: GROUP BY clause must list every field in the SELECT list except for fields contained in an aggregate function)
            oCmd.ExecuteNonQuery();
            #endregion
            //if (!System.IO.File.Exists(fisVanzari)) { eroare = "Eroare: lipsa fisier: " + fisVanzari; jurnal.scrie(eroare); return; }
            //oCmd.CommandText = @"SELECT * FROM " + fisVanzari;
        }
        #region Zona Transart
        public XmlDocument getAgenti()
        {
            String command = "SELECT agent AS CodAgent, nume AS NumeAgent FROM nom\\agenti WHERE !EMPTY(caleps) AND !EMPTY(nume) AND INLIST(gest,'01','02','20')";
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "Agenti";
            //write to xml
            StringWriter writer = new StringWriter();
            dt.WriteXml(writer, true);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(writer.ToString());
            return xmlDocument;
        }
        public XmlDocument getDepozite()
        {
            String command = "SELECT gest AS CodDepozit, adresa AS AdresaDepozit, localitate AS OrasDepozit, deng AS NumeDepozit " +
                                "FROM nom\\gest WHERE INLIST(gest,'01','02','20') ";
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "Depozite";
            //write to xml
            StringWriter writer = new StringWriter();
            dt.WriteXml(writer, true);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(writer.ToString());
            return xmlDocument;
        }
        public XmlDocument getRetete()
        {
            String command = "SELECT codp,denm FROM nom\\fp WHERE reteta = .T.";
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "Retete";
            WebServiceBergenbier.Classes.Retete retete = new WebServiceBergenbier.Classes.Retete();
            retete.setReteteFromDataTable(dt);
            XmlSerializer serializer = new XmlSerializer(typeof(WebServiceBergenbier.Classes.Retete));
            String result = String.Empty;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, retete);
                memoryStream.Position = 0;
                result = new StreamReader(memoryStream).ReadToEnd();
            }
            //write to xml
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);
            return xmlDocument;
        }
        public XmlDocument getRetetar(String codp)
        {
            String command = "SELECT fp.codp,fp.denm FROM nom\\fp LEFT JOIN nom\\fr ON fr.codp == fp.codp WHERE UPPER(ALLTRIM(fr.codr)) = '"+codp.Trim()+"'";
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "Retetar";
            //write to xml
            StringWriter writer = new StringWriter();
            dt.WriteXml(writer, true);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(writer.ToString());
            return xmlDocument;
        }
        public XmlDocument getClienti(DateTime initialPeriod, DateTime finalPeriod)
        {
            String initialDateTimeFile = @"dbf\fa" + initialPeriod.Month.ToString().PadLeft(2, '0') + initialPeriod.Year.ToString();
            String command = "SELECT DISTINCT fb.fuben AS CodClient, fb.codf AS CodFiscal, fb.denfb AS DenumireClient," +
                                    " fb.Loc AS OrasClient, adresa AS AdresaClient " +
                                    "FROM " + initialDateTimeFile + " AS fa " +
                                    "LEFT JOIN nom\\fb ON fb.fuben == fa.fuben "+
                                    "LEFT JOIN nom\\fp ON fp.codp == fa.codp " +
                                    "WHERE fp.fuben == 'G292' AND INLIST(fa.gest,'01','02','20')";
            initialPeriod = initialPeriod.AddMonths(1);
            while (initialPeriod <= finalPeriod)
            {
                initialDateTimeFile = @"dbf\fa" + initialPeriod.Month.ToString().PadLeft(2, '0') + initialPeriod.Year.ToString();
                command = command + " UNION " +
                                    "SELECT DISTINCT fb.fuben AS CodClient, fb.codf AS CodFiscal, fb.denfb AS DenumireClient," +
                                    " fb.Loc AS OrasClient, adresa AS AdresaClient " +
                                    "FROM " + initialDateTimeFile + " AS fa " +
                                    "LEFT JOIN nom\\fb ON fb.fuben == fa.fuben " +
                                    "LEFT JOIN nom\\fp ON fp.codp == fa.codp " +
                                    "WHERE fp.fuben == 'G292' AND INLIST(fa.gest,'01','02','20')";
                initialPeriod = initialPeriod.AddMonths(1);
            }
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "Depozite";
            //write to xml
            StringWriter writer = new StringWriter();
            dt.WriteXml(writer, true);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(writer.ToString());
            return xmlDocument;
        }
        public XmlDocument getFacturi(DateTime initialPeriod, DateTime finalPeriod)
        {
            String initialDateTimeFile = @"dbf\fa" + initialPeriod.Month.ToString().PadLeft(2, '0') + initialPeriod.Year.ToString();
            String command = "SELECT DISTINCT fa.nrdoc AS CodFactura, fa.nrdoc AS NumarFactura, SPACE(5) AS SerieFactura," +
                                    " fa.gest AS codDepozit, fa.data AS DataEmitere, fa.fuben AS CodClient, fa.agent AS CodAgent " +
                                    "FROM " + initialDateTimeFile + " AS fa " +
                                    "LEFT JOIN nom\\fp ON fp.codp == fa.codp " +
                                     "WHERE fp.fuben == 'G292' AND INLIST(fa.gest,'01','02','20')";
            initialPeriod = initialPeriod.AddMonths(1);
            while (initialPeriod <= finalPeriod)
            {
                initialDateTimeFile = @"dbf\fa" + initialPeriod.Month.ToString().PadLeft(2, '0') + initialPeriod.Year.ToString();
                command = command + " UNION " +
                    "SELECT DISTINCT fa.nrdoc AS CodFactura, fa.nrdoc AS NumarFactura, SPACE(5) AS SerieFactura," +
                                    " fa.gest AS codDepozit, fa.data AS DataEmitere, fa.fuben AS CodClient, fa.agent AS CodAgent " +
                                    "FROM " + initialDateTimeFile + " AS fa " +
                                    "LEFT JOIN nom\\fp ON fp.codp == fa.codp " +
                                     "WHERE fp.fuben == 'G292' AND INLIST(fa.gest,'01','02','20')";
                initialPeriod = initialPeriod.AddMonths(1);
            }
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "Facturi";
            //write to xml
            StringWriter writer = new StringWriter();
            dt.WriteXml(writer, true);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(writer.ToString());
            return xmlDocument;
        }
        public XmlDocument getStoc()
        {
            String command = "SELECT st.gest AS CodDepozit, st.codp AS CodProdus, st.cant AS cantitate "+
                                "FROM dbf\\stocreal AS st LEFT JOIN nom\\fp ON st.codp == fp.codp " +
                                        "WHERE fp.fuben == 'G292' AND INLIST(st.gest,'01','02','20')";
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "Facturi";
            //write to xml
            StringWriter writer = new StringWriter();
            dt.WriteXml(writer, true);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(writer.ToString());
            return xmlDocument;
        }

        public XmlDocument getFacturiPozitii(DateTime initialPeriod, DateTime finalPeriod)
        {
            String initialDateTimeFile = @"dbf\fa" + initialPeriod.Month.ToString().PadLeft(2, '0') + initialPeriod.Year.ToString();
            String command = "SELECT RECNO() AS CodPozitie, fa.nrdoc AS CodFactura, fa.codp AS CodProdus," +
                                    " fa.cant AS Cantitate " +
                                    "FROM " + initialDateTimeFile + " AS fa " +
                                    "LEFT JOIN nom\\fp ON fp.codp == fa.codp " +
                                     "WHERE fp.fuben == 'G292' AND INLIST(fa.gest,'01','02','20')";
            initialPeriod = initialPeriod.AddMonths(1);
            while (initialPeriod <= finalPeriod)
            {
                initialDateTimeFile = @"dbf\fa" + initialPeriod.Month.ToString().PadLeft(2, '0') + initialPeriod.Year.ToString();
                command = command + " UNION " +
                                    "SELECT RECNO() AS CodPozitie, fa.nrdoc AS CodFactura, fa.codp AS CodProdus," +
                                    " fa.cant AS Cantitate " +
                                    "FROM " + initialDateTimeFile + " AS fa " +
                                    "LEFT JOIN nom\\fp ON fp.codp == fa.codp " +
                                    "WHERE fp.fuben == 'G292' AND INLIST(fa.gest,'01','02','20')";
                initialPeriod = initialPeriod.AddMonths(1);
            }
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "Facturi";
            //write to xml
            StringWriter writer = new StringWriter();
            dt.WriteXml(writer, true);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(writer.ToString());
            return xmlDocument;
        }

        #endregion 
        public DataTable getSetari()
        {
            // aceasta metoda nu va fi expusa prin WS
            oCmd.CommandText = @"SELECT * FROM " + fisSetari;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            return dt;
        }
        public DataTable getSales(DateTime initialPeriod, DateTime finalPeriod)
        {
            String initialDateTimeFile = @"dbf\fa" + initialPeriod.Month.ToString().PadLeft(2, '0') + initialPeriod.Year.ToString();
            String command = @"SELECT fa.nrdoc AS numa_document, fa.data AS data_document, fa.fuben AS cod_partener," +
                                    " fb.denfb AS denumire_partener," +
                                    " fa.gest AS gestiune, fa.agent AS agent, fp.codp AS cod_produs, fa.cant AS cantitate," +
                                    " fp.denm AS denumire, fa.tvac AS cota_tva," +
                                    " fa.datasc AS data_scadenta, fa.agent AS cod_agent, agenti.nume AS nume_agent," +
                                    " fa.pcv AS pret_vanzare_fara_tva, fa.pv AS pret_vanzare, fa.cant*fa.pv AS valoare_vanzare " +
                                    "FROM " + initialDateTimeFile + " AS fa " +
                                    @"LEFT JOIN nom\fb on fa.fuben == fb.fuben " +
                                    @"LEFT JOIN nom\fp ON fp.codp == fa.codp " +
                                    @"LEFT JOIN nom\agenti ON agenti.agent == fa.agent " +
                                     "WHERE fp.fuben == 'G292'";
            initialPeriod = initialPeriod.AddMonths(1);
            while (initialPeriod <= finalPeriod)
            {
                initialDateTimeFile = @"dbf\fa" + initialPeriod.Month.ToString().PadLeft(2, '0') + initialPeriod.Year.ToString();
                command = command + " UNION " +
                    @"SELECT fa.nrdoc AS numa_document, fa.data AS data_document, fa.fuben AS cod_partener," +
                                " fb.denfb AS denumire_partener," +
                                " fa.gest AS gestiune, fa.agent AS agent, fp.codp AS cod_produs, fa.cant AS cantitate," +
                                " fp.denm AS denumire, fa.tvac AS cota_tva," +
                                " fa.datasc AS data_scadenta, fa.agent AS cod_agent, agenti.nume AS nume_agent," +
                                " fa.pcv AS pret_vanzare_fara_tva, fa.pv AS pret_vanzare, fa.cant*fa.pv AS valoare_vanzare " +
                                "FROM " + initialDateTimeFile + " AS fa " +
                                @"LEFT JOIN nom\fb on fa.fuben == fb.fuben " +
                                @"LEFT JOIN nom\fp ON fp.codp == fa.codp " +
                                @"LEFT JOIN nom\agenti ON agenti.agent == fa.agent" +
                                " WHERE fp.fuben == 'G292'";
                initialPeriod = initialPeriod.AddMonths(1);
            }
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "fact";
            return dt;
        }
        public DataTable getStocv2()
        {
            String command = @"SELECT st.gest AS gestiune, st.codp AS cod_produs, st.denp AS denumire_produs, 
                                        st.pv AS pret_vanzare, st.cant AS stoc FROM dbf\stocreal AS st LEFT JOIN nom\fp ON st.codp == fp.codp
                                        WHERE fp.fuben == 'G292'";
            oCmd.CommandText = command;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "fact";
            return dt;
        }
        public DataTable getUser(string codUser)
        {
            //jurnal.scrie("getUser=[" + codUser + "]");
            oCmd.CommandText = @"SELECT * FROM " + fisPers + " WHERE alltrim(ccod)=='" + codUser.Trim().Replace("\"", "").Replace("'", "") + "'";
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "pers";
            return dt;
        }
        public string getCcodOfCard(string card)
        {
            string cardX = card.Trim().Replace("\"", "").Replace("'", "");
            if (cardX.Length == 0) { return "ERR"; }
            oCmd.CommandText = @"SELECT ccod FROM " + fisPers + " WHERE alltrim(card)='" + cardX + "'";
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            if (dt.Rows.Count < 1)
            {
                return "ERR";
            }
            else
            {
                oCmd.CommandText = @"update " + fisPers + " set card='' WHERE alltrim(card)='" + card.Trim().Replace("\"", "").Replace("'", "") + "'";
                oCmd.ExecuteNonQuery();
                return dt.Rows[0]["ccod"].ToString();
            }
        }
        public DataTable getUsers()
        {
            // aceasta metoda nu va fi expusa prin WS
            oCmd.CommandText = @"SELECT * where !lmanager FROM " + fisPers;
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "pers";
            return dt;
        }
        public DataTable getMeseOfUser(string codOspatar)
        {
            //oCmd.CommandText = @"SELECT Ntable , round(sum(Ncant*Npret),2) as Valoare , 0 as blocat, DATETIME()-min(tora) as secunde FROM " + fisVanzari + " WHERE cuser='" + codOspatar.Replace("\"", "").Replace("'", "") + "' GROUP BY Ntable";
            oCmd.CommandText = @"SELECT v.Ntable , round(v.Ncant*v.Npret,2) as Valoare , m.blocat , DATETIME()-v.tora as secunde, " +
                                    "iif(ncant=0 , spac(50) , ALLTRIM(STRTRAN(PADL(ncant,10),'.000',''))+' x '+cden ) as produs " +
                                " FROM [" + fisVanzari + "] v " +
                                " LEFT JOIN (SELECT RECNO() as masa, iif(lblocat,1,0) as blocat FROM [" + fisMese + "] ) m ON v.ntable=m.masa  " +
                                " WHERE v.cuser='" + codOspatar.Replace("\"", "").Replace("'", "") + "' ";
            DataTable dv = new DataTable("mese");
            dv.Load(oCmd.ExecuteReader());
            var meseL = from v in dv.AsEnumerable()
                        group v by new { ntable = v.Field<Decimal>("ntable") } into gv
                        select new
                        {
                            ntable = gv.Key.ntable,
                            valoare = gv.Sum(x => x.Field<decimal>("valoare")),
                            blocat = gv.ElementAt(0).Field<decimal>("blocat"),
                            secunde = gv.Max(x => x.Field<decimal>("secunde")),
                            produse = string.Join("\n", gv.Select(x => x.Field<String>("produs").Trim()).ToArray().Reverse()),
                        };

            //oCmd.CommandText = @"SELECT recno() as masa , iif(lblocat,1,0) as blocat FROM " + fisMese ;
            //DataTable dm = new DataTable("mese");
            //dm.Load(oCmd.ExecuteReader());

            //for (int i = 0; i < dv.Rows.Count; i++)
            //{
            //    int tno;
            //    int.TryParse(dv.Rows[i]["ntable"].ToString() , out tno);
            //    dv.Rows[i]["blocat"]= ( dm.Rows[ tno-1 ]["blocat"].ToString() ) ;
            //}
            DataTable mese = Utile.LINQToDataTable(meseL);
            mese.TableName = "mese";
            return mese;
        }
        public DataTable getMeseLibere()
        {
            oCmd.CommandText = @"SELECT Ntable FROM " + fisVanzari + " GROUP BY Ntable WHERE ntable>0";
            DataTable dv = new DataTable("mese");
            dv.Load(oCmd.ExecuteReader());

            oCmd.CommandText = @"SELECT recno() as masa FROM " + fisMese;
            DataTable dm = new DataTable("mese");
            dm.Load(oCmd.ExecuteReader());

            for (int i = 0; i < dv.Rows.Count; i++)
            {
                int tno;
                int.TryParse(dv.Rows[i]["ntable"].ToString(), out tno);
                dm.Rows[tno - 1].Delete();
            }

            return dm;
        }
        public DataTable getProductsOfMasa(string codMasa)
        {
            oCmd.CommandText = @"SELECT cden,ncant,round(npret,2) as npret,round(npret*ncant,2) as valoare,detalii,fel,stare,uid FROM " + fisVanzari + " WHERE  ntable=" + codMasa.Replace("\"", "").Replace("'", "");
            DataTable dt = new DataTable("masa");
            dt.Load(oCmd.ExecuteReader());
            return dt;
        }
        public string getNbonOfMasa(string codMasa)
        {
            string nbon = "0";
            oCmd.CommandText = @"SELECT nbon FROM " + fisVanzari + " WHERE  ntable=" + codMasa.Replace("\"", "").Replace("'", "");
            DataTable dt = new DataTable("masa");
            dt.Load(oCmd.ExecuteReader());
            if (dt.Rows.Count > 0)
            {
                nbon = dt.Rows[0]["nbon"].ToString().Trim();
            }
            return nbon;
        }
        public DataTable getGest(bool getAll)
        {
            oCmd.CommandText = @"SELECT g.Cgest, g.Cdeng, g.calevsid, g.pretPeBon, g.numeimprim, g.coloaneimp, " +
                    "   ( SELECT count(*) FROM [" + fisCateg + "] c WHERE c.cgest=g.cgest) as nrcateg " +
                    " FROM [" + fisGest + "] g ";
            DataTable dt = new DataTable("gest");
            dt.Load(oCmd.ExecuteReader());
            if (!getAll) { dt = dt.Select("nrcateg>0", "cgest").CopyToDataTable(); }
            dt.TableName = "gest";
            return dt;
        }
        public DataTable getCateg()
        {
            oCmd.CommandText = @"SELECT cgest,ncod,cden, (select COUNT(*) FROM [" + fisProd + "] WHERE ncateg=categ.ncod AND lactiv ) as produse " +
                                " FROM " + fisCateg;
            DataTable dt = new DataTable("categ");
            dt.Load(oCmd.ExecuteReader());
            dt = dt.Select("produse>0", "ncod").CopyToDataTable();
            dt.Columns.Remove("produse");
            dt.TableName = "categ";
            return dt;
        }
        public DataTable getProd(tipGetprod tip)
        {
            //oCmd.CommandText = @"SELECT p.Ncod,p.Cden,round(p.Npv,2) as Npv,p.Ncateg,p.Lfractie,c.cgest,p.fel "+
            //                    " FROM " + fisProd + " as prod , "+fisCateg +
            //                    " as categ WHERE Lactiv and prod.ncateg=categ.ncod order by prod.ncod";
            String ct = "1" + DateTime.Now.ToString("HHmm");
            String vanzRap = " and (p.vanzrapida and between('" + ct + "', '1'+p.orastart, (iif(p.orastop>p.orastart,'1','2')) + p.orastop) )";

            oCmd.CommandText = @"SELECT p.nCod,p.cDen,round(p.nPv,2) as nPv,p.nCateg,iif(p.lFractie,1,0) as fra,c.cGest,p.Fel,p.obs,iif(p.favorite,1,0) as fav, iif(p.vanzRapida,1,0) as vrap" +
                                  " FROM [" + fisProd + "] p " +
                                        " JOIN [" + fisCateg + "] c ON p.nCateg=c.nCod " +
                                        " WHERE p.lActiv " + (tip.Equals(tipGetprod.vanzareRapida) ? vanzRap : "") +
                                  " ORDER BY p.nCod ";
            DataTable dt = new DataTable("prod");
            dt.Load(oCmd.ExecuteReader());
            return dt;
        }
        public DataTable getProd(string codp)
        {
            oCmd.CommandText = @"SELECT * " +
                                  " FROM [" + fisProd + "] p " +
                                  " WHERE ncod=" + codp.Trim() +
                                  " ORDER BY p.nCod ";
            DataTable dt = new DataTable("prod");
            dt.Load(oCmd.ExecuteReader());
            return dt;
        }
        public string getDataFiscalaWinPOS()
        {
            oCmd.CommandText = @"SELECT SUBSTR(DTOS(datafisc),1,4)+'-'+SUBSTR(DTOS(datafisc),5,2)+'-'+SUBSTR(DTOS(datafisc),7,2) FROM " + fisSetari;
            return (string)oCmd.ExecuteScalar();
        }
        public struct gestTXT
        {
            public int pozitii;
            public decimal total;
            public string bon;
            public int lastFel;
        }
        public String ncontor(String codGest)
        {
            oCmd.CommandText = @"UPDATE " + fisGest + " set ncontor=ncontor+1  where upper(allt(Cgest)) = '" + codGest.Trim().ToUpper() + "'";
            oCmd.ExecuteScalar();
            oCmd.CommandText = @"SELECT ncontor FROM " + fisGest + " where upper(allt(Cgest)) = '" + codGest.Trim().ToUpper() + "'";
            return oCmd.ExecuteScalar().ToString();
        }
        public DataTable getVanzariDeschise(string coduriGest)
        {
            // selectam vanzarile deschise, insumand cantitatea per uid  ( incluzand stornarile care au acelasi uid cu pozitia initiala) : obtinem o singura pozitie per UID
            // am sortat in ordine invers cronologica: ORDER BY tora DESC pentru afisare in mod stiva pe tableta : ultimele sus
            // - pastram tora primei pozitii ( marcarii pe masa ) : MIN(v.tora)
            // - luam ultima stare in care a fost : MAX(stare)
            oCmd.CommandText = @"SELECT uid, MAX(stare) as stare, v.ntable, v.ncodp, v.cden, sum(v.ncant) as ncant, detalii, v.fel, TTOC(MIN(v.tora),3) as tora, " +
                                    "   nvl(o.cnume,spac(30)) as nume_osp, v.cuser, spac(250) as stornari " +
                                    " FROM [" + fisVanzari + "] v " +
                                    "   LEFT JOIN [" + fisProd + "] p ON v.ncodp=p.ncod " +
                                    "   LEFT JOIN [" + fisCateg + "] c ON p.ncateg=c.ncod " +
                                    "   LEFT JOIN [" + fisPers + "] o ON upper(v.cuser)=upper(o.ccod) " +
                                    " WHERE !EMPTY(uid) AND !EMPTY(ncodp) " + (coduriGest.Trim().Length == 0 ? "" : " AND  allt(c.cgest)$'" + coduriGest + "'") +
                                    " ORDER BY tora DESC " +
                                    " GROUP BY uid";
            DataTable vanzDT = new DataTable("vanz");
            vanzDT.Load(oCmd.ExecuteReader());

            // selectam pozitiile care implica stornari (+,-)
            oCmd.CommandText = @"SELECT uid, ncant, stare, tora " +
                                " FROM [" + fisVanzari + "] " +
                                " WHERE !EMPTY(uid) and !EMPTY(ncodp) AND uid IN (SELECT DISTINCT uid FROM [" + fisVanzari + "] WHERE lstornat)";
            DataTable stornariDT = new DataTable();
            stornariDT.Load(oCmd.ExecuteReader());
            // compunem un string cu miscarile: +cant;-cant...
            foreach (DataRow vDR in vanzDT.Rows)
            {
                decimal uid = (decimal)vDR["uid"];
                string sir = "";
                foreach (DataRow sDR in stornariDT.Select("uid=" + uid))
                {
                    DateTime tora = DateTime.Parse(sDR["tora"].ToString());
                    string time = tora.ToString("HH:mm:ss") + ((tora.Date.DayOfYear == DateTime.Now.Date.DayOfYear) ? "" : tora.ToString(" (dd.MM.yyyy)"));
                    sir += (sir.Length > 0 ? "," : "") + "[" +
                        JsonConvert.SerializeObject(((decimal)sDR["ncant"]).ToString("+0.###;-0.###")) + "," +
                        JsonConvert.SerializeObject(time) + "," +
                        JsonConvert.SerializeObject(sDR["stare"].ToString()) +
                        "]";
                }
                if (sir.Length > 0)
                {
                    vDR["stornari"] = "[" + sir + "]";
                }
            }
            // stornarile au acelasi uid cu pozitia initiala
            //var stornoUID = from v in vanzDT.AsEnumerable()
            //                where v.Field<bool>("lstornat")
            //                group v by new { uid = v.Field<Decimal>("uid") } into gv
            //                select new
            //                {
            //                    uid = gv.Key.uid,
            //                    stornariString = string.Join(";", gv.Select(x => x.Field<decimal>("ncant").ToString("0.###").Trim())),
            //                };
            return vanzDT;
        }
        public DataTable getStariVanzari()
        {
            oCmd.CommandText = @"SELECT * " +
                                    " FROM [" + fisStariVanzari + "] v " +
                                    " WHERE activ ";
            DataTable dt = new DataTable("stari");
            dt.Load(oCmd.ExecuteReader());
            return dt;
        }
        public enum JV_Actiuni
        {
            JV_BON_SECTIE = 1,
            JV_NOTA_PLATA_SI_BON_FISCAL = 2,
            JV_NOTA_PROFORMA = 3,
            JV_REIMPRIMARE_NOTA_PLATA = 4,
            JV_REIMPRIMARE_BON_FISCAL = 5,
            JV_ACCES_PLATA = 6,
            JV_DO_VFP_CODE = 7
        }
        public string nextStareVanz(string _uid)
        {
            RezultatBSO rez = insertJSONVANZcallVanzariEXE($"nextStareVanz({_uid})", JV_Actiuni.JV_DO_VFP_CODE);
            if (!rez.succes)
            {
                return rez.mesaj;
            }
            // luam noua valoare
            oCmd = dbfConn.CreateCommand();
            oCmd.CommandText = @"SELECT stare FROM [" + fisVanzari + "] WHERE !lstornat AND uid = " + _uid;
            string newStare_ = oCmd.ExecuteScalar().ToString();
            return newStare_;


            // old version:
            decimal uid = -1;
            if (!decimal.TryParse(_uid, out uid)) { return "ERR: parametru UID invalid"; }
            string conditie = "!lstornat AND uid=" + uid;
            oCmd.CommandText = @"SELECT stare FROM [" + fisVanzari + "] WHERE "+conditie;
            object _oldStare = oCmd.ExecuteScalar();
            if (_oldStare == null)
            {
                // nu exista nicio vanzare nestornata, luam o stare de la stornate
                conditie = "uid=" + uid;
                oCmd.CommandText = @"SELECT stare FROM [" + fisVanzari + "] WHERE " + conditie;
                _oldStare = oCmd.ExecuteScalar();
            }
            if (_oldStare == null)
            {
                return "ERR: nu exista vanzarea cu UID=" + _uid;
            }
            decimal oldStare = (decimal)_oldStare;
            // luam urmatoarea stare
            decimal newStare = -1;
            DataTable dts = getStariVanzari();
            for (int i = 0; i < dts.Rows.Count; i++)
            {
                decimal snid = (decimal)dts.Rows[i]["nid"];
                if (snid == oldStare)
                {
                    //int j = ((i + 1) == dts.Rows.Count) ? 0 : (i + 1);
                    int j = (i + 1) % dts.Rows.Count;
                    newStare = (decimal)dts.Rows[j]["nid"];
                    // updatam 
                    //oCmd.CommandText = @"UPDATE [" + fisVanzari + "] SET stare=" + newStare.ToString() + " WHERE " + conditie;
                    insertJSONVANZcallVanzariEXE("UPDATE vanz SET stare=" + newStare.ToString() + " WHERE " + conditie, JV_Actiuni.JV_DO_VFP_CODE);
                    //int rowsAffected = oCmd.ExecuteNonQuery();
                }
            }
            // luam noua valoare
            oCmd.CommandText = @"SELECT stare FROM [" + fisVanzari + "] WHERE " + conditie;
            string newStareS = oCmd.ExecuteScalar().ToString();
            return newStareS;
        }
        public RezultatBSO insertJSONVANZ(string json, JV_Actiuni operatie)
        {
            RezultatBSO bso = new RezultatBSO();
            string vfpScript = @" 
            *** START ***
                SET EXCLUSIVE OFF 
                vcale='<vcale>'
                m.json='<vjson>'
                m.operatie = <voperatie>
                **
                err = ''
                ON ERROR err = err + MESSAGE()+'; '
                OPEN DATABASE (vcale) SHARED 
                IF !EMPTY(err)
                    RETURN 'ERR(-1): '+err 
                ENDIF
                ** nid-ul se genereaza la insert prin defaultValue = newId()
                USE jsonvanz SHARED
                INSERT INTO jsonvanz (json, operatie) values (m.json, m.operatie)
                newid = nid
                *
                CLOSE DATABASES ALL 
                ON ERROR
                IF !EMPTY(err)
                    RETURN 'ERR:(newid='+ALLT(padr(iif(type('newid')='U','NaN',newid),10))+'): '+err
                ENDIF
                RETURN newid
            *** END ***";
            vfpScript = vfpScript.Replace("<vcale>", this.fisDBC);
            vfpScript = vfpScript.Replace("<vjson>", json);
            vfpScript = vfpScript.Replace("<voperatie>", ((int)operatie).ToString());
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "ExecScript";
            oCmd.Parameters.Clear();
            oCmd.Parameters.AddWithValue("code", vfpScript);
            string rez = "";
            try
            {
                rez = oCmd.ExecuteScalar().ToString().Trim();
            }
            catch (Exception e)
            {
                bso.mesaj = "ERR: Nu s-a putut executa oleDB.commmad ( " + e.Message + " ) ";
            }
            oCmd = dbfConn.CreateCommand();                     // reset the command
            if (rez.ToUpper().StartsWith("ERR"))
            {
                bso.mesaj = "ERR: " + rez;
            }
            bso.succes = string.IsNullOrEmpty(bso.mesaj);
            if (bso.succes)
            {
                bso.obiect = long.Parse(rez.ToString());
                bso.mesaj = "OK";
            }
            return bso;
        }
        public RezultatBSO insertJSONVANZcallVanzariEXE(string json, JV_Actiuni operatie)
        {
            jurnal.scrie("--------------------------------------------------------");
            jurnal.scrie("insertJSONVANZcallVanzariEXE("+operatie.ToString()+") " + json);
            RezultatBSO rez = insertJSONVANZ(json, operatie);
            if (!rez.succes)
            {
                return rez;
            }
            long jvnid = (long)rez.obiect;

            // lansam vanzari.exe cu parametrul nid
            string xcp = Setari.calePOS.Trim();
            xcp = xcp.Substring(0, xcp.Length - (xcp.EndsWith(@"\") ? 5 : 4));     // eliminam DATE\ sau DATE
            String caleVanzariEXE = xcp + "vanzari.exe";

            long? exitCode = null;
            using (Process process = new Process())
            {
                process.StartInfo.FileName = caleVanzariEXE;
                process.StartInfo.Arguments = jvnid.ToString();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = xcp;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                StringBuilder output = new StringBuilder();
                StringBuilder error = new StringBuilder();

                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) => { if (e.Data == null) { outputWaitHandle.Set(); } else { output.AppendLine(e.Data); } };
                    process.ErrorDataReceived += (sender, e) => { if (e.Data == null) { errorWaitHandle.Set(); } else { error.AppendLine(e.Data); } };

                    process.Start();

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    int timeout = 10 * 1000;
                    if (process.WaitForExit(timeout) && outputWaitHandle.WaitOne(timeout) && errorWaitHandle.WaitOne(timeout))
                    {
                        // Process completed. Check process.ExitCode here.
                        exitCode = process.ExitCode;
                    }
                    else
                    {
                        // Timed out.
                        exitCode = -1000;
                        process.Kill();
                    }
                }
                if (output.Length > 0) { jurnal.scrie("Process.OUTPUT:" + output.ToString()); }
                if (error.Length > 0) { jurnal.scrie("Process.ERROR:" + error.ToString()); }
            }

            rez = new RezultatBSO();
            //string raspuns = "";
            if ((exitCode < 0 && exitCode >= -4) || (exitCode == -1000))
            {
                rez.obiect = exitCode;
                if (exitCode == -1000) { rez.mesaj = "ERR: -1000: Timed out!"; }
                // erori care nu ajung ca raspuns in JSNOVANZ.dbf:
                if (exitCode == -1) { rez.mesaj = "ERR: -1: Nu se poate accesa baza de date (SERVER-ul)!"; }
                if (exitCode == -2) { rez.mesaj = "ERR: -2: Eroare la deschiderea bazei de date."; }
                if (exitCode == -3) { rez.mesaj = "ERR: -3: NID-ul trimis(" + jvnid.ToString() + ") nu exista in JSNOVANZ"; }
            }
            else {
                // preluam raspunsul din jsonvanz
                oCmd = dbfConn.CreateCommand();
                oCmd.CommandText = @"SELECT raspuns,extrainfo " +
                                    " FROM [" + fisJSONVANZ + "]" +
                                    " WHERE nid=" + jvnid.ToString();
                DataTable dt = new DataTable();
                dt.Load(oCmd.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    rez.obiect = long.Parse(dt.Rows[0]["raspuns"].ToString());
                    rez.mesaj = dt.Rows[0]["extrainfo"].ToString().Trim();
                }
            }
            rez.succes = ((long)rez.obiect) >= 0;
            jurnal.scrie("saveVanzareJson: raspuns = " + rez.toText());
            return rez;
        }
        public string saveVanzareJson(string vanzare)
        {
            return insertJSONVANZcallVanzariEXE(vanzare, JV_Actiuni.JV_BON_SECTIE).mesaj;
        }

        public string saveVanzare(string vanzare)
        {
            jurnal.scrie("saveVanzare " + vanzare);
            
            JavaScriptSerializer js = new JavaScriptSerializer();
            string[][] strJSON;
            try
            {
                strJSON = js.Deserialize<string[][]>(vanzare);
            }
            catch (Exception e) { return "ERR: Eroare parsare vanzare (JSON): " + e.Message; }
            if (strJSON.Length < 2)
            {
                return "ERR: Eroare parsare vanzare: lungime json<2";
            }
            else
            {
                string codUser = strJSON[0][0];
                string codMasa = strJSON[0][1];
                string numeUser = this.getUser(codUser).Rows[0]["cnume"].ToString();

                oCmd.CommandText = @"SELECT nbon,nota " +
                                    " FROM " + fisVanzari +
                                    " WHERE cuser='" + codUser.Replace("\"", "").Replace("'", "") + "' and ntable=" + codMasa.Replace("\"", "").Replace("'", "");
                DataTable dt = new DataTable("nbon");
                dt.Load(oCmd.ExecuteReader());
                jurnal.scrie("saveVanzare " + oCmd.CommandText + " rows:" + dt.Rows.Count.ToString());
                if (dt.Rows.Count <= 0)
                {
                    return "ERR: Comanda NU a fost salvata. Nu mai exista nota de plata! ";
                }
                string nBon = dt.Rows[dt.Rows.Count - 1]["nbon"].ToString();
                string nNota = dt.Rows[dt.Rows.Count - 1]["nota"].ToString();
                DataTable feluri = this.getFeluriProduse();
                DataTable gestiuni = this.getGest(true);
                gestiuni.PrimaryKey = new DataColumn[] { gestiuni.Columns["cgest"] };
                if (gestiuni.Rows.Count > 0)
                {
                    gestTXT[] bonuri = new gestTXT[gestiuni.Rows.Count];
                    for (int i = 0; i < gestiuni.Rows.Count; i++)
                    {
                        bonuri[i].bon = (char)27 + newLine + gestiuni.Rows[i]["Cdeng"] + newLine + "Masa:" + codMasa.PadLeft(3) + " Ospatar: " + numeUser.PadLeft(10);
                        bonuri[i].bon += newLine + "Bon: " + ncontor(gestiuni.Rows[i]["Cgest"].ToString());
                        bonuri[i].bon += newLine + "- - - - - - - - - - - - - - - -";
                        bonuri[i].pozitii = 0;
                        bonuri[i].total = 0;
                        bonuri[i].lastFel = 0;
                    }
                    NumberStyles style;
                    CultureInfo culture;
                    style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
                    culture = CultureInfo.CreateSpecificCulture("en-GB");
                    DataTable produse = this.getProd(tipGetprod.toate);

                    // eliminam primul element din array (user,masa) pentru ca nu are acelasi nr. de elemente ca detaliile : pentru a putea sorta 
                    List<String[]> ls = strJSON.ToList();
                    ls.RemoveAt(0);
                    strJSON = ls.ToArray();
                    DataTable setari = this.getSetari();
                    bool useFeluri = false;
                    try { useFeluri = (bool)setari.Rows[0]["useFeluri"]; }
                    catch { }
                    useFeluri = useFeluri && (feluri.Rows.Count > 0);
                    int felColumn = 3;
                    if (useFeluri)
                    {
                        // ordonam produsele dupa fel
                        var sorted = strJSON.OrderBy(item => item[felColumn]);
                        strJSON = sorted.ToArray();
                    }

                    // parcurgem produsele
                    for (int i = 0; i < strJSON.Length; i++)
                    {
                        string ncodp = strJSON[i][0];
                        DataRow[] produs = produse.Select("ncod=" + ncodp);
                        if (produs.GetLength(0) > 0)
                        {
                            int indiceGest = gestiuni.Rows.IndexOf(gestiuni.Rows.Find(produs[0].Field<string>("cgest")));
                            string denumire = produs[0].Field<string>("Cden").Replace('"', ' ').Replace('\\', ' ').Replace('\'', ' ').Replace(',', ' ');
                            string pret = produs[0].Field<decimal>("Npv").ToString("f2").Replace(',', '.');

                            string ncant = strJSON[i][1];
                            string detalii = strJSON[i][2];
                            int nidFel = 0;
                            try { nidFel = int.Parse(strJSON[i][felColumn]); }
                            catch { }

                            // insert into vanz.dbf
                            oCmd.CommandText = @"INSERT into " + fisVanzari + " (cuser,ntable,nbon,NOTA,ncodp,cden,npret,ncant,tora,detalii,fel) values (" +
                                            "'" + codUser.ToUpper() + "'," + codMasa + "," + nBon + "," + nNota + "," + ncodp + "," + "'" + denumire + "'," + pret + "," + ncant + "," + "DATETIME( )" + ",'" + detalii + "'," + nidFel.ToString() + ")";
                            oCmd.ExecuteNonQuery();
                            
                            // o incercare de a lui george de a genera si vanz_uid la insert in vanz.dbf:
                            //insertIntoVanz(codUser.ToUpper(), codMasa, nBon, nNota, ncodp, denumire, pret, ncant, detalii, nidFel.ToString());

                            bonuri[indiceGest].pozitii += 1;
                            decimal vCant = 0;
                            decimal.TryParse(ncant, style, culture, out vCant);
                            decimal vPret = 0;
                            decimal.TryParse(pret, style, culture, out vPret);
                            decimal valpos = vCant * vPret;
                            bonuri[indiceGest].total += valpos;

                            //int colImp = (int)gestiuni.Rows[indiceGest]["coloaneimp"];
                            string bLinie = fontBold + newLine + (vCant.ToString((Math.Round(vCant) == vCant) ? "f0" : "f3") + " x " + denumire).Substring(0, 31) + fontNormal;
                            string pvLinie = newLine + ncant + " x " + pret + " = " + valpos.ToString("f2");
                            string detLinie = newLine + "(" + detalii.Trim() + ")";
                            string felLinie = newLine + newLine + fontBold + "*** FELUL: " + feluri.Rows[nidFel]["den"].ToString().Trim() + fontNormal + newLine;
                            if ((useFeluri) && (bonuri[indiceGest].lastFel != nidFel))
                            {
                                bonuri[indiceGest].bon += felLinie;
                                bonuri[indiceGest].lastFel = nidFel;
                            }

                            bonuri[indiceGest].bon += bLinie;
                            if ((bool)gestiuni.Rows[indiceGest]["pretPeBon"]) { bonuri[indiceGest].bon += pvLinie; }
                            if (detalii.Trim().Length > 0) { bonuri[indiceGest].bon += detLinie; }
                            bonuri[indiceGest].bon += newLine + "- - - - - - - - - - - - - - - -";

                            // verificam daca produsul trebuie sa apara si pe bonul altei gestiuni
                            string prodObs = (string)produs[0]["obs"];
                            if (prodObs.Contains("GEST:"))
                            {
                                int startPoz = prodObs.IndexOf("GEST:") + 5;
                                int endPoz = prodObs.IndexOf(".", startPoz);
                                string sirGest = "," + prodObs.Substring(startPoz, endPoz - startPoz) + ",";
                                for (int j = 0; j < gestiuni.Rows.Count; j++)
                                {
                                    if (sirGest.Contains("," + gestiuni.Rows[j]["cgest"].ToString().Trim() + ","))
                                    {
                                        bonuri[j].pozitii += 1;
                                        bonuri[j].total += valpos;
                                        if ((useFeluri) && (bonuri[j].lastFel != nidFel))
                                        {
                                            bonuri[j].bon += felLinie;
                                            bonuri[j].lastFel = nidFel;
                                        }
                                        bonuri[j].bon += bLinie;
                                        if ((bool)gestiuni.Rows[j]["pretPeBon"]) { bonuri[j].bon += pvLinie; }
                                        if (detalii.Trim().Length > 0) { bonuri[j].bon += detLinie; }
                                        bonuri[j].bon += newLine + "- - - - - - - - - - - - - - - -";
                                    }
                                }
                            }
                        }
                    }
                    // am salvat datele  , acum fac txt-uri din bonuri[k].bon pentru bonuri[k].pozitii>0 :
                    for (int k = 0; k < bonuri.Length; k++)
                    {
                        if (bonuri[k].pozitii > 0)
                        {
                            String numeUnic = codMasa + "_" + (DateTime.Now.ToBinary()).ToString() + "_" + k.ToString() + ".txt";
                            String numeFisier = gestiuni.Rows[k]["calevsid"].ToString() + @"\bon" + gestiuni.Rows[k]["cgest"].ToString().Trim() + numeUnic;
                            string tempfile = System.IO.Path.GetTempPath() + "\\" + numeUnic;
                            jurnal.scrie("saveVanzare tempfile" + tempfile);
                            
                            if ((bool)gestiuni.Rows[k]["pretPeBon"]) { bonuri[k].bon += newLine + "Total = " + bonuri[k].total.ToString("f2"); }
                            bonuri[k].bon += newLine + System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                            bonuri[k].bon += newLine + " .";
                            bonuri[k].bon += newLine + " .";
                            bonuri[k].bon += newLine + " .";
                            bonuri[k].bon += newLine + " .";
                            bonuri[k].bon += newLine + " .";
                            bonuri[k].bon += newLine + " .";
                            bonuri[k].bon += newLine + ".....................";
                            bonuri[k].bon += newLine + (char)29 + "V0";
                            if (gestiuni.Rows[k]["numeimprim"].ToString().Contains("DATECS")) { bonuri[k].bon += "\r" + (char)27 + (char)30 + (char)27 + (char)30 + (char)27 + (char)30; }
                            StreamWriter sw = new StreamWriter(tempfile);
                            sw.Write(bonuri[k].bon);
                            sw.Close();
                            try
                            {
                                File.Copy(tempfile, numeFisier);
                            }
                            catch (Exception e)
                            {
                                return "ERR: Eroare la trimiterea bonului spre imprimare: \n\r" + e.Message;
                            }
                        }
                    }
                }
                jurnal.scrie("saveVanzare-end : OK");
                return "OK";
            }
        }
        public string insertIntoVanz(string codUser, string codMasa, string nBon, string nNota, string ncodp, string denumire, string pret, string ncant, string detalii, string nidFel)
        {
            //jurnal.scrie("deschideMasa user=<" + user + "> masa=<" + masa + ">");
            string vfpScript = @" RETURN insertVanz()
            ***********************************************************************************************************
            FUNCTION insertVanz
                err=''
                ON ERROR err = iif(empty(err), 'ERR: ' ,err+'; ') + MESSAGE()
                **
                vcale   ='<vcale>'
                xtable  = <xtable>
                xuser   ='<xuser>'
                xbon    = <xbon>
                xnota   = <xnota>
                xcodp   = <xcodp>
                xden    ='<xden>'
                xpret   = <xpret>
                xcant   = <xcant>
                xdet    ='<xdet>'
                xfel    ='<xfel>'
                **
                OPEN DATABASE (vcale) shared
				select 0
				use vanz shared
				APPEND BLANK 
                REPLACE uid     WITH NEXTVALUE_V_UID()
				REPLACE tora    WITH DATETIME(),;
                        cuser   WITH xuser, ;
                        ntable  WITH xtable, ;
                        nbon    WITH xbon ,;
                        nota    WITH xnota,;
                        ncodp   WITH xcodp,;
                        cden    WITH xden,;
                        npret   WITH xpret,;
                        ncant   WITH xcant,;
                        detalii WITH xdet,;
                        fel     WITH xfel
						lstornat WITH .f.,;
                        nid     WITH xbon
                use in vanz
                CLOSE DATABASES ALL
                rez = iif(empty(err),'OK',err)
            RETURN rez
            ***********************************************************************************************************
            FUNCTION NEXTVALUE_V_UID
	            && PSEUDOGENERATOR PT NBON CARE SE BAZEAZA PE CERTITUDINEA CA E APELAT DOAR CU BAZA DE DATE DESCHISA
	            LOCAL XS,nextval
	            XS=SELECT()
	            SELECT 0
	            USE lastcod shared 
	            DO WHILE !FLOCK()
	            ENDDO
	            REPLACE vanz_uid WITH vanz_uid+1
	            nextval=vanz_uid
	            USE
	            SELECT (XS)
            RETURN nextval
            ***********************************************************************************************************
            ";
            vfpScript = vfpScript.Replace("<vcale>", fisDBC);
            vfpScript = vfpScript.Replace("<xtable>", codMasa.Replace('\'', ' ').Replace('"', ' '));
            vfpScript = vfpScript.Replace("<xuser>", codUser);
            vfpScript = vfpScript.Replace("<xbon>", nBon);
            vfpScript = vfpScript.Replace("<xnota>", nNota);
            vfpScript = vfpScript.Replace("<xcodp>", ncodp);
            vfpScript = vfpScript.Replace("<xden>", denumire);
            vfpScript = vfpScript.Replace("<xpret>", pret);
            vfpScript = vfpScript.Replace("<xcant>", ncant);
            vfpScript = vfpScript.Replace("<xdet>", detalii);
            vfpScript = vfpScript.Replace("<xfel>", nidFel);

            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "ExecScript";
            oCmd.Parameters.Clear();
            oCmd.Parameters.AddWithValue("code", vfpScript);
            string rez = "ERR: Comanda <insertVanz> nu a putut fi executata";
            try
            {
                rez = oCmd.ExecuteScalar().ToString();
            }
            catch (Exception e) { rez += e.ToString(); jurnal.scrie("EROARE-insertVanz: " + rez); }
            oCmd = dbfConn.CreateCommand();                     // reset the command
            return rez;
        }
        public bool nota(int masa, string _camera, string _nc, string _lct, string _lt, string _ot, string _scv, string _mwp, double numerar, double card, double ecash)
        {
            string pars = "masa="+masa.ToString() + ", camera=" + _camera + ", nc=" + _nc + ", lct=" + _lct + ", lt=" + _lt + ", ot=" + _ot + ", scv=" + _scv + ", mwp=" + _mwp + ", numerar=" + numerar.ToString() + ", card=" + card.ToString() + ", ecash=" + ecash.ToString();
            jurnal.scrie("nota-start (" + pars + ")");

            _camera = _camera.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim();  _camera = "'" + _camera + "'";
            _nc = _nc.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _nc = "'" + _nc + "'";
            _lct = _lct.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _lct = "'" + _lct + "'";
            _lt = _lt.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _lt = "'" + _lt + "'";
            _ot = _ot.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _ot = "'" + _ot + "'";
            _scv = _scv.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _scv = "'" + _scv + "'";
            _mwp = _mwp.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _mwp = "'" + _mwp + "'";
            string ntipplata="1";
            if (numerar + card + ecash == numerar) 
            { 
                ntipplata = "1"; 
            }
            else 
            {
                if (numerar + card + ecash == card)
                {
                    ntipplata = "3";
                }
                else
                {
                    if (numerar + card + ecash == ecash)
                    {
                        ntipplata = "7";
                    }
                    else
                    {
                        ntipplata = "0";
                    }
                }
            }
            string sNumerar=numerar.ToString().Replace(',','.');
            string sCard = card.ToString().Replace(',', '.');
            string sEcash = ecash.ToString().Replace(',', '.');
            oCmd.CommandText = @"UPDATE mese SET lblocat=.t. , _camera="+_camera+" , _nc="+_nc+" , _lct="+_lct+" , _lt="+
                               _lt+" , _ot="+_ot+" , _scv="+_scv+" , _mwp="+_mwp+",ntipplata="+ntipplata+" , plata1="+sNumerar+
                               " , plata3=" + sCard + " , plata7=" + sEcash + " where recno()=" + masa.ToString();
            int hm = oCmd.ExecuteNonQuery();
            jurnal.scrie("nota-end : mese: records updated=" + hm.ToString());
            return (hm==1);
        }
        public string deschideMasa(string user , string masa)
        {
            jurnal.scrie("deschideMasa user=<" + user + "> masa=<" + masa + ">");
            string vfpScript = @" RETURN deschideMasa()
            ***********************************************************************************************************
            FUNCTION deschideMasa
                vcale='<vcale>'
                xnrmasa=<masa>
                vcoduser='<user>'
                **
                rez='ERR:'
                ON ERROR err = err + MESSAGE()
                OPEN DATABASE (vcale) shared
				select 0
				use mese shared
				go xnrmasa
				retx=0
				locked=.f.
				DO WHILE !locked and retx<3
					locked=LOCK()
					retx=retx+1
				ENDDO
				if locked				  
					v_nrbon=NEXTVALUE_V_NRBON()
					SELECT 0
					USE VANZ SHARED 
					LOCATE FOR ntable=xnrmasa 
					IF FOUND()
                        if !allt(cuser)==vcoduser
                            rez='ERR: Masa '+ALLTRIM(STR(xnrmasa,4))+' a fost deschisa de alt utilizator.'
                        else
                            rez='ERR: Masa '+ALLTRIM(STR(xnrmasa,4))+' e deja deschisa.'
                        endif
					ELSE  
						APPEND BLANK 
						REPLACE cuser WITH vcoduser,ntable WITH xnrmasa,tora WITH DATETIME(),nbon WITH v_nrbon ,;
							lstornat WITH .f.,nid with v_nrbon, nota WITH 1
                        rez='OK'
					ENDIF
                    use in vanz
				else
					rez='ERR: Masa '+ALLTRIM(STR(xnrmasa,4))+' e in curs de deschidere de catre alt utilizator.'
				endif
                use in mese
                CLOSE DATABASES all 
            RETURN rez
            ***********************************************************************************************************
            FUNCTION NEXTVALUE_V_NRBON
	            && PSEUDOGENERATOR PT NBON CARE SE BAZEAZA PE CERTITUDINEA CA E APELAT DOAR CU BAZA DE DATE DESCHISA
	            LOCAL XS,Lnrbon
	            XS=SELECT()
	            SELECT 0
	            USE setari
	            DO WHILE !FLOCK()
	            ENDDO
	            replace nbon WITH nbon+1
	            lNRBON=nbon
	            USE
	            SELECT (XS)
            RETURN lnrbon
            ***********************************************************************************************************
            ";
            vfpScript = vfpScript.Replace("<vcale>", fisDBC);
            vfpScript = vfpScript.Replace("<masa>", masa.Replace('\'', ' ').Replace('"', ' '));
            vfpScript = vfpScript.Replace("<user>", user.ToUpper());
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "ExecScript";
            oCmd.Parameters.Clear();
            oCmd.Parameters.AddWithValue("code", vfpScript);
            string rez="ERR: Comanda <deschideMasa> nu a putut fi executata";
            try
            {
                rez = oCmd.ExecuteScalar().ToString();
            }
            catch (Exception e) { jurnal.scrie("EROARE-deschideMasa: " + e.Message); }
            oCmd = dbfConn.CreateCommand();                     // reset the command
            return rez;
        }
        public bool deschideMasa_old(string user , string masa)
        {
            jurnal.scrie("deschideMasa user=<" + user + "> masa=<"+masa+">");
            oCmd.CommandText = @"SELECT count(*) as aparitii FROM " + fisVanzari + " where ntable="+masa.Replace('\'',' ').Replace('"',' ');
            DataTable dv = new DataTable("vanzari");
            dv.Load(oCmd.ExecuteReader());
            int ap;
            int.TryParse(dv.Rows[0]["aparitii"].ToString(), out ap);
            if (ap > 0)
            {
                return false;
            }
            else
            {
                oCmd.CommandText = @"update  " + fisSetari + " set nbon=nbon+1";
                oCmd.ExecuteNonQuery();
                oCmd.CommandText = @"select nbon from " + fisSetari ;
                DataTable db = new DataTable("setari");
                db.Load(oCmd.ExecuteReader());
                int nbon;
                int.TryParse(db.Rows[0]["nbon"].ToString(), out nbon);
                oCmd.CommandText = @"INSERT into " + fisVanzari + " (tora,cuser,ntable,nbon,nid,lstornat,nota) values ( " +
                                "DATETIME( ) , '" + user.ToUpper() + "'," + masa + "," + nbon.ToString() + "," + nbon.ToString()+ ", .f., 1)";
                oCmd.ExecuteNonQuery();                
                return true;
            }
        }
        public RezultatBSO elibereazaMasa(string masa) 
        {
            jurnal.scrie("elibereazaMasa (masa=" + masa + ")");

            RezultatBSO rez = new RezultatBSO();
            oCmd.CommandText = @"SELECT sum(ncant*npret) as total FROM " + fisVanzari + " where ntable=" + masa.Replace('\'', ' ').Replace('"', ' ');
            DataTable dv = new DataTable("valmasa");
            dv.Load (oCmd.ExecuteReader());
            if ( (dv.Rows.Count == 0) || (dv.Rows[0]["total"].GetType().Name.Contains("DBNull") ) )
            {
                rez.succes = false;
                rez.mesaj = "Masa " + masa + " nu mai este deschisa!";
                return rez ;
            }
            decimal valoare = (decimal)dv.Rows[0]["total"];
            if (valoare != 0)
            {
                rez.succes = false;
                rez.mesaj = "Masa " + masa + " nu poate fi eliberata: contine produse.";
                return rez;
            }
            else 
            {
                oCmd.CommandText = @"SELECT * from " + fisVanzari + " where ntable=" + masa.Replace('\'', ' ').Replace('"', ' ')+" and ncodp!=0 ";
                DataTable dt = new DataTable("vanzare");
                dt.Load(oCmd.ExecuteReader());
                oCmd.CommandText = @"delete from " + fisVanzari + " where ntable=" + masa.Replace('\'', ' ').Replace('"', ' ');
                oCmd.ExecuteNonQuery();
                oCmd.CommandText = @"select dtoc(datafisc) as datafisc, npos from " +fisSetari ;
                DataTable ds = new DataTable("setari");
                ds.Load(oCmd.ExecuteReader());
                string datafisc = "{" + ds.Rows[0]["datafisc"].ToString() + "}";
                string npos = ds.Rows[0]["npos"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    oCmd.CommandText = @"insert into " + fisIstoric + "(DDATAVANZ,TORA,CUSER,NTABLE,NBON,NCODP,NPRET,NCANT,LSTORNAT,NDOCINT,CDEN,POS,NTIPPLATA) values (" +
                    datafisc + " , " +
                    " datetime() " + " , " +
                    "'" + dt.Rows[i]["cuser"].ToString() + "' , " +
                    dt.Rows[i]["ntable"].ToString() + " , " +
                    dt.Rows[i]["nbon"].ToString() + " , " +
                    dt.Rows[i]["ncodp"].ToString() + " , " +
                    dt.Rows[i]["npret"].ToString().Replace(',', '.') + " , " +
                    dt.Rows[i]["ncant"].ToString().Replace(',', '.') + " , " +
                    ((bool)dt.Rows[i]["lstornat"] ? ".t." : ".f.") + " , " +
                    dt.Rows[i]["ndocint"].ToString() + " , " +
                    "'" + dt.Rows[i]["cden"].ToString().Replace('\'', ' ').Replace('"', ' ') + "' , " +
                    npos + " , " +
                    "1" +
                    ")";
                    oCmd.ExecuteNonQuery();
                }
            }
            rez.succes = true;
            jurnal.scrie("elibereazaMasa-end (rez=" + rez.ToString() + ")");
            return rez;
        }
        public bool getSetareCardvaloric()
        {
            oCmd.CommandText = @"SELECT cardvaloric FROM " + fisSetari ;
            DataTable dv = new DataTable("setari");
            dv.Load(oCmd.ExecuteReader());
            bool cv;
            bool.TryParse(dv.Rows[0]["cardvaloric"].ToString(), out cv);
            return cv;
        }
        public DataTable getFeluriProduse()
        {
            oCmd.CommandText = @"SELECT recn() as nid,den FROM " + fisFeluri;
            DataTable dt = new DataTable("feluri");
            dt.Load(oCmd.ExecuteReader());
            DataRow fel0 = dt.NewRow();
            fel0["nid"] = 0;
            fel0["den"] = " ";
            dt.Rows.InsertAt(fel0, 0);
            return dt;
        }
        public string getLastUpProd()
        {
            oCmd.CommandText = @"SELECT allt(padl(lastUpProd,10)) as lastUpProd FROM [" + fisSetari + "]";
            return (string)oCmd.ExecuteScalar();
        }
        public string getValOfCotaTva(string cotatva)
        {
            oCmd.CommandText = @"SELECT valoare " +
                                " FROM  [" + fisCoteTva + "] " +
                                " WHERE allt(cota)=='" + cotatva.ToUpper().Trim() + "'";
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            if ((dt==null) || (dt.Rows.Count==0)){
                return "";
            }
            return dt.Rows[0]["valoare"].ToString();
        }
        //
        public string AccesGetTip( string tipAcces )
        {
            oCmd.CommandText = @"SELECT allt(den) as den FROM [" + fisTipProd + "] where tip=" + tipAcces;
            return (string)oCmd.ExecuteScalar();
        }
        public DataTable AccesGetProdus(string tipAcces, WS.TipTagClient tipTag)
        {
            oCmd.CommandText = @"SELECT 0 as succes, spac(30) as camera, spac(50) as numeclient, 0 as tip_tag, 000000000.00 as sold, ncod, cden, npv " +
                                " FROM  [" + fisProd + "] p " +
                                " WHERE p.tip=" + tipAcces + " and p.lactiv and " + (tipTag == WS.TipTagClient.Adult ? "p.jetoanea>0" : "p.jetoanec>0");
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            dt.TableName = "acces";
            return dt;
        }
        public long genNrBon()
        {
            jurnal.scrie("genNrBon-start");
            string vfpScript = @" 
            **
            SET EXCL OFF
            CLOSE ALL
            vcale='<vcale>'
            **
            v_nrbon = -1
            err = ''
            ON ERROR err = err + MESSAGE()
            OPEN DATABASE (vcale) SHARED
            SELECT 1
	        USE setari SHARED
	        DO WHILE !FLOCK()
	        ENDDO
	        REPLACE nbon WITH nbon+1
	        v_nrbon = nbon 
            CLOSE DATABASES all 
            ON ERROR
            IF !EMPTY(err)
                RETURN ALLT(STR(v_nrbon))+' : '+err
            ENDIF
            RETURN v_nrbon
            **";
            vfpScript = vfpScript.Replace("<vcale>", fisDBC);
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "ExecScript";
            oCmd.Parameters.Clear();
            oCmd.Parameters.AddWithValue("code", vfpScript);
            string sBon = "-1";
            try
            {
                sBon = oCmd.ExecuteScalar().ToString();
            }
            catch (Exception e) { jurnal.scrie("EROARE-genNrBon: " + e.Message); }
            // resetam comanda 
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Clear();
            oCmd = dbfConn.CreateCommand();                     // reset the command

            long nBon = -1;
            long.TryParse(sBon, out nBon);
            jurnal.scrie("genNrBon-end: nbon=" + sBon + " -> " + nBon.ToString());
            return nBon;
        }
        public bool AccesSave (string datafisc, string codUser, string nBon, DataRow drProd, string cant, string pret, string suma)
        {
            DateTime datavanz = DateTime.Now.Date;
            DateTime.TryParse(datafisc, out datavanz);
            cant = cant.Replace(",", ".");
            pret = pret.Replace(",", ".");
            suma = suma.Replace(",", ".");
            string codp = drProd["ncod"].ToString();
            string denumire = drProd["cden"].ToString().TrimEnd();
            string jetoanea = drProd["jetoanea"].ToString();
            string jetoanec = drProd["jetoanec"].ToString();
            string tva = getValOfCotaTva(drProd["cotatva"].ToString()).Replace(",", ".");
            string masa = "1";
            string pos = "1";
            string tipplata = "7";

            oCmd.CommandText = @"INSERT INTO [" + fisIstoric + "] (ddatavanz,tora,cuser,ntable,nbon,ncodp,npret,ncant,cden,pos,ntipplata,plata7,pretintreg,jetoanea,jetoanec,ntva) " +
                " values (" + datavanz.ToString("{^yyyy-MM-dd}") + "," + "DATETIME()" + ",'" + codUser.ToUpper() + "'," + masa + "," + nBon + "," + codp + "," + pret + "," + cant +
                ",'" + denumire + "'," + pos + "," + tipplata + "," + suma + "," + pret + "," + jetoanea + "," + jetoanec + "," + tva + ")";
            int rezins = oCmd.ExecuteNonQuery();
            jurnal.scrie("AccesSave: istoric=" + rezins.ToString() + " insert");
            if (rezins != 1)
            {
                jurnal.eroare("AccesSave: " + oCmd.CommandText);
                jurnal.eroare("AccesSave: ExecuteNonQuery=" + rezins.ToString());
            }
            return rezins == 1;
        }
        public RezultatBSO AccesGetCount(string data, string tipAcces)
        {
            RezultatBSO rez = new RezultatBSO();
            rez.mesaj = "";
            DateTime datavanz = DateTime.Now.Date;
            DateTime.TryParse(data, out datavanz);
            oCmd.CommandText = @"SELECT p.tip, sum(i.jetoanea) as adulti, sum(i.jetoanec) as copii " +
                                " FROM [" + fisIstoric + "] i " +
                                "    LEFT JOIN [" + fisProd + "] p ON i.ncodp=p.ncod " +
                                "    LEFT JOIN [" + fisTipProd + "] t ON p.tip=t.tip " +
                                " WHERE p.tip=" + tipAcces + " AND (i.jetoanea + i.jetoanec)#0 AND i.ddatavanz=" + datavanz.ToString("{^yyyy-MM-dd}") +
                                " GROUP BY p.tip ";
            Debug.WriteLine("------- AccesGetCount: ");
            Debug.WriteLine(oCmd.CommandText);
            DataTable dt = new DataTable();
            try
            {
                dt.Load(oCmd.ExecuteReader());
                dt.TableName = "acces";
                rez.obiect = dt;
                rez.succes = true;
            }
            catch (Exception e) { rez.succes = false; rez.mesaj = e.Message; jurnal.scrie("Eroare - AccesGetCount: " + e.ToString()); }
            return rez;
        }
    }

    public class EPay : IDisposable
    {
        FirebirdSql.Data.FirebirdClient.FbConnection fbconexiune = new FirebirdSql.Data.FirebirdClient.FbConnection();
        string mesajErr = "";
        /* IDisposable */
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed resources                    
                }
                // Call the appropriate methods to clean up unmanaged resources here
                firebird.deconectareBD(fbconexiune);
                disposed = true;
            }
        }
        ~EPay()
        {
            Dispose(false);            
        }
        /* END IDisposable */
        public EPay()
        {
            firebird.conectareBD(fbconexiune, Setari.caleEcashFDB, Setari.userFirebird, Setari.passFirebird, ref mesajErr);
        }

        public RezultatBSO epay(string tag, string suma, string pin, string data, string nrdoc, string detalii, string sursa, string utilizator)
        {
            String sales1 = "0.00", sales2 = "0.00", sales3 = "0.00", sales4 = "0.00";
            string comanda = @"select * from SP_WRAP_TRANZACTIE('" + tag.Trim() + "'," +
                                                 "-"+suma.Replace(",", ".").Trim() + "," +
                                                 sales1.Replace(",", ".").Trim() + "," +
                                                 sales2.Replace(",", ".").Trim() + "," +
                                                 sales3.Replace(",", ".").Trim() + "," +
                                                 sales4.Replace(",", ".").Trim() + "," +
                                                 pin.Trim() + "," +
                                                 "'" + data + "'," +
                                                  "'" + nrdoc + "'," +
                                                 "'" + detalii.Trim() + "'," +
                                                 "'" + sursa.Trim() + "'," +
                                                 "'" + utilizator.Trim() + "',2)";
            RezultatBSO rez = firebird.ExecutaSQL(fbconexiune, comanda, firebird.tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            if (rez.obiect != null)
            {
                ((DataTable)rez.obiect).TableName = "plata";
            }
            //DataTable dt = (DataTable)rez.obiect;
            //if (dt != null) { dt.TableName = "plata"; }
            return rez;
        }
        public RezultatBSO epay_old_versiune_4(string tag, string suma, string pin, string data, string nrdoc, string detalii, string sursa, string utilizator)
        {
            string comanda = @"select * from SP_WRAP_TRANZACTIE('" + tag.Trim() + "'," +
                                                 "-" + suma.Replace(",", ".").Trim() + "," +
                                                 pin.Trim() + "," +
                                                 "'" + data + "'," +
                                                  "'" + nrdoc + "'," +
                                                 "'" + detalii.Trim() + "'," +
                                                 "'" + sursa.Trim() + "'," +
                                                 "'" + utilizator.Trim() + "',2)";
            RezultatBSO rez = firebird.ExecutaSQL(fbconexiune, comanda, firebird.tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            if (rez.obiect != null)
            {
                ((DataTable)rez.obiect).TableName = "plata";
            }
            //DataTable dt = (DataTable)rez.obiect;
            //if (dt != null) { dt.TableName = "plata"; }
            return rez;
        }

        public DataTable realizariExtern(string nidtableta, string dela, string panala)
        {
            jurnal.scrie(System.DateTime.Now.ToString() + " " + "nidatableta=" + nidtableta.ToString() + "; dela=" + dela.ToString() + "; panala=" + panala.ToString());
            string comanda = "";
            comanda = @"SELECT user_extern,-sum(suma) as vanzari, utilizatori.username as nume  " +
                        " FROM miscari INNER JOIN utilizatori ON miscari.user_extern=CASR(utilizatori.nid as varchar(10)) " +
                        " WHERE nidtableta in (SELECT tablete.nid FROM tablete WHERE tablete.nidextern=(SELECT nidextern FROM tablete WHERE tablete.nid= " + nidtableta.Replace(',', ' ') + ")) " +
                                   " AND datafiscala>='" + dela + "' AND datafiscala<='" + panala + "' " +
                        " GROUP BY user_extern,nume";
            RezultatBSO rez=firebird.ExecutaSQL(fbconexiune, comanda, firebird.tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            DataTable dt = (DataTable)rez.obiect;
            if (dt != null)
            {
                dt.TableName = "realizari";
            }
            return dt;
        }

        public DataTable externi(int nid)
        {
            string comanda = @"SELECT e.denumire as denextern , e.datafiscala , t.denumire as dentableta, e.nid as nidextern "+
                                " FROM externi e, tablete t"+
                                " WHERE e.nid = t.nidextern and t.nid=" + nid.ToString();
            RezultatBSO rez = firebird.ExecutaSQL(fbconexiune, comanda, firebird.tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            DataTable dt = (DataTable)rez.obiect;
            if (dt != null) { dt.TableName = "externi"; }
            return dt;
        }

        public string inchidereZi(int nidTableta) 
        {
            string comanda = @" update externi set datafiscala=dateadd(day,1,datafiscala) where nid = ( select nidextern from tablete where nid=" + nidTableta.ToString() + ") returning datafiscala  ";
            RezultatBSO rez = firebird.ExecutaSQL(fbconexiune, comanda, firebird.tipexecutie.FbCommand_ExecuteScalar, "", false, "Eroare");
            if (rez.obiect == null)
            {
                return "ERR : "+rez.mesaj;
            }
            else
            {
                return ((DateTime)rez.obiect).ToString("yyyy-MM-dd");
            }
        }

        public DataTable versiune(string par) 
        {
            if (!Setari.tokenInSetariIni)
            {
                string comanda = @"select versiune,data,explicatii from versiunedb ";
                if (par.CompareTo("token") == 0) { comanda = @"select * from versiunedb "; }
                RezultatBSO rez = firebird.ExecutaSQL(fbconexiune, comanda, firebird.tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
                DataTable dt = (DataTable)rez.obiect;
                if (dt != null)
                {
                    dt.TableName = "versiunedb";
                }
                return dt;
            }
            else
            {
                DataTable dt = new DataTable("versiunedb");
                dt.Columns.Add("X", typeof(String));
                DataRow r0 = dt.NewRow();
                r0["X"] = Setari.token;
                dt.Rows.InsertAt(r0, 0);
                return dt;
            }        
        }

        public string getToken()
        {
            string comanda = @"select X from versiunedb ";
            RezultatBSO rez = firebird.ExecutaSQL(fbconexiune, comanda, firebird.tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            DataTable dt = (DataTable)rez.obiect;
            if (dt != null) { dt.TableName = "versiunedb"; }
            return dt.Rows[0]["X"].ToString();
        }

        public DataTable getUserExtern(string parola) {
            string comanda = @"select nid,username, nidextern, operator, administrator, blocat from utilizatori where upper(parola)='" + parola.ToUpper().Trim().Replace("\"", "").Replace("'", "") + "'";
            RezultatBSO rez = firebird.ExecutaSQL(fbconexiune, comanda, firebird.tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            DataTable dt = (DataTable)rez.obiect;
            if (dt != null) { dt.TableName = "utilizatori"; }
            return dt;
        }
    }

    public static class DataTable2XmlDocument
    {
        static public XmlDocument convert(DataTable tabela, String mesajEroare)
        {
            XmlDocument result = new XmlDocument();
            using (StringWriter sw = new StringWriter())
            {
                if (tabela == null)
                {
                    String mE = mesajEroare.Replace('\\', ' ').Replace('\'', ' ').Replace('"', ' ').Replace('<', ' ').Replace('>', ' ');
                    result.LoadXml("<DocumentElement><EROAREFATALA><MESAJ>" + mE + "</MESAJ></EROAREFATALA></DocumentElement>");
                }
                else
                {
                    tabela.WriteXml(sw);
                    result.LoadXml(sw.ToString());
                }
            }
            return result;
        }
    }

    public static class Utile
    {
        public static bool isTrue(object val)
        {
            return (val.ToString().Equals("1") || val.ToString().ToUpper().Equals("TRUE"));
        }
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}