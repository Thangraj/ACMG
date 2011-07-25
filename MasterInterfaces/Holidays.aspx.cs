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
    /// This class contains the Add / Edit functionality of Holiday Module.
    /// <version>1.0</version>
    /// <lastmodifieddate>18-Jul-2011</lastmodifieddate>
    /// <author>CSC</author>
    /// </summary>
    
    public partial class Holidays : System.Web.UI.Page
    {
        DataSet dsHolidayList = new DataSet();
        DataView dvHoliday = new DataView();

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
                    ViewState["sortOrder"] = "";
                    ViewState["SortExpression"] = "";
                    // calling the below method to populate the Holiday GridView
                    populateHolidayGrid("","");

                    // populating the DropDown field with the State values...
                    populateDrpStates();    

                    // making the data textbox as read-only to make date selection from Calendar control.. 
                    txtTheDate.Attributes.Add("readonly", "readonly");
                }
           }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = ex.Message;
                    
            }

        }

        /// <summary>
        /// This event will be fired after the user selects the "Radiobutton" in the GridView
        /// </summary>
        protected void rdEditHoliday_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                deSelect_RB_In_Gridview();
                // clearing the old status message
                ConfirmationMessage.InnerText = "";
                // changing the back color of the controls to White...
                drpState.BackColor = System.Drawing.Color.White;
                txtHolidayName.BackColor = System.Drawing.Color.White;
                txtNotes.BackColor = System.Drawing.Color.White;

                System.Web.UI.WebControls.RadioButton senderRB = (System.Web.UI.WebControls.RadioButton)sender;
                senderRB.Checked = true;
                // setting the HolidayId in the hidden field to verify the modification at "Save Changes" button click
                hdnHolidayId.Value = senderRB.Text;
                populateSelectedRecord(Convert.ToInt32(hdnHolidayId.Value));
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
        /// This event will perform the Add / Edit functionality depending upon the user's action
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {

                    if (hdnFlagType.Value == "EDIT")
                    {

                        //hdnFlagType.Value = "";
                        DataAccess.MySQLAccess myUpdObj = new DataAccess.MySQLAccess();
                        int iOutput = myUpdObj.updateHolidayDetails("LocalMySqlServer", Convert.ToInt32(hdnHolidayId.Value), drpState.SelectedValue, drpMonth.SelectedValue, drpDay.SelectedValue, txtHolidayName.Text, txtNotes.Text, txtTheDate.Text);

                        //checking whether record has been updated
                        if (iOutput > 0)
                        {
                            populateHolidayGrid("","");
                            ConfirmationMessage.InnerText = "Updated Successfully !";
                            
                            //storing the values in the hidden fields to verify the modification & to highlight in the screen..
                            hdnState.Value = drpState.SelectedValue;
                            //hdnState.Value = txtState.Text;
                            hdnMonth.Value = drpMonth.SelectedValue;
                            hdnDay.Value = drpDay.SelectedValue;
                            hdnHolidayName.Value = txtHolidayName.Text;
                            hdnNotes.Value = txtNotes.Text;
                            hdnDate.Value = txtTheDate.Text;

                        }
                    }

                    if (hdnFlagType.Value == "ADD")
                    {
                        // call the routine to add a new Holiday record into the table..
                        DataAccess.MySQLAccess myInsObj = new DataAccess.MySQLAccess();
                        int intInsOut = myInsObj.insertHoliday("LocalMySqlServer", drpState.SelectedValue, drpMonth.SelectedValue, drpDay.SelectedValue, txtHolidayName.Text, txtNotes.Text, txtTheDate.Text);
                        if (intInsOut > 0)
                        {
                            populateHolidayGrid("","");
                            ConfirmationMessage.InnerText = "Inserted Successfully !";
                        }

                    }
                    btnAddNew.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (hdnFlagType.Value == "EDIT")
                {
                    divErrorMsg.InnerText =  "Update Failure: " + ex.Message;
                }

                if (hdnFlagType.Value == "ADD")
                {
                    divErrorMsg.InnerText = "Insert Failure: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// This event will implement the paging functionality to the Holidays Gridview.
        /// </summary>
        protected void gvHolidays_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvHolidays.PageIndex = e.NewPageIndex;
                // getting the sort expression from the viewstate
                
                string strSortExp = ViewState["SortExpression"].ToString();
                if ((strSortExp != string.Empty) && (sortOrder != string.Empty))
                {
                    populateHolidayGrid(strSortExp, sortOrder);
                }
                else
                {
                    populateHolidayGrid("", "");
                }
            }
            catch (Exception ex)
            {
                divErrorMsg.InnerText = "Error Message :" + ex.Message;
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
                hdnHolidayId.Value = "";
            }
            catch(Exception ex)
            {
                divErrorMsg.InnerText = "Error Message :" + ex.Message;
            }
            
        }

        /// <summary>
        /// This event will handle the sorting logic of the Gridview.
        /// </summary>
        protected void gvHolidays_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["SortExpression"] = e.SortExpression;
            populateHolidayGrid(e.SortExpression, sortOrder);
        }


#endregion

        #region Private Methods
        /// <summary>
        /// This function is used to fetch Holiday values from Database and populate it in Gridview
        /// </summary>

        private void populateHolidayGrid(string strSortExp, string strSortDir)
        {
            
            try
            {
               
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
               
                
                dsHolidayList = myObj.getHolidays("LocalMySqlServer");
                dvHoliday = dsHolidayList.Tables[0].DefaultView;
               
               
                //checking whether the sorting has been applied or not..
                if (strSortExp != string.Empty)
                {
                    dvHoliday.Sort = String.Format("{0} {1}", strSortExp, strSortDir);
                    // dvHoliday.Sort = (string)ViewState["SortExpression"];
                }

                gvHolidays.DataSource = dvHoliday;
                gvHolidays.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// this function will deselect all the other RadioButton and enable the selected Radiobutton only.
        /// </summary>
        private void deSelect_RB_In_Gridview()
        {
            try
            {
                Int32 iCnt = 0;
                foreach (GridViewRow gvRow in gvHolidays.Rows)
                {
                    System.Web.UI.WebControls.RadioButton rb = (System.Web.UI.WebControls.RadioButton)gvHolidays.Rows[iCnt].FindControl("rdEditHoliday");
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
        /// this function will takes the HolidayID as input and fetch the remaining Holiday record values from DB and
        /// populate in the screen..
        /// </summary>
        private void populateSelectedRecord(int iHolidayId)
        {
            try
            {
                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsSelHoliday = new DataSet();
                dsSelHoliday = myObj.getHolidayByID("LocalMySqlServer", iHolidayId);

                // Assigning the selected Holiday values to the corresponding fields...
                drpState.SelectedValue = dsSelHoliday.Tables[0].Rows[0]["State"].ToString();
                //drpState.SelectedValue = dsSelHoliday.Tables[0].Rows[0]["State"].ToString();
               // drpState.Items.FindByValue(dsSelHoliday.Tables[0].Rows[0]["State"].ToString()).Selected = true;
                drpMonth.SelectedValue = dsSelHoliday.Tables[0].Rows[0]["Month"].ToString();
                drpDay.SelectedValue = dsSelHoliday.Tables[0].Rows[0]["Day"].ToString();
                txtHolidayName.Text = dsSelHoliday.Tables[0].Rows[0]["HolidayName"].ToString();
                txtNotes.Text = dsSelHoliday.Tables[0].Rows[0]["Notes"].ToString();
                txtTheDate.Text = dsSelHoliday.Tables[0].Rows[0]["theDate"].ToString();

                //storing the values in the hidden fields to verify the modification & to highlight in the screen..
                hdnState.Value = dsSelHoliday.Tables[0].Rows[0]["State"].ToString();
                hdnMonth.Value = dsSelHoliday.Tables[0].Rows[0]["Month"].ToString();
                hdnDay.Value = dsSelHoliday.Tables[0].Rows[0]["Day"].ToString();
                hdnHolidayName.Value = dsSelHoliday.Tables[0].Rows[0]["HolidayName"].ToString();
                hdnNotes.Value = dsSelHoliday.Tables[0].Rows[0]["Notes"].ToString();
                hdnDate.Value = dsSelHoliday.Tables[0].Rows[0]["theDate"].ToString();

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
                populateHolidayGrid("","");
                // clearing all the field values in the form

                populateDrpStates();
                drpState.BackColor = System.Drawing.Color.White;
                
                // setting the default values for the dropdownlist
                drpMonth.SelectedValue = "01";
                drpDay.SelectedValue = "01";
                txtHolidayName.Text = "";
                txtHolidayName.BackColor = System.Drawing.Color.White;
                txtNotes.Text = "";
                txtNotes.BackColor = System.Drawing.Color.White;
                txtTheDate.Text = "";
                txtTheDate.BackColor = System.Drawing.Color.White;
                ConfirmationMessage.InnerText = "";
                // clearing the hidden field values ...
                hdnState.Value = "";
                hdnMonth.Value = "01";
                hdnDay.Value = "01";
                hdnHolidayName.Value = "";
                hdnNotes.Value = "";
                hdnDate.Value = "";

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// this function will be used to handle the sorting functionality of the GridView.
        /// </summary>
        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";
                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                } return ViewState["sortOrder"].ToString();
            }

            set
            {
                ViewState["sortOrder"] = value;
            }
        }



        /// <summary>
        /// This function is used to populate dropdown with the US State values from Database..
        /// </summary>

        private void populateDrpStates()
        {

            try
            {

                // code to populate the Holiday data in the Gridview..
                DataAccess.MySQLAccess myObj = new DataAccess.MySQLAccess();
                DataSet dsStateList = new DataSet();
                DataTable dtStateList = new DataTable();

                dsStateList = myObj.getUsStates("LocalMySqlServer");
                dtStateList = dsStateList.Tables[0];

                drpState.DataSource = dtStateList;
                drpState.DataValueField = "STATE_ABBR";
                drpState.DataTextField = "STATE";
                drpState.DataBind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion




    }
}