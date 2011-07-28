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
    /// This class contains the Add / Edit functionality of CallDisposition Module.
    /// <version>1.0</version>
    /// <lastmodifieddate>27-Jul-2011</lastmodifieddate>
    /// <author>CSC</author>
    /// </summary>

    public partial class CallDispositions : System.Web.UI.Page
    {

        #region Protected methods
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                // adding the Javascript function to the Save button
                btnSave.Attributes.Add("onclick", "return highlightModFields();");

                if (!IsPostBack)
                {
                    populateCallDispGrid();
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
        protected void rdEditCallDisp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                deSelect_RB_In_Gridview();
                ConfirmationMessage.InnerText = "";
                // changing the backcolor of the controls to White..
                txtCallDisposition.BackColor = System.Drawing.Color.White;
                rblDoNotCall.BackColor = System.Drawing.Color.White;
                rblSale.BackColor = System.Drawing.Color.White;
                rblContact.BackColor = System.Drawing.Color.White;
                rblBadLead.BackColor = System.Drawing.Color.White;
                rblCallBack.BackColor = System.Drawing.Color.White;
                rblNotInterested.BackColor = System.Drawing.Color.White;
                rblSystemDefaults.BackColor = System.Drawing.Color.White;
                rblCustom.BackColor = System.Drawing.Color.White;
                rblNotCalled.BackColor = System.Drawing.Color.White;
                rblDialed.BackColor = System.Drawing.Color.White;
                rblNotDialed.BackColor = System.Drawing.Color.White;
                rblProcessedGood.BackColor = System.Drawing.Color.White;
                rblProcessedBad.BackColor = System.Drawing.Color.White;
                rblNotProcessed.BackColor = System.Drawing.Color.White;

                
                System.Web.UI.WebControls.RadioButton rbSender = (System.Web.UI.WebControls.RadioButton)sender;
                rbSender.Checked = true;
                // setting the CallDispositionsID in the hiddenfield - (to check modifications during Save button click)
                hdnCallDispId.Value = rbSender.Text;
                populateSelectedRecord(Convert.ToInt32(hdnCallDispId.Value));
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


        /// <summary>
        /// This event will perform the Add / Edit functionality depending upon the user's action
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    #region Logging Functionality Variables
                    // Below fields will be used for logging functionality..
                    string strUser = "";
                    string strDateTime = DateTime.Now.ToString();
                    string strScreenName = "CallDispositions";
                    string strTableName = "tbl_calldispositions";
                    StringBuilder strBeforeImage = new StringBuilder();
                    StringBuilder strAfterImage = new StringBuilder();

                    strUser = User.Identity.Name;
                    strBeforeImage.Append(hdnCallDispId.Value + " || " + hdnCallDisposition.Value + " || " + hdnDoNotCall.Value + " || " +
                                            hdnSale.Value + " || " + hdnContact.Value + " || "+ hdnBadLead.Value + " || " + hdnCallBack.Value + " || "+
                                            hdnNotInterested.Value + " || "+ hdnSystemDefaults.Value + " || "+ hdnCustom.Value + " || "+ hdnNotCalled.Value + " || " +
                                            hdnDialed.Value + " || "+ hdnNotDialed.Value + " || "+ hdnProcessedGood.Value + " || "+ hdnProcessedBad.Value + " || "+
                                            hdnNotProcessed.Value
                                         );

                    strAfterImage.Append(hdnCallDispId.Value + " || " + txtCallDisposition.Text + " || " + rblDoNotCall.SelectedValue + " || "
                                          + rblSale.SelectedValue + " || " + rblContact.SelectedValue + " || "+ rblBadLead.SelectedValue + " || "
                                          + rblCallBack.SelectedValue + " || " + rblNotInterested.SelectedValue + " || " + rblSystemDefaults.SelectedValue + " || "
                                          + rblCustom.SelectedValue + " || " + rblNotCalled.SelectedValue + " || " + rblDialed.SelectedValue + " || "
                                          + rblNotDialed.SelectedValue + " || " + rblProcessedGood.SelectedValue + " || " + rblProcessedBad.SelectedValue + " || "
                                          + rblNotProcessed.SelectedValue 
                                         );
                    #endregion

                    if (hdnFlagType.Value == "EDIT")
                    {

                        DataAccess.MySQLAccess myUpdObj = new DataAccess.MySQLAccess();
                        int iOutput = myUpdObj.updateCallDisposition("LocalMySqlServer", Convert.ToInt32(hdnCallDispId.Value), txtCallDisposition.Text,
                                                            rblDoNotCall.SelectedValue, rblSale.SelectedValue, rblContact.SelectedValue,
                                                            rblBadLead.SelectedValue, rblCallBack.SelectedValue, rblNotInterested.SelectedValue,
                                                            rblSystemDefaults.SelectedValue, rblCustom.SelectedValue, rblNotCalled.SelectedValue,
                                                            rblDialed.SelectedValue, rblNotDialed.SelectedValue, rblProcessedGood.SelectedValue,
                                                            rblProcessedBad.SelectedValue, rblNotProcessed.SelectedValue,
                                                            strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());

                        //checking whether record has been updated
                        if (iOutput > 0)
                        {
                            populateCallDispGrid();
                            ConfirmationMessage.InnerText = "Updated Successfully !";

                            //storing the values in the hidden fields to verify the modification & to highlight in the screen..
                            hdnCallDisposition.Value = txtCallDisposition.Text;
                            hdnDoNotCall.Value = rblDoNotCall.SelectedValue;
                            hdnSale.Value = rblSale.SelectedValue;
                            hdnContact.Value = rblContact.SelectedValue;
                            hdnBadLead.Value = rblBadLead.SelectedValue;
                            hdnCallBack.Value = rblCallBack.SelectedValue;
                            hdnNotInterested.Value = rblNotInterested.SelectedValue;
                            hdnSystemDefaults.Value = rblSystemDefaults.SelectedValue;
                            hdnCustom.Value = rblCustom.SelectedValue;
                            hdnNotCalled.Value = rblNotCalled.SelectedValue;
                            hdnDialed.Value = rblDialed.SelectedValue;
                            hdnNotDialed.Value = rblNotDialed.SelectedValue;
                            hdnProcessedGood.Value = rblProcessedGood.SelectedValue;
                            hdnProcessedBad.Value = rblProcessedBad.SelectedValue;
                            hdnNotProcessed.Value = rblNotProcessed.SelectedValue;
                        }
                    }

                    if (hdnFlagType.Value == "ADD")
                    {
                        // call the routine to add a new CallCenter record into the table..
                        DataAccess.MySQLAccess myCallDispObj = new DataAccess.MySQLAccess();
                        int intInsOut = myCallDispObj.insertCallDisposition("LocalMySqlServer", txtCallDisposition.Text, rblDoNotCall.SelectedValue,
                                                                       rblSale.SelectedValue, rblContact.SelectedValue,rblBadLead.SelectedValue,
                                                                       rblCallBack.SelectedValue, rblNotInterested.SelectedValue, rblSystemDefaults.SelectedValue,
                                                                       rblCustom.SelectedValue, rblNotCalled.SelectedValue, rblDialed.SelectedValue,
                                                                       rblNotDialed.SelectedValue, rblProcessedGood.SelectedValue, rblProcessedBad.SelectedValue,
                                                                       rblNotProcessed.SelectedValue,
                                                                       strUser, strDateTime, strScreenName, strTableName, strBeforeImage.ToString(), strAfterImage.ToString());
                        if (intInsOut > 0)
                        {
                            populateCallDispGrid();
                            ConfirmationMessage.InnerText = "Inserted Successfully !";

                            //storing the values in the hidden fields to verify the modification & to highlight in the screen..
                            hdnCallDisposition.Value = txtCallDisposition.Text;
                            hdnDoNotCall.Value = rblDoNotCall.SelectedValue;
                            hdnSale.Value = rblSale.SelectedValue;
                            hdnContact.Value = rblContact.SelectedValue;
                            hdnBadLead.Value = rblBadLead.SelectedValue;
                            hdnCallBack.Value = rblCallBack.SelectedValue;
                            hdnNotInterested.Value = rblNotInterested.SelectedValue;
                            hdnSystemDefaults.Value = rblSystemDefaults.SelectedValue;
                            hdnCustom.Value = rblCustom.SelectedValue;
                            hdnNotCalled.Value = rblNotCalled.SelectedValue;
                            hdnDialed.Value = rblDialed.SelectedValue;
                            hdnNotDialed.Value = rblNotDialed.SelectedValue;
                            hdnProcessedGood.Value = rblProcessedGood.SelectedValue;
                            hdnProcessedBad.Value = rblProcessedBad.SelectedValue;
                            hdnNotProcessed.Value = rblNotProcessed.SelectedValue;
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
                hdnCallDispId.Value = "";
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message :" + ex.Message;
            }
        }

        /// <summary>
        /// This event will implement the paging functionality to the Holidays Gridview.
        /// </summary>
        protected void gvCallDisposition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCallDisposition.PageIndex = e.NewPageIndex;
                populateCallDispGrid();
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message:" + ex.Message;
            }

        }

        #endregion
        
        #region Private Methods
        /// <summary>
        /// this function will fetch the values from Database and populate the DialerRules Gridview.
        /// </summary>
        private void populateCallDispGrid()
        {
            try
            {
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsCallDisp = new DataSet();
                dsCallDisp = myObj.getCallDispositions("LocalMySqlServer");

                DataTable dtDialerRules = new DataTable();
                dtDialerRules = dsCallDisp.Tables[0];

                gvCallDisposition.DataSource = dtDialerRules;
                gvCallDisposition.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// this function will takes the CallDispositionID as input and selects the remaining Call Disposition record values from 
        /// DB and populate in the screen.
        /// </summary>
        private void populateSelectedRecord(int iCallDispID)
        {
            try
            {
                string strDoNotCall = "";
                string strSale = "";
                string strContact = "";
                string strBadLead = "";
                string strCallBack = "";
                string strNotInterested = "";
                string strSystemDefaults = "";
                string strCustom = "";
                string strNotCalled = "";
                string strDialed = "";
                string strNotDialed = "";
                string strProcessedGood = "";
                string strProcessedBad = "";
                string strNotProcessed = "";

                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsSelCallDisp = new DataSet();
                dsSelCallDisp = myObj.getCallDispByID("LocalMySqlServer", iCallDispID);

                // Assigning the selected Dailer Rule values to the corresponding fields...
                //hdnCallDispId.Value = dsSelCallDisp.Tables[0].Rows[0]["CallDispositionID"].ToString();
                txtCallDisposition.Text = dsSelCallDisp.Tables[0].Rows[0]["CallDisposition"].ToString();
                
                #region converting the Boolean values (Yes, No) to 1 & 0 respectively...
                if ((dsSelCallDisp.Tables[0].Rows[0]["DoNotCall"].ToString() == "Yes"))
                {
                    strDoNotCall = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["DoNotCall"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["DoNotCall"] == null))
                {
                    strDoNotCall = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["Sale"].ToString() == "Yes"))
                {
                    strSale = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["Sale"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["Sale"] == null))
                {
                    strSale = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["Contact"].ToString() == "Yes"))
                {
                    strContact = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["Contact"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["Contact"] == null))
                {
                    strContact = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["BadLead"].ToString() == "Yes"))
                {
                    strBadLead = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["BadLead"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["BadLead"] == null))
                {
                    strBadLead = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["CallBack"].ToString() == "Yes"))
                {
                    strCallBack = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["CallBack"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["CallBack"] == null))
                {
                    strCallBack = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["NotInterested"].ToString() == "Yes"))
                {
                    strNotInterested = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["NotInterested"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["NotInterested"] == null))
                {
                    strNotInterested = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["SystemDefaults"].ToString() == "Yes"))
                {
                    strSystemDefaults = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["SystemDefaults"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["SystemDefaults"] == null))
                {
                    strSystemDefaults = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["Custom"].ToString() == "Yes"))
                {
                    strCustom = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["Custom"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["Custom"] == null))
                {
                    strCustom = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["NotCalled"].ToString() == "Yes"))
                {
                    strNotCalled = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["NotCalled"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["NotCalled"] == null))
                {
                    strNotCalled = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["Dialed"].ToString() == "Yes"))
                {
                    strDialed = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["Dialed"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["Dialed"] == null))
                {
                    strDialed = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["NotDialed"].ToString() == "Yes"))
                {
                    strNotDialed = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["NotDialed"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["NotDialed"] == null))
                {
                    strNotDialed = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["ProcessedGood"].ToString() == "Yes"))
                {
                    strProcessedGood = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["ProcessedGood"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["ProcessedGood"] == null))
                {
                    strProcessedGood = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["ProcessedBad"].ToString() == "Yes"))
                {
                    strProcessedBad = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["ProcessedBad"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["ProcessedBad"] == null))
                {
                    strProcessedBad = "0";
                }

                if ((dsSelCallDisp.Tables[0].Rows[0]["NotProcessed"].ToString() == "Yes"))
                {
                    strNotProcessed = "1";
                }
                else if ((dsSelCallDisp.Tables[0].Rows[0]["NotProcessed"].ToString() == "No") || (dsSelCallDisp.Tables[0].Rows[0]["NotProcessed"] == null))
                {
                    strNotProcessed = "0";
                }


                #endregion

                rblDoNotCall.SelectedValue = strDoNotCall;
                rblSale.SelectedValue = strSale;
                rblContact.SelectedValue = strContact;
                rblBadLead.SelectedValue = strBadLead;
                rblCallBack.SelectedValue = strCallBack;
                rblNotInterested.SelectedValue = strNotInterested;
                rblSystemDefaults.SelectedValue = strSystemDefaults;
                rblCustom.SelectedValue = strCustom;
                rblNotCalled.SelectedValue = strNotCalled;
                rblDialed.SelectedValue = strDialed;
                rblNotDialed.SelectedValue = strNotDialed;
                rblProcessedGood.SelectedValue = strProcessedGood;
                rblProcessedBad.SelectedValue = strProcessedBad;
                rblNotProcessed.SelectedValue = strNotProcessed;

                
                // setting the values in the hidden field to identify the modifications during the Save button click..
                hdnCallDisposition.Value = dsSelCallDisp.Tables[0].Rows[0]["CallDisposition"].ToString();
                hdnDoNotCall.Value = strDoNotCall;
                hdnSale.Value = strSale;
                hdnContact.Value = strContact;
                hdnBadLead.Value = strBadLead;
                hdnCallBack.Value = strCallBack;
                hdnNotInterested.Value = strNotInterested;
                hdnSystemDefaults.Value = strSystemDefaults;
                hdnCustom.Value = strCustom;
                hdnNotCalled.Value = strNotCalled;
                hdnDialed.Value = strDialed;
                hdnNotDialed.Value = strNotDialed;
                hdnProcessedGood.Value = strProcessedGood;
                hdnProcessedBad.Value = strProcessedBad;
                hdnNotProcessed.Value = strNotProcessed;
                
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
                foreach (GridViewRow gvRow in gvCallDisposition.Rows)
                {
                    System.Web.UI.WebControls.RadioButton rb = (System.Web.UI.WebControls.RadioButton)gvCallDisposition.Rows[iCnt].FindControl("rdEditCallDisp");
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
                populateCallDispGrid();

                // setting the default values for the controls
                txtCallDisposition.Text = "";
                txtCallDisposition.BackColor = System.Drawing.Color.White;
                rblDoNotCall.ClearSelection();
                rblDoNotCall.BackColor = System.Drawing.Color.White;
                rblSale.ClearSelection();
                rblSale.BackColor = System.Drawing.Color.White;
                rblContact.ClearSelection();
                rblContact.BackColor = System.Drawing.Color.White;
                rblBadLead.ClearSelection();
                rblBadLead.BackColor = System.Drawing.Color.White;
                rblCallBack.ClearSelection();
                rblCallBack.BackColor = System.Drawing.Color.White;
                rblNotInterested.ClearSelection();
                rblNotInterested.BackColor = System.Drawing.Color.White;
                rblSystemDefaults.ClearSelection();
                rblSystemDefaults.BackColor = System.Drawing.Color.White;
                rblCustom.ClearSelection();
                rblCustom.BackColor = System.Drawing.Color.White;
                rblNotCalled.ClearSelection();
                rblNotCalled.BackColor = System.Drawing.Color.White;
                rblDialed.ClearSelection();
                rblDialed.BackColor = System.Drawing.Color.White;
                rblNotDialed.ClearSelection();
                rblNotDialed.BackColor = System.Drawing.Color.White;
                rblProcessedGood.ClearSelection();
                rblProcessedGood.BackColor = System.Drawing.Color.White;
                rblProcessedBad.ClearSelection();
                rblProcessedBad.BackColor = System.Drawing.Color.White;
                rblNotProcessed.ClearSelection();
                rblNotProcessed.BackColor = System.Drawing.Color.White;

                ConfirmationMessage.InnerText = "";

                // clearing the hidden field values ...
                hdnCallDispId.Value = "";
                hdnCallDisposition.Value = "";
                hdnDoNotCall.Value = "";
                hdnSale.Value = "";
                hdnContact.Value = "";
                hdnBadLead.Value = "";
                hdnCallBack.Value = "";
                hdnNotInterested.Value = "";
                hdnSystemDefaults.Value = "";
                hdnCustom.Value = "";
                hdnNotCalled.Value = "";
                hdnDialed.Value = "";
                hdnNotDialed.Value = "";
                hdnProcessedGood.Value = "";
                hdnProcessedBad.Value = "";
                hdnNotProcessed.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



#endregion

      
    }
}