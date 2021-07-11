using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Interfaces
{
    interface IActor
    {
        string name { get; set; }
        int perception { get; set; }
    }
}
