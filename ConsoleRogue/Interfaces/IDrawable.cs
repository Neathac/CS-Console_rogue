using ConsoleRogue.Components.Map;
using ConsoleRogue.Customizables;
using RLNET;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Interfaces
{
    interface IDrawable
    {
        RLColor colour { get; set; }
        char symbol { get; set; }
        int xCoor { get; set; }
        int yCoor { get; set; }
        void Draw(RLConsole console, RogueSharp.IMap map);
    }
}
