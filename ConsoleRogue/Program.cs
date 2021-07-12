﻿using ConsoleRogue.Components.Actors;
using ConsoleRogue.Components.Drivers;
using ConsoleRogue.Components.Map;
using ConsoleRogue.Customizables;
using RLNET;
using System;

namespace ConsoleRogue
{
    class Program
    {
        // Size of screens and parent console object, other screens will be nested within
        private static readonly int screenWidth = 100;
        private static readonly int screenHeigth = 70;
        private static RLRootConsole rootConsole;

        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int mapWidth = 80;
        private static readonly int mapHeight = 48;
        private static RLConsole mapConsole;

        // Below the map console is the message console which displays attack rolls and other information
        private static readonly int messageWidth = 80;
        private static readonly int messageHeight = 11;
        private static RLConsole messageConsole;

        // The stat console is to the right of the map and display player and monster stats
        private static readonly int statWidth = 20;
        private static readonly int statHeight = 70;
        private static RLConsole statConsole;

        // Above the map is the inventory console which shows the players equipment, abilities, and items
        private static readonly int inventoryWidth = 80;
        private static readonly int inventoryHeight = 11;
        private static RLConsole inventoryConsole;
        private static bool reRender = true;

        public static Tileset tileset { get; set; }

        public static Player player { get; set; }

        public static MovementDriver moveDrive { get; set; }
        static void Main(string[] args)
        {
            player = new Player();

            string fontFile = "terminal8x8.png";
            string screenName = "Test";

            // Loading of the font file "terminal8x8.png"
            rootConsole = new RLRootConsole(fontFile, screenWidth, screenHeigth, 8, 8, 1f, screenName);
            rootConsole.Update += onRootConsoleUpdate;
            rootConsole.Render += onRootConsoleRender;
            
            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidth, messageHeight);
            statConsole = new RLConsole(statWidth, statHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeight);

            Generator generator = new Generator( mapWidth, mapHeight, 8, 17 );
            int numSeed = Environment.TickCount;
            Random seed = new Random(numSeed);
            tileset = generator.generateMap(seed);

            mapConsole.SetBackColor(0, 0, mapWidth, mapHeight, ObjectColoring.background);

            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, ObjectColoring.messageColor);
            messageConsole.Print(1, 1, "Messages", ObjectColoring.textColor);

            statConsole.SetBackColor(0, 0, statWidth, statHeight, ObjectColoring.statsColor);
            statConsole.Print(1, 1, "Stats", ObjectColoring.textColor);

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, ObjectColoring.inventoryColor);
            inventoryConsole.Print(1, 1, "Inventory", ObjectColoring.textColor);

            moveDrive = new MovementDriver();

            rootConsole.Run();
        }

        private static void onRootConsoleUpdate( object sender, UpdateEventArgs e)
        {
            RLKeyPress key = rootConsole.Keyboard.GetKeyPress();
            if( key != null)
            {
                if(key.Key != RLKey.Escape)
                {
                    reRender = moveDrive.mapKeyToDir( player, key);
                }
                else
                {
                    rootConsole.Close();
                }
            }
            tileset.updatePlayerVisibility( player );
        }

        private static void onRootConsoleRender( object sender, UpdateEventArgs e)
        {
            if (reRender)
            {
                tileset.Draw(mapConsole);
                player.Draw( mapConsole, tileset );
                RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, inventoryHeight);
                RLConsole.Blit(statConsole, 0, 0, statWidth, statHeight, rootConsole, mapWidth, 0);
                RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, 0, screenHeigth - messageHeight);
                RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeight, rootConsole, 0, 0);
                
                rootConsole.Draw();
                reRender = false;
            }            
        }
    }
}
