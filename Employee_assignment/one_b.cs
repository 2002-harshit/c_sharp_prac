/*

* When you have an interface implemented by a base class in C#, you don't need to explicitly implement the interface in the derived classes unless you want to provide specific implementations for the interface methods in those derived classes. 
*/

/*
* We can have two approcahes to this either implement interface on Employee and let both classes inherit it, or impkement interface for both permanent and trainee, this approach brings in code redundancy
*/

using System;
using System.Diagnostics.Contracts;

namespace One_b
{

    public interface IEmployee
    {
        void AcceptDetails();
        void CalculateSalary();
        void DisplayDetails();
    }

    public abstract class Employee : IEmployee
    {
        public uint EmpId;
        public string EmpName { get; set; } = string.Empty;
        public double Salary { get; protected set; }
        public DateTime Doj { get; set; }
        public virtual void AcceptDetails()
        {
            Console.Write("Enter EmpID: ");
            EmpId = Convert.ToUInt32(Console.ReadLine());

            Console.Write("Enter Employee Name: ");
            var nameInput = Console.ReadLine();
            EmpName = nameInput ?? string.Empty;

            Console.Write("Enter Employee Salary: ");
            if (double.TryParse(Console.ReadLine(), out double parsedSalary))
            {
                Salary = parsedSalary;
            }
            else
            {
                Console.WriteLine("Invalid input for salary");
                return;
            }


            Console.Write("Enter Date of Joining (dd/mm/yyyy): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime parsedDoj))
            {
                Doj = parsedDoj;
            }
            else
            {
                Console.WriteLine("Invalid input for Date of Joining.");
                return;
            }
        }

        public abstract void CalculateSalary();

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"Employee ID: {EmpId}, Name: {EmpName}, Date of Joining: {Doj.ToShortDateString()}, Salary: {Salary}");
        }
    }

    class Permanent : Employee
    {
        public double BasicPay { get; set; }
        public double HRA { get; set; }
        public double DA { get; set; }
        public double PF { get; set; }

        public override void AcceptDetails()
        {
            base.AcceptDetails();
            Console.Write("Enter Basic Pay: ");

            if (double.TryParse(Console.ReadLine(), out double parsedBP))
            {
                BasicPay = parsedBP;
            }
            else
            {
                Console.WriteLine("Invalid input for Basic Pay");
                return;
            }

            Console.Write("Enter HRA: ");

            if (double.TryParse(Console.ReadLine(), out double parsedHRA))
            {
                HRA = parsedHRA;
            }
            else
            {
                Console.WriteLine("Invalid input for HRA");
                return;
            }

            Console.Write("Enter DA: ");

            if (double.TryParse(Console.ReadLine(), out double parsedDA))
            {
                DA = parsedDA;
            }
            else
            {
                Console.WriteLine("Invalid input for DA");
                return;
            }

            Console.Write("Enter PF: ");
            if (double.TryParse(Console.ReadLine(), out double parsedPF))
            {
                PF = parsedPF;
            }
            else
            {
                Console.WriteLine("Invalid input for PF");
                return;
            }

        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Basic Pay: {BasicPay}, HRA: {HRA}, DA: {DA}, PF:{PF}");
        }

        public override void CalculateSalary()
        {

            Salary = Salary + BasicPay + HRA + DA - PF;
            Console.WriteLine($"Salary: {Salary}");
        }
    }

    public class Trainee : Employee
    {
        public double Bonus { get; set; }
        public string ProjectName { get; set; } = string.Empty;

        public override void AcceptDetails()
        {
            base.AcceptDetails();
            Console.Write("Enter Bonus: ");

            if (double.TryParse(Console.ReadLine(), out double parsedBonus))
            {
                Bonus = parsedBonus;
            }
            else
            {
                Console.WriteLine("Invalid input for Bonus");
                return;
            }

            Console.Write("Enter Poject Name: ");
            var projName = Console.ReadLine();

            ProjectName = projName ?? string.Empty;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Bonus: {Bonus}, Project Name: {ProjectName}");

        }

        public override void CalculateSalary()
        {
            double bonusPerc = 0;
            if (ProjectName.ToLower().Equals("banking"))
            {
                bonusPerc = 0.05;
            }
            else if (ProjectName.ToLower().Equals("insurance"))
            {
                bonusPerc = 0.1;

            }

            Salary += Salary * bonusPerc;
            Console.WriteLine($"Salary: {Salary}");
        }


    }
}



