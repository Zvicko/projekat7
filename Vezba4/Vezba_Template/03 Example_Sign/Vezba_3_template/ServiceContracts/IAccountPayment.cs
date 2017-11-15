using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
	[ServiceContract]
    public interface IAccountPayment
    {
		[OperationContract]
		void AddAccount(Account a);

		[OperationContract]
		void RemoveAccount(string username);

		[OperationContract]
		void DepositToAccount(string username, double amount);

		[OperationContract]
		void WithdrawalFromAccount(string username, double amount);

		[OperationContract]
		void PrintState();
    }
}
