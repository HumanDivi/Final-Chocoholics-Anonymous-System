using System;

namespace Chocoholics_Anonymous.CA_Models
{
    public class Transaction
    {
        private int transaction_Id;
        private DateTime currentDateAndTime;
        private DateTime dateOfService;
        private int provider_Id;
        private int member_Id;
        private int service_Id;
        private String comments;

        public Transaction()
        {
        }

        public Transaction(int transactionId, DateTime currentDateAndTime, DateTime dateOfService, int providerId, int memberId, int serviceId, string comments)
        {
            transaction_Id = transactionId;
            this.currentDateAndTime = currentDateAndTime;
            this.dateOfService = dateOfService;
            provider_Id = providerId;
            member_Id = memberId;
            service_Id = serviceId;
            this.comments = comments;
        }

        public int TransactionId
        {
            get => transaction_Id;
            set => transaction_Id = value;
        }

        public DateTime CurrentDateAndTime
        {
            get => currentDateAndTime;
            set => currentDateAndTime = value;
        }

        public DateTime DateOfService
        {
            get => dateOfService;
            set => dateOfService = value;
        }
      
        public int ProviderId
        {
            get => provider_Id;
            set => provider_Id = value;
        }

        public int MemberId
        {
            get => member_Id;
            set => member_Id = value;
        }

        public int ServiceId
        {
            get => service_Id;
            set => service_Id = value;
        }

        public string Comments
        {
            get => comments;
            set => comments = value;
        }
    }
}