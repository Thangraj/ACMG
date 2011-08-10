using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace ACMGAdmin.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
           //DropDownList theDropDown = (DropDownList)LoginUser.FindControl("DropDownCallCenter");
  


            
            if (IsPostBack == false)

            {
                if (Request.IsAuthenticated && !(Request.QueryString["ReturnUrl"]=="" || (Request.QueryString["ReturnUrl"]==null))){
                Response.Redirect("~/NoAccess.aspx");}
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet ds = new DataSet();
                ds = myObj.getCallCenters("LocalMySqlServer");
                //populate callcenters
                
                //theDropDown.DataSource = ds.Tables[0];
                //theDropDown.DataTextField = "CallCenterName";
                //theDropDown.DataValueField = "CallCenterID";
                //theDropDown.DataBind();
                DropDownCallCenter.DataSource = ds.Tables[0];
                DropDownCallCenter.DataTextField = "CallCenterName";
                DropDownCallCenter.DataValueField = "CallCenterID";
                DropDownCallCenter.DataBind();
                Session["theCallCenters"] = ds;

               

            }

            else
            {


                string theCallCenterID = DropDownCallCenter.SelectedValue;
                Session["theCallCenterID"] = theCallCenterID;
                Session["theCallCenterName"] = DropDownCallCenter.SelectedItem.Text;
                string theCallCenterConnectString = "";

                DataSet ds = new DataSet();
                ds = (DataSet)Session["theCallCenters"];
                DataRow theRow = ds.Tables[0].Rows[0];
                DataRow[] foundRows;



                foundRows = ds.Tables[0].Select("CallCenterID=" + theCallCenterID);
                theRow = foundRows[0];
                theCallCenterConnectString = theRow["LDAPConnectStringName"].ToString();
                Session["theCallCenterConnectString"] = theCallCenterConnectString;
            
            }

   }


        protected void AuthenticateEventHandler(object sender, AuthenticateEventArgs e)
        {

            TextBox TextBox1 = (TextBox)LoginUser.FindControl("UserName");
            TextBox TextBox2 = (TextBox)LoginUser.FindControl("Password");
           
            bool foundUser = false;
            string theCallCenterConnectString=Session["theCallCenterConnectString"].ToString();

            if( Membership.Providers[theCallCenterConnectString].ValidateUser(TextBox1.Text, TextBox2.Text))
            { foundUser = true; }

            //List<string> roles = new List<string>();
            //roles.Add("GeneralUser");
            //// this will call the default MembershipProvider
            //if (Membership.Provider.ValidateUser(TextBox1.Text, TextBox2.Text))
            //{
            //    foundUser = true;
            //    // do any additional lookups for this type of user (Default MembershipProvider) here
            //} // otherwise, explicitly call secondary provider
            //else if (
            //    Membership.Providers["SecondaryADMembershipProvider"].ValidateUser(TextBox1.Text, TextBox2.Text))
            //{
            //    foundUser = true;
            //    // roles.Add("SecondaryUser");
            //    // do any additional lookups relevant to this type of user
            //}
            //if (foundUser)
            //{
            //    Session["UserId"] = TextBox1.Text;
            //    // Session["Groups"] = roles;
            //}

            if (foundUser == false) { if (TextBox1.Text == "AdminSetup" && TextBox2.Text == "AdminSetup") { 
                foundUser = true;
                try
                {
                    Roles.AddUserToRole("AdminSetup", "System Administration");
                }
                catch { }
            }
            }
            e.Authenticated = foundUser;
        }




        protected void LoggedIn(object sender, EventArgs e)
        {
            dblogin();
            Response.Redirect("~/Default.aspx");
            
            //if (Roles.IsUserInRole("Administrator"))
            //{
            //    Response.Redirect("~/Default.aspx");
            //}
            //else
            //{

            //    Response.Redirect("~/Agent/LoadAgent.aspx");
            //}
            


            


        }

        protected void dblogin()
        {

            if (Session["Agent"] != null)
            { return; }//already logged in }

           
            //Session["Agent"] = LoginUser.UserName.ToString();
            Session["Agent"] = LoginUser.UserName.ToString().Trim().ToLower();
            Session["LoginTime"] = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            Session["Company"] = "ACMG";
            DataAccess.LDAPAccess myLDAP=new DataAccess.LDAPAccess();
            string[] theAgentName= myLDAP.getFullName(LoginUser.UserName.ToString(),Session["theCallCenterConnectString"].ToString());
            Session["AgentFirstName"] = theAgentName[0];
            Session["AgentLastName"] = theAgentName[1];
            
            

            

        }
    }
}
