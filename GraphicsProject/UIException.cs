using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProject
{
    public enum UIExceptions
    {
        TransformationInputEmpty
    }

    class UIException : Exception
    {
        public UIExceptions Status { get; set; }

        public UIException(UIExceptions uiException) : base()
        {
            Status = uiException;
        }

        public UIException(UIExceptions uiException, string message) : base(message)
        {
            Status = uiException;
        }
    }
}
