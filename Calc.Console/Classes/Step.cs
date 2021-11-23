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

    /// <summary>
    /// Шаг выполнения вычислений
    /// </summary>
    abstract class Step : IStep
    {
        protected string line { get; }

        public Step(string line)
        {
            this.line = line;
        }
        public abstract IStep NextStep { get; }

        public abstract double Result { get; }

        protected string ParseString()
        {
            foreach (char c in line)
            {
                int openBreaker = line.IndexOfAny("({[".ToCharArray());
                if (openBreaker != -1) // Если есть открывающая скобка
                {
                    int closeBreaker = line.IndexOfAny("]})".ToCharArray());
                    if (closeBreaker != -1) // Если есть закрывающая скобка
                    {
                        int secondBreaker = line.IndexOfAny("({[".ToCharArray(), openBreaker + 1);
                        if (secondBreaker != -1) // Если между ними есть вложенная открывающая скобка
                        {

                        }
                    }
                    else throw new InvalidOperationException($"Нет закрывающей скобки в строке '{line}'");
                }
            }
            return line; // В строке нет скобок - вернуть как есть
        }
    }

}
