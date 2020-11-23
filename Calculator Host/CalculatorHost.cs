using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Calculator_Host
{
    class CalculatorHost
    {
        static void Main()
        {
            ServiceHost host = new ServiceHost(typeof(WCFCalculator_Service.WCFCalculatorImpl));
            host.Open();
            Console.ReadLine();

        }
    }
}
