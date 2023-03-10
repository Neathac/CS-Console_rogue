using ConsoleRogue.Customizables;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Components.Actors
{
    class Goblin : Enemy
    {
        public Goblin(int level, int x, int y)
        {
            Random random = new Random();
            perception = 10;
            name = "Goblin";
            colour = ObjectColoring.enemy;
            symbol = 'G';
            attack = random.Next(2+(level*2), 6+(level*2));
            defense = random.Next(0 + (level*2), 2 + (level*2));
            attackLuck = 5;
            defenseLuck = 5;
            maxHealth = random.Next(2 + (level*2), 7 + (level*2));
            health = maxHealth;
            agility = 100 - level * 5;
            moves = 0;
            xCoor = x; // Default position
            yCoor = y;
        }
    }

}
