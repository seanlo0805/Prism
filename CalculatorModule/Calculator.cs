using CalculatorInterfaces;
using NonUICommonDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorModule
{
    public class Calculator : ICalculator
    {
        public int Execute(CommandTypes commandTypes, Arguments args)
        {
            switch (commandTypes)
            {
                case CommandTypes.Add:
                    return Add(args);
                case CommandTypes.Div:
                    return Div(args);
                case CommandTypes.Mul:
                    return Mul(args);
                case CommandTypes.Sub:
                    return Sub(args);
                default:
                    throw new InvalidOperationException();
            }
        }

        public int Add(Arguments args)
        {
            return args.X + args.Y;
        }
        public int Sub(Arguments args)
        {
            return args.X - args.Y;
        }
        public int Mul(Arguments args)
        {
            return args.X * args.Y;
        }
        public int Div(Arguments args)
        {
            return args.X / args.Y;
        }
    }
}
