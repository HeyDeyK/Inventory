using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
