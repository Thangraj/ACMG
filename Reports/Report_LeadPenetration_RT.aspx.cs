using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACMGAdmin.Reports
{
    public partial class Report_LeadPenetration_RT : System.Web.UI.Page
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

        //protected void AgentsBound(object sender, EventArgs e)
        //{
        //    DropDownListAgent.Items.Insert(0, new ListItem("ALL","ALL"));
        //}

        //protected void CallCenterBound(object sender, EventArgs e)
        //{
        //    DropDownListCallCenter.Items.Insert(0, new ListItem("ALL","0"));
        //}
    }
}