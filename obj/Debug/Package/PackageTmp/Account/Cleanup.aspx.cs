using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


namespace ACMGAdmin.Agent
{
    public partial class Cleanup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (User.Identity.IsAuthenticated)
            {

                string myScriptCheck = @"function openCheck() {window.close();  }";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScriptCheck", myScriptCheck, true);

                //Response.Write("<p> Closed without Logging Out...Destroying the Following Session Variables:</p>");
                //for (int i = 0; i < Session.Count; i++)
                //{
                //    Response.Write("<p>" + Session.Keys[i].ToString() + " - " + Session[i].ToString() + "</p>");
                //}

                //remove authentication cookie
                FormsAuthentication.SignOut();
                
                
            }
            else
            {
                string myScriptClose = @"function openCheck() {window.close();  }";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScriptClose", myScriptClose, true);
                //for (int i = 0; i < Session.Count; i++)
                //{
                //    Response.Write("<p>" + Session.Keys[i].ToString() + " - " + Session[i].ToString() + "</p>");
                //}

            }

            //release extension
            ReleaseExtension();

            Session.Clear();

            Session.RemoveAll();

            Session.Abandon();

        }

        protected void ReleaseExtension()
        {

            if (Session["PhoneExtensionAssignmentID"] == null)
            { return; }

            string theResult = "";
            DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
            theResult = myObj.releaseExtension(Session["PhoneExtensionAssignmentID"].ToString(), "LocalMySqlServer").ToString();
            Session["PhoneExtensionAssignmentID"] = 0;
            Session["PhoneExtensionID"] = 0;
            Session["SwitchName"] = 0;
            Session["SwitchAddress"] = 0;
            Session["SwitchPort"] = 0;
            Session["Extension"] = 0;
            Session["UserName"] = 0;
            Session["Password"] = 0;
        }
       
    }
}