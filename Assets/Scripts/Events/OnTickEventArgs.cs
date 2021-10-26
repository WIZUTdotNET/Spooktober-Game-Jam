using System;
using System.Numerics;

namespace Events
{
    public class OnTickEventArgs : EventArgs
    {
        public BigInteger Souls { get; private set; }

        public OnTickEventArgs(BigInteger souls)
        {
            Souls = souls;
        }
    }
}