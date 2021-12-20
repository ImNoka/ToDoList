using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListBL.Serializer
{
    interface IFileSerializer<T>
    {
        //void Serialize(T list, string path);
        T Deserialize(string path);
    }
}
