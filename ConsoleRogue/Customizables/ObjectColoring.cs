using System;
using System.Collections.Generic;
using System.Text;
using RLNET;

namespace ConsoleRogue.Customizables
{
    class ObjectColoring
    {
        public static RLColor background = RLColor.Black;


        public static RLColor textColor = RLColor.White;

        // Menu elements
        public static RLColor inventoryColor = Pallete.SecondaryDark;
        public static RLColor messageColor = RLColor.Gray;
        public static RLColor statsColor = Pallete.TerciaryDarkLight;
        public static RLColor healthBarLight = Pallete.Primary;
        public static RLColor healthBarDark = Pallete.PrimaryDark;

        // Map elements
        public static RLColor wallVisible = Pallete.PrimaryLight;
        public static RLColor floorVisible = Pallete.SecondaryLight;
        public static RLColor wallInvisible = Pallete.PrimaryDark;
        public static RLColor floorInvisible = Pallete.SecondaryDark;

        public static RLColor player = Pallete.Complementary;
        public static RLColor enemy = Pallete.TerciaryDark;

    }
}
