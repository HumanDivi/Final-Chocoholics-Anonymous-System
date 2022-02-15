using System;
using Chocoholics_Anonymous.CA_DataDriver;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_ServiceManagers.ProviderManager
{
    public class ProviderManager
    {
        private ProviderDriver providerDriver;

        public ProviderManager(ProviderDriver providerDriver)
        {
            this.providerDriver = providerDriver;
        }

        public void addProvider()
        {
            Provider provider = promptForProvider();
            provider.ProviderId = generateID();
            provider.IsDeleted = false;

            providerDriver.add(provider);
        }

        public void editProvider()
        {
            int id = promptForID();
            Provider oldProvider = providerDriver.get(id);
            Provider newProvider = promptForProvider();

            oldProvider.Name = newProvider.Name;
            oldProvider.Address = newProvider.Address;
            oldProvider.City = newProvider.City;
            oldProvider.State = newProvider.State;
            oldProvider.Zip = newProvider.Zip;
        }

        public void removeProvider()
        {
            int id = promptForID();
            Provider provider = providerDriver.get(id);
            
            provider.IsDeleted = true;
        }

        public void listProviders()
        {
            Console.WriteLine(providerDriver.list());
        }

        private Provider promptForProvider()
        {
            Provider provider = new Provider();
            Console.Write("Name: ");
            provider.Name = Console.ReadLine();
            Console.Write("Street Address: ");
            provider.Address = Console.ReadLine();
            Console.Write("City: ");
            provider.City = Console.ReadLine();
            Console.Write("State: ");
            provider.City = Console.ReadLine();
            Console.Write("Zip: ");
            provider.State = Console.ReadLine();
            return provider;
        }
        
        private int promptForID()
        {
            Console.Write("Enter ID: ");
            return int.Parse(Console.ReadLine());
        }

        private int generateID()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999);
        }
    }
}