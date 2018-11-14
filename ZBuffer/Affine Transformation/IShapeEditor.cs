using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmuEngine.Shapes;

namespace EmuEngine.Affine_Transformation
{
    interface IShapeEditor
    {
        void Rotate(MCommonPrimitive shape, double sx, double sy, double sz);
        void RotateRange(List<MCommonPrimitive> shapes, double sx, double sy, double sz);

        void Translate(MCommonPrimitive shape, float sx, float sy, float sz);
        void TranslateRange(List<MCommonPrimitive> shapes, float sx, float sy, float sz);

        void Scale(MCommonPrimitive shape, float sx, float sy, float sz);
        void ScaleRange(List<MCommonPrimitive> shapes, float sx, float sy, float sz);
    }
}
