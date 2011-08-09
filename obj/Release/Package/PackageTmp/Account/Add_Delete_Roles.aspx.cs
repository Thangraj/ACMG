using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ACMGAdmin.Account
{
    public partial class Add_Delete_Roles : System.Web.UI.Page
    {
        
        

        private void RefreshAvailableRolesListBox()
        {
            lbxAvailableRoles.SelectedIndex = -1;
            lbxAvailableRoles.DataSource = Roles.GetAllRoles();
            lbxAvailableRoles.DataBind();

            if (lbxAvailableRoles.Items.Count == 0)
            {
                lblRoleInfoText.Text = "There are currently no roles:";
                lbxAvailableRoles.Visible = false;
                btnDeleteRole.Visible = false;
            }
            else
            {
                lblRoleInfoText.Text = "Available Roles:";
                lbxAvailableRoles.Visible = true;
                btnDeleteRole.Visible = true;
            }
        }

        void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Roles.RoleExists("Administrators"))
                {
                    txtCreateRole.Text = "Administrators";
                }
            }
        }

        void Page_Init(object sender, EventArgs e)
        {
            RefreshAvailableRolesListBox();
        }

        protected void btnCreateRole_Click1(object sender, EventArgs e)
        {
            string roleName = txtCreateRole.Text;

            try
            {
                Roles.CreateRole(roleName);

                lblResults.Text = null;
                lblResults.Visible = false;

                txtCreateRole.Text = null;
            }
            catch (Exception ex)
            {
                lblResults.Text = "Could not create the role: " + Server.HtmlEncode(ex.Message);
                lblResults.Visible = true;
            }

            RefreshAvailableRolesListBox();
        }

        protected void btnDeleteRole_Click1(object sender, EventArgs e)
        {
            if (lbxAvailableRoles.SelectedIndex != -1)
            {
                try
                {
                    Roles.DeleteRole(lbxAvailableRoles.SelectedValue);

                    lblResults.Text = null;
                    lblResults.Visible = false;
                }
                catch (Exception ex)
                {
                    lblResults.Text = "Could not delete the role: " + Server.HtmlEncode(ex.Message);
                    lblResults.Visible = true;
                }
            }

            RefreshAvailableRolesListBox();
        }  

    }
}