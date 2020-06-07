using System;
using System.Collections.Generic;
using System.Text;

namespace GameException
{
    public class ParameterException : Exception
    {
        public ParameterException()
        {
        }
        public ParameterException(string message)
            : base(message)
        {
        }

        public ParameterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
