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
        public static VisualFoxpro.FoxApplication FoxApplication  = new VisualFoxpro.FoxApplication();
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
        static void RetriveProducts()
        {
            //we set the default 
            FoxApplication.DefaultFilePath = defaultAppPath + @"\NOM\";
            //then we create the foxPrg
            String applicationPath = CreateFoxPrg(VfpFunction.retreiveProducts);
            //set the fox window to invisible
            FoxApplication.Visible = false;
            //and we run a cmd app
            FoxApplication.DoCmd("DO " + applicationPath);
        }
        public static void RetrieveProductsTest()
        {
#warning TBD: Remove before deployment
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            //we set the default 
            FoxApplication.DefaultFilePath = defaultAppPath + @"\NOM\";
            var t0 = stopwatch.ElapsedMilliseconds;
            //then we create the foxPrg
            String applicationPath = CreateFoxPrg(VfpFunction.retreiveProducts);
            var t1 = stopwatch.ElapsedMilliseconds;
            //set the fox window to invisible
            FoxApplication.Visible = false;
            var t2 = stopwatch.ElapsedMilliseconds;
            //and we run a cmd app
            FoxApplication.DoCmd("DO " + applicationPath);
            String v1 = "";
            var t3 = stopwatch.ElapsedMilliseconds;
            InsertProductTest(defaultAppPath + @"\NOM\produse.sql");
            var t4 = stopwatch.ElapsedMilliseconds;
            String pi = "";
        }
        /// <summary>
        /// this function will be the main creation of the vfpFileProgram
        /// </summary>
        /// <param name="vfpFunction">the function for which to create the prg</param>
        static String CreateFoxPrg(VfpFunction vfpFunction)
        {
            //first we will retrieve the needed element from the list
            VFPProgram vFPProgram = VFPIntegration.vfpPrograms.Where(x => x.Function == vfpFunction).FirstOrDefault();
            //then we will repair the program by replacing all placeHolders
            RepairVFPProgram(vFPProgram);
            //then we will create the appPath
            String functionPath = appPath + vFPProgram.FunctionName;
            //we create the file and append it with the neededText
            File.WriteAllText(functionPath, vFPProgram.FunctionText);
            //and finally return the prg path for we will need it
            return functionPath;
        }
        static void RepairVFPProgram(VFPProgram vFPProgram)
        {
            switch (vFPProgram.Function)
            {
                case VfpFunction.retreiveProducts:
#if (DEBUG)
                    vFPProgram.FunctionText = vFPProgram.FunctionText.Replace("<idPrefix>", "'1'");
#else
                    vFPProgram.FunctionText = vFPProgram.FunctionText.Replace("<idPrefix>", "'" + Settings.ServerSettings.dataSynch.LocalWorkStation.WorkStationID + "'");
#endif
                    break;
            }
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
        static void InsertProductTest(String programPath)
        {
#warning TBD: Remove before deployment
            String sqlQuery = File.ReadAllText(programPath);
            DatabaseControl.PosgreSqlConnection connection = new DatabaseControl.PosgreSqlConnection("Host = 5.2.228.239; Port = 26662; Database = TestDataSynch; User Id = postgres; Password = pgsql");
            connection.OpenConnection();
            connection.ExecuteNonQuery(sqlQuery);
            connection.CloseConnection();
        }

    }
}
