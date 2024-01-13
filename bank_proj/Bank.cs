using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Bank
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(string message) : base(message)
        {
        }
    }

    public class NotEnoughAmountException : Exception
    {
        public NotEnoughAmountException(string message) : base(message) { }
    }

    public class SBAccount
    {
        private static int TOTAL_ACCOUNTS = 1;
        public int AccountNumber { get; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public decimal CurrentBalance { get; set; }

        public SBAccount(string cn, string ca)
        {
            AccountNumber = TOTAL_ACCOUNTS;
            CustomerName = cn;
            CustomerAddress = ca;
            CurrentBalance = 0m;
            TOTAL_ACCOUNTS++;
        }

        public override string ToString()
        {
            return $"Acc No: {AccountNumber}\nName: {CustomerName}\nAddres: {CustomerAddress}\nBalance: {CurrentBalance}\n";
        }
    }

    //* if you dont proide the setter, its just a readonly property, it can be set  only in the constructor
    //* haing a private setter is different, the value can be set from anywhere in the class

    public class SBTransaction
    {
        private static int TOTAL_TRANSACTIONS = 1;
        public int TransactionId { get; }//* a readonly property, you can use readonly keyword

        public DateTime TransactionDate { get; }
        public int AccountNumber { get; }
        public decimal Amount { get; }
        public string TransactionType { get; }

        public SBTransaction(DateTime td, int an, decimal am, string tt)
        {
            TransactionId = TOTAL_TRANSACTIONS;
            TransactionDate = td;
            AccountNumber = an;
            Amount = am;
            TransactionType = tt;
            TOTAL_TRANSACTIONS++;
        }

        public override string ToString()
        {
            return $"Transaction id: {TransactionId}\nTransaction Date: {TransactionDate}\nAcc No: {AccountNumber}\nAmount: {Amount}\nTransaction Type: {TransactionType}";
        }
    }

    public interface IBankRepository
    {
        void NewAccount(SBAccount acc);
        List<SBAccount> GetAllAccounts();
        SBAccount GetAccountDetails(int accno);
        void DepositAmount(int accno, decimal amt);
        void WithdrawAmount(int accno, decimal amt);
        List<SBTransaction> GetTransactions(int accno);
    }

    public class BankRepository : IBankRepository
    {
        List<SBAccount> BankAccounts = [];
        List<SBTransaction> BankTransactions = [];

        public BankRepository() { }

        public BankRepository(List<SBAccount> ba, List<SBTransaction> bt)
        {
            BankAccounts = ba;
            BankTransactions = bt;
        }


        public void DepositAmount(int accno, decimal amt)
        {
            if (amt == 0) return;
            SBAccount? ac = BankAccounts.Find(ba => ba.AccountNumber == accno);
            if (ac != null)
            {
                ac.CurrentBalance += amt;

                BankTransactions.Add(new SBTransaction(DateTime.Now, ac.AccountNumber, amt, "Deposit"));
            }
            else
            {
                throw new AccountNotFoundException("Account does not exist");
            }
        }

        public SBAccount GetAccountDetails(int accno)
        {
            SBAccount? ac = BankAccounts.Find(ba => ba.AccountNumber == accno);
            if (ac != null)
            {
                return ac;
            }
            else
            {
                throw new AccountNotFoundException("Account does not exist");
            }

        }

        public List<SBAccount> GetAllAccounts()
        {
            return BankAccounts;
        }

        public List<SBTransaction> GetTransactions(int accno)
        {
            return BankTransactions.FindAll(tr => tr.AccountNumber == accno);
        }

        public void NewAccount(SBAccount acc)
        {
            BankAccounts.Add(acc);
        }

        public void WithdrawAmount(int accno, decimal amt)
        {
            if (amt == 0) return;
            SBAccount? ac = BankAccounts.Find(ba => ba.AccountNumber == accno);
            if (ac != null)
            {
                if (ac.CurrentBalance - amt < 0)
                {
                    throw new NotEnoughAmountException("Not enough balance");
                }
                else
                {
                    ac.CurrentBalance -= amt;
                    BankTransactions.Add(new SBTransaction(DateTime.Now, ac.AccountNumber, amt, "Withdraw"));

                }

            }
            else
            {
                throw new AccountNotFoundException("Account does not exist");
            }
        }
    }

}