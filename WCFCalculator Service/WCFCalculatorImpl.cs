using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Data_Layer;
using Calculator_Library;
using ServiceContracts;
using System.Threading.Tasks;


namespace WCFCalculator_Service
{

    /// <summary>
    /// The main class of the calculator service.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class WCFCalculatorImpl : IWCFCalculator
    {
        private IDataHolder<double> data = null;
        private ICalculatorFunctions calc = null;
        private bool isError = false;

        /// <summary>
        /// Create new data holder and calculator object per session
        /// </summary>
        public void InitializeConnection()
        {
            data = new DataHolderImpl();
            calc = new CalculatorFunctionsImpl();
            Console.WriteLine("A new connection");
        }
        /// <summary>
        /// Get a list of strings and process each string by itself.
        /// If one of the strings causes an error we return the stack to its previous state and send an error message 
        /// to the client.
        /// </summary>
        /// <param name="request">A list of strings' '.</param>
        public void ProcessRequest(List<string> request)
        {
            Stack<double> temp = new Stack<double>(data.GetData());
            foreach (string req in request)
            {
                ProcessSingleRequest(req);
                if(isError)
                {

                    data.SetData(temp);
                    isError = false;
                    SendStack();
                    return;
                }
            }
            SendStack();
        }
        /// <summary>
        /// Creates a string from the stack and send it to the client using a Callback
        /// </summary>
        private void SendStack()
        {
            if(data.GetSize() == 0)
            {
                Callback.PrintMessage("The stack is empty");
                return;
            }
            string stack = "";
            foreach (double x in data.GetData())
            {
                stack += x + "\n";
            }
            Callback.PrintMessage(String.Format("The current stack:" + "\n" + "{0}", stack));
            return;
        }
        /// <summary>
        /// Process one request.
        /// Must be one of: '+', '-', '/', '*' or a number in a double format.
        /// </summary>
        /// <param name="request"></param>
        private void ProcessSingleRequest(string request)
        {
            int size = data.GetSize();
            switch (request)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                    if (size < 2)
                    {
                        ProcessError(request, 2);
                        return;
                    }
                    ProcessAction(request);
                    break;
                case "_":
                    if (size == 0)
                    {
                        ProcessError(request, 1);
                        return;
                    }
                    data.RemoveElement();
                    break;
                default:
                    double number = Double.Parse(request);
                    data.InsertElement(number);
                    break;
            }
            
        }

        /// <summary>
        /// Send an error message to the client
        /// </summary>
        /// <param name="req">A string that causes the error</param>
        /// <param name="arguments">number of arguments that needed to be in the stack for the request</param>
        private void ProcessError(string req, int arguments)
        {
            string message = String.Format("You are trying to do an action {0} that requires {1} arguments, but the stack contains only {2} arguments", req,arguments ,data.GetSize());
            isError = true;
            Callback.PrintMessage(message);
            Callback.PrintMessage("Returing the stack to its previous state");
            return;

        }


        /// <summary>
        /// The function removes the top 2 numbers from the stack
        /// and with the given 'request' argument calculates a new number to push.
        /// The 'request' must be one of: '+', '-', '/' or '*'.
        /// </summary>
        /// <param name="request">String</param>
        private void ProcessAction(string request)
        {
            double leftArgument = data.RemoveElement();
            double rightArgument = data.RemoveElement();
            switch (request)
            {
                case "+":
                    data.InsertElement(calc.Addition(leftArgument, rightArgument));
                    break;
                case "-":
                    data.InsertElement(calc.Subtraction(leftArgument, rightArgument));
                    break;
                case "*":
                    data.InsertElement(calc.Multiplication(leftArgument, rightArgument));
                    break;
                case "/":
                    data.InsertElement(calc.Division(leftArgument, rightArgument));
                    break;
            }
        }


        ICallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<ICallback>();
            }
        }

    }
}
