using System;
using System.Collections.Generic;       // NDesk.Options
using System.Collections.Specialized;   // Redmine.Net.Api
using NDesk.Options;
using System.Linq;
using Atlassian.Jira;
using System.Net;
using MihaZupan;
using OpenSSL;

namespace werobotsjiraauth
{
    class Program
    {

        static void RMGoToJira(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"werobotsjiraauth.txt");
            //
            var privateKey = lines[2]; // <- get your private key in here
            var decoder = new OpenSSL.PrivateKeyDecoder.OpenSSLPrivateKeyDecoder();
            var keyInfo = decoder.Decode(privateKey);
            var consumerSecret = OpenSSL.PrivateKeyDecoder.RSAExtensions.ToXmlString(keyInfo, true);

            var jiraIO = Jira.CreateOAuthRestClient(lines[0], lines[1], consumerSecret, lines[3], lines[4]);  // URL, consKey, consSec, authAcc, authSec
            //
            //jiraIO.RestClient.Settings.Proxy = new HttpToSocks5Proxy("127.0.0.1", 1080);
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
