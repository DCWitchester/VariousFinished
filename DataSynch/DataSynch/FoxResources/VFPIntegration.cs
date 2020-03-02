using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSynch.FoxResources
{
    class VFPIntegration
    {
        /// <summary>
        /// the main fox Separtor <= declared so i can change later
        /// </summary>
        static String foxSeparator = "*<Separator/>*";
        /// <summary>
        /// this will be the main list for all the vfp programs
        /// </summary>
        public static List<VFPProgram> vfpPrograms = new List<VFPProgram>();

        /// <summary>
        /// the main function for creating the list of vfpPrograms
        /// </summary>
        public static void CreateFunctionsList()
        {
            String foxManifest = ReadFoxManifest();
            String[] array = foxManifest.Split(new String[] { foxSeparator }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < array.Count(); i = i + 2)
                vfpPrograms.Add(new VFPProgram(ReturnStringTitle(array[i]), array[i + 1],SetFunctionToProgramList(array[i])));
        }
        /// <summary>
        /// this function will read the foxManifest and return the complete string to memory
        /// </summary>
        /// <returns></returns>
        static String ReadFoxManifest()
        {
            var assembly = Assembly.GetExecutingAssembly();
            String resourceName = assembly.GetManifestResourceNames().Single(str => str.Contains("foxPrograms.txt"));
            String programString = String.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)) return reader.ReadToEnd();
        }
        /// <summary>
        /// this fucntion will set the function for a given title for fast calls within list
        /// </summary>
        /// <param name="functionName">the function Name</param>
        /// <returns>the VfpFunction enum equivalency</returns>
        static VfpFunction SetFunctionToProgramList(String functionName)
        {
            //we will parse the enum
            foreach(VfpFunction function in Enum.GetValues(typeof(VfpFunction)))
            {
                //and return the equivalency when found
                if (function.ToString().ToUpper() == functionName.Split('.')[0].Replace('*',' ').Trim().ToUpper()) return function;
            }
            //if it is not found we will equivalate it with none
            return VfpFunction.none;
        }
        /// <summary>
        /// this fucntion will correct the title element for the program
        /// </summary>
        /// <param name="originalFileTitle">the original title retrieve from the title</param>
        /// <returns>the parsed title</returns>
        static String ReturnStringTitle(String originalFileTitle)
        {
            return originalFileTitle.Replace('*', ' ').Replace('\r', ' ').Replace('\n', ' ').Trim();
        }
    }
    /// <summary>
    /// this will be the main class for the program
    /// </summary>
    class VFPProgram
    {
        public VfpFunction Function { get; set; } = VfpFunction.none;
        public String FunctionName { get; set; } = String.Empty;
        public String FunctionText { get; set; } = String.Empty;
        public VFPProgram(String name, String text,VfpFunction function)
        {
            FunctionName = name;
            FunctionText = text;
            Function = function;
        }
    }
    /// <summary>
    /// the enum for the VFP functions
    /// </summary>
    enum VfpFunction
    {
        none = -1,
        mem2ini = 0,
        retreiveProducts = 1
    }
}
