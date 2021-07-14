using ConsoleRogue.Components.Actors;
using RLNET;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Components.Drivers
{
    class Statistics
    {
        public static int attack(Actor attacker, Actor defender)
        {
            Random random = new Random();
            int dealtDmg = attacker.attack - defender.defense;
            if (random.Next(attacker.attackLuck, 100) > 95)
            {
                dealtDmg *= 2;
            }
            if (random.Next(defender.defenseLuck, 100) > 95)
            {
                dealtDmg /= 2;
            }
            defender.health = defender.health - dealtDmg;
            if(defender.health <= 0)
            {
                return -1;
            }
            return dealtDmg;
        }
    }
}
