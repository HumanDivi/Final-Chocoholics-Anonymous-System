using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_DataDriver
{
    public class TransactionDriver
    {
        // private SqlCommands sqlInstance = new SqlCommands();
        private LinkedList<Transaction> dataBase;

        public TransactionDriver(LinkedList<Transaction> dataBase)
        {
            this.dataBase = dataBase;
        }
 
        public Transaction get(int id)
        {
            foreach (var transaction in dataBase)
            {
                if (transaction.TransactionId == id) return transaction;
            }

            return null;
        }

        public void add(Transaction transaction)
        {
            dataBase.AddLast(transaction);
        }
        
        public String list()
        {
            String list = "";
            foreach (var transaction in dataBase)
            {
                list += $"{transaction.TransactionId} -- {transaction.MemberId} -- {transaction.ProviderId} -- {transaction.ServiceId}\n";
            }
            return list;
        }
        
        public Transaction[] getAll()
        {
            Transaction[] transactions = new Transaction[dataBase.Count];
            
            int count = 0;
            foreach (var transaction in dataBase)
            {
                transactions[count++] = transaction;
            }

            return transactions;
        }

    }

}

