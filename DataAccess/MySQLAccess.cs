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

        #region Holiday Screen

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

        #region DialerRule Screen

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
        // This method will output the DialerRules data in a Dataset
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
        
        #endregion

        #region CallCenter Screen

        #region getCallCenter
        // This method will output the CallCenter data in a Dataset
        public DataSet getCallCenter(string theConnectionString)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getCallCenter";
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
                    DataSet dsCallCenter = new DataSet();
                    myAdapter.Fill(dsCallCenter);

                    return dsCallCenter;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region getCallCenterByID
        // This method will check the i/p CallCenterID in DB and if present it will output the CallCenter data in a Dataset..
        public DataSet getCallCenterByID(string theConnectionString, int intCallCenterID)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getCallCenterByID";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("iCallCenterID", MySqlDbType.Int32);
                    myCommand.Parameters[0].Value = intCallCenterID;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsSelectedCallCenter = new DataSet();
                    myAdapter.Fill(dsSelectedCallCenter);

                    return dsSelectedCallCenter;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region insertCallCenter

        public Int32 insertCallCenter(string theConnectionString, string strCallCenter, string strCallCenterNotes,
                                      string strConnectionStringName, string strActive, string strModifyUser, 
                                      string strModifyDateTime, string strScreenName, string strTableName, 
                                      string strBeforeImage, string strAfterImage)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_insCallCenter";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCallCenterCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCallCenterCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamCallCenter = new MySqlParameter();
                    myParamCallCenter.ParameterName = "strCallCenterName";
                    myParamCallCenter.Value = strCallCenter;
                    myCallCenterCommand.Parameters.Add(myParamCallCenter);
                    myParamCallCenter.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotes = new MySqlParameter();
                    myParamNotes.ParameterName = "strCallCenterNotes";
                    myParamNotes.Value = strCallCenterNotes;
                    myCallCenterCommand.Parameters.Add(myParamNotes);
                    myParamNotes.Direction = ParameterDirection.Input;

                    MySqlParameter myParamConnectString = new MySqlParameter();
                    myParamConnectString.ParameterName = "strConnectStringName";
                    myParamConnectString.Value = strConnectionStringName;
                    myCallCenterCommand.Parameters.Add(myParamConnectString);
                    myParamConnectString.Direction = ParameterDirection.Input;

                    MySqlParameter myParamActive = new MySqlParameter();
                    myParamActive.ParameterName = "iActive";
                    myParamActive.Value = Convert.ToInt32(strActive);
                    myCallCenterCommand.Parameters.Add(myParamActive);
                    myParamActive.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myCallCenterCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myCallCenterCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myCallCenterCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myCallCenterCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myCallCenterCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myCallCenterCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oCount";
                    myParamOutRes.Value = strAfterImage;
                    myCallCenterCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myCallCenterCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myCallCenterCommand.Parameters["oCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region updateCallCenterDetails
        public Int32 updateCallCenterDetails(string theConnectionString, int intCallCenterID, string strCallCenter, string strCallCenterNotes, 
                                             string strConnectionStringName, string strActive, string strModifyUser, string strModifyDateTime,
                                             string strScreenName, string strTableName, string strBeforeImage, string strAfterImage)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_updCallCenter";
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
                    MySqlParameter myParamCallCenterID = new MySqlParameter();
                    myParamCallCenterID.ParameterName = "iCallCenterId";
                    myParamCallCenterID.Value = intCallCenterID;
                    myCommand.Parameters.Add(myParamCallCenterID);
                    myParamCallCenterID.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCallCenter = new MySqlParameter();
                    myParamCallCenter.ParameterName = "strCallCenterName";
                    myParamCallCenter.Value = strCallCenter;
                    myCommand.Parameters.Add(myParamCallCenter);
                    myParamCallCenter.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotes = new MySqlParameter();
                    myParamNotes.ParameterName = "strCallCenterNotes";
                    myParamNotes.Value = strCallCenterNotes;
                    myCommand.Parameters.Add(myParamNotes);
                    myParamNotes.Direction = ParameterDirection.Input;

                    MySqlParameter myParamConnStringName = new MySqlParameter();
                    myParamConnStringName.ParameterName = "strConnectStringName";
                    myParamConnStringName.Value = strConnectionStringName;
                    myCommand.Parameters.Add(myParamConnStringName);
                    myParamConnStringName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamActive = new MySqlParameter();
                    myParamActive.ParameterName = "iActive";
                    myParamActive.Value = Convert.ToInt32(strActive);
                    myCommand.Parameters.Add(myParamActive);
                    myParamActive.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oCount";
                    myParamOutRes.Value = strAfterImage;
                    myCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myCommand.Parameters["oCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion


        #endregion

        #region CallDisposition Screen

        #region getCallDispositions
        // This method will fetch the DialerRules details from the database...
        public DataSet getCallDispositions(string theConnectionString)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getCallDispositions";
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
                    DataSet dsCallDisposition = new DataSet();
                    myAdapter.Fill(dsCallDisposition);

                    return dsCallDisposition;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region getCallDispByID
        // This method will output the Call Dispositions data in a Dataset
        public DataSet getCallDispByID(string theConnectionString, int intCallDispID)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getCallDispByID";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("iCallDispositionId", MySqlDbType.Int32);
                    myCommand.Parameters[0].Value = intCallDispID;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsCallDisposition = new DataSet();
                    myAdapter.Fill(dsCallDisposition);

                    return dsCallDisposition;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region insertCallDisposition

        public Int32 insertCallDisposition(string theConnectionString, string strCallDisp, string strDoNotCall,
                                      string strSale, string strContact, string strBadLead, string strCallBack,
                                      string strNotInterested, string strSystemDefaults, string strCustom, string strNotCalled,
                                      string strDialed, string strNotDialed, string strProcessedGood, string strProcessedBad,
                                      string strNotProcessed,
                                      string strModifyUser,string strModifyDateTime, string strScreenName, 
                                      string strTableName,string strBeforeImage, string strAfterImage)
        {
            
            string theCommandName = "sp_admin_insCallDisp";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCallDispCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCallDispCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamCallDisp = new MySqlParameter();
                    myParamCallDisp.ParameterName = "strCallDisp";
                    myParamCallDisp.Value = strCallDisp;
                    myCallDispCommand.Parameters.Add(myParamCallDisp);
                    myParamCallDisp.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDoNotCall = new MySqlParameter();
                    myParamDoNotCall.ParameterName = "iDoNotCall";
                    myParamDoNotCall.Value = Convert.ToInt32(strDoNotCall);
                    myCallDispCommand.Parameters.Add(myParamDoNotCall);
                    myParamDoNotCall.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSale = new MySqlParameter();
                    myParamSale.ParameterName = "iSale";
                    myParamSale.Value = Convert.ToInt32(strSale);
                    myCallDispCommand.Parameters.Add(myParamSale);
                    myParamSale.Direction = ParameterDirection.Input;

                    MySqlParameter myParamContact = new MySqlParameter();
                    myParamContact.ParameterName = "iContact";
                    myParamContact.Value = Convert.ToInt32(strContact);
                    myCallDispCommand.Parameters.Add(myParamContact);
                    myParamContact.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBadLead = new MySqlParameter();
                    myParamBadLead.ParameterName = "iBadLead";
                    myParamBadLead.Value = Convert.ToInt32(strBadLead);
                    myCallDispCommand.Parameters.Add(myParamBadLead);
                    myParamBadLead.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCallBack = new MySqlParameter();
                    myParamCallBack.ParameterName = "iCallBack";
                    myParamCallBack.Value = Convert.ToInt32(strCallBack);
                    myCallDispCommand.Parameters.Add(myParamCallBack);
                    myParamCallBack.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotInterested = new MySqlParameter();
                    myParamNotInterested.ParameterName = "iNotInterested";
                    myParamNotInterested.Value = Convert.ToInt32(strNotInterested);
                    myCallDispCommand.Parameters.Add(myParamNotInterested);
                    myParamNotInterested.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSysDefault = new MySqlParameter();
                    myParamSysDefault.ParameterName = "iSystemDefaults";
                    myParamSysDefault.Value = Convert.ToInt32(strSystemDefaults);
                    myCallDispCommand.Parameters.Add(myParamSysDefault);
                    myParamSysDefault.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCustom = new MySqlParameter();
                    myParamCustom.ParameterName = "iCustom";
                    myParamCustom.Value = Convert.ToInt32(strCustom);
                    myCallDispCommand.Parameters.Add(myParamCustom);
                    myParamCustom.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotCalled = new MySqlParameter();
                    myParamNotCalled.ParameterName = "iNotCalled";
                    myParamNotCalled.Value = Convert.ToInt32(strNotCalled);
                    myCallDispCommand.Parameters.Add(myParamNotCalled);
                    myParamNotCalled.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDialed = new MySqlParameter();
                    myParamDialed.ParameterName = "iDialed";
                    myParamDialed.Value = Convert.ToInt32(strDialed);
                    myCallDispCommand.Parameters.Add(myParamDialed);
                    myParamDialed.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotDialed = new MySqlParameter();
                    myParamNotDialed.ParameterName = "iNotDialed";
                    myParamNotDialed.Value = Convert.ToInt32(strNotDialed);
                    myCallDispCommand.Parameters.Add(myParamNotDialed);
                    myParamNotDialed.Direction = ParameterDirection.Input;

                    MySqlParameter myParamProcessedGood = new MySqlParameter();
                    myParamProcessedGood.ParameterName = "iProcessedGood";
                    myParamProcessedGood.Value = Convert.ToInt32(strProcessedGood);
                    myCallDispCommand.Parameters.Add(myParamProcessedGood);
                    myParamProcessedGood.Direction = ParameterDirection.Input;

                    MySqlParameter myParamProcessedBad = new MySqlParameter();
                    myParamProcessedBad.ParameterName = "iProcessedBad";
                    myParamProcessedBad.Value = Convert.ToInt32(strProcessedBad);
                    myCallDispCommand.Parameters.Add(myParamProcessedBad);
                    myParamProcessedBad.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotProcessed = new MySqlParameter();
                    myParamNotProcessed.ParameterName = "iNotProcessed";
                    myParamNotProcessed.Value = Convert.ToInt32(strNotProcessed);
                    myCallDispCommand.Parameters.Add(myParamNotProcessed);
                    myParamNotProcessed.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myCallDispCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myCallDispCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myCallDispCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myCallDispCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myCallDispCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myCallDispCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oCount";
                    myParamOutRes.Value = strAfterImage;
                    myCallDispCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myCallDispCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myCallDispCommand.Parameters["oCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region updateCallDisposition

        public Int32 updateCallDisposition(string theConnectionString, int intCallDispID, string strCallDisp, string strDoNotCall,
                                            string strSale, string strContact, string strBadLead, string strCallBack,
                                            string strNotInterested, string strSystemDefaults, string strCustom, string strNotCalled,
                                            string strDialed, string strNotDialed, string strProcessedGood, string strProcessedBad,
                                            string strNotProcessed,
                                            string strModifyUser, string strModifyDateTime, string strScreenName,
                                            string strTableName, string strBeforeImage, string strAfterImage)
        {

            string theCommandName = "sp_admin_updCallDisp";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCallDispUpdCmd = new MySqlCommand(theCommandName, mySqlCon);
                    myCallDispUpdCmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamCallDispId = new MySqlParameter();
                    myParamCallDispId.ParameterName = "iCallDispositionID";
                    myParamCallDispId.Value = intCallDispID;
                    myCallDispUpdCmd.Parameters.Add(myParamCallDispId);
                    myParamCallDispId.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCallDisp = new MySqlParameter();
                    myParamCallDisp.ParameterName = "strCallDisp";
                    myParamCallDisp.Value = strCallDisp;
                    myCallDispUpdCmd.Parameters.Add(myParamCallDisp);
                    myParamCallDisp.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDoNotCall = new MySqlParameter();
                    myParamDoNotCall.ParameterName = "iDoNotCall";
                    myParamDoNotCall.Value = Convert.ToInt32(strDoNotCall);
                    myCallDispUpdCmd.Parameters.Add(myParamDoNotCall);
                    myParamDoNotCall.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSale = new MySqlParameter();
                    myParamSale.ParameterName = "iSale";
                    myParamSale.Value = Convert.ToInt32(strSale);
                    myCallDispUpdCmd.Parameters.Add(myParamSale);
                    myParamSale.Direction = ParameterDirection.Input;

                    MySqlParameter myParamContact = new MySqlParameter();
                    myParamContact.ParameterName = "iContact";
                    myParamContact.Value = Convert.ToInt32(strContact);
                    myCallDispUpdCmd.Parameters.Add(myParamContact);
                    myParamContact.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBadLead = new MySqlParameter();
                    myParamBadLead.ParameterName = "iBadLead";
                    myParamBadLead.Value = Convert.ToInt32(strBadLead);
                    myCallDispUpdCmd.Parameters.Add(myParamBadLead);
                    myParamBadLead.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCallBack = new MySqlParameter();
                    myParamCallBack.ParameterName = "iCallBack";
                    myParamCallBack.Value = Convert.ToInt32(strCallBack);
                    myCallDispUpdCmd.Parameters.Add(myParamCallBack);
                    myParamCallBack.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotInterested = new MySqlParameter();
                    myParamNotInterested.ParameterName = "iNotInterested";
                    myParamNotInterested.Value = Convert.ToInt32(strNotInterested);
                    myCallDispUpdCmd.Parameters.Add(myParamNotInterested);
                    myParamNotInterested.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSysDefault = new MySqlParameter();
                    myParamSysDefault.ParameterName = "iSystemDefaults";
                    myParamSysDefault.Value = Convert.ToInt32(strSystemDefaults);
                    myCallDispUpdCmd.Parameters.Add(myParamSysDefault);
                    myParamSysDefault.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCustom = new MySqlParameter();
                    myParamCustom.ParameterName = "iCustom";
                    myParamCustom.Value = Convert.ToInt32(strCustom);
                    myCallDispUpdCmd.Parameters.Add(myParamCustom);
                    myParamCustom.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotCalled = new MySqlParameter();
                    myParamNotCalled.ParameterName = "iNotCalled";
                    myParamNotCalled.Value = Convert.ToInt32(strNotCalled);
                    myCallDispUpdCmd.Parameters.Add(myParamNotCalled);
                    myParamNotCalled.Direction = ParameterDirection.Input;

                    MySqlParameter myParamDialed = new MySqlParameter();
                    myParamDialed.ParameterName = "iDialed";
                    myParamDialed.Value = Convert.ToInt32(strDialed);
                    myCallDispUpdCmd.Parameters.Add(myParamDialed);
                    myParamDialed.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotDialed = new MySqlParameter();
                    myParamNotDialed.ParameterName = "iNotDialed";
                    myParamNotDialed.Value = Convert.ToInt32(strNotDialed);
                    myCallDispUpdCmd.Parameters.Add(myParamNotDialed);
                    myParamNotDialed.Direction = ParameterDirection.Input;

                    MySqlParameter myParamProcessedGood = new MySqlParameter();
                    myParamProcessedGood.ParameterName = "iProcessedGood";
                    myParamProcessedGood.Value = Convert.ToInt32(strProcessedGood);
                    myCallDispUpdCmd.Parameters.Add(myParamProcessedGood);
                    myParamProcessedGood.Direction = ParameterDirection.Input;

                    MySqlParameter myParamProcessedBad = new MySqlParameter();
                    myParamProcessedBad.ParameterName = "iProcessedBad";
                    myParamProcessedBad.Value = Convert.ToInt32(strProcessedBad);
                    myCallDispUpdCmd.Parameters.Add(myParamProcessedBad);
                    myParamProcessedBad.Direction = ParameterDirection.Input;

                    MySqlParameter myParamNotProcessed = new MySqlParameter();
                    myParamNotProcessed.ParameterName = "iNotProcessed";
                    myParamNotProcessed.Value = Convert.ToInt32(strNotProcessed);
                    myCallDispUpdCmd.Parameters.Add(myParamNotProcessed);
                    myParamNotProcessed.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myCallDispUpdCmd.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myCallDispUpdCmd.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myCallDispUpdCmd.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myCallDispUpdCmd.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myCallDispUpdCmd.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myCallDispUpdCmd.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oCount";
                    myParamOutRes.Value = strAfterImage;
                    myCallDispUpdCmd.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myCallDispUpdCmd.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myCallDispUpdCmd.Parameters["oCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #endregion

        #region Phone Extensions

        #region getPhoneExtensions
        /// <summary>
        /// This method will fetch the Phone Extension details from the database...
        /// </summary>
        public DataSet getPhoneExtensions(string theConnectionString)
        {
            //to get all the PhoneExtension List from the DB
            string theCommandName = "sp_admin_getPhoneExtensions";
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
                    DataSet dsPhoneExt = new DataSet();
                    myAdapter.Fill(dsPhoneExt);

                    return dsPhoneExt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region getPhoneExtensionByID

        /// <summary>
        /// This method will check the i/p PhoneExtensionID in DB and if present it will output the matched 
        /// record in a Dataset..
        /// </summary>
        public DataSet getPhoneExtensionByID(string theConnectionString, int intPhoneExtID)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getPhoneExtensionByID";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("iPhoneExtensionID", MySqlDbType.Int32);
                    myCommand.Parameters[0].Value = intPhoneExtID;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsSelectedPhoneExt = new DataSet();
                    myAdapter.Fill(dsSelectedPhoneExt);

                    return dsSelectedPhoneExt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region insertPhoneExtension
        /// <summary>
        /// This method will take the user i/p from the screen and inserts the PhoneExtension record in the Database 
        /// </summary>
        public Int32 insertPhoneExtension(string theConnectionString, string strSwitchName, string strCompany,
                                          string strSwitchAddress, string strSwitchPort, string strExtension, 
                                          string strUserName, string strPassword, 
                                          string strModifyUser, string strModifyDateTime, string strScreenName,
                                          string strTableName, string strBeforeImage, string strAfterImage)
        {

            string theCommandName = "sp_admin_insPhoneExtension";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myPhoneExtCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myPhoneExtCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamSwitchName = new MySqlParameter();
                    myParamSwitchName.ParameterName = "strSwitchName";
                    myParamSwitchName.Value = strSwitchName;
                    myPhoneExtCommand.Parameters.Add(myParamSwitchName);
                    myParamSwitchName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCompany = new MySqlParameter();
                    myParamCompany.ParameterName = "strCompany";
                    myParamCompany.Value = strCompany;
                    myPhoneExtCommand.Parameters.Add(myParamCompany);
                    myParamCompany.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSwitchAddress = new MySqlParameter();
                    myParamSwitchAddress.ParameterName = "strSwitchAddress";
                    myParamSwitchAddress.Value = strSwitchAddress;
                    myPhoneExtCommand.Parameters.Add(myParamSwitchAddress);
                    myParamSwitchAddress.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSwitchPort = new MySqlParameter();
                    myParamSwitchPort.ParameterName = "strSwitchPort";
                    myParamSwitchPort.Value = strSwitchPort;
                    myPhoneExtCommand.Parameters.Add(myParamSwitchPort);
                    myParamSwitchPort.Direction = ParameterDirection.Input;

                    MySqlParameter myParamExtension = new MySqlParameter();
                    myParamExtension.ParameterName = "strExtension";
                    myParamExtension.Value = strExtension;
                    myPhoneExtCommand.Parameters.Add(myParamExtension);
                    myParamExtension.Direction = ParameterDirection.Input;

                    MySqlParameter myParamUserName = new MySqlParameter();
                    myParamUserName.ParameterName = "strUserName";
                    myParamUserName.Value = strUserName;
                    myPhoneExtCommand.Parameters.Add(myParamUserName);
                    myParamUserName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamPassword = new MySqlParameter();
                    myParamPassword.ParameterName = "strPassword";
                    myParamPassword.Value = strPassword;
                    myPhoneExtCommand.Parameters.Add(myParamPassword);
                    myParamPassword.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myPhoneExtCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myPhoneExtCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myPhoneExtCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myPhoneExtCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myPhoneExtCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myPhoneExtCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oCount";
                    myParamOutRes.Value = strAfterImage;
                    myPhoneExtCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myPhoneExtCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myPhoneExtCommand.Parameters["oCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region updatePhoneExtension
        /// <summary>
        /// This method will take the changes entered by the user from the screen and updates the PhoneExtension record in the Database 
        /// </summary>
        public Int32 updatePhoneExtension(string theConnectionString, int intPhoneExtId, string strSwitchName, 
                                          string strCompany, string strSwitchAddress, string strSwitchPort, 
                                          string strExtension, string strUserName, string strPassword,
                                          string strModifyUser, string strModifyDateTime, string strScreenName,
                                          string strTableName, string strBeforeImage, string strAfterImage)
        {

            string theCommandName = "sp_admin_updPhoneExtension";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myPhoneExtCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myPhoneExtCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamPhonExtId = new MySqlParameter();
                    myParamPhonExtId.ParameterName = "iPhoneExtensionID";
                    myParamPhonExtId.Value = intPhoneExtId;
                    myPhoneExtCommand.Parameters.Add(myParamPhonExtId);
                    myParamPhonExtId.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSwitchName = new MySqlParameter();
                    myParamSwitchName.ParameterName = "strSwitchName";
                    myParamSwitchName.Value = strSwitchName;
                    myPhoneExtCommand.Parameters.Add(myParamSwitchName);
                    myParamSwitchName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCompany = new MySqlParameter();
                    myParamCompany.ParameterName = "strCompany";
                    myParamCompany.Value = strCompany;
                    myPhoneExtCommand.Parameters.Add(myParamCompany);
                    myParamCompany.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSwitchAddress = new MySqlParameter();
                    myParamSwitchAddress.ParameterName = "strSwitchAddress";
                    myParamSwitchAddress.Value = strSwitchAddress;
                    myPhoneExtCommand.Parameters.Add(myParamSwitchAddress);
                    myParamSwitchAddress.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSwitchPort = new MySqlParameter();
                    myParamSwitchPort.ParameterName = "strSwitchPort";
                    myParamSwitchPort.Value = strSwitchPort;
                    myPhoneExtCommand.Parameters.Add(myParamSwitchPort);
                    myParamSwitchPort.Direction = ParameterDirection.Input;

                    MySqlParameter myParamExtension = new MySqlParameter();
                    myParamExtension.ParameterName = "strExtension";
                    myParamExtension.Value = strExtension;
                    myPhoneExtCommand.Parameters.Add(myParamExtension);
                    myParamExtension.Direction = ParameterDirection.Input;

                    MySqlParameter myParamUserName = new MySqlParameter();
                    myParamUserName.ParameterName = "strUserName";
                    myParamUserName.Value = strUserName;
                    myPhoneExtCommand.Parameters.Add(myParamUserName);
                    myParamUserName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamPassword = new MySqlParameter();
                    myParamPassword.ParameterName = "strPassword";
                    myParamPassword.Value = strPassword;
                    myPhoneExtCommand.Parameters.Add(myParamPassword);
                    myParamPassword.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myPhoneExtCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myPhoneExtCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myPhoneExtCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myPhoneExtCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myPhoneExtCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myPhoneExtCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oCount";
                    myParamOutRes.Value = strAfterImage;
                    myPhoneExtCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myPhoneExtCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myPhoneExtCommand.Parameters["oCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #endregion

        #region Lead Campaigns

        #region getLeadCampaigns
        /// <summary>
        /// This method will fetch the Lead Campaign details from the database...
        /// </summary>
        public DataSet getLeadCampaigns(string theConnectionString)
        {
            //to get all the PhoneExtension List from the DB
            string theCommandName = "sp_admin_getLeadCampaigns";
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
                    DataSet dsLeadCampaign = new DataSet();
                    myAdapter.Fill(dsLeadCampaign);

                    return dsLeadCampaign;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region getLeadCampaignsByID

        /// <summary>
        /// This method will check the i/p LeadCampaignId in DB and if present it will output the matched 
        /// record in a Dataset..
        /// </summary>
        public DataSet getLeadCampaignsByID(string theConnectionString, int intLeadCampaignsId)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getLeadCampaignsByID";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("iLeadCampaignID", MySqlDbType.Int32);
                    myCommand.Parameters[0].Value = intLeadCampaignsId;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsSelectedLeadCampaign = new DataSet();
                    myAdapter.Fill(dsSelectedLeadCampaign);

                    return dsSelectedLeadCampaign;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region insertLeadCampaign
        /// <summary>
        /// This method will take the user i/p from the screen and inserts the LeadCampaign record in the Database 
        /// It creates a record in the admin_log table for the logging functionality
        /// </summary>
        public Int32 insertLeadCampaign(string theConnectionString, string strCampaignProdCode, string strProductLine,
                                          string strChannel, string strTargusCode,
                                          string strModifyUser, string strModifyDateTime, string strScreenName,
                                          string strTableName, string strBeforeImage, string strAfterImage)
        {

            string theCommandName = "sp_admin_insLeadCampaign";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myLeadCampCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myLeadCampCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamProdCode = new MySqlParameter();
                    myParamProdCode.ParameterName = "strCampaignProdCode";
                    myParamProdCode.Value = strCampaignProdCode;
                    myLeadCampCommand.Parameters.Add(myParamProdCode);
                    myParamProdCode.Direction = ParameterDirection.Input;

                    MySqlParameter myParamProdLine = new MySqlParameter();
                    myParamProdLine.ParameterName = "strProductLine";
                    myParamProdLine.Value = strProductLine;
                    myLeadCampCommand.Parameters.Add(myParamProdLine);
                    myParamProdLine.Direction = ParameterDirection.Input;

                    MySqlParameter myParamChannel = new MySqlParameter();
                    myParamChannel.ParameterName = "strChannel";
                    myParamChannel.Value = strChannel;
                    myLeadCampCommand.Parameters.Add(myParamChannel);
                    myParamChannel.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTargusCode = new MySqlParameter();
                    myParamTargusCode.ParameterName = "strTargusCode";
                    myParamTargusCode.Value = strTargusCode;
                    myLeadCampCommand.Parameters.Add(myParamTargusCode);
                    myParamTargusCode.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myLeadCampCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myLeadCampCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myLeadCampCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myLeadCampCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myLeadCampCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myLeadCampCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oResCount";
                    myLeadCampCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myLeadCampCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myLeadCampCommand.Parameters["oResCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region updateLeadCampaign
        /// <summary>
        /// This method will take the changes entered by the user from the screen and updates the LeadCmapaign record in the Database 
        /// </summary>
        public Int32 updateLeadCampaign(string theConnectionString, int intLeadCampaignId, string strCampaignProdCode, string strProductLine,
                                          string strChannel, string strTargusCode,
                                          string strModifyUser, string strModifyDateTime, string strScreenName,
                                          string strTableName, string strBeforeImage, string strAfterImage)
        {

            string theCommandName = "sp_admin_updLeadCampaign";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myLeadCampaignCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myLeadCampaignCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamLeadCampaignId = new MySqlParameter();
                    myParamLeadCampaignId.ParameterName = "intLeadCampaignID";
                    myParamLeadCampaignId.Value = intLeadCampaignId;
                    myLeadCampaignCommand.Parameters.Add(myParamLeadCampaignId);
                    myParamLeadCampaignId.Direction = ParameterDirection.Input;

                    MySqlParameter myParamCampaignProdCode = new MySqlParameter();
                    myParamCampaignProdCode.ParameterName = "strCampaignProdCode";
                    myParamCampaignProdCode.Value = strCampaignProdCode;
                    myLeadCampaignCommand.Parameters.Add(myParamCampaignProdCode);
                    myParamCampaignProdCode.Direction = ParameterDirection.Input;

                    MySqlParameter myParamProductLine = new MySqlParameter();
                    myParamProductLine.ParameterName = "strProductLine";
                    myParamProductLine.Value = strProductLine;
                    myLeadCampaignCommand.Parameters.Add(myParamProductLine);
                    myParamProductLine.Direction = ParameterDirection.Input;

                    MySqlParameter myParamChannel = new MySqlParameter();
                    myParamChannel.ParameterName = "strChannel";
                    myParamChannel.Value = strChannel;
                    myLeadCampaignCommand.Parameters.Add(myParamChannel);
                    myParamChannel.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTargusCode = new MySqlParameter();
                    myParamTargusCode.ParameterName = "strTargusCode";
                    myParamTargusCode.Value = strTargusCode;
                    myLeadCampaignCommand.Parameters.Add(myParamTargusCode);
                    myParamTargusCode.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myLeadCampaignCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myLeadCampaignCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myLeadCampaignCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myLeadCampaignCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myLeadCampaignCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myLeadCampaignCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oResCount";
                    myLeadCampaignCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myLeadCampaignCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myLeadCampaignCommand.Parameters["oResCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #endregion

        #region Calling Times ByState

        #region getCallingTimesByState
        /// <summary>
        /// This method will fetch the Calling Times ByState details from the database...
        /// </summary>
        public DataSet getCallingTimesByState(string theConnectionString)
        {
            //to get all the PhoneExtension List from the DB
            string theCommandName = "sp_admin_getCallingTimesByState";
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
                    DataSet dsCallingTimes = new DataSet();
                    myAdapter.Fill(dsCallingTimes);

                    return dsCallingTimes;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region getCallingTimesByStateAbbr

        /// <summary>
        /// This method will check the i/p State Abbr in DB and if present it will output the matched 
        /// record in a Dataset..
        /// </summary>
        public DataSet getCallingTimesByStateAbbr(string theConnectionString, string strStateAbbr)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getCallingTimesByStateAbbr";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("strState", MySqlDbType.VarChar);
                    myCommand.Parameters[0].Value = strStateAbbr;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsSelectedCallingTimes = new DataSet();
                    myAdapter.Fill(dsSelectedCallingTimes);

                    return dsSelectedCallingTimes;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region insCallingTimesByState
        /// <summary>
        /// This method will take the user i/p from the screen and inserts the CallingTimesByState record in the Database 
        /// It creates a record in the admin_log table for the logging functionality
        /// </summary>
        public Int32 insCallingTimesByState(string theConnectionString, string strState, string strWeekdayStartTime,
                                          string strWeekdayEndTime, string strSaturdayStartTime, string strSaturdayEndTime,
                                          string strSundayStartTime, string strSundayEndTime,
                                          string strHolidayStartTime, string strHolidayEndTime,
                                          string strModifyUser, string strModifyDateTime, string strScreenName,
                                          string strTableName, string strBeforeImage, string strAfterImage)
        {

            string theCommandName = "sp_admin_insCallingTimesByState";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCallingTimesCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCallingTimesCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamState = new MySqlParameter();
                    myParamState.ParameterName = "strState";
                    myParamState.Value = strState;
                    myCallingTimesCommand.Parameters.Add(myParamState);
                    myParamState.Direction = ParameterDirection.Input;

                    MySqlParameter myParamWeekdayStartTime = new MySqlParameter();
                    myParamWeekdayStartTime.ParameterName = "strWeekdayStartTime";
                    myParamWeekdayStartTime.Value = strWeekdayStartTime;
                    myCallingTimesCommand.Parameters.Add(myParamWeekdayStartTime);
                    myParamWeekdayStartTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamWeekdayEndTime = new MySqlParameter();
                    myParamWeekdayEndTime.ParameterName = "strWeekdayEndTime";
                    myParamWeekdayEndTime.Value = strWeekdayEndTime;
                    myCallingTimesCommand.Parameters.Add(myParamWeekdayEndTime);
                    myParamWeekdayEndTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSaturdayStartTime = new MySqlParameter();
                    myParamSaturdayStartTime.ParameterName = "strSaturdayStartTime";
                    myParamSaturdayStartTime.Value = strSaturdayStartTime;
                    myCallingTimesCommand.Parameters.Add(myParamSaturdayStartTime);
                    myParamSaturdayStartTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSaturdayEndTime = new MySqlParameter();
                    myParamSaturdayEndTime.ParameterName = "strSaturdayEndTime";
                    myParamSaturdayEndTime.Value = strSaturdayEndTime;
                    myCallingTimesCommand.Parameters.Add(myParamSaturdayEndTime);
                    myParamSaturdayEndTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSundayStartTime = new MySqlParameter();
                    myParamSundayStartTime.ParameterName = "strSundayStartTime";
                    myParamSundayStartTime.Value = strSundayStartTime;
                    myCallingTimesCommand.Parameters.Add(myParamSundayStartTime);
                    myParamSundayStartTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSundayEndTime = new MySqlParameter();
                    myParamSundayEndTime.ParameterName = "strSundayEndTime";
                    myParamSundayEndTime.Value = strSundayEndTime;
                    myCallingTimesCommand.Parameters.Add(myParamSundayEndTime);
                    myParamSundayEndTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamHolidayStartTime = new MySqlParameter();
                    myParamHolidayStartTime.ParameterName = "strHolidayStartTime";
                    myParamHolidayStartTime.Value = strHolidayStartTime;
                    myCallingTimesCommand.Parameters.Add(myParamHolidayStartTime);
                    myParamHolidayStartTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamHolidayEndTime = new MySqlParameter();
                    myParamHolidayEndTime.ParameterName = "strHolidayEndTime";
                    myParamHolidayEndTime.Value = strHolidayEndTime;
                    myCallingTimesCommand.Parameters.Add(myParamHolidayEndTime);
                    myParamHolidayEndTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myCallingTimesCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myCallingTimesCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myCallingTimesCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myCallingTimesCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myCallingTimesCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myCallingTimesCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "vResult";
                    myCallingTimesCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myCallingTimesCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myCallingTimesCommand.Parameters["vResult"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region updCallingTimesByState
        /// <summary>
        /// This method will take the user i/p from the screen and updated the CallingTimesByState record in the Database 
        /// It creates a record in the admin_log table for the logging functionality
        /// </summary>
        public Int32 updCallingTimesByState(string theConnectionString, string strState, string strWeekdayStartTime,
                                          string strWeekdayEndTime, string strSaturdayStartTime, string strSaturdayEndTime,
                                          string strSundayStartTime, string strSundayEndTime,
                                          string strHolidayStartTime, string strHolidayEndTime,
                                          string strModifyUser, string strModifyDateTime, string strScreenName,
                                          string strTableName, string strBeforeImage, string strAfterImage)
        {

            string theCommandName = "sp_admin_updCallingTimesByState";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCallingTimesCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCallingTimesCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamState = new MySqlParameter();
                    myParamState.ParameterName = "strState";
                    myParamState.Value = strState;
                    myCallingTimesCommand.Parameters.Add(myParamState);
                    myParamState.Direction = ParameterDirection.Input;

                    MySqlParameter myParamWeekdayStartTime = new MySqlParameter();
                    myParamWeekdayStartTime.ParameterName = "strWeekdayStartTime";
                    myParamWeekdayStartTime.Value = strWeekdayStartTime;
                    myCallingTimesCommand.Parameters.Add(myParamWeekdayStartTime);
                    myParamWeekdayStartTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamWeekdayEndTime = new MySqlParameter();
                    myParamWeekdayEndTime.ParameterName = "strWeekdayEndTime";
                    myParamWeekdayEndTime.Value = strWeekdayEndTime;
                    myCallingTimesCommand.Parameters.Add(myParamWeekdayEndTime);
                    myParamWeekdayEndTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSaturdayStartTime = new MySqlParameter();
                    myParamSaturdayStartTime.ParameterName = "strSaturdayStartTime";
                    myParamSaturdayStartTime.Value = strSaturdayStartTime;
                    myCallingTimesCommand.Parameters.Add(myParamSaturdayStartTime);
                    myParamSaturdayStartTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSaturdayEndTime = new MySqlParameter();
                    myParamSaturdayEndTime.ParameterName = "strSaturdayEndTime";
                    myParamSaturdayEndTime.Value = strSaturdayEndTime;
                    myCallingTimesCommand.Parameters.Add(myParamSaturdayEndTime);
                    myParamSaturdayEndTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSundayStartTime = new MySqlParameter();
                    myParamSundayStartTime.ParameterName = "strSundayStartTime";
                    myParamSundayStartTime.Value = strSundayStartTime;
                    myCallingTimesCommand.Parameters.Add(myParamSundayStartTime);
                    myParamSundayStartTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamSundayEndTime = new MySqlParameter();
                    myParamSundayEndTime.ParameterName = "strSundayEndTime";
                    myParamSundayEndTime.Value = strSundayEndTime;
                    myCallingTimesCommand.Parameters.Add(myParamSundayEndTime);
                    myParamSundayEndTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamHolidayStartTime = new MySqlParameter();
                    myParamHolidayStartTime.ParameterName = "strHolidayStartTime";
                    myParamHolidayStartTime.Value = strHolidayStartTime;
                    myCallingTimesCommand.Parameters.Add(myParamHolidayStartTime);
                    myParamHolidayStartTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamHolidayEndTime = new MySqlParameter();
                    myParamHolidayEndTime.ParameterName = "strHolidayEndTime";
                    myParamHolidayEndTime.Value = strHolidayEndTime;
                    myCallingTimesCommand.Parameters.Add(myParamHolidayEndTime);
                    myParamHolidayEndTime.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myCallingTimesCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myCallingTimesCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myCallingTimesCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myCallingTimesCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myCallingTimesCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myCallingTimesCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "vResult";
                    myCallingTimesCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myCallingTimesCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myCallingTimesCommand.Parameters["vResult"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #endregion

        #region RoutingGroups

        #region getRoutingGroups
        /// <summary>
        /// This method will fetch the RoutingGroups details from the database...
        /// </summary>
        public DataSet getRoutingGroups(string theConnectionString)
        {
            //to get all the PhoneExtension List from the DB
            string theCommandName = "sp_admin_getRoutingGroups";
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
                    DataSet dsRoutingGroups = new DataSet();
                    myAdapter.Fill(dsRoutingGroups);

                    return dsRoutingGroups;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region getRoutingGroupsByID

        /// <summary>
        /// This method will check the i/p RoutingGroupId in DB and if present it will output the matched 
        /// record in a Dataset..
        /// </summary>
        public DataSet getRoutingGroupsByID(string theConnectionString, int intRoutingGroupId)
        {
            //to get all the Holiday List from the DB
            string theCommandName = "sp_admin_getRoutingGroupsByID";
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("iRoutingGroupId", MySqlDbType.Int32);
                    myCommand.Parameters[0].Value = intRoutingGroupId;

                    //Creating an empty Dataadapter to the command..
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = myCommand;
                    // filling the Dataset from the output of stored procedure
                    DataSet dsSelectedRoutingGroup = new DataSet();
                    myAdapter.Fill(dsSelectedRoutingGroup);

                    return dsSelectedRoutingGroup;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region insRoutingGroups
        /// <summary>
        /// This method will take the user i/p from the screen and inserts the routinggroup record in the Database 
        /// It creates a record in the admin_log table for the logging functionality
        /// </summary>
        public Int32 insRoutingGroups(string theConnectionString, string strRoutingGroup, string strRoutingGroupDesc,
                                          string strModifyUser, string strModifyDateTime, string strScreenName,
                                          string strTableName, string strBeforeImage, string strAfterImage)
        {

            string theCommandName = "sp_admin_insRoutingGroups";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myRoutingGroups = new MySqlCommand(theCommandName, mySqlCon);
                    myRoutingGroups.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamRoutingGroup = new MySqlParameter();
                    myParamRoutingGroup.ParameterName = "strRoutingGroup";
                    myParamRoutingGroup.Value = strRoutingGroup;
                    myRoutingGroups.Parameters.Add(myParamRoutingGroup);
                    myParamRoutingGroup.Direction = ParameterDirection.Input;

                    MySqlParameter myParamRoutingGroupDesc = new MySqlParameter();
                    myParamRoutingGroupDesc.ParameterName = "strRoutingGroupDesc";
                    myParamRoutingGroupDesc.Value = strRoutingGroupDesc;
                    myRoutingGroups.Parameters.Add(myParamRoutingGroupDesc);
                    myParamRoutingGroupDesc.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myRoutingGroups.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myRoutingGroups.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myRoutingGroups.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myRoutingGroups.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myRoutingGroups.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myRoutingGroups.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oCount";
                    myRoutingGroups.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myRoutingGroups.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myRoutingGroups.Parameters["oCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region updRoutingGroups
        /// <summary>
        /// This method will take the user i/p from the screen and updated the RoutingGroups record in the Database 
        /// It creates a record in the admin_log table for the logging functionality
        /// </summary>
        public Int32 updRoutingGroups(string theConnectionString, Int32 iRoutingGroupId,
                                          string strRoutingGroup, string strRoutingGroupDesc,
                                          string strModifyUser, string strModifyDateTime, string strScreenName,
                                          string strTableName, string strBeforeImage, string strAfterImage)
        {

            string theCommandName = "sp_admin_updRoutingGroups";
            Int32 intRecAffected = 0;
            try
            {
                //getting the connectionstring from the Appln to fetch the data...
                string myConString = GetConnectionStringByName(theConnectionString);
                using (MySql.Data.MySqlClient.MySqlConnection mySqlCon = GetConnection(myConString))
                {
                    mySqlCon.Open();
                    MySqlCommand myCallingTimesCommand = new MySqlCommand(theCommandName, mySqlCon);
                    myCallingTimesCommand.CommandType = CommandType.StoredProcedure;

                    MySqlParameter myParamRoutingGroupId = new MySqlParameter();
                    myParamRoutingGroupId.ParameterName = "iRoutingGroupId";
                    myParamRoutingGroupId.Value = iRoutingGroupId;
                    myCallingTimesCommand.Parameters.Add(myParamRoutingGroupId);
                    myParamRoutingGroupId.Direction = ParameterDirection.Input;

                    MySqlParameter myParamRoutingGroup = new MySqlParameter();
                    myParamRoutingGroup.ParameterName = "strRoutingGroup";
                    myParamRoutingGroup.Value = strRoutingGroup;
                    myCallingTimesCommand.Parameters.Add(myParamRoutingGroup);
                    myParamRoutingGroup.Direction = ParameterDirection.Input;

                    MySqlParameter myParamRoutingGroupDesc = new MySqlParameter();
                    myParamRoutingGroupDesc.ParameterName = "strRoutingGroupDesc";
                    myParamRoutingGroupDesc.Value = strRoutingGroupDesc;
                    myCallingTimesCommand.Parameters.Add(myParamRoutingGroupDesc);
                    myParamRoutingGroupDesc.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyUser = new MySqlParameter();
                    myParamModifyUser.ParameterName = "strModifyUser";
                    myParamModifyUser.Value = strModifyUser;
                    myCallingTimesCommand.Parameters.Add(myParamModifyUser);
                    myParamModifyUser.Direction = ParameterDirection.Input;

                    MySqlParameter myParamModifyDate = new MySqlParameter();
                    myParamModifyDate.ParameterName = "strModifyDateTime";
                    myParamModifyDate.Value = strModifyDateTime;
                    myCallingTimesCommand.Parameters.Add(myParamModifyDate);
                    myParamModifyDate.Direction = ParameterDirection.Input;

                    MySqlParameter myParamScreenName = new MySqlParameter();
                    myParamScreenName.ParameterName = "strScreenName";
                    myParamScreenName.Value = strScreenName;
                    myCallingTimesCommand.Parameters.Add(myParamScreenName);
                    myParamScreenName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamTabelName = new MySqlParameter();
                    myParamTabelName.ParameterName = "strTableName";
                    myParamTabelName.Value = strTableName;
                    myCallingTimesCommand.Parameters.Add(myParamTabelName);
                    myParamTabelName.Direction = ParameterDirection.Input;

                    MySqlParameter myParamBeforeImage = new MySqlParameter();
                    myParamBeforeImage.ParameterName = "tBeforeImage";
                    myParamBeforeImage.Value = strBeforeImage;
                    myCallingTimesCommand.Parameters.Add(myParamBeforeImage);
                    myParamBeforeImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamAfterImage = new MySqlParameter();
                    myParamAfterImage.ParameterName = "tAfterImage";
                    myParamAfterImage.Value = strAfterImage;
                    myCallingTimesCommand.Parameters.Add(myParamAfterImage);
                    myParamAfterImage.Direction = ParameterDirection.Input;

                    MySqlParameter myParamOutRes = new MySqlParameter();
                    myParamOutRes.ParameterName = "oCount";
                    myCallingTimesCommand.Parameters.Add(myParamOutRes);
                    myParamOutRes.Direction = ParameterDirection.Output;

                    myCallingTimesCommand.ExecuteNonQuery();
                    intRecAffected = Convert.ToInt32(myCallingTimesCommand.Parameters["oCount"].Value);
                    return intRecAffected;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #endregion


        #endregion







    }



}