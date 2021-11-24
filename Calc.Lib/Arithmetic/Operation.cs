//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Calc.Lib.Arithmetic
//{
//    abstract class Operation : Brackets
//    {
//        protected double left { get; set; }
//        protected double right { get; set; }
//        protected Operation(string line, char op) : base(line)
//        {
//            string sl = line.Substring(0, line.IndexOf(op));
//            string sr = line.Substring(line.IndexOf(op), line.Length - line.IndexOf(op));

//            left = new Number(sl).Result;
//            right = new Number(sr).Result;
//        }

//        public static IStep ParseOperation(string line)
//        {
//            foreach (char c in line)
//            {
//                switch (c)
//                {
//                    case '(': return new Brackets(line);
//                    case '+': return new Plus(line);
//                    case '-': return new Minus(line);
//                    case '*': return new Multiple(line);
//                    case '/': return new Divide(line);
//                    default: continue;
//                }
//            }
//            return null;
//        }
//        public override IStep NextStep { get; }
//    }

//    class Plus : Operation
//    {
//        public Plus(string line) : base(line, '+')
//        {

//        }

//        public override double Result => left + right;
//    }
//    class Minus : Operation
//    {
//        public Minus(string line) : base(line, '-')
//        {

//        }
       
//        public override double Result => left - right;
//    }
//    class Multiple : Operation
//    {
//        public Multiple(string line) : base(line, '*')
//        {

//        }
        
//        public override double Result => left * right;
//    }
//    class Divide : Operation
//    {
//        public Divide(string line) : base(line, '/')
//        {

//        }
        
//        public override double Result => left / right;
//    }

//}
