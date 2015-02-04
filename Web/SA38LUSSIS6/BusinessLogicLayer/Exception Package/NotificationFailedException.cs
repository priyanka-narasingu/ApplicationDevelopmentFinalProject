using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Exception_Package
{
    class NotificationFailedException: Exception
    {
        public NotificationFailedException()
        {
        }

        public NotificationFailedException(string message)
            : base(message)
        {
        }

        public NotificationFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
