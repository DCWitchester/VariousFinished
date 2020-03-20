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
#if !DEBUG
        /// <summary>
        /// the main VfpApllication Environment DataPath
        /// </summary>
        static String defaultAppPath = Settings.ServerSettings.dataSynch.LocalWorkStation.WorkStationFilePath;
#else
        static String defaultAppPath = @"E:\(MENTOR)\#DATE\E\(SERVER)";
#endif
        /// <summary>
        /// the path towards the app install directory
        /// </summary>
        static String appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\";
        static String sqlPath = appPath + @"sql\";
        /// <summary>
        /// the main schedchuler for controling all data Retrieval
        /// </summary>
        public static void RetriveLocalFiles()
        {
            //we create a new Task Array
            //tried to create a list but control falls upon creating the items
            Task[] tasks = new Task[2];
            //then we run the task factory for document retrieval
            tasks[0] = Task.Factory.StartNew(() => RetrieveDocuments());
            //and the one for nomenclator retrival
            tasks[1] = Task.Factory.StartNew(() => RetrieveNomenclators());
            //and await for their return
            Task.WaitAll(tasks);
        }
        #region Retrieve Documents
        /// <summary>
        /// the function used for Document Retrieval
        /// </summary>
        static void RetrieveDocuments()
        {
#warning  TBD Documents
            RetrieveSales();
        }
        /// <summary>
        /// this function will parse the local DBF directory for all sales 
        /// </summary>
        static void RetrieveSales()
        {
            //we create a directory info for the DBF folder
            DirectoryInfo driveInfo = new DirectoryInfo(defaultAppPath + @"\DBF\");
            //retrieve the list of files FAMMyyyy.dbf that have suffered alterations since the last update
            foreach (String salesFile in driveInfo.GetFiles().Where(x => x.Name.StartsWith("FA"))
                                                            .Where(x => x.Extension == "dbf")
                                                            .Where(x => x.LastWriteTime > Settings.Settings.programSettings.LastUpdateTime)
                                                            .Select(x => x.FullName))
                RetriveSalesFromFile(salesFile);
            //and foreach file we of the files we retrieve the sales into an sql File

        }
        /// <summary>
        /// this function will retrieve from a given file all altered elements
        /// </summary>
        /// <param name="dbfFile">the file path that needs to be accesed</param>
        static void RetriveSalesFromFile(String dbfFile)
        {
            //we set the default 
            FoxApplication.DefaultFilePath = defaultAppPath + @"\DBF\";
            //then we create the foxPrg
            String applicationPath = CreateFoxPrg(VfpFunction.retreiveSales);
            //we make corrections for the respective file
            File.WriteAllText(applicationPath, File.ReadAllText(applicationPath).Replace("<SalesFile>", dbfFile));
            //set the fox window to invisible
            FoxApplication.Visible = false;
            //and we run a cmd app
            FoxApplication.DoCmd("DO " + applicationPath);
        }

        #endregion
        #region Retrieve Nomenclators
        /// <summary>
        /// the main path for Nomenclators Retrieval
        /// </summary>
        static void RetrieveNomenclators()
        {
#warning TBD Nomenclatoare
            RetrieveProducts();
        }
        /// <summary>
        /// this function will retrieve the products into a sqlFile
        /// </summary>
        static void RetrieveProducts()
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
        #endregion
        #region
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
        /// <summary>
        /// this function is used to Repair the given function by replacing all the placeHolders within the String
        /// </summary>
        /// <param name="vFPProgram">the VfpProgram</param>
        static void RepairVFPProgram(VFPProgram vFPProgram)
        {
            switch (vFPProgram.Function)
            {
                case VfpFunction.retreiveProducts:
#if (DEBUG)
                    vFPProgram.FunctionText = vFPProgram.FunctionText.Replace("<deviceID>", "1");
#else
                    vFPProgram.FunctionText = vFPProgram.FunctionText.Replace("<deviceID>", Settings.ServerSettings.dataSynch.LocalWorkStation.WorkStationID.ToString());
#endif
                    vFPProgram.FunctionText = vFPProgram.FunctionText.Replace("<FileName>", appPath + @"\produse" + DateTime.Now.Ticks.ToString() + ".sql");
                    break;
                case VfpFunction.retreiveSales:
#if (DEBUG)
                    vFPProgram.FunctionText = vFPProgram.FunctionText.Replace("<deviceID>", "1");
#else
                    vFPProgram.FunctionText = vFPProgram.FunctionText.Replace("<deviceID>", Settings.ServerSettings.dataSynch.LocalWorkStation.WorkStationID.ToString());
#endif
                    vFPProgram.FunctionText = vFPProgram.FunctionText.Replace("<FileName>", appPath + @"\vanzari" + DateTime.Now.Ticks.ToString() + ".sql");
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
        #endregion
        #region Testing Area
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
        static void InsertProductTest(String programPath)
        {
            #warning TBD: Remove before deployment
            String sqlQuery = File.ReadAllText(programPath);
            DatabaseControl.PosgreSqlConnection connection = new DatabaseControl.PosgreSqlConnection("Host = 5.2.228.239; Port = 26662; Database = TestDataSynch; User Id = postgres; Password = pgsql");
            connection.OpenConnection();
            connection.ExecuteNonQuery(sqlQuery);
            connection.CloseConnection();
        }
        #endregion
    }
}