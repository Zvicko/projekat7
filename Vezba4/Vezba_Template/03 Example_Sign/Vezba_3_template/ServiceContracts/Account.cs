using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
	[DataContract]
	public class Account
	{
		const double kamataNaUplatu = 0.5;
		const double kamataNaIsplatu = 0.7;

		string userName = string.Empty;
		double currentState = 0;

		[DataMember]
		public string Username
		{
			get { return userName; }
			set { userName = value; }
		}

		[DataMember]
		public double CurrentState
		{
			get { return currentState; }
			set { currentState = value; }
		}

		public Account(string _username)
		{
			userName = _username;
		}

		public Account(string _username, double _initState) : this(_username)
		{
			currentState = _initState;
		}

		public void Deposit(double d)
		{
			/// additional logic/checks can be added.
			currentState += d - d * kamataNaUplatu;
		}

		public void Withdrawal(double w)
		{
			/// additional logic/checks can be added.
			currentState -= (w + w * kamataNaIsplatu);
		}
	}
}
