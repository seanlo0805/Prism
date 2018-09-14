using NonUICommonDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInterfaces
{
    public interface IInputParserService
    {
        CommandTypes ParseCommand(string command);
        int ParseArgument(string argument);
    }
}
