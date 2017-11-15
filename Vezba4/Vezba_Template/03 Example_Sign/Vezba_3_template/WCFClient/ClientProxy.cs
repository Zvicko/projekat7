using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFClient
{
	public class ClientProxy : ChannelFactory<IAccountPayment>, IAccountPayment, IDisposable
	{
		IAccountPayment factory;

		public ClientProxy(NetTcpBinding binding, string address) : base(binding, address)
		{
			factory = this.CreateChannel();
		}

		public void AddAccount(Account a)
		{
			try
			{
				factory.AddAccount(a);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error {0}", e.Message);
			}
		}

		public void RemoveAccount(string username)
		{
			try
			{
				factory.RemoveAccount(username);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error {0}", e.Message);
			}
		}

		public void DepositToAccount(string username, double amount)
		{
			try
			{
				factory.DepositToAccount(username, amount);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error {0}", e.Message);
			}
		}

		public void WithdrawalFromAccount(string username, double amount)
		{
			try
			{
				factory.WithdrawalFromAccount(username, amount);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error {0}", e.Message);
			}
		}

		public void PrintState()
		{
			try
			{
				factory.PrintState();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error {0}", e.Message);
			}
		}
	}
}
