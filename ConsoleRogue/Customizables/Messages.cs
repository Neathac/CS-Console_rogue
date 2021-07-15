using ConsoleRogue.Components.Actors;
using ConsoleRogue.Misc_Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Customizables
{
    class Messages
    {
        private static string dealtDamage = "You dealt damage for ";
        private static string takenDamage = "You have taken damage for ";
        private static string moved = "You moved ";
        private static string found = "You found ";
        private static string destroyed = "You destroyed a ";
        public static string getDamageMessage(Events newEvent, int damage)
        {
            switch (newEvent)
            {
                case Events.DealtDamage:
                    return dealtDamage + damage.ToString();
                case Events.RecievedDamage:
                    return takenDamage + damage.ToString();
                default:
                    return "No damage dealt";
            }
        }

        public static string getDestroyed(Actor killed)
        {
            return (destroyed + killed.name);
        }

        public static string getExit()
        {
            return (found + "an exit");
        }

        public static string getFound(Misc_Globals.Pack pack)
        {
            switch (pack)
            {
                case Misc_Globals.Pack.AGILITY:
                    return (found + "an agility boost");
                case Misc_Globals.Pack.ATTACK:
                    return (found + "an attack boost");
                case Misc_Globals.Pack.DEFENSE:
                    return (found + "an defence boost");
                case Misc_Globals.Pack.HEALTH:
                    return (found + "an health boost");
                case Misc_Globals.Pack.EXIT:
                    return (found + " an exit");
                default:
                    return "";
            }
        }
        public static string getMove(MovementDirs direction)
        {
            switch (direction)
            {
                case MovementDirs.Down:
                    return moved + "south";
                case MovementDirs.Top:
                    return moved + "north";
                case MovementDirs.Left:
                    return moved + "west";
                case MovementDirs.Right:
                    return moved + "east";
                default:
                    return "No movement";
            }
        }
    }
}
