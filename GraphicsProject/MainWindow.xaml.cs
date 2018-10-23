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
        private Scene Scene { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            Scene = new Scene((int)screen.Width, (int)screen.Height, 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var painter = new Painter();

            Random rand = new Random();

            var ls = new Stopwatch();
            ls.Start();
            var box1 = new MBox(new MPoint(0, 0, 0), 50, 50, 50);
            var box2 = new MBox(new MPoint(75, 75, 0), 50, 50, 50);
            var box3 = new MBox(new MPoint(75, 75, 0), 50, 50, 50);
            var mfacet = new MFacet(new MPoint(50, 35, 0), new MPoint(100, 23, 0), new MPoint(72, 75, 0));
            var mfacet2 = new MFacet(new MPoint(50, 35, 0), new MPoint(100, 23, 0), new MPoint(72, 75, 0));
            //new ShapeEditor().RotateY(box1, 20);
            //new ShapeEditor().RotateZ(box1, 90);
            //new ShapeEditor().Move(box1, -50, 0, 0);
            //new ShapeEditor().ProjectShape(mfacet2, Scene.CurrentCamera);
            //new ShapeEditor().RotateZVertices(box3.GetVertices(), 25);
            Scene.AddShape(box1);
            //Scene.AddShape(mfacet);
            //Scene.AddShape(mfacet2);
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
            screen.Source = Scene.Render();
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

        private void btn_rotateRight_Click(object sender, RoutedEventArgs e)
        {

        }

        //TODO Deal with event firing only after image.source is setted
        private void screen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.GetPosition(screen);
        }

        private void screen_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void menuItem_newScene_Click(object sender, RoutedEventArgs e)
        {
            Scene = new Scene((int)screen.Width, (int)screen.Height, 1);

            screen.Source = Scene.Render();
        }

        
        private void menuItem_saveScene_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            var dialogResult = dlg.ShowDialog();

            if (dialogResult == true)
            {
                GraphicsProjectIO.WriteToXmlFile(dlg.FileName, Scene);
            }
        }

        //TODO Change this to something more adequate
        private void menuItem_openScene_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            var dialogResult = dlg.ShowDialog();

            if (dialogResult == true)
            {
                var deserialaziedScene = GraphicsProjectIO.ReadFromXmlFile<Scene>(dlg.FileName);

                deserialaziedScene.Bitmap = Scene.Bitmap;

                Scene = deserialaziedScene;

                screen.Source = Scene.Render();
            }
        }
    }
}
