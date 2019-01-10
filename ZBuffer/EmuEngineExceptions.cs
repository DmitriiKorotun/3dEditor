using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine
{
    public class EmuEngineExceptions
    {
        public enum ShuttleExceptions
        {
            DoesntHaveFreeSpaceForEngines,
            DoesntHaveFreeSpaceForWings,
            DoesntHaveFreeSpaceForTop,
            DoesntHaveFreeSpaceForDoors,
            EngineRadiusTooSmall,
            BodyRadiusTooSmall,
            BodyHeightTooSmall,
            TopRadiusTooSmall,
            WingLengthTooSmall,
            WingLegLengthTooSmall,
            WingLegRadiusTooSmall,
            WingLegAngleTooSmall,
            WingLegAngleTooBig
        }

        public class ShuttleException : Exception
        {
            public ShuttleExceptions Status { get; set; }

            public ShuttleException(ShuttleExceptions uiException) : base()
            {
                Status = uiException;
            }

            public ShuttleException(ShuttleExceptions uiException, string message) : base(message)
            {
                Status = uiException;
            }

        }
    }
}
