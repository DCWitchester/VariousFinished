using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDisplay.Miscellaneous
{
    public enum PolygonColor
    {
        black = 0,
        red = 1,
        green = 2,
        blue = 3,
        yellow = 4,
        cyan = 5,
        magenta = 6,
        indigo = 7
    }
    public class ColorSettings
    {
        public static Color getColorOfPolygonColor(Int32 polygonColor) 
        {
            return ((PolygonColor)polygonColor) switch
            {
                PolygonColor.black      => Color.Black,
                PolygonColor.red        => Color.Red,
                PolygonColor.green      => Color.Green,
                PolygonColor.blue       => Color.Blue,
                PolygonColor.yellow     => Color.Yellow,
                PolygonColor.cyan       => Color.Cyan,
                PolygonColor.magenta    => Color.Magenta,
                PolygonColor.indigo     => Color.Indigo,
                _ => Color.Black,
            };
        }
    }
}
