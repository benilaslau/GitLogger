using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitLogger;

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();
        }

        public static void ShowTheLogs()
        {
            string path = Config.RepoPath.Replace("\\", "/");
            int option = 0;
            Logs logs = new Logs(path);
            do
            {
                Console.WriteLine("1.Show all logs.\n2.Show after date.");
                option = Convert.ToInt16(Console.ReadLine());
                if (option == 1)
                {
                    PrintToSreen(logs.GitCommitsList);
                }
                if (option == 2)
                {
                    ValidateDate(logs);
                    PrintToSreen(logs.GitCommitsList);
                }
            } while (option != 0);
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
                ValidateDate(logs); // if date was not valid
            }
        }

        public static void PrintToSreen(List<GitCommits> listgit)
        {
            foreach (var c in listgit)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Commit: {0}", c.CommitHash);
                Console.ResetColor();
                Console.WriteLine("Author: {0}\nDate: {1}\nMessage: {2}\n", c.Author, c.Date.ToString(), c.Message);
                foreach (var file in c.Files)
                {
                    Console.WriteLine("{0}\t {1}\n\n", file.Status, file.FileName);
                }
            }

        }
    }
}
