using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Shapes.ComplexShapes
{
    public class Shuttle : MComplex
    {
        public MSideCylinder Body { get; set; }        
        public MSideCylinder UpperBody { get; set; }
        public MSideCylinder LowerBody { get; set; }        
        public MSideCylinder EngineLower { get; set; }
        public MTopCylinder UpperBodyFastener { get; set; }
        public MTopCylinder LowerBodyToEngineFastener { get; set; }
        public MTopCylinder EngineToBodyFastener { get; set; }


        public float BodyRadius { get; set; } = 25;
        public float BodyHeight { get; set; } = 80;

        public float UpperBodyFastenerBotRadius { get; set; } = 25;
        public float UpperBodyFastenerTopRadius { get; set; } = 20;
        public float UpperBodyFastenerHeight { get; set; } = 20;

        public float UpperBodyRadius { get; set; } = 20;
        public float UpperBodyHeight { get; set; } = 15;

        public float LowerBodyRadius { get; set; } = 20;
        public float LowerBodyHeight { get; set; } = 30;

        public float LowerBodyToEngineFastenerBotRadius { get; set; } = 15;
        public float LowerBodyToEngineFastenerTopRadius { get; set; } = 20;
        public float LowerBodyToEngineFastenerHeight { get; set; } = 15;

        public float EngineToBodyFastenerBotRadius { get; set; } = 15;
        public float EngineToBodyFastenerTopRadius { get; set; } = 10;
        public float EngineToBodyFastenerHeight { get; set; } = 25;

        public float EngineLowerRadius { get; set; } = 15;
        public float EngineLowerHeight { get; set; } = 30;

        readonly MPoint bodyBotCenter = new MPoint(0, 0, -40);
        readonly MPoint upperBodyFastenerBotCenter = new MPoint(0, 0, 80);
        readonly MPoint upperBodyBotCenter = new MPoint(0, 0, 100);
        readonly MPoint lowerBodyBotCenter = new MPoint(0, 0, -30);
        readonly MPoint lowerBodyToEngineFastenerBotCenter = new MPoint(0, 0, -45);
        readonly MPoint engineToBodyFastenerBotCenter = new MPoint(0, 0, -70);
        readonly MPoint engineLowerBotCenter = new MPoint(0, 0, -100);

        public Shuttle()
        {
            //InitPrimitives();
            //FillPrimitivesList();
        }

        protected override void InitPrimitives()
        {
            Body = new MSideCylinder(bodyBotCenter, BodyRadius, BodyHeight);

            // Upper Body
            UpperBodyFastener = new MTopCylinder(
                upperBodyFastenerBotCenter, UpperBodyFastenerBotRadius, UpperBodyFastenerTopRadius, UpperBodyFastenerHeight
                );

            UpperBody = new MSideCylinder(upperBodyBotCenter, UpperBodyRadius, UpperBodyHeight);

            // Lower Body
            LowerBody = new MSideCylinder(lowerBodyBotCenter, LowerBodyRadius, LowerBodyHeight);
            LowerBodyToEngineFastener = new MTopCylinder(
                lowerBodyToEngineFastenerBotCenter,
                LowerBodyToEngineFastenerBotRadius, LowerBodyToEngineFastenerTopRadius, LowerBodyToEngineFastenerHeight
                );

            // Engine
            EngineToBodyFastener = new MTopCylinder(
                engineToBodyFastenerBotCenter,
                EngineToBodyFastenerBotRadius, EngineToBodyFastenerTopRadius, EngineToBodyFastenerHeight
                );
            EngineLower = new MSideCylinder(engineLowerBotCenter, EngineLowerRadius, EngineLowerHeight);
        }

        protected override void FillPrimitivesList()
        {
            this.AddPrimitive(Body);
            this.AddPrimitive(UpperBodyFastener);
            this.AddPrimitive(UpperBody);
            this.AddPrimitive(LowerBody);
            this.AddPrimitive(LowerBodyToEngineFastener);
            this.AddPrimitive(EngineToBodyFastener);
            this.AddPrimitive(EngineLower);
        }
    }
}