using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACMGAdmin.MasterInterfaces
{
    /// <summary>
    /// This class contains the Add / Edit functionality of PhoneExtensions Module.
    /// <version>1.0</version>
    /// <lastmodifieddate>28-Jul-2011</lastmodifieddate>
    /// <author>CSC</author>
    /// </summary>


    public partial class PhoneExtensions : System.Web.UI.Page
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
                    populatePhoneExtGrid();
                }
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }

        }

        /// <summary>
        /// This event will implement the paging functionality to the Holidays Gridview.
        /// </summary>
        protected void gvPhoneExtensions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPhoneExtensions.PageIndex = e.NewPageIndex;
                populatePhoneExtGrid();
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }

        }


         /// <summary>
        /// This event will be fired after the user selects the "Radiobutton" in the GridView
        /// </summary>
        protected void rdEditPhoneExt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                deSelect_RB_In_Gridview();

                System.Web.UI.WebControls.RadioButton senderRB = (System.Web.UI.WebControls.RadioButton)sender;
                senderRB.Checked = true;
                ConfirmationMessage.InnerText = "";

                // changing the back color of the controls in Edit mode...
                txtSwitchName.BackColor = System.Drawing.Color.White;
                txtCompany.BackColor = System.Drawing.Color.White;
                txtSwitchAddress.BackColor = System.Drawing.Color.White;
                txtSwitchPort.BackColor = System.Drawing.Color.White;
                txtExtension.BackColor = System.Drawing.Color.White;
                txtUserName.BackColor = System.Drawing.Color.White;
                txtPassword.BackColor = System.Drawing.Color.White;

                // setting the hdnCallCenterId in the hiddenfield - (to check modifications during Save button click)
                hdnPhoneExtensionId.Value = senderRB.Text;
                populateSelectedRecord(Convert.ToInt32(hdnPhoneExtensionId.Value));

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
        /// This event will ADD / UPDATE the selected PhoneExtension records details in the database.
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
                    string strScreenName = "PhoneExtensions";
                    string strTableName = "tbl_phoneextensions";
                    StringBuilder strBeforeImage = new StringBuilder();
                    StringBuilder strAfterImage = new StringBuilder();

                    strUser = User.Identity.Name;
                    strBeforeImage.Append(hdnPhoneExtensionId.Value + " || " + hdnSwitchName.Value + " || " + hdnCompany.Value + " || " +
                                            hdnSwitchAddress.Value + " || " + hdnSwitchPort.Value + " || " + hdnExtension.Value + " || " +
                                            hdnUserName.Value + " || " + hdnPassword.Value);

                    strAfterImage.Append(hdnPhoneExtensionId.Value + " || " + hdnSwitchName.Value + " || " + hdnCompany.Value + " || " +
                                            hdnSwitchAddress.Value + " || " + hdnSwitchPort.Value + " || " + hdnExtension.Value + " || " +
                                            hdnUserName.Value + " || " + hdnPassword.Value);

                    if (hdnFlagType.Value == "EDIT")
                    {

                        DataAccess.MySQLAccess myUpdObj = new DataAccess.MySQLAccess();
                        int iOutput = myUpdObj.updatePhoneExtension("LocalMySqlServer", Convert.ToInt32(hdnPhoneExtensionId.Value), txtSwitchName.Text,
                                                            txtCompany.Text, txtSwitchAddress.Text, txtSwitchPort.Text, txtExtension.Text,
                                                            txtUserName.Text, txtPassword.Text,
                                                            strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());

                        //checking whether record has been updated
                        if (iOutput > 0)
                        {
                            populatePhoneExtGrid();
                            ConfirmationMessage.InnerText = "Updated Successfully !";

                            populateHiddenFields();
                        }
                    }

                    if (hdnFlagType.Value == "ADD")
                    {
                        // call the routine to add a new CallCenter record into the table..
                        DataAccess.MySQLAccess myCallCenterObj = new DataAccess.MySQLAccess();
                        int intInsOut = myCallCenterObj.insertPhoneExtension("LocalMySqlServer", txtSwitchName.Text, txtCompany.Text, txtSwitchAddress.Text, txtSwitchPort.Text,
                                                                          txtExtension.Text,txtUserName.Text, txtPassword.Text,
                                                                          strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());
                        if (intInsOut > 0)
                        {
                            populatePhoneExtGrid();
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
                hdnPhoneExtensionId.Value = "";
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message :" + ex.Message;
            }
        }
        #endregion


        #region Private Methods

        /// <summary>
        /// this function will fetch the values from Database and populate the DialerRules Gridview.
        /// </summary>
        private void populatePhoneExtGrid()
        {
            try
            {
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsPhoneExtensions = new DataSet();
                dsPhoneExtensions = myObj.getPhoneExtensions("LocalMySqlServer");

                DataTable dtDialerRules = new DataTable();
                dtDialerRules = dsPhoneExtensions.Tables[0];

                gvPhoneExtensions.DataSource = dtDialerRules;
                gvPhoneExtensions.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// this function will takes the PhoneExtensionID as input and selects the PhoneExtension record values from DB and
        /// populate in the screen.
        /// </summary>
        private void populateSelectedRecord(int iPhoneExtensionID)
        {
            try
            {
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsSelPhoneExt = new DataSet();
                dsSelPhoneExt = myObj.getPhoneExtensionByID("LocalMySqlServer", iPhoneExtensionID);

                // Assigning the selected Dailer Rule values to the corresponding fields...
                hdnPhoneExtensionId.Value = dsSelPhoneExt.Tables[0].Rows[0]["PhoneExtensionID"].ToString();
                txtSwitchName.Text = dsSelPhoneExt.Tables[0].Rows[0]["SwitchName"].ToString();
                txtCompany.Text = dsSelPhoneExt.Tables[0].Rows[0]["Company"].ToString();
                txtSwitchAddress.Text = dsSelPhoneExt.Tables[0].Rows[0]["SwitchAddress"].ToString();
                txtSwitchPort.Text = dsSelPhoneExt.Tables[0].Rows[0]["SwitchPort"].ToString();
                txtExtension.Text = dsSelPhoneExt.Tables[0].Rows[0]["Extension"].ToString();
                txtUserName.Text = dsSelPhoneExt.Tables[0].Rows[0]["UserName"].ToString();
                txtPassword.Text = dsSelPhoneExt.Tables[0].Rows[0]["Password"].ToString();

                // setting the values in the hidden field to identify the modifications during the Save button click..
                hdnSwitchName.Value = dsSelPhoneExt.Tables[0].Rows[0]["SwitchName"].ToString();
                hdnCompany.Value = dsSelPhoneExt.Tables[0].Rows[0]["Company"].ToString();
                hdnSwitchAddress.Value = dsSelPhoneExt.Tables[0].Rows[0]["SwitchAddress"].ToString();
                hdnSwitchPort.Value = dsSelPhoneExt.Tables[0].Rows[0]["SwitchPort"].ToString();
                hdnExtension.Value = dsSelPhoneExt.Tables[0].Rows[0]["Extension"].ToString();
                hdnUserName.Value = dsSelPhoneExt.Tables[0].Rows[0]["UserName"].ToString();
                hdnPassword.Value = dsSelPhoneExt.Tables[0].Rows[0]["Password"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


         /// <summary>
        /// this function will take the Textbox values and populate the hidden field values  
        /// & this is used for verifying the values modified in the screen.
        /// </summary>
        private void populateHiddenFields()
        {
            try
            {
                //storing the values in the hidden fields to verify the modification & to highlight in the screen..
                hdnSwitchName.Value = txtSwitchName.Text;
                hdnCompany.Value = txtCompany.Text;
                hdnSwitchAddress.Value = txtSwitchAddress.Text;
                hdnSwitchPort.Value = txtSwitchPort.Text;
                hdnExtension.Value = txtExtension.Text;
                hdnUserName.Value = txtUserName.Text;
                hdnPassword.Value = txtPassword.Text;
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
                foreach (GridViewRow gvRow in gvPhoneExtensions.Rows)
                {
                    System.Web.UI.WebControls.RadioButton rb = (System.Web.UI.WebControls.RadioButton)gvPhoneExtensions.Rows[iCnt].FindControl("rdEditPhoneExt");
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
                populatePhoneExtGrid();

                //// setting the default values for the controls before adding a new record...
                txtSwitchName.Text = "";
                txtSwitchName.BackColor = System.Drawing.Color.White;
                txtCompany.Text = "";
                txtCompany.BackColor = System.Drawing.Color.White;
                txtSwitchAddress.Text = "";
                txtSwitchAddress.BackColor = System.Drawing.Color.White;
                txtSwitchPort.Text = "";
                txtSwitchPort.BackColor = System.Drawing.Color.White;
                txtExtension.Text = "";
                txtExtension.BackColor = System.Drawing.Color.White;
                txtUserName.Text = "";
                txtUserName.BackColor = System.Drawing.Color.White;
                txtPassword.Text = "";
                txtPassword.BackColor = System.Drawing.Color.White;
                ConfirmationMessage.InnerText = "";

                // clearing the hidden field values ...
                hdnPhoneExtensionId.Value = "";
                hdnSwitchName.Value = "";
                hdnCompany.Value = "";
                hdnSwitchAddress.Value = "";
                hdnSwitchPort.Value = "";
                hdnExtension.Value = "";
                hdnUserName.Value = "";
                hdnPassword.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion



    }
}