using NonUICommonDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInferfaces
{
    public interface ICalculator
    {
        int Execute(CommandTypes commandTypes, Arguments args);
        int Add(Arguments args);
        int Sub(Arguments args);
        int Mul(Arguments args);
        int Div(Arguments args);
    }
}
