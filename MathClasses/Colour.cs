using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClassesAidan
{
    public class Colour
    {
        //Data storage of the colour
        public uint colour;

        //Bitmasks for the component colours
        [Flags]
        private enum ColourBitmask : uint
        {
            RED = 4278190080,
            GREEN = 16711680,
            BLUE = 65280,
            ALPHA = 255,
        }

        //Base constructor is black
        public Colour()
        {
            colour = 0;
        }

        //Creates a colour from provided component bytes by bitshifting in succession
        public Colour(byte red, byte green, byte blue, byte alpha)
        {
            colour = (uint)(red << 24) + (uint)(green << 16) + (uint)(blue << 8) + (uint)alpha;
        }

        //Getters get the component colour by masking for the correct byte, then bitshifting them to the appropriate values
        //Setters set the component colour by first blanking out the correct byte, then combining with the new colour byte bitshifted to the correct position.
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
        //

    }
}
