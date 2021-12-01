using System;
using System.Drawing;

namespace DataAccess.Exceptions
{
    public class VoetbaltruitjeRepoException : Exception
    {
        public VoetbaltruitjeRepoException()
        {

        }

        public VoetbaltruitjeRepoException(string message) : base(message)
        {

        }

        public VoetbaltruitjeRepoException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}