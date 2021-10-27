using System;
using System.Runtime.Serialization;

namespace Exceptions.Upgrades
{
    public class UpgradeAlreadyBoughtException : Exception
    {
        public UpgradeAlreadyBoughtException()
        {
        }

        public UpgradeAlreadyBoughtException(string message) : base(message)
        {
        }

        public UpgradeAlreadyBoughtException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UpgradeAlreadyBoughtException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}