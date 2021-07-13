using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Interfaces
{
    interface IActor
    {
        string name { get; set; }
        int health { get; set; }
        int maxHealth { get; set; }
        int perception { get; set; }
        int attack { get; set; }
        int defense { get; set; }
        int attackLuck { get; set; }
        int defenseLuck { get; set; }
        int gold { get; set; }
        
    }
}
