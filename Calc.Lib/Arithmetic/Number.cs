using System;
using System.Collections.Generic;
using System.Text;

namespace Calc.Lib.Arithmetic
{
    class Number : Step
    {
        private readonly double? result;
        public Number(string line) : base(line)
        {
            if (double.TryParse(line, out double res))
            {
                result = res;
            }
            else
            {
                NextStep = Brackets.ParseBrackets(line);
            }
        }

       
        public override IStep NextStep { get; }

        public override double Result => result ?? NextStep.Result;
    } 
}
