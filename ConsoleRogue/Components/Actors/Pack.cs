using ConsoleRogue.Customizables;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Components.Actors
{
    class Pack : Actor
    {
        public Misc_Globals.Pack packKind { get; set; }
        public Pack(int x, int y, Misc_Globals.Pack kind)
        {
            perception = 10;
            colour = ObjectColoring.pickupColor;
            if(kind != Misc_Globals.Pack.EXIT)
            {
                symbol = 'P';
            }
            else
            {
                symbol = 'E';
            }
            attack = 0;
            defense = 0;
            attackLuck = 0;
            defenseLuck = 0;
            maxHealth = 1;
            health = maxHealth;
            agility = Int32.MaxValue;
            moves = 0;
            xCoor = x; // Default position
            yCoor = y;
            packKind = kind;
        }
    }
}
