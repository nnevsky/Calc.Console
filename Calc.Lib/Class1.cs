using System;
using System.Collections.Generic;
using System.Text;

namespace Calc.Lib1
{
   
    public interface IStep
    {
        IStep NextStep { get; }
        double Result { get; }
    }

    public abstract class Step : IStep
    {
        protected double? result;
        protected string line;

        public Step(string line)
        {
            this.line = line;
            Parse();
        }
        public Step(IStep nextStep) => this.NextStep = nextStep;

        public IStep NextStep { get; set; }

        public virtual double Result => result ?? NextStep.Result;

        protected abstract void Parse();
    }

    public class Number : Step
    {
        public Number(string line) : base(line)
        {
           
        }

        protected override void Parse()
        {
            if (double.TryParse(line, out double res))
            {
                result = res;
            }
            else
            {
                NextStep = Operation.ParseOperation(line);
                //NextStep = new Brackets(line);
            }
        }
    }
    public class Brackets : Step
    {
        public Brackets(string line) : base(line)
        {

        }
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
                        sb.Append(line.Substring(0, indexOpen-1));
                        sb.Append(NextStep.Result.ToString());
                        sb.Append(line.Substring(indexClose + 1, line.Length - indexClose - 1));
                        line = sb.ToString();
                    }
                }
                else throw new InvalidOperationException($"Нет закрывающей скобки в строке '{line}'");
            }

            NextStep = Operation.ParseOperation(line); // В строке нет скобок - вернуть как действие
        }

        protected int IndexOpenBracket(string line) => line.IndexOfAny("({[".ToCharArray());

        protected int IndexCloseBracket(string line) => line.IndexOfAny("]})".ToCharArray());
        
    }
    public abstract class Operation : Step
    {
        protected double left { get; set; }
        protected double right { get; set; }
        protected Operation(string line, char op) : base(line)
        {
            string sl = line.Substring(0, line.IndexOf(op)); // Слева от знака операции
            string sr = line.Substring(line.IndexOf(op), line.Length - line.IndexOf(op));  // Справа от знака операции

            left = new Number(sl).Result;
            right = new Number(sr.Substring(0, IndexCloseBracket(sr))).Result;
        }
        int IndexCloseBracket(string line) => line.IndexOfAny("]})".ToCharArray()); //**//
        public static IStep ParseOperation(string line)
        {
            foreach (char c in line)
            {
                switch (c)
                {
                    //case '(': return new Brackets(line);
                    case '+': return new Plus(line);
                    case '-': return new Minus(line);
                    case '*': return new Multiple(line);
                    case '/': return new Divide(line);
                    default: continue;
                }
            }
            return null;
        }

        //protected override void Parse()
        //{
        //    NextStep = ParseOperation(line);
        //}
    }
    class Plus : Operation
    {
        public Plus(string line) : base(line, '+')
        {

        }

        public override double Result => left + right;
    }
    class Minus : Operation
    {
        public Minus(string line) : base(line, '-')
        {

        }

        public override double Result => left - right;
    }
    class Multiple : Operation
    {
        public Multiple(string line) : base(line, '*')
        {

        }

        public override double Result => left * right;
    }
    class Divide : Operation
    {
        public Divide(string line) : base(line, '/')
        {

        }

        public override double Result => left / right;
    }
}
