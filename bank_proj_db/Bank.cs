using System;
using bank_proj_db.Models;

namespace bank_proj_db.Models
{

    //* this is extending the partial classes, the classes have to be in separate files, but in the same namespace, partial classes is nothing but code in small chunks in different files but same namespace
    public partial class Sbaccount
    {
        public override string ToString()
        {
            return $"Acc No: {Accountnumber}\nName: {Customername}\nAddres: {Customeraddress}\nBalance: {Currentbalance}\n";
        }
    }

    public partial class Sbtransaction
    {
        public override string ToString()
        {
            return $"Transaction id: {Transactionid}\nTransaction Date: {Transactiondate}\nAcc No: {Accountnumber}\nAmount: {Amount}\nTransaction Type: {Transactiontype}";
        }
    }
}

namespace bank_proj_db
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
    public interface IBankRepository
    {
        void NewAccount(Sbaccount acc);
        List<Sbaccount> GetAllAccounts();
        Sbaccount GetAccountDetails(int accno);
        void DepositAmount(int accno, decimal amt);
        void WithdrawAmount(int accno, decimal amt);
        List<Sbtransaction> GetTransactions(int accno);
    }

    public class BankRepository : IBankRepository
    {
        private readonly PostgresContext db = new();
        public void DepositAmount(int accno, decimal amt)
        {
            if (amt == 0) return;

            Sbaccount? ac = db.Sbaccounts.Find(accno);

            if (ac != null)
            {
                ac.Currentbalance += amt;

                db.Sbtransactions.Add(new Sbtransaction
                {
                    Transactiondate = DateOnly.FromDateTime(DateTime.Now),
                    Accountnumber = accno,
                    Amount = amt,
                    Transactiontype = "Deposit"
                });

                db.SaveChanges();
            }
            else
            {
                throw new AccountNotFoundException("Account does not exist");
            }

        }

        public Sbaccount GetAccountDetails(int accno)
        {
            Sbaccount? ac = db.Sbaccounts.Find(accno);
            if (ac != null)
            {
                return ac;
            }
            else
            {
                throw new AccountNotFoundException("Account does not exist");
            }
        }

        public List<Sbaccount> GetAllAccounts()
        {
            return db.Sbaccounts.ToList();
            // return [.. db.Sbaccounts];
        }

        public List<Sbtransaction> GetTransactions(int accno)
        {
            return db.Sbtransactions.Where(tr => tr.Accountnumber == accno).ToList();

        }

        public void NewAccount(Sbaccount acc)
        {
            db.Sbaccounts.Add(acc);
            db.SaveChanges();
        }

        public void WithdrawAmount(int accno, decimal amt)
        {
            if (amt == 0) return;
            Sbaccount? ac = db.Sbaccounts.Find(accno);
            if (ac != null)
            {
                if (ac.Currentbalance - amt < 0)
                {
                    throw new NotEnoughAmountException("Not enough balance");
                }
                else
                {
                    ac.Currentbalance -= amt;
                    db.Sbtransactions.Add(new Sbtransaction
                    {
                        Transactiondate = DateOnly.FromDateTime(DateTime.Now),
                        Accountnumber = accno,
                        Amount = amt,
                        Transactiontype = "Withdrawl"
                    });

                    db.SaveChanges();
                }

            }
            else
            {
                throw new AccountNotFoundException("Account does not exist");
            }
        }
    }

}