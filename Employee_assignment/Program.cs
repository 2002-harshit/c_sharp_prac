namespace Main
{
    public class MainProgram
    {
        public static void Main()
        {
            // For the first part
            // One_a.Permanent p1 = new();
            // p1.GetDetails();
            // p1.ShowDetails();
            // p1.CalculateSalary();

            // One_a.Trainee t1 = new();
            // t1.GetTraineeDetails();
            // t1.ShowTraineeDetails();
            // t1.CalculateSalary();

            //For the second part

            One_b.Permanent p1 = new();
            p1.AcceptDetails();
            p1.DisplayDetails();
            p1.CalculateSalary();

            One_b.Trainee t1 = new();
            t1.AcceptDetails();
            t1.DisplayDetails();
            t1.CalculateSalary();


        }
    }

}