using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace EvidenceSkladeb
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Who { get; set; }
        public string Lenght { get; set; }
        public int Done { get; set; }

        public TodoItem()
        {
            
        }

        public override string ToString()
        {
            return "ID" + ID + " Name " + Name + " Interpret " + Who;
        }
    }
}
