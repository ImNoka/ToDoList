using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ToDoListBL.Model;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDoListBL.Serializer
{
    class ITaskSerializer : IFileSerializer<List<Task>>
    {
        /// <summary>
        /// Tasks file handler.
        /// </summary>
        public ITaskSerializer()
        {

        }

        /// <summary>
        /// Load and deserialize task list.
        /// </summary>
        /// <param name="path">Task list name.</param>
        /// <returns></returns>
        public List<Task> Deserialize(string path)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(Environment.CurrentDirectory + "\\TaskLists\\" + path + ".dat", FileMode.OpenOrCreate))
            {
                return formatter.Deserialize(fs) as List<Task>;
            }
        }

        /// <summary>
        /// Serialize and save task list to file. 
        /// </summary>
        /// <param name="tasks">Current task list.</param>
        /// <param name="path">Saving path.</param>
        public void Serialize(List<Task> tasks, string path)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(Environment.CurrentDirectory + "\\TaskLists\\" + path + ".dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, tasks);
            }
        }

    }
}
