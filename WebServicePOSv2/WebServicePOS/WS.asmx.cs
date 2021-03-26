using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Xml;
using WebServicePOS.Setari;
using WebServicePOS.Auxiliary;
using static WebServicePOS.Auxiliary.Enumerators;
using WebServicePOS.WebServiceFunctionality;
using WebServicePOS.VFPClasses.POSMobil;
using WebServicePOS.VFPClasses.POSTable;

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
            Settings.ReadSettings();
        }

        [WebMethod]
        public XmlDocument getSettingsWS() 
        {
            // aici scrie CE MAMA DRACULUI AI FACUT IN FIECARE VERSIUNE !!!!
            // ver.5 : ePay trimite catre SP_WRAP_TRANZACTIE cele 4 sume necesare micros-ului

            XmlDocument result = new XmlDocument();
            string xml = "<DocumentElement>"+
                            "<SettingsWS>" +
                                "<versiune>5.1</versiune>" +
                                "<releaseDate>2016-05-28</releaseDate>" +
                                "<caleServer>" + Settings.caleEcashFDB + "</caleServer>" +
                                "<calePOS>" + Settings.calePOS + "</calePOS>" +
                                "<tokenInSettingsIni>" + (Settings.tokenInSettingsIni?"da":"nu") + "</tokenInSettingsIni>" +
                                "<notificationURL>" + Settings.notificationURL + "</notificationURL>" +
                            "</SettingsWS>" +
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
            if (Settings.TokenIsOk(par))
            {
                using (PosMobil vfp = new PosMobil())
                {
                    DataTable user = vfp.GetUser(codUser);
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
            if (Settings.TokenIsOk(par))
            {
                using (PosMobil vfp = new PosMobil())
                {
                    DataTable user;
                    try { user = vfp.GetUser(codUser); }
                    catch (Exception e) { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS:\n" + e.Message); }
                    if (user.Rows.Count < 1) { return DataTable2XmlDocument.Convert(null,"ERR: Cod utilizator necunoscut."); }
                    else { return DataTable2XmlDocument.Convert(user, "Eroare comunicare WinPOS."); }
                }
            }
            else
            {
                return DataTable2XmlDocument.Convert(null,"ERR_t");
            }
        }
        [WebMethod]
        public string getCcodOfCard(string par, string card)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par)) { return vfp.GetCcodOfCard(card); }
                else { return "ERR"; }
            }
        }       

        [WebMethod]
        public XmlDocument getMeseOfUser(string par, string codUser)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetMeseOfUser(codUser), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public XmlDocument getMeseLibere(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetMeseLibere(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public XmlDocument getProductsOfMasa(string par,string codMasa)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetProductsOfMasa(codMasa), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public string getLastUpProd(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return vfp.GetLastUpProd(); }
                else
                { return "ERR_t"; }
            }
        }

        [WebMethod]
        public XmlDocument getGest(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetGest(false), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }

            }
        }

        [WebMethod]
        public XmlDocument getCateg(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetCateg(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public string getGCP(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                {
                    DataTable gestiuni = vfp.GetGest(false);
                    DataTable categorii = vfp.GetCateg();
                    DataTable produse = vfp.GetProd(ProductType.toate);

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
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetProd(ProductType.toate), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public XmlDocument getProdVanzareRapida(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetProd(ProductType.vanzareRapida), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public XmlDocument getFeluriProduse(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetFeluriProduse(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        [WebMethod]
        public string saveVanzare(string par,string vanzare)
        {
            // versiune mai veche
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return vfp.SaveVanzare(vanzare); }
                else { return "ERR_t"; }
            }
        }
        [WebMethod]
        public string saveVanzareJson(string par, string vanzare)
        {
            // versiune mai noua - e nevoie de update la winpos-bd pentru a folosi
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return vfp.SaveVanzareJson(vanzare); }
                else { return "ERR_t"; }
            }
        }

        [WebMethod]
        public XmlDocument nota(string par,string masa, string numerar, string card, string tag, string suma, string pin, string data, string nrdoc, string detalii, string sursa, string utilizator)
        {
            if (!Settings.TokenIsOk(par))
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
                using (PosMobil vfp = new PosMobil())
                {
                    nrdoc = vfp.GetNbonOfMasa(masa);
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
                    DataTable dt = (DataTable)(epay.Epay(tag, suma, pin, data, nrdoc, detalii, sursa, utilizator).obiect);
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
                            using (PosMobil vfp = new PosMobil())
                            {
                                BlocatOK = vfp.Nota(valoareMasa, _camera, _nc, _lct, _lt, _ot, _scv, _mwp, valoareNumerar, valoareCard, valoareECash);
                            }
                        }
                    }
                    rasp2 = DataTable2XmlDocument.Convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
                    XmlNode elem = rasp2.CreateNode(XmlNodeType.Element, "masa", null);
                    elem.InnerText = (BlocatOK?"blocat":"neblocat");
                    rasp2.DocumentElement.AppendChild(elem); 
                }
            }
            else
            {
                bool BlocatOK = false;
                using (PosMobil vfp = new PosMobil())
                {
                    BlocatOK = vfp.Nota(valoareMasa, _camera, _nc, _lct, _lt, _ot, _scv, _mwp, valoareNumerar, valoareCard, valoareECash);
                }
                rasp2.LoadXml("<DocumentElement><plata><SUCCES>"+(BlocatOK?"1":"-1")+"</SUCCES><NUMECLIENT></NUMECLIENT></plata></DocumentElement>");
            }
            return rasp2;
        }

        [WebMethod]
        public string deschideMasa(string par,string user, string masa)
        {         
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return vfp.DeschideMasa(user, masa); }
                else { return "ERR_t"; }
            }
        }

        [WebMethod]
        public string elibereazaMasa(string par,string masa)
        {

            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                {
                    RezultatBSO rez = vfp.ElibereazaMasa(masa);
                    return rez.succes ? "OK" : "ERR: "+rez.mesaj; }
                else
                { return "ERR_t"; }
            }
        }

        [WebMethod]
        public string getDataFiscalaWinPOS(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return vfp.GetDataFiscalaWinPOS(); }
                else
                { return "ERR_t"+" ["+Settings.token+ "42x"+"]   ["+par+"]  "+(Settings.TokenIsOk(par)?"ok":"no")+"   "+ ((Settings.token + "42x").CompareTo(par.Trim())).ToString(); }
            }

        }

        [WebMethod]
        public XmlDocument epay(string par, string tag, string suma, string pin, string data, string nrdoc, string detalii, string sursa, string utilizator)
        {
            if (!Settings.TokenIsOk(par))
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
                DataTable dt = (DataTable)(epay.Epay(tag, suma, pin, data, nrdoc, detalii, sursa, utilizator).obiect);
                return DataTable2XmlDocument.Convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
            }
        }

        [WebMethod]
        public XmlDocument realizariExtern(string par,string nidtableta, string dela, string panala)
        {
            using (EPay epay = new EPay())
            {
                if (Settings.TokenIsOk(par))
                {
                    DataTable dt = epay.RealizariExtern(nidtableta, dela, panala);
                    return DataTable2XmlDocument.Convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
                }
                else { return DataTable2XmlDocument.Convert(null, "Eroare comunicare cu serverul de plati Ecash_t."); }
            }
        }

        [WebMethod]
        public XmlDocument externi(string par,string nidTableta)
        {
            if (!Settings.TokenIsOk(par))
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
                DataTable dt = epay.Externi(iNid);
                return DataTable2XmlDocument.Convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
            }
        }

        [WebMethod]
        public string inchidereZi(string par,string nidTableta)
        {
            int iNidTableta;
            int.TryParse(nidTableta, out iNidTableta);
            using (EPay epay = new EPay())
            {
                if (Settings.TokenIsOk(par))
                { return epay.InchidereZi(iNidTableta); }
                else { return "ERR_t"; }
            }

        }
   
        [WebMethod]
        public XmlDocument versiune(string par)
        {
            using (EPay epay = new EPay())
            {
                DataTable dt = epay.Versiune(par);
                return DataTable2XmlDocument.Convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
            }
        }

        [WebMethod]
        public XmlDocument getUserExtern(string par, string parola)
        {
            using (EPay epay = new EPay())
            {
                if (Settings.TokenIsOk(par))
                {
                    DataTable dt = epay.GetUserExtern(parola);
                    return DataTable2XmlDocument.Convert(dt, "Eroare comunicare cu serverul de plati Ecash.");
                }
                else { return DataTable2XmlDocument.Convert(null, "Eroare comunicare cu serverul de plati Ecash_t."); }
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
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                {
                    return vfp.GetSetareCardvaloric() ? "TRUE" : "FALSE";
                }
                else { return "ERR_t"; }
            }
        }

        [WebMethod]
        public string AccesGetTip(string par, string tipAcces)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                {
                    return vfp.AccesGetTip(tipAcces);
                }
                else { return "ERR_t"; }
            }
        }

        [WebMethod]
        public XmlDocument AccesInit(string par, string tipAcces , string tagClient, string dataFiscala, string sursa, string utilizator)
        {
            if (!Settings.TokenIsOk(par)) { return DataTable2XmlDocument.Convert(null, "ERR_t"); }
            // citim din ecash informatii despre Tag ( nume , camera , arePin , sold , TIP ) ; citim din vmentor produsul ( denumire + pret ) 
            // raspundem cu : succes integer, camera varchar(30), numeclient varchar(500), tip_tag integer, sold numeric(18,2) , DENUMIRE Produs , PRET Produs 
            using (EPay epay = new EPay()) 
            {
                DataTable dtEcash = (DataTable)(epay.Epay(tagClient, "0", "0", dataFiscala, "0", "acces init", sursa, utilizator).obiect);
                if (dtEcash == null) { return DataTable2XmlDocument.Convert(null, "Eroare comunicare cu serverul de plati Ecash."); }
                if (((int)dtEcash.Rows[0]["succes"]) < 0) { return DataTable2XmlDocument.Convert(null, dtEcash.Rows[0]["numeclient"].ToString()); }
                using (PosMobil vfp = new PosMobil())
                {
                    TipTagClient tipTagClient = (TipTagClient)dtEcash.Rows[0]["tip_tag"];
                    DataTable dt = vfp.AccesGetProdus(tipAcces, tipTagClient);
                    if (dt.Rows.Count < 1)
                    {
                        return DataTable2XmlDocument.Convert(null, "Nu exista produs definit in WINPOS pentru acces " + tipTagClient.ToString());
                    }
                    else
                    {
                        dt.TableName = "acces";
                        dt.Rows[0]["succes"] = (int)dtEcash.Rows[0]["succes"];
                        dt.Rows[0]["camera"] = (String)dtEcash.Rows[0]["camera"];
                        dt.Rows[0]["numeclient"] = (String)dtEcash.Rows[0]["numeclient"];
                        dt.Rows[0]["tip_tag"] = (int)dtEcash.Rows[0]["tip_tag"];
                        dt.Rows[0]["sold"] = (decimal)dtEcash.Rows[0]["sold"];
                        return DataTable2XmlDocument.Convert(dt, "Eroare acces.init");
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

            Jurnal.Write("AccesPlata(utilizator:" + utilizator + ", tagClient:" + tagClient + ", codp:" + codp + ", cant:" + cant + ", pret:" + pret + ")");
            XmlDocument rasp = new XmlDocument();
            if (!Settings.TokenIsOk(par))
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
            using (PosMobil vfp = new PosMobil())
            {
                string dataFiscala = vfp.GetDataFiscalaWinPOS();
                DataTable dtProd = vfp.GetProd(codp);
                if (dtProd.Rows.Count == 0)
                {
                    rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: nu exista produsul trimis!</NUMECLIENT></plata></DocumentElement>");
                    return rasp;
                }
                long nBon = vfp.GenNrBon();
                if (nBon <= 0)
                {
                    rasp.LoadXml("<DocumentElement><plata><SUCCES>-1</SUCCES><NUMECLIENT>Eroare: nu s-a putut genera numar document!</NUMECLIENT></plata></DocumentElement>");
                    return rasp;
                }
                using (EPay epay = new EPay())
                {
                    // facem plata:
                    RezultatBSO rezEcash = epay.Epay(tagClient, valoare.ToString("F2"), pin, dataFiscala, nBon.ToString(), detalii, sursa, utilizator);
                    if (rezEcash.obiect == null)
                    {
                        return DataTable2XmlDocument.Convert(null, "Eroare comunicare cu serverul de plati Ecash: " + rezEcash.mesaj);
                    }
                    dtEcash = (DataTable)rezEcash.obiect;
                    if (((int)dtEcash.Rows[0]["succes"]) < 0)
                    {
                        return DataTable2XmlDocument.Convert(dtEcash, "Tranzactie nereusita!");
                    }
                }
                // salvam in istoric:
                vfp.AccesSave(dataFiscala, utilizator, nBon.ToString(), dtProd.Rows[0], cant, pret, valoare.ToString());
                dtEcash.TableName = "plata";

                // generam o chitanta
                DataTable Settings = vfp.GetSettings();
                int nrex = 0;
                try { nrex = int.Parse(Settings.Rows[0]["accesnrex"].ToString()); }
                catch (Exception e) 
                {
                    Jurnal.Error(e.Message);
                }
                int nrcol = 32;
                string chitanta = (char)27 + vfp.newLine;
                for (int i = 1; i <= nrex; i++)
                {
                    chitanta += vfp.fontBold + Settings.Rows[0]["cdensoc"].ToString().Trim().PadToCenter(24, ' ') + vfp.fontNormal + vfp.newLine;
                    chitanta += Settings.Rows[0]["csediu"].ToString().Trim().PadToCenter(24,' ') + vfp.newLine;
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
                String vsidFile = Settings.Rows[0]["accesvsid"].ToString() + @"\" + numeUnic;
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
                    Jurnal.Write(errmes);
                    dtEcash.Rows[0]["numeclient"] = dtEcash.Rows[0]["numeclient"].ToString().Trim() + " [" + errmes + "]";
                }

            }
            return DataTable2XmlDocument.Convert(dtEcash, "Tranzactie nereusita!");
        }

        [WebMethod]
        public XmlDocument AccesGetCount(string par, string data, string tipAcces)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                {
                    RezultatBSO rez = vfp.AccesGetCount(data, tipAcces);
                    return DataTable2XmlDocument.Convert((DataTable)rez.obiect, "Eroare WS.AccesGetCount: " + rez.mesaj);
                }
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }

        //
        [WebMethod]
        public XmlDocument getVanzariDeschise(string par, string coduriGest)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetVanzariDeschise(coduriGest), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }
        [WebMethod]
        public XmlDocument getStariVanzari(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetStariVanzari(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }
        [WebMethod]
        public string nextStareVanz(string par, string uid)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par)) { return vfp.NextStareVanz(uid); }
                else { return "ERR_t."; }
            }
        }
        [WebMethod]
        public XmlDocument getUsers(string par)
        {
            using (PosMobil vfp = new PosMobil())
            {
                if (Settings.TokenIsOk(par))
                { return DataTable2XmlDocument.Convert(vfp.GetUsers(), "Eroare comunicare WinPOS."); }
                else
                { return DataTable2XmlDocument.Convert(null, "Eroare comunicare WinPOS_t."); }
            }
        }
    }
}