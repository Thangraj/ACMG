<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="Holidays.aspx.cs" Inherits="ACMGAdmin.MasterInterfaces.Holidays" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   // function which will highlight the field values whcih have been changed in Edit Mode and intimate the user with the
 <script type="text/javascript">

    // message box...
    function highlightModFields() {

        // checking whether the Page is valid, to implement the below given check in the form
        if (Page_IsValid) {

            // variable declarations
            var bChanged = false;

            var txtStateObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("drpState").ClientID%>');
            var monthObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("drpMonth").ClientID%>');
            var dayObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("drpDay").ClientID%>');
            var holidayNameObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtHolidayName").ClientID%>');
            var notesObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtNotes").ClientID%>');
            var dateObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtTheDate").ClientID%>');

            var hdnStateObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnState").ClientID%>');
            var hdnMonthObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnMonth").ClientID%>');
            var hdnDayObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnDay").ClientID%>');
            var hdnHolidayNameObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnHolidayName").ClientID%>');
            var hdnNotesObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnNotes").ClientID%>');
            var hdnDateObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnDate").ClientID%>');

            // comparing & identifying whether the changes has been donw in the screen
            if (txtStateObj.value != hdnStateObj.value) {
                bChanged = true;
                txtStateObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtStateObj.style.backgroundColor = '#FFFFFF';
            }

            if (monthObj.value != hdnMonthObj.value) {
                bChanged = true;
                monthObj.style.backgroundColor = '#FF3366';
            }
            else {
                monthObj.style.backgroundColor = '#FFFFFF';
            }

            if (dayObj.value != hdnDayObj.value) {
                bChanged = true;
                dayObj.style.backgroundColor = '#FF3366';
            }
            else {
                dayObj.style.backgroundColor = '#FFFFFF';
            }

            if (holidayNameObj.value != hdnHolidayNameObj.value) {
                bChanged = true;
                holidayNameObj.style.backgroundColor = '#FF3366';
            }
            else {
                holidayNameObj.style.backgroundColor = '#FFFFFF';
            }

            if (notesObj.value != hdnNotesObj.value) {
                bChanged = true;
                notesObj.style.backgroundColor = '#FF3366';
            }
            else {
                notesObj.style.backgroundColor = '#FFFFFF';
            }

            if (dateObj.value != hdnDateObj.value) {
                bChanged = true;
                dateObj.style.backgroundColor = '#FF3366';
            }
            else {
                dateObj.style.backgroundColor = '#FFFFFF';
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
                    <asp:Label ID="lblHeader" CssClass="detailheader" runat="server" Text="Holiday Details:"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvHolidays" runat="server" AutoGenerateColumns="False" Font-Size="9pt"
                        AllowPaging="True" OnPageIndexChanging="gvHolidays_PageIndexChanging" 
                        AllowSorting="true" onsorting="gvHolidays_Sorting">
                        <AlternatingRowStyle BackColor="#E9ECF1" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#5970A6" Font-Bold="True" ForeColor="White" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Edit"  >
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdEditHoliday" runat="server"  GroupName="HolidayGroup" AutoPostBack="true"
                                         oncheckedchanged="rdEditHoliday_CheckedChanged" Text='<%# Eval("HolidayId") %>' TextAlign ="Right"  width="0px" Font-Size="0px" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="White" Font-Size="0px" Width="0px"/>
                                <ItemStyle CssClass="detailitem" Width="0px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="HolidayId" ReadOnly="true" Visible="false" HeaderText="Holiday Id"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="State" ReadOnly="true" HeaderText="State" HeaderStyle-CssClass="detailheader" SortExpression="State"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="50px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Month" ReadOnly="true" HeaderText="Month" HeaderStyle-CssClass="detailheader" SortExpression="Month"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="50px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Day" ReadOnly="true" HeaderText="Day" HeaderStyle-CssClass="detailheader" SortExpression="Day"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="75px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HolidayName" ReadOnly="true" HeaderText="Holiday Name" SortExpression="HolidayName"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="175px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="175px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Notes" ReadOnly="true" HeaderText="Notes" HeaderStyle-CssClass="detailheader"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="350px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="350px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TheDate" ReadOnly="true" HeaderText="TheDate" HeaderStyle-CssClass="detailheader"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="125px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="125px" HorizontalAlign="Center"></ItemStyle>
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
                <td style="background-color: #E9ECF1; width: 300px">
                    <asp:Label ID="lblState" Text="State" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpState" runat="server" >
                    </asp:DropDownList>
               
            </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1; width: 300px">
                    <asp:Label ID="lblMonth" Text="Month" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpMonth" runat="server" TabIndex="2">
                        <asp:ListItem Selected="True" Text="01" Value="01"></asp:ListItem>
                        <asp:ListItem Text="02" Value="02"></asp:ListItem>
                        <asp:ListItem Text="03" Value="03"></asp:ListItem>
                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
                        <asp:ListItem Text="06" Value="06"></asp:ListItem>
                        <asp:ListItem Text="07" Value="07"></asp:ListItem>
                        <asp:ListItem Text="08" Value="08"></asp:ListItem>
                        <asp:ListItem Text="09" Value="09"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1; width: 300px">
                    <asp:Label ID="lblDay" Text="Day" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpDay" runat="server" TabIndex="3">
                        <asp:ListItem Selected="True" Text="01" Value="01"></asp:ListItem>
                        <asp:ListItem Text="02" Value="02"></asp:ListItem>
                        <asp:ListItem Text="03" Value="03"></asp:ListItem>
                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
                        <asp:ListItem Text="06" Value="06"></asp:ListItem>
                        <asp:ListItem Text="07" Value="07"></asp:ListItem>
                        <asp:ListItem Text="08" Value="08"></asp:ListItem>
                        <asp:ListItem Text="09" Value="09"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1; width: 300px">
                    <asp:Label ID="lblHolidayName" Text="Holiday Name" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHolidayName" Width="250px" Font-Names="Arial" TabIndex="4"
                        runat="server" CssClass="detailitem"  Text="" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvHolidayName" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtHolidayName" ErrorMessage="Please enter holiday name value !"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regValHolidayName" runat="server" ControlToValidate="txtHolidayName" Display="Dynamic"
                    ValidationExpression="(\s|.){0,255}$" ErrorMessage="Max Length is 255!"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1; width: 300px">
                    <asp:Label ID="lblNotes" Text="Notes" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNotes" Width="250px" TextMode="MultiLine" TabIndex="5"
                        Font-Names="Arial" MaxLength="255" 
                        Height="50px" runat="server" CssClass="detailitem" Text="" 
                        ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNotes" ForeColor="Red" Display="Dynamic"  runat="server" ControlToValidate="txtNotes" ErrorMessage="Please enter notes value !"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="regExValNotes" runat="server" ControlToValidate="txtNotes" Display="Dynamic"
                    ValidationExpression="(\s|.){0,255}$" ForeColor="Red" ErrorMessage="Max Length is 255!"></asp:RegularExpressionValidator>
                    
                </td>
            </tr>
            <tr>
                <td style="background-color: #E9ECF1; width: 300px">
                    <asp:Label ID="lblTheDate" Text="Next Date" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTheDate"  Width="100px" runat="server" CssClass="detailitem" MaxLength="20" Text=""></asp:TextBox>
                    <asp:ImageButton ID="ibTheDate" ImageUrl="~/images/Calendar.jpg" TabIndex="6" runat="server" CausesValidation="false"  />
                    <cc1:CalendarExtender ID="calExTheDate" PopupButtonID="ibTheDate"  PopupPosition="Right" 
                        Format="yyyy-MM-dd" runat="server" Enabled="true" TargetControlID="txtTheDate"></cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfvDate" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="txtTheDate" ErrorMessage="Please enter date value !"></asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save Changes" Enabled="false" TabIndex="7"
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
    <asp:HiddenField ID="hdnHolidayId" runat="server" />
    <asp:HiddenField ID="hdnFlagType" runat="server" />
    <asp:HiddenField ID="hdnState" runat="server" />
    <asp:HiddenField ID="hdnMonth" runat="server" />
    <asp:HiddenField ID="hdnDay" runat="server" />
    <asp:HiddenField ID="hdnHolidayName" runat="server" />
    <asp:HiddenField ID="hdnNotes" runat="server" />
    <asp:HiddenField ID="hdnDate" runat="server" />
    </td>
</tr>
        </table>
    </div>

</asp:Content>
