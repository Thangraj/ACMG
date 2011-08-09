using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ACMGAdmin.Account
{
    public partial class AddUser : System.Web.UI.Page
    {   MembershipUser user;

    string theCallCenterConnectString;
    //
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.QueryString.HasKeys()) { 
            if (!Roles.IsUserInRole("Administrators") && !Roles.IsUserInRole("User Admin"))
            {
                //Label1.Text = User.Identity.Name + " is in role TestRole";
                Response.Redirect("NoAccess.aspx");
                //Response.Write("Only Admin allowed");
                //Response.End();
            }
            else
            {
                //Label1.Text = User.Identity.Name + " is NOT in role TestRole";
            }
            }
            theCallCenterConnectString = Session["theCallCenterConnectString"].ToString();
            LabelUserList.Text = "Add User for " + Session["theCallCenterName"].ToString();
            if (IsPostBack)
            {
                try
                {
                    user = Membership.Providers[theCallCenterConnectString].GetUser(username.Text, false);
                    if (user == null)
                    {
                        string theAddresult = "";
                        theAddresult=AddNewUser();
                        if (theAddresult == "Success")
                        { 
                        addFullName();
                        Response.Redirect("ViewUsersByRole.aspx");
                        }
                        else
                        {
                            ConfirmationMessage.InnerText = "Insert Failure: " + theAddresult;
                        }
                    }
                    else
                    {
                        ConfirmationMessage.InnerText = "Insert Failure: User Already Exists.";
                    }
                    
                }
                catch (Exception ex)
                {
                    ConfirmationMessage.InnerText = "Insert Failure: " + ex.Message;
                }
            }
        }


        protected string AddNewUser()
        {
            // Add User.
            MembershipCreateStatus theCreateStatus= new MembershipCreateStatus();
           MembershipUser newUser = Membership.Providers[theCallCenterConnectString].CreateUser(username.Text, password.Text, email.Text,null,null,true,null,out theCreateStatus);
           //newUser.Comment = comment.Text;
           // Membership.Providers[theCallCenterConnectString].UpdateUser(newUser);
           if (theCreateStatus.ToString() == "Success")
           {
               // Add Roles.
               foreach (ListItem rolebox in UserRoles.Items)
               {
                   if (rolebox.Selected)
                   {
                       Roles.AddUserToRole(username.Text, rolebox.Text);
                   }
               }
           }
           return theCreateStatus.ToString();
        }

        private void Page_PreRender()
        {
            UserRoles.DataSource = Roles.GetAllRoles();
            UserRoles.DataBind();
        }


        protected void addFullName() 
        {

            DataAccess.LDAPAccess theLDAP = new DataAccess.LDAPAccess();


            theLDAP.updateUserFullName(username.Text, txtfname.Text, txtlname.Text, theCallCenterConnectString);
        
        
        
        }


    }
}