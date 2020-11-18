using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Calculator_Host
{
    class CalculatorHost
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WCFCalculator_Service.WCFCalculatorImpl));
            host.Open();
            Console.ReadLine();

        }
    }
}
