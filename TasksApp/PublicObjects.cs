using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksApp
{
    /// <summary>
    /// this class will contain all public objects meant for use in multiple forms
    /// </summary>
    public class PublicObjects
    {
        /// <summary>
        /// the main structure for the loggedInUser
        /// </summary>
        public static DatabaseControl.DatabaseObjects.User loggedInUser = new DatabaseControl.DatabaseObjects.User();
        /// <summary>
        /// the complete list of taskTree for the loggedInUser
        /// </summary>
        public static List<ObjectStructures.TasksTree> tasksTrees = new List<ObjectStructures.TasksTree>();
        /// <summary>
        /// the complete list of taskStatuses
        /// </summary>
        public static List<DatabaseControl.DatabaseObjects.TaskStatus> taskStatuses = new List<DatabaseControl.DatabaseObjects.TaskStatus>();
    }
}
