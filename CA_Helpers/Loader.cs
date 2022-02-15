using System;
using System.Collections.Generic;
using Chocoholics_Anonymous.CA_DataDriver;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_Helpers
{
    public class Loader
    {
        private MemberDriver memberDriver;
        private ProviderDriver providerDriver;
        private ServiceDriver serviceDriver;
        private TransactionDriver transactionDriver;

        public Loader(MemberDriver memberDriver, ProviderDriver providerDriver, ServiceDriver serviceDriver, TransactionDriver transactionDriver)
        {
            this.memberDriver = memberDriver;
            this.providerDriver = providerDriver;
            this.serviceDriver = serviceDriver;
            this.transactionDriver = transactionDriver;
        }

        public void load()
        {
            int TrentonId = 123456789;
            int BrandonId = generateID9();
            int SamId = generateID9();
            int DivisId = generateID9();
            
            int SpaPlusId = generateID9();
            int ChippewaGolfCourseId = generateID9();
            int TopGolfId = generateID9();
            int PlanetInShapeId = generateID9();
            
            int NineHolesId = generateID6();
            int EighteenHolesId = generateID6();
            int MessageId = generateID6();
            int OneHrResId = generateID6();
            
            LinkedList<Member> memberList = new LinkedList<Member>();
            memberList.AddLast(new Member(TrentonId, "Trenton Letz", "18261 Edwards Road", "Doylestown", "OH", "44230", false, false));
            memberList.AddLast(new Member(BrandonId, "Brandon Bauer", "39865 Dummy Street", "Doylestown", "OH", "44230", false, false));
            memberList.AddLast(new Member(SamId, "Samantha Bowman", "1800 Alliance Way", "Alliance", "OH", "48653", false, false));
            memberList.AddLast(new Member(DivisId, "Divi", "98521 Park Drive", "Toledo", "OH", "43606", false, false));

            foreach (var member in memberList) memberDriver.add(member);

            LinkedList<Provider> providerList =  new LinkedList<Provider>();
            providerList.AddLast(new Provider(SpaPlusId, "Spa Plus", "18261 Edwards Road", "Doylestown", "OH", "44230", false));
            providerList.AddLast(new Provider(ChippewaGolfCourseId, "Chippewa Golf Course", "39865 Dummy Street", "Doylestown", "OH", "44230", false));
            providerList.AddLast(new Provider(TopGolfId, "Top Golf", "1800 Alliance Way", "Alliance", "OH", "48653", false));
            providerList.AddLast(new Provider(PlanetInShapeId, "Planet In-Shape", "98521 Park Drive", "Toledo", "OH", "43606", false));
            
            foreach (var provider in providerList) providerDriver.add(provider);
            
            LinkedList<Service> serviceList =  new LinkedList<Service>();
            serviceList.AddLast(new Service(NineHolesId, "9 Holes", ChippewaGolfCourseId, 1000));
            serviceList.AddLast(new Service(EighteenHolesId, "18 Holes", ChippewaGolfCourseId, 900));
            serviceList.AddLast(new Service(MessageId, "Massage", SpaPlusId, 250));
            serviceList.AddLast(new Service(OneHrResId, "1 Hour Reservation", PlanetInShapeId, 510));
            
            foreach (var service in serviceList) serviceDriver.add(service);
            
            LinkedList<Transaction> transactionList = new LinkedList<Transaction>();
            transactionList.AddLast(new Transaction(generateID9(), new DateTime(2020, 4, 6), new DateTime(2020, 4, 6), TopGolfId, TrentonId, OneHrResId, "No Comments"));
            transactionList.AddLast(new Transaction(generateID9(), new DateTime(2020, 5, 6), new DateTime(2020, 5, 6), TopGolfId, TrentonId, OneHrResId, "No Comments"));
            transactionList.AddLast(new Transaction(generateID9(), new DateTime(2020, 5, 6), new DateTime(2020, 5, 6), SpaPlusId, SamId, MessageId, "No Comments"));

            foreach (var transaction in transactionList) transactionDriver.add(transaction);
        }
        
        private int generateID9()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999);
        }
        
        private int generateID6()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
    }
}