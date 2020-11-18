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
    class CalcualatorClient
    {
        static void Main(string[] args)
        {
            InstanceContext context = new InstanceContext(new CallbackHandler());
            WCFCalculatorClient c = new WCFCalculatorClient(context);
            c.initializeConnection();

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
