using System;
using System.Runtime.Serialization;

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

        public BestellingRepoException(string message, Exception inner) : base(message, inner)
        {
        }

        
    }
}   