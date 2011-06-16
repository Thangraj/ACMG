using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACMGAdmin.Compliance
{
    public partial class LiveCallMonitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserName"] != null)
            
            {

                LiteralFOP.Text = "<iframe src='http://npx1601908.aretta.net/fop2/?exten=" + Session["UserName"].ToString() + "&pass=1234' width='100%' height='600px'></iframe>";
  
 


            }
        }
    }
}