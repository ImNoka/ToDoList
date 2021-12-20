using System;
using System.Collections.Generic;
using System.Text;
using ToDoListBL.Model;
using System.IO;
using Newtonsoft.Json;

namespace ToDoListBL.Serializer
{
    class ILangSerializer : IFileSerializer<List<CMessage>>
    {
        /// <summary>
        /// Language file reader.
        /// </summary>
        public ILangSerializer()
        {

        }

        /// <summary>
        /// Load selected localisation.
        /// </summary>
        /// <param name="path">Localisation path</param>
        /// <returns></returns>
        public List<CMessage> Deserialize(string path = "eng")
        {
            using (StreamReader file = File.OpenText("Localization\\" + path + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (List<CMessage>)serializer.Deserialize(file, typeof(List<CMessage>));
            }
        }

        private void Serialize(List<CMessage> list, string path = "eng")
        {
            using (StreamWriter file = File.CreateText("Localisation\\" + path + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, list);
            }
        }
    }
}
