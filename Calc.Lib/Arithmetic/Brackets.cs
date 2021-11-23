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
            int indexOpen = IndexOpenBracket(line); // Открывающая скобка
            if (indexOpen != -1) 
            {
                int indexClose = IndexCloseBracket(line);   // Закрывающая скобка
                if (indexClose != -1) 
                {
                    int indexInner = IndexOpenBracket(line.Substring(1));   // Вложенная скобка
                    if (indexInner != -1 && indexInner < indexClose) 
                    {
                        return new Number(line.Substring(indexInner, indexClose - indexInner));
                    }
                    else
                    {
                        return new Number(line.Substring(indexOpen +1, indexClose - indexOpen)); // Вычислить действие в скобках
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
