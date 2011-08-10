<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="CallingTimesByState.aspx.cs" Inherits="ACMGAdmin.MasterInterfaces.CallingTimesByState" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">

    function highlightModFields() {

        // checking whether the Page is valid, to implement the below given check in the form
        if (Page_IsValid) {

            // variable declarations
            var bChanged = false;

            var txtStateObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtState").ClientID%>');
            var txtWeekdayStartTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtWeekdayStartTime").ClientID%>');
            var txtWeekdayEndTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtWeekdayEndTime").ClientID%>');
            var txtSaturdayStartTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtSaturdayStartTime").ClientID%>');
            var txtSaturdayEndTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtSaturdayEndTime").ClientID%>');
            var txtSundayStartTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtSundayStartTime").ClientID%>');
            var txtSundayEndTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtSundayEndTime").ClientID%>');
            var txtHolidayStartTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtHolidayStartTime").ClientID%>');
            var txtHolidayEndTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtHolidayEndTime").ClientID%>');

            var hdnStateObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnState").ClientID%>');
            var hdnWeekdayStartTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnWeekdayStartTime").ClientID%>');
            var hdnWeekdayEndTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnWeekdayEndTime").ClientID%>');
            var hdnSaturdayStartTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnSaturdayStartTime").ClientID%>');
            var hdnSaturdayEndTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnSaturdayEndTime").ClientID%>');
            var hdnSundayStartTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnSundayStartTime").ClientID%>');
            var hdnSundayEndTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnSundayEndTime").ClientID%>');
            var hdnHolidayStartTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnHolidayStartTime").ClientID%>');
            var hdnHolidayEndTimeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnHolidayEndTime").ClientID%>');

            // comparing & identifying whether the changes has been done in the screen
            if (txtStateObj.value != hdnStateObj.value) {
                bChanged = true;
                txtStateObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtStateObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtWeekdayStartTimeObj.value != hdnWeekdayStartTimeObj.value) {
                bChanged = true;
                txtWeekdayStartTimeObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtWeekdayStartTimeObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtWeekdayEndTimeObj.value != hdnWeekdayEndTimeObj.value) {
                bChanged = true;
                txtWeekdayEndTimeObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtWeekdayEndTimeObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtSaturdayStartTimeObj.value != hdnSaturdayStartTimeObj.value) {
                bChanged = true;
                txtSaturdayStartTimeObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtSaturdayStartTimeObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtSaturdayEndTimeObj.value != hdnSaturdayEndTimeObj.value) {
                bChanged = true;
                txtSaturdayEndTimeObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtSaturdayEndTimeObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtSundayStartTimeObj.value != hdnSundayStartTimeObj.value) {
                bChanged = true;
                txtSundayStartTimeObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtSundayStartTimeObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtSundayEndTimeObj.value != hdnSundayEndTimeObj.value) {
                bChanged = true;
                txtSundayEndTimeObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtSundayEndTimeObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtHolidayStartTimeObj.value != hdnHolidayStartTimeObj.value) {
                bChanged = true;
                txtHolidayStartTimeObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtHolidayStartTimeObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtHolidayEndTimeObj.value != hdnHolidayEndTimeObj.value) {
                bChanged = true;
                txtHolidayEndTimeObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtHolidayEndTimeObj.style.backgroundColor = '#FFFFFF';
            }

            // checking whether any one changes has been applied, then to pop-up the alert msg...
            if (bChanged) {
                var out = confirm("Highlighted Fields have been modified, Do you want to save?");
                if (out) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                var outRes = confirm("No changes have been made, Do you want to save?");
                if (outRes) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }


    </script>




<div>

    <table>
            <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblState" Text="State" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                 <asp:TextBox ID="txtState" Width="50px" Font-Names="Arial" MaxLength="2"
                        runat="server" Style="text-transform: uppercase" CssClass="detailitem"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvState" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtState" ErrorMessage="Please Enter State !"></asp:RequiredFieldValidator>
                </td>
            </tr>
        
             <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblWeekdayStartTime" Text="Weekday StartTime" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWeekdayStartTime" Width="50px" Font-Names="Arial"  maxlength="5"
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvWeekdayStartTime" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtWeekdayStartTime" ErrorMessage="Please Enter Value !"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rgvWeekdayStartTime" Display="Dynamic" ForeColor="Red" runat="server"
                        ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtWeekdayStartTime" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>
              
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblWeekdayEndTime" Text="Weekday EndTime" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWeekdayEndTime" Width="50px" Font-Names="Arial" maxlength="5"
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvWeekdayEndTime" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtWeekdayEndTime" ErrorMessage="Please Enter Value !"></asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="rgvWeekdayEndTime" Display="Dynamic" ForeColor="Red" runat="server"
                            ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtWeekdayEndTime" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblSaturdayStartTime" Text="Saturday StartTime" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSaturdayStartTime" Width="50px" Font-Names="Arial"  maxlength="5"
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvSaturdayStartTime" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtSaturdayStartTime" ErrorMessage="Please Enter Value !"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgvSaturdayStartTime" Display="Dynamic" ForeColor="Red" runat="server"
                            ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtSaturdayStartTime" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>

                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblSaturdayEndTime" Text="Saturday EndTime" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSaturdayEndTime" Width="50px" Font-Names="Arial"  maxlength="5"
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvSaturdayEndTime" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtSaturdayEndTime" ErrorMessage="Please Enter Value !"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="rgvSaturdayEndTime" Display="Dynamic" ForeColor="Red" runat="server"
                            ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtSaturdayEndTime" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>

                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblSundayStartTime" Text="Sunday StartTime" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSundayStartTime" Width="50px" Font-Names="Arial"  maxlength="5"
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvSundayStartTime" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtSundayStartTime" ErrorMessage="Please Enter Value !"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="rgvSundayStartTime" Display="Dynamic" ForeColor="Red" runat="server"
                            ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtSundayStartTime" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblSundayEndTime" Text="Sunday EndTime" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSundayEndTime" Width="50px" Font-Names="Arial" maxlength="5"
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvSundayEndTime" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtSundayEndTime" ErrorMessage="Please Enter Value !"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="rgvSundayEndTime" Display="Dynamic" ForeColor="Red" runat="server"
                            ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtSundayEndTime" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblHolidayStartTime" Text="Holiday StartTime" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHolidayStartTime" Width="50px" Font-Names="Arial"  MaxLength="5" 
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvHolidayStartTime" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtHolidayStartTime" ErrorMessage="Please Enter Value !"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rgvHolidayStartTime" Display="Dynamic" ForeColor="Red" runat="server"
                            ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtHolidayStartTime" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>

                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblHolidayEndTime" Text="Holiday EndTime" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHolidayEndTime" Width="50px" Font-Names="Arial"  maxlength="5"
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvHolidayEndTime" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtHolidayEndTime" ErrorMessage="Please Enter Value !"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="rgvHolidayEndTime" Display="Dynamic" ForeColor="Red" runat="server"
                            ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtHolidayEndTime" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>
                </td>
            </tr>


            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save Changes" Enabled="false" 
                        onclick="btnSave_Click" />
                </td>
                <td align="left">
                    <asp:Button ID="btnAddNew" runat="server" Text="AddNew" TabIndex="8" CausesValidation="false"  OnClientClick="return confirm('Do you want to clear the fields and add a new record?')"
                        onclick="btnAddNew_Click" />
                </td>
            </tr>
            <tr>
	            <td colspan="2" align="center">
	                <div id="ConfirmationMessage" runat="server" class="alert" ></div>
	            </td>
            </tr>

            <tr>
	            <td colspan="2" align="center">
	                <div id="divErrorMsg" runat="server" style="color:Red"></div>
	            </td>
            </tr>
            <tr>
                 <td>
                    <asp:HiddenField ID="hdnState" runat="server" />
                    <asp:HiddenField ID="hdnWeekdayStartTime" runat="server" />
                    <asp:HiddenField ID="hdnWeekdayEndTime" runat="server" />
                    <asp:HiddenField ID="hdnSaturdayStartTime" runat="server" />
                    <asp:HiddenField ID="hdnSaturdayEndTime" runat="server" />
                    <asp:HiddenField ID="hdnSundayStartTime" runat="server" />
                    <asp:HiddenField ID="hdnSundayEndTime" runat="server" />
                    <asp:HiddenField ID="hdnHolidayStartTime" runat="server" />
                    <asp:HiddenField ID="hdnHolidayEndTime" runat="server" />
                    <asp:HiddenField ID="hdnFlagType" runat="server" />
                </td>
            
            </tr>
        </table>
    
    <br />

    <table>
            <tr>
                <td>
                    <h3>
                    <asp:Label ID="lblHeader" CssClass="detailheader" runat="server" Text="Calling Times By State Details:"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvCallingTimesByState" runat="server" 
                        AutoGenerateColumns="False" Font-Size="9pt"
                        AllowPaging="True" 
                        OnPageIndexChanging="gvCallingTimesByState_PageIndexChanging" PageSize="100" 
                        >
                        <AlternatingRowStyle BackColor="#E9ECF1" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#5970A6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Edit"  >
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdEditCallingTimes" runat="server"  AutoPostBack="true" OnCheckedChanged="rdEditCallingTimes_OnCheckedChanged"
                                         Text='<%# Eval("State") %>' TextAlign ="Right"  width="0px" Font-Size="0px" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="White" Font-Size="0px" Width="0px"/>
                                <ItemStyle CssClass="detailitem" Width="0px"/>
                            </asp:TemplateField>
                            <asp:BoundField DataField="State" ReadOnly="true" HeaderText="State" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="125px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="125px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="WeekdayStartTime" ReadOnly="true" HeaderText="Weekday StartTime" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="WeekdayEndTime" ReadOnly="true" HeaderText="Weekday EndTime" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SaturdayStartTime" ReadOnly="true" HeaderText="Saturday StartTime"  
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="SaturdayEndTime" ReadOnly="true" HeaderText="Saturday EndTime" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SundayStartTime" ReadOnly="true" HeaderText="Sunday StartTime" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SundayEndTime" ReadOnly="true" HeaderText="Sunday EndTime"  
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HolidayStartTime" ReadOnly="true" HeaderText="Holiday StartTime" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HolidayEndTime" ReadOnly="true" HeaderText="Holiday EndTime"  
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="125px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="125px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        

</div>


</asp:Content>
