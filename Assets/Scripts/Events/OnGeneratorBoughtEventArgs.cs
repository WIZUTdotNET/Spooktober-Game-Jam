using Models;
using System;

namespace Events
{
    public class OnGeneratorBoughtEventArgs : EventArgs
    {
        public Generator Generator { get; private set; }

        public OnGeneratorBoughtEventArgs(Generator generator)
        {
            Generator = generator;
        }
    }
}