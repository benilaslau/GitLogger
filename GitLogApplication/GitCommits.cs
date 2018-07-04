using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLogApplication
{
    public class GitCommits
    {
        public GitCommits()
        {
            CommitHash = "";
            Author = "";
            Message = "";
        }
        public string CommitHash { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

    }
}
