using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace DataSynch.FoxResources
{
    class VFPCaller
    {
        /// <summary>
        /// the main VfpApllication environment for use within all the entire class
        /// </summary>
        static VisualFoxpro.FoxApplication FoxApplication;
        /// <summary>
        /// the main VfpApllication Environment DataPath
        /// </summary>
        //static String defaultAppPath = Settings.ServerSettings.dataSynch.LocalWorkStation.WorkStationFilePath;
        static String defaultAppPath = @"E:\(MENTOR)\#DATE\E\(SERVER)";
        static String appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\";
        void RetrieveNomenclators()
        {
#warning TBD Nomenclatoare
        }
        /// <summary>
        /// this function will retrieve the products into a sqlFile
        /// </summary>
        public static void RetriveProducts()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            FoxApplication = new VisualFoxpro.FoxApplication();
            var t1 = watch.ElapsedMilliseconds;
            FoxApplication.DefaultFilePath = defaultAppPath + @"\NOM\";
            String applicationPath = CreateFoxPrg(VfpFunction.retreiveProducts);
            var t2 = watch.ElapsedMilliseconds;
            //FoxApplication.Visible = false;
            FoxApplication.Visible = true;
            FoxApplication.Width = 1000;
            FoxApplication.Height = 1000;
            var t3 = watch.ElapsedMilliseconds;
            FoxApplication.DoCmd("DO " + applicationPath);
            var t4 = watch.ElapsedMilliseconds;
            String x = "";
        }
        /// <summary>
        /// this function will be the main creation of the vfpFileProgram
        /// </summary>
        /// <param name="vfpFunction">the function for which to create the prg</param>
        static String CreateFoxPrg(VfpFunction vfpFunction)
        {
            //first we will retrieve the needed element from the list
            VFPProgram vFPProgram = VFPIntegration.vfpPrograms.Where(x => x.Function == vfpFunction).FirstOrDefault();
            //then we will create the appPath
            String functionPath = appPath + vFPProgram.FunctionName;
            //we create the file and append it with the neededText
            File.WriteAllText(functionPath, vFPProgram.FunctionText);
            //and finally return the prg path for we will need it
            return functionPath;
        }
        /// <summary>
        /// this function will clear the programs within the path
        /// </summary>
        static void ClearPrograms()
        {
            //we create a directory info for the executing directory
            DirectoryInfo directoryInfo = new DirectoryInfo(appPath);
            //then foreach *.prg or *.fxp file
            foreach (FileInfo file in directoryInfo.GetFiles().Where(f => f.Extension == ".prg" || f.Extension == ".fxp"))
            {
                //we try to delete it
                try
                {
                    File.Delete(file.FullName);
                }
                catch { }
            }
        }
    }
}
