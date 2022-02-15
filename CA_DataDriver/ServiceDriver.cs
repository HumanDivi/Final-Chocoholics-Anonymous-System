using System;
using System.Collections.Generic;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_DataDriver
{
    public class ServiceDriver
    {
        private LinkedList<Service> dataBase;

        public ServiceDriver(LinkedList<Service> dataBase)
        {
            this.dataBase = dataBase;
        }

        public void add(Service service)
        {
            dataBase.AddLast(service);
        }

        public Service get(int id)
        {
            foreach (var service in dataBase)
            {
                if (service.ServiceId == id) return service;
            }

            return null;
        }
        
        public String list()
        {
            String list = "";
            
            foreach (var service in dataBase)
            {
                list += $"{service.ServiceId} -- {service.Name}\n";
            }

            return list;
        }
        
        public String list(int provider_Id)
        {
            String list = "";
            
            foreach (var service in dataBase)
            {
                if(service.ProviderId == provider_Id)
                    list += $"{service.ServiceId} -- {service.Name}\n";
            }

            return list;
        }
    }
}