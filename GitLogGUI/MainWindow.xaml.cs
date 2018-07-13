using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GitLogger;

namespace GitLogGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowAllLogs_Click(object sender, RoutedEventArgs e)
        {
            LogsText.Text = "";
            string path = Config.RepoPath.Replace("\\", "/");
            Logs logs = new Logs(path);
            
            foreach(var item in logs.GitCommitsList)
            {
                LogsText.Text += "Commit: " + item.CommitHash + "\nAuthor" + item.Author + "\nDate" + item.Date + "\n";
                foreach(var file in item.Files)
                {
                    LogsText.Text += file.Status + "\t" + file.FileName + "\n";
                }
                LogsText.Text += "\n";
            }
            
        }
        private void ShowByDate_Click(object sender, RoutedEventArgs e)
        {
            LogsText.Text = "";
            if (AfterDatePicker.SelectedDate != DateTime.MinValue)
            {
                string path = Config.RepoPath.Replace("\\", "/");
                Logs logs = new Logs(path);
                
                logs.StartDate = AfterDatePicker.SelectedDate ?? DateTime.MinValue;
                TimeSpan ts = new TimeSpan(Convert.ToInt16(DateHours.Text), Convert.ToInt16(DateMinutes.Text), 0);
                logs.StartDate += ts; 
                foreach (var item in logs.GitCommitsList)
                {
                    LogsText.Text += "Commit: " + item.CommitHash + "\nAuthor" + item.Author + "\nDate" + item.Date + "\n";
                    foreach (var file in item.Files)
                    {
                        LogsText.Text += file.Status + "\t" + file.FileName + "\n";
                    }
                    LogsText.Text += "\n";
                }
            }
            else {
                MessageBox.Show("Select a date please");
            }
        }
    }
}
