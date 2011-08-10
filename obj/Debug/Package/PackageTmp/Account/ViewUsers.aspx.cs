using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ACMGAdmin.Account
{
    public partial class ViewUsers : System.Web.UI.Page
    {
        int pageSize = 10;
        int totalUsers;
        int totalPages;
        int currentPage = 0;
        DataAccess.LDAPAccess myLDAP=new DataAccess.LDAPAccess();
            

        public void Page_Load()
        {
            
            
            if (!IsPostBack)
            {
                GetUsers();
            }
        }

        private void GetUsers()
        {


            string theCallCenterConnectString = Session["theCallCenterConnectString"].ToString();
            //MembershipProvider theProvider = Membership.Providers[theCallCenterConnectString];
            MembershipUserCollection theUsers = Membership.Providers[theCallCenterConnectString].GetAllUsers(currentPage, pageSize, out totalUsers);
            //Users.DataSource = theUsers;// Membership.GetAllUsers();
            // Users.DataBind();


            LabelUserList.Text = "User List for " + Session["theCallCenterName"].ToString();
            //UsersOnlineLabel.Text = Membership.Providers[theCallCenterConnectString].GetNumberOfUsersOnline().ToString();

            UserGrid.DataSource = theUsers;//Membership.GetAllUsers(currentPage, pageSize, out totalUsers);
            totalPages = ((totalUsers - 1) / pageSize) + 1;

            // Ensure that we do not navigate past the last page of users.

            if (currentPage > totalPages)
            {
                currentPage = totalPages;
                GetUsers();
                return;
            }

            UserGrid.DataBind();
            CurrentPageLabel.Text = currentPage.ToString();
            TotalPagesLabel.Text = totalPages.ToString();

            if (currentPage == totalPages)
                NextButton.Visible = false;
            else
                NextButton.Visible = true;

            if (currentPage == 1)
                PreviousButton.Visible = false;
            else
                PreviousButton.Visible = true;

            if (totalUsers <= 0)
                NavigationPanel.Visible = false;
            else
                NavigationPanel.Visible = true;
        }

        public void NextButton_OnClick(object sender, EventArgs args)
        {
            currentPage = Convert.ToInt32(CurrentPageLabel.Text);
            currentPage++;
            GetUsers();
        }

        public void PreviousButton_OnClick(object sender, EventArgs args)
        {
            currentPage = Convert.ToInt32(CurrentPageLabel.Text);
            currentPage--;
            GetUsers();
        }

        protected string getFullname(Object theUsername)
        {

            string[] theAgentName = myLDAP.getFullName(theUsername.ToString(), Session["theCallCenterConnectString"].ToString());
            //Session["AgentFirstName"] = theAgentName[0];
            //Session["AgentLastName"] = theAgentName[1];

            return theAgentName[0].ToString() + " " + theAgentName[1].ToString() ;
        }
    }
}