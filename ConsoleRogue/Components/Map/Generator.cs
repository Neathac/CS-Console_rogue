using RogueSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Components.Map
{
    class Generator
    {
        private readonly int width;
        private readonly int height;

        private readonly Tileset tileset;

        public Generator( int width, int height )
        {
            this.height = height;
            this.width = width;
            tileset = new Tileset();
        }

        public Tileset generateMap()
        {
            // RogueSharp Initialize
            tileset.Initialize( width, height );

            foreach( Cell cell in tileset.GetAllCells())
            {
                tileset.SetCellProperties( cell.X, cell.Y, true, true, true);
            }

            foreach ( Cell cell in tileset.GetCellsInColumns(0, width -1) )
            {
                tileset.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            foreach (Cell cell in tileset.GetCellsInRows(0, height - 1))
            {
                tileset.SetCellProperties(cell.X, cell.Y, false, false, true);
            }
            return tileset;
        }
    }
}
