using System;
using System.Drawing;

namespace DataAccess.Exceptions
{
    public class BestellingRepoException : Exception
    {
        public BestellingRepoException()
        {
            
        }

        public BestellingRepoException(string message) : base(message)
        {
            
        }

        public BestellingRepoException(string message , Exception innerException): base(message, innerException)
        {

        }
    }
}