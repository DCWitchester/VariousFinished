using System;
using Npgsql;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksApp.DatabaseControl
{
    /// <summary>
    /// the main Database Link Class for containing all functions that link to the database
    /// </summary>
    public class DatabaseLink
    {
        /// <summary>
        /// the central npgSqlConnection that will be used by all functions
        /// </summary>
        public static DatabaseController.PosgreSqlConnection npgSqlConnection = new DatabaseController.PosgreSqlConnection();
        #region Connection Close Function
        /// <summary>
        /// the main function for closing a connection on success
        /// </summary>
        /// <returns>true</returns>
        public static Boolean NormalConnectionClose()
        {
            npgSqlConnection.CloseConnection();
            return true;
        }
        /// <summary>
        /// the main function dor closing a connection on error
        /// </summary>
        /// <returns>false</returns>
        public static Boolean ErrorConnectionClose()
        {
            npgSqlConnection.CloseConnection();
            return false;
        }
        #endregion
        public class UserFunctions
        {
            /// <summary>
            /// this function will check if there is a valid user for this username/password combo
            /// </summary>
            /// <param name="user"></param>
            public static void CheckUser(ObjectStructures.Users user)
            {
                String sqlCommand = "SELECT COUNT(*) FROM utilizatori WHERE (nume_utilizator = :p_username OR email = :p_email) AND (parola = :p_password) AND activ";
                NpgsqlParameter[] npgsqlParameters =
                {
                    new NpgsqlParameter(":p_username",user.UserName),
                    new NpgsqlParameter(":p_email",user.UserName),
                    new NpgsqlParameter(":p_password",user.Password)
                };
                if (!npgSqlConnection.OpenConnection())
                {
                    user.isPasswordValid = false;
                    return;
                }
                if ((Int64)npgSqlConnection.ExecuteScalar(sqlCommand, npgsqlParameters) > 0) user.isPasswordValid = NormalConnectionClose();
                else user.isPasswordValid = ErrorConnectionClose();
                return;
            }
            public static Boolean RetrieveUser(ObjectStructures.Users user, DatabaseObjects.User databaseUser) 
            {
                String sqlCommand = "SELECT id,nume_utilizator, parola, display_name, email FROM utilizatori WHERE (nume_utilizator = :p_username OR email = :p_email) AND (parola = :p_password) AND activ";
                NpgsqlParameter[] npgsqlParameter =
                {
                    new NpgsqlParameter(":p_username",user.UserName),
                    new NpgsqlParameter(":p_email",user.UserName),
                    new NpgsqlParameter(":p_password",user.Password)
                };
                if (!npgSqlConnection.OpenConnection()) return false;
                DataTable result = npgSqlConnection.ExecuteReaderToDataTable(sqlCommand,npgsqlParameter);
                if(result != null && result.Rows.Count > 0)
                {
                    databaseUser.Id = (Int32)result.Rows[0]["ID"];
                    databaseUser.DisplayName = result.Rows[0]["DISPLAY_NAME"].ToString();
                    databaseUser.Username = result.Rows[0]["NUME_UTILIZATOR"].ToString();
                    databaseUser.Password = result.Rows[0]["PAROLA"].ToString();
                    databaseUser.Email = result.Rows[0]["EMAIL"].ToString();
                    return NormalConnectionClose();
                }
                return ErrorConnectionClose();

            }
        }
        public class TaskFunctions
        {
            /// <summary>
            /// this function will retrieve a complete list of tasks for a given user
            /// </summary>
            /// <param name="user">the user for which we retrieve the tasks</param>
            /// <param name="tasks">the new task list</param>
            /// <returns>the state of the query</returns>
            public static Boolean RetrieveTasksForUser(DatabaseObjects.User user,List<DatabaseObjects.Task> tasks)
            {
                String sqlCommand = "SELECT tasks.id,tasks.task,tasks.parent_task,tasks.task_status " +
                                        "FROM tasks " +
                                        "LEFT JOIN utilizatori ON tasks.creator = utilizatori.id " +
                                        "LEFT JOIN shared_tasks ON shared_tasks.task_id = tasks.id " +
                                        "WHERE (tasks.creator = :p_creator_id " +
                                        "OR tasks.is_public " +
                                        "OR (shared_tasks.shared_with = :p_creator_id AND shared_tasks.activ)) " +
                                        "AND tasks.activ";
                NpgsqlParameter[] npgsqlParameters =
                {
                    new NpgsqlParameter(":p_creator_id",user.Id)
                };
                if (!npgSqlConnection.OpenConnection()) return false;
                DataTable result = npgSqlConnection.ExecuteReaderToDataTable(sqlCommand,npgsqlParameters);
                if (result == null || result.Rows.Count == 0) return ErrorConnectionClose();
                foreach(DataRow dataRow in result.Rows)
                {
                    DatabaseObjects.Task task = new DatabaseObjects.Task
                    {
                        Id = (Int32)dataRow["ID"],
                        CurrentTask = dataRow["TASK"].ToString(),
                        ParentTask = (Int32)dataRow["PARENT_TASK"],
                        TaskStatus = (Int32)dataRow["TASK_STATUS"]
                    };
                    tasks.Add(task);
                }
                return NormalConnectionClose();
            }
            /// <summary>
            /// this function will retrieve the complete list of tasks
            /// </summary>
            /// <returns>the state of the query</returns>
            public static Boolean RetrieveTaskStatus()
            {
                PublicObjects.taskStatuses = new List<DatabaseObjects.TaskStatus>();
                String sqlCommand = "SELECT * FROM task_status";
                if (!npgSqlConnection.OpenConnection()) return false;
                DataTable result = npgSqlConnection.ExecuteReaderToDataTable(sqlCommand);
                if (result == null || result.Rows.Count == 0) return ErrorConnectionClose();
                foreach(DataRow dataRow in result.Rows)
                {
                    DatabaseObjects.TaskStatus taskStatus = new DatabaseObjects.TaskStatus
                    {
                        Id = (Int32)dataRow["ID"],
                        Status_code = dataRow["STATUS_CODE"].ToString(),
                        Status_text = dataRow["STATUS_TEXT"].ToString(),
                        Status_color = dataRow["STATUS_COLOR"].ToString()
                    };
                    PublicObjects.taskStatuses.Add(taskStatus);
                }
                return NormalConnectionClose();
            }

            public static Boolean RetrieveTask(Int32 taskID, DatabaseObjects.Task currentTask)
            {
                String sqlCommand = "SELECT * FROM tasks WHERE id = :p_task_id";
                NpgsqlParameter[] queryParameters = 
                { 
                    new NpgsqlParameter(":p_task_id", taskID) 
                };
                if (!npgSqlConnection.OpenConnection()) return false;
                DataTable result = npgSqlConnection.ExecuteReaderToDataTable(sqlCommand, queryParameters);
                if (result != null || result.Rows.Count > 0)
                {
                    currentTask.Id = (Int32)result.Rows[0]["ID"];
                    currentTask.CurrentTask = result.Rows[0]["TASK"].ToString();
                    currentTask.TaskStatus = (Int32)result.Rows[0]["TASK_STATUS"];
                    currentTask.ParentTask = (Int32)result.Rows[0]["PARENT_TASK"];
                    currentTask.Explanation = result.Rows[0]["EXPLICATII"].ToString();
                    return NormalConnectionClose();
                }
                return ErrorConnectionClose();
            }

            public static Boolean UpdateTask(DatabaseObjects.Task task)
            {
                String sqlCommand = @"UPDATE tasks 
                                        SET task = :p_task, task_status = :p_task_status, explicatii = :p_explicatii 
                                        WHERE id = :p_id";
                NpgsqlParameter[] queryParameters =
                {
                    new NpgsqlParameter(":p_task",task.CurrentTask),
                    new NpgsqlParameter(":p_task_status",task.TaskStatus),
                    new NpgsqlParameter(":p_explicatii",task.Explanation),
                    new NpgsqlParameter(":p_id",task.Id)
                };
                if (npgSqlConnection.OpenConnection()) 
                { 
                    npgSqlConnection.ExecuteNonQuery(sqlCommand, queryParameters);
                    return NormalConnectionClose();
                }
                return ErrorConnectionClose();
            }

            public static Boolean InsertTask(DatabaseObjects.Task task)
            {
                String sqlCommand = @"INSERT INTO tasks(task,task_status,explicatii,creator,is_public,parent_task) 
                                        VALUES(:p_task,:p_task_status,:p_explicatii,:p_creator,:p_is_public,:p_parent)";
                NpgsqlParameter[] queryParameters =
                {
                    new NpgsqlParameter(":p_task",task.CurrentTask),
                    new NpgsqlParameter(":p_task_status",task.TaskStatus),
                    new NpgsqlParameter(":p_explicatii",task.Explanation),
                    new NpgsqlParameter(":p_creator",PublicObjects.loggedInUser.Id),
                    new NpgsqlParameter(":p_is_public",true),
                    new NpgsqlParameter(":p_parent",task.ParentTask),
                    new NpgsqlParameter(":p_id",task.Id)
                };
                if (npgSqlConnection.OpenConnection())
                {
                    npgSqlConnection.ExecuteNonQuery(sqlCommand, queryParameters);
                    return NormalConnectionClose();
                }
                return ErrorConnectionClose();
            }
        }
    }
}
