using ConsoleRogue.Customizables;
using ConsoleRogue.Interfaces;
using RLNET;
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
            name = "Credit warrior";
            colour = ObjectColoring.player;
            symbol = '@';
            attack = 5;
            defense = 2;
            attackLuck = 5;
            defenseLuck = 5;
            maxHealth = 10;
            health = 10;
            agility = 80;
            gold = 0;
            xCoor = 10; // Default position
            yCoor = 10;
        }
        public void drawStats(RLConsole console)
        {
            console.Clear();
            console.Print(1,1,name,ObjectColoring.textColor);
            console.Print(1, 3, "Attack: " + attack.ToString(), ObjectColoring.textColor);
            console.Print(1, 5, "Defense: " + defense.ToString(), ObjectColoring.textColor);
            console.Print(1, 7, "Health: " + health.ToString() + "/" + maxHealth.ToString(), ObjectColoring.textColor);
            console.Print(1, 9, "Agility: " + (100-agility).ToString(), ObjectColoring.textColor);
        }
        public void setStart(int x, int y)
        {
            xCoor = x; 
            yCoor = y;
        }
    }
}
