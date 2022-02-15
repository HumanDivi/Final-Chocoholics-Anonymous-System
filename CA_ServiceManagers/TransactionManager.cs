using System;
using Chocoholics_Anonymous.CA_DataDriver;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_ServiceManagers.TransactionManager
{
    public class TransactionManager
    {
        private TransactionDriver transactionDriver;
        private MemberDriver memberDriver;
        private ServiceDriver serviceDriver;
        private ProviderDriver providerDriver;

        public TransactionManager(TransactionDriver transactionDriver, MemberDriver memberDriver, ServiceDriver serviceDriver, ProviderDriver providerDriver)
        {
            this.transactionDriver = transactionDriver;
            this.memberDriver = memberDriver;
            this.serviceDriver = serviceDriver;
            this.providerDriver = providerDriver;
        }
        public void createTransaction()
        {
            Console.Write("Enter Provider ID: ");
            int ProviderId = int.Parse(Console.ReadLine());

            if (providerDriver.get(ProviderId) == null)
            {
                Console.WriteLine("Invalid Provider Number");
                return;
            }
            
            Console.Write("Enter Member ID: ");
            int MemberId = int.Parse(Console.ReadLine());

            Member member = memberDriver.get(MemberId);
            
            if (member == null)
            {
                Console.WriteLine("Invalid Member Number");
                return;
            }
            
            if (member.IsSuspended)
            {
                Console.WriteLine("Member Suspended");
                Console.WriteLine("Fees owed: $15.00");
                return;
            }
            
            Console.WriteLine("Validated");
            
            Console.Write("Date of Service: ");
            DateTime DateOfSerice = DateTime.Parse(Console.ReadLine());

            Console.Write("Service ID: ");
            int ServiceId = int.Parse(Console.ReadLine());

            Service service = serviceDriver.get(ServiceId);
            
            if (service == null || service.ProviderId != ProviderId)
            {
                Console.WriteLine("Error, could not find service");
                return;
            }

            Console.WriteLine(service.Name);
            Console.Write("is this correct? (y/n) ");
            String input = Console.ReadLine();

            if (input != "y")
            {
                return;
            }
            
            Console.Write("Enter Comments: ");
            String Comments = Console.ReadLine();
            
            Transaction transaction = new Transaction(generateID(), DateTime.Now, DateOfSerice, ProviderId, MemberId, ServiceId, Comments);
            
            Console.WriteLine($"Fee to be paid: ${service.Fee:F}");
            
            transactionDriver.add(transaction);
        }

        private int generateID()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999);
        }
    }

}