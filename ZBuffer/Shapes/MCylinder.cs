using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes
{
    abstract class MCylinder
    {
        MPoint CenterBot { get; set; }  //центр нижнего основания
        MPoint CenterTop { get; set; }  //центр верхнего основания
        float Radius { get; set; }  //радиус
        float Heigth { get; set; }  //высота
        MPoint[] BottomDots { get; set; } //точки нижней окружности 36
        MPoint[] TopDots { get; set; }  //точки верхней окружности 36
        MFacet[] Facets { get; set; }  //боковые грани 72
        MFacet[] CircleFacets { get; set; }  //грани оснований 72

        public MCylinder(MPoint centerBot, float radius, float heigth)
        {
            CenterBot = centerBot;

            Radius = radius;

            Heigth = heigth;
        }

        private MPoint[] CalcDots(MPoint center)
        {
            return new MPoint[36];
        }
    }
}
