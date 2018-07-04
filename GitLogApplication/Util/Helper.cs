using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLogApplication.Util
{
    public static class Helper
    {

        #region public methods
        public static string ListLog(string path)
        {
            //git --git-dir=/mycode/.git --work-tree=/mycode log ?? --name-status
            var output = RunProcess(string.Format(" --git-dir={0}/.git --work-tree={1} log", path.Replace("\\", "/"), path.Replace("\\", "/")));
            return output;
        }

        public static List<GitCommits> ParseLogs(string log)
        {
            GitCommits commit = null;
            List<GitCommits> commits = new List<GitCommits>(); ;
            using (var reader = new StringReader(log))
            {
                do
                {
                    var line = reader.ReadLine();

                    if (line.StartsWith("commit "))
                    {
                        if (commit != null)
                            commits.Add(commit);
                        commit = new GitCommits();
                        commit.CommitHash = line.Split(' ')[1];
                    }

                    if (line.StartsWith("Author:"))
                    {
                        commit.Author = line.Split(' ')[1] + " " + line.Split(' ')[2];
                    }

                    if (line.StartsWith("Date:"))
                    {
                        string stringDate = line.After("Date:   ");

                        DateTime date;
                        DateTime.TryParseExact(
                            stringDate,
                            "ddd MMM d HH:mm:ss yyyy K",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None,
                            out date
                        );
                        commit.Date = date.ToString();
                        
                    }

                    if (line.Length > 0 && line[3] == ' ')
                    {
                        commit.Message = line.After("    ");
                    }

                } while (reader.Peek() != -1);
            }
            return commits;
        }
        #endregion



        #region private methods
        private static string RunProcess(string command)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = Config.GitExectuable.Replace("\\", "/");
            p.StartInfo.Arguments = command;
            p.Start();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }

        // method to read after a string value
        private static string After(this string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }
        #endregion
    }
}
