using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.CalculatorService ;
using WCFCalculator_Service;
using System.ServiceModel;

namespace Client
{
    class CalcualatorClient
    {
        static void Main(string[] args)
        {
            InstanceContext context = new InstanceContext(new CallbackHandler());
            WCFCalculatorClient calculator = new WCFCalculatorClient(context);
            calculator.initializeConnection();
            while (true)
            {
                Console.WriteLine("Waiting for next request");
                string request = Console.ReadLine();
                if(request == null)
                {
                    Console.WriteLine("no input was given");
                    continue;
                }
                else
                {
                    string[] requests = request.Split(' ');
                    if (checkInput(requests))
                        calculator.processRequest(requests);
                }

            }
        }

        public static bool checkInput(string[] requests)
        {
            foreach (string str in requests)
            {
                try
                {
                    Double.Parse(str);
                }catch (FormatException)
                {
                    switch (str)
                    {
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                        case "_":
                            continue;
                        case "End":
                        case "quit":
                        case "q":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid argument {0}", str);
                            return false;
                    }
                }
            }
            return true;
        }
 
    }

    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CallbackHandler : Client.CalculatorService.IWCFCalculatorCallback
    {
        public void printMessage(string error)
        {
            Console.WriteLine(error);
        }
    }

}
