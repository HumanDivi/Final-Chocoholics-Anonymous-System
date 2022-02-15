using System;
using Chocoholics_Anonymous.CA_DataDriver;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_ServiceManagers.ServiceManager
{
    public class ServiceManager
    {
        private ServiceDriver serviceDriver;
        private ProviderDriver providerDriver;

        public ServiceManager(ServiceDriver serviceDriver, ProviderDriver providerDriver)
        {
            this.serviceDriver = serviceDriver;
            this.providerDriver = providerDriver;
        }

        public void addService()
        {
            Service service = promptForService();
            service.ServiceId = generateID();

            if(providerDriver.get(service.ProviderId) != null)
                serviceDriver.add(service);
        }

        public void editService()
        {
            int id = promptForID();
            Service oldService = serviceDriver.get(id);
            Service newService = promptForService();

            oldService.Name = newService.Name;
            oldService.Fee = newService.Fee;
        }

        public void listServices()
        {
            Console.WriteLine(serviceDriver.list());
        }

        private Service promptForService()
        {
            Service service = new Service();
            Console.Write("Name: ");
            service.Name = Console.ReadLine();
            Console.Write("Provider ID: ");
            service.ProviderId = int.Parse(Console.ReadLine());
            Console.Write("Fee: ");
            service.Fee = int.Parse(Console.ReadLine());
            return service;
        }
        
        private int promptForID()
        {
            Console.Write("Enter ID: ");
            return int.Parse(Console.ReadLine());
        }

        private int generateID()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
    }
}