using System;
using System.Collections.Generic;
using Chocoholics_Anonymous.CA_DataDriver;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_ServiceManagers
{
    public class ReportManager
    {
        private ReportDriver reportDriver;
        private MemberDriver memberDriver;
        private ProviderDriver providerDriver;
        private ServiceDriver serviceDriver;
        private TransactionDriver transactionDriver;

        public ReportManager(ReportDriver reportDriver, MemberDriver memberDriver, ProviderDriver providerDriver, ServiceDriver serviceDriver, TransactionDriver transactionDriver)
        {
            this.reportDriver = reportDriver;
            this.memberDriver = memberDriver;
            this.providerDriver = providerDriver;
            this.serviceDriver = serviceDriver;
            this.transactionDriver = transactionDriver;
        }

        public void generateSummaryReport()
        {
            Report report = new Report();
            DateTime now = DateTime.Now;
            DateTime weekAgo = now.Subtract(new TimeSpan(7, 0, 0, 0));

            // Header
            String header = "";
            
            header += $"# Summary Report\n\n";

            report.Header = header;

            // Body
            String body = "";
            int numProviders = 0;
            int numConsultations = 0;
            double totalFee = 0.0;
            Transaction[] transactions = transactionDriver.getAll();

            body += $"## Accounts Payable\n\n";
            body += $"| Provider | Consultations | Total Fee |\n";
            body += $"| ---------- | ---------- | ---------- |\n";
            
            Dictionary<int, int> providerConsulations = new Dictionary<int, int>();
            Dictionary<int, double> providerFee = new Dictionary<int, double>();

            Service service;
            foreach (var transaction in transactions)
            {
                if (DateTime.Compare(transaction.DateOfService, now) < 0 
                    && DateTime.Compare(transaction.DateOfService, weekAgo) > 0)
                {
                    service = serviceDriver.get(transaction.ServiceId);
                    if (!providerConsulations.ContainsKey(transaction.ProviderId))
                    {
                        providerConsulations.Add(transaction.ProviderId, 1);
                        providerFee.Add(transaction.ProviderId, service.Fee);
                        numProviders++;
                    }
                    else
                    {
                        providerConsulations[transaction.ProviderId]++;
                        providerFee[transaction.ProviderId] += service.Fee;
                    }

                    numConsultations++;
                    totalFee += service.Fee;
                }
            }

            foreach (var element in providerConsulations)
            {
                Provider provider = providerDriver.get(element.Key);
                
                body += $"| {provider.Name} ";
                
                // Get provider name from transaction
                body += $"| {providerConsulations[element.Key]} ";
                
                // Get service name from transaction
                body += $"| ${providerFee[element.Key]:F} |\n";
            }

            report.Body = body;
            
            String footer = "";

            footer += $"## Totals\n\n";
            footer += $"| Providers | Consultations | Fee |\n";
            footer += $"| ---------- | ---------- | ---------- |\n";
            footer += $"| {numProviders} | {numConsultations} | ${totalFee:F} |\n";

            report.Footer = footer;

            reportDriver.print($"Summary_Report_{now.Month}-{now.Day}-{now.Year}", "md", report);
        }

        public void generateEFTReport()
        {
            Report report = new Report();
            DateTime now = DateTime.Now;
            DateTime weekAgo = now.Subtract(new TimeSpan(7, 0, 0, 0));

            Transaction[] transactions = transactionDriver.getAll();

            String body = "";
            
            body += $"Provider, Provider ID, Total Owed\n";
            
            Dictionary<int, double> providerFee = new Dictionary<int, double>();

            Service service;
            foreach (var transaction in transactions)
            {
                if (DateTime.Compare(transaction.DateOfService, now) < 0 &&
                    DateTime.Compare(transaction.DateOfService, weekAgo) > 0)
                {
                    service = serviceDriver.get(transaction.ServiceId);
                    if (!providerFee.ContainsKey(transaction.ProviderId))
                    {
                        providerFee.Add(transaction.ProviderId, service.Fee);
                    }
                    else
                    {
                        providerFee[transaction.ProviderId] += service.Fee;
                    }
                }
            }
            
            foreach (var element in providerFee)
            {
                Provider provider = providerDriver.get(element.Key);
                
                body += $"{provider.Name}, ";
                
                // Get provider name from transaction
                body += $"{element.Key}, ";
                
                // Get service name from transaction
                body += $"{providerFee[element.Key]:F}\n";
            }
            
            report.Body = body;
            
            DateTime date = DateTime.Now;
            
            reportDriver.print($"EFT_{date.Month}-{date.Day}-{date.Year}", "csv", report);
        }

        public void generateMembersReports()
        {
            Member[] members = memberDriver.getAll();
            foreach (var member in members)
            {
                generateMemberReport(member);
            }
        }

        // Generate a report for a given member
        public void generateMemberReport(Member member)
        {
            Report report = new Report();
            DateTime now = DateTime.Now;
            DateTime weekAgo = now.Subtract(new TimeSpan(7, 0, 0, 0));
            
            // Get all of the transactions for a member
            Transaction[] transactions = transactionDriver.getAll();
            LinkedList<Transaction> memberTransactions = new LinkedList<Transaction>();
            
            foreach (var transaction in transactions)
            {
                if (DateTime.Compare(transaction.DateOfService, now) < 0 &&
                    DateTime.Compare(transaction.DateOfService, weekAgo) > 0)
                {
                    if (transaction.MemberId == member.MemberId)
                        memberTransactions.AddLast(transaction);
                }
            }

            if (memberTransactions.Count == 0) return;

            // Header
            String header = "";

            header += $"# Member Report\n\n";
            header += $"*{member.Name}* ({member.MemberId.ToString()})\n\n";
            header += $"{member.Address}\n\n";
            header += $"{member.City}, {member.State}, {member.Zip}\n";

            report.Header = header;

            // Body
            String body = "";

            body += $"## Transactions\n\n";
            body += $"| Date | Provider | Service |\n";
            body += $"| ---------- | ---------- | ---------- |\n";
            
            foreach (var transaction in memberTransactions)
            {
                body += $"| {transaction.DateOfService.Month}-{transaction.DateOfService.Day}-{transaction.DateOfService.Year} ";
                
                // Get provider name from transaction
                body += $"| {providerDriver.get(transaction.ProviderId).Name}";
                
                // Get service name from transaction
                body += $"| {serviceDriver.get(transaction.ServiceId).Name} |\n";
            }

            body += "\n";

            report.Body = body;
            
            DateTime date = DateTime.Now;

            reportDriver.print($"{member.Name.Replace(" ", "_")}_{date.Month}-{date.Day}-{date.Year}", "md", report);
        }
        
        public void generateProvidersReports()
        {
            Provider[] providers = providerDriver.getAll();
            foreach (var provider in providers)
            {
                generateProviderReport(provider);
            }
        }
        
        // Generate a report for given provider
        public void generateProviderReport(Provider provider)
        {
            Report report = new Report();
            DateTime now = DateTime.Now;
            DateTime weekAgo = now.Subtract(new TimeSpan(7, 0, 0, 0));
            
            // Get all of the transactions for a member
            Transaction[] transactions = transactionDriver.getAll();
            LinkedList<Transaction> providerTransactions = new LinkedList<Transaction>();
            
            foreach (var transaction in transactions)
            {
                if (DateTime.Compare(transaction.DateOfService, now) < 0 &&
                    DateTime.Compare(transaction.DateOfService, weekAgo) > 0)
                {
                    if (transaction.ProviderId == provider.ProviderId)
                        providerTransactions.AddLast(transaction);
                }
            }

            if (providerTransactions.Count == 0) return;
            
            // Header
            String header = "";

            header += $"# Provider Report\n\n";
            header += $"*{provider.Name}* ({provider.ProviderId})\n\n";
            header += $"{provider.Address}\n\n";
            header += $"{provider.City}, {provider.State}, {provider.Zip}\n";

            report.Header = header;

            // Body
            String body = "";
            int numConsultations = 0;
            double totalFee = 0.0;
            
            body += "## Services Provided\n\n";
            body += "| Service Date | Received Date | Member Name | Member Number | Service Code | Fee |\n";
            body += "| ---------- | ---------- | ---------- | ---------- | ---------- | ---------- |\n";
            
            foreach (var transaction in providerTransactions)
            {
                body += $"| {transaction.DateOfService.Month}-{transaction.DateOfService.Day}-{transaction.DateOfService.Year} ";
                
                body += $"| {transaction.CurrentDateAndTime} ";
                
                Member member = memberDriver.get(transaction.MemberId);
                
                body += $"| {member.Name} ";
                
                body += $"| {member.MemberId} ";
                
                body += $"| {transaction.ServiceId} ";

                double fee = serviceDriver.get(transaction.ServiceId).Fee;
                
                body += $"| ${fee:F} |\n";
            
                numConsultations++;
                totalFee += fee;
            }

            body += "\n";

            report.Body = body;

            String footer = "";

            footer += $"## Totals\n\n";
            footer += $"| Members Consulted | Fee |\n";
            footer += $"| ---------- | ---------- |\n";
            footer += $"| {numConsultations} | ${totalFee:F} |\n";

            report.Footer = footer;

            DateTime date = DateTime.Now;
            
            reportDriver.print($"{provider.Name.Replace(" ", "_")}_{date.Month}-{date.Day}-{date.Year}", "md",  report);
        }
    }
}