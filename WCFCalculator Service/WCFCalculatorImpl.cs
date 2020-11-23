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

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    // NOTE: You canuse the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class WCFCalculatorImpl : IWCFCalculator
    {
        private IDataHolder<double> data = null;
        private ICalculatorFunctions calc = null;
        private bool isError = false;


        public void InitializeConnection()
        {
            data = new DataHolderImpl();
            calc = new CalculatorFunctionsImpl();
            Console.WriteLine("A new connection");
        }

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

        private void ProcessError(string req, int arguments)
        {
            string message = String.Format("You are trying to do an action {0} that requires {1} arguments, but the stack contains only {2} arguments", req,arguments ,data.GetSize());
            isError = true;
            Callback.PrintMessage(message);
            Callback.PrintMessage("Returing the stack to its previous state");
            return;

        }



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
