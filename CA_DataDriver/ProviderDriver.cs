using System;
using System.Collections.Generic;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_DataDriver
{
    public class ProviderDriver
    {
        private LinkedList<Provider> dataBase;

        public ProviderDriver(LinkedList<Provider> dataBase)
        {
            this.dataBase = dataBase;
        }

        public void add(Provider provider)
        {
            dataBase.AddLast(provider);
        }

        public Provider get(int id)
        {
            foreach (var provider in dataBase)
            {
                if (provider.ProviderId == id) return provider;
            }

            return null;
        }
        
        public Provider[] getAll()
        {
            Provider[] providers = new Provider[dataBase.Count];
            
            int count = 0;
            foreach (var provider in dataBase)
            {
                providers[count++] = provider;
            }

            return providers;
        }
        
        public String list()
        {
            String list = "";
            
            foreach (var provider in dataBase)
            {
                if (!provider.IsDeleted)
                {
                    list += $"{provider.ProviderId} -- {provider.Name}\n";
                }
            }

            return list;
        }
    }
}