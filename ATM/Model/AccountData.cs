using System;

namespace ATM.Model
{
    public interface IAccounts
    {
    }
    public class AccountData : IAccounts
    {
        public Account[] _account ={
            new Account{cardNumber=1234567891234567,pin=1234,accountType='c',amount=10000,modifiedDate=DateTime.UtcNow},
            new Account{cardNumber=1234567891234589,pin=5678,accountType='s',amount=20000,modifiedDate=DateTime.UtcNow},
        };
    }
}
