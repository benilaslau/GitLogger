using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLogApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ShowTheLogs();   
            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();
        }

        public static void ShowTheLogs()
        {
            Logs logs = new Logs();
            logs.Path = @"C:\Users\blaslau\source\repos\CalculatorSolution\";
            Console.WriteLine("1.Show all logs.\n2.Show after date.");
            int option = Convert.ToInt16(Console.ReadLine());
            if(option == 1)
            {
                logs.PrintToSreen(logs.GitCommitsList);
            }
            if(option == 2)
            {
               ValidateDate(logs);
                DateTime date = logs.StartDate;
               logs.PrintToSreen(logs.GitCommitListAfterDate);

            }
            //
            //string date = Console.ReadLine();
            //Console.WriteLine();
            //logs.StartDate = DateTime.Parse(date);
            
        }

        public static void ValidateDate(Logs logs)
        {
            Console.WriteLine("Enter a date after you want to see logs.\nFormat is MM/dd/yyyy HH:mm:ss ");
            string date = Console.ReadLine();
            DateTime datetime;            
            if (DateTime.TryParse(date, out datetime))
            {
                logs.StartDate = datetime;
            }
            else
            {
                ValidateDate(logs);
            }
        }
    }
}
