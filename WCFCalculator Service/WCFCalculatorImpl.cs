using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Data_Layer;
using Calculator_Library;
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


        public void initializeConnection()
        {
            data = new DataHolderImpl();
            calc = new CalculatorFunctionsImpl();
            Console.WriteLine("A new connection");
        }

        public void processRequest(List<string> request)
        {
            Stack<double> temp = new Stack<double>(data.GetData());
            foreach (string req in request)
            {
                processSingleRequest(req);
                if(isError)
                {
                    data.setData(temp);
                    isError = false;
                    return;
                }
            }
        }

        private void processSingleRequest(string request)
        {
            try
            {
                double number = Double.Parse(request);
                data.InsertElement(number);
            }catch (FormatException e)
            {
                if(data.GetSize() >=2)
                    processAction(request);
                else
                {
                    string message = String.Format("You are trying to do an action {0} that requires 2 arguments, but the stack contains only {1} arguments", request, data.GetSize());
                    isError = true;
                    Callback.printMessage(message);
                    Callback.printMessage("Returing the stack to its previous state");
                    return;
                }
            }
        }

 

        private void processAction(string request)
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
