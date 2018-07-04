using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Console.WriteLine(log);
            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();

        }
    }
}
