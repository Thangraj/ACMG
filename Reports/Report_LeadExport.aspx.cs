using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACMGAdmin.Reports
{
    public partial class Report_LeadExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (txtStartDate.Text == "") { txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy"); }
            if (txtEndDate.Text == "") { txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy"); }
            
        }

        protected void RunReport_Click(object sender, EventArgs e)
        {
           
           this.ReportViewer1.LocalReport.Refresh();
        
        }

        
        

        protected void ProductLineBound(object sender, EventArgs e)
        {
            DropDownListProductLine.Items.Insert(0, new ListItem("ALL", "ALL"));
        }

        //protected void PhoneTypeBound(object sender, EventArgs e)
        //{
        //    DropDownListPhoneType.Items.Insert(0, new ListItem("ALL", "ALL"));
        //    DropDownListPhoneType.Items.Insert(0, new ListItem("Land Line", "Land Line"));
        //    DropDownListPhoneType.Items.Insert(0, new ListItem("Cell Phone", "Cell Phone"));
        //}
    }
}