using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EmuEngine.Tools;
using EmuEngine.Shapes;
using System.Windows.Media.Media3D;
using System.Runtime.Serialization;
using EmuEngine.Affine_Transformation;
using System.Drawing;
using System.Numerics;

namespace EmuEngine
{
    //TODO Think about changing DataSerializer to XmlSerializer
    [DataContract]
    [KnownType(typeof(MFacet))]
    public class Scene
    {
        public int Height { get; set; }
        public int Width { get; set; }

        //TODO Make public?
        [DataMember]
        public List<MCommonPrimitive> Shapes { get; set; }
        [DataMember]
        public List<MCommonPrimitive> SelectedShapes { get; set; }
        private byte[] pixels;
        private EmuMath.ZBuffer zBuffer { get; set; }
        private WriteableBitmap bitmap;
        public StageManager StageManager { get; set; }


        public Scene(int width, int heigth, int z)
        {
            Width = width;
            Height = heigth;

            pixels = new byte[Height * Width * 3];

            StageManager = new StageManager();

            //CurrentCamera = new Tools.OrthographicCamera(-160, 160, -90, 90, -50, 50);

            Shapes = new List<MCommonPrimitive>();

            //TODO Rework
            SelectedShapes = new List<MCommonPrimitive>();
        }

        public void SwitchCameraType()
        {
            //CurrentCamera
        }

        public WriteableBitmap Render()
        {
            new ShapeEditor().GetTransformedShapes(Shapes, StageManager.CurrentCamera);

            byte[] pixels = Enumerable.Repeat((byte)255, Height * Width * 3).ToArray();

            var facets = GetAllFacets();
            var zbuffer = new EmuMath.ZBuffer(pixels, Width, Height);

            for (int i = 0; i < facets.Count; ++i)
            {
                if (!facets[i].Vertices[0].IsClipped || !facets[i].Vertices[1].IsClipped || !facets[i].Vertices[2].IsClipped)
                {
                    var v0 = new System.Numerics.Vector3(facets[i].Vertices[0].X, facets[i].Vertices[0].Y, facets[i].Vertices[0].Z);
                    var v1 = new System.Numerics.Vector3(facets[i].Vertices[1].X, facets[i].Vertices[1].Y, facets[i].Vertices[1].Z);
                    var v2 = new System.Numerics.Vector3(facets[i].Vertices[2].X, facets[i].Vertices[2].Y, facets[i].Vertices[2].Z);

                    zbuffer.DrawTriangleToXyBuffer(v0, v1, v2, facets[i].Vertices[0].ARGB);
                }
            }

            return new Painter().DrawSceneByPoints(this.Width, this.Height, pixels);
        }

        //public WriteableBitmap Render()
        //{
        //    new ShapeEditor().GetTransformedShapes(Shapes, StageManager.CurrentCamera);

        //    var points = GetAllPoints();

        //    //for (int i = 0; i < facets.Count; ++i)
        //    //{
        //    //    if (!facets[i].Vertices[0].IsClipped || !facets[i].Vertices[1].IsClipped || !facets[i].Vertices[2].IsClipped)
        //    //    {
        //    //        var v0 = new System.Numerics.Vector3(facets[i].Vertices[0].X, facets[i].Vertices[0].Y, facets[i].Vertices[0].Z);
        //    //        var v1 = new System.Numerics.Vector3(facets[i].Vertices[1].X, facets[i].Vertices[1].Y, facets[i].Vertices[1].Z);
        //    //        var v2 = new System.Numerics.Vector3(facets[i].Vertices[2].X, facets[i].Vertices[2].Y, facets[i].Vertices[2].Z);

        //    //        zbuffer.DrawTriangleToXyBuffer(v0, v1, v2, facets[i].Vertices[0].ARGB);
        //    //    }
        //    //}

        //    return new Painter().DrawSceneByPoints(this.Width, this.Height, points);
        //}

        private void ClearBuffer()
        {
            Array.Clear(pixels, 0, pixels.Length);
        }

        public Bitmap RenderBitmap()
        {
            new ShapeEditor().GetTransformedShapes(Shapes, StageManager.CurrentCamera);

            List<MPoint> allPoints = GetAllPoints();

            return new Painter().DrawSceneByPointsBitmap(this.Width, this.Height, allPoints);
        }

        public void AddShape(MCommonPrimitive shape)
        {
            Shapes.Add(shape);
        }

        //TODO Rework
        public void RotateSelected(double angle)
        {
            //TODO remove this
            SelectedShapes = Shapes;

            var editor = new ShapeEditor();

            for (int i = 0; i < SelectedShapes.Count; ++i)
                editor.Rotate(SelectedShapes[i], 10, 0, 0);
        }

        private List<MPoint> GetAllPoints()
        {
            var allPoints = new List<MPoint>();

            foreach (MShape shape in Shapes)
            {
                allPoints.AddRange(shape.GetAllPoints());
            }
            //foreach(MShape shape in Shapes)
            //{
            //    foreach (MPoint point in shape.GetPoints())
            //        allPoints.Add(point);
            //}


            return allPoints;
        }

        private List<MFacet> GetAllFacets()
        {
            var allFacets = new List<MFacet>();

            foreach (MCommonPrimitive shape in Shapes)
            {
                allFacets.AddRange(shape.GetAllFacets());
            }
            //foreach(MShape shape in Shapes)
            //{
            //    foreach (MPoint point in shape.GetPoints())
            //        allPoints.Add(point);
            //}


            return allFacets;
        }

        public void Clear()
        {
            this.Shapes.Clear();
        }
    }
}
