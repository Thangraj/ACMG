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
    /// This class contains the Add / Edit functionality of RoutingGroups Module.
    /// <version>1.0</version>
    /// <lastmodifieddate>03-Aug-2011</lastmodifieddate>
    /// <author>CSC</author>
    /// </summary>
    /// 
    public partial class RoutingGroups : System.Web.UI.Page
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
                    populateRoutingGroupGrid();
                }
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }
        }

        /// <summary>
        /// This event will implement the paging functionality to the RoutingGroups Gridview.
        /// </summary>
        protected void gvRoutingGroups_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvRoutingGroups.PageIndex = e.NewPageIndex;
                populateRoutingGroupGrid();
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }

        }

        /// <summary>
        /// This event will be fired after the user selects the "Radiobutton" in the GridView
        /// </summary>
        protected void rdEditRoutingGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                deSelect_RB_In_Gridview();

                System.Web.UI.WebControls.RadioButton senderRB = (System.Web.UI.WebControls.RadioButton)sender;
                senderRB.Checked = true;
                ConfirmationMessage.InnerText = "";

                // changing the back color of the controls in Edit mode...
                txtRoutingGroup.BackColor = System.Drawing.Color.White;
                txtRoutingGroup.BackColor = System.Drawing.Color.White;
                txtRoutingGroupDesc.BackColor = System.Drawing.Color.White;
                txtRoutingGroupDesc.BackColor = System.Drawing.Color.White;
                
                // setting the RoutingGroupId in the hiddenfield - (to check modifications during Save button click)
                hdnRoutingGroupId.Value = senderRB.Text;
                populateSelectedRecord(Convert.ToInt32(hdnRoutingGroupId.Value));

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
        /// This event will ADD / UPDATE the selected RoutingGroup records details in the database.
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
                    string strScreenName = "RoutingGroups";
                    string strTableName = "tbl_routinggroups";
                    StringBuilder strBeforeImage = new StringBuilder();
                    StringBuilder strAfterImage = new StringBuilder();

                    strUser = User.Identity.Name;
                    strBeforeImage.Append(hdnRoutingGroupId.Value + " || " + hdnRoutingGroup.Value + " || " + hdnRoutingGroupDesc.Value);

                    strAfterImage.Append(hdnRoutingGroupId.Value + " || " + txtRoutingGroup.Text + " || " + txtRoutingGroupDesc.Text);

                    if (hdnFlagType.Value == "EDIT")
                    {

                        DataAccess.MySQLAccess myUpdObj = new DataAccess.MySQLAccess();
                        int iOutput = myUpdObj.updRoutingGroups("LocalMySqlServer", Convert.ToInt32(hdnRoutingGroupId.Value), txtRoutingGroup.Text,txtRoutingGroupDesc.Text,
                                                            strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());

                        //checking whether record has been updated
                        if (iOutput > 0)
                        {
                            populateRoutingGroupGrid();
                            ConfirmationMessage.InnerText = "Updated Successfully !";

                            populateHiddenFields();
                        }
                    }

                    if (hdnFlagType.Value == "ADD")
                    {
                        // call the routine to add a new RoutingGroup record into the table..
                        DataAccess.MySQLAccess myRoutingGroupObj = new DataAccess.MySQLAccess();
                        int intInsOut = myRoutingGroupObj.insRoutingGroups("LocalMySqlServer", txtRoutingGroup.Text, txtRoutingGroupDesc.Text, 
                                                                          strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());
                        if (intInsOut > 0)
                        {
                            populateRoutingGroupGrid();
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
                hdnRoutingGroupId.Value = "";
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message :" + ex.Message;
            }
        }


        #region Private Methods

        /// <summary>
        /// this function will fetch the values from Database and populate the Routing Groups Gridview.
        /// </summary>
        private void populateRoutingGroupGrid()
        {
            try
            {
                // code to populate the Routing Groups data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsRoutingGroups = new DataSet();
                dsRoutingGroups = myObj.getRoutingGroups("LocalMySqlServer");

                DataTable dtRoutingGroups = new DataTable();
                dtRoutingGroups = dsRoutingGroups.Tables[0];

                gvRoutingGroups.DataSource = dtRoutingGroups;
                gvRoutingGroups.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// this function will takes the RoutingGroupID as input and selects the RoutingGroup record values from DB and
        /// populate in the screen.
        /// </summary>
        private void populateSelectedRecord(int iRoutingGroupID)
        {
            try
            {
                // code to populate the Routing Groups data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsSelRoutingGroup = new DataSet();
                dsSelRoutingGroup = myObj.getRoutingGroupsByID("LocalMySqlServer", iRoutingGroupID);

                // Assigning the selected Routing Groups values to the corresponding fields...
                hdnRoutingGroupId.Value = dsSelRoutingGroup.Tables[0].Rows[0]["RoutingGroupID"].ToString();
                txtRoutingGroup.Text = dsSelRoutingGroup.Tables[0].Rows[0]["RoutingGroup"].ToString();
                txtRoutingGroupDesc.Text = dsSelRoutingGroup.Tables[0].Rows[0]["RoutingGroupDesc"].ToString();
                
                // setting the values in the hidden field to identify the modifications during the Save button click..
                hdnRoutingGroup.Value = dsSelRoutingGroup.Tables[0].Rows[0]["RoutingGroup"].ToString();
                hdnRoutingGroupDesc.Value = dsSelRoutingGroup.Tables[0].Rows[0]["RoutingGroupDesc"].ToString();
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
                hdnRoutingGroup.Value = txtRoutingGroup.Text;
                hdnRoutingGroupDesc.Value = txtRoutingGroupDesc.Text;
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
                foreach (GridViewRow gvRow in gvRoutingGroups.Rows)
                {
                    System.Web.UI.WebControls.RadioButton rb = (System.Web.UI.WebControls.RadioButton)gvRoutingGroups.Rows[iCnt].FindControl("rdEditRoutingGroup");
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
                // calling the populateRoutingGroupGrid to refresh the Grid Data
                populateRoutingGroupGrid();

                //// setting the default values for the controls before adding a new record...
                txtRoutingGroup.Text = "";
                txtRoutingGroup.BackColor = System.Drawing.Color.White;
                txtRoutingGroupDesc.Text = "";
                txtRoutingGroupDesc.BackColor = System.Drawing.Color.White;
                ConfirmationMessage.InnerText = "";

                // clearing the hidden field values ...
                hdnRoutingGroupId.Value = "";
                hdnRoutingGroup.Value = "";
                hdnRoutingGroupDesc.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #endregion
    }
}