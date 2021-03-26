using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebServicePOS.WebServiceFunctionality;

namespace WebServicePOS.Firebird
{
    public class FirebirdConnectionClass
    {

        public enum Tipexecutie 
        { 
            FbCommand_ExecuteNonQuery = 1, 
            FbCommand_ExecuteReader, 
            FbCommand_ExecuteScalar, 
            FbDataAdapter_Fill 
        }

        public RezultatBSO ExecutaSQL(FirebirdSql.Data.FirebirdClient.FbConnection conexiune, string sirsql, Tipexecutie tipexec, string tabela, bool iesire, string mesaj, FirebirdSql.Data.FirebirdClient.FbParameter[] ppar)
        {
            FirebirdSql.Data.FirebirdClient.FbTransaction tranzactie = null;
            return ExecutaSQL(conexiune, sirsql, tipexec, tabela, iesire, mesaj, ppar, tranzactie);
        }
        public RezultatBSO ExecutaSQL(FirebirdSql.Data.FirebirdClient.FbConnection conexiune, string sirsql, Tipexecutie tipexec, string tabela, bool iesire, string mesaj)
        {
            FirebirdSql.Data.FirebirdClient.FbParameter[] par = { };
            return ExecutaSQL(conexiune, sirsql, tipexec, tabela, iesire, mesaj, par);
        }
        public RezultatBSO ExecutaSQL(FirebirdSql.Data.FirebirdClient.FbConnection conexiune, string sirsql, Tipexecutie tipexec, string tabela, bool iesire, string mesaj, FirebirdSql.Data.FirebirdClient.FbParameter[] ppar, FirebirdSql.Data.FirebirdClient.FbTransaction tranzactie)
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
            if (tipexec != Tipexecutie.FbDataAdapter_Fill) { for (int i = 0; i < ppar.GetLength(0); i++) { fbcommand.Parameters.Add(ppar[i]); } }
            else { for (int i = 0; i < ppar.GetLength(0); i++) { fbDA.SelectCommand.Parameters.Add(ppar[i]); } }
            {
                try
                {
                    switch (tipexec)
                    {
                        case Tipexecutie.FbCommand_ExecuteNonQuery:
                            //Executes commands such as SQL INSERT, DELETE, UPDATE , and SET statements.
                            //fbcommand.Transaction = tranzactie;
                            rez.obiect = fbcommand.ExecuteNonQuery();
                            break;
                        case Tipexecutie.FbCommand_ExecuteReader:
                            //Executes commands that return rows.
                            //fbcommand.Transaction = tranzactie;
                            rez.obiect = fbcommand.ExecuteReader();
                            break;
                        case Tipexecutie.FbCommand_ExecuteScalar:
                            //Retrieves a single value (for example, an aggregate value) from a database.
                            //fbcommand.Transaction = tranzactie;
                            rez.obiect = fbcommand.ExecuteScalar();
                            break;
                        case Tipexecutie.FbDataAdapter_Fill:
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
                    Jurnal.Write("firebird.executa " + exc.ErrorCode.ToString() + " " + exc.Message);
                    Jurnal.Write("fbcommand: " + fbcommand.CommandText);
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
        public FirebirdSql.Data.FirebirdClient.FbTransaction BeginTranzactie(FirebirdSql.Data.FirebirdClient.FbConnection conexiune, string nume)
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
        public bool ComiteTranzactia(FirebirdSql.Data.FirebirdClient.FbTransaction tranzactie)
        {
            tranzactie.Commit();
            return true;
        }
        public bool EndTranzactie(FirebirdSql.Data.FirebirdClient.FbTransaction tranzactie)
        {
            tranzactie.Commit();
            return true;
        }
        public bool ConectareBD(FirebirdSql.Data.FirebirdClient.FbConnection fbconexiune, string caleServer, string userServer, string passServer, ref string mesaj)
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
                        mesaj = "Nu ma pot conecta la baza de date!   Verificati Settingsle de conectare, serverul firebird...";
                        break;
                    case 335544721:
                        mesaj = "Nu ma pot conecta la baza de date!   Verificati Settingsle de conectare, serverul firebird...";
                        break;
                    case 335544472:
                        mesaj = "Nu ma pot conecta la baza de date!   Verificati Settingsle de conectare ( username si parola )";
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
        public void DeconectareBD(FirebirdSql.Data.FirebirdClient.FbConnection fbconexiune)
        {
            fbconexiune.Close();
        }
    }
}