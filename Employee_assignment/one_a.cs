using System;

//* classes can be public or internal, this is on the basis of asemblies or a project, a public class can be accessed across assemblies, an internal class wihin the  same assembly or the project, namespace is out of question(not dependent)

//* Mostly a project is compiled to just a single assembly

//* for properties access modifiers, the most restrictive wins like the intersection of a venn diagram

//* virtual goes for potential overriding , abstarct is definite overriding

namespace One_a
{

    public abstract class Employee
    {
        public uint EmpId;
        public string EmpName { get; set; } = string.Empty;
        public double Salary { get; protected set; }
        public DateTime Doj { get; set; }

        public void AcceptDetails()
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

        public void DisplayDetails()
        {
            Console.WriteLine($"Employee ID: {EmpId}, Name: {EmpName}, Date of Joining: {Doj.ToShortDateString()}, Salary: {Salary}");
        }


        // public virtual void CalculateSalary()
        // {
        //     // Base implementation for salary calculation, marked virtual for potential overriding,
        //     //! potential remeber
        //     Console.WriteLine($"Salary: {Salary}");
        // }
        public abstract void CalculateSalary();

    }

    public class Permanent : Employee
    {
        public double BasicPay { get; set; }
        public double HRA { get; set; }
        public double DA { get; set; }
        public double PF { get; set; }

        public void GetDetails()
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

        public void ShowDetails()
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

        public void GetTraineeDetails()
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

        public void ShowTraineeDetails()
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
