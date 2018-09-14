using CalculatorInterfaces;
using NonUICommonDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputOutputModule
{
    public class ConsoleInputService : IInputService
    {
        public String ReadCommand()
        {
            return Console.ReadLine();
        }

        public Arguments ReadArguments()
        {
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            return new Arguments() { X = x, Y = y };
        }
    }
}
