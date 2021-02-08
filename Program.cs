using System;
using System.Collections.Generic;       // NDesk.Options
using System.Collections.Specialized;   // Redmine.Net.Api
using NDesk.Options;
using System.Linq;
using Atlassian.Jira;

namespace werobotsjiraauth
{
    class Program
    {

        static void RMGoToJira(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"werobotsjiraauth.txt");
            //
            var jiraIO = Jira.CreateOAuthRestClient(lines[0], lines[1], lines[2], lines[3], lines[4]);  // URL, consKey, consSec, authAcc, authSec
            //
            Atlassian.Jira.Issue moveTo = jiraIO.Issues.GetIssueAsync(lines[5]).Result;
            //
            Console.WriteLine("Task info {0}: {1}", moveTo.Key, moveTo.Summary);
        }

        static void Main(string[] args)
        {
            RMGoToJira(args);
        }
    }
}
