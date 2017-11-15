using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFService
{
	public class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/SecurityAdministration";

			ServiceHost host = new ServiceHost(typeof(AccountPaymentService));
			host.AddServiceEndpoint(typeof(IAccountPayment), binding, address);

			host.Open();

			Console.WriteLine("SecurityAdministration service is started.");
			Console.WriteLine("Press <enter> to stop service...");

			Console.ReadLine();
			host.Close();
		}
	}
}
