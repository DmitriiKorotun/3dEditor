using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBuffer.Shapes;

namespace ZBuffer.Affine_Transformation
{
    interface IShapeEditor
    {
        void RotateX(MCommonPrimitive shape, double angle);
        void RotateY(MCommonPrimitive shape, double angle);
        void RotateZ(MCommonPrimitive shape, double angle);

        void Move(MCommonPrimitive shape, float Sx, float Sy, float Sz);

        void Scale(MCommonPrimitive shape, float Sx, float Sy, float Sz);
    }
}
