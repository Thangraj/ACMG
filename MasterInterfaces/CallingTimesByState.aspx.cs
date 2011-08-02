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
    /// This class contains the Add / Edit functionality of CallingTimesByState Module.
    /// <version>1.0</version>
    /// <lastmodifieddate>01-Aug-2011</lastmodifieddate>
    /// <author>CSC</author>
    /// </summary>

    public partial class CallingTimesByState : System.Web.UI.Page
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
                    populateCallingTimesGrid();
                }
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }

        }


        /// <summary>
        /// This event will implement the paging functionality to the CallingTimesByState Gridview.
        /// </summary>
        protected void gvCallingTimesByState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCallingTimesByState.PageIndex = e.NewPageIndex;
                populateCallingTimesGrid();
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }

        }

        /// <summary>
        /// This event will be fired after the user selects the "Radiobutton" in the GridView
        /// </summary>
        protected void rdEditCallingTimes_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                deSelect_RB_In_Gridview();

                System.Web.UI.WebControls.RadioButton senderRB = (System.Web.UI.WebControls.RadioButton)sender;
                senderRB.Checked = true;
                ConfirmationMessage.InnerText = "";

                // changing the back color of the controls in Edit mode...
                txtState.BackColor = System.Drawing.Color.White;
                txtWeekdayStartTime.BackColor = System.Drawing.Color.White;
                txtWeekdayEndTime.BackColor = System.Drawing.Color.White;
                txtSaturdayStartTime.BackColor = System.Drawing.Color.White;
                txtSaturdayEndTime.BackColor = System.Drawing.Color.White;
                txtSundayStartTime.BackColor = System.Drawing.Color.White;
                txtSundayEndTime.BackColor = System.Drawing.Color.White;
                txtHolidayStartTime.BackColor = System.Drawing.Color.White;
                txtHolidayEndTime.BackColor = System.Drawing.Color.White;

                // setting the hdnCallCenterId in the hiddenfield - (to check modifications during Save button click)
                hdnState.Value = senderRB.Text;
                populateSelectedRecord(hdnState.Value);

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
        /// This event will ADD / UPDATE the selected CallingTimesByState records details in the database.
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
                    string strScreenName = "CallingTimesByState";
                    string strTableName = "tbl_callingtimesbystate";
                    StringBuilder strBeforeImage = new StringBuilder();
                    StringBuilder strAfterImage = new StringBuilder();

                    strUser = User.Identity.Name;
                    strBeforeImage.Append(hdnState.Value + " || " + hdnWeekdayStartTime.Value + " || " + hdnWeekdayEndTime.Value + " || " +
                                           hdnSaturdayStartTime.Value + " || " + hdnSaturdayEndTime.Value + " || " + hdnSundayStartTime.Value  + " || " +
                                           hdnSundayEndTime.Value + " || " + hdnHolidayStartTime.Value + " || "+ hdnHolidayEndTime.Value
                                          );

                    strAfterImage.Append(txtState.Text + " || " + txtWeekdayStartTime.Text + " || " + txtWeekdayEndTime.Text + " || " +
                                            txtSaturdayStartTime.Text + " || " + txtSaturdayEndTime.Text + " || "+
                                            txtSundayStartTime.Text + " || " + txtSundayEndTime.Text + " || " + txtHolidayStartTime.Text + " || " +
                                            txtHolidayEndTime.Text);

                    if (hdnFlagType.Value == "EDIT")
                    {

                        DataAccess.MySQLAccess myUpdObj = new DataAccess.MySQLAccess();
                        int iOutput = myUpdObj.updCallingTimesByState("LocalMySqlServer", txtState.Text, txtWeekdayStartTime.Text, txtWeekdayEndTime.Text, txtSaturdayStartTime.Text, 
                                                            txtSaturdayEndTime.Text, txtSundayStartTime.Text, txtSundayEndTime.Text, txtHolidayStartTime.Text, txtHolidayEndTime.Text,
                                                            strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());

                        //checking whether record has been updated
                        if (iOutput > 0)
                        {
                            populateCallingTimesGrid();
                            ConfirmationMessage.InnerText = "Updated Successfully !";

                            populateHiddenFields();
                        }
                    }

                    if (hdnFlagType.Value == "ADD")
                    {
                        // call the routine to add a new LeadCampaign record into the table..
                        DataAccess.MySQLAccess myLeadCampaignObj = new DataAccess.MySQLAccess();
                        int intInsOut = myLeadCampaignObj.insCallingTimesByState("LocalMySqlServer", txtState.Text, txtWeekdayStartTime.Text, txtWeekdayEndTime.Text, txtSaturdayStartTime.Text,
                                                            txtSaturdayEndTime.Text, txtSundayStartTime.Text, txtSundayEndTime.Text, txtHolidayStartTime.Text, txtHolidayEndTime.Text,
                                                            strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());
                        if (intInsOut > 0)
                        {
                            populateCallingTimesGrid();
                            ConfirmationMessage.InnerText = "Inserted Successfully !";

                            populateHiddenFields();
                        }
                        else if (intInsOut == -1)
                        {
                            ConfirmationMessage.InnerText = "Record Already Exists. Please Edit The Record Details !";
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
                hdnState.Value = "";
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message :" + ex.Message;
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// this function will fetch the values from Database and populate the CallingTimes Gridview.
        /// </summary>
        private void populateCallingTimesGrid()
        {
            try
            {
                // code to populate the Phone Extension data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsPhoneExtensions = new DataSet();
                dsPhoneExtensions = myObj.getCallingTimesByState("LocalMySqlServer");

                DataTable dtDialerRules = new DataTable();
                dtDialerRules = dsPhoneExtensions.Tables[0];

                gvCallingTimesByState.DataSource = dtDialerRules;
                gvCallingTimesByState.DataBind();
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
        private void populateSelectedRecord(string strState)
        {
            try
            {
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsCallingTimes = new DataSet();
                dsCallingTimes = myObj.getCallingTimesByStateAbbr("LocalMySqlServer", strState);

                // Assigning the selected CallingTimesByState values to the corresponding fields...
                hdnState.Value = dsCallingTimes.Tables[0].Rows[0]["State"].ToString();
                txtState.Text = dsCallingTimes.Tables[0].Rows[0]["State"].ToString();
                txtWeekdayStartTime.Text = dsCallingTimes.Tables[0].Rows[0]["WeekdayStartTime"].ToString();
                txtWeekdayEndTime.Text = dsCallingTimes.Tables[0].Rows[0]["WeekdayEndTime"].ToString();
                txtSaturdayStartTime.Text = dsCallingTimes.Tables[0].Rows[0]["SaturdayStartTime"].ToString();
                txtSaturdayEndTime.Text = dsCallingTimes.Tables[0].Rows[0]["SaturdayEndTime"].ToString();
                txtSundayStartTime.Text = dsCallingTimes.Tables[0].Rows[0]["SundayStartTime"].ToString();
                txtSundayEndTime.Text = dsCallingTimes.Tables[0].Rows[0]["SundayEndTime"].ToString();
                txtHolidayStartTime.Text = dsCallingTimes.Tables[0].Rows[0]["HolidayStartTime"].ToString();
                txtHolidayEndTime.Text = dsCallingTimes.Tables[0].Rows[0]["HolidayEndTime"].ToString();

                // setting the values in the hidden field to identify the modifications during the Save button click..
                hdnWeekdayStartTime.Value = dsCallingTimes.Tables[0].Rows[0]["WeekdayStartTime"].ToString();
                hdnWeekdayEndTime.Value = dsCallingTimes.Tables[0].Rows[0]["WeekdayEndTime"].ToString();
                hdnSaturdayStartTime.Value = dsCallingTimes.Tables[0].Rows[0]["SaturdayStartTime"].ToString();
                hdnSaturdayEndTime.Value = dsCallingTimes.Tables[0].Rows[0]["SaturdayEndTime"].ToString();
                hdnSundayStartTime.Value = dsCallingTimes.Tables[0].Rows[0]["SundayStartTime"].ToString();
                hdnSundayEndTime.Value = dsCallingTimes.Tables[0].Rows[0]["SundayEndTime"].ToString();
                hdnHolidayStartTime.Value = dsCallingTimes.Tables[0].Rows[0]["HolidayStartTime"].ToString();
                hdnHolidayEndTime.Value = dsCallingTimes.Tables[0].Rows[0]["HolidayEndTime"].ToString();
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
                hdnState.Value = txtState.Text;
                hdnWeekdayStartTime.Value = txtWeekdayStartTime.Text;
                hdnWeekdayEndTime.Value = txtWeekdayEndTime.Text;
                hdnSaturdayStartTime.Value = txtSaturdayStartTime.Text;
                hdnSaturdayEndTime.Value = txtSaturdayEndTime.Text;
                hdnSundayStartTime.Value = txtSundayStartTime.Text;
                hdnSundayEndTime.Value = txtSundayEndTime.Text;
                hdnHolidayStartTime.Value = txtHolidayStartTime.Text;
                hdnHolidayEndTime.Value = txtHolidayEndTime.Text;
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
                foreach (GridViewRow gvRow in gvCallingTimesByState.Rows)
                {
                    System.Web.UI.WebControls.RadioButton rb = (System.Web.UI.WebControls.RadioButton)gvCallingTimesByState.Rows[iCnt].FindControl("rdEditCallingTimes");
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
                // calling the populateCallingTimesGrid to refresh the Grid Data
                populateCallingTimesGrid();

                // setting the default values for the controls before adding a new record...
                txtState.Text = "";
                txtState.BackColor = System.Drawing.Color.White;
                txtWeekdayStartTime.Text = "";
                txtWeekdayStartTime.BackColor = System.Drawing.Color.White;
                txtWeekdayEndTime.Text = "";
                txtWeekdayEndTime.BackColor = System.Drawing.Color.White;
                txtSaturdayStartTime.Text = "";
                txtSaturdayStartTime.BackColor = System.Drawing.Color.White;
                txtSaturdayEndTime.Text = "";
                txtSaturdayEndTime.BackColor = System.Drawing.Color.White;
                txtSundayStartTime.Text = "";
                txtSundayStartTime.BackColor = System.Drawing.Color.White;
                txtSundayEndTime.Text = "";
                txtSundayEndTime.BackColor = System.Drawing.Color.White;
                txtHolidayStartTime.Text = "";
                txtHolidayStartTime.BackColor = System.Drawing.Color.White;
                txtHolidayEndTime.Text = "";
                txtHolidayEndTime.BackColor = System.Drawing.Color.White;

                ConfirmationMessage.InnerText = "";

                // clearing the values from the hidden controls...
                hdnState.Value = "";
                hdnWeekdayStartTime.Value = "";
                hdnWeekdayEndTime.Value = "";
                hdnSaturdayStartTime.Value = "";
                hdnSaturdayEndTime.Value  = "";
                hdnSundayStartTime.Value  = "";
                hdnSundayEndTime.Value  = "";
                hdnHolidayStartTime.Value  = "";
                hdnHolidayEndTime.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}