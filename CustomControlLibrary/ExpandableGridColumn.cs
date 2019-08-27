using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControlLibrary
{
    public class ExpandableGridColumn
    {
        public string Header { get; set; }
        public int Order { get; set; }
        public string ColumnName { get; set; }
        public bool Visibility { get; set; }
        public int Width { get; set; }
        public bool IsAlwaysVisible { get; set; } = false;        
    }
}
