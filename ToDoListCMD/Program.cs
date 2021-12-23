using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoListBL.Model;
using ToDoListBL.Controllers;

namespace ToDoListCMD
{
    class Program
    {
        static TaskController Controller;
        static void Main(string[] args)
        {

            Controller = new TaskController();
            // Interfaces will be called in turn.
            // This algoritm introducted instead of 
            // recursive calling new methods inside them.
            while (true)
            {
                ArrowTaskListsInterface(ref Controller);
                ArrowTasksInterface(ref Controller);
            }


        }


#region ----------------oldInterface----------------
        /// <summary>
        /// Activate interface for working with selected task list.
        /// </summary>
        /// <param name="controller">Your task controller.</param>
        public static void TasksInterface(ref TaskController controller)
        {
            Console.WriteLine("0 - HELP");
            do
            {
                var k = Console.ReadKey();
                Console.Clear();
                switch (k.Key)
                {
                    case ConsoleKey.D0:
                        Console.WriteLine("HELP:\n" +
                            "0 - HELP\n" +
                            "1 - Add new task\n" +
                            "2 - Delete task\n" +
                            "3 - Save changes\n" +
                            "4 - Select task\n" +
                            "5 - Edit task\n" +
                            "6 - Show task info\n" +
                            "7 - Get task list\n" +
                            "Esc - Return to list of task list\n");
                        continue;
                    case ConsoleKey.Escape:
                        Console.WriteLine("Do you want save changes before exit list? Y/another key");
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            controller.Save();
                            controller.ClearTaskSelection();
                        }
                        break;
                    case ConsoleKey.D1:
                        Console.Write("Task name: ");
                        controller.AddNewTask(Console.ReadLine()); // Add new task.
                        WriteHelp();
                        continue;
                    case ConsoleKey.D2:                                 // TODO: Arrow selection.
                        Console.Write("Input task name to delete: ");
                        controller.DeleteTask(Console.ReadLine()); // Delete. 
                        WriteHelp();
                        continue;
                    case ConsoleKey.D3:
                        controller.Save(); // Serialize and save to dat file.
                        WriteAnyKey();
                        WriteHelp();
                        Console.WriteLine("Saved.");
                        continue;
                    case ConsoleKey.D4:                                 // TODO: Arrow selection.
                        Console.Write("Input task name to select: ");
                        controller.SelectTask(Console.ReadLine()); // Select task.
                        WriteHelp();
                        continue;
                    case ConsoleKey.D5:
                        controller.StringEdit(); //Edit selected task content text.
                        WriteHelp();
                        continue;
                    case ConsoleKey.D6:
                        WriteHelp();
                        controller.TaskInfo(); // Show selected task info.
                        continue;
                    case ConsoleKey.D7:
                        WriteHelp();
                        controller.TaskList(); // Show task list.
                        continue;
                    default:
                        WriteHelp(" DEFAULT CASE.");
                        continue;
                }
                break;
            }
            while (true);
            Console.Clear();
            controller.ClearListSelection();
            //Controller = controller;
            //TaskListsInterface(controller);
        }

        /// <summary>
        /// Activate interface for working with list of task list.
        /// </summary>
        /// <param name="controller">Your task controller.</param>
        public static void TaskListsInterface(ref TaskController controller)
        {
            Console.WriteLine("0 - HELP");
            do
            {
                var k = Console.ReadKey();
                Console.Clear();
                switch (k.Key)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine("Do you want to exit? Y/another key");
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                            Environment.Exit(0);
                        WriteHelp();
                        continue;
                    case ConsoleKey.D0:
                        Console.WriteLine("HELP:\n" +
                            "0 - HELP\n" +
                            "1 - Add new task list\n" +
                            "2 - Delete task ;ist\n" +
                            "3 - Save changes\n" +
                            "4 - Select task list\n" +
                            "5 - Edit list of task list\n" +
                            "6 - Show task info\n" +
                            "7 - Get list of task list\n" +
                            "Esc - Exit app\n");
                        continue;
                    case ConsoleKey.D1:
                        Console.WriteLine("Input list name: ");
                        controller.AddNewTaskList(Console.ReadLine());
                        WriteAnyKey();
                        WriteHelp();
                        continue;
                    case ConsoleKey.D2:
                        Console.WriteLine("Input list name to delete: ");
                        controller.DeleteTaskList(Console.ReadLine());
                        WriteAnyKey();
                        WriteHelp();
                        continue;
                    case ConsoleKey.D3:
                        continue;
                    case ConsoleKey.D4:
                        Console.WriteLine("Input list name to select");
                        controller.SelectTaskList(Console.ReadLine());
                        WriteAnyKey();
                        WriteHelp();
                        continue;
                    case ConsoleKey.D5:
                        break;
                    case ConsoleKey.D6:
                        controller.TaskListInfo();
                        WriteAnyKey();
                        WriteHelp();
                        continue;
                    case ConsoleKey.D7:
                        Console.WriteLine("Yor lists:");
                        foreach (string n in controller.AllTaskLists())
                        {
                            Console.WriteLine(n);
                        }
                        WriteAnyKey();
                        WriteHelp();
                        continue;
                    default:
                        WriteHelp(" DEFAULT CASE.");
                        continue;
                }
                break;
            }
            while (true);
            //Controller = controller;
            Console.Clear();
            //TasksInterface(controller);
        }

        /// <summary>
        /// Write help header string.
        /// </summary>
        /// <param name="s">Helper addition.</param>
        public static void WriteHelp(string s = "")
        {
            Console.Clear();
            Console.WriteLine("0 - HELP\n" + s);
        }

        public static void WriteAnyKey()
        {
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Some testing instructions.
        /// </summary>
        public static void DeveloperFunction()
        {
            CMessage cMessage = new CMessage();
        }

#endregion

        #region ----------experimentalInterface----------

        /// <summary>
        /// Activate Task menu.
        /// </summary>
        /// <param name="controller">Using TaskController with saving state.</param>
        public static void ArrowTasksInterface(ref TaskController controller)
        {
            int maxCaret = WriteTaskMenu();
            Console.CursorTop = 1;
            int caret = Console.CursorTop;
            do
            {
                ArrowSelector(ref caret, maxCaret);
                switch(caret)
                {
                    case 1: // Add new task.
                        WriteConsole("Task name: ");
                        WriteConsole(controller.AddNewTask(Console.ReadLine()),true);
                        WriteTaskMenu();
                        continue;
                    case 2:
                        {
                            WriteConsole("Select task to delete:");
                            int maxsCaret = WriteList(ref controller, controller.TaskList()) - 1;
                            //Console.CursorTop++;
                            ArrowSelector(ref caret, maxsCaret);
                            controller.DeleteTask(controller.TaskList()[caret - 1]);
                            WriteConsole("Task deleted.", true);
                            WriteTaskMenu();
                            continue;
                        }
                    case 3:
                        controller.Save(); // Serialize and save to dat file.
                        WriteConsole("Saved.", true);
                        WriteTaskMenu();
                        continue;
                    case 4:     //Select task to edit or take info.
                        {
                            WriteConsole("Select task:");
                            int maxsCaret = WriteList(ref controller, controller.TaskList()) - 1;
                            //Console.CursorTop++;
                            ArrowSelector(ref caret, maxsCaret);
                            controller.SelectTask(controller.TaskList()[caret - 1]);
                            WriteTaskMenu();
                            continue;
                        }
                    case 5:     //Edit selected task content text.
                        WriteConsole(controller.StringEdit(),true);
                        WriteTaskMenu();
                        continue;
                    case 6: // Show selected task info.
                        WriteConsole(controller.TaskInfo(),true);
                        WriteTaskMenu();
                        continue;
                    case 7:         // Show task list.
                        WriteList(ref controller, controller.TaskList(),true);
                        WriteTaskMenu();
                        continue;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("Do you want save changes before exit list? Y/another key");
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            controller.Save();
                            controller.ClearTaskSelection();
                            return;
                        }
                        WriteTaskMenu();
                        continue;   
                }

            } while (true);
        }

        /// <summary>
        /// Activate Lists menu.
        /// </summary>
        /// <param name="controller">Using TaskController with saving state.</param>
        public static void ArrowTaskListsInterface(ref TaskController controller)
        {
            int maxCaret = WriteTaskListsMenu();
            Console.CursorTop = 1;
            int caret = Console.CursorTop;
            do
            {
                ArrowSelector(ref caret, maxCaret);
                        switch(caret)
                        {
                            case 1:
                        {
                            WriteConsole("Input list name: ");
                            Console.CursorTop++;
                            WriteConsole(controller.AddNewTaskList(Console.ReadLine()),true);
                            WriteTaskListsMenu();
                            continue;
                        }
                            case 2:
                        {
                            WriteConsole("Select task list to delete:");
                            int maxsCaret = WriteList(ref controller, controller.AllTaskLists()) - 1;
                            ArrowSelector(ref caret, maxsCaret);
                            controller.DeleteTaskList(controller.AllTaskLists()[caret - 1]);
                            WriteConsole("Task deleted.", true);
                            WriteTaskListsMenu();
                            continue;
                        }
                            case 3:
                                continue;
                            case 4:
                        {
                            WriteConsole("Select task list:");
                            int maxsCaret = WriteList(ref controller, controller.AllTaskLists()) - 1;
                            ArrowSelector(ref caret, maxsCaret);
                            controller.SelectTaskList(controller.AllTaskLists()[caret - 1]);
                            WriteTaskListsMenu();
                            continue;
                        }
                            case 5:
                                break;
                            case 6:
                                try
                                {
                                    WriteConsole("Task info:\n" + controller.TaskListInfo(),true);
                                }
                                catch(ArgumentNullException ex)
                                {
                                    WriteConsole(ex.ParamName,true);
                                }
                                WriteTaskListsMenu();
                                continue;
                            case 7:
                        {
                            WriteList(ref controller, controller.AllTaskLists(), true);
                            WriteTaskListsMenu();
                            continue;
                        }
                            case 8:
                        {
                            ExitMenu();
                            WriteTaskListsMenu();
                            continue;
                        }
                            default:
                                continue;
                        }
                        break;
            }
            while (true);
        }

        /// <summary>
        /// Console.WriteLine decoration with clearing console.
        /// </summary>
        /// <param name="s">Text to write.</param>
        /// <param name="anykey">This key created for waiting option
        /// after writing text.</param>
        /// <returns>Caret state.</returns>
        public static int WriteConsole(string s="", bool anykey=false)
        {
            Console.Clear();
            Console.WriteLine(s);
            if(anykey == true)
            {
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }
            int caret = Console.CursorTop - 1;
            Console.CursorTop = 1;
            return caret;
        }

        /// <summary>
        /// Write Task menu items.
        /// </summary>
        /// <returns></returns>
        public static int WriteTaskMenu()
        {
            return WriteConsole(
                            "Lists menu:\n" +
                            "1 - Add new task\n" +
                            "2 - Delete task\n" +
                            "3 - Save changes\n" +
                            "4 - Select task\n" +
                            "5 - Edit task\n" +
                            "6 - Show task info\n" +
                            "7 - Get task list\n" +
                            "Esc - Return to list of task list");
        }

        /// <summary>
        /// Write task list menu items.
        /// </summary>
        /// <returns></returns>
        public static int WriteTaskListsMenu()
        {
            return WriteConsole(
                            "Lists menu:\n" +
                            "1 - Add new task list\n" +
                            "2 - Delete task list\n" +
                            "3 - Save changes\n" +
                            "4 - Select task list\n" +
                            "5 - Edit list of task list\n" +
                            "6 - Show task info\n" +
                            "7 - Get list of task list\n" +
                            "Esc - Exit app");
        }


        /// <summary>
        /// Write task list or list of task list.
        /// </summary>
        /// <param name="controller">Using TaskController with saving state.</param>
        /// <param name="l">List<string> to write.</param>
        /// <param name="anykey">This key created for waiting option
        /// after writing text.</param>
        /// <returns>Caret state.</returns>
        public static int WriteList(ref TaskController controller, List<string> l,bool anykey=false)
        {
            string list = null;
            foreach (string n in l)
            {
                list += n + "\n";
            }
            return WriteConsole("List:\n" + list, anykey);
        }

        /// <summary>
            /// Exit app.
            /// </summary>
        public static void ExitMenu()
        {
            WriteConsole("Do you want to exit? Y/another key");
            if (Console.ReadKey().Key == (ConsoleKey.Y))
                Environment.Exit(0);
        }

        /// <summary>
        /// Arrow selection method.
        /// </summary>
        /// <param name="caret">Saving caret state.</param>
        /// <param name="maxCaret">Max caret position.</param>
        public static void ArrowSelector(ref int caret, int maxCaret)
        {
            int minCaret = Console.CursorTop;
            caret = Console.CursorTop;
            do
            {
                var k = Console.ReadKey(true);
                switch (k.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (caret == minCaret)
                        {
                            Console.CursorTop = maxCaret;
                            caret = maxCaret;
                            continue;
                        }
                        Console.CursorTop--;
                        caret--;
                        continue;
                    case ConsoleKey.DownArrow:
                        if (caret == maxCaret)
                        {
                            Console.CursorTop = minCaret;
                            caret = minCaret;
                            continue;
                        }
                        Console.CursorTop++;
                        caret++;
                        continue;
                    case ConsoleKey.Enter:
                        break;
                    default:
                        continue;
                }
                break;
            }
            while (true);
        }


        /*
        public static int WriteAllTasks(ref TaskController controller)
        {
            string list = null;
            foreach (string n in controller.AllTaskLists())
            {
                list += n + "\n";
            }
            return WriteConsole("Lists:\n" + list);
        }

        public static int WriteAllTaskLists(ref TaskController controller)
        {
            string list=null;
            foreach (string n in controller.AllTaskLists())
            {
                list += n + "\n";
            }
            return WriteConsole("Lists:\n" + list);
        }
        */

        #endregion

    }


}
