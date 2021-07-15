using System;
using System.Collections.Generic;
using System.Text;
using ConsoleRogue.Components.Actors;
using ConsoleRogue.Customizables;
using ConsoleRogue.Misc_Globals;
using RLNET;
using RogueSharp;

namespace ConsoleRogue.Components.Map
{
    class Tileset : RogueSharp.Map
    {
        public static char unpassable = '#';
        public static char passable = '.';
        public List<Goblin> goblins;
        public List<Actors.Pack> packs;
        public Actors.Pack exit;

        public Tileset()
        {
            this.goblins = new List<Goblin>();
            this.packs = new List<Actors.Pack>();
        }
        public void Draw( RLConsole mapConsole )
        {
            mapConsole.Clear();
            foreach ( Cell cell in GetAllCells() )
            {
                SetConsoleSymbolForCell( mapConsole, cell );
            }
        }

        public void removeGoblin(Goblin goblin)
        {
            goblins.Remove(goblin);
            SetCellProperties(goblin.xCoor, goblin.yCoor, true, true, true);
        }

        public void setGoblins(List<Goblin> newGoblins)
        {
            goblins.Clear();
            foreach (Goblin goblin in newGoblins)
            {
                goblins.Add(goblin);
            }
        }

        public void setPacks(List<Actors.Pack> newPacks)
        {
            packs.Clear();
            foreach(Actors.Pack pack in newPacks)
            {
                packs.Add(pack);
            }
        }
        public void removePack(Actors.Pack pack)
        {
            packs.Remove(pack);
            SetCellProperties(pack.xCoor, pack.yCoor, true, true, true);
        }

        private void SetConsoleSymbolForCell( RLConsole console, Cell cell)
        {
            // Individual rooms can be unexplored, meaning we don't always want to return them
            if ( !cell.IsExplored )
            {
                return;
            }

            // Differentiating between currently visible and invisible tiles
            if ( IsInFov( cell.X, cell.Y ) )
            {

                //Differentiating between passable and impassable tiles
                if ( cell.IsWalkable )
                {
                    console.Set( cell.X, cell.Y, ObjectColoring.floorVisible, ObjectColoring.background, passable );
                } else
                {
                    console.Set(cell.X, cell.Y, ObjectColoring.wallVisible, ObjectColoring.background, unpassable);
                }
            } else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, ObjectColoring.floorInvisible, ObjectColoring.background, passable);
                }
                else
                {
                    console.Set(cell.X, cell.Y, ObjectColoring.wallInvisible, ObjectColoring.background, unpassable);
                }
            }
        }

        public void updatePlayerVisibility( Player player )
        {
            ComputeFov(player.xCoor, player.yCoor, player.perception, true);
            foreach( Cell cell in GetAllCells())
            {
                if( IsInFov( cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
         }

        public bool newActorPosition( Actor actor, int xCoor, int yCoor )
        {
            // Get the cell we want to move the actor to
            ICell targetCell = GetCell( xCoor, yCoor);
            // If we are able to move the actor there:
            if (targetCell.IsWalkable)
            {
                // Get the cell actor is currently on
                ICell currentCell = GetCell( actor.xCoor, actor.yCoor );
                // If current actor is standing on our cell, it is unwalkable, so we have to make it walkable again
                // Could be an interesting mechanic - consider and try
                SetCellProperties( currentCell.X, currentCell.Y, currentCell.IsTransparent, true, currentCell.IsExplored );
                // Moving the actor
                actor.xCoor = xCoor;
                actor.yCoor = yCoor;
                // New position is now unwalkable, because our actor is now standing there
                SetCellProperties(targetCell.X, targetCell.Y, targetCell.IsTransparent, false, currentCell.IsExplored);
                
                // Can't think of a better place to update player FoV
                if ( actor is Player )
                {
                    updatePlayerVisibility(actor as Player);
                }
                return true;
            }

            return false;
        }

        public Actor getNearbyEntity(Actor actor)
        {
            foreach(Goblin goblin in goblins)
            {
                if (IsInFov(goblin.xCoor, goblin.yCoor))
                {
                    if (Math.Abs(goblin.yCoor - actor.yCoor) <= 2 && Math.Abs(goblin.xCoor - actor.xCoor) <= 2)
                    {
                        return goblin;
                    }
                }
            }
            foreach(Actors.Pack pack in packs)
            {
                if (IsInFov(pack.xCoor, pack.yCoor))
                {
                    if (Math.Abs(pack.yCoor - actor.yCoor) <= 2 && Math.Abs(pack.xCoor - actor.xCoor) <= 2)
                    {
                        return pack;
                    }
                }
            }
            if(IsInFov(exit.xCoor, exit.yCoor))
            {
                if (Math.Abs(exit.yCoor - actor.yCoor) <= 2 && Math.Abs(exit.xCoor - actor.xCoor) <= 2)
                {
                    return exit;
                }
            }
            return actor;
        }

        public MovementDirs getRelativeDirs(int[] x, int[] y)
        {
            if (x[0] > y[0]) // x is to the right of y
            {
                if (x[1] > y[1]) // x is below y
                {
                    return MovementDirs.DownRight;
                }
                else if (x[1] == y[1]) // x is on the same vertical line as y
                {
                    return MovementDirs.Right;
                }
                else // x is above y
                {
                    return MovementDirs.TopRight;
                }
            }
            else if (x[0] == y[0]) // x and y are on the same horizontal level
            {
                if (x[1] > y[1]) // x is below y
                {
                    return MovementDirs.Down;
                }
                else if (x[1] == y[1]) // x is on the same vertical line as y
                {
                    return MovementDirs.InPlace;
                }
                else // x is above y
                {
                    return MovementDirs.Top;
                }
            }
            else // x is to the left of y
            {
                if (x[1] > y[1]) // x is below y
                {
                    return MovementDirs.DownLeft;
                }
                else if (x[1] == y[1]) // x is on the same vertical line as y
                {
                    return MovementDirs.Left;
                }
                else // x is above y
                {
                    return MovementDirs.TopLeft;
                }
            }
        }
    }
}
