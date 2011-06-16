using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ACMGAdmin.Account
{
    public partial class ViewUsersByRole : System.Web.UI.Page
    {

        int pageSize = 500;
        int totalUsers;
        int totalPages;
        int currentPage = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }




        protected void Page_Init(object sender, EventArgs e)
        {
            UserRoles.DataSource = Roles.GetAllRoles();
            UserRoles.DataBind();
            LabelUserList.Text = "User List for " + Session["theCallCenterName"].ToString();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            
            string theCallCenterConnectString = Session["theCallCenterConnectString"].ToString();
            
            MembershipUserCollection allUsers = Membership.Providers[theCallCenterConnectString].GetAllUsers(currentPage, pageSize, out totalUsers);//Membership.GetAllUsers();
            MembershipUserCollection filteredUsers = new MembershipUserCollection();

            if (UserRoles.SelectedIndex > 0)
            {
                string[] usersInRole = Roles.GetUsersInRole(UserRoles.SelectedValue);
                foreach (MembershipUser user in allUsers)
                {
                    foreach (string userInRole in usersInRole)
                    {
                        if (userInRole == user.UserName)
                        {
                            filteredUsers.Add(user);
                            break; // Breaks out of the inner foreach loop to avoid unneeded checking.
                        }
                    }
                }
            }
            else
            {
                filteredUsers = allUsers;
            }
            UserGrid.DataSource = filteredUsers;
            UserGrid.DataBind();
        }

       



    }
}