using ConsoleRogue.Misc_Globals;
using ConsoleRogue.Components.Map;
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleRogue.Components.Actors;
using RLNET;

namespace ConsoleRogue.Components.Drivers
{
    class MovementDriver
    {
        public bool movePlayer( Player player ,MovementDirs direction )
        {
            switch (direction)
            {
                // TODO - Again, move the program property tileset into MiscGlobals
                case MovementDirs.Down:
                    return Program.tileset.newActorPosition(player, player.xCoor, player.yCoor + 1);
                case MovementDirs.DownLeft:
                    return Program.tileset.newActorPosition(player, player.xCoor - 1, player.yCoor + 1);
                case MovementDirs.DownRight:
                    return Program.tileset.newActorPosition(player, player.xCoor + 1, player.yCoor + 1);
                case MovementDirs.Right:
                    return Program.tileset.newActorPosition(player, player.xCoor + 1, player.yCoor);
                case MovementDirs.Left:
                    return Program.tileset.newActorPosition(player, player.xCoor - 1, player.yCoor);
                case MovementDirs.Top:
                    return Program.tileset.newActorPosition(player, player.xCoor, player.yCoor - 1);
                case MovementDirs.TopRight:
                    return Program.tileset.newActorPosition(player, player.xCoor + 1, player.yCoor - 1);
                case MovementDirs.TopLeft:
                    return Program.tileset.newActorPosition(player, player.xCoor - 1, player.yCoor - 1);
                default:
                    return false;
            }
        }

        // Unsure as to how to read multiple different keys at once
        // Maybe some crazy structure reading individual buttons in driver?
        // TODO / TO call a feature
        public bool mapKeyToDir( Player player ,RLKeyPress keyPress )
        {
            switch( keyPress.Key)
            {
                case RLKey.Up:
                    return movePlayer( player ,MovementDirs.Top );
                case RLKey.Down:
                    return movePlayer(player, MovementDirs.Down);
                case RLKey.Left:
                    return movePlayer(player, MovementDirs.Left);
                case RLKey.Right:
                    return movePlayer(player, MovementDirs.Right);
                default:
                    return false;
            }
        }
    }
}
