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
        private readonly int minRooms;
        private readonly int maxRooms;
        private readonly Tileset tileset;

        private List<Room> rooms;

        public Generator( int width, int height, int minRooms, int maxRooms )
        {
            this.height = height;
            this.width = width;
            this.minRooms = minRooms;
            this.maxRooms = maxRooms;
            tileset = new Tileset();
            rooms = new List<Room>();
        }

        public Tileset generateMap( Random seed )
        {
            rooms.Clear();
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

            int roomsAmount = seed.Next(minRooms, maxRooms);
            for (int i = 0; i < roomsAmount; ++i)
            {
                Room currRoom = new Room(seed.Next(0, width), seed.Next(0, height), seed.Next(minRooms, maxRooms), seed.Next(minRooms, maxRooms));
                if (isValid(currRoom))
                {
                    rooms.Add(currRoom);
                    createRoom(currRoom);
                }
                else
                {
                    i -= 1;
                }
            }

            return tileset;
        }

        private void createRoom(Room room)
        {
            {
                List<int[]> coorList = new List<int[]>();
                for (int i = 0; i <= room.width; ++i)
                {
                    tileset.SetCellProperties(room.leftEdge + i, room.topEdge, false, false, false);
                    tileset.SetCellProperties(room.leftEdge + i, room.bottomEdge, false, false, false);
                }
                for (int i = 0; i <= room.height; ++i)
                {
                    tileset.SetCellProperties(room.leftEdge, room.topEdge + i, false, false, false);
                    tileset.SetCellProperties(room.rightEdge, room.topEdge + i, false, false, false);
                }
            }
        }

        private bool isValid( Room room )
        {
            if(room.bottomEdge >= height)
            {
                return false;
            }
            else if (room.topEdge <= 0)
            {
                return false;
            }
            else if (room.leftEdge <= 0)
            {
                return false;
            }
            else if (room.rightEdge >= width)
            {
                return false;
            }
            return true;
        }
    }
}
