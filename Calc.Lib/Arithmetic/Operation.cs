using System;
using System.Collections.Generic;
using System.Text;

namespace Calc.Lib.Arithmetic
{
    abstract class Operation : Step
    {
        protected double left { get; set; }
        protected double right { get; set; }
        protected Operation(string line, char op) : base(line)
        {
            string sl = line.Substring(0, line.IndexOf(op));
            string sr = line.Substring(line.IndexOf(op), line.Length - 1);

            left = new Number(sl).Result;
            right = new Number(sr).Result;
        }

        public static Operation ParseOperation(string line)
        {
            foreach (char c in line)
            {
                switch (c)
                {
                    case '+': return new Plus(line);
                    case '-': return new Minus(line);
                    case '*': return new Multiple(line);
                    case '/': return new Divide(line);
                    default: return null;
                }
            }
            return null;
        }
    }

    class Plus : Operation
    {
        public Plus(string line) : base(line, '+')
        {

        }
        public override IStep NextStep => throw new NotImplementedException();

        public override double Result => throw new NotImplementedException();
    }
    class Minus : Operation
    {
        public Minus(string line) : base(line, '-')
        {

        }
        public override IStep NextStep => throw new NotImplementedException();

        public override double Result => throw new NotImplementedException();
    }
    class Multiple : Operation
    {
        public Multiple(string line) : base(line, '*')
        {

        }
        public override IStep NextStep => throw new NotImplementedException();

        public override double Result => throw new NotImplementedException();
    }
    class Divide : Operation
    {
        public Divide(string line) : base(line, '/')
        {

        }
        public override IStep NextStep => throw new NotImplementedException();

        public override double Result => throw new NotImplementedException();
    }

}
