using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ZBuffer.Shapes
{
    public abstract class MShape
    {
        public abstract HashSet<Point3D> GetHashedPoints();
        public abstract List<Point3D> GetPoints();
    }
}
