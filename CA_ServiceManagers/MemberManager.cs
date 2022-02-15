using System;
using Chocoholics_Anonymous.CA_DataDriver;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_ServiceManagers.MemberManager
{
    public class MemberManager
    {
        private MemberDriver memberDriver;

        public MemberManager(MemberDriver memberDriver)
        {
            this.memberDriver = memberDriver;
        }

        public void addMember()
        {
            Member member = promptForMember();
            member.MemberId = generateID();
            member.IsDeleted = false;
            member.IsSuspended = false;

            memberDriver.add(member);
        }

        public void editMember()
        {
            int id = promptForID();
            Member oldMember = memberDriver.get(id);
            Member newMember = promptForMember();

            oldMember.Name = newMember.Name;
            oldMember.Address = newMember.Address;
            oldMember.City = newMember.City;
            oldMember.State = newMember.State;
            oldMember.Zip = newMember.Zip;
        }

        public void removeMember()
        {
            int id = promptForID();
            Member member = memberDriver.get(id);
            
            member.IsDeleted = true;
        }

        public void suspendMember()
        {
            int id = promptForID();
            
            suspendMember(id);
        }
        
        public void suspendMember(int id)
        {
            Member member = memberDriver.get(id);
            
            member.IsSuspended = true;
        }

        public void listMembers()
        {
            Console.WriteLine(memberDriver.list());
        }

        private Member promptForMember()
        {
            Member member = new Member();
            Console.Write("Name: ");
            member.Name = Console.ReadLine();
            Console.Write("Street Address: ");
            member.Address = Console.ReadLine();
            Console.Write("City: ");
            member.City = Console.ReadLine();
            Console.Write("State: ");
            member.City = Console.ReadLine();
            Console.Write("Zip: ");
            member.State = Console.ReadLine();
            return member;
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