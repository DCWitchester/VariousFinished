using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebServicePOS.WebServiceFunctionality
{
    public static class Jurnal
    {
        public static string tempfile = System.IO.Path.GetTempPath() + "\\WS_MentorPOS_log.txt";
        public static void Write(string mesaj)
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
        public static void Error(string mesaj)
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
}