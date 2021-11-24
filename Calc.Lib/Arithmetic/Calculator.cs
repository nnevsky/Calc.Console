using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Calc.Lib.Arithmetic
{
    public interface IStep
    {
        double Result { get; }
        IStep NextStep { get; }
    }
    public abstract class Step : IStep
    {
        protected string line;

        protected Step(string line)
        {
            this.line = line;
            //Parse();
        }

        protected abstract void Parse();
        public abstract double Result { get; }

        public IStep NextStep { get; protected set; }
    }
    public class Number : Step
    {
        private double? result;
        public Number(string line) : base(line) { Parse(); }

        public Number(double result) : base(result.ToString())
        {
            this.result = result;
        }
        public override double Result => result ?? NextStep.Result;

        protected override void Parse()
        {
            if (double.TryParse(line, out double res))
            {
                result = res;
            }
            else
            {
                NextStep = Operation.ParseOperation(line);
            }
        }
    }

    public abstract class Operation : Step
    {
        protected double left { get; set; }
        protected double right { get; set; }

        public Operation(string line) : base(line) { Parse(); }

        protected Operation(string line, char op) : base(line) 
        {
            string sl = line.Substring(0, line.IndexOf(op)); // Слева от знака операции
            string sr = line.Substring(line.IndexOf(op)+1, line.Length - line.IndexOf(op) -1);  // Справа от знака операции

            left = new Number(sl).Result;
            right = new Number(sr).Result;
            //right = new Number(sr.Substring(0, IndexCloseBracket(sr))).Result;
        }
       
        public static IStep ParseOperation(string line)
        {
            if (line.IndexOfAny("({[]})".ToCharArray()) != -1)
            {
                return new Bracket(line);
            }
            foreach (char c in line)
            {
                switch (c)
                {
                    case '+': return new Plus(line);
                    case '-': return new Minus(line);
                    case '*': return new Multiple(line);
                    case '/': return new Divide(line);
                    //case '(': return new Bracket(line);
                    default: continue;
                }
            }
            return null;
        }

        protected override void Parse()
        {
            NextStep = ParseOperation(line);
        }
    }
    class Bracket : Step
    {
        public Bracket(string line) : base(line) { Parse(); }

        protected override void Parse()
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
                        NextStep = new Number(line.Substring(indexInner, indexClose - indexInner));
                    }
                    else
                    {
                        NextStep = new Number(line.Substring(indexOpen + 1, line.Length - indexClose)); // Вычислить действие в скобках

                        StringBuilder sb = new StringBuilder();
                        sb.Append(line.Substring(0, indexOpen - 1));
                        sb.Append(NextStep.Result.ToString());
                        sb.Append(line.Substring(indexClose + 1, line.Length - indexClose - 1));
                        line = sb.ToString();
                    }
                }
                else throw new InvalidOperationException($"Нет закрывающей скобки в строке '{line}'");
            }
        }

        protected int IndexOpenBracket(string line) => line.IndexOfAny("({[".ToCharArray());

        protected int IndexCloseBracket(string line) => line.IndexOfAny("]})".ToCharArray());

        public override double Result => NextStep.Result;
    }
    class Plus : Operation
    {
        public Plus(string line) : base(line, '+') { }

        public override double Result => left + right;
    }
    class Minus : Operation
    {
        public Minus(string line) : base(line, '-') { }

        public override double Result => left - right;
    }
    class Multiple : Operation
    {
        public Multiple(string line) : base(line, '*') { }

        public override double Result => left * right;
    }
    class Divide : Operation
    {
        public Divide(string line) : base(line, '/') { }

        public override double Result => left / right;
    }
}
