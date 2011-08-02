<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="DialerRules.aspx.cs" Inherits="ACMGAdmin.MasterInterfaces.DialerRules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    
    function highlightModFields() {

        // checking whether the Page is valid, to implement the below given check in the form
        if (Page_IsValid) {
            // variable declarations
            var bChanged = false;

            var txtDaysBetDialsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtDaysBetDials").ClientID%>');
            var txtHrsBetDialsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtHrsBetDials").ClientID%>');
            var txtMinsBetDialsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtMinsBetDials").ClientID%>');
            var txtMaxAttemptsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtMaxAttempts").ClientID%>');
            var txtMaxDaysInpoolObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtMaxDaysInpool").ClientID%>');
            var txtStartTimeESTObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtStartTimeEST").ClientID%>');
            var txtEndTimeESTObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtEndTimeEST").ClientID%>');
            var rblHolidaysObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblHolidays").ClientID%>');
            var rblActiveObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblActive").ClientID%>');
            var txtPriorityObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtPriority").ClientID%>');
            var txtArchiveAfterDaysObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtArchiveAfterDays").ClientID%>');

            var hdnDaysBetDialsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnDaysBetDials").ClientID%>');
            var hdnHrsBetDialsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnHrsBetDials").ClientID%>');
            var hdnMinsBetDialsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnMinsBetDials").ClientID%>');
            var hdnMaxAttemptsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnMaxAttempts").ClientID%>');
            var hdnMaxDaysInpoolObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnMaxDaysInpool").ClientID%>');
            var hdnStartTimeESTObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnStartTimeEST").ClientID%>');
            var hdnEndTimeESTObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnEndTimeEST").ClientID%>');
            var hdnDialHolidaysObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnDialHolidays").ClientID%>');
            var hdnDialActiveObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnDialActive").ClientID%>');
            var hdnPriorityObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnPriority").ClientID%>');
            var hdnArchiveAfterDaysObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnArchiveAfterDays").ClientID%>');


            // comparing & identifying whether the changes has been donw in the screen
            if (txtDaysBetDialsObj.value != hdnDaysBetDialsObj.value) {
                bChanged = true;
                txtDaysBetDialsObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtDaysBetDialsObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtHrsBetDialsObj.value != hdnHrsBetDialsObj.value) {
                bChanged = true;
                txtHrsBetDialsObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtHrsBetDialsObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtMinsBetDialsObj.value != hdnMinsBetDialsObj.value) {
                bChanged = true;
                txtMinsBetDialsObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtMinsBetDialsObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtMaxAttemptsObj.value != hdnMaxAttemptsObj.value) {
                bChanged = true;
                txtMaxAttemptsObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtMaxAttemptsObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtMaxDaysInpoolObj.value != hdnMaxDaysInpoolObj.value) {
                bChanged = true;
                txtMaxDaysInpoolObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtMaxDaysInpoolObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtStartTimeESTObj.value != hdnStartTimeESTObj.value) {
                bChanged = true;
                txtStartTimeESTObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtStartTimeESTObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtEndTimeESTObj.value != hdnEndTimeESTObj.value) {
                bChanged = true;
                txtEndTimeESTObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtEndTimeESTObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtStartTimeESTObj.value != hdnStartTimeESTObj.value) {
                bChanged = true;
                txtStartTimeESTObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtStartTimeESTObj.style.backgroundColor = '#FFFFFF';
            }

           
             // checking the Radiobutton selected value
            for (var i = 0; i < '<%= rblHolidays.Items.Count %>'; i++) {

                if (document.getElementById(rblHolidaysObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblHolidaysObj.id + "_" + [i].toString()).value != hdnDialHolidaysObj.value) {
                        bChanged = true;
                        rblHolidaysObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblHolidaysObj.style.backgroundColor = '#FFFFFF';
                    }
                    
                }
            }


            for (var i = 0; i < '<%= rblActive.Items.Count %>'; i++) {

                if (document.getElementById(rblActiveObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblActiveObj.id + "_" + [i].toString()).value != hdnDialActiveObj.value) {
                        bChanged = true;
                        rblActiveObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblActiveObj.style.backgroundColor = '#FFFFFF';
                    }
                    
                }
            }

            
            if (txtPriorityObj.value != hdnPriorityObj.value) {
                bChanged = true;
                txtPriorityObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtPriorityObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtArchiveAfterDaysObj.value != hdnArchiveAfterDaysObj.value) {
                bChanged = true;
                txtArchiveAfterDaysObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtArchiveAfterDaysObj.style.backgroundColor = '#FFFFFF';
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
                <td>
                    <h3>
                    <asp:Label ID="lblHeader" CssClass="detailheader" runat="server" Text="Dialer Rule Details:"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvDialerRules" runat="server" AutoGenerateColumns="false" Font-Size="9" 
                     AllowPaging="true" PageSize="10" 
                        onpageindexchanging="gvDialerRules_PageIndexChanging">
                        <AlternatingRowStyle BackColor="#E9ECF1" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#5970A6" Font-Bold="True" Height="20px" ForeColor="White" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdEditDialer" runat="server" AutoPostBack="true" Text='<%# Eval("DialerRulesID") %>'
                                        OnCheckedChanged="rdEditDialer_CheckedChanged" width="0px" Font-Size="0px"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CampaignId" ReadOnly="true" Visible="false" HeaderText="Campaign Id"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CampaignName" ReadOnly="true" HeaderText="Campaign" 
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PhoneType" ReadOnly="true" HeaderText="Type" HeaderStyle-CssClass="detailheader"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DaysBetweenDials" ReadOnly="true" HeaderText="Days BTW Dials"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HoursBetweenDials" ReadOnly="true" HeaderText="Hrs BTW Dials"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MinutesBetweenDials" ReadOnly="true" HeaderText="Min BTW Dials"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MaxAttempts" ReadOnly="true" HeaderText="Max Attempt"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MaxDaysInPool" ReadOnly="true" HeaderText="Max Days"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="StartDialTime_EST" ReadOnly="true" HeaderText="Start Dial"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EndDialTime_EST" ReadOnly="true" HeaderText="End Dial"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DialOnHolidays" ReadOnly="true" HeaderText="Holidays"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DialActive" ReadOnly="true" HeaderText="Active" HeaderStyle-CssClass="detailheader"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DialPriority" ReadOnly="true" HeaderText="Priority"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ArchiveAfterDays" ReadOnly="true" HeaderText="Archive Days"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br />
        <table>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblCmpName" Text="Campaign Name" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCmpame" runat="server" BorderStyle="None" BackColor="White" Width="150px"
                        CssClass="detailitem" ReadOnly="true" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblPhoneType" Text="Phone Type" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhoneType" BorderStyle="None" BackColor="White" runat="server"
                        Width="150px" CssClass="detailitem" ReadOnly="true" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblDaysBetDials" Text="Days Between Dials" CssClass="detailheader"
                        runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDaysBetDials" Width="100px" runat="server" CssClass="detailitem" MaxLength="2"
                        Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqvalDaysBetDials" Display="Dynamic" ForeColor="Red" ControlToValidate="txtDaysBetDials" 
                        runat="server" ErrorMessage="Please enter days between dials value !"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvDaysBetDials" runat="server" Display="Dynamic"
                        ControlToValidate="txtDaysBetDials" MinimumValue="0" MaximumValue="14" ForeColor="Red"
                        ErrorMessage="Please enter value between 0 - 14" Type="Integer"></asp:RangeValidator>
                    <asp:RegularExpressionValidator ID="regDaysBetDials" Display="Dynamic" runat="server" ControlToValidate="txtDaysBetDials" 
                    ValidationExpression="^\d*" ForeColor="Red" ErrorMessage="Please enter number only!"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblHrsBetDials" Text="Hours Between Dials" CssClass="detailheader"
                        runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHrsBetDials" Width="100px" runat="server" CssClass="detailitem" MaxLength="2"
                        Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvHrsBetDials" Display="Dynamic" ForeColor="Red" ControlToValidate="txtHrsBetDials" runat="server" ErrorMessage="Please enter hours between dials value !"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvHrsBetDials" runat="server" Display="Dynamic"
                        ControlToValidate="txtHrsBetDials" MinimumValue="0" MaximumValue="23" ForeColor="Red"
                        ErrorMessage="Please enter value between 0 - 23" Type="Integer"></asp:RangeValidator>
                    <asp:RegularExpressionValidator ID="rgvHrsBetDials" Display="Dynamic" runat="server" ControlToValidate="txtHrsBetDials" 
                    ValidationExpression="^\d*" ForeColor="Red" ErrorMessage="Please enter number only!"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblMinsBetDials" Text="Minutes Between Dials" CssClass="detailheader"
                        runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMinsBetDials" Width="100px" runat="server" CssClass="detailitem" MaxLength="2"
                        Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMinsBetDials"  Display="Dynamic" ForeColor="Red" ControlToValidate="txtMinsBetDials" runat="server" ErrorMessage="Please enter minutes between dials value!"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvMinsBetDials" runat="server" Display="Dynamic"
                        ControlToValidate="txtMinsBetDials" MinimumValue="0" MaximumValue="59" ForeColor="Red"
                        ErrorMessage="Please enter value between 0 - 59" Type="Integer"></asp:RangeValidator>
                    <asp:RegularExpressionValidator ID="rgvMinsBetDials" Display="Dynamic" runat="server" ControlToValidate="txtMinsBetDials" 
                    ValidationExpression="^\d*" ForeColor="Red" ErrorMessage="Please enter number only!"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblMaxAttempts" Text="Max Attempts" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMaxAttempts" Width="100px" runat="server" CssClass="detailitem" MaxLength="2"
                        Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMaxAttempts"  Display="Dynamic" ForeColor="Red" ControlToValidate="txtMaxAttempts" runat="server" ErrorMessage="Please enter max attempts value !"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvMaxAttempts" runat="server" Display="Dynamic"
                        ControlToValidate="txtMaxAttempts" MinimumValue="1" MaximumValue="20" ForeColor="Red"
                        ErrorMessage="Please enter value between 1 - 20" Type="Integer"></asp:RangeValidator>
                    <asp:RegularExpressionValidator ID="rgvMaxAttempts" Display="Dynamic" runat="server" ControlToValidate="txtMaxAttempts" 
                    ValidationExpression="^\d*" ForeColor="Red" ErrorMessage="Please enter number only!"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblMaxDaysInPool" Text="Max Days In Pool" CssClass="detailheader"
                        runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMaxDaysInpool" Width="100px" runat="server" CssClass="detailitem" MaxLength="2"
                        Text=""></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvMaxDaysInPool"  Display="Dynamic" ForeColor="Red" ControlToValidate="txtMaxDaysInpool" runat="server" ErrorMessage="Please enter max days in pool value !"></asp:RequiredFieldValidator>
                     <asp:RangeValidator ID="rvMaxDaysInpool" runat="server" Display="Dynamic"
                        ControlToValidate="txtMaxDaysInpool" MinimumValue="1" MaximumValue="30" ForeColor="Red"
                        ErrorMessage="Please enter value between 1 - 30" Type="Integer"></asp:RangeValidator>
                     <asp:RegularExpressionValidator ID="rgvMaxDaysInPool" Display="Dynamic" runat="server" ControlToValidate="txtMaxDaysInpool" 
                    ValidationExpression="^\d*" ForeColor="Red" ErrorMessage="Please enter number only!"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblStartTimeEST" Text="Start DialTime EST" CssClass="detailheader"
                        runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStartTimeEST" Width="100px" runat="server" CssClass="detailitem" MaxLength="5"
                        Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvStartTime"  Display="Dynamic" ForeColor="Red" ControlToValidate="txtStartTimeEST" runat="server" ErrorMessage="Please enter start time value !"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regvalStartTime" Display="Dynamic" ForeColor="Red" runat="server"
                    ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtStartTimeEST" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>
              
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblEndTimeEST" Text="End DialTime EST" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEndTimeEST" Width="100px" runat="server" CssClass="detailitem" MaxLength="5"
                        Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEndTime"  Display="Dynamic" ForeColor="Red" ControlToValidate="txtEndTimeEST" runat="server" ErrorMessage="Please enter end time value !"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regvalEndTime" Display="Dynamic" ForeColor="Red" runat="server"
                    ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ControlToValidate="txtEndTimeEST" ErrorMessage="Please enter time in 24 hr format"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblDialHolidays" Text="Dial On Holidays" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblHolidays" runat="server" RepeatDirection="Horizontal"
                        Width="150px">
                        <asp:ListItem Text="True" Value="1"></asp:ListItem>
                        <asp:ListItem  Text="False" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblDialActive" Text="Dial Active" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblActive" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="True" Value="1"></asp:ListItem>
                        <asp:ListItem Text="False" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblPriority" Text="Dial Priority" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPriority" runat="server" Width="100px" CssClass="detailitem" MaxLength="2"
                        Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPriority"  Display="Dynamic" ForeColor="Red" ControlToValidate="txtPriority" runat="server" ErrorMessage="Please enter priority value !"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="rgvPriority" Display="Dynamic" runat="server" ControlToValidate="txtPriority" 
                    ValidationExpression="^\d*" ForeColor="Red" ErrorMessage="Please enter number only!"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblArchiveDays" Text="Archive After Days" CssClass="detailheader"
                        runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtArchiveAfterDays" runat="server" Width="100px" CssClass="detailitem" MaxLength="3"
                        Text=""></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvArchiveAfterDays"  Display="Dynamic" ForeColor="Red" ControlToValidate="txtArchiveAfterDays" runat="server" ErrorMessage="Please enter archive value !"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvArchiveAfterDays" runat="server" Display="Dynamic"
                        ControlToValidate="txtArchiveAfterDays" MinimumValue="1" MaximumValue="365" ForeColor="Red"
                        ErrorMessage="Please enter value between 1 - 365" Type="Integer"></asp:RangeValidator>
                    <asp:RegularExpressionValidator ID="rgvArchiveAfterDays" Display="Dynamic" runat="server" ControlToValidate="txtArchiveAfterDays" 
                    ValidationExpression="^\d*" ForeColor="Red" ErrorMessage="Please enter number only!"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClientClick="return highlightModFields();"
                        onclick="btnSave_Click"  />
                </td>
                <td align="left">
                    <asp:Button ID="btnAddNew" runat="server" Visible="false" Text="AddNew" />
                </td>
            </tr>
            <tr>
	            <td colspan="2" align="center">
	                <div id="ConfirmationMessage" runat="server" class="alert" ></div>
	            </td>
            </tr>
            <tr>
	            <td colspan="2" align="center">
	                <div id="divErrorMsg" runat="server" style="color:Red" ></div>
	            </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnDialerRuleId" runat="server" />
                    <asp:HiddenField ID="hdnCampaignId" runat="server" />

                    <asp:HiddenField ID="hdnCmpame" runat="server" />
                    <asp:HiddenField ID="hdnPhoneType" runat="server" />
                    <asp:HiddenField ID="hdnDaysBetDials" runat="server" />
                    <asp:HiddenField ID="hdnHrsBetDials" runat="server" />
                    <asp:HiddenField ID="hdnMinsBetDials" runat="server" />
                    <asp:HiddenField ID="hdnMaxAttempts" runat="server" />
                    <asp:HiddenField ID="hdnMaxDaysInpool" runat="server" />
                    <asp:HiddenField ID="hdnStartTimeEST" runat="server" />
                    <asp:HiddenField ID="hdnEndTimeEST" runat="server" />
                    <asp:HiddenField ID="hdnDialHolidays" runat="server" />
                    <asp:HiddenField ID="hdnDialActive" runat="server" />
                    <asp:HiddenField ID="hdnPriority" runat="server" />
                    <asp:HiddenField ID="hdnArchiveAfterDays" runat="server" />
                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
