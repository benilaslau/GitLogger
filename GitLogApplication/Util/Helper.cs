using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        #endregion
    }
}
