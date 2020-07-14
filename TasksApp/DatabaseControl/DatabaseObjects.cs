using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksApp.DatabaseControl
{
    public class DatabaseObjects
    {
        /// <summary>
        /// the main User Class for use within the program
        /// </summary>
        public class User
        {
            /// <summary>
            /// the class ID
            /// </summary>
            public Int32 Id { get; set; } = new Int32();
            /// <summary>
            /// the username
            /// </summary>
            public String Username { get; set; } = String.Empty;
            /// <summary>
            /// the password
            /// </summary>
            public String Password { get; set; } = String.Empty;
            /// <summary>
            /// the display name 
            /// </summary>
            public String DisplayName { get; set; } = String.Empty;
            /// <summary>
            /// the e-mail adress
            /// </summary>
            public String Email { get; set; } = String.Empty;
            public Boolean IsLogged { get; set; } = new Boolean();
        }
        public class Task
        {
            public Int32 Id { get; set; } = new Int32();
            public String CurrentTask { get; set; } = String.Empty;
            public Int32 ParentTask { get; set; } = new Int32();
            public Int32 TaskStatus { get; set; } = new Int32();
            public String Explanation { get; set; } = String.Empty;
        }
        public class TaskStatus
        {
            public Int32 Id { get; set; } = new Int32();
            public String Status_code { get; set; } = String.Empty;
            public String Status_text { get; set; } = String.Empty;
            public String Status_color { get; set; } = String.Empty;
        }
    }
}
