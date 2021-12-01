using System;
using System.Drawing;

namespace DataAccess.Exceptions
{
    public class KlantRepoException : Exception
    {
        public KlantRepoException()
        {

        }

        public KlantRepoException(string message) : base(message)
        {

        }

        public KlantRepoException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}