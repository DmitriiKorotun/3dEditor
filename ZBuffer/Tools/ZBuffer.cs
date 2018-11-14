using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Tools
{
    class ZBuffer
    {
        public struct Cell
        {
            float z;  //координата z
            int argb;  //цвет пикселя
        };


        private Cell[] buffer;
        public Cell[] Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }

        public ZBuffer(int sceneSize)
        {
            buffer = new Cell[sceneSize];
        }
    }
}
