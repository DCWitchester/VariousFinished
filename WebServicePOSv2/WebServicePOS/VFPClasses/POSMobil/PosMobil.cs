using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using WebServicePOS.Setari;
using WebServicePOS.WebServiceFunctionality;
using static WebServicePOS.Auxiliary.Enumerators;

namespace WebServicePOS.VFPClasses.POSMobil
{
    public class PosMobil : VFPConnection
    {
        #region Enums
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
        #endregion

        #region Structs
        private struct GestTXT
        {
            public int pozitii;
            public decimal total;
            public string bon;
            public int lastFel;
        }
        #endregion

        #region PosMobil
        public DataTable GetSettings()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            // aceasta metoda nu va fi expusa prin WS
            oCmd.CommandText = @"SELECT * FROM " + base.fisSettings;
            DataTable dt = new DataTable();
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            return dt;
        }

        public DataTable GetUser(string codUser)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            //jurnal.scrie("getUser=[" + codUser + "]");
            oCmd.CommandText = @"SELECT * FROM " + fisPers + " WHERE alltrim(ccod)=='" + codUser.Trim().Replace("\"", "").Replace("'", "") + "'";
            DataTable dt = new DataTable();
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            dt.TableName = "pers";
            return dt;
        }

        public string GetCcodOfCard(string card)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            string cardX = card.Trim().Replace("\"", "").Replace("'", "");
            if (cardX.Length == 0) { return "ERR"; }
            oCmd.CommandText = @"SELECT ccod FROM " + fisPers + " WHERE alltrim(card)='" + cardX + "'";
            DataTable dt = new DataTable();
            if (!base.OpenConnection()) return "ERR";
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return "ERR"; }
            if (dt.Rows.Count < 1)
            {
                return "ERR";
            }
            else
            {
                oCmd.CommandText = @"update " + fisPers + " set card='' WHERE alltrim(card)='" + card.Trim().Replace("\"", "").Replace("'", "") + "'";
                oCmd.ExecuteNonQuery();
                base.CloseConnection();
                return dt.Rows[0]["ccod"].ToString();
            }

        }

        public DataTable GetUsers()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            // aceasta metoda nu va fi expusa prin WS
            oCmd.CommandText = @"SELECT * where !lmanager FROM " + fisPers;
            DataTable dt = new DataTable();
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            dt.TableName = "pers";
            return dt;
        }

        public DataTable GetMeseOfUser(string codOspatar)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            //oCmd.CommandText = @"SELECT Ntable , round(sum(Ncant*Npret),2) as Valoare , 0 as blocat, DATETIME()-min(tora) as secunde FROM " + fisVanzari + " WHERE cuser='" + codOspatar.Replace("\"", "").Replace("'", "") + "' GROUP BY Ntable";
            oCmd.CommandText = @"SELECT v.Ntable , round(v.Ncant*v.Npret,2) as Valoare , m.blocat , DATETIME()-v.tora as secunde, " +
                                    "iif(ncant=0 , spac(50) , ALLTRIM(STRTRAN(PADL(ncant,10),'.000',''))+' x '+cden ) as produs " +
                                " FROM [" + fisVanzari + "] v " +
                                " LEFT JOIN (SELECT RECNO() as masa, iif(lblocat,1,0) as blocat FROM [" + fisMese + "] ) m ON v.ntable=m.masa  " +
                                " WHERE v.cuser='" + codOspatar.Replace("\"", "").Replace("'", "") + "' ";
            DataTable dv = new DataTable("mese");
            if (!base.OpenConnection()) return null;
            try
            {
                dv.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            var meseL = from v in dv.AsEnumerable()
                        group v by new { ntable = v.Field<Decimal>("ntable") } into gv
                        select new
                        {
                            gv.Key.ntable,
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

        public DataTable GetMeseLibere()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            oCmd.CommandText = @"SELECT Ntable FROM " + fisVanzari + " GROUP BY Ntable WHERE ntable>0";
            DataTable dv = new DataTable("mese");
            if (!base.OpenConnection()) return null;
            try
            {
                dv.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }

            oCmd.CommandText = @"SELECT recno() as masa FROM " + fisMese;
            DataTable dm = new DataTable("mese");
            if (!base.OpenConnection()) return null;
            try
            {
                dm.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }

            for (int i = 0; i < dv.Rows.Count; i++)
            {
                int.TryParse(dv.Rows[i]["ntable"].ToString(), out int tno);
                dm.Rows[tno - 1].Delete();
            }

            return dm;
        }

        public DataTable GetProductsOfMasa(string codMasa)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            oCmd.CommandText = @"SELECT cden,ncant,round(npret,2) as npret,round(npret*ncant,2) as valoare,detalii,fel,stare,uid FROM " + fisVanzari + " WHERE  ntable=" + codMasa.Replace("\"", "").Replace("'", "");
            DataTable dt = new DataTable("masa");
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            return dt;
        }

        public string GetNbonOfMasa(string codMasa)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            string nbon = "0";
            oCmd.CommandText = @"SELECT nbon FROM " + fisVanzari + " WHERE  ntable=" + codMasa.Replace("\"", "").Replace("'", "");
            DataTable dt = new DataTable("masa");
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            if (dt.Rows.Count > 0)
            {
                nbon = dt.Rows[0]["nbon"].ToString().Trim();
            }
            return nbon;
        }

        public DataTable GetGest(bool getAll)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            oCmd.CommandText = @"SELECT g.Cgest, g.Cdeng, g.calevsid, g.pretPeBon, g.numeimprim, g.coloaneimp, " +
                    "   ( SELECT count(*) FROM [" + fisCateg + "] c WHERE c.cgest=g.cgest) as nrcateg " +
                    " FROM [" + fisGest + "] g ";
            DataTable dt = new DataTable("gest");
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            if (!getAll) { dt = dt.Select("nrcateg>0", "cgest").CopyToDataTable(); }
            dt.TableName = "gest";
            return dt;
        }

        public DataTable GetCateg()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            oCmd.CommandText = @"SELECT cgest,ncod,cden, (select COUNT(*) FROM [" + fisProd + "] WHERE ncateg=categ.ncod AND lactiv ) as produse " +
                                " FROM " + fisCateg;
            DataTable dt = new DataTable("categ");
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            dt = dt.Select("produse>0", "ncod").CopyToDataTable();
            dt.Columns.Remove("produse");
            dt.TableName = "categ";
            return dt;
        }

        public DataTable GetProd(ProductType tip)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            //oCmd.CommandText = @"SELECT p.Ncod,p.Cden,round(p.Npv,2) as Npv,p.Ncateg,p.Lfractie,c.cgest,p.fel "+
            //                    " FROM " + fisProd + " as prod , "+fisCateg +
            //                    " as categ WHERE Lactiv and prod.ncateg=categ.ncod order by prod.ncod";
            String ct = "1" + DateTime.Now.ToString("HHmm");
            String vanzRap = " and (p.vanzrapida and between('" + ct + "', '1'+p.orastart, (iif(p.orastop>p.orastart,'1','2')) + p.orastop) )";

            oCmd.CommandText = @"SELECT p.nCod,p.cDen,round(p.nPv,2) as nPv,p.nCateg,iif(p.lFractie,1,0) as fra,c.cGest,p.Fel,p.obs,iif(p.favorite,1,0) as fav, iif(p.vanzRapida,1,0) as vrap" +
                                  " FROM [" + fisProd + "] p " +
                                        " JOIN [" + fisCateg + "] c ON p.nCateg=c.nCod " +
                                        " WHERE p.lActiv " + (tip.Equals(ProductType.vanzareRapida) ? vanzRap : "") +
                                  " ORDER BY p.nCod ";
            DataTable dt = new DataTable("prod");
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            return dt;
        }

        public DataTable GetProd(string codp)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            oCmd.CommandText = @"SELECT * " +
                                  " FROM [" + fisProd + "] p " +
                                  " WHERE ncod=" + codp.Trim() +
                                  " ORDER BY p.nCod ";
            DataTable dt = new DataTable("prod");
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            return dt;
        }

        public string GetDataFiscalaWinPOS()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            oCmd.CommandText = @"SELECT SUBSTR(DTOS(datafisc),1,4)+'-'+SUBSTR(DTOS(datafisc),5,2)+'-'+SUBSTR(DTOS(datafisc),7,2) FROM " + fisSettings;
            if (!base.OpenConnection()) return "";
            return (string)oCmd.ExecuteScalar();
        }

        public String Ncontor(String codGest)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            oCmd.CommandText = @"UPDATE " + fisGest + " set ncontor=ncontor+1  where upper(allt(Cgest)) = '" + codGest.Trim().ToUpper() + "'";
            if (!base.OpenConnection()) return "";
            oCmd.ExecuteScalar();
            oCmd.CommandText = @"SELECT ncontor FROM " + fisGest + " where upper(allt(Cgest)) = '" + codGest.Trim().ToUpper() + "'";
            return oCmd.ExecuteScalar().ToString();
        }

        public DataTable GetVanzariDeschise(string coduriGest)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
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
            if (!base.OpenConnection()) return null;
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

        public DataTable GetStariVanzari()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            oCmd.CommandText = @"SELECT * " +
                                    " FROM [" + fisStariVanzari + "] v " +
                                    " WHERE activ ";
            DataTable dt = new DataTable("stari");
            if (!base.OpenConnection()) return null;
            try
            {
                dt.Load(oCmd.ExecuteReader());
            }
            catch { return null; }
            finally { base.CloseConnection(); }
            return dt;
        }

        public string NextStareVanz(string _uid)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            if (!base.OpenConnection()) return "";
            RezultatBSO rez = InsertJSONVANZcallVanzariEXE($"nextStareVanz({_uid})", JV_Actiuni.JV_DO_VFP_CODE);
            if (!rez.succes)
            {
                return rez.mesaj;
            }
            // luam noua valoare
            oCmd.CommandText = @"SELECT stare FROM [" + fisVanzari + "] WHERE !lstornat AND uid = " + _uid;
            string newStare_ = oCmd.ExecuteScalar().ToString();
            return newStare_;

            /*
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
            return newStareS;*/
        }

        public RezultatBSO InsertJSONVANZ(string json, JV_Actiuni operatie)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            RezultatBSO bso = new RezultatBSO();
            if (!base.OpenConnection()) return bso;
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
            base.CloseConnection();
            return bso;
        }

        public RezultatBSO InsertJSONVANZcallVanzariEXE(string json, JV_Actiuni operatie)
        {
            Jurnal.Write("--------------------------------------------------------");
            Jurnal.Write("insertJSONVANZcallVanzariEXE(" + operatie.ToString() + ") " + json);
            RezultatBSO rez = InsertJSONVANZ(json, operatie);
            if (!rez.succes)
            {
                return rez;
            }
            long jvnid = (long)rez.obiect;

            // lansam vanzari.exe cu parametrul nid
            string xcp = Settings.calePOS.Trim();
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
                if (output.Length > 0) { Jurnal.Write("Process.OUTPUT:" + output.ToString()); }
                if (error.Length > 0) { Jurnal.Write("Process.ERROR:" + error.ToString()); }
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
            else
            {
                // preluam raspunsul din jsonvanz
                System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
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
            Jurnal.Write("saveVanzareJson: raspuns = " + rez.toText());
            return rez;
        }

        public string SaveVanzareJson(string vanzare)
        {
            return InsertJSONVANZcallVanzariEXE(vanzare, JV_Actiuni.JV_BON_SECTIE).mesaj;
        }

        public string SaveVanzare(string vanzare)
        {
            Jurnal.Write("saveVanzare " + vanzare);

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
                string numeUser = this.GetUser(codUser).Rows[0]["cnume"].ToString();

                System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
                oCmd.CommandText = @"SELECT nbon,nota " +
                                    " FROM " + fisVanzari +
                                    " WHERE cuser='" + codUser.Replace("\"", "").Replace("'", "") + "' and ntable=" + codMasa.Replace("\"", "").Replace("'", "");
                DataTable dt = new DataTable("nbon");
                dt.Load(oCmd.ExecuteReader());
                Jurnal.Write("saveVanzare " + oCmd.CommandText + " rows:" + dt.Rows.Count.ToString());
                if (dt.Rows.Count <= 0)
                {
                    return "ERR: Comanda NU a fost salvata. Nu mai exista nota de plata! ";
                }
                string nBon = dt.Rows[dt.Rows.Count - 1]["nbon"].ToString();
                string nNota = dt.Rows[dt.Rows.Count - 1]["nota"].ToString();
                DataTable feluri = this.GetFeluriProduse();
                DataTable gestiuni = this.GetGest(true);
                gestiuni.PrimaryKey = new DataColumn[] { gestiuni.Columns["cgest"] };
                if (gestiuni.Rows.Count > 0)
                {
                    GestTXT[] bonuri = new GestTXT[gestiuni.Rows.Count];
                    for (int i = 0; i < gestiuni.Rows.Count; i++)
                    {
                        bonuri[i].bon = (char)27 + newLine + gestiuni.Rows[i]["Cdeng"] + newLine + "Masa:" + codMasa.PadLeft(3) + " Ospatar: " + numeUser.PadLeft(10);
                        bonuri[i].bon += newLine + "Bon: " + Ncontor(gestiuni.Rows[i]["Cgest"].ToString());
                        bonuri[i].bon += newLine + "- - - - - - - - - - - - - - - -";
                        bonuri[i].pozitii = 0;
                        bonuri[i].total = 0;
                        bonuri[i].lastFel = 0;
                    }
                    NumberStyles style;
                    CultureInfo culture;
                    style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
                    culture = CultureInfo.CreateSpecificCulture("en-GB");
                    DataTable produse = this.GetProd(ProductType.toate);

                    // eliminam primul element din array (user,masa) pentru ca nu are acelasi nr. de elemente ca detaliile : pentru a putea sorta 
                    List<String[]> ls = strJSON.ToList();
                    ls.RemoveAt(0);
                    strJSON = ls.ToArray();
                    DataTable Settings = this.GetSettings();
                    bool useFeluri = false;
                    try { useFeluri = (bool)Settings.Rows[0]["useFeluri"]; }
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
                            decimal.TryParse(ncant, style, culture, out decimal vCant);
                            decimal.TryParse(pret, style, culture, out decimal vPret);
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
                            Jurnal.Write("saveVanzare tempfile" + tempfile);

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
                Jurnal.Write("saveVanzare-end : OK");
                return "OK";
            }
        }

        public string InsertIntoVanz(string codUser, string codMasa, string nBon, string nNota, string ncodp, string denumire, string pret, string ncant, string detalii, string nidFel)
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

            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "ExecScript";
            oCmd.Parameters.Clear();
            oCmd.Parameters.AddWithValue("code", vfpScript);
            string rez = "ERR: Comanda <insertVanz> nu a putut fi executata";
            try
            {
                rez = oCmd.ExecuteScalar().ToString();
            }
            catch (Exception e) { rez += e.ToString(); Jurnal.Write("EROARE-insertVanz: " + rez); }
            base.CloseConnection();
            return rez;
        }

        public bool Nota(int masa, string _camera, string _nc, string _lct, string _lt, string _ot, string _scv, string _mwp, double numerar, double card, double ecash)
        {
            string pars = "masa=" + masa.ToString() + ", camera=" + _camera + ", nc=" + _nc + ", lct=" + _lct + ", lt=" + _lt + ", ot=" + _ot + ", scv=" + _scv + ", mwp=" + _mwp + ", numerar=" + numerar.ToString() + ", card=" + card.ToString() + ", ecash=" + ecash.ToString();
            Jurnal.Write("nota-start (" + pars + ")");

            _camera = _camera.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _camera = "'" + _camera + "'";
            _nc = _nc.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _nc = "'" + _nc + "'";
            _lct = _lct.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _lct = "'" + _lct + "'";
            _lt = _lt.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _lt = "'" + _lt + "'";
            _ot = _ot.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _ot = "'" + _ot + "'";
            _scv = _scv.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _scv = "'" + _scv + "'";
            _mwp = _mwp.Replace('"', ' ').Replace('\'', ' ').Replace('&', ' ').Trim(); _mwp = "'" + _mwp + "'";
            string ntipplata;
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
            string sNumerar = numerar.ToString().Replace(',', '.');
            string sCard = card.ToString().Replace(',', '.');
            string sEcash = ecash.ToString().Replace(',', '.');
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"UPDATE mese SET lblocat=.t. , _camera=" + _camera + " , _nc=" + _nc + " , _lct=" + _lct + " , _lt=" +
                               _lt + " , _ot=" + _ot + " , _scv=" + _scv + " , _mwp=" + _mwp + ",ntipplata=" + ntipplata + " , plata1=" + sNumerar +
                               " , plata3=" + sCard + " , plata7=" + sEcash + " where recno()=" + masa.ToString();
            int hm = oCmd.ExecuteNonQuery();
            Jurnal.Write("nota-end : mese: records updated=" + hm.ToString());
            base.CloseConnection();
            return (hm == 1);
        }

        public string DeschideMasa(string user, string masa)
        {
            Jurnal.Write("deschideMasa user=<" + user + "> masa=<" + masa + ">");
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
	            USE Settings
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
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "ExecScript";
            oCmd.Parameters.Clear();
            oCmd.Parameters.AddWithValue("code", vfpScript);
            string rez = "ERR: Comanda <deschideMasa> nu a putut fi executata";
            try
            {
                rez = oCmd.ExecuteScalar().ToString();
            }
            catch (Exception e) { Jurnal.Write("EROARE-deschideMasa: " + e.Message); }
            base.CloseConnection();
            return rez;
        }

        public bool DeschideMasa_old(string user, string masa)
        {
            Jurnal.Write("deschideMasa user=<" + user + "> masa=<" + masa + ">");
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"SELECT count(*) as aparitii FROM " + fisVanzari + " where ntable=" + masa.Replace('\'', ' ').Replace('"', ' ');
            DataTable dv = new DataTable("vanzari");
            dv.Load(oCmd.ExecuteReader());
            int.TryParse(dv.Rows[0]["aparitii"].ToString(), out int ap);
            if (ap > 0)
            {
                base.CloseConnection();
                return false;
            }
            else
            {
                oCmd.CommandText = @"update  " + fisSettings + " set nbon=nbon+1";
                oCmd.ExecuteNonQuery();
                oCmd.CommandText = @"select nbon from " + fisSettings;
                DataTable db = new DataTable("Settings");
                db.Load(oCmd.ExecuteReader());
                int.TryParse(db.Rows[0]["nbon"].ToString(), out int nbon);
                oCmd.CommandText = @"INSERT into " + fisVanzari + " (tora,cuser,ntable,nbon,nid,lstornat,nota) values ( " +
                                "DATETIME( ) , '" + user.ToUpper() + "'," + masa + "," + nbon.ToString() + "," + nbon.ToString() + ", .f., 1)";
                oCmd.ExecuteNonQuery();
                base.CloseConnection();
                return true;
            }
        }

        public RezultatBSO ElibereazaMasa(string masa)
        {
            Jurnal.Write("elibereazaMasa (masa=" + masa + ")");

            RezultatBSO rez = new RezultatBSO();
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"SELECT sum(ncant*npret) as total FROM " + fisVanzari + " where ntable=" + masa.Replace('\'', ' ').Replace('"', ' ');
            DataTable dv = new DataTable("valmasa");
            dv.Load(oCmd.ExecuteReader());
            if ((dv.Rows.Count == 0) || (dv.Rows[0]["total"].GetType().Name.Contains("DBNull")))
            {
                rez.succes = false;
                rez.mesaj = "Masa " + masa + " nu mai este deschisa!";
                base.CloseConnection();
                return rez;
            }
            decimal valoare = (decimal)dv.Rows[0]["total"];
            if (valoare != 0)
            {
                rez.succes = false;
                rez.mesaj = "Masa " + masa + " nu poate fi eliberata: contine produse.";
                base.CloseConnection();
                return rez;
            }
            else
            {
                oCmd.CommandText = @"SELECT * from " + fisVanzari + " where ntable=" + masa.Replace('\'', ' ').Replace('"', ' ') + " and ncodp!=0 ";
                DataTable dt = new DataTable("vanzare");
                dt.Load(oCmd.ExecuteReader());
                oCmd.CommandText = @"delete from " + fisVanzari + " where ntable=" + masa.Replace('\'', ' ').Replace('"', ' ');
                oCmd.ExecuteNonQuery();
                oCmd.CommandText = @"select dtoc(datafisc) as datafisc, npos from " + fisSettings;
                DataTable ds = new DataTable("Settings");
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
            Jurnal.Write("elibereazaMasa-end (rez=" + rez.ToString() + ")");
            base.CloseConnection();
            return rez;
        }

        public bool GetSetareCardvaloric()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"SELECT cardvaloric FROM " + fisSettings;
            DataTable dv = new DataTable("Settings");
            dv.Load(oCmd.ExecuteReader());
            bool.TryParse(dv.Rows[0]["cardvaloric"].ToString(), out bool cv);
            base.CloseConnection();
            return cv;
        }

        public DataTable GetFeluriProduse()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"SELECT recn() as nid,den FROM " + fisFeluri;
            DataTable dt = new DataTable("feluri");
            dt.Load(oCmd.ExecuteReader());
            DataRow fel0 = dt.NewRow();
            fel0["nid"] = 0;
            fel0["den"] = " ";
            dt.Rows.InsertAt(fel0, 0);
            base.CloseConnection();
            return dt;
        }

        public string GetLastUpProd()
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"SELECT allt(padl(lastUpProd,10)) as lastUpProd FROM [" + fisSettings + "]";
            base.CloseConnection();
            return (string)oCmd.ExecuteScalar();
        }

        public string GetValOfCotaTva(string cotatva)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"SELECT valoare " +
                                " FROM  [" + fisCoteTva + "] " +
                                " WHERE allt(cota)=='" + cotatva.ToUpper().Trim() + "'";
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            base.CloseConnection();
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                return "";
            }
            return dt.Rows[0]["valoare"].ToString();
        }

        public string AccesGetTip(string tipAcces)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"SELECT allt(den) as den FROM [" + fisTipProd + "] where tip=" + tipAcces;
            return (string)oCmd.ExecuteScalar();
        }

        public DataTable AccesGetProdus(string tipAcces, WS.TipTagClient tipTag)
        {
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"SELECT 0 as succes, spac(30) as camera, spac(50) as numeclient, 0 as tip_tag, 000000000.00 as sold, ncod, cden, npv " +
                                " FROM  [" + fisProd + "] p " +
                                " WHERE p.tip=" + tipAcces + " and p.lactiv and " + (tipTag == WS.TipTagClient.Adult ? "p.jetoanea>0" : "p.jetoanec>0");
            DataTable dt = new DataTable();
            dt.Load(oCmd.ExecuteReader());
            base.CloseConnection();
            dt.TableName = "acces";
            return dt;
        }

        public long GenNrBon()
        {
            Jurnal.Write("genNrBon-start");
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
	        USE Settings SHARED
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
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "ExecScript";
            oCmd.Parameters.Clear();
            oCmd.Parameters.AddWithValue("code", vfpScript);
            string sBon = "-1";
            try
            {
                sBon = oCmd.ExecuteScalar().ToString();
            }
            catch (Exception e) { Jurnal.Write("EROARE-genNrBon: " + e.Message); }
            // resetam comanda 
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Clear();

            long.TryParse(sBon, out long nBon);
            Jurnal.Write("genNrBon-end: nbon=" + sBon + " -> " + nBon.ToString());
            base.CloseConnection();
            return nBon;
        }

        public bool AccesSave(string datafisc, string codUser, string nBon, DataRow drProd, string cant, string pret, string suma)
        {
            DateTime.TryParse(datafisc, out DateTime datavanz);
            cant = cant.Replace(",", ".");
            pret = pret.Replace(",", ".");
            suma = suma.Replace(",", ".");
            string codp = drProd["ncod"].ToString();
            string denumire = drProd["cden"].ToString().TrimEnd();
            string jetoanea = drProd["jetoanea"].ToString();
            string jetoanec = drProd["jetoanec"].ToString();
            string tva = GetValOfCotaTva(drProd["cotatva"].ToString()).Replace(",", ".");
            string masa = "1";
            string pos = "1";
            string tipplata = "7";

            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
            oCmd.CommandText = @"INSERT INTO [" + fisIstoric + "] (ddatavanz,tora,cuser,ntable,nbon,ncodp,npret,ncant,cden,pos,ntipplata,plata7,pretintreg,jetoanea,jetoanec,ntva) " +
                " values (" + datavanz.ToString("{^yyyy-MM-dd}") + "," + "DATETIME()" + ",'" + codUser.ToUpper() + "'," + masa + "," + nBon + "," + codp + "," + pret + "," + cant +
                ",'" + denumire + "'," + pos + "," + tipplata + "," + suma + "," + pret + "," + jetoanea + "," + jetoanec + "," + tva + ")";
            int rezins = oCmd.ExecuteNonQuery();
            Jurnal.Write("AccesSave: istoric=" + rezins.ToString() + " insert");
            if (rezins != 1)
            {
                Jurnal.Error("AccesSave: " + oCmd.CommandText);
                Jurnal.Error("AccesSave: ExecuteNonQuery=" + rezins.ToString());
            }
            base.CloseConnection();
            return rezins == 1;
        }

        public RezultatBSO AccesGetCount(string data, string tipAcces)
        {
            RezultatBSO rez = new RezultatBSO
            {
                mesaj = ""
            };
            DateTime.TryParse(data, out DateTime datavanz);
            System.Data.OleDb.OleDbCommand oCmd = base.FileBaseConnection.CreateCommand();
            base.OpenConnection();
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
            catch (Exception e) { rez.succes = false; rez.mesaj = e.Message; Jurnal.Write("Eroare - AccesGetCount: " + e.ToString()); }
            base.CloseConnection();
            return rez;
        }
        #endregion
    }
}