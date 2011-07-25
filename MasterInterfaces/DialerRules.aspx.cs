using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ACMGAdmin.MasterInterfaces
{
    /// <summary>
    /// This class contains the Edit functionality of DialerRules Module.
    /// <version>1.0</version>
    /// <lastmodifieddate>18-Jul-2011</lastmodifieddate>
    /// <author>CSC</author>
    /// </summary>

    public partial class DialerRules : System.Web.UI.Page
    {
        #region Protected Methods

        /// <summary>
        /// On page-load event setting the attributes to the control and populating the Gridview 
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                // adding the Javascript function to the Save button
                btnSave.Attributes.Add("onclick", "return highlightModFields();");
    
                if (!IsPostBack)
                {
                    populateDialerRuleGrid();
                }
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }
          
            
        }

        protected void gvDialerRules_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDialerRules.PageIndex = e.NewPageIndex;
                populateDialerRuleGrid();
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }
            
        }

        /// <summary>
        /// This event will be fired after the user selects the "Radiobutton" in the GridView
        /// </summary>
        protected void rdEditDialer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                deSelect_RB_In_Gridview();

                System.Web.UI.WebControls.RadioButton senderRB = (System.Web.UI.WebControls.RadioButton)sender;
                senderRB.Checked = true;
                ConfirmationMessage.InnerText = "";
                // setting the DialerRulesId in the hiddenfield - (to check modifications during Save button click)
                hdnDialerRuleId.Value = senderRB.Text;
                populateSelectedRecord(Convert.ToInt32(hdnDialerRuleId.Value));
            }
            catch(Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:"+ex.Message ;
            }
        }

        /// <summary>
        /// This event will update the selected DialerRule records details in the database.
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    DataAccess.MySQLAccess myUpdObj = new DataAccess.MySQLAccess();
                    int iOutput = myUpdObj.updateDialerRules("LocalMySqlServer", Convert.ToInt32(hdnDialerRuleId.Value), Convert.ToInt32(hdnCampaignId.Value), txtCmpame.Text, txtPhoneType.Text, Convert.ToInt32(txtDaysBetDials.Text),
                                                            Convert.ToInt32(txtHrsBetDials.Text), Convert.ToInt32(txtMinsBetDials.Text), Convert.ToInt32(txtMaxAttempts.Text), Convert.ToInt32(txtMaxDaysInpool.Text), txtStartTimeEST.Text,
                                                            txtEndTimeEST.Text, Convert.ToInt32(rblHolidays.SelectedValue), Convert.ToInt32(rblActive.SelectedValue), Convert.ToInt32(txtPriority.Text), Convert.ToInt32(txtArchiveAfterDays.Text));

                    //checking whether record has been updated
                    if (iOutput > 0)
                    {
                        populateDialerRuleGrid();
                        ConfirmationMessage.InnerText = "Updated Successfully !";

                        // store the values in the hidden fields to verify the modified fields...
                        hdnDaysBetDials.Value = txtDaysBetDials.Text;
                        hdnHrsBetDials.Value = txtHrsBetDials.Text;
                        hdnMinsBetDials.Value = txtMinsBetDials.Text;
                        hdnMaxAttempts.Value = txtMaxAttempts.Text;
                        hdnMaxDaysInpool.Value = txtMaxDaysInpool.Text;
                        hdnStartTimeEST.Value = txtStartTimeEST.Text;
                        hdnEndTimeEST.Value = txtEndTimeEST.Text;
                        hdnDialHolidays.Value = rblHolidays.SelectedValue;
                        hdnDialActive.Value = rblActive.SelectedValue;
                        hdnPriority.Value = txtPriority.Text;
                        hdnArchiveAfterDays.Value = txtArchiveAfterDays.Text;

                    }
                }
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:"+ex.Message ;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// this function will fetch the values from Database and populate the DialerRules Gridview.
        /// </summary>
        private void populateDialerRuleGrid()
        {
            try
            {
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsDialerRules = new DataSet();
                dsDialerRules = myObj.getDialerRules("LocalMySqlServer");

                DataTable dtDialerRules = new DataTable();
                dtDialerRules = dsDialerRules.Tables[0];

                gvDialerRules.DataSource = dtDialerRules;
                gvDialerRules.DataBind();
                // putting the datatable in session 
                Session["dtDialerRules"] = dtDialerRules;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// this function will takes the DialerRulesID as input and selects the remaining DialerRules record values from DB and
        /// populate in the screen.
        /// </summary>
        private void populateSelectedRecord(int iDialerRulesID)
        {
            try
            {
                string strDailholidays = "";
                string strDialActive = "";
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsSelDialerRules = new DataSet();
                dsSelDialerRules = myObj.getDialerRulesByID("LocalMySqlServer", iDialerRulesID);

                // Assigning the selected Dailer Rule values to the corresponding fields...
                hdnCampaignId.Value = dsSelDialerRules.Tables[0].Rows[0]["CampaignID"].ToString();
                txtCmpame.Text = dsSelDialerRules.Tables[0].Rows[0]["CampaignName"].ToString();
                txtPhoneType.Text = dsSelDialerRules.Tables[0].Rows[0]["PhoneType"].ToString();
                txtDaysBetDials.Text = dsSelDialerRules.Tables[0].Rows[0]["DaysBetweenDials"].ToString();
                txtHrsBetDials.Text = dsSelDialerRules.Tables[0].Rows[0]["HoursBetweenDials"].ToString();
                txtMinsBetDials.Text = dsSelDialerRules.Tables[0].Rows[0]["MinutesBetweenDials"].ToString();
                txtMaxAttempts.Text = dsSelDialerRules.Tables[0].Rows[0]["MaxAttempts"].ToString();
                txtMaxDaysInpool.Text = dsSelDialerRules.Tables[0].Rows[0]["MaxDaysInPool"].ToString();
                txtStartTimeEST.Text = dsSelDialerRules.Tables[0].Rows[0]["StartDialTime_EST"].ToString();
                txtEndTimeEST.Text = dsSelDialerRules.Tables[0].Rows[0]["EndDialTime_EST"].ToString();

                // converting the Boolean values (True, False) to 1 & 0 respectively...
                if ((dsSelDialerRules.Tables[0].Rows[0]["DialOnHolidays"].ToString() == "Yes"))
                {
                    strDailholidays = "1";
                }
                else if ((dsSelDialerRules.Tables[0].Rows[0]["DialOnHolidays"].ToString() == "No") || (dsSelDialerRules.Tables[0].Rows[0]["DialOnHolidays"] == null))
                {
                    strDailholidays = "0";
                }

                if ((dsSelDialerRules.Tables[0].Rows[0]["DialActive"].ToString() == "Yes"))
                {
                    strDialActive = "1";
                }
                else if ((dsSelDialerRules.Tables[0].Rows[0]["DialActive"].ToString() == "No") || (dsSelDialerRules.Tables[0].Rows[0]["DialActive"] == null))
                {
                    strDialActive = "0";
                }

                rblHolidays.SelectedValue = strDailholidays;
                rblActive.SelectedValue = strDialActive;
                txtPriority.Text = dsSelDialerRules.Tables[0].Rows[0]["DialPriority"].ToString();
                txtArchiveAfterDays.Text = dsSelDialerRules.Tables[0].Rows[0]["ArchiveAfterDays"].ToString();


                // setting the values in the hidden field to identify the modifications during the Save button click..
                 hdnDaysBetDials.Value = dsSelDialerRules.Tables[0].Rows[0]["DaysBetweenDials"].ToString();
                 hdnHrsBetDials.Value = dsSelDialerRules.Tables[0].Rows[0]["HoursBetweenDials"].ToString();
                 hdnMinsBetDials.Value = dsSelDialerRules.Tables[0].Rows[0]["MinutesBetweenDials"].ToString();
                 hdnMaxAttempts.Value = dsSelDialerRules.Tables[0].Rows[0]["MaxAttempts"].ToString();
                 hdnMaxDaysInpool.Value = dsSelDialerRules.Tables[0].Rows[0]["MaxDaysInPool"].ToString();
                 hdnStartTimeEST.Value = dsSelDialerRules.Tables[0].Rows[0]["StartDialTime_EST"].ToString();
                 hdnEndTimeEST.Value = dsSelDialerRules.Tables[0].Rows[0]["EndDialTime_EST"].ToString();
                 hdnDialHolidays.Value = strDailholidays;
                 hdnDialActive.Value = strDialActive;
                 hdnPriority.Value = dsSelDialerRules.Tables[0].Rows[0]["DialPriority"].ToString();
                 hdnArchiveAfterDays.Value = dsSelDialerRules.Tables[0].Rows[0]["ArchiveAfterDays"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// this function will deselect all the other RadioButton and enable the selected Radiobutton only..
        /// </summary>
        
        private void deSelect_RB_In_Gridview()
        {
            try
            {
                Int32 iCnt = 0;
                foreach (GridViewRow gvRow in gvDialerRules.Rows)
                {
                    System.Web.UI.WebControls.RadioButton rb = (System.Web.UI.WebControls.RadioButton)gvDialerRules.Rows[iCnt].FindControl("rdEditDialer");
                    rb.Checked = false;
                    iCnt = iCnt + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        
    }
}