using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmuEngine.EmuEngineExceptions;

namespace EmuEngine.Shapes
{
    public class Shuttle : MCommonPrimitive
    {
        private const float defaultWingsRadius = 8;

        MSideCylinder Body { get; set; }
        MSideCylinder Top { get; set; }
        //MSideCylinder EngineToBody { get; set; }
        //MSideCylinder EngineExhaust { get; set; }

        MTopCylinder BodyHead { get; set; }
        //MTopCylinder EngineMount { get; set; }
        //MTopCylinder ExhaustMount { get; set; }
        Engine[] Engines { get; set; }
        Door[] Doors { get; set; }
        public Wing[] Wings { get; set; }

        MSideCylinder WingLeg { get; set; }

        public Shuttle()
        {
            Engines = new Engine[] { new Engine(0, 0, -20) };
            //Wings = new Wing[] { new Wing(new MPoint(-30, 30, 15), new MPoint(-32.5, 30, 15), 30, 5, 60), new Wing(new MPoint(30, 30, 15), new MPoint(32.5, 30, 15), 30, 5, 60) };

            Body = new MSideCylinder(new MPoint(0, 0, 0), 30, 60);
            Top = new MSideCylinder(new MPoint(0, 0, 80), 20, 10);
            //EngineToBody = new MSideCylinder(new MPoint(0, 0, -20), 20, 20);
            //EngineExhaust = new MSideCylinder(new MPoint(0, 0, -65), 15, 20);

            BodyHead = new MTopCylinder(new MPoint(0, 0, 60), 30, 20, 20);
            //EngineMount = new MTopCylinder(new MPoint(0, 0, -30), 15, 20, 10);
            //ExhaustMount = new MTopCylinder(new MPoint(0, 0, -45), 15, 10, 15);
        }

        public Shuttle(int x, int y, int z)
        {
            Engines = new Engine[] { new Engine(x, y, z - 20) };
            Wings = new Wing[] { new Wing(new MPoint(x - 45, y, z + 30), new MPoint(x - 47.5, 30, 15),
                30, 5, 60, 0, 0), new Wing(new MPoint(x + 45, y, z + 30), new MPoint(x + 47.5, 30, 15), 30, 5, 60, 0, 0) };

            Doors = new Door[] { new Door(new MPoint(0, 30, 30), new MPoint(x + 45, y, z + 30), 5, 30, 30, 0, 0) };

            Body = new MSideCylinder(new MPoint(0, 0, z), 30, 60, true);              // Корпус
            Top = new MSideCylinder(new MPoint(0, 0, z + 80), 20, 10, true);          // Часть носа

            //EngineToBody = new MSideCylinder(new MPoint(0, 0, z + -20), 20, 20, true); // Часть заднего фюзеляжа
            //EngineExhaust = new MSideCylinder(new MPoint(0, 0, z + -65), 15, 20, true); // Часть двигателя

            BodyHead = new MTopCylinder(new MPoint(0, 0, z + 60), 30, 20, 20, true);  // Часть носа
            //EngineMount = new MTopCylinder(new MPoint(0, 0, z + -30), 15, 20, 10, true); // Часть заднего фюзеляжа
            //ExhaustMount = new MTopCylinder(new MPoint(0, 0, z + -45), 15, 10, 15, true); // Часть двигателя

            //Door = new MBox(new MPoint(30, 30, z), 15, 10, 15); // Дверь
            //Wing = new MBox(new MPoint(50, 40, z + 10), 10, 40, 10); // Крыло
            //WingLeg = new MSideCylinder(new MPoint(0, 0, z + -45), 50, 10); // Устойчивая часть крыла
            RecalculateWings();
            RecalculateDoors();
            RecalculateColor();
        }

        private void RecalculateColor()
        {
            Body.SetColor(System.Drawing.Color.Red.ToArgb());
            BodyHead.SetColor(System.Drawing.Color.SkyBlue.ToArgb());
            Top.SetColor(System.Drawing.Color.SkyBlue.ToArgb());

            foreach (Engine engine in Engines)
                engine.SetColor(System.Drawing.Color.OliveDrab.ToArgb());

            foreach (Wing wing in Wings)
            {
                wing.SetColor(System.Drawing.Color.Turquoise.ToArgb());
                wing.WingCylinder.SetColor(System.Drawing.Color.Pink.ToArgb());
                wing.WingLeg.SetColor(System.Drawing.Color.Green.ToArgb());
                wing.WingLeg.Platform.SetColor(System.Drawing.Color.Orange.ToArgb());
            }

            foreach (Door door in Doors)
                door.Window.SetColor(System.Drawing.Color.LightBlue.ToArgb());
        }

        public void ChangeDoorsCount(int count)
        {
            if (Doors.Length < 1 || (GetAcceptableRadius(count, Body.GetRadius()) < 10))
                throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForDoors);
            //if (Engines.Length > 0 && GetAcceptableRadius(count, Body.GetRadius()) < Engines[0].EngineRadius)
            //throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForEngines);

            Door[] doors = null;

            if (count > Doors.Length)
            {
                doors = new Door[count];

                Array.Copy(Doors, doors, Doors.Length);
            }
            else if (count < Doors.Length)
            {
                doors = new Door[count];

                Array.Copy(Doors, doors, count);
            }
            else
                doors = Doors;

            Doors = doors;

            RecalculateDoors();
            RecalculateColor();
        }

        private void RecalculateDoors()
        {
            if (Doors.Length > 0)
            {
                float bodyRadius = Body.GetRadius();

                var neededDoorRadius = GetAcceptableRadius(Doors.Length, bodyRadius);

                if (10 <= neededDoorRadius)
                {
                    var da = 2 * Math.PI / Doors.Length;
                    double x, y, angle = 0;

                    float width, length, height, sx = 0, sy = 0;
                    MPoint wingCenter, cylinderCenter, legCenter;

                    for (int i = 0; i < Doors.Length; ++i, angle += da)
                    {
                        x = bodyRadius * Math.Cos(angle);
                        y = bodyRadius * Math.Sin(angle);

                        if (angle < Math.PI / 4 || angle >= 7 * Math.PI / 4)
                        {
                            width = Doors[0].Width;
                            length = Doors[0].Length;
                            height = 30;

                            wingCenter = new MPoint((float)x + length, (float)y, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX, wingCenter.SY, wingCenter.SZ);

                            sy = 90;
                            sx = 0;
                            //legCenter = new MPoint(cylinderCenter.SX + 20, cylinderCenter.SY, cylinderCenter.SZ + 20);
                        }

                        else if (angle < 3 * Math.PI / 4)
                        {
                            width = Doors[0].Length;
                            length = Doors[0].Width;
                            height = 30;

                            wingCenter = new MPoint((float)x, (float)y + width, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX, wingCenter.SY, wingCenter.SZ);

                            sx = 90;
                            sy = 0;
                            //legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY + 20, cylinderCenter.SZ + 20);
                        }
                        else if (angle < 5 * Math.PI / 4)
                        {
                            width = Doors[0].Width;
                            length = Doors[0].Length;
                            height = 30;

                            wingCenter = new MPoint((float)x - length, (float)y, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX, wingCenter.SY, wingCenter.SZ);

                            sy = 90;
                            sx = 0;
                            //legCenter = new MPoint(cylinderCenter.SX - 20, cylinderCenter.SY, cylinderCenter.SZ + 20);
                        }
                        else
                        {
                            width = Doors[0].Length;
                            length = Doors[0].Width;
                            height = 30;

                            wingCenter = new MPoint((float)x, (float)y - width, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX, wingCenter.SY, wingCenter.SZ);

                            sx = 90;
                            sy = 0;
                            //legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY - 20, cylinderCenter.SZ + 20);
                        }

                        Doors[i] = new Door(wingCenter, cylinderCenter, length, width, height, sx, sy);
                    }
                }
                else
                    throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForDoors);
            }
        }

        public void ChangeBodyRadius(int radius)
        {
            var acceptableEngineRadius = GetAcceptableRadius(Engines.Length, radius);
            var acceptableWingsRadius = GetAcceptableRadius(Wings.Length, radius);

            if (radius < 15)
                throw new ShuttleException(ShuttleExceptions.BodyRadiusTooSmall);
            else if (acceptableEngineRadius < Engines[0].EngineRadius)
                throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForEngines);
            else if (radius < BodyHead.GetRadius())
                throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForTop);
            else if (radius < defaultWingsRadius)
                throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForWings);

            //WINGS PART
            Body = new MSideCylinder(new MPoint(0, 0, 0), radius, Body.Height, true);
            RecalculateEngines();
            RecalculateWings();
            RecalculateTop();
            RecalculateColor();
        }

        public void ChangeBodyHeight(int height)
        {
            if (height < 60)
                throw new ShuttleException(ShuttleExceptions.BodyHeightTooSmall);

            Body = new MSideCylinder(new MPoint(0, 0, 0), Body.GetRadius(), height, true);

            RecalculateWings();
            RecalculateTop();
            RecalculateColor();
        }

        public void ChangeTopRadius(int radius)
        {
            if (radius < 15)
                throw new ShuttleException(ShuttleExceptions.TopRadiusTooSmall);
            else if(radius > Body.GetRadius())
                throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForTop);

            BodyHead = new MTopCylinder(new MPoint(0, 0, Body.Height), radius, radius - 10, 20, true);

            Top = new MSideCylinder(new MPoint(0, 0, Body.Height + BodyHead.Height), radius - 10, 10, true);
            RecalculateColor();
        }

        public void ChangeWingsLength(int length)
        {
            if (length < 5)
                throw new ShuttleException(ShuttleExceptions.WingLengthTooSmall);

            RecalculateWings(length);
            RecalculateColor();
        }

        public void ChangeEngineRadius(int radius)
        {
            if (radius < 15)
                throw new ShuttleException(ShuttleExceptions.EngineRadiusTooSmall);

            if (Engines.Length < 1 || (GetAcceptableRadius(Engines.Length, Body.GetRadius()) < radius))
                throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForEngines);

            foreach (Engine engine in Engines)
                engine.EngineRadius = radius;

            RecalculateEngines();
            RecalculateColor();
        }

        public void ChangeEngineCount(int count)
        {
            if (Engines.Length < 1 || (GetAcceptableRadius(count, Body.GetRadius()) < Engines[0].EngineRadius))
                throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForEngines);
            //if (Engines.Length > 0 && GetAcceptableRadius(count, Body.GetRadius()) < Engines[0].EngineRadius)
            //throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForEngines);

            Engine[] engines = null;

            if (count > Engines.Length)
            {
                engines = new Engine[count];

                Array.Copy(Engines, engines, Engines.Length);
            }
            else if (count < Engines.Length)
            {
                engines = new Engine[count];

                Array.Copy(Engines, engines, count);
            }
            else
                engines = Engines;

            Engines = engines;

            RecalculateEngines();
            RecalculateColor();
        }

        public void ChangeWingsCount(int count)
        {
            if (Wings.Length < 1 || (GetAcceptableRadius(count, Body.GetRadius()) < defaultWingsRadius))
                throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForEngines);
            //if (Engines.Length > 0 && GetAcceptableRadius(count, Body.GetRadius()) < Engines[0].EngineRadius)
            //throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForEngines);

            Wing[] wings = null;

            if (count > Wings.Length)
            {
                wings = new Wing[count];

                Array.Copy(Wings, wings, Wings.Length);
            }
            else if (count < Wings.Length)
            {
                wings = new Wing[count];

                Array.Copy(Wings, wings, count);
            }
            else
                wings = Wings;

            Wings = wings;

            RecalculateWings();
            RecalculateColor();
        }

        public void ChangeWingLegLength(int length)
        {
            foreach (Wing wing in Wings)
                wing.ChangeLegLength(length);

            RecalculateColor();
        }

        public void ChangeWingLegRadius(int radius)
        {
            foreach (Wing wing in Wings)
                wing.ChangeLegRadius(radius);

            RecalculateColor();
        }

        public void ChangeWingLegAngle(int angle)
        {
            foreach (Wing wing in Wings)
                wing.ChangeLegAngle(angle);

            RecalculateColor();
        }

        private float GetAcceptableRadius(int count, float outerRadius)
        {
            var acceptableRadius = outerRadius;

            if (count != 1)
                acceptableRadius = (float)(outerRadius * Math.Sin(Math.PI / count) / (1 + Math.Sin(Math.PI / count)));

            return acceptableRadius;
        }

        private void RecalculateTop()
        {
            BodyHead = new MTopCylinder(new MPoint(0, 0, Body.Height), BodyHead.GetRadius(), BodyHead.GetRadius() - 10, 20, true);

            Top = new MSideCylinder(new MPoint(0, 0, Body.Height + BodyHead.Height), Top.GetRadius(), 10, true);

            RecalculateColor();
        }

        private void RecalculateEngines()
        {
            if (Engines.Length > 0) {
                float bodyRadius = Body.GetRadius(), engineRadius = Engines[0].EngineRadius;

                var neededEngineRadius = GetAcceptableRadius(Engines.Length, bodyRadius);

                if (Engines[0].EngineRadius <= neededEngineRadius)
                {
                    if (Engines.Length < 2)
                    {
                        Engines[0] = new Engine(0, 0, -20, engineRadius)
                        {
                            ModelMatrix = this.ModelMatrix
                        };
                    }
                    else
                    {
                        var da = Engines.Length > 1 ? 2 * Math.PI / Engines.Length : 1;
                        double x, y, angle = 0.0;


                        for (int i = 0; i < Engines.Length; ++i, angle += da)
                        {
                            x = (bodyRadius - engineRadius) * Math.Cos(angle);
                            y = (bodyRadius - engineRadius) * Math.Sin(angle);

                            Engines[i] = new Engine((int)x, (int)y, -20, engineRadius)
                            {
                                ModelMatrix = this.ModelMatrix
                            };
                        }
                    }
                }
                else
                    throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForEngines);
            }
        }

        private void RecalculateWings(int newLength)
        {
            if (Wings.Length > 0)
            {
                float bodyRadius = Body.GetRadius();

                var neededWingRadius = GetAcceptableRadius(Wings.Length, bodyRadius);

                if (defaultWingsRadius <= neededWingRadius)
                {
                    var da = 2 * Math.PI / Wings.Length;
                    double x, y, angle = 0.7;

                    float width, length, height, sx = 0, sy = 0;
                    MPoint wingCenter, cylinderCenter, legCenter;

                    for (int i = 0; i < Wings.Length; ++i, angle += da)
                    {
                        x = bodyRadius * Math.Cos(angle);
                        y = bodyRadius * Math.Sin(angle);

                        if (angle < Math.PI / 4 || angle >= 7 * Math.PI / 4)
                        {
                            width = 5;
                            length = newLength;
                            height = 60;

                            wingCenter = new MPoint((float)x + newLength / 2 - 2, (float)y, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX + newLength / 2, wingCenter.SY, wingCenter.SZ - height / 2);

                            sy = 135;
                            sx = 0;
                            //legCenter = new MPoint(cylinderCenter.SX + 20, cylinderCenter.SY, cylinderCenter.SZ + 20);
                        }

                        else if (angle < 3 * Math.PI / 4)
                        {
                            width = newLength;
                            length = 5;
                            height = 60;

                            wingCenter = new MPoint((float)x, (float)y + newLength / 2 - 2, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX, wingCenter.SY + newLength / 2, wingCenter.SZ - height / 2);

                            sx = 45;
                            sy = 0;
                            //legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY + 20, cylinderCenter.SZ + 20);
                        }
                        else if (angle < 5 * Math.PI / 4)
                        {
                            width = 5;
                            length = newLength;
                            height = 60;

                            wingCenter = new MPoint((float)x - newLength / 2 + 2, (float)y, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX - newLength / 2, wingCenter.SY, wingCenter.SZ - height / 2);

                            sy = 45;
                            sx = 0;
                            //legCenter = new MPoint(cylinderCenter.SX - 20, cylinderCenter.SY, cylinderCenter.SZ + 20);
                        }
                        else
                        {
                            width = newLength;
                            length = 5;
                            height = 60;

                            wingCenter = new MPoint((float)x, (float)y - newLength / 2 + 2, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX, wingCenter.SY - newLength / 2, wingCenter.SZ - height / 2);

                            sx = 135;
                            sy = 0;
                            //legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY - 20, cylinderCenter.SZ + 20);
                        }

                        Wings[i] = new Wing(wingCenter, cylinderCenter, length, width, height, sx, sy);
                    }
                }
                else
                    throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForWings);
            }
        }

        private void RecalculateWings()
        {
            if (Wings.Length > 0)
            {
                float bodyRadius = Body.GetRadius();

                var neededWingRadius = GetAcceptableRadius(Wings.Length, bodyRadius);

                if (defaultWingsRadius <= neededWingRadius)
                {
                    var da = 2 * Math.PI / Wings.Length;
                    double x, y, angle = 0.7;

                    float width, length, height, sx = 0, sy = 0;
                    MPoint wingCenter, cylinderCenter, legCenter;

                    for (int i = 0; i < Wings.Length; ++i, angle += da)
                    {
                        x = bodyRadius * Math.Cos(angle);
                        y = bodyRadius * Math.Sin(angle);

                        if (angle < Math.PI / 4 || angle >= 7 * Math.PI / 4)
                        {
                            width = 5;
                            length = Wings[0].Length;
                            height = 60;

                            wingCenter = new MPoint((float)x + Wings[0].Length / 2 - 2, (float)y, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX + Wings[0].Length / 2, wingCenter.SY, wingCenter.SZ - height / 2);

                            sy = 135;
                            sx = 0;
                            //legCenter = new MPoint(cylinderCenter.SX + 20, cylinderCenter.SY, cylinderCenter.SZ + 20);
                        }

                        else if (angle < 3 * Math.PI / 4)
                        {
                            width = Wings[0].Length;
                            length = 5;
                            height = 60;

                            wingCenter = new MPoint((float)x, (float)y + Wings[0].Length / 2 - 2, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX, wingCenter.SY + Wings[0].Length / 2, wingCenter.SZ - height / 2);

                            sx = 45;
                            sy = 0;
                            //legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY + 20, cylinderCenter.SZ + 20);
                        }
                        else if (angle < 5 * Math.PI / 4)
                        {
                            width = 5;
                            length = Wings[0].Length;
                            height = 60;

                            wingCenter = new MPoint((float)x - Wings[0].Length / 2 + 2, (float)y, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX - Wings[0].Length / 2, wingCenter.SY, wingCenter.SZ - height / 2);

                            sy = 45;
                            sx = 0;
                            //legCenter = new MPoint(cylinderCenter.SX - 20, cylinderCenter.SY , cylinderCenter.SZ + 20);
                        }
                        else
                        {
                            width = Wings[0].Length;
                            length = 5;
                            height = 60;

                            wingCenter = new MPoint((float)x, (float)y - Wings[0].Length / 2 + 2, Body.Height / 2);
                            cylinderCenter = new MPoint(wingCenter.SX, wingCenter.SY - Wings[0].Length / 2, wingCenter.SZ - height / 2);

                            sx = 135;
                            sy = 0;
                            //legCenter = new MPoint(cylinderCenter.SX, cylinderCenter.SY - 20, cylinderCenter.SZ + 20);
                        }

                        Wings[i] = new Wing(wingCenter, cylinderCenter, length, width, height, sx, sy);
                    }
                }
                else
                    throw new ShuttleException(ShuttleExceptions.DoesntHaveFreeSpaceForWings);
            }
        }

        public override List<MPoint> GetAllPoints()
        {
            var points = new List<MPoint>();

            points.AddRange(Body.GetAllPoints());
            points.AddRange(Top.GetAllPoints());
            //points.AddRange(EngineToBody.GetAllPoints());
            //points.AddRange(EngineExhaust.GetAllPoints());
            foreach (Engine engine in Engines)
                points.AddRange(engine.GetAllPoints());

            foreach (Wing wing in Wings)
                points.AddRange(wing.GetAllPoints());

            foreach (Door door in Doors)
                points.AddRange(door.GetAllPoints());

            points.AddRange(BodyHead.GetAllPoints());
            //points.AddRange(EngineMount.GetAllPoints());
            //points.AddRange(ExhaustMount.GetAllPoints());

            return points;
        }

        public override List<MPoint> GetVertices()
        {
            var vertices = new List<MPoint>();

            vertices.AddRange(Body.GetVertices());
            vertices.AddRange(Top.GetVertices());
            //vertices.AddRange(EngineToBody.GetVertices());
            //vertices.AddRange(EngineExhaust.GetVertices());
            foreach (Engine engine in Engines)
                vertices.AddRange(engine.GetVertices());

            foreach (Wing wing in Wings)
                vertices.AddRange(wing.GetVertices());

            foreach (Door door in Doors)
                vertices.AddRange(door.GetVertices());

            vertices.AddRange(BodyHead.GetVertices());
            //vertices.AddRange(EngineMount.GetVertices());
            //vertices.AddRange(ExhaustMount.GetVertices());


            return vertices;
        }

        //public List<MCommonPrimitive> GetAllCommonPrimitives()
        //{
        //    var primitives = new List<MCommonPrimitive>();

        //    Body.ModelMatrix = this.ModelMatrix;
        //    Top.ModelMatrix = this.ModelMatrix;

        //    primitives.Add(Body);
        //    primitives.Add(Top);
        //    //vertices.AddRange(EngineToBody.GetVertices());
        //    //vertices.AddRange(EngineExhaust.GetVertices());
        //    foreach (Engine engine in Engines)
        //    {
        //        engine.ModelMatrix = this.ModelMatrix;
        //        primitives.Add(engine);
        //    }

        //    foreach (Wing wing in Wings)
        //    {
        //        primitives.AddRange(wing.GetAllCommonPrimitives());
        //    }

        //    primitives.Add(BodyHead);

        //    return primitives;
        //}

        public override List<MFacet> GetAllFacets()
        {
            var facets = new List<MFacet>();

            facets.AddRange(Body.GetAllFacets());
            facets.AddRange(Top.GetAllFacets());
            //facets.AddRange(EngineToBody.GetAllFacets());
            //facets.AddRange(EngineExhaust.GetAllFacets());
            foreach (Engine engine in Engines)
                facets.AddRange(engine.GetAllFacets());

            foreach (Wing wing in Wings)
                facets.AddRange(wing.GetAllFacets());

            foreach (Door door in Doors)
                facets.AddRange(door.GetAllFacets());

            facets.AddRange(BodyHead.GetAllFacets());
            //facets.AddRange(EngineMount.GetAllFacets());
            //facets.AddRange(ExhaustMount.GetAllFacets());

            return facets;
        }
    }
}
