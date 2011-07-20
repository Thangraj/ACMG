using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ACMGAdmin.Account
{
    public partial class EditUser : System.Web.UI.Page
    {
        
        string username;
        string theCallCenterConnectString ;
        DataAccess.LDAPAccess myLDAP=new DataAccess.LDAPAccess();

        MembershipUser user;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Roles.IsUserInRole("Administrators") && !Roles.IsUserInRole("User Admin"))
            {
                //Label1.Text = User.Identity.Name + " is in role TestRole";
               Response.Redirect("~NoAccess.aspx");
                
            }
            
            
            theCallCenterConnectString = Session["theCallCenterConnectString"].ToString();
            username = Request.QueryString["username"];
            if (username == null || username == "")
            {
                Response.Redirect("ViewUsersByRole.aspx");
            }
            

            //this binds to the particular user from the particular Membership provider
            user = Membership.Providers[theCallCenterConnectString].GetUser(username, false);
            var myDataSource = new List<MembershipUser> {user};
            UserInfo.DataSource = myDataSource;
            UserInfo.DataBind();
            //replace with object- only using Phillipine center.
            if (IsPostBack == false) { 
            UserUpdateMessage.Text = "";
            LabelFullName.Text = getFullname(user.UserName);
            
            }
        }

        protected void UserInfo_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            //Need to handle the update manually because MembershipUser does not have a
            //parameterless constructor  

            user.Email = (string)e.NewValues[0];
            user.Comment = (string)e.NewValues[1];
            user.IsApproved = (bool)e.NewValues[2];

            try
            {
                // Update user info:
                Membership.Providers[theCallCenterConnectString].UpdateUser(user);

                
                // Update user roles:
                UpdateUserRoles();

                UserUpdateMessage.Text = "Update Successful.";

                e.Cancel = true;
                UserInfo.ChangeMode(DetailsViewMode.ReadOnly);
            }
            catch (Exception ex)
            {
                UserUpdateMessage.Text = "Update Failed: " + ex.Message;

                e.Cancel = true;
                UserInfo.ChangeMode(DetailsViewMode.ReadOnly);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Load the User Roles into checkboxes.
            UserRoles.DataSource = Roles.GetAllRoles();
            UserRoles.DataBind();
            if (!Roles.IsUserInRole("Administrators"))
            {
                UserRoles.Items.Remove("Administrators");
            }
            // Disable checkboxes if appropriate:
            //if (UserInfo.CurrentMode != DetailsViewMode.Edit)
            //{
            //    foreach (ListItem checkbox in UserRoles.Items)
            //    {
            //        checkbox.Enabled = false;
            //    }
            //}

            // Bind these checkboxes to the User's own set of roles.
            string[] userRoles = Roles.GetRolesForUser(username);
            foreach (string role in userRoles)
            {
                ListItem checkbox = UserRoles.Items.FindByValue(role);
                checkbox.Selected = true;
            }
        }

        protected void UpdateUserRoles()
        {
            foreach (ListItem rolebox in UserRoles.Items)
            {
                if (rolebox.Selected)
                {
                    if (!Roles.IsUserInRole(username, rolebox.Text))
                    {
                        Roles.AddUserToRole(username, rolebox.Text);
                    }
                }
                else
                {
                    if (Roles.IsUserInRole(username, rolebox.Text))
                    {
                        Roles.RemoveUserFromRole(username, rolebox.Text);
                    }
                }
            }
        }

        protected void DeleteUser(object sender, EventArgs e)
        {
            //Membership.DeleteUser(username, false); // DC: My apps will NEVER delete the related data.
            //Membership.Providers[theCallCenterConnectString].DeleteUser(username, true); // DC: except during testing, of course!
            Response.Redirect("ViewUsersByRole.aspx");
        }

        protected void UnlockUser(object sender, EventArgs e)
        {
            // Dan Clem, added 5/30/2007 post-live upgrade.

            // Unlock the user.
            user.UnlockUser();

            // DataBind the GridView to reflect same.
            UserInfo.DataBind();
        }

        protected string getFullname(Object theUsername)
        {

            string[] theAgentName = myLDAP.getFullName(theUsername.ToString(), Session["theCallCenterConnectString"].ToString());
            //Session["AgentFirstName"] = theAgentName[0];
            //Session["AgentLastName"] = theAgentName[1];


            TextBoxFname.Text = theAgentName[0].ToString().Trim();
            TextBoxLname.Text = theAgentName[1].ToString().Trim();

           

            if (!(TextBoxFname.Text == "" || TextBoxLname.Text == "")) { TextBoxLname.Enabled = false; TextBoxFname.Enabled = false; }

            return theAgentName[0].ToString() + " " + theAgentName[1].ToString();
        }

        protected void addFullName()
        {

            DataAccess.LDAPAccess theLDAP = new DataAccess.LDAPAccess();


            theLDAP.updateUserFullName(user.UserName, TextBoxFname.Text.Trim(), TextBoxLname.Text.Trim(), theCallCenterConnectString);



        }

        protected void UserInfo_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {

        }

        protected void EditUserRoles(object sender, EventArgs e)
        {
            try
            {
                // Update user info:
                // Membership.Providers[theCallCenterConnectString].UpdateUser(user);
                
                
                //update name:
                addFullName();
                // Update user roles:
                UpdateUserRoles();

                UserUpdateMessage.Text = "Update Successful.";

               // e.Cancel = true;
               // UserInfo.ChangeMode(DetailsViewMode.ReadOnly);
            }
            catch (Exception ex)
            {
                UserUpdateMessage.Text = "Update Failed: " + ex.Message;

               //e.Cancel = true;
                UserInfo.ChangeMode(DetailsViewMode.ReadOnly);
            }
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                //update password:
                Boolean ischanged = user.ChangePassword(CurrentPassword.Text, NewPassword.Text);

                UserUpdateMessage.Text = "Update Password " + ischanged.ToString();

                // e.Cancel = true;
                // UserInfo.ChangeMode(DetailsViewMode.ReadOnly);
            }
            catch (Exception ex)
            {
                UserUpdateMessage.Text = "Update Password Failed: " + ex.Message;

                //e.Cancel = true;
                UserInfo.ChangeMode(DetailsViewMode.ReadOnly);
            }
        }

        protected void CancelPushButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewUsersByRole.aspx");
        }

       
    }
}