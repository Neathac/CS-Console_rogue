using ConsoleRogue.Components.Actors;
using ConsoleRogue.Components.Map;
using ConsoleRogue.Customizables;
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
            if(dealtDmg < 0)
            {
                dealtDmg = 0;
            }
            defender.health = defender.health - dealtDmg;
            if(defender.health <= 0)
            {
                return -1;
            }
            return dealtDmg;
        }
        public static Events playerAction(Actor player,Actor target, Tileset tileset, Messenger messenger)
        {
            if(target is Enemy)
            {
                int attackRes = attack(player, target);
                if(attackRes < 0)
                {
                    tileset.removeGoblin(target as Goblin);
                    messenger.Add(Messages.getDestroyed(target));
                    return Events.Destroyed;
                }
                messenger.Add(Messages.getDamageMessage(Events.DealtDamage, attackRes));
                return Events.DealtDamage;
            }
            else if (target is Actors.Pack)
            {
                Actors.Pack casted = target as Actors.Pack;
                switch (casted.packKind)
                {
                    case Misc_Globals.Pack.AGILITY:
                        player.agility -= 2;
                        tileset.removePack(target as Actors.Pack);
                        break;
                    case Misc_Globals.Pack.ATTACK:
                        player.attack += 2;
                        tileset.removePack(target as Actors.Pack);
                        break;
                    case Misc_Globals.Pack.DEFENSE:
                        player.defense += 2;
                        tileset.removePack(target as Actors.Pack);
                        break;
                    case Misc_Globals.Pack.HEALTH:
                        player.maxHealth += 2;
                        player.health = player.maxHealth;
                        tileset.removePack(target as Actors.Pack);
                        break;
                    case Misc_Globals.Pack.EXIT:
                        messenger.Add(Messages.getExit());
                        return Events.Exit;
                    default:                        
                        break;
                }
                messenger.Add(Messages.getFound(casted.packKind));
            }
            return Events.Nothing;
        }
    }
}
