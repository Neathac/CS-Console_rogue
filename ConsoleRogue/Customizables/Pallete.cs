using RLNET;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Customizables
{
    class Pallete
    {
        // Primary - Red
        // Secondary - Blue
        // Terciary - Yellow
        // Complementary - Green
        public static RLColor Primary = new RLColor(167, 46, 49);
        public static RLColor Secondary = new RLColor(51, 44, 117);
        public static RLColor Terciary = new RLColor(168, 140, 46);
        public static RLColor Complementary = new RLColor(43, 136, 38);

        public static RLColor PrimaryDark = new RLColor(123, 1, 4);
        public static RLColor SecondaryDark = new RLColor(18, 13, 86);
        public static RLColor TerciaryDark = new RLColor(124, 96, 1);
        public static RLColor ComplementaryDark = new RLColor(6, 100, 1);

        //First shade keyword indicates dominant shade
        public static RLColor PrimaryDarkLight = new RLColor(146, 21, 24);
        public static RLColor SecondaryDarkLight = new RLColor(32, 27, 102);
        public static RLColor TerciaryDarkLight = new RLColor(147, 118, 21);
        public static RLColor ComplementaryDarkLight = new RLColor(22, 119, 17);

        public static RLColor PrimaryLight = new RLColor(213, 120, 122);
        public static RLColor SecondaryLight = new RLColor(98, 94, 150);
        public static RLColor TerciaryLight = new RLColor(215, 193, 121);
        public static RLColor ComplementaryLight = new RLColor(102, 174, 98);

        public static RLColor PrimaryLightDark = new RLColor(192, 80, 83);
        public static RLColor SecondaryLightDark = new RLColor(72, 67, 135);
        public static RLColor TerciaryLightDark = new RLColor(193, 168, 81);
        public static RLColor ComplementaryLightDark = new RLColor(70, 157, 65);
    }
}
