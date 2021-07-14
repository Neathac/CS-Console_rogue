using ConsoleRogue.Customizables;
using RLNET;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Components.Actors
{
    class Enemy : Actor
    {
        public void drawStats( RLConsole statConsole, int position)
        {
            int yPos = 13 + (position * 2);
            statConsole.Print(1, yPos, symbol.ToString(), colour);
            int width = Convert.ToInt32(((double)health / (double)maxHealth)*16.0);
            int remainingWidth = 16 - width;
            statConsole.SetBackColor(3, yPos, width, 1, ObjectColoring.healthBarLight);
            statConsole.SetBackColor(3 + width, yPos, remainingWidth, 1, ObjectColoring.healthBarDark);
            statConsole.Print(2, yPos, name, ObjectColoring.textColor);
        }
    }
}
