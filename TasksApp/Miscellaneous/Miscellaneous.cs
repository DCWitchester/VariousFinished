using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksApp.Miscellaneous
{
    public class Miscellaneous
    {
        public enum PageOperations
        {
            None = 0,
            View = 1,
            Add = 2,
            AddSubtask = 3,
            Edit = 4
        }
        public static IList<ObjectStructures.TasksTree> createTaskTreeFromTasks(List<DatabaseControl.DatabaseObjects.Task> tasks)
        {
            List<ObjectStructures.TasksTree> tasksTrees = new List<ObjectStructures.TasksTree>();
            foreach(DatabaseControl.DatabaseObjects.Task task in tasks.Where(x=>x.ParentTask == 0))
            {
                tasksTrees.Add(generateTreeItem(task, tasks));
            }
            return tasksTrees.OrderBy(item=>item.CurrentTask.Id).ToList();
        }
        public static ObjectStructures.TasksTree generateTreeItem(DatabaseControl.DatabaseObjects.Task task, List<DatabaseControl.DatabaseObjects.Task> tasks)
        {
            ObjectStructures.TasksTree tasksTree = new ObjectStructures.TasksTree();
            List<ObjectStructures.TasksTree> ChildrenList = new List<ObjectStructures.TasksTree>();
            tasksTree.CurrentTask = task;
            foreach(DatabaseControl.DatabaseObjects.Task childTask in tasks.Where(x=>x.ParentTask == task.Id))
            {
                var test = generateTreeItem(childTask, tasks);
                ChildrenList.Add(generateTreeItem(childTask, tasks));
            }
            tasksTree.Children = ChildrenList.AsEnumerable();
            return tasksTree;
        }
    }
}
