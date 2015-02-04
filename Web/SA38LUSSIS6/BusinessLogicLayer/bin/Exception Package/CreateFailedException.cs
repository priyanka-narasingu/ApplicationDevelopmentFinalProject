using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Exception_Package
{
    public class CreateFailedException : Exception
    {
        public CreateFailedException()
        {
        }

        public CreateFailedException(string message)
            : base(message)
        {
        }

        public CreateFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    }


