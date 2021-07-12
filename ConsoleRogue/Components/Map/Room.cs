using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Components.Map
{
    class Room
    {
        public int width;
        public int height;
        public int centerX;
        public int centerY;
        public int bottomEdge;
        public int topEdge;
        public int leftEdge;
        public int rightEdge;

        public Room( int x, int y, int width, int height)
        {
            if(width % 2 != 0)
            {
                width -= 1;
            }
            if(height % 2 != 0)
            {
                height -= 1;
            }
            this.width = width;
            this.height = height;
            this.centerX = x;
            this.centerY = y;
            this.leftEdge = x - (width / 2);
            this.rightEdge = x + (width / 2);
            this.topEdge = y - (height / 2);
            this.bottomEdge = y + (height / 2);
        }
    }
}
