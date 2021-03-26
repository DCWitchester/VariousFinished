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
using System.Xml.Serialization;
using WebServicePOS.Auxiliary;
using WebServicePOS.Setari;
using WebServicePOS.Firebird;
using static WebServicePOS.Auxiliary.Enumerators;
using WebServicePOS.WebServiceFunctionality;

namespace WebServicePOS
{

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