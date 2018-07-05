using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLogger
{
    public class Logs
    {
        private string _logResult;
        private string _path;
        private List<GitCommits> _gitCommits;
        private List<GitCommits> _gitCommitsAfterDate;

        #region Properties
        public DateTime StartDate { get; set; }
        public string LogResult
        {
            get
            {
                if (!string.IsNullOrEmpty(_path))
                {
                    _logResult = Util.Helper.ListLog(Path);
                }
                else _logResult = "No result returned";
                return _logResult;
            }
            set { _logResult = value; }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public List<GitCommits> GitCommitsList
        {
            get
            {
                if (!string.IsNullOrEmpty(LogResult))
                {
                    _gitCommits = Util.Helper.ParseLogs(LogResult);
                }
                return _gitCommits;
            }
            set { _gitCommits = value; }
        }

        public List<GitCommits> GitCommitListAfterDate
        {
            get
            {
                if (!string.IsNullOrEmpty(LogResult))
                {
                    _gitCommitsAfterDate = Util.Helper.RetrieveCommitsAfterDate(StartDate, GitCommitsList);
                }
                return _gitCommitsAfterDate;
            }
            set { _gitCommitsAfterDate = value; }
        }
        #endregion

        public void PrintToSreen(List<GitCommits> listgit)
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
