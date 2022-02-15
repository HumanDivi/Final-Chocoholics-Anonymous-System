using System;

namespace Chocoholics_Anonymous.CA_Models
{
    public class Member
    {
        private int member_Id;
        private String name;
        private String address;
        private String city;
        private String state;
        private String zip;
        private bool isSuspended;
        private bool isDeleted;

        public Member()
        {
        }

        public Member(int memberId, string name, string address, string city, string state, string zip, bool isSuspended, bool isDeleted)
        {
            member_Id = memberId;
            this.name = name;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.isSuspended = isSuspended;
            this.isDeleted = isDeleted;
        }

        public int MemberId
        {
            get => member_Id;
            set => member_Id = value;
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

        public bool IsSuspended
        {
            get => isSuspended;
            set => isSuspended = value;
        }

        public bool IsDeleted
        {
            get => isDeleted;
            set => isDeleted = value;
        }
    }
}