using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine
{
    public class Screen
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Far { get; set; }
        public int Near { get; set; }

        public Screen(int width, int height, int far, int near)
        {
            Width = width;
            Height = height;
            Far = far;
            Near = near;
        }
    }
}
