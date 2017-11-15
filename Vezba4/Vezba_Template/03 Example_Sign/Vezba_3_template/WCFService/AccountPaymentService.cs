using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService
{
	public class AccountPaymentService : IAccountPayment
	{
		//Dictionary key is userName
		private static Dictionary<string, Account> accounts = new Dictionary<string, Account>();


		public void AddAccount(Account a)
		{
			if (accounts.ContainsKey(a.Username))
			{
				//Throw exception instead.
				Console.WriteLine("User {0} already added.", a.Username);
			}

			accounts.Add(a.Username, a);
		}


		public void RemoveAccount(string username)
		{
			if (!accounts.ContainsKey(username))
			{
				//Throw exception instead.
				Console.WriteLine("User {0} doesn't exist.", username);
			}

			accounts.Remove(username);
		}

		public void DepositToAccount(string username, double amount)
		{
			Account a = null;
			accounts.TryGetValue(username, out a);
			if (a != null)
			{
				a.Deposit(amount);

			}
		}

		public void WithdrawalFromAccount(string username, double amount)
		{
			Account a = null;
			accounts.TryGetValue(username, out a);
			if (a != null)
			{
				a.Withdrawal(amount);

			}
		}

		public void PrintState()
		{
			foreach(Account a in accounts.Values)
			{
				Console.WriteLine("Account for user: {0}", a.Username);
				Console.WriteLine("Current account state: {0}", a.CurrentState);
				Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = =");
			}
		}
	}
}
