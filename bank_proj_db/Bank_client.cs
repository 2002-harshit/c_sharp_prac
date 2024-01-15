using bank_proj_db.Models;

namespace bank_proj_db
{
    public class MainProgram
    {

        public static void Main()
        {
            BankRepository BR = new();

            Console.WriteLine("Welcome to banking facility:");
            int choice;
            do
            {
                Console.WriteLine("\n1. Deposit ammount");
                Console.WriteLine("2. Withdraw amount");
                Console.WriteLine("3. Get your account details");
                Console.WriteLine("4. Get all account details");
                Console.WriteLine("5. Get your transaction details");
                Console.WriteLine("6: Open a new account");
                Console.WriteLine("7. Exit\n");
                Console.WriteLine("Enter your choice (1-7)\n");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = -1;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                Console.WriteLine("Enter account number:");
                                int inputAc = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter amount:");
                                decimal inputAm = Convert.ToDecimal(Console.ReadLine());
                                BR.DepositAmount(inputAc, inputAm);
                                Console.WriteLine("\nAmmount Deposited");
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Enter account number: ");
                                int inputAc = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter amount:");
                                decimal inputAm = Convert.ToDecimal(Console.ReadLine());
                                BR.WithdrawAmount(inputAc, inputAm);
                                Console.WriteLine("\nAmmount Withdrawn");
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Enter account number: ");
                                int inputAc = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(BR.GetAccountDetails(inputAc));
                                break;
                            }
                        case 4:
                            {
                                foreach (Sbaccount acc in BR.GetAllAccounts())
                                {
                                    Console.WriteLine(acc);
                                }
                                break;
                            }
                        case 5:
                            {
                                Console.WriteLine("Enter account number: ");
                                int inputAc = Convert.ToInt32(Console.ReadLine());
                                foreach (Sbtransaction t in BR.GetTransactions(inputAc))
                                {
                                    Console.WriteLine();
                                    Console.WriteLine(t);
                                }
                                break;
                            }
                        case 6:
                            {
                                Console.WriteLine("Enter Name ");
                                string? inputName = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(inputName))
                                {
                                    Console.WriteLine("Name is required");
                                    break;
                                }

                                Console.WriteLine("Enter Address");
                                string? inputAddr = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(inputAddr))
                                {
                                    Console.WriteLine("Address is required");
                                    break;
                                }

                                BR.NewAccount(new Sbaccount
                                {
                                    Customername = inputName,
                                    Customeraddress = inputAddr,
                                    Currentbalance = 0
                                });
                                Console.WriteLine("\nAccount created");
                                break;
                            }
                        case 7:
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (AccountNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (NotEnoughAmountException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }

            }
            while (choice != 7);
        }
    }
}