using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLogger.Util
{
    public static class Helper
    {

        #region public methods
        public static string ListLog(string path)
        {
            //git --git-dir=/mycode/.git --work-tree=/mycode log ?? --name-status
            const string command = " --git-dir={0}/.git --work-tree={1} log --name-status";
            var output = RunProcess(string.Format(command, path.Replace("\\", "/"), path.Replace("\\", "/")));
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
                    string line = reader.ReadLine();

                    if (line.StartsWith("commit "))
                    {
                        if (commit != null)
                            commits.Add(commit);
                        commit = new GitCommits();
                        commit.CommitHash = line.Split(' ')[1];
                        line = reader.ReadLine();
                        commit.Author = line.Split(' ')[1] + " " + line.Split(' ')[2];
                        line = reader.ReadLine();
                        commit.Date = FormateGitDate(line.After("Date:   "));
                        line = reader.ReadLine();
                        line = reader.ReadLine();
                        commit.Message = line.After("    ");
                    }
                    else if (line.Length > 1 && Char.IsLetter(line[0]) && line[1] == '\t')
                    {
                        var status = line.Split('\t')[0];
                        var file = line.Split('\t')[1];
                        commit.Files.Add(new FileStatus() { Status = status, FileName = file });
                    }

                } while (reader.Peek() != -1);
            }
            return commits;
        }

        public static List<GitCommits> RetrieveCommitsAfterDate(DateTime date, List<GitCommits> commits)
        {
            List<GitCommits> commitsAfterDate = new List<GitCommits>();
            foreach (var commit in commits)
            {
                if (commit.Date > date)
                {
                    commitsAfterDate.Add(commit);
                }
            }
            return commitsAfterDate;
        }
        #endregion

        #region private methods
        private static string RunProcess(string command)
        {
            string output = String.Empty;
            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = Config.GitExectuable.Replace("\\", "/");
                p.StartInfo.Arguments = command;
                p.Start();
                // Read the output stream first and then wait.
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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

        private static DateTime FormateGitDate(string gitDate)
        {
            DateTime date;
            DateTime.TryParseExact(
                gitDate,
                "ddd MMM d HH:mm:ss yyyy K",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date
            );
            return date;
        }
        #endregion
    }
}
