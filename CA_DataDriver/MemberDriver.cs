using System;
using System.Collections;
using System.Collections.Generic;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_DataDriver
{
    public class MemberDriver
    {
        private LinkedList<Member> dataBase;

        public MemberDriver(LinkedList<Member> dataBase)
        {
            this.dataBase = dataBase;
        }

        public void add(Member member)
        {
            dataBase.AddLast(member);
        }

        public Member get(int id)
        {
            foreach (var member in dataBase)
            {
                if (member.MemberId == id) return member;
            }

            return null;
        }
        
        public Member[] getAll()
        {
            Member[] members = new Member[dataBase.Count];
            
            int count = 0;
            foreach (var member in dataBase)
            {
                members[count++] = member;
            }

            return members;
        }
        
        public String list()
        {
            String list = "";
            
            foreach (var member in dataBase)
            {
                if (!member.IsDeleted)
                {
                    String status = member.IsSuspended ? "Suspended" : "Active";
                    list += $"{member.MemberId.ToString()} -- {member.Name} -- {status}\n";
                }
            }

            return list;
        }
    }
}