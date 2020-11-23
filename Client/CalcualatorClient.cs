﻿using System;
using Client.CalculatorService;
using System.ServiceModel;

namespace Client
{
    class CalcualatorClient
    {
        static void Main()
        {
            InstanceContext context = new InstanceContext(new CallbackHandler());
            WCFCalculatorClient calculator = new WCFCalculatorClient(context);
            calculator.InitializeConnection();
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
                    if (CheckInput(requests))
                        calculator.ProcessRequest(requests);
                }

            }
        }

        public static bool CheckInput(string[] requests)
        {
            foreach (string str in requests)
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
                        try
                        {
                            Double.Parse(str);
                            break;
                        }
                        catch (FormatException)
                        {
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
        public void PrintMessage(string error)
        {
            Console.WriteLine(error);
        }
    }

}
