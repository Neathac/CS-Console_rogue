using ConsoleRogue.Components.Actors;
using ConsoleRogue.Components.Map;
using ConsoleRogue.Misc_Globals;
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
        public static Events playerAction(Actor player,Actor target, Tileset tileset)
        {
            if(target is Enemy)
            {
                if(attack(player, target) < 0)
                {
                    tileset.removeGoblin(target as Goblin);
                    return Events.Destroyed;
                }
                return Events.DealtDamage;
            }
            else if (target is Actors.Pack)
            {
                Actors.Pack casted = target as Actors.Pack;
                switch (casted.packKind)
                {
                    case Misc_Globals.Pack.AGILITY:
                        player.agility -= 2;
                        tileset.packs.Remove(target as Actors.Pack);
                        break;
                    case Misc_Globals.Pack.ATTACK:
                        player.attack += 2;
                        tileset.packs.Remove(target as Actors.Pack);
                        break;
                    case Misc_Globals.Pack.DEFENSE:
                        player.defense += 2;
                        tileset.packs.Remove(target as Actors.Pack);
                        break;
                    case Misc_Globals.Pack.HEALTH:
                        player.maxHealth += 2;
                        player.health = player.maxHealth;
                        tileset.packs.Remove(target as Actors.Pack);
                        break;
                    case Misc_Globals.Pack.EXIT:
                        
                        break;
                }
            }
            return Events.Nothing;
        }
    }
}
