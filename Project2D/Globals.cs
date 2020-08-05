using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTest
{
    static class Globals
    {
        private static float deltaTime;

        internal static float DeltaTime { get => deltaTime; set => deltaTime = value; }

        internal static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        internal static void Log(float val)
        {
            Console.WriteLine(val);
        }

        internal static void Log(Coordinate val)
        {
            Console.WriteLine("{" + val.x + "," + val.y + "}");
        }
    }

    struct Coordinate
    {
        internal float x, y;
        internal Coordinate(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
