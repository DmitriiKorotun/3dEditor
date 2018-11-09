using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

            var box1 = new MBox(new MPoint(0, 0, 0), 50, 50, 50);

            Scene.AddShape(box1);

            screen.Source = Scene.Render();

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

        private async void btn_rotateLeft_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                var editor = new ShapeEditor();

                for (int i = 0; i < 500; ++i)
                {
                    editor.RotateRange(Scene.SelectedShapes, 10, 0, 0);

                    if (screen.Dispatcher.CheckAccess())
                        Render();
                    else
                        screen.Dispatcher.Invoke(new Action(Render));

                    Thread.Sleep(500);
                }
            });
        }

        private void Render()
        {
            screen.Source = Scene.Render();
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

                Scene = deserialaziedScene;

                screen.Source = Scene.Render();
            }
        }
    }
}
