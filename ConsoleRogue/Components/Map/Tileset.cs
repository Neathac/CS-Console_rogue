using System;
using System.Collections.Generic;
using System.Text;
using ConsoleRogue.Components.Actors;
using ConsoleRogue.Customizables;
using RLNET;
using RogueSharp;

namespace ConsoleRogue.Components.Map
{
    class Tileset : RogueSharp.Map
    {
        public static char unpassable = '#';
        public static char passable = '.';

        public void Draw( RLConsole mapConsole )
        {
            mapConsole.Clear();
            foreach ( Cell cell in GetAllCells() )
            {
                SetConsoleSymbolForCell( mapConsole, cell );
            }
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
    }
}
