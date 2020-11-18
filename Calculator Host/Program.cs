using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Calculator_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WCFCalculator_Service.WCFCalculatorImpl));
/*            host.AddServiceEndpoint((typeof(WCFCalculator_Service.IWCFCalculator)), new WSHttpBinding(), "");
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpsGetEnabled = true;
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);*/
            host.Open();
            Console.ReadLine();

        }
    }
}
