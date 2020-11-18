using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFCalculator_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallback))]
    public interface IWCFCalculator
    {
        [OperationContract]
        void initializeConnection();

        [OperationContract]

        Stack<double> processRequest(string request);
        // TODO: Add your service operations here
    }

    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void Error(string error);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WCFCalculator_Service.ContractType".
    
}
