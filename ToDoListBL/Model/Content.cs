using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListBL.Model
{
    [Serializable]
    public class Content
    {
        /// <summary>
        /// Task text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Task content.
        /// </summary>
        /// <param name="text">Task text.</param>
        public Content(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Task text.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
