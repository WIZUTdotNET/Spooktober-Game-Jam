using System;
using System.Numerics;

namespace Events
{
    public class OnClickEventArgs : EventArgs
    {
        public BigInteger Souls { get; private set; }

        public OnClickEventArgs(BigInteger souls)
        {
            Souls = souls;
        }
    }
}