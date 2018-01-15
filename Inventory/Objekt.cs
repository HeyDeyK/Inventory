using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using System.Windows.Media;

namespace Inventory
{
    [DelimitedRecord("|")]
    class Objekt
    {
        public string name;
        public double LeftPos;
        public double TopPos;
        public double width;
        public double height;
    }
}
