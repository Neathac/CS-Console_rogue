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

        public void updatePlayerVisibility()
        {
            Player player = Program.player; // TODO - Create a class holding all game agents that need to be accessed directly. Driver code shouldn't hold them
            ComputeFov(player.xCoor, player.yCoor, player.perception, true);
            foreach( Cell cell in GetAllCells())
            {
                if( IsInFov( cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
         }
    }
}
