using System;
using System.Collections.Generic;
using System.Text;

namespace Calc.Console.Classes
{
    interface IStep
    {
        IStep NextStep { get; }

        double Result { get; }
    }

    abstract class Step : IStep
    {
        protected string line { get; }

        public Step(string line)
        {
            this.line = line;
        }
        public abstract IStep NextStep { get; }

        public abstract double Result { get; }
    }

}
