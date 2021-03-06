using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ToDoListBL.Model;
using System.Runtime;
using System.Runtime.Serialization.Formatters.Binary;
using ToDoListBL.Serializer;

namespace ToDoListBL.Controllers
{
    public class TaskController
    {
        #region ------------------------Fields and properties-----------------------
        private ITaskSerializer serializer { get; set; }

        /// <summary>
        /// Loaded Task list.
        /// </summary>
        private List<Task> Tasks;

        /// <summary>
        /// Names of aviable lists.
        /// </summary>
        private List<string> listNames;

        private string listname;
        /// <summary>
        /// Selected name of task list.
        /// </summary>
        private string listName
        {
            get
            {
                return listname;
            }
            set
            {
                listname = value;
                listPath = value;
            }
        }

        
        private string listpath;
        /// <summary>
        /// List name decorated to path.
        /// </summary>
        private string listPath 
        { 
            get
            {
                return listpath;
            }
            set
            {
                listpath = Environment.CurrentDirectory + "\\TaskLists\\" +
                    value + ".dat";
            }
        }

        /// <summary>
        /// Selected task.
        /// </summary>
        private Task task { get; set; }

        /// <summary>
        /// Text edition filed.
        /// </summary>
        private StringEditor stringEditor { get; set; }

        #endregion

        /// <summary>
        /// Class for task managment. (Add/Save/Load/Edit)
        /// </summary>
        public TaskController()
        {
            AllTaskLists();
        }

        #region ------------TaskManagment-------------------------------------

        /// <summary>
        /// Serialize to binary format and save task to file.
        /// </summary>
        public void Save()
        {
            serializer = new ITaskSerializer();
            serializer.Serialize(Tasks, listName);
        }

        /// <summary>
        /// Load task from file.
        /// </summary>
        /// <returns></returns>
        public List<Task> Load()
        {
            serializer = new ITaskSerializer();
            return serializer.Deserialize(listName);
        }

        /// <summary>
        /// Get task list items.
        /// </summary>
        public List<string> TaskList()
        {
            List<string> s=new List<string>();
            foreach (Task t in Tasks)
            {
                s.Add(t.Name);
            }
            return s;
        }

        /// <summary>
        /// Add new task.
        /// </summary>
        /// <param name="taskName">Task name.</param>
        public string AddNewTask(string taskName)
        {
            if ((Tasks.Count == 0) || ((Tasks.Find(x => x.Name.Contains(taskName))) == null))
            {
                string text;
                Console.Write("Input text:");
                text = Console.ReadLine();
                Tasks.Add(new Task(taskName, new Content(text)));
                Save();
                return "Created";
            }
            return taskName + " is already there.";
        }

        /// <summary>
        /// Delete task from list.
        /// </summary>
        /// <param name="taskName">Task name.</param>
        public void DeleteTask(string taskName)
        {
            try
            {
                Tasks.Remove(Tasks.Find(x => x.Name.Equals(taskName)));
                if (task!=null&&task.Name.Equals(taskName))
                    task = null;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Task {taskName} not found.");
            }


        }

        /// <summary>
        /// Handle clearing selection.
        /// </summary>
        public void ClearTaskSelection()
        {
            task = null;
        }
        #endregion




        #region ------------TaskEditing---------------------------------------
        /// <summary>
        /// Show info of selected task.
        /// </summary>
        /// <param name="taskName">Task name.</param>
        /// <returns></returns>
        public string TaskInfo()
        {
            try
            {
                return "Task name: " + task.Name + "\n" +
                    "Content: " + task.Content.ToString() + "\n" +
                    "Data: " + task.CrTime
                    ?? throw new NullReferenceException("Select task.\n");
            }
            catch (NullReferenceException)
            {
                return "Please, select task.\n";
            }
        }

        /// <summary>
        /// Select task to edit.
        /// </summary>
        /// <param name="taskName">Task name.</param>
        public void SelectTask(string taskName)
        {
            task = Tasks.Find(x => x.Name == taskName);
            Console.WriteLine(task != null ? taskName + " selected." : "Task not found.");
        }

        /// <summary>
        /// Edit text of selected task.
        /// </summary>
        public string StringEdit()
        {
            try
            {
                stringEditor = new StringEditor(task.Content.Text);
                task.Content.Text = stringEditor.Str_();
                Tasks[Tasks.FindIndex(x => x.Name == task.Name)] = task;
                return "Edition completed";
            }
            catch (NullReferenceException)
            {
                return "Please, select task before start edition.";
            }
        }

        #endregion




        #region ------------ListManagment--------------
        /// <summary>
        /// Select task list for working.
        /// </summary>
        /// <param name="name">Task list name.</param>
        public void SelectTaskList(string name)
        {
            Tasks = new List<Task>();
            serializer = new ITaskSerializer();
            try
            {
                Tasks = serializer.Deserialize(listNames.Contains(name) ? name : throw new ArgumentNullException("Task list " + name + " not found."));
                listName = name;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.ParamName);  //TODO : перекинуть трай кэтч в Program
            }
        }

        public string AddNewTaskList(string name)
        {
            serializer = new ITaskSerializer();
            if (AllTaskLists().Contains(name))
                return "A list with same name already exists.";
            serializer.Serialize(new List<Task>(), name);
            return "New task list created.";
        }

        /// <summary>
        /// Return list names of existing task lists.
        /// </summary>
        /// <returns></returns>
        public List<string> AllTaskLists()  // Here I can use LINQ method ToList() for array->list transform, but not in this app.
        {
            listNames = new List<string>();
            if (!Directory.Exists(Environment.CurrentDirectory + "\\TaskLists"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\TaskLists");
            string[] array = Directory.GetFiles(Environment.CurrentDirectory + "\\TaskLists");

            foreach (string s in array)
            {
                listNames.Add(s.Substring((Environment.CurrentDirectory + "\\TaskLists ").Length,
                    (s).Length - (Environment.CurrentDirectory + "\\TaskLists ").Length - 4));
            }
            return listNames;   
        }

        /// <summary>
        /// Delete task list.
        /// </summary>
        /// <param name="name">Task list name to delete.</param>
        public void DeleteTaskList(string name)
        {
            if (!AllTaskLists().Contains(name))
                Console.WriteLine("List not found.");
            else
            {
                File.Delete(Environment.CurrentDirectory + "\\TaskLists\\" + name + ".dat");
                if (listName == name) //If deleted list was selected list.
                {
                    Tasks.Clear();
                    listName = null;
                }
                AllTaskLists();
            }
        }

        public string TaskListInfo()
        {
            if (listName != null)
            {
                string s = listName + "\n" +
                    "Creation time: " + File.GetCreationTime(listPath).ToString() +
                    "\nEdition time: " + File.GetLastWriteTime(listPath) + "\n";
                foreach(string l in TaskList())
                {
                    s += l+"\n";
                }
                return s;

            }
            else return new ArgumentNullException("Select task list.").ParamName;
        }

        public void ClearListSelection()
        {
            Tasks.Clear();
            listName = null;
        }

        #endregion
    }
}