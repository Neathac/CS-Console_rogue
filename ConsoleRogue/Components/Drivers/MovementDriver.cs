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
        public bool movePlayer( Actor player ,MovementDirs direction )
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
        public MovementDirs mapKeyToDir( RLKeyPress keyPress )
        {
            switch( keyPress.Key)
            {
                case RLKey.Up:
                    return MovementDirs.Top;
                case RLKey.Down:
                    return MovementDirs.Down;
                case RLKey.Left:
                    return MovementDirs.Left;
                case RLKey.Right:
                    return MovementDirs.Right;
                default:
                    return MovementDirs.InPlace;
            }
        }

        public bool goblinAction(Goblin goblin, Player player, Tileset tileset)
        {
            if (goblin.toMove(player.agility))
            {
                switch(tileset.getRelativeDirs(new int[] { player.xCoor, player.yCoor }, new int[] { goblin.xCoor, goblin.yCoor }))
                {
                    case MovementDirs.Down:
                        if(areAdjecent(goblin, player, MovementDirs.Down))
                        {
                            return true;
                        }
                        movePlayer(goblin, MovementDirs.Down);
                        return false;
                    case MovementDirs.Top:
                        if (areAdjecent(goblin, player, MovementDirs.Top))
                        {
                            return true;
                        }
                        movePlayer(goblin, MovementDirs.Top);
                        return false;
                    case MovementDirs.Left:
                        if (areAdjecent(goblin, player, MovementDirs.Left))
                        {
                            return true;
                        }
                        movePlayer(goblin, MovementDirs.Left);
                        return false;
                    case MovementDirs.Right:
                        if (areAdjecent(goblin, player, MovementDirs.Right))
                        {
                            return true;
                        }
                        movePlayer(goblin, MovementDirs.Right);
                        return false;
                    case MovementDirs.DownLeft:
                        if(goblin.health % 2 == 0)
                        {
                            if (movePlayer(goblin, MovementDirs.Left))
                            {
                                return false;
                            }
                            movePlayer(goblin, MovementDirs.Down);
                            return false;
                        }
                        if (movePlayer(goblin, MovementDirs.Down))
                        {
                            return false;
                        }
                        movePlayer(goblin, MovementDirs.Left);
                        return false;
                    case MovementDirs.DownRight:
                        if (goblin.health % 2 == 0)
                        {
                            if (movePlayer(goblin, MovementDirs.Right))
                            {
                                return false;
                            }
                            movePlayer(goblin, MovementDirs.Down);
                            return false;
                        }
                        if (movePlayer(goblin, MovementDirs.Down))
                        {
                            return false;
                        }
                        movePlayer(goblin, MovementDirs.Right);
                        return false;
                    case MovementDirs.TopLeft:
                        if (goblin.health % 2 == 0)
                        {
                            if (movePlayer(goblin, MovementDirs.Left))
                            {
                                return false;
                            }
                            movePlayer(goblin, MovementDirs.Top);
                            return false;
                        }
                        if (movePlayer(goblin, MovementDirs.Top))
                        {
                            return false;
                        }
                        movePlayer(goblin, MovementDirs.Left);
                        return false;
                    case MovementDirs.TopRight:
                        if (goblin.health % 2 == 0)
                        {
                            if (movePlayer(goblin, MovementDirs.Right))
                            {
                                return false;
                            }
                            movePlayer(goblin, MovementDirs.Top);
                            return false;
                        }
                        if (movePlayer(goblin, MovementDirs.Top))
                        {
                            return false;
                        }
                        movePlayer(goblin, MovementDirs.Right);
                        return false;

                }
            }
            return false;
        }

        public bool areAdjecent(Actor actor1, Actor actor2, MovementDirs direction)
        {
            switch (direction)
            {
                case MovementDirs.Down:
                    if (actor1.yCoor + 1 == actor2.yCoor)
                    {
                        return true;
                    }
                    return false;
                case MovementDirs.Top:
                    if (actor1.yCoor - 1 == actor2.yCoor)
                    {
                        return true;
                    }
                    return false;
                case MovementDirs.Right:
                    if (actor1.xCoor + 1 == actor2.xCoor)
                    {
                        return true;
                    }
                    return false;
                case MovementDirs.Left:
                    if (actor1.xCoor - 1 == actor2.xCoor)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}
