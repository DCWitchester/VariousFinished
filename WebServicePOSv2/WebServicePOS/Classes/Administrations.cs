using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServicePOS.Classes
{
    /// <summary>
    /// the XmlRoot for the Administrations List
    /// </summary>
    [XmlRoot(ElementName = "Gest")]
    public class Administrations
    {
        public List<AdministrationItem> administrations { get; set; } = new List<AdministrationItem>();

        public void SetAdministrationsFromDataTable(DataTable dt)
        {
            foreach (DataRow element in dt.Rows)
            {
                administrations.Add(new AdministrationItem
                {
                    AdminitrationCode = element[0].ToString(),
                    AdministrationName = element[1].ToString(),
                    DisplayOrder = (Int32)((Decimal)element[2])
                });
            }
        }
    }
}