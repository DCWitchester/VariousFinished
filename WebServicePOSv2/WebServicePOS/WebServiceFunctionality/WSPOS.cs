using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebServicePOS.WebServiceFunctionality
{
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
            try
            {
                version = System.IO.File.ReadAllText(theFile).Trim();
            }
            catch (Exception e)
            {
                version = "ERR : " + e.Message;
            }
            return version;
        }
    }
}