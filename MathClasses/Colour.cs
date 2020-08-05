using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Colour
    {
        public uint colour;

        [Flags]
        private enum ColourBitmask : uint
        {
            RED = 4278190080,
            GREEN = 16711680,
            BLUE = 65280,
            ALPHA = 255,
        }

        public Colour()
        {
            colour = 0;
        }

        public Colour(byte red, byte green, byte blue, byte alpha)
        {
            colour = (uint)(red << 24) + (uint)(green << 16) + (uint)(blue << 8) + (uint)alpha;
        }

        public byte GetRed()
        {
            return (byte)((colour & (uint)ColourBitmask.RED) >> 24);
        }
        public void SetRed(byte red)
        {
            colour = (uint)((byte)(colour & ~(uint)ColourBitmask.RED) | red << 24);
        }

        public byte GetGreen()
        {
            return (byte)((colour & (uint)ColourBitmask.GREEN) >> 16);
        }
        public void SetGreen(byte green)
        {
            colour = (uint)((byte)(colour & ~(uint)ColourBitmask.GREEN) | green << 16);
        }

        public byte GetBlue()
        {
            return (byte)((colour & (uint)ColourBitmask.BLUE) >> 8);
        }
        public void SetBlue(byte blue)
        {
            colour = (uint)((byte)(colour & ~(uint)ColourBitmask.BLUE) | blue << 8);
        }

        public byte GetAlpha()
        {
            return (byte)((colour & (uint)ColourBitmask.ALPHA));
        }
        public void SetAlpha(byte alpha)
        {
            colour = (uint)((byte)(colour & ~(uint)ColourBitmask.ALPHA) | alpha);
        }

    }
}
