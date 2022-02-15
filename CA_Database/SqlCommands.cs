using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_Database
{
    public class SqlCommands{
        string connectionStr = "Server=DESKTOP-BHSVNPJ;Database=ChocAn;Trusted_Connection=true";

        //Get a List of all Providers
        public DataTable getMembersList()
        {
            DataTable tblProviders;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlDataAdapter sDA = new SqlDataAdapter("SELECT * FROM Members", conn);
                DataSet ds = new DataSet();
                sDA.Fill(ds, "Members");
                tblProviders = ds.Tables["Members"];
            }
            return tblProviders;
        }

        // Get a Specific Member
        public (String, String, String, String, int, bool, bool) getMember(int memberId)
        {
            String name = "", address = "", city = "", state = "";
            int zipcode = 0;
            bool isSuspended = true, isDeleted = true;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("SELECT * FROM Members WHERE Id = " + memberId, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(1).TrimEnd();
                        address = reader.GetString(2).TrimEnd();
                        city = reader.GetString(3).TrimEnd();
                        state = reader.GetString(4).TrimEnd();
                        zipcode = reader.GetInt32(5);
                        isSuspended = reader.GetBoolean(6);
                        isDeleted = reader.GetBoolean(7);
                    }
                }
            }

            return (name, address, city, state, zipcode, isSuspended, isDeleted);
        }

        //Suspend a Certain Member
        public void suspendMember(int memberId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("UPDATE Members SET Suspended = 1 WHERE Id = " + memberId + "; ", conn); 
                command.ExecuteNonQuery();
            }
        }


        //Delete a Specific Member. Will not fully delete them bt instead mark them as deleted
        public void deleteMember(int memberId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("UPDATE Members SET Deleted = 1 WHERE Id = " + memberId + "; ", conn);
                command.ExecuteNonQuery();
            }
        }


        //Create a New Member
        public void addMember(string name, string address, string city, string state, int zipcode)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("INSERT INTO Members (Name, Address, City, State, Zipcode, Suspended, Deleted) VALUES(" + name + ", " + address + "," + city + "," + state + "," + zipcode + "," + 0 + "," + 0 + "; ", conn);
                command.ExecuteNonQuery();
            }
        }


        //Update a Member's information
        public void updateMember(int memberId, string name, string address, string city, string state, int zipcode)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("UPDATE Members SET Name = " + name + ", Address = " + address + ", City = " + city + ", State = " + state + ", Zipcode = " + zipcode + "; ", conn);
                command.ExecuteNonQuery();
            }
        }

        //Get a Specific Provider
        public (String, String, String, String, int, bool) getProvider(int providerId)
        {
            String name = "", address = "", city = "", state = "";
            int zipcode = 0;
            bool isDeleted = true;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("SELECT * FROM providers WHERE Id = " + providerId, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(1).TrimEnd();
                        address = reader.GetString(2).TrimEnd();
                        city = reader.GetString(3).TrimEnd();
                        state = reader.GetString(4).TrimEnd();
                        zipcode = reader.GetInt32(5);
                        isDeleted = reader.GetBoolean(6);
                    }
                }
            }

            return (name, address, city, state, zipcode, isDeleted);
        }

        //Get a List of all Providers
        public DataTable getProviderList()
        {
            DataTable tblProviders;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlDataAdapter sDA = new SqlDataAdapter("SELECT * FROM Providers", conn);
                DataSet ds = new DataSet();
                sDA.Fill(ds, "Providers");
                tblProviders = ds.Tables["Providers"];
            }

            return tblProviders;
        }


        //Delete a Certain Provider. Will delete them fully from the table
        public void deleteProvider(int providerId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("UPDATE Providers SET Deleted = 1 WHERE Id = " + providerId + "; ", conn);
                command.ExecuteNonQuery();
            }
        }

        //Add a new Provider
        public void addProvider(string name, string address, string city, string state, int zipcode)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("INSERT INTO Providers (Name, Address, City, State, Zipcode, Deleted) VALUES(" + name + ", " + address + "," + city + "," + state + "," + zipcode + "," + 0 + "; ", conn);
                command.ExecuteNonQuery();
            }
        }

        //Update a Certain Provider's info
        public void updateProvider(int providerId, string name, string address, string city, string state, int zipcode)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("UPDATE Providers SET Name = " + name + ", Address = " + address + ", City = " + city + ", State = " + state + ", Zipcode = " + zipcode + " WHERE Id = "  + providerId + "; ", conn);
                command.ExecuteNonQuery();
            }
        }


        //Get a service's information
        public (string, decimal, int) getServiceInfo(int serviceId)
        {
            string name = "";
            decimal fee = 0.00m;
            int providerId = 0;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("SELECT * FROM Services WHERE Id = " + serviceId, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(1).TrimEnd();
                        fee = reader.GetSqlMoney(2).Value;
                        providerId = reader.GetInt32(3);
                    }
                }
            }

            return (name, fee, providerId);
        }


        //Delete a service
        public void deleteService(int serviceId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("DELETE FROM Services WHERE Id = ;" + serviceId + "; ", conn);
                command.ExecuteNonQuery();
            }
        }

        //Add a service
        public void addService(string name, decimal fee, int providerId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("INSERT INTO Services (Name, Fee, provider_Id) VALUES(" + name + ", " + fee + "," + providerId + "; ", conn);
                command.ExecuteNonQuery();
            }
        }


        //Update a Certain Service
        public void updateServices(int serviceId, string name, decimal fee, int providerId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("UPDATE Services SET Name = " + name + ", Fee = " + fee + ", provider_Id = " + providerId + "WHERE Id = " + serviceId + "; ", conn);
                command.ExecuteNonQuery();
            }
        }


        //Get the full list of services provided to a certain member
        public DataTable getMemberServices(int memberId)
        {
            DataTable tblProviders;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlDataAdapter sDA = new SqlDataAdapter("SELECT * FROM ServicesProvided WHERE Member_Id = " + memberId, conn);
                DataSet ds = new DataSet();
                sDA.Fill(ds, "ServicesProvided");
                tblProviders = ds.Tables["ServicesProvided"];
            }

            return tblProviders;
        }


        //Delete a provided service
        public void deleteProvidedService(int serviceProvidedId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("DELETE FROM ServicesProvided WHERE Id = ;" + serviceProvidedId + "; ", conn);
                command.ExecuteNonQuery();
            }
        }


        //Add a new service that has been provided
        public void addProvidedService(DateTime dateOfService, DateTime dateTimeReceived, int serviceId, int memberId,
            int providerId, string comments = "")
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("INSERT INTO ServicesProvided (DateofService, DateTimeReceived, Service_Id, Member_Id, Provider_Id, Comments) VALUES(" + dateOfService + ", " + dateTimeReceived + "," + serviceId + ", " + memberId + ", " + providerId + ", " + comments + "; ", conn);
                command.ExecuteNonQuery();
            }
        }

        //Update the provided service. Date and times cannot change here
        public void updateProvidedServices(int serviceProvidedId, int serviceId, int memberId, int providerId,
            string comments = "")
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlCommand command = new SqlCommand("UPDATE ServicesProvided SET Service_Id = " + serviceId + ", Member_Id = " + memberId + ", provider_Id = " + providerId + ", comments = " + comments + "WHERE Id = " + serviceProvidedId + "; ", conn);
                command.ExecuteNonQuery();
            }
        }

        //Get all the services provided by a certain provider
        public DataTable getProviderServices(int providerId)
        {
            DataTable tblProviderServices;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionStr;
                SqlDataAdapter sDA = new SqlDataAdapter("SELECT * FROM ServicesProvided WHERE Provider_Id = " + providerId, conn);
                DataSet ds = new DataSet();
                sDA.Fill(ds, "ServicesProvided");
                tblProviderServices = ds.Tables["ServicesProvided"];
            }

            return tblProviderServices;
        }
    }
}
