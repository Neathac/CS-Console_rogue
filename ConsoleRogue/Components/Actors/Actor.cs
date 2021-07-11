﻿using ConsoleRogue.Components.Map;
using ConsoleRogue.Customizables;
using ConsoleRogue.Interfaces;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Components.Actors
{
    class Actor : IActor, IDrawable
    {
        public string name { get; set; }
        public int perception { get; set; }
        public RLColor colour { get; set; }
        public char symbol { get; set; }
        public int xCoor { get; set; }
        public int yCoor { get; set; }
        public void Draw( RLConsole console, RogueSharp.IMap map)
        {
            if( !map.GetCell( xCoor, yCoor ).IsExplored )
            {
                return;
            }

            if(map.IsInFov(xCoor, yCoor))
            {
                console.Set(xCoor, yCoor, colour, ObjectColoring.floorVisible, symbol);
            }
            else
            {
                console.Set(xCoor, yCoor, ObjectColoring.floorInvisible, ObjectColoring.background, Tileset.passable);
            }
        }
    }
}