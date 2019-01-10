using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
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
using EmuEngine;
using EmuEngine.Affine_Transformation;
using EmuEngine.Shapes;
using System.IO;
using System.Drawing;
using System.Data;
using static EmuEngine.EmuEngineExceptions;

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
            var testBox = new MBox(new MPoint(0, 0, 0), 50, 50, 50);
            //MCommonPrimitive mcylinder = new MTopCylinder(new MPoint(-25, 25, 40), 10, 20, 30);
            Shuttle shuttle = new Shuttle(0, 0, 0);
            new ShapeEditor().Translate(shuttle, 0, 0, 250);
            new ShapeEditor().Rotate(shuttle, 90, 0, 0);

            Scene.AddShape(shuttle);
            //Scene.AddShape(mcylinder);

            screen.Source = Scene.Render();

            //screen.Source = BitmapToImageSource(Scene.RenderBitmap());
            FillObjectsGrid();            
        }

        //TO DEL
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            Scene.Clear();

            screen.Source = null;

            FillObjectsGrid();
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

            //MCommonPrimitive mbox = new MSideCylinder(leftFaceCorner, width, length, height);
            MCommonPrimitive mbox = new MSideCylinder(leftFaceCorner, 10, height);
        }

        private async void btn_rotateLeft_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                var editor = new ShapeEditor();

                for (int i = 0; i < 500; ++i)
                {
                    editor.RotateRange(Scene.SelectedShapes, 10, 10, 0);

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
            //screen.Source = BitmapToImageSource(Scene.RenderBitmap());
            screen.Source = Scene.Render();
        }
        
        //TODO Deal with event firing only after image.source is setted
        private void screen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //e.GetPosition(screen);
        }

        private void menuItem_newScene_Click(object sender, RoutedEventArgs e)
        {
            Scene.Clear();

            screen.Source = null;

            FillObjectsGrid();
        }


        private void menuItem_saveScene_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

                var dialogResult = dlg.ShowDialog();

                if (dialogResult == true)
                {
                    throw new FormatException();
                    GraphicsProjectIO.WriteToXmlFile(dlg.FileName, Scene);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("При попытке сохранения сцены что-то пошло не так... ");
            }
        }

        //TODO Change this to something more adequate
        private void menuItem_openScene_Click(object sender, RoutedEventArgs e)
        {
            try
            {             
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                var dialogResult = dlg.ShowDialog();

                if (dialogResult == true)
                {
                    throw new FormatException();
                    var deserialaziedScene = GraphicsProjectIO.ReadFromXmlFile<Scene>(dlg.FileName);

                    Scene = deserialaziedScene;

                    screen.Source = Scene.Render();
                }
            }
            catch (FormatException)
            {               
                MessageBox.Show("При попытке открытия сцены что-то пошло не так... ");
            }
        }

        private void btn_rotate_Click(object sender, RoutedEventArgs e)
        {
            GetSelected();
            TransformShapes(RotateSelected, tb_TransformationX.Text, tb_TransformationY.Text, tb_TransformationZ.Text, 0);
        }

        private void btn_move_Click(object sender, RoutedEventArgs e)
        {
            GetSelected();
            TransformShapes(MoveSelected, tb_TransformationX.Text, tb_TransformationY.Text, tb_TransformationZ.Text, 0);
        }

        private void btn_scale_Click(object sender, RoutedEventArgs e)
        {
            GetSelected();
            TransformShapes(ScaleSelected, tb_TransformationX.Text, tb_TransformationY.Text, tb_TransformationZ.Text, 1);
        }

        private void TransformShapes(Transofrmation operation, string sx, string sy, string sz, float defaultInputValue)
        {
            try
            {
                Vector3 coordinates = GetCoordinates(sx, sy, sz, defaultInputValue);

                operation(coordinates);

                screen.Source = Scene.Render();
                //screen.Source = BitmapToImageSource(Scene.RenderBitmap());
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (UIException ex) when (ex.Status == UIExceptions.TransformationInputEmpty)
            {
                MessageBox.Show("Заполните хотя бы одно из полей выше");
            }
        }

        private void MoveSelected(Vector3 coordinates)
        {          
            new ShapeEditor().TranslateRange(Scene.SelectedShapes, coordinates.X, coordinates.Y, coordinates.Z);
        }

        private void ScaleSelected(Vector3 multiplier)
        {
            new ShapeEditor().ScaleRange(Scene.SelectedShapes, multiplier.X, multiplier.Y, multiplier.Z);
        }

        private void RotateSelected(Vector3 angles)
        {
            new ShapeEditor().RotateRange(Scene.SelectedShapes, angles.X, angles.Y, angles.Z);
        }

        private Vector3 GetCoordinates(string sx, string sy, string sz, float defaultValue)
        {
            if (sx == "" && sy == "" && sz == "")
                throw new UIException(UIExceptions.TransformationInputEmpty, "All transformation input fields are empty");

            Vector3 coordinates = new Vector3(
                sx != "" ? float.Parse(sx) : defaultValue,
                sy != "" ? float.Parse(sy) : defaultValue,
                sz != "" ? float.Parse(sz) : defaultValue
                );

            return coordinates;
        }

        private delegate void Transofrmation(Vector3 coordinates);

        private void btn_createCamera_Click(object sender, RoutedEventArgs e)
        {
            float fov = Int32.Parse(tb_CameraFov.Text),
                vfov = Int32.Parse(tb_CameraVfov.Text),
                n = Int32.Parse(tb_CameraNear.Text),
                f = Int32.Parse(tb_CameraFar.Text);

            Scene.StageManager.CreateCamera(fov, vfov, n, f);

            Render();
        }

        private void btn_switchCamera_Click(object sender, RoutedEventArgs e)
        {
            Scene.StageManager.SwitchCameraType();

            Render();
        }

        private void btn_rotateCamera_Click(object sender, RoutedEventArgs e)
        {
            Vector3 coordinates = GetCoordinates(tb_TransformationX.Text, tb_TransformationY.Text, tb_TransformationZ.Text, 0);
            Scene.StageManager.RotateCamera(coordinates.X, coordinates.Y, coordinates.Z);
            Render();
        }

        private void btn_moveCamera_Click(object sender, RoutedEventArgs e)
        {
            Vector3 coordinates = GetCoordinates(tb_TransformationX.Text, tb_TransformationY.Text, tb_TransformationZ.Text, 0);
            Scene.StageManager.MoveCamera(coordinates.X, coordinates.Y, coordinates.Z);
            Render();
        }

        private void FillObjectsGrid()
        {
            // Creating DataSource here as datatable having two columns
            DataTable dt = new DataTable();
            dt.Columns.Add("Имя");
            dt.Columns[0].ReadOnly = true;
            dt.Columns.Add("Выбран", typeof(bool));
            dt.Columns.Add("Объект", typeof(MCommonPrimitive));

            var items = new List<MCommonPrimitive>();

            if (dgrid_Objects.ItemsSource != null)
            {
                foreach (DataRowView dr in dgrid_Objects.ItemsSource)
                {
                    if ((bool)dr[1])
                        items.Add(dr[2] as MCommonPrimitive);
                }
            }

            foreach (MCommonPrimitive shape in Scene.Shapes)
            {
                if (shape.Name != "")
                {
                    var row = dt.NewRow();
                    row["Имя"] = shape.Name;                
                    row["Объект"] = shape;

                    if (!items.Contains(shape))
                        row["Выбран"] = false;
                    else
                        row["Выбран"] = true;

                    dt.Rows.Add(row);
                }
            }

            // Set AutoGenerateColumns true to generate columns as per datasource.
            dgrid_Objects.AutoGenerateColumns = true;
            
            // Finally bind the datasource to datagridview.
            dgrid_Objects.DataContext = dt;
            dgrid_Objects.CanUserAddRows = false;
        }

        private void GetSelected()
        {
            Scene.SelectedShapes.Clear();

            foreach (DataRowView dr in dgrid_Objects.ItemsSource)
            {
                var isSelected = (bool)dr[1];

                if (isSelected)
                    Scene.SelectedShapes.Add(dr[2] as MCommonPrimitive);
            }
        }

        private void ChangeParameters()
        {           
            GetSelected();
            try
            {
                ChangeEngineRadius();
                ChangeEngineCount();
                ChangeBodyRadius();
                ChangeBodyHeight();
                ChangeWingsCount();
                ChangeWingLength();
            }
            catch (FormatException)
            {
                MessageBox.Show("Вы неверно ввели один или более параметров");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.DoesntHaveFreeSpaceForEngines)
            {
                MessageBox.Show("Не хватает свободного места для двигателей");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.EngineRadiusTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленький радиус для двигателя");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.BodyRadiusTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленький радиус для корпуса");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.DoesntHaveFreeSpaceForWings)
            {
                MessageBox.Show("Не хватает свободного места для крыльев");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.DoesntHaveFreeSpaceForTop)
            {
                MessageBox.Show("Не хватает свободного места для носовой части");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.BodyHeightTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленькую высоту для корпуса");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.TopRadiusTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленький радиус для носовой части");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.WingLengthTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленькую длину для крыла");
            }
            finally
            {
                Render();
            }
        }

        private void ChangeEngineRadius()
        {
            if (tb_EngineRadius.Text == "")
                return;

            var engineRadius = int.Parse(tb_EngineRadius.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeEngineRadius(engineRadius);
            }
        }

        private void ChangeEngineCount()
        {
            if (tb_EnginesCount.Text == "")
                return;

            var enginesCount = int.Parse(tb_EnginesCount.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeEngineCount(enginesCount);
            }
        }

        private void ChangeLegRadius()
        {
            if (tb_LegRadius.Text == "")
                return;

            var legRadius = int.Parse(tb_LegRadius.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeWingLegRadius(legRadius);
            }
        }

        private void ChangeLegLength()
        {
            if (tb_legLength.Text == "")
                return;

            var legLength = int.Parse(tb_legLength.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeWingLegLength(legLength);
            }
        }

        private void ChangeLegAngle()
        {
            if (tb_LegAngle.Text == "")
                return;

            var legAngle = int.Parse(tb_LegAngle.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeWingLegAngle(legAngle);
            }
        }

        private void ChangeBodyRadius()
        {
            if (tb_BodyRadius.Text == "")
                return;

            var bodyRadius = int.Parse(tb_BodyRadius.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeBodyRadius(bodyRadius);
            }
        }

        private void ChangeBodyHeight()
        {
            if (tb_BodyHeight.Text == "")
                return;

            var bodyHeight = int.Parse(tb_BodyHeight.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeBodyHeight(bodyHeight);
            }
        }

        private void ChangeTopRadius()
        {
            if (tb_NoseRadius.Text == "")
                return;

            var topRadius = int.Parse(tb_NoseRadius.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeTopRadius(topRadius);
            }
        }

        private void ChangeWingsCount()
        {
            if (tb_WingsCount.Text == "")
                return;

            var wingsCount = int.Parse(tb_WingsCount.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeWingsCount(wingsCount);
            }
        }

        private void ChangeWingLength()
        {
            if (tb_WingLength.Text == "")
                return;

            var wingLength = int.Parse(tb_WingLength.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeWingsLength(wingLength);
            }
        }

        private void ChangeDoorsCount()
        {
            if (tb_DoorsCount.Text == "")
                return;

            var doorsCount = int.Parse(tb_DoorsCount.Text);

            foreach (Shuttle shuttle in Scene.SelectedShapes)
            {
                shuttle.ChangeDoorsCount(doorsCount);
            }
        }

        private void btn_parametrizationDo1_Click(object sender, RoutedEventArgs e)
        {
            ChangeParameters();
        }

        private void btn_parametrizationDo2_Click(object sender, RoutedEventArgs e)
        {
            ChangeParameters2();
        }

        private void ChangeParameters2()
        {
            GetSelected();
            try
            {
                ChangeTopRadius();
                ChangeLegRadius();
                ChangeLegAngle();
                ChangeLegLength();
                ChangeDoorsCount();
            }
            catch (FormatException)
            {
                MessageBox.Show("Вы неверно ввели один или более параметров");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.DoesntHaveFreeSpaceForEngines)
            {
                MessageBox.Show("Не хватает свободного места для двигателей");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.EngineRadiusTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленький радиус для двигателя");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.BodyRadiusTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленький радиус для корпуса");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.DoesntHaveFreeSpaceForWings)
            {
                MessageBox.Show("Не хватает свободного места для крыльев");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.DoesntHaveFreeSpaceForTop)
            {
                MessageBox.Show("Не хватает свободного места для носовой части");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.BodyHeightTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленькую высоту для корпуса");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.TopRadiusTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленький радиус для носовой части");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.WingLegAngleTooBig)
            {
                MessageBox.Show("Вы указали слишком большой угол для опорной части");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.WingLegAngleTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленький угол для опорной части");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.WingLegLengthTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленькую длину для опорной части");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.WingLegRadiusTooSmall)
            {
                MessageBox.Show("Вы указали слишком маленький радиус для опорной части");
            }
            catch (ShuttleException ex) when (ex.Status == ShuttleExceptions.DoesntHaveFreeSpaceForDoors)
            {
                MessageBox.Show("Не хватает свободного места для дверей");
            }
            finally
            {
                Render();
            }
        }
    }
}
