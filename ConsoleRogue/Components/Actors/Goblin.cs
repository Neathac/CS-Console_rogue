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
            attack = random.Next(2+level, 7+level);
            defense = random.Next(0 + level, 2 + level);
            attackLuck = 5;
            defenseLuck = 5;
            maxHealth = random.Next(2 + level, 7 + level);
            health = maxHealth;
            agility = 100 - level * 5; 
            xCoor = x; // Default position
            yCoor = y;
        }
    }

}
