using System;

namespace Chocoholics_Anonymous.CA_Models
{
    public class Provider
    {
        private int provider_Id;
        private String name;
        private String address;
        private String city;
        private String state;
        private String zip;
        private bool isDeleted;

        public Provider()
        {
        }

        public Provider(int providerId, string name, string address, string city, string state, string zip, bool isDeleted)
        {
            provider_Id = providerId;
            this.name = name;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.isDeleted = isDeleted;
        }

        public int ProviderId
        {
            get => provider_Id;
            set => provider_Id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Address
        {
            get => address;
            set => address = value;
        }

        public string City
        {
            get => city;
            set => city = value;
        }

        public string State
        {
            get => state;
            set => state = value;
        }

        public string Zip
        {
            get => zip;
            set => zip = value;
        }

        public bool IsDeleted
        {
            get => isDeleted;
            set => isDeleted = value;
        }
    }
}