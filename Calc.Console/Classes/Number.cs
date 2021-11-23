using System;
using System.Collections.Generic;
using System.Text;

namespace Calc.Console.Classes
{
    class Number : Step
    {
        private readonly double? result;
        public Number(string line) : base(line)
        {
            if(double.TryParse(line, out double res))
            {
                result = res;
            }
            else
            {
                ParseString();
            }
        }

       
        public override IStep NextStep { get; }

        public override double Result => result ?? NextStep.Result;
    }

    abstract class Operation : Step
    {
        protected Operation(string line, char op) : base(line)
        {
           
        }

        public static IStep ParseOperation(string line)
        {
            foreach (char c in line)
            {
                switch(c)
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
