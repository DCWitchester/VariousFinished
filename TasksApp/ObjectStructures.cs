using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksApp
{
    public class ObjectStructures
    {
        public class Users
        {
            [Required(ErrorMessage = "Nu ati introdus numele de utilizator.")]
            public String UserName { get; set; } = String.Empty;
            [Required(ErrorMessage = "Nu ati introdus parola.")]
            public String Password { get; set; } = String.Empty;

            [Range(typeof(bool), "true", "true", ErrorMessage = "Combinatia utilizator\\parola nu exista.")]
            public Boolean isPasswordValid { get; set; }
            public void CheckPassword(String RetrivedPassword) 
            {
                isPasswordValid = RetrivedPassword == Password;
            }
            public String errorMessage { get; set; }
        }

        public class UserTask
        {
            [Required(ErrorMessage = "Taskul nou nu poate fi nimic.")]
            public String Task { get; set; } = String.Empty;
            public Int32 Task_Status { get; set; }
            public String Task_Info { get; set; } = String.Empty;

            public void SetUserTaskFromDatabaseTask(DatabaseControl.DatabaseObjects.Task currentTask)
            {
                Task = currentTask.CurrentTask;
                Task_Status = currentTask.TaskStatus;
                Task_Info = currentTask.Explanation;
            }

            public void SetDatabaseTaskFromUserTask(DatabaseControl.DatabaseObjects.Task currentTask)
            {
                currentTask.CurrentTask = Task;
                currentTask.TaskStatus = Task_Status;
                currentTask.Explanation = Task_Info;
            }
        }

        public class TasksTree
        {
            public DatabaseControl.DatabaseObjects.Task CurrentTask { get; set; }
            public IEnumerable<TasksTree> Children { get; set; }
            public String getColorForTask => PublicObjects.taskStatuses.Where(item => item.Id == CurrentTask.TaskStatus)
                                                                        .Select(item => item.Status_color).FirstOrDefault();
        }



    }
}
