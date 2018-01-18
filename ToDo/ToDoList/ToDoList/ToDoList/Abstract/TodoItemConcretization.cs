using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace ToDoList.Abstract
{
    class TodoItemConcretization : ATable
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public TodoItemConcretization()
        {
        }

        public override string ToString()
        {
            return "ID" + ID + " Name " + Name + " Text " + Text;
        }
    }
}
