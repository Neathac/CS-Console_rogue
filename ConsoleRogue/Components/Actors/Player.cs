using ConsoleRogue.Customizables;
using ConsoleRogue.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Components.Actors
{
    class Player : Actor
    {
        public Player()
        {
            perception = 15;
            colour = ObjectColoring.player;
            symbol = '@';
            xCoor = 10; // Default position
            yCoor = 10;
        }
        public void setStart(int x, int y)
        {
            xCoor = x; 
            yCoor = y;
        }
    }
}
