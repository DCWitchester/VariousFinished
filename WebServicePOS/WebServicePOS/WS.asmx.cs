﻿using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Xml;

namespace WebServicePOS
{
    /// <summary>
    /// Summary description for WS
    /// </summary>
    [WebService(Namespace = "http://www.mentorsoft.ro/WebPOS/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WS : System.Web.Services.WebService
    {
        public WS()
        {
            Setari.citeste();
        }

        [WebMethod]
        public XmlDocument getSetariWS() 
        {
            // aici scrie CE MAMA DRACULUI AI FACUT IN FIECARE VERSIUNE !!!!
            // ver.5 : ePay trimite catre SP_WRAP_TRANZACTIE cele 4 sume necesare micros-ului

            XmlDocument result = new XmlDocument();
            string xml = "<DocumentElement>"+
                            "<setariWS>" +
                                "<versiune>5.1</versiune>" +
                                "<releaseDate>2016-05-28</releaseDate>" +
                                "<caleServer>" + Setari.caleEcashFDB + "</caleServer>" +
                                "<calePOS>" + Setari.calePOS + "</calePOS>" +
                                "<tokenInSetariIni>" + (Setari.tokenInSetariIni?"da":"nu") + "</tokenInSetariIni>" +
                                "<notificationURL>" + Setari.notificationURL + "</notificationURL>" +
                            "</setariWS>" +
                         "</DocumentElement>";
            result.LoadXml(xml);
            return result;
        }

        [WebMethod]
        public Byte[] getApkFile()
        {
            using (WSPOS wsPOS = new WSPOS())
            {
                return wsPOS.getApkFile();
            }
        }
        [WebMethod]
        public String getApkVersion()
        {
            using (WSPOS wsPOS = new WSPOS())
            {
                return wsPOS.getApkVersion();
            }
        }

        [WebMethod]
        public string OLDgetUser(string par,string codUser)
        {
            if (Setari.tokenIsOk(par))
            {
                using (vfpPOS vfp = new vfpPOS())
                {
                    DataTable user = vfp.getUser(codUser);
                    if (user.Rows.Count < 1)
                    {
                        return "ERR: Cod utilizator necunoscut.";
                    }
                    else
                    {
                        string numeUser = user.Rows[0]["CNUME"].ToString().Trim();
                        numeUser = numeUser.Length == 0 ? "fara nume" : numeUser;
                        numeUser = numeUser == "ERR" ? "_ERR" : numeUser;
                        numeUser = numeUser == "ADMIN" ? "_ADMIN" : numeUser;
                        numeUser = (bool)user.Rows[0]["LMANAGER"] ? "ADMIN" : numeUser;
                        return "OK_" + numeUser;
                    }
                }
            }
            else
            {
                return "ERR_t";
            }
        }

        [WebMethod]
        public XmlDocument getUser(string par, string codUser)
        {
            if (Setari.tokenIsOk(par))
            {
                using (vfpPOS vfp = new vfpPOS())
                {
                    DataTable user;
                    try { user = vfp.getUser(codUser); }
                    catch (Exception e) { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS:\n" + e.Message); }
                    if (user.Rows.Count < 1) { return DataTable2XmlDocument.convert(null,"ERR: Cod utilizator necunoscut."); }
                    else { return DataTable2XmlDocument.convert(user, "Eroare comunicare WinPOS."); }
                }
            }
            else
            {
                return DataTable2XmlDocument.convert(null,"ERR_t");
            }
        }
        [WebMethod]
        public string getCcodOfCard(string par, string card)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par)) { return vfp.getCcodOfCard(card); }
                else { return "ERR"; }
            }
        }       

        [WebMethod]
        public XmlDocument getMeseOfUser(string par, string codUser)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getMeseOfUser(codUser), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public XmlDocument getMeseLibere(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getMeseLibere(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public XmlDocument getProductsOfMasa(string par,string codMasa)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getProductsOfMasa(codMasa), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public string getLastUpProd(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return vfp.getLastUpProd(); }
                else
                { return "ERR_t"; }
            }
        }

        [WebMethod]
        public XmlDocument getGest(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getGest(false), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }

            }
        }

        #region WebMenu
        [WebMethod]
        public XmlDocument getMeniu()
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                XmlDocument document = vfp.getMenu();
                return vfp.getMenu();
            }
        }

        [WebMethod]
        public void setSale(String sale)
        {
            //String xml = File.ReadAllText("D:\\xml.txt");
            using (vfpPOS vfp = new vfpPOS())
            {
                vfp.setSale(sale.ToString());
            }
        }

        [WebMethod]
        public XmlDocument getIsTableOpen(String table)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                return vfp.getIsTableOpen(table);
            }
        }

        [WebMethod]
        public XmlDocument getAdministration()
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                return vfp.getAdministrations();
            }
        }

        [WebMethod]
        public XmlDocument getCategories()
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                return vfp.getCategories();
            }
        }
        #endregion

        [WebMethod]
        public XmlDocument getCateg(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getCateg(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public string getGCP(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                {
                    DataTable gestiuni = vfp.getGest(false);
                    DataTable categorii = vfp.getCateg();
                    DataTable produse = vfp.getProd(tipGetprod.toate);

                    //return JsonConvert.SerializeObject(gestiuni);

                    // g.Cgest, g.Cdeng, g.calevsid, g.pretPeBon, g.numeimprim, g.coloaneimp
                    string json = "[";
                    foreach (DataRow gestiune in gestiuni.Rows)
                    {
                        string jsonCateg = "[";
                        foreach (DataRow categorie in categorii.Rows)
                        {
                            if (categorie["cgest"].ToString() == gestiune["Cgest"].ToString())
                            {
                                //cgest,ncod,cden
                                string jsonProduse = "[";
                                foreach (DataRow produs in produse.Rows)
                                {
                                    if (produs["nCateg"].ToString() == categorie["ncod"].ToString())
                                    {
                                        //p.nCod,p.cDen,round(p.nPv,2) as nPv,p.nCateg,iif(p.lFractie,1,0) as fra,c.cGest,p.Fel,p.obs,iif(p.favorite,1,0) as fav
                                        jsonProduse += (jsonProduse=="["?"":",")+
                                                       "[" + 
                                                       JsonConvert.SerializeObject(produs["nCod"].ToString())
                                                       + "," +
                                                       JsonConvert.SerializeObject(produs["cDen"].ToString().Trim() )
                                                       + "," +
                                                       JsonConvert.SerializeObject(produs["nPv"].ToString().Replace(',','.'))
                                                       + "," +
                                                       JsonConvert.SerializeObject(produs["fra"].ToString())
                                                       + "," +
                                                       JsonConvert.SerializeObject(produs["Fel"].ToString())
                                                       + "," +
                                                       JsonConvert.SerializeObject(produs["fav"].ToString())
                                                       + "," +
                                                       JsonConvert.SerializeObject(produs["vrap"].ToString())
                                                       + "]";
                                    }
                                }
                                jsonProduse += "]";
                                jsonCateg += (jsonCateg == "[" ? "" : ",") 
                                             +"["+ 
                                             JsonConvert.SerializeObject(categorie["ncod"].ToString()) 
                                             + "," +
                                             JsonConvert.SerializeObject(categorie["cden"].ToString().Trim()) 
                                             + "," + 
                                             jsonProduse 
                                             +"]";
                            }
                        }
                        jsonCateg += "]";
                        json += (json == "[" ? "" : ",") + 
                            "[" + 
                            JsonConvert.SerializeObject(gestiune["Cgest"].ToString())
                            + "," +
                            JsonConvert.SerializeObject(gestiune["Cdeng"].ToString().Trim())
                            + "," +
                            jsonCateg
                            + "]"
                            ;
                    }
                    json += "]";
                    //return json.Replace('&', '_').Replace('<', '_').Replace('>', '_').Replace('\'', '_').Replace('"', '_');
                    return json;

                }
                else
                { return "Err_t"; }
            }
        }

        [WebMethod]
        public XmlDocument getProd(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getProd(tipGetprod.toate), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public XmlDocument getProdVanzareRapida(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getProd(tipGetprod.vanzareRapida), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public XmlDocument getFeluriProduse(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getFeluriProduse(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public string saveVanzare(string par,string vanzare)
        {
            // versiune mai veche
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return vfp.saveVanzare(vanzare); }
                else { return "ERR_t"; }
            }
        }
        [WebMethod]
        public string saveVanzareJson(string par, string vanzare)
        {
            // versiune mai noua - e nevoie de update la winpos-bd pentru a folosi
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return vfp.saveVanzareJson(vanzare); }
                else { return "ERR_t"; }
            }
        }

        [WebMethod]
        public XmlDocument nota(string par,string masa, string numerar, string card, string tag, string suma, string pin, string data, string nrdoc, string detalii, string sursa, string utilizator)
        {
            if (!Setari.tokenIsOk(par))
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: comunicare _t!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            int valoareMasa;
            if (!int.TryParse(masa, out valoareMasa))
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: masa nu este un numar!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            if (valoareMasa <= 0)
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: masa nu e un numar pozitiv!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            NumberStyles style;
            CultureInfo culture;
            style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
            culture = CultureInfo.CreateSpecificCulture("en-GB");
            double valoareNumerar;            
            if (!double.TryParse(numerar, style , culture , out valoareNumerar))
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: suma <<numerar>> nu este un numar!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            if (valoareNumerar < 0)
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: suma <<numerar>> este negativa!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            double valoareCard;
            if (!double.TryParse(card, style, culture, out valoareCard))
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: suma <<card>> nu este un numar!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            if (valoareCard < 0)
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: suma <<card>> este negativa!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            double valoareECash;
            if (!double.TryParse(suma, style, culture, out valoareECash))
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: suma <<ecash>> nu este un numar!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            if (valoareECash < 0)
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: suma <<ecash>> este negativa!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            Int32 nbon = 0;
            Int32.TryParse(nrdoc, out nbon);
            if (nbon == 0)
            {
                using (vfpPOS vfp = new vfpPOS())
                {
                    nrdoc = vfp.getNbonOfMasa(masa);
                }
            }
            string _camera = "";
            string _nc = "";
            string _lct = "";
            string _lt = "";
            string _ot = "";
            string _scv = "";
            string _mwp = "";
            XmlDocument rasp2 = new XmlDocument();
            if (valoareECash > 0)
            {
                using (EPay epay = new EPay())
                {
                    bool BlocatOK = false;
                    DataTable dt = (DataTable)(epay.epay(tag, suma, pin, data, nrdoc, detalii, sursa, utilizator).obiect);
                    if (dt.Rows.Count > 0)
                    {
                        int isucces = int.Parse( dt.Rows[0]["SUCCES"].ToString() );
                        _camera = dt.Rows[0]["CAMERA"].ToString();
                        _nc = dt.Rows[0]["NUMECLIENT"].ToString();
                        _lct = dt.Rows[0]["LASTCARDTRANZACTIE"].ToString();
                        _ot = dt.Rows[0]["ORATRANZACTIE"].ToString();
                        _mwp = dt.Rows[0]["MOMENTREACTIVARE"].ToString();
                        _lt = dt.Rows[0]["LASTTRANZACTIE"].ToString();
                        _scv = dt.Rows[0]["SOLD"].ToString();
                        if (isucces > 0) 
                        {
                            using (vfpPOS vfp = new vfpPOS())
                            {
                                BlocatOK = vfp.nota(valoareMasa, _camera, _nc, _lct, _lt, _ot, _scv, _mwp, valoareNumerar, valoareCard, valoareECash);
                            }
                        }
                    }
                    rasp2 = DataTable2XmlDocument.convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
                    XmlNode elem = rasp2.CreateNode(XmlNodeType.Element, "masa", null);
                    elem.InnerText = (BlocatOK?"blocat":"neblocat");
                    rasp2.DocumentElement.AppendChild(elem); 
                }
            }
            else
            {
                bool BlocatOK = false;
                using (vfpPOS vfp = new vfpPOS())
                {
                    BlocatOK = vfp.nota(valoareMasa, _camera, _nc, _lct, _lt, _ot, _scv, _mwp, valoareNumerar, valoareCard, valoareECash);
                }
                rasp2.LoadXml("<DocumentElement><plata><SUCCES>"+(BlocatOK?"1":"-1")+"</SUCCES><NUMECLIENT></NUMECLIENT></plata></DocumentElement>");
            }
            return rasp2;
        }

        [WebMethod]
        public string deschideMasa(string par,string user, string masa)
        {         
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return vfp.deschideMasa(user, masa); }
                else { return "ERR_t"; }
            }
        }

        [WebMethod]
        public string elibereazaMasa(string par,string masa)
        {

            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                {
                    RezultatBSO rez = vfp.elibereazaMasa(masa);
                    return rez.succes ? "OK" : "ERR: "+rez.mesaj; }
                else
                { return "ERR_t"; }
            }
        }

        [WebMethod]
        public string getDataFiscalaWinPOS(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return vfp.getDataFiscalaWinPOS(); }
                else
                { return "ERR_t"+" ["+Setari.token+ "42x"+"]   ["+par+"]  "+(Setari.tokenIsOk(par)?"ok":"no")+"   "+ ((Setari.token + "42x").CompareTo(par.Trim())).ToString(); }
            }

        }

        [WebMethod]
        public XmlDocument epay(string par, string tag, string suma, string pin, string data, string nrdoc, string detalii, string sursa, string utilizator)
        {
            if (!Setari.tokenIsOk(par))
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare_t</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            NumberStyles style;
            CultureInfo culture;
            style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
            culture = CultureInfo.CreateSpecificCulture("en-GB");
            decimal valoare;
            if (!decimal.TryParse(suma, style, culture, out valoare))
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: suma trimisa nu este un numar!</NUMECLIENT></plata></DocumentElement>");
                return rasp; 
            }
            if (valoare < 0)
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: suma trimisa este negativa!</NUMECLIENT></plata></DocumentElement>");
                return rasp; 
            }

            using (EPay epay = new EPay())
            {
                DataTable dt = (DataTable)(epay.epay(tag, suma, pin, data, nrdoc, detalii, sursa, utilizator).obiect);
                return DataTable2XmlDocument.convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
            }
        }

        [WebMethod]
        public XmlDocument realizariExtern(string par,string nidtableta, string dela, string panala)
        {
            using (EPay epay = new EPay())
            {
                if (Setari.tokenIsOk(par))
                {
                    DataTable dt = epay.realizariExtern(nidtableta, dela, panala);
                    return DataTable2XmlDocument.convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
                }
                else { return DataTable2XmlDocument.convert(null, "Eroare comunicare cu serverul de plati Ecash_t."); }
            }
        }

        [WebMethod]
        public XmlDocument externi(string par,string nidTableta)
        {
            if (!Setari.tokenIsOk(par))
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><eroare><denumire>ERR_t</denumire></eroare></DocumentElement>");
                return rasp;
            }
            int iNid;
            if (!int.TryParse(nidTableta, out iNid))
            {
                XmlDocument rasp = new XmlDocument();
                rasp.LoadXml("<DocumentElement><eroare><denumire>ERR: ID-ul trimis nu este un numar!</denumire></eroare></DocumentElement>");
                return rasp;
            }
            using (EPay epay = new EPay())
            {
                DataTable dt = epay.externi(iNid);
                return DataTable2XmlDocument.convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
            }
        }

        [WebMethod]
        public string inchidereZi(string par,string nidTableta)
        {
            int iNidTableta;
            int.TryParse(nidTableta, out iNidTableta);
            using (EPay epay = new EPay())
            {
                if (Setari.tokenIsOk(par))
                { return epay.inchidereZi(iNidTableta); }
                else { return "ERR_t"; }
            }

        }
   
        [WebMethod]
        public XmlDocument versiune(string par)
        {
            using (EPay epay = new EPay())
            {
                DataTable dt = epay.versiune(par);
                return DataTable2XmlDocument.convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
            }
        }

        [WebMethod]
        public XmlDocument getUserExtern(string par, string parola)
        {
            using (EPay epay = new EPay())
            {
                if (Setari.tokenIsOk(par))
                {
                    DataTable dt = epay.getUserExtern(parola);
                    return DataTable2XmlDocument.convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
                }
                else { return DataTable2XmlDocument.convert(null, "Eroare comunicare cu serverul de plati Ecash_t."); }
            }
        }

        [WebMethod]
        public XmlDocument GetTempPath()
        {
            XmlDocument result = new XmlDocument();
            result.LoadXml("<DocumentElement><MESAJ>" + System.IO.Path.GetTempPath() + "</MESAJ></DocumentElement>");
            return result;
        }

        [WebMethod]
        public string getSetareCardvaloric(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                {
                    return vfp.getSetareCardvaloric() ? "TRUE" : "FALSE";
                }
                else { return "ERR_t"; }
            }
        }

        [WebMethod]
        public string AccesGetTip(string par, string tipAcces)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                {
                    return vfp.AccesGetTip(tipAcces);
                }
                else { return "ERR_t"; }
            }
        }

        [WebMethod]
        public XmlDocument AccesInit(string par, string tipAcces , string tagClient, string dataFiscala, string sursa, string utilizator)
        {
            if (!Setari.tokenIsOk(par)) { return DataTable2XmlDocument.convert(null, "ERR_t"); }
            // citim din ecash informatii despre Tag ( nume , camera , arePin , sold , TIP ) ; citim din vmentor produsul ( denumire + pret ) 
            // raspundem cu : succes integer, camera varchar(30), numeclient varchar(500), tip_tag integer, sold numeric(18,2) , DENUMIRE Produs , PRET Produs 
            using (EPay epay = new EPay()) 
            {
                DataTable dtEcash = (DataTable)(epay.epay(tagClient, "0", "0", dataFiscala, "0", "acces init", sursa, utilizator).obiect);
                if (dtEcash == null) { return DataTable2XmlDocument.convert(null, "Eroare comunicare cu serverul de plati Ecash."); }
                if (((int)dtEcash.Rows[0]["succes"]) < 0) { return DataTable2XmlDocument.convert(null, dtEcash.Rows[0]["numeclient"].ToString()); }
                using (vfpPOS vfp = new vfpPOS())
                {
                    TipTagClient tipTagClient = (TipTagClient)dtEcash.Rows[0]["tip_tag"];
                    DataTable dt = vfp.AccesGetProdus(tipAcces, tipTagClient);
                    if (dt.Rows.Count < 1)
                    {
                        return DataTable2XmlDocument.convert(null, "Nu exista produs definit in WINPOS pentru acces " + tipTagClient.ToString());
                    }
                    else
                    {
                        dt.TableName = "acces";
                        dt.Rows[0]["succes"] = (int)dtEcash.Rows[0]["succes"];
                        dt.Rows[0]["camera"] = (String)dtEcash.Rows[0]["camera"];
                        dt.Rows[0]["numeclient"] = (String)dtEcash.Rows[0]["numeclient"];
                        dt.Rows[0]["tip_tag"] = (int)dtEcash.Rows[0]["tip_tag"];
                        dt.Rows[0]["sold"] = (decimal)dtEcash.Rows[0]["sold"];
                        return DataTable2XmlDocument.convert(dt, "Eroare acces.init");
                    }
                }
            }
        }
        public enum TipTagClient
        {
            Adult = 0,
            Copil = 1
        }

        [WebMethod]
        public XmlDocument AccesPlata(string par, string utilizator, string tagClient, string pin, string codp, string cant, string pret, string detalii, string sursa)
        {
            //
            //string par="56DX5442x"; 
            //string utilizator="0000000001"; 
            //string tagClient="1122";
            //string pin="0"; 
            //string codp="281";
            //string cant="1.00";
            //string pret = "2.00";
            //string detalii = "test";
            //string sursa = "test";

            jurnal.scrie("AccesPlata(utilizator:" + utilizator + ", tagClient:" + tagClient + ", codp:" + codp + ", cant:" + cant + ", pret:" + pret + ")");
            XmlDocument rasp = new XmlDocument();
            if (!Setari.tokenIsOk(par))
            {
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare_t</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            NumberStyles style;
            CultureInfo culture;
            style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
            culture = CultureInfo.CreateSpecificCulture("en-GB");
            decimal cantd, pretd, valoare;
            if (!decimal.TryParse(cant, style, culture, out cantd))
            {
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: cantitatea trimisa nu este un numar!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            if (!decimal.TryParse(pret, style, culture, out pretd))
            {
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: pretul trimis nu este un numar!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            valoare = Math.Round(cantd * pretd, 2);
            if (valoare < 0)
            {
                rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: suma trimisa este negativa!</NUMECLIENT></plata></DocumentElement>");
                return rasp;
            }
            // facem plata in ecash
            // salvam tranzactia in istoric
            // imprimam nota 
            DataTable dtEcash;
            using (vfpPOS vfp = new vfpPOS())
            {
                string dataFiscala = vfp.getDataFiscalaWinPOS();
                DataTable dtProd = vfp.getProd(codp);
                if (dtProd.Rows.Count == 0)
                {
                    rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: nu exista produsul trimis!</NUMECLIENT></plata></DocumentElement>");
                    return rasp;
                }
                long nBon = vfp.genNrBon();
                if (nBon <= 0)
                {
                    rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: nu s-a putut genera numar document!</NUMECLIENT></plata></DocumentElement>");
                    return rasp;
                }
                using (EPay epay = new EPay())
                {
                    // facem plata:
                    RezultatBSO rezEcash = epay.epay(tagClient, valoare.ToString("F2"), pin, dataFiscala, nBon.ToString(), detalii, sursa, utilizator);
                    if (rezEcash.obiect == null)
                    {
                        return DataTable2XmlDocument.convert(null, "Eroare comunicare cu serverul de plati Ecash: " + rezEcash.mesaj);
                    }
                    dtEcash = (DataTable)rezEcash.obiect;
                    if (((int)dtEcash.Rows[0]["succes"]) < 0)
                    {
                        return DataTable2XmlDocument.convert(dtEcash, "Tranzactie nereusita!");
                    }
                }
                // salvam in istoric:
                vfp.AccesSave(dataFiscala, utilizator, nBon.ToString(), dtProd.Rows[0], cant, pret, valoare.ToString());
                dtEcash.TableName = "plata";

                // generam o chitanta
                DataTable setari = vfp.getSetari();
                int nrex = 0;
                try { nrex = int.Parse(setari.Rows[0]["accesnrex"].ToString()); }
                catch (Exception e) { }
                int nrcol = 32;
                string chitanta = (char)27 + vfp.newLine;
                for (int i = 1; i <= nrex; i++)
                {
                    chitanta += vfp.fontBold + setari.Rows[0]["cdensoc"].ToString().Trim().PadCenter(24, ' ') + vfp.fontNormal + vfp.newLine;
                    chitanta += setari.Rows[0]["csediu"].ToString().Trim().PadCenter(24,' ') + vfp.newLine;
                    chitanta += "".PadLeft(nrcol, '-') + vfp.newLine;
                    chitanta += "Data: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + vfp.newLine;
                    chitanta += "Bon : " + nBon.ToString() + vfp.newLine;
                    chitanta += "".PadLeft(nrcol, '-') + vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += vfp.fontBold + "SUMA: " + valoare.ToString("F2") + " lei" + vfp.fontNormal + vfp.newLine;
                    chitanta += detalii + vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += "Client: " + dtEcash.Rows[0]["numeclient"].ToString().Trim() + vfp.newLine;
                    chitanta += "Camera: " + dtEcash.Rows[0]["camera"].ToString().Trim() + vfp.newLine;
                    chitanta += "SOLD DISPONIBIL: " + vfp.fontBold + ((decimal)dtEcash.Rows[0]["sold"]).ToString("F2").Trim()+ " lei" + vfp.fontNormal + vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += "Semnatura................" + vfp.newLine;
                    chitanta += "Exemplar " + i.ToString() + "/" + nrex.ToString() + vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += vfp.newLine;
                    if (i < nrex)
                    {
                        chitanta += "> - - - - - - - - - - - - - - <" + vfp.newLine;
                        chitanta += vfp.newLine;
                        chitanta += vfp.newLine;
                        chitanta += vfp.newLine;
                        chitanta += (char)29 + "V0";
                    }
                    chitanta += vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += vfp.newLine;
                    chitanta += vfp.newLine;
                }
                chitanta += (char)29 + "V0";

                String numeUnic = "ch_" + nBon.ToString() + "_" + DateTime.Now.ToBinary().ToString() + ".txt";
                string tempFile = System.IO.Path.GetTempPath() + "\\" + numeUnic;
                String vsidFile = setari.Rows[0]["accesvsid"].ToString() + @"\" + numeUnic;
                StreamWriter sw = new StreamWriter(tempFile);
                sw.Write(chitanta);
                sw.Close();
                try
                {
                    File.Copy(tempFile, vsidFile);
                }
                catch (Exception e)
                {
                    string errmes = "ERR: Eroare la trimiterea chitantei spre imprimare: \n\r" + e.Message;
                    jurnal.scrie(errmes);
                    dtEcash.Rows[0]["numeclient"] = dtEcash.Rows[0]["numeclient"].ToString().Trim() + " [" + errmes + "]";
                }

            }
            return DataTable2XmlDocument.convert(dtEcash, "Tranzactie nereusita!");
        }

        [WebMethod]
        public XmlDocument AccesGetCount(string par, string data, string tipAcces)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                {
                    RezultatBSO rez = vfp.AccesGetCount(data, tipAcces);
                    return DataTable2XmlDocument.convert((DataTable)rez.obiect, "Eroare WS.AccesGetCount: " + rez.mesaj);
                }
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        //
        [WebMethod]
        public XmlDocument getVanzariDeschise(string par, string coduriGest)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getVanzariDeschise(coduriGest), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }
        [WebMethod]
        public XmlDocument getStariVanzari(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getStariVanzari(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }
        [WebMethod]
        public string nextStareVanz(string par, string uid)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par)) { return vfp.nextStareVanz(uid); }
                else { return "ERR_t."; }
            }
        }
        [WebMethod]
        public XmlDocument getUsers(string par)
        {
            using (vfpPOS vfp = new vfpPOS())
            {
                if (Setari.tokenIsOk(par))
                { return DataTable2XmlDocument.convert(vfp.getUsers(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }
    }
}