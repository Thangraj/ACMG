 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

namespace ACMGAdmin
{
	public partial class ACMG : System.Web.UI.MasterPage
	{

        //protected void Page_Init(object sender, EventArgs e)

            

        //{

        //    if (Session.IsNewSession) {
        //        //remove authentication cookie
        //        FormsAuthentication.SignOut(); 
        //        Response.Redirect("~/Account/Login.aspx"); 
            
            
        //    }
			


			




        //}
		
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //    string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        //    LabelNotify.Text = "Outbound Management and Administration Portal V." + version;

			
		 

        //}


        //protected void HeadLoginStatus_LoggedOut(Object sender, System.EventArgs e)
        //{

        //    Session.Clear();

        //    Session.RemoveAll();

        //    Session.Abandon();



        //}





        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session.IsNewSession)
            {
                //remove authentication cookie
                FormsAuthentication.SignOut();
                Response.Redirect("~/Account/Login.aspx");


            }
            if (Request.Path == "/Compliance/LiveCallMonitor.aspx" && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)//if agent page then add code to open cleanup window
            {
                 
                //= @"NewWin=window.open('/Agent/PreviewDialAgent.aspx?w=' + screen.width + '&h=' + screen.height ,null,'width=' + screen.width + ', height=' + screen.height + ', top=0, left=0, scrollbars=yes,resizable=yes,menubar=no,toolbar=no,location=no,directories=no,status=no');window.open('','_self','');setTimeout('self.close();',5000);";
                string myScriptClose = @"function sessioncleanup() {window.open('/Account/Cleanup.aspx','_blank','width=500'+',height=500'+',screenX=0,screenY=0,directories=0,fullscreen=0,location=0,menubar=0,scrollbars=0,status=0,toolbar=0');  }";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScriptClose", myScriptClose, true);
                //make menu invisible
                this.LoginView1.Visible = false;

            }
            else
            {

                string myScriptClose = @"function sessioncleanup() {return; }"; //{alert('Window Unloaded'); }";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScriptClose", myScriptClose, true);
            }
            if (IsPostBack == false)
            {
                Page.ClientScript.RegisterClientScriptInclude("PhoneScript", "/Phone/phonecontrol.js?" + DateTime.Now.ToString("yyMMddHHmmss"));


                if (Request.IsSecureConnection)
                {
                    Page.ClientScript.RegisterClientScriptInclude("DeployJavaScript", "https://www.java.com/js/deployJava.js");
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptInclude("DeployJavaScript", "http://www.java.com/js/deployJava.js");
                }



                if (IsPostBack == false & Session["PhoneExtensionAssignmentID"] == null & Request.Path == "/Compliance/LiveCallMonitor.aspx")
                {//if this is the agent interface then get an extension to use
                    GetExtension();

                    LiteralthePhone.Text = "<script > var attributes = { id: 'webphone', code: 'webphone.webphone.class', name: 'webphone', archive: 'webphone.jar', codebase: '/Phone/', width: '260', height: '50', MAYSCRIPT: true };";
                    LiteralthePhone.Text += " var parameters = { JAVA_CODEBASE: '/Phone/', usepcmu: '3', compact: 'true', call: 'false', ";
                    LiteralthePhone.Text += "	 MAYSCRIPT: 'true', mayscript: 'yes', scriptable: 'true', jsscriptevent: '3', autocfgsave: '0', classloader_cache: false, ";
                    LiteralthePhone.Text += "  transfertype: '1', discontransfer: '4', maxlines:'1', ";
                    LiteralthePhone.Text += "	 haschat: '1', hascall: '0', serveraddress: '" + Session["SwitchAddress"].ToString() + "', username: '" + Session["UserName"].ToString() + "', password:'" + Session["Password"].ToString() + "' , register: true };";
                    LiteralthePhone.Text += "   deployJava.runApplet(attributes, parameters, '1.4');	</script>";

                    //add phone control

                    //LiteralthePhoneControl.Text = "<div id='phone2'><input type='button' value='Call:' onclick='voipCall(txtCall2.value)'";
                    //LiteralthePhoneControl.Text += " style='width: 60px;  color: #124986; margin-right: 5px;' />";
                    //LiteralthePhoneControl.Text += " <input id='txtCall2' type='text' /> <input type='button' value='Hold' onclick='voipHold()' ";
                    //LiteralthePhoneControl.Text += " style='width: 70px; color: #124986;' /><input type='button' value='Hold Off' onclick='voipUNHold()'";
                    //LiteralthePhoneControl.Text += " style='width: 70px; color: #124986; margin-right: 5px;' /><input type='button' value='Hangup' onclick='voipHangup()' ";
                    //LiteralthePhoneControl.Text += " style='width: 70px; color: #124986;' /><input type='button' value='Transfer' onclick='voipTransferDialog()' ";
                    //LiteralthePhoneControl.Text += " style='width: 70px; color: #124986; margin-right: 10px;' /> <input type='button' value='Audio Set' onclick='voipSetSound()'  style='width: 75px; color: #124986;' /> </div>";

                    //admin phone control
                    LiteralthePhoneControl.Text = "<div id='phone2'>";
                    LiteralthePhoneControl.Text += " <input type='button' value='Hangup' onclick='voipHangup()' style='width: 70px; color: #124986;' />";
                    LiteralthePhoneControl.Text += " <input type='button' value='Audio Set' onclick='voipSetSound()'  style='width: 75px; color: #124986;' /> </div>";


                }

                if (Session["PhoneExtensionAssignmentID"] != null & Request.Path == "/Compliance/LiveCallMonitor.aspx")
                {//if this is the agent interface then get an extension to use

                    LiteralthePhone.Text = "<script > var attributes = { id: 'webphone', code: 'webphone.webphone.class', name: 'webphone', archive: 'webphone.jar', codebase: '/Phone/', width: '260', height: '50', MAYSCRIPT: true };";
                    LiteralthePhone.Text += " var parameters = { JAVA_CODEBASE: '/Phone/', usepcmu: '3', compact: 'true', call: 'false', ";
                    LiteralthePhone.Text += "	 MAYSCRIPT: 'true', mayscript: 'yes', scriptable: 'true', jsscriptevent: '3', autocfgsave: '0', classloader_cache: false, ";
                    LiteralthePhone.Text += "  transfertype: '1', discontransfer: '4', maxlines:'1', ";
                    LiteralthePhone.Text += "	 haschat: '1', hascall: '0', serveraddress: '" + Session["SwitchAddress"].ToString() + "', username: '" + Session["UserName"].ToString() + "', password:'" + Session["Password"].ToString() + "' , register: true };";
                    LiteralthePhone.Text += "   deployJava.runApplet(attributes, parameters, '1.4');	</script>";

                    //add phone control

                    //LiteralthePhoneControl.Text = "<div id='phone2'><input type='button' value='Call:' onclick='voipCall(txtCall2.value)'";
                    //LiteralthePhoneControl.Text += " style='width: 60px;  color: #124986; margin-right: 5px;' />";
                    //LiteralthePhoneControl.Text += " <input id='txtCall2' type='text' /> <input type='button' value='Hold' onclick='voipHold()' ";
                    //LiteralthePhoneControl.Text += " style='width: 70px; color: #124986;' /><input type='button' value='Hold Off' onclick='voipUNHold()'";
                    //LiteralthePhoneControl.Text += " style='width: 70px; color: #124986; margin-right: 5px;' /><input type='button' value='Hangup' onclick='voipHangup()' ";
                    //LiteralthePhoneControl.Text += " style='width: 70px; color: #124986;' /><input type='button' value='Transfer' onclick='voipTransferDialog()' ";
                    //LiteralthePhoneControl.Text += " style='width: 70px; color: #124986; margin-right: 10px;' /> <input type='button' value='Audio Set' onclick='voipSetSound()'  style='width: 75px; color: #124986;' /> </div>";

                    //admin phone control
                    LiteralthePhoneControl.Text = "<div id='phone2'>";
                    LiteralthePhoneControl.Text += " <input type='button' value='Hangup' onclick='voipHangup()' style='width: 70px; color: #124986;' />";
                    LiteralthePhoneControl.Text += " <input type='button' value='Audio Set' onclick='voipSetSound()'  style='width: 75px; color: #124986;' /> </div>";

                }




            }




        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //string theBrowser = Request.Browser.Browser.ToString();
            //if (theBrowser != "IE") { Response.Redirect("/UseIE.htm"); }
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            LabelNotify.Text = "Outbound Management and Administration Portal V." + version;


        }


        protected void HeadLoginStatus_LoggedOut(Object sender, System.EventArgs e)
        {
            

            //release extension
            ReleaseExtension();
            //CLEAR SESSION
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");

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

        protected void GetExtension()
        {


            if (Session["PhoneExtensionAssignmentID"] != null)
            { return; }// LabelNotify.Text = "already have an extension"; return; }//already have an extension
            //if (Session["AgentLoginID"] == null)
            //{ return; }//{ LabelNotify.Text = "must login first"; return; }//must login first
            string[] theResult;

            DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();

            theResult = myObj.getExtensionAdmin(null, Session["Agent"].ToString(), Session["Company"].ToString(), "LocalMySqlServer");
            //if (theResult[0] == null) { LabelGetextension.Text = "cound not get an extension to use"; }
            Session["PhoneExtensionAssignmentID"] = theResult[0];
            Session["PhoneExtensionID"] = theResult[1];
            Session["SwitchName"] = theResult[2];
            Session["SwitchAddress"] = theResult[3];
            Session["SwitchPort"] = theResult[4];
            Session["Extension"] = theResult[5];
            Session["UserName"] = theResult[6];
            Session["Password"] = theResult[7];
            //LabelNotify.Text = Session["AgentLoginID"].ToString() + Session["PhoneExtensionAssignmentID"].ToString() + ":" + Session["PhoneExtensionID"].ToString() + ":" + Session["SwitchName"].ToString()
            //   + ":" + Session["SwitchAddress"].ToString() + ":" + Session["SwitchPort"].ToString() + ":" + Session["Extension"].ToString() + ":" +
            //    Session["UserName"].ToString() + ":" + Session["Password"].ToString();


        }
		
		
		
		
	}
}