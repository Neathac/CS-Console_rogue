using ConsoleRogue.Components.Actors;
using ConsoleRogue.Misc_Globals;
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
        private List<Goblin> goblinList;
        private List<Actors.Pack> packList;
        private List<Room> rooms;
        private bool placedExit;

        public Generator( int width, int height, int minRooms, int maxRooms )
        {
            placedExit = false;
            this.height = height;
            this.width = width;
            this.minRooms = minRooms;
            this.maxRooms = maxRooms;
            tileset = new Tileset();
            rooms = new List<Room>();
            goblinList = new List<Goblin>();
            packList = new List<Actors.Pack>();
        }

        public Tileset generateMap( Random seed, int level )
        {
            bool placedPlayer = false;
            Room playerRoom = new Room(1,1,1,1);
            packList.Clear();
            rooms.Clear();
            goblinList.Clear();
            // RogueSharp Initialize
            tileset.Initialize( width, height );

            foreach( Cell cell in tileset.GetAllCells())
            {
                tileset.SetCellProperties( cell.X, cell.Y, true, true, false);
            }

            foreach ( Cell cell in tileset.GetCellsInColumns(0, width -1) )
            {
                tileset.SetCellProperties(cell.X, cell.Y, false, false, false);
            }

            foreach (Cell cell in tileset.GetCellsInRows(0, height - 1))
            {
                tileset.SetCellProperties(cell.X, cell.Y, false, false, false);
            }

            int roomsAmount = seed.Next(minRooms, maxRooms);
            for (int i = 0; i < roomsAmount; ++i)
            {
                Room currRoom = new Room(seed.Next(0, width-10), seed.Next(0, height-5), seed.Next(minRooms, maxRooms), seed.Next(minRooms, maxRooms));
                if (isValid(currRoom))
                {
                    // TODO - Add room overlap detection
                    rooms.Add(currRoom);
                    createRoom(currRoom);
                    if (!placedPlayer)
                    {
                        Program.player.setStart(currRoom.centerX, currRoom.centerY);
                        placedPlayer = true;
                        playerRoom = currRoom;
                    }
                    else
                    {
                        goblinList = createEnemies(currRoom.centerX, currRoom.centerY, level, goblinList, playerRoom);
                        packList = placePack(currRoom.centerX, currRoom.centerY, packList);
                    }
                }
                else
                {
                    i -= 1;
                }
            }
            connectAllRooms(rooms);

            return tileset;
        }

        public List<Actors.Pack> placePack(int x, int y, List<Actors.Pack> packs)
        {
            if (placedExit)
            {
                Random random = new Random();
                switch (random.Next(1, minRooms + 3))
                {
                    case 2:
                        Actors.Pack pack = new Actors.Pack(x,y, Misc_Globals.Pack.AGILITY);
                        packs.Add(pack);
                        return packs;
                    case 3:
                        Actors.Pack pack2 = new Actors.Pack(x, y, Misc_Globals.Pack.ATTACK);
                        packs.Add(pack2);
                        return packs;
                    case 4:
                        Actors.Pack pack3 = new Actors.Pack(x, y, Misc_Globals.Pack.DEFENSE);
                        packs.Add(pack3);
                        return packs;
                    case 5:
                        Actors.Pack pack4 = new Actors.Pack(x, y, Misc_Globals.Pack.HEALTH);
                        packs.Add(pack4);
                        return packs;
                    default:                       
                        
                        return packs;
                }               
            }
            tileset.exit = new Actors.Pack(x, y, Misc_Globals.Pack.EXIT);
            placedExit = true;
            return packs;
        }
        public List<Actors.Pack> getPacks()
        {
            return packList;
        }

        public List<Goblin> getGoblins()
        {
            return goblinList;
        }

        private void connectAllRooms(List<Room> rooms)
        {
            for(int i = 0; i < rooms.Count; ++i)
            {
                for (int j = i+1; j < rooms.Count; ++j)
                {
                    connectTwoRooms(rooms[i], rooms[j], tileset.getRelativeDirs(new int[] { rooms[i].centerX, rooms[i].centerY }, new int[] { rooms[j].centerX, rooms[j].centerY }));
                }
            }
        }

        private void connectTwoRooms(Room room1, Room room2, MovementDirs relDirs)
        {
            switch (relDirs)
            {
                case MovementDirs.Down:
                    digTunnel(new int[] { room1.centerX, room1.centerY }, new int[] { -1, room2.centerY}, new int[] { 0, -1});
                    break;
                case MovementDirs.Top:
                    digTunnel(new int[] { room1.centerX, room1.centerY }, new int[] { -1, room2.centerY }, new int[] { 0, 1 });
                    break;
                case MovementDirs.Left:
                    digTunnel(new int[] { room1.centerX, room1.centerY }, new int[] { room2.centerX, -1 }, new int[] { 1, 0 });
                    break;
                case MovementDirs.Right:
                    digTunnel(new int[] { room1.centerX, room1.centerY }, new int[] { room2.centerX, -1 }, new int[] { -1, 0 });
                    break;
                case MovementDirs.DownLeft:
                    digTunnel(new int[] { room1.centerX, room1.centerY }, new int[] { -1, room2.centerY }, new int[] { 0, -1 });
                    digTunnel(new int[] { room1.centerX, room2.centerY }, new int[] { room2.centerX, -1 }, new int[] { 1, 0 });
                    break;
                case MovementDirs.DownRight:
                    digTunnel(new int[] { room1.centerX, room1.centerY }, new int[] { -1, room2.centerY }, new int[] { 0, -1 });
                    digTunnel(new int[] { room1.centerX, room2.centerY }, new int[] { room2.centerX, -1 }, new int[] { -1, 0 });
                    break;
                case MovementDirs.TopLeft:
                    digTunnel(new int[] { room1.centerX, room1.centerY }, new int[] { -1, room2.centerY }, new int[] { 0, 1 });
                    digTunnel(new int[] { room1.centerX, room2.centerY }, new int[] { room2.centerX, -1 }, new int[] { 1, 0 });
                    break;
                case MovementDirs.TopRight:
                    digTunnel(new int[] { room1.centerX, room1.centerY }, new int[] { -1, room2.centerY }, new int[] { 0, 1 });
                    digTunnel(new int[] { room1.centerX, room2.centerY }, new int[] { room2.centerX, -1 }, new int[] { -1, 0 });
                    break;
            }
        }

        private void digTunnel(int[] start, int[] limit, int[] motion)
        {
            int[] walloffDir = { 1-Math.Abs(motion[0]), 1 - Math.Abs(motion[1]) };
            bool digging = false;
            while (start[0] != limit[0] && start[1] != limit[1])
            {
                if(tileset.GetCell(start[0]+motion[0], start[1] + motion[1]).IsWalkable && digging) // Just broke into a walkable chamber
                {
                    digging = false;
                }
                else if (!tileset.GetCell(start[0] + motion[0], start[1] + motion[1]).IsWalkable && !digging) // Just found a wall
                {
                    digging = true;
                }
                start[0] += motion[0];
                start[1] += motion[1];
                if (digging)
                {
                    tileset.SetCellProperties(start[0], start[1], true, true, false);
                    tileset.SetCellProperties(start[0]+walloffDir[0], start[1] + walloffDir[1], false, false, false);
                    tileset.SetCellProperties(start[0] - walloffDir[0], start[1] - walloffDir[1], false, false, false);
                }
            }
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

        private List<Goblin> createEnemies(int centerX, int centerY, int level, List<Goblin> goblins, Room playerRoom)
        {
            if(Math.Abs(centerX-playerRoom.centerX)>=4 && Math.Abs(centerY - playerRoom.centerY) >= 4)
            {
                for(int i = -1; i <= 1; i +=2)
                {
                    for (int j = -1; j <= 1; j += 2)
                    {
                        if (tileset.GetCell(centerX + i, centerY + j).IsWalkable)
                        {
                            Goblin goblin = new Goblin(level, centerX + i, centerY + j);
                            goblins.Add(goblin);
                        }
                    }
                }
            }
            
            return goblins;
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
