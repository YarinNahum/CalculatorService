using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.CalculatorReference;
using WCFCalculator_Service;
using System.ServiceModel;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            InstanceContext context = new InstanceContext(new CallbackHandler());
            WCFCalculatorClient c = new WCFCalculatorClient(context);
            c.initializeConnection();
            c.processRequest("1");
            c.processRequest("22");
            c.processRequest("333");
            c.processRequest("666666");
            Stack<double> s = c.processRequest("4444");
            foreach (double x in s)
            {
                Console.WriteLine(x);
            }
            Console.ReadLine();
        }
 
    }

    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CallbackHandler : Client.CalculatorReference.IWCFCalculatorCallback
    {
        public void Error(string error)
        {
            Console.WriteLine(error);
        }
    }

}
