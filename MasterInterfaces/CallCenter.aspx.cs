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
    /// This class contains the Add / Edit functionality of CallCenter Module.
    /// <version>1.0</version>
    /// <lastmodifieddate>26-Jul-2011</lastmodifieddate>
    /// <author>CSC</author>
    /// </summary>

    public partial class CallCenter : System.Web.UI.Page
    {

        #region Protected Methods

        /// <summary>
        /// On page-load event setting the attributes to the control and populating the Gridview 
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            btnSave.Attributes.Add("onclick", "return highlightModFields();");

            try
            {

                if (!IsPostBack)
                {
                    // calling the below method to populate the Holiday GridView
                    populateCallCenterGrid();
                }
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = ex.Message;

            }

        }


        /// <summary>
        /// This event will implement the paging functionality to the Holidays Gridview.
        /// </summary>
        protected void gvCallCenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCallCenter.PageIndex = e.NewPageIndex;
                populateCallCenterGrid();
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message: " + ex.Message;
            }



        }


        /// <summary>
        /// This event will Add / Update the selected CallCenter records details in the database.
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
                    string strScreenName = "CallCenter";
                    string strTableName = "tbl_callcenter";
                    StringBuilder strBeforeImage = new StringBuilder();
                    StringBuilder strAfterImage = new StringBuilder();

                    strUser = User.Identity.Name;
                    strBeforeImage.Append(hdnCallCenterId.Value + " || " + hdnCallCenterName.Value + " || " + hdnCallCenterNotes.Value + " || " +
                                            hdnConnectionString.Value + " || " + hdnActive.Value);

                    strAfterImage.Append(hdnCallCenterId.Value+" || "+ txtCallCenter.Text +" || "+ txtCallCenterNotes.Text+ " || "
                                          +txtConnectionString.Text+" || "+ rblActive.SelectedValue);

                    if (hdnFlagType.Value == "EDIT")
                    {
                        
                        DataAccess.MySQLAccess myUpdObj = new DataAccess.MySQLAccess();
                        int iOutput = myUpdObj.updateCallCenterDetails("LocalMySqlServer", Convert.ToInt32(hdnCallCenterId.Value), txtCallCenter.Text, 
                                                            txtCallCenterNotes.Text, txtConnectionString.Text, rblActive.SelectedValue, 
                                                            strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());

                        //checking whether record has been updated
                        if (iOutput > 0)
                        {
                            populateCallCenterGrid();
                            ConfirmationMessage.InnerText = "Updated Successfully !";

                            //storing the values in the hidden fields to verify the modification & to highlight in the screen..
                            hdnCallCenterName.Value = txtCallCenter.Text;
                            hdnCallCenterNotes.Value = txtCallCenterNotes.Text;
                            hdnConnectionString.Value = txtConnectionString.Text;
                            hdnActive.Value = rblActive.SelectedValue;
                            
                        }
                    }

                    if (hdnFlagType.Value == "ADD")
                    {
                        // call the routine to add a new CallCenter record into the table..
                        DataAccess.MySQLAccess myCallCenterObj = new DataAccess.MySQLAccess();
                        int intInsOut = myCallCenterObj.insertCallCenter("LocalMySqlServer", txtCallCenter.Text, txtCallCenterNotes.Text, txtConnectionString.Text, rblActive.SelectedValue,
                                                                         strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());
                        if (intInsOut > 0)
                        {
                            populateCallCenterGrid();
                            ConfirmationMessage.InnerText = "Inserted Successfully !";

                            //storing the values in the hidden fields to verify the modification & to highlight in the screen..
                            hdnCallCenterName.Value = txtCallCenter.Text;
                            hdnCallCenterNotes.Value = txtCallCenterNotes.Text;
                            hdnConnectionString.Value = txtConnectionString.Text;
                            hdnActive.Value = rblActive.SelectedValue;
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
                //bAddFlag = true;
                hdnFlagType.Value = "ADD";
                clearAllFieldValues();
                btnSave.Enabled = true;
                //disabling the AddNew field
                btnAddNew.Enabled = false;
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message :" + ex.Message;
            }

        }


        /// <summary>
        /// This event will be fired after the user selects the "Radiobutton" in the GridView
        /// </summary>
        protected void rdEditCallCenter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                deSelect_RB_In_Gridview();

                System.Web.UI.WebControls.RadioButton senderRB = (System.Web.UI.WebControls.RadioButton)sender;
                senderRB.Checked = true;
                ConfirmationMessage.InnerText = "";

                // changing the back color of the controls in Edit mode...
                txtCallCenter.BackColor = System.Drawing.Color.White;
                txtCallCenterNotes.BackColor = System.Drawing.Color.White;
                txtConnectionString.BackColor = System.Drawing.Color.White;
                rblActive.BackColor = System.Drawing.Color.White;

                // setting the hdnCallCenterId in the hiddenfield - (to check modifications during Save button click)
                hdnCallCenterId.Value = senderRB.Text;
                populateSelectedRecord(Convert.ToInt32(hdnCallCenterId.Value));

                hdnFlagType.Value = "EDIT";
                // Clearing the error message in the form
                divErrorMsg.InnerText = "";
                btnSave.Enabled = true;
                btnAddNew.Enabled = true;
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }
        }


        #endregion


        #region Private Methods

        /// <summary>
        /// This function is used to fetch CallCenter values from Database and populate it in Gridview
        /// </summary>
        private void populateCallCenterGrid()
        {
            try
            {
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsCallCenter = new DataSet();
                dsCallCenter = myObj.getCallCenter("LocalMySqlServer");

                DataTable dtCallCenter = new DataTable();
                dtCallCenter = dsCallCenter.Tables[0];

                gvCallCenter.DataSource = dtCallCenter;
                gvCallCenter.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// this function will takes the CallCenterID as input and selects the remaining CallCenter record values from DB and
        /// populate in the screen.
        /// </summary>
        private void populateSelectedRecord(int iCallCenterID)
        {
            string strDialActive = "";
            try
            {
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsSelCallCenter = new DataSet();
                dsSelCallCenter = myObj.getCallCenterByID("LocalMySqlServer", iCallCenterID);

                // Assigning the selected Dailer Rule values to the corresponding fields...
                hdnCallCenterId.Value = dsSelCallCenter.Tables[0].Rows[0]["CallCenterID"].ToString();
                txtCallCenter.Text = dsSelCallCenter.Tables[0].Rows[0]["CallCenterName"].ToString();
                txtCallCenterNotes.Text = dsSelCallCenter.Tables[0].Rows[0]["CallCenterNotes"].ToString();
                txtConnectionString.Text = dsSelCallCenter.Tables[0].Rows[0]["LDAPConnectStringName"].ToString();

                if ((dsSelCallCenter.Tables[0].Rows[0]["Active"].ToString() == "Yes"))
                {
                    strDialActive = "1";
                }
                else if ((dsSelCallCenter.Tables[0].Rows[0]["Active"].ToString() == "No") || (dsSelCallCenter.Tables[0].Rows[0]["Active"] == null))
                {
                    strDialActive = "0";
                }

                rblActive.SelectedValue = strDialActive;


                // setting the values in the hidden field to identify the modifications during the Save button click..
                hdnCallCenterName.Value = dsSelCallCenter.Tables[0].Rows[0]["CallCenterName"].ToString();
                hdnCallCenterNotes.Value = dsSelCallCenter.Tables[0].Rows[0]["CallCenterNotes"].ToString();
                hdnConnectionString.Value = dsSelCallCenter.Tables[0].Rows[0]["LDAPConnectStringName"].ToString();
                hdnActive.Value = strDialActive;
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
                foreach (GridViewRow gvRow in gvCallCenter.Rows)
                {
                    System.Web.UI.WebControls.RadioButton rb = (System.Web.UI.WebControls.RadioButton)gvCallCenter.Rows[iCnt].FindControl("rdEditCallCenter");
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
                // calling the populateHolidayGrid to refresh the Grid Data
                populateCallCenterGrid();
                
                // setting the default values for the dropdownlist
                txtCallCenter.Text = "";
                txtCallCenter.BackColor = System.Drawing.Color.White;
                txtCallCenterNotes.Text = "";
                txtCallCenterNotes.BackColor = System.Drawing.Color.White;
                txtConnectionString.Text = "";
                txtConnectionString.BackColor = System.Drawing.Color.White;
                rblActive.ClearSelection();
                rblActive.BackColor = System.Drawing.Color.White;
                ConfirmationMessage.InnerText = "";
                
                // clearing the hidden field values ...
                hdnCallCenterId.Value = "";
                hdnCallCenterName.Value = "";
                hdnCallCenterNotes.Value = "";
                hdnConnectionString.Value = "";
                hdnActive.Value = "";

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}