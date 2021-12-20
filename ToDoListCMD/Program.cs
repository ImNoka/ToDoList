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
                TaskListsInterface(ref Controller);
                TasksInterface(ref Controller);
            }


        }

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
                        controller.ShowInfo(); // Show selected task info.
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
                        controller.ShowTaskListInfo();
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
    }
}
