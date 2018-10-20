using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZBuffer;
using ZBuffer.Affine_Transformation;
using ZBuffer.Shapes;

namespace GraphicsProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var scene = new Scene((int)screen.Width, (int)screen.Height, 1);
            var painter = new Painter();

            Random rand = new Random();

            var ls = new Stopwatch();
            ls.Start();
            var box1 = new MBox(new MPoint(75, 75, 0), 50, 50, 50);
            var box2 = new MBox(new MPoint(75, 75, 0), 50, 50, 50);
            var box3 = new MBox(new MPoint(75, 75, 0), 50, 50, 50);
            new ShapeEditor().RotateZ(box2, 80);
            //new ShapeEditor().RotateZVertices(box3.GetVertices(), 25);
            //scene.AddShape(box1);
            scene.AddShape(box2);
            //scene.AddShape(box3);

            //for (int i = 0; i < 100; ++i)
            //{
            //    scene.AddShape(new MFacet(new MPoint(rand.Next((int)screen.Width), rand.Next((int)screen.Height), 0),
            //        new MPoint(rand.Next((int)screen.Width), rand.Next((int)screen.Height), 0), new MPoint(rand.Next((int)screen.Width), rand.Next((int)screen.Height), 0)));
            //}
            var lol = ls.Elapsed;
            //scene.AddShape(new MFacet(new MPoint(100, 100, 0), new MPoint(100, 200, 0), new MPoint(200, 100, 0)));

            //painter.DrawPoint(scene, new System.Windows.Media.Media3D.Point3D(100, 100, 100));
            //painter.DrawFacet(scene, new MFacet(new MPoint(1, 1, 0), new MPoint(100, 100, 0), new MPoint(1, 1, 0)));
            //Random rand = new Random();
            //for (int i = 0; i < 10; ++i)
            //{
            //    painter.DrawFacet(scene, new MFacet(new MPoint(rand.Next((int)screen.Width), rand.Next((int)screen.Height), 0),
            //        new MPoint(rand.Next((int)screen.Width), rand.Next((int)screen.Height), 0), new MPoint(1, 1, 0)));
            //}
            //painter.DrawFacet(scene, new MFacet(new MPoint(100, 100, 0), new MPoint(80, 80, 0), new MPoint(1, 1, 0)));
            //painter.DrawFacet(scene, new MFacet(new MPoint(1, 1, 0), new MPoint(100, 100, 0), new MPoint(1, 1, 0)));

            ls.Restart();
            screen.Source = scene.Render();
            var lol2 = ls.Elapsed;
            var lol4 = ls.Elapsed;
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            screen.Source = null;
        }

        private void btn_createShape_Click(object sender, RoutedEventArgs e)
        {
            float width = Int32.Parse(tb_width.Text);
            float length = Int32.Parse(tb_width.Text);
            float height = Int32.Parse(tb_width.Text);

            float x = Int32.Parse(tb_leftCornerX.Text);
            float y = Int32.Parse(tb_leftCornerY.Text);
            float z = Int32.Parse(tb_leftCornerZ.Text);

            var leftFaceCorner = new MPoint(x, y, z);

            MBox mbox = new MBox(leftFaceCorner, width, length, height);
        }

        private void btn_rotateLeft_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
