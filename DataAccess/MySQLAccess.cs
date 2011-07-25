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


        #region MasterInterface_Methods

        #region getHolidays
        // This method will output the Holiday data in a Dataset
        public DataSet getHolidays(string theConnectionString)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getHolidays";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsHolidays = new DataSet();
                    myAdapter.Fill(dsHolidays);

                    return dsHolidays;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region getHolidayByID
        // This method will output the Holiday data in a Dataset
        public DataSet getHolidayByID(string theConnectionString, int intHolidayID)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getHolidayByID";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("iHolidayID", MySqlDbType.Int32);
                    myCommand.Parameters[0].Value = intHolidayID;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsSelectedHoliday = new DataSet();
                    myAdapter.Fill(dsSelectedHoliday);

                    return dsSelectedHoliday;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region updateHolidayDetails
        public Int32 updateHolidayDetails(string theConnectionString, int intHolidayID, string strState, string strMonth, string strDay, string strHolidayName, string strNotes, string strDate)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_updHolidays";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;

                    // input parameters for stored procedure
                    MySqlParameter myParamHoliday = new MySqlParameter();
                    myParamHoliday.ParameterName = "intHolidayId";
                    myParamHoliday.Value = intHolidayID;
                    myCommand.Parameters.Add(myParamHoliday);
                    myParamHoliday.Direction = ParameterDirection.Input;

                    MySqlParameter myParamState = new MySqlParameter();
                    myParamState.ParameterName = "strState";
                    myParamState.Value = strState;
                    myCommand.Parameters.Add(myParamState);
                    myParamState.Direction = ParameterDirection.Input;

                    MySqlParameter myParamMonth = new MySqlParameter();
                    myParamMonth.ParameterName = "strMonth";
                    myParamMonth.Value = strMonth;
                    myCommand.Parameters.Add(myParamMonth);
                    myParamMonth.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDay = new MySqlParameter();
                    myParamDay.ParameterName = "strDay";
                    myParamDay.Value = strDay;
                    myCommand.Parameters.Add(myParamDay);
                    myParamDay.Direction = ParameterDirection.Input;

                    MySqlParameter myParamHolName = new MySqlParameter();
                    myParamHolName.ParameterName = "strHolidayName";
                    myParamHolName.Value = strHolidayName;
                    myCommand.Parameters.Add(myParamHolName);
                    myParamHolName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotes = new MySqlParameter();
                    myParamNotes.ParameterName = "strNotes";
                    myParamNotes.Value = strNotes;
                    myCommand.Parameters.Add(myParamNotes);
                    myParamNotes.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDate = new MySqlParameter();
                    myParamDate.ParameterName = "strTheDate";
                    myParamDate.Value = strDate;
                    myCommand.Parameters.Add(myParamDate);
                    myParamDate.Direction = ParameterDirection.Input;

                    intRecAffected = myCommand.ExecuteNonQuery();

                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region insertHoliday

        public Int32 insertHoliday(string theConnectionString, string strState, string strMonth, string strDay, string strHolidayName, string strNotes, string strDate)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_insHoliday";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamState = new MySqlParameter();
                    myParamState.ParameterName = "strState";
                    myParamState.Value = strState;
                    myCommand.Parameters.Add(myParamState);
                    myParamState.Direction = ParameterDirection.Input;

                    MySqlParameter myParamMonth = new MySqlParameter();
                    myParamMonth.ParameterName = "strMonth";
                    myParamMonth.Value = strMonth;
                    myCommand.Parameters.Add(myParamMonth);
                    myParamMonth.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDay = new MySqlParameter();
                    myParamDay.ParameterName = "strDay";
                    myParamDay.Value = strDay;
                    myCommand.Parameters.Add(myParamDay);
                    myParamDay.Direction = ParameterDirection.Input;

                    MySqlParameter myParamHolName = new MySqlParameter();
                    myParamHolName.ParameterName = "strHolidayName";
                    myParamHolName.Value = strHolidayName;
                    myCommand.Parameters.Add(myParamHolName);
                    myParamHolName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotes = new MySqlParameter();
                    myParamNotes.ParameterName = "strNotes";
                    myParamNotes.Value = strNotes;
                    myCommand.Parameters.Add(myParamNotes);
                    myParamNotes.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDate = new MySqlParameter();
                    myParamDate.ParameterName = "strTheDate";
                    //myParamDate.Value = DateTime.ParseExact(strDate,"yyyy-MM-dd",null);
                    myParamDate.Value = strDate;
                    myCommand.Parameters.Add(myParamDate);
                    myParamDate.Direction = ParameterDirection.Input;

                    intRecAffected = myCommand.ExecuteNonQuery();

                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region getDialerRules
        // This method will fetch the DialerRules details from the database...
        public DataSet getDialerRules(string theConnectionString)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getDialerRules";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsDialerRules = new DataSet();
                    myAdapter.Fill(dsDialerRules);

                    return dsDialerRules;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region getDialerRulesByID
        // This method will output the Holiday data in a Dataset
        public DataSet getDialerRulesByID(string theConnectionString, int intDialerRulesID)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getDialerRulesByID";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("iDialerRulesId", MySqlDbType.Int32);
                    myCommand.Parameters[0].Value = intDialerRulesID;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsSelectedDialerRules = new DataSet();
                    myAdapter.Fill(dsSelectedDialerRules);

                    return dsSelectedDialerRules;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region updateDialerRules

        public Int32 updateDialerRules(string theConnectionString, int intDialerRulesID, int intCampaignID, string strCampaignName, string strPhoneType, int intDaysBetweenDials,
                                        int intHoursBetweenDials, int intMinutesBetweenDials, int intMaxAttempts, int intMaxDaysInPool, string strStartDialTime_EST,
                                        string strEndDialTime_EST, int intDialOnHolidays, int intDialActive, int intDialPriority, int intArchiveAfterDays)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_updDialerRules";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;

                    // input parameters for stored procedure
                    MySqlParameter myParamDialerRules = new MySqlParameter();
                    myParamDialerRules.ParameterName = "iDialerRulesID";
                    myParamDialerRules.Value = intDialerRulesID;
                    myCommand.Parameters.Add(myParamDialerRules);
                    myParamDialerRules.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCampaignId = new MySqlParameter();
                    myParamCampaignId.ParameterName = "iCampaignID";
                    myParamCampaignId.Value = intCampaignID;
                    myCommand.Parameters.Add(myParamCampaignId);
                    myParamCampaignId.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCampaignName = new MySqlParameter();
                    myParamCampaignName.ParameterName = "strCampaignName";
                    myParamCampaignName.Value = strCampaignName;
                    myCommand.Parameters.Add(myParamCampaignName);
                    myParamCampaignName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamPhoneType = new MySqlParameter();
                    myParamPhoneType.ParameterName = "strPhoneType";
                    myParamPhoneType.Value = strPhoneType;
                    myCommand.Parameters.Add(myParamPhoneType);
                    myParamPhoneType.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDaysBetDials = new MySqlParameter();
                    myParamDaysBetDials.ParameterName = "iDaysBetweenDials";
                    myParamDaysBetDials.Value = intDaysBetweenDials;
                    myCommand.Parameters.Add(myParamDaysBetDials);
                    myParamDaysBetDials.Direction = ParameterDirection.Input;

                    MySqlParameter myParamhrsBetDials = new MySqlParameter();
                    myParamhrsBetDials.ParameterName = "iHoursBetweenDials";
                    myParamhrsBetDials.Value = intHoursBetweenDials;
                    myCommand.Parameters.Add(myParamhrsBetDials);
                    myParamhrsBetDials.Direction = ParameterDirection.Input;

                    MySqlParameter myParamMinsBetDials = new MySqlParameter();
                    myParamMinsBetDials.ParameterName = "iMinutesBetweenDials";
                    myParamMinsBetDials.Value = intMinutesBetweenDials;
                    myCommand.Parameters.Add(myParamMinsBetDials);
                    myParamMinsBetDials.Direction = ParameterDirection.Input;

                    MySqlParameter myParamMaxAttempts = new MySqlParameter();
                    myParamMaxAttempts.ParameterName = "iMaxAttempts";
                    myParamMaxAttempts.Value = intMaxAttempts;
                    myCommand.Parameters.Add(myParamMaxAttempts);
                    myParamMaxAttempts.Direction = ParameterDirection.Input;

                    MySqlParameter myParamMaxDaysInPool = new MySqlParameter();
                    myParamMaxDaysInPool.ParameterName = "iMaxDaysInPool";
                    myParamMaxDaysInPool.Value = intMaxDaysInPool;
                    myCommand.Parameters.Add(myParamMaxDaysInPool);
                    myParamMaxDaysInPool.Direction = ParameterDirection.Input;

                    MySqlParameter myParamStartDialTime = new MySqlParameter();
                    myParamStartDialTime.ParameterName = "strStartDialTime_EST";
                    myParamStartDialTime.Value = strStartDialTime_EST;
                    myCommand.Parameters.Add(myParamStartDialTime);
                    myParamStartDialTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamEndDialTime = new MySqlParameter();
                    myParamEndDialTime.ParameterName = "strEndDialTime_EST";
                    myParamEndDialTime.Value = strEndDialTime_EST;
                    myCommand.Parameters.Add(myParamEndDialTime);
                    myParamEndDialTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDialHolidays = new MySqlParameter();
                    myParamDialHolidays.ParameterName = "iDialOnHolidays";
                    myParamDialHolidays.Value = intDialOnHolidays;
                    myCommand.Parameters.Add(myParamDialHolidays);
                    myParamDialHolidays.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDialActive = new MySqlParameter();
                    myParamDialActive.ParameterName = "iDialActive";
                    myParamDialActive.Value = intDialActive;
                    myCommand.Parameters.Add(myParamDialActive);
                    myParamDialActive.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDialPriority = new MySqlParameter();
                    myParamDialPriority.ParameterName = "iDialPriority";
                    myParamDialPriority.Value = intDialPriority;
                    myCommand.Parameters.Add(myParamDialPriority);
                    myParamDialPriority.Direction = ParameterDirection.Input;

                    MySqlParameter myParamArchiveAfterDays = new MySqlParameter();
                    myParamArchiveAfterDays.ParameterName = "iArchiveAfterDays";
                    myParamArchiveAfterDays.Value = intArchiveAfterDays;
                    myCommand.Parameters.Add(myParamArchiveAfterDays);
                    myParamArchiveAfterDays.Direction = ParameterDirection.Input;

                    intRecAffected = myCommand.ExecuteNonQuery();

                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion


        #region getUsStates
        // This method will fetch all the US States from tbl_us_states table..
        public DataSet getUsStates(string theConnectionString)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getUsStates";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myDataAdapter = new MySqlDataAdapter();
                    myDataAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsStates = new DataSet();
                    myDataAdapter.Fill(dsStates);

                    return dsStates;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion


        #endregion


       

    
    
    
    }



}