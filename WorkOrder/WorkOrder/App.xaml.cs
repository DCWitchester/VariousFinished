using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.FSharp.Core;
using Microsoft.FSharp.Collections;
using FsharpLibrary;

namespace WorkOrder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// this function will check if the program has another instance running
        /// </summary>
        void CheckUniqueEntry()
        {
            bool createdNew = false;
            string mutexName = System.Reflection.Assembly.GetExecutingAssembly().GetType().GUID.ToString();
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(false, mutexName, out createdNew))
            {
                if (!createdNew)
                {
                    Messages.Message.StartupError();
                    Miscellaneous.ProgramClose();
                }
            }
            //Test1
            //List<ObjectStructures.Task> taskList = new List<ObjectStructures.Task>();
            //ObjectStructures.Task task = new ObjectStructures.Task(2, 2, "", 2, "", false);
            //taskList.Add(task);
            //ObjectStructures.taskList = Microsoft.FSharp.Collections.ListModule.OfSeq(ObjectStructures.taskList.Append(task).ToList<ObjectStructures.Task>());
            //ObjectStructures.taskList = ObjectStructures.AddTask(ObjectStructures.taskList,task);
            //ObjectStructures.Task task1 = new ObjectStructures.Task(3, 3, "", 3, "", false);
            //ObjectStructures.AddTask(task1);
            //ObjectStructures.AddTask(task);
            List<FsharpLibrary.DatabaseObjects.User> userlist = FsharpLibrary.DatabaseConnection.UserFunctuins.getAllUsers().ToList();
            DatabaseObjects.User user = new DatabaseObjects.User(0,"mentor","rotnem","");
            DatabaseObjects.User user1 = new DatabaseObjects.User(0, "mentor1", "rotnem", "");
            bool b = DatabaseConnection.UserFunctuins.checkUser(user1);
            bool a = DatabaseConnection.UserFunctuins.checkUser(user);
            string x = "";
            bool y = false && false;
            bool z = false & false;
        }
        //the override is used to launch a separate .cs as a startup.
        void ProgramStartup(object sender, StartupEventArgs e)
        {
            SetCultureInfo();
            CheckUniqueEntry();
        }
        /// <summary>
        /// this function will change the culture environment to international english: en-IN
        /// </summary>
        void SetCultureInfo()
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-IN");
            System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("en-IN");
        }
    }
}
