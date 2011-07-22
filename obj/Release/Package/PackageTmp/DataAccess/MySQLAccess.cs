using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.Mail;


namespace ACMGAdmin.DataAccess
{
    public class MySQLAccess
    {   //used as proxy to interact with MySQL database

        // Retrieves a connection string by name. Names are stored in the web.config file- and in the IIS config
        // Returns null if the name is not found.
        public string GetConnectionStringByName(string name)
         {
            //Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            //If found, return the connection string.
            if (settings != null)
            returnValue = settings.ConnectionString;

            return returnValue;
         }

        public MySqlConnection GetConnection(string connectstring)
        {
            try
            {

            MySqlConnection retCon = null;

            retCon = new MySql.Data.MySqlClient.MySqlConnection(connectstring);

            return retCon;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Int64 executeNonQuery(string theNonQuery,string theConnectStringName)
        {
            ///generic function to execute SQL command 
            Int32 recsAffected = 0;//this will hold the number of records affected by the nonQuery command
            try
            {
                //get the actual connection string stored in the applciation for this name
                string myCS = GetConnectionStringByName(theConnectStringName);

                // Create and open the connection in a using block. This
                // ensures that all resources will be closed and disposed
                // when the code exits. Use the connect string from above.
                using (MySql.Data.MySqlClient.MySqlConnection myCON = GetConnection(myCS))
                {
                    myCON.Open();
                    //string InsertTme = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    MySqlCommand myCommand = new MySqlCommand(theNonQuery, myCON);
                    recsAffected=myCommand.ExecuteNonQuery();
                    
                }

                return recsAffected;

            }
            catch (Exception e)
            {
                
                throw;
                
            }
            

        }
        public void getDataSet(string theQuery, string theConnectStringName)
        {
            //generic get data set 
             try
            {
                //get the actual connection string stored in the applciation for this name
                string myCS = GetConnectionStringByName(theConnectStringName);

                // Create and open the connection in a using block. This
                // ensures that all resources will be closed and disposed
                // when the code exits. Use the connect string from above.
                using (MySql.Data.MySqlClient.MySqlConnection myCON = GetConnection(myCS))
                {
                    myCON.Open();
                    //string InsertTme = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    MySqlCommand myCommand = new MySqlCommand(theQuery, myCON);
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}",
                            myReader[0], myReader[1], myReader[2]);
                         Console.WriteLine(myReader.GetInt32(0) + ", " + myReader.GetString(1));
                    }
                    Console.ReadLine();
                }

                

            }
             catch (Exception e)
             {

                 throw;

             }
            
        }
              

        public DataSet getCallCenters(string theConnectStringName)
        {
            //get all active call centers
            string theCommand = "getActiveCallCenters";

            try
            {
                //get the actual connection string stored in the applciation for this name
                string myCS = GetConnectionStringByName(theConnectStringName);

                // Create and open the connection in a using block. This
                // ensures that all resources will be closed and disposed
                // when the code exits. Use the connect string from above.
                using (MySql.Data.MySqlClient.MySqlConnection myCON = GetConnection(myCS))
                {
                    myCON.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommand, myCON);
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    //create data adapter and assign the command object created above
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    //create and fill a dataset from the stored procedure
                    DataSet theCallCenters = new DataSet();
                    myAdapter.Fill(theCallCenters);



                    return theCallCenters;

                //returns:CallCenterID,CallCenterName,CallCenterNotes,LDAPConnectStringName,Active

                    
                }



            }
            catch (Exception e)
            {
                //if error then return null
                return null;

            }

        }


        public string[] getExtensionAdmin(string theAgentLoginID, string theAgent, string theCompany, string theConnectStringName)
        {
            //`getPhoneExtension`(IN theAgentLoginID int,IN theAgentName varchar(45),IN theCallCenter varchar(45))
            //reserve a phone extension to use and return extension info- set session variables 
            string theCommand = "getPhoneExtensionAdmin";
            string[] theReturnValues = new string[8];
            try
            {
                //get the actual connection string stored in the applciation for this name
                string myCS = GetConnectionStringByName(theConnectStringName);

                // Create and open the connection in a using block. This
                // ensures that all resources will be closed and disposed
                // when the code exits. Use the connect string from above.
                using (MySql.Data.MySqlClient.MySqlConnection myCON = GetConnection(myCS))
                {
                    myCON.Open();
                    //string InsertTme = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    MySqlCommand myCommand = new MySqlCommand(theCommand, myCON);
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    //add input parameters for stored procedure
                    MySqlParameter myInParam1 = new MySqlParameter();
                    myInParam1.ParameterName = "theAgentLoginID";
                    myInParam1.Value = theAgentLoginID;
                    myCommand.Parameters.Add(myInParam1);
                    myInParam1.Direction = System.Data.ParameterDirection.Input;
                    //add input parameters for stored procedure
                    MySqlParameter myInParam2 = new MySqlParameter();
                    myInParam2.ParameterName = "theAgentName";
                    myInParam2.Value = theAgent;
                    myCommand.Parameters.Add(myInParam2);
                    myInParam2.Direction = System.Data.ParameterDirection.Input;
                    //add input parameters for stored procedure
                    MySqlParameter myInParam3 = new MySqlParameter();
                    myInParam3.ParameterName = "theCallCenter";
                    myInParam3.Value = theCompany;
                    myCommand.Parameters.Add(myInParam3);
                    myInParam3.Direction = System.Data.ParameterDirection.Input;

                    //execute and return record to reader
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {   //read the return record fields into the return array:
                        theReturnValues[0] = myReader["PhoneExtensionAssignmentID"].ToString();
                        theReturnValues[1] = myReader["PhoneExtensionID"].ToString();
                        theReturnValues[2] = myReader["SwitchName"].ToString();
                        theReturnValues[3] = myReader["SwitchAddress"].ToString();
                        theReturnValues[4] = myReader["SwitchPort"].ToString();
                        theReturnValues[5] = myReader["Extension"].ToString();
                        theReturnValues[6] = myReader["UserName"].ToString();
                        theReturnValues[7] = myReader["Password"].ToString();

                    }
                    return theReturnValues;
                }



            }
            catch (Exception e)
            {

                throw;

            }

        }

        public Int64 releaseExtension(string thePhoneExtensionAssignmentID, string theConnectStringName)
        {

            //updates the current Phone extension assignement record with the end time from the server (current server time)
            //this makes the extension available to be reserved by another agent
            string theCommand = "releasePhoneExtension";
            Int32 recsAffected = 0;//this will hold the number of records affected by the nonQuery command
            try
            {
                //get the actual connection string stored in the applciation for this name
                string myCS = GetConnectionStringByName(theConnectStringName);

                // Create and open the connection in a using block. This
                // ensures that all resources will be closed and disposed
                // when the code exits. Use the connect string from above.
                using (MySql.Data.MySqlClient.MySqlConnection myCON = GetConnection(myCS))
                {
                    myCON.Open();
                    //string InsertTme = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    MySqlCommand myCommand = new MySqlCommand(theCommand, myCON);
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    MySqlParameter myInParam = new MySqlParameter();
                    myInParam.ParameterName = "thePhoneExtensionAssignmentID";
                    myInParam.Value = thePhoneExtensionAssignmentID;
                    myCommand.Parameters.Add(myInParam);
                    myInParam.Direction = System.Data.ParameterDirection.Input;



                    recsAffected = myCommand.ExecuteNonQuery();


                }

                return recsAffected;

            }
            catch (Exception e)
            {

                throw;

            }


        }

       
       

    
   



    }



}
    
    
   