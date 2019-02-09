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
        void Rotate(MShape shape, double sx, double sy, double sz);
        void RotateRange(List<MShape> shapes, double sx, double sy, double sz);

        void Translate(MShape shape, float sx, float sy, float sz);
        void TranslateRange(List<MShape> shapes, float sx, float sy, float sz);

        void Scale(MShape shape, float sx, float sy, float sz);
        void ScaleRange(List<MShape> shapes, float sx, float sy, float sz);
    }
}
