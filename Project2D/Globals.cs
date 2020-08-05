using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTest
{
    static class Globals
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static void Log(float val)
        {
            Console.WriteLine(val);
        }

        public static void Log(Coordinate val)
        {
            Console.WriteLine("{" + val.x + "," + val.y + "}");
        }
    }

    struct Coordinate
    {
        public float x, y;
        public Coordinate(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
