using System;
using System.Collections.Generic;
using Chocoholics_Anonymous.CA_DataDriver;
using Chocoholics_Anonymous.CA_Helpers;
using Chocoholics_Anonymous.CA_Models;
using Chocoholics_Anonymous.CA_ServiceManagers;
using Chocoholics_Anonymous.CA_ServiceManagers.MemberManager;
using Chocoholics_Anonymous.CA_ServiceManagers.ProviderManager;
using Chocoholics_Anonymous.CA_ServiceManagers.ServiceManager;
using Chocoholics_Anonymous.CA_ServiceManagers.TransactionManager;

namespace Chocoholics_Anonymous.CA_System
{
    public class System
    {
        private MemberManager memberManager;
        private ProviderManager providerManager;
        private ServiceManager serviceManager;
        private ReportManager reportManager;
        private TransactionManager transactionManager;

        public System()
        {
            // Init the data storage solution
            LinkedList<Member> memberList = new LinkedList<Member>();
            LinkedList<Provider> providerList = new LinkedList<Provider>();
            LinkedList<Service> serviceList = new LinkedList<Service>();
            LinkedList<Transaction> transactionList = new LinkedList<Transaction>();

            // Init the data driver
            MemberDriver memberDriver = new MemberDriver(memberList);
            ProviderDriver providerDriver = new ProviderDriver(providerList);
            ServiceDriver serviceDriver = new ServiceDriver(serviceList);
            ReportDriver reportDriver = new ReportDriver();
            TransactionDriver transactionDriver = new TransactionDriver(transactionList);

            // Init the service managers
            MemberManager memberManager = new MemberManager(memberDriver);
            ProviderManager providerManager = new ProviderManager(providerDriver);
            ServiceManager serviceManager = new ServiceManager(serviceDriver, providerDriver);
            ReportManager reportManager = new ReportManager(reportDriver, memberDriver, providerDriver, serviceDriver,
                transactionDriver);
            TransactionManager transactionManager = new TransactionManager(transactionDriver, memberDriver, serviceDriver, providerDriver);

            Loader loader = new Loader(memberDriver, providerDriver, serviceDriver, transactionDriver);
            loader.load();

            this.memberManager = memberManager;
            this.providerManager = providerManager;
            this.serviceManager = serviceManager;
            this.reportManager = reportManager;
            this.transactionManager = transactionManager;
        }

        public void userLogin()
        {
        }

        public void launchTerminal()
        {
            bool systemIsRunning = true;
            String cmdString = "";
            String[] cmdArgs = { };

            while (systemIsRunning)
            {
                try
                {
                    Console.WriteLine("Enter Command");
                    Console.Write("> ");
                    cmdString = Console.ReadLine();
                    cmdArgs = cmdString.ToLower().Split(' ');

                    if (cmdArgs[0] == "exit") return;

                    runServiceCommand(cmdArgs);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void runServiceCommand(String[] cmdArr)
        {
            switch (cmdArr[0])
            {
                case "transaction":
                    transactionManager.createTransaction();
                    return;
            }
            switch (cmdArr[1])
            {
                case "member":
                    switch (cmdArr[0])
                    {
                        case "add":
                            memberManager.addMember();
                            break;
                        case "edit":
                            memberManager.editMember();
                            break;
                        case "remove":
                            memberManager.removeMember();
                            break;
                        case "suspend":
                            memberManager.suspendMember();
                            break;
                        case "list":
                            memberManager.listMembers();
                            break;
                    }

                    break;
                case "provider":
                    switch (cmdArr[0])
                    {
                        case "add":
                            providerManager.addProvider();
                            break;
                        case "edit":
                            providerManager.editProvider();
                            break;
                        case "remove":
                            providerManager.removeProvider();
                            break;
                        case "list":
                            providerManager.listProviders();
                            break;
                    }

                    break;
                case "service":
                    switch (cmdArr[0])
                    {
                        case "add":
                            serviceManager.addService();
                            break;
                        case "edit":
                            serviceManager.editService();
                            break;
                        case "list":
                            serviceManager.listServices();
                            break;
                    }

                    break;
                case "report":
                    switch (cmdArr[0])
                    {
                        case "all":
                            reportManager.generateSummaryReport();
                            reportManager.generateMembersReports();
                            reportManager.generateProvidersReports();
                            reportManager.generateEFTReport();
                            break;
                        case "summary":
                            reportManager.generateSummaryReport();
                            break;
                        case "members":
                            reportManager.generateMembersReports();
                            break;
                        case "providers":
                            reportManager.generateProvidersReports();
                            break;
                        case "eft":
                            reportManager.generateEFTReport();
                            break;
                    }

                    break;
            }
        }

        public void runAutoTask(String[] cmdArr)
        {
            switch (cmdArr[1])
            {
                case "member":
                    switch (cmdArr[0])
                    {
                        case "suspend":
                            memberManager.suspendMember(int.Parse(cmdArr[2]));
                            break;
                    }
                    
                    break;
                case "report":
                    switch (cmdArr[0])
                    {
                        case "all":
                            reportManager.generateSummaryReport();
                            reportManager.generateMembersReports();
                            reportManager.generateProvidersReports();
                            reportManager.generateEFTReport();
                            break;
                        case "summary":
                            reportManager.generateSummaryReport();
                            break;
                        case "members":
                            reportManager.generateMembersReports();
                            break;
                        case "providers":
                            reportManager.generateProvidersReports();
                            break;
                        case "EFT":
                            reportManager.generateEFTReport();
                            break;
                    }

                    break;
            }
        }
    }
}