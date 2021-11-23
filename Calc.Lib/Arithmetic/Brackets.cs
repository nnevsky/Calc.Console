using System;
using System.Collections.Generic;
using System.Text;

namespace Calc.Lib.Arithmetic
{
    /// <summary>
    /// Скобки
    /// </summary>
    class Brackets : Step
    {
        public Brackets(string line) : base(line)
        {

        }

        public static IStep ParseBrackets(string line)
        {
            int openBreaket = IndexOpenBracket(line);
            if (openBreaket != -1) // Если есть открывающая скобка
            {
                int closeBreaket = IndexCloseBracket(line);
                if (closeBreaket != -1) // Если есть закрывающая скобка
                {
                    int secondBreaket = IndexOpenBracket(line.Substring(1));
                    if (secondBreaket != -1 && secondBreaket < closeBreaket) // Если между ними есть вложенная открывающая скобка
                    {

                    }
                    else
                    {

                    }
                }
                else throw new InvalidOperationException($"Нет закрывающей скобки в строке '{line}'");
            }

            return Operation.ParseOperation(line); // В строке нет скобок - вернуть как действие
        }

        private static int IndexOpenBracket(string line)
        {
            return line.IndexOfAny("({[".ToCharArray());
        }

        private static int IndexCloseBracket(string line)
        {
            return line.IndexOfAny("]})".ToCharArray());
        }
        public override IStep NextStep { get; }

        public override double Result => NextStep.Result;
    }
}
