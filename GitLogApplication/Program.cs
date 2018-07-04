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
            string path = @"C:\Users\blaslau\source\repos\CalculatorSolution\";
            string log;
            try
            {
                log = Util.Helper.ListLog(path);
                List<GitCommits> listCmts = Util.Helper.ParseLogs(log);
                string date = "7/3/2018 2:30:50 PM";
                DateTime dt = DateTime.Parse(date);
                List<GitCommits> cmtsAfterDate = Util.Helper.RetrieveCommitsAfterDate(dt, listCmts);
                PrintToSreen(cmtsAfterDate);


            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();
        }

        public static void PrintToSreen(List<GitCommits> listgit)
        {
            foreach(var c in listgit)
            {
                Console.WriteLine("commit: {0}\n Author: {1}\n Date: {2}\n Message: {3}\n", c.CommitHash, c.Author, c.Date.ToString(), c.Message);
            }
        }
    }
}
