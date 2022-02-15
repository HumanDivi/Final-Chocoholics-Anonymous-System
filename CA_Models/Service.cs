using System;

namespace Chocoholics_Anonymous.CA_Models
{
    public class Service
    {
        private int service_Id;
        private String name;
        private int provider_Id;
        private int fee;

        public Service()
        {
        }

        public Service(int serviceId, string name, int providerId, int fee)
        {
            service_Id = serviceId;
            this.name = name;
            provider_Id = providerId;
            this.fee = fee;
        }

        public int ServiceId
        {
            get => service_Id;
            set => service_Id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int ProviderId
        {
            get => provider_Id;
            set => provider_Id = value;
        }

        public int Fee
        {
            get => fee;
            set => fee = value;
        }
    }
}