using ConsoleRogue.Misc_Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Customizables
{
    class Messages
    {
        private string dealtDamage = "You dealt damage for ";
        private string takenDamage = "You have taken damage for ";
        private string moved = "You moved ";
        private string found = "You found ";
        private string destroyed = "You destroyed a ";
        public string getDamageMessage(Events newEvent, int damage)
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

        public string getMove(MovementDirs direction)
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
