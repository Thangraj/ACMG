using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Web.Security;
using System.Configuration;
using System.Web.Configuration;

namespace ACMGAdmin.DataAccess
{
    public class LDAPAccess
    {

        protected string GetConnectionStringByName(string name)
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


        public string[] getFullName(string theUserName, string theConnectStringName)
        {
            theUserName = theUserName.Trim();
            string[] theReturnValues = new string[2];
            string theConnectstring = GetConnectionStringByName(theConnectStringName);
            string firstName = "";
            string lastName = "";
            DirectoryEntry searchRoot = new DirectoryEntry(theConnectstring, "obmdialer@acmg.local", "ObMDialPa$$499");
            DirectorySearcher ds = new DirectorySearcher(searchRoot);
            ds.SearchScope = SearchScope.Subtree;
            ds.PropertiesToLoad.Add("sn");
            ds.PropertiesToLoad.Add("givenName");
            ds.Filter = string.Format("(&(objectCategory=person)(samAccountName={0}))", theUserName);
            SearchResult sr = ds.FindOne();
            if (sr != null)
            {

                DirectoryEntry theUser = sr.GetDirectoryEntry();
                
                if (sr.Properties["givenName"].Count > 0)
                {
                    firstName = sr.Properties["givenName"][0].ToString();
                }
                
                
                if (sr.Properties["sn"].Count > 0)
                {
                    lastName = sr.Properties["sn"][0].ToString();
                }
                


            }
            theReturnValues[0] = firstName;
            theReturnValues[1] = lastName;

            return theReturnValues;


        }


        public void updateUserFullName(string theUserName, string theFirstName, string thelastName,string theConnectStringName)
        {

            string theConnectstring = GetConnectionStringByName(theConnectStringName);
            DirectoryEntry searchRoot = new DirectoryEntry(theConnectstring, "obmdialer@acmg.local", "ObMDialPa$$499");
            DirectorySearcher ds = new DirectorySearcher(searchRoot);
            ds.SearchScope = SearchScope.Subtree;
           // ds.PropertiesToLoad.Add("sn");
           // ds.PropertiesToLoad.Add("givenName");
            ds.Filter = string.Format("(&(objectCategory=person)(samAccountName={0}))", theUserName.Trim());
            SearchResult sr = ds.FindOne();
            if (sr != null)
            {
                DirectoryEntry theUser = sr.GetDirectoryEntry();
                AddUpdateProperty(theUser, "givenName", theFirstName.Trim());
                AddUpdateProperty(theUser, "sn", thelastName.Trim());
                theUser.CommitChanges();
           }



        }



        protected void AddUpdateProperty(DirectoryEntry r, string propName, Object value)
        {
            if (r.Properties[propName].Count == 0)
            {
                r.Properties[propName].Add(value);
            }
            else
            {

                r.Properties[propName][0] = value;


            }
        }
           



    }
}