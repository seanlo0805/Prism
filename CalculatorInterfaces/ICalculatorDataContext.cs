using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInterfaces
{
    public interface ICalculatorDataContext
    {
        ICalculator CalculatorHandler { get; set;}
        IInputParserService InputParserService { get; set; }
    }
}
