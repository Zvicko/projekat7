using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFClient
{
	class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();			
			string address = "net.tcp://localhost:9999/SecurityAdministration";


			Account a1 = new Account("user1");
			Account a2 = new Account("user2", 100);

			using (ClientProxy proxy = new ClientProxy(binding, address))
			{
				proxy.AddAccount(a1);
				proxy.AddAccount(a2);
				proxy.DepositToAccount(a1.Username, 150);
				proxy.WithdrawalFromAccount(a2.Username, 50);
				proxy.PrintState();
			}

			Console.ReadLine();
		}
	}
}
