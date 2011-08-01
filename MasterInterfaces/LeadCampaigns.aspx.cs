using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACMGAdmin.MasterInterfaces
{
    /// <summary>
    /// This class contains the Add / Edit functionality of LeadCampaign Module.
    /// <version>1.0</version>
    /// <lastmodifieddate>29-Jul-2011</lastmodifieddate>
    /// <author>CSC</author>
    /// </summary>
    public partial class LeadCampaigns : System.Web.UI.Page
    {
        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                // adding the Javascript function to the Save button
                btnSave.Attributes.Add("onclick", "return highlightModFields();");

                if (!IsPostBack)
                {
                    populateLeadCampaignGrid();
                }
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }
        }

        /// <summary>
        /// This event will be fired after the user selects the "Radiobutton" in the GridView
        /// </summary>
        protected void rdEditLeadCampaign_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                deSelect_RB_In_Gridview();

                System.Web.UI.WebControls.RadioButton senderRB = (System.Web.UI.WebControls.RadioButton)sender;
                senderRB.Checked = true;
                ConfirmationMessage.InnerText = "";

                // changing the back color of the controls in Edit mode...
                txtCampaignProdCode.BackColor = System.Drawing.Color.White;
                txtProductLine.BackColor = System.Drawing.Color.White;
                txtChannel.BackColor = System.Drawing.Color.White;
                txtTargusCode.BackColor = System.Drawing.Color.White;

                // setting the hdnCallCenterId in the hiddenfield - (to check modifications during Save button click)
                hdnLeadCampaignId.Value = senderRB.Text;
                populateSelectedRecord(Convert.ToInt32(hdnLeadCampaignId.Value));

                hdnFlagType.Value = "EDIT";
                // Clearing the error message in the form
                divErrorMsg.InnerText = "";
                btnSave.Enabled = true;
                btnAddNew.Enabled = true;
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message :" + ex.Message;

            }
        }


        /// <summary>
        /// This event will implement the paging functionality to the LeadCampaigns Gridview.
        /// </summary>
        protected void gvLeadCampaigns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLeadCampaigns.PageIndex = e.NewPageIndex;
                populateLeadCampaignGrid();
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }

        }

         /// <summary>
        /// This event will ADD / UPDATE the selected LeadCampaign record details in the database.
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    // Below fields will be used for logging functionality..
                    string strUser = "";
                    string strDateTime = DateTime.Now.ToString();
                    string strScreenName = "LeadCampaigns";
                    string strTableName = "tbl_leadcampaigns";
                    StringBuilder strBeforeImage = new StringBuilder();
                    StringBuilder strAfterImage = new StringBuilder();

                    strUser = User.Identity.Name;
                    strBeforeImage.Append(hdnLeadCampaignId.Value + " || " + hdnCampaignProductCode.Value + " || " + hdnProductLine.Value + " || " +
                                            hdnChannel.Value + " || " + hdnTargusCode.Value);

                    strAfterImage.Append(hdnLeadCampaignId.Value + " || " + txtCampaignProdCode.Text + " || " + txtProductLine.Text+ " || " +
                                            txtChannel.Text + " || " + txtTargusCode.Text);

                    if (hdnFlagType.Value == "EDIT")
                    {

                        DataAccess.MySQLAccess myUpdObj = new DataAccess.MySQLAccess();
                        int iOutput = myUpdObj.updateLeadCampaign("LocalMySqlServer", Convert.ToInt32(hdnLeadCampaignId.Value), txtCampaignProdCode.Text,
                                                            txtProductLine.Text, txtChannel.Text, txtTargusCode.Text,
                                                            strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());

                        //checking whether record has been updated
                        if (iOutput > 0)
                        {
                            populateLeadCampaignGrid();
                            ConfirmationMessage.InnerText = "Updated Successfully !";

                            populateHiddenFields();
                        }
                    }

                    if (hdnFlagType.Value == "ADD")
                    {
                        // call the routine to add a new LeadCampaign record into the table..
                        DataAccess.MySQLAccess myLeadCampaignObj = new DataAccess.MySQLAccess();
                        int intInsOut = myLeadCampaignObj.insertLeadCampaign("LocalMySqlServer", txtCampaignProdCode.Text, txtProductLine.Text, 
                                                                                txtChannel.Text, txtTargusCode.Text,
                                                                                strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());
                        if (intInsOut > 0)
                        {
                            populateLeadCampaignGrid();
                            ConfirmationMessage.InnerText = "Inserted Successfully !";

                            populateHiddenFields();
                        }

                    }
                    btnAddNew.Enabled = true;


                }
            }
            catch (Exception ex)
            {
                if (hdnFlagType.Value == "EDIT")
                {
                    divErrorMsg.InnerText = "Update Failure: " + ex.Message;
                }

                if (hdnFlagType.Value == "ADD")
                {
                    divErrorMsg.InnerText = "Insert Failure: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// This event will initalize the variables and make the screen ready for "Add Mode".
        /// </summary>
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                hdnFlagType.Value = "ADD";
                clearAllFieldValues();
                btnSave.Enabled = true;
                //disabling the AddNew field
                btnAddNew.Enabled = false;
                hdnLeadCampaignId.Value = "";
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message :" + ex.Message;
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// this function will fetch the values from Database and populate the Lead Campaign Gridview.
        /// </summary>
        private void populateLeadCampaignGrid()
        {
            try
            {
                // code to populate the Lead Campaign data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsLeadCampaigns = new DataSet();
                dsLeadCampaigns = myObj.getLeadCampaigns("LocalMySqlServer");

                DataTable dtLeadCampaigns = new DataTable();
                dtLeadCampaigns = dsLeadCampaigns.Tables[0];

                gvLeadCampaigns.DataSource = dtLeadCampaigns;
                gvLeadCampaigns.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// this function will takes the LeadCampaignID as input and selects the Lead Campaign record values from DB and
        /// populate in the screen.
        /// </summary>
        private void populateSelectedRecord(int iLeadCampaignID)
        {
            try
            {
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsSelPhoneExt = new DataSet();
                dsSelPhoneExt = myObj.getLeadCampaignsByID("LocalMySqlServer", iLeadCampaignID);

                // Assigning the selected Dailer Rule values to the corresponding fields...
                hdnLeadCampaignId.Value = dsSelPhoneExt.Tables[0].Rows[0]["LeadCampaignID"].ToString();
                txtCampaignProdCode.Text = dsSelPhoneExt.Tables[0].Rows[0]["CampaignProductCode"].ToString();
                txtProductLine.Text = dsSelPhoneExt.Tables[0].Rows[0]["ProductLine"].ToString();
                txtChannel.Text = dsSelPhoneExt.Tables[0].Rows[0]["Channel"].ToString();
                txtTargusCode.Text = dsSelPhoneExt.Tables[0].Rows[0]["TargusCode"].ToString();

                // setting the values in the hidden field to identify the modifications during the Save button click..
                hdnLeadCampaignId.Value = dsSelPhoneExt.Tables[0].Rows[0]["LeadCampaignID"].ToString();
                hdnCampaignProductCode.Value = dsSelPhoneExt.Tables[0].Rows[0]["CampaignProductCode"].ToString();
                hdnProductLine.Value = dsSelPhoneExt.Tables[0].Rows[0]["ProductLine"].ToString();
                hdnChannel.Value = dsSelPhoneExt.Tables[0].Rows[0]["Channel"].ToString();
                hdnTargusCode.Value = dsSelPhoneExt.Tables[0].Rows[0]["TargusCode"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// this function will take the Textbox values and populate the hidden fields  
        /// & this is used for verifying the values modified in the screen.
        /// </summary>
        private void populateHiddenFields()
        {
            try
            {
                //storing the values in the hidden fields to verify the modification & to highlight in the screen..
                hdnCampaignProductCode.Value = txtCampaignProdCode.Text;
                hdnProductLine.Value = txtProductLine.Text;
                hdnChannel.Value = txtChannel.Text;
                hdnTargusCode.Value = txtTargusCode.Text;
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
                foreach (GridViewRow gvRow in gvLeadCampaigns.Rows)
                {
                    System.Web.UI.WebControls.RadioButton rb = (System.Web.UI.WebControls.RadioButton)gvLeadCampaigns.Rows[iCnt].FindControl("rdEditLeadCampaign");
                    rb.Checked = false;
                    iCnt = iCnt + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// this function will clear all the field values in the screen & it will be called before "Adding" new record.
        /// </summary>
        private void clearAllFieldValues()
        {
            try
            {
                // calling the populateLeadCampaignGrid to refresh the Grid Data
                populateLeadCampaignGrid();

                // setting the default values for the controls before adding a new record...
                txtCampaignProdCode.Text = "";
                txtCampaignProdCode.BackColor = System.Drawing.Color.White;
                txtProductLine.Text = "";
                txtProductLine.BackColor = System.Drawing.Color.White;
                txtChannel.Text = "";
                txtChannel.BackColor = System.Drawing.Color.White;
                txtTargusCode.Text = "";
                txtTargusCode.BackColor = System.Drawing.Color.White;
                ConfirmationMessage.InnerText = "";

                // clearing the values from the hidden controls...
                hdnCampaignProductCode.Value = "";
                hdnProductLine.Value = "";
                hdnChannel.Value = "";
                hdnTargusCode.Value = "";
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}