using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ToDoListBL.Model
{
    [Serializable]
    public class CMessage
    {
        private string Language { get; set; }

        #region ConsoleMessages
        public string Head { get; private set; }
        public string Help { get; private set; }
        public string Done { get; private set; }
        public string Select { get; private set; }
        public string TaskList { get; private set; }
        public string Exit { get; private set; }
        public string Localisated { get; private set; }
        #endregion

        #region TaskControllerMessages
        public string[] Info1 { get; private set; }
        public string Info2 { get; private set; }
        public string SelectTaskMessage1 { get; private set; }
        public string SelectTaskMessage2 { get; private set; }
        public string StringEditMessage { get; private set; }
        public string TaskListMessage { get; private set; }

        #endregion

        #region StringEditorMessages
        public string Text { get; private set; }
        #endregion

        public CMessage()
        {
        }

        /*
        public CMessage()
        {
            using (var fs = new FileStream(Environment.CurrentDirectory + "\\eng.txt", FileMode.Open))
            {
                using (StreamWriter file = File.CreateText(Environment.CurrentDirectory + "\\eng.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, this);
                }

            }
            
        }
        */


    }
}