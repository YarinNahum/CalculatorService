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


        public void initializeConnection()
        {
            data = new DataHolderImpl();
            calc = new CalculatorFunctionsImpl();
            Console.WriteLine("A new connection");
        }

        public Stack<double> processRequest(string request)
        {
            int length = request.Length;
            Console.WriteLine("The request length is {0}", length);
            data.InsertElement(length);
            if (length == 6)
                Callback.Error("the length is 6");
            return data.GetData();
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
