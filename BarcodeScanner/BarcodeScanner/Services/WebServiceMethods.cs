using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BarcodeScanner.Services
{
    class WebServiceMethods
    {
        public static String GetProductName(String productCode)
        {
            //we initialize a string for containing the retrieved xmlDocuments
            String xmlDocument = String.Empty;
            HttpClient http = new HttpClient();
            xmlDocument = http.GetStringAsync(WebMethods.GetProductDetails + productCode.Substring(0,12)).Result;
            //and deserialize the object to the needed structure
            return DeserializeDocument(xmlDocument);
                        
        }

        /// <summary>
        /// this function will deserialize the xml Menu Document
        /// </summary>
        /// <param name="xmlDocument">the retrieved XmlDocument</param>
        private static String DeserializeDocument(String xmlDocument)
        {
            //we initialize a serializer over the menu object
            XmlSerializer serializer = new XmlSerializer(typeof(XmlClasses.ProductDisplay));
            //initialize a new Meniu class
            XmlClasses.ProductDisplay product = new XmlClasses.ProductDisplay();
            //then using a text reader
            using (TextReader reader = new StringReader(xmlDocument))
            {
                //we deserialize the meniu to a class
                product = (XmlClasses.ProductDisplay)serializer.Deserialize(reader);
            }
            return product.ProductName;
        }


        public static void SendProductQunatitites(List<ObjectClasses.Products> products)
        {
            ObjectClasses.Qunatities quantities = new ObjectClasses.Qunatities();
            String json = quantities.SetQuantitiesFromProductList(products);

            HttpClient httpClient = new HttpClient();
            httpClient.GetAsync(WebMethods.SetProductQuantity + json);
        }
    }
}
