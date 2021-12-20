using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListBL.Model
{
    [Serializable]
    public class Task
    {
        /// <summary>
        /// Task name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Task content(text,images).
        /// </summary>
        public Content Content { get; set; }
        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime CrTime { get; set; }

        /// <summary>
        /// Create new task.
        /// </summary>
        /// <param name="name">Task name.</param>
        /// <param name="content">Task content.</param>
        public Task(string name, Content content)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name is null.", nameof(name));
            Name = name;
            Content = content ?? throw new ArgumentNullException("No text and image.", nameof(content));

            CrTime = DateTime.Today;
        }


    }
}