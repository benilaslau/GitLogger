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

        public Logs(string path)
        {
            this.Path = path;
        }

        #region Properties
        public DateTime StartDate { get; set; }
        public string LogResult
        {
            get
            {
                if (!string.IsNullOrEmpty(Path))
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
                    if (StartDate > DateTime.MinValue)
                    {
                        _gitCommits = Util.Helper.RetrieveCommitsAfterDate(StartDate, Util.Helper.ParseLogs(LogResult));
                    }
                    else
                    {
                        _gitCommits = Util.Helper.ParseLogs(LogResult);
                    }
                }
                return _gitCommits;
            }
            set { _gitCommits = value; }
        }

        
        #endregion

        }

    
}
