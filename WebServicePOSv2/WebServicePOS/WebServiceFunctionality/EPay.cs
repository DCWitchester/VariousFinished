using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebServicePOS.Firebird;
using WebServicePOS.Setari;

namespace WebServicePOS.WebServiceFunctionality
{
    public class EPay : IDisposable
    {
        FirebirdSql.Data.FirebirdClient.FbConnection fbconexiune = new FirebirdSql.Data.FirebirdClient.FbConnection();
        readonly FirebirdConnectionClass FirebirdConnection = new FirebirdConnectionClass();
        readonly string MesajErr = "";

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
                FirebirdConnection.DeconectareBD(fbconexiune);
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
            FirebirdConnection.ConectareBD(fbconexiune, Settings.caleEcashFDB, Settings.userFirebird, Settings.passFirebird, ref MesajErr);
        }

        public RezultatBSO Epay(string tag, string suma, string pin, string data, string nrdoc, string detalii, string sursa, string utilizator)
        {
            if (Settings.caleEcashFDB.ToUpper().Contains("mentorh".ToUpper()))
            {
                return Epay_Mentorhotel(tag, suma, pin, data, nrdoc, detalii, sursa, utilizator);
            }
            String sales1 = "0.00", sales2 = "0.00", sales3 = "0.00", sales4 = "0.00";
            string comanda = @"select * from SP_WRAP_TRANZACTIE('" + tag.Trim() + "'," +
                                                 "-" + suma.Replace(",", ".").Trim() + "," +
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
            RezultatBSO rez = FirebirdConnection.ExecutaSQL(fbconexiune, comanda, FirebirdConnectionClass.Tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            if (rez.obiect != null)
            {
                ((DataTable)rez.obiect).TableName = "plata";
            }
            //DataTable dt = (DataTable)rez.obiect;
            //if (dt != null) { dt.TableName = "plata"; }
            return rez;
        }
        public RezultatBSO Epay_Old_Versiune_4(string tag, string suma, string pin, string data, string nrdoc, string detalii, string sursa, string utilizator)
        {
            string comanda = @"select * from SP_WRAP_TRANZACTIE('" + tag.Trim() + "'," +
                                                 "-" + suma.Replace(",", ".").Trim() + "," +
                                                 pin.Trim() + "," +
                                                 "'" + data + "'," +
                                                  "'" + nrdoc + "'," +
                                                 "'" + detalii.Trim() + "'," +
                                                 "'" + sursa.Trim() + "'," +
                                                 "'" + utilizator.Trim() + "',2)";
            RezultatBSO rez = FirebirdConnection.ExecutaSQL(fbconexiune, comanda, FirebirdConnectionClass.Tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            if (rez.obiect != null)
            {
                ((DataTable)rez.obiect).TableName = "plata";
            }
            //DataTable dt = (DataTable)rez.obiect;
            //if (dt != null) { dt.TableName = "plata"; }
            return rez;
        }

        /// <summary>
        /// metoda noua comunicare cu mentorh.fdb
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="suma"></param>
        /// <param name="pin"></param>
        /// <param name="data"></param>
        /// <param name="nrdoc"></param>
        /// <param name="detalii"></param>
        /// <param name="sursa"></param>
        /// <param name="utilizator"></param>
        /// <returns></returns>
        public RezultatBSO Epay_Mentorhotel(string tag, string suma, string pin, string data, string nrdoc, string detalii, string sursa, string utilizator)
        {
            string comanda = @"select * from SP_PLATABONURIVALORICE('" + tag.Trim() + "'," +
                                                 "-" + suma.Replace(",", ".").Trim() + "," +
                                                 pin.Trim() + "," +
                                                 "'" + data + "'," +
                                                  "'" + nrdoc + "'," +
                                                 "'" + detalii.Trim() + "'," +
                                                 "'" + sursa.Trim() + "'," +
                                                 " -10 ";
            RezultatBSO rez = FirebirdConnection.ExecutaSQL(fbconexiune, comanda, FirebirdConnectionClass.Tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            if (rez.obiect != null)
            {
                ((DataTable)rez.obiect).TableName = "plata";
            }
            //DataTable dt = (DataTable)rez.obiect;
            //if (dt != null) { dt.TableName = "plata"; }
            return rez;
        }

        public DataTable RealizariExtern(string nidtableta, string dela, string panala)
        {
            Jurnal.Write(System.DateTime.Now.ToString() + " " + "nidatableta=" + nidtableta.ToString() + "; dela=" + dela.ToString() + "; panala=" + panala.ToString());
            string comanda = @"SELECT user_extern,-sum(suma) as vanzari, utilizatori.username as nume  " +
            " FROM miscari INNER JOIN utilizatori ON miscari.user_extern=CASR(utilizatori.nid as varchar(10)) " +
            " WHERE nidtableta in (SELECT tablete.nid FROM tablete WHERE tablete.nidextern=(SELECT nidextern FROM tablete WHERE tablete.nid= " + nidtableta.Replace(',', ' ') + ")) " +
                       " AND datafiscala>='" + dela + "' AND datafiscala<='" + panala + "' " +
            " GROUP BY user_extern,nume";
            RezultatBSO rez = FirebirdConnection.ExecutaSQL(fbconexiune, comanda, FirebirdConnectionClass.Tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            DataTable dt = (DataTable)rez.obiect;
            if (dt != null)
            {
                dt.TableName = "realizari";
            }
            return dt;
        }

        public DataTable Externi(int nid)
        {
            string comanda = @"SELECT e.denumire as denextern , e.datafiscala , t.denumire as dentableta, e.nid as nidextern " +
                                " FROM externi e, tablete t" +
                                " WHERE e.nid = t.nidextern and t.nid=" + nid.ToString();
            RezultatBSO rez = FirebirdConnection.ExecutaSQL(fbconexiune, comanda, FirebirdConnectionClass.Tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            DataTable dt = (DataTable)rez.obiect;
            if (dt != null) { dt.TableName = "externi"; }
            return dt;
        }

        public string InchidereZi(int nidTableta)
        {
            string comanda = @" update externi set datafiscala=dateadd(day,1,datafiscala) where nid = ( select nidextern from tablete where nid=" + nidTableta.ToString() + ") returning datafiscala  ";
            RezultatBSO rez = FirebirdConnection.ExecutaSQL(fbconexiune, comanda, FirebirdConnectionClass.Tipexecutie.FbCommand_ExecuteScalar, "", false, "Eroare");
            if (rez.obiect == null)
            {
                return "ERR : " + rez.mesaj;
            }
            else
            {
                return ((DateTime)rez.obiect).ToString("yyyy-MM-dd");
            }
        }

        public DataTable Versiune(string par)
        {
            if (!Settings.tokenInSettingsIni)
            {
                string comanda = @"select versiune,data,explicatii from versiunedb ";
                if (par.CompareTo("token") == 0) { comanda = @"select * from versiunedb "; }
                RezultatBSO rez = FirebirdConnection.ExecutaSQL(fbconexiune, comanda, FirebirdConnectionClass.Tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
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
                r0["X"] = Settings.token;
                dt.Rows.InsertAt(r0, 0);
                return dt;
            }
        }

        public string GetToken()
        {
            string comanda = @"select X from versiunedb ";
            RezultatBSO rez = FirebirdConnection.ExecutaSQL(fbconexiune, comanda, FirebirdConnectionClass.Tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            DataTable dt = (DataTable)rez.obiect;
            if (dt != null) { dt.TableName = "versiunedb"; }
            return dt.Rows[0]["X"].ToString();
        }

        public DataTable GetUserExtern(string parola)
        {
            string comanda = @"select nid,username, nidextern, operator, administrator, blocat from utilizatori where upper(parola)='" + parola.ToUpper().Trim().Replace("\"", "").Replace("'", "") + "'";
            RezultatBSO rez = FirebirdConnection.ExecutaSQL(fbconexiune, comanda, FirebirdConnectionClass.Tipexecutie.FbDataAdapter_Fill, "", false, "Eroare");
            DataTable dt = (DataTable)rez.obiect;
            if (dt != null) { dt.TableName = "utilizatori"; }
            return dt;
        }
    }
}