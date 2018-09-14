using CalculatorInterfaces;
using NonUICommonDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputOutputModule
{
    public class InputParserService : IInputParserService
    {
        public int ParseArgument(string argument)
        {
            return int.Parse(argument);
        }

        public CommandTypes ParseCommand(string command)
        {
            return (CommandTypes)Enum.Parse(typeof(CommandTypes), command);
        }
    }
}
