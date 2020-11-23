using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceContracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallback))]
    public interface IWCFCalculator
    {
        /// <summary>
        /// Initialize connection for each new client.
        /// </summary>
        [OperationContract]
        void InitializeConnection();
        /// <summary>
        /// Recieves a list of request for the calculator to perform.
        /// </summary>
        /// <param name="request">List of strings</param>
        [OperationContract(IsOneWay = true)]
        void ProcessRequest(List<string> request);

    }

    public interface ICallback
    {
        /// <summary>
        /// Print the message in the client console.
        /// </summary>
        /// <param name="error"></param>
        [OperationContract(IsOneWay = true)]
        void PrintMessage(string error);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WCFCalculator_Service.ContractType".
    
}
