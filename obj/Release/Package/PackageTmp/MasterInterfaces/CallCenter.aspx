<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="CallCenter.aspx.cs" Inherits="ACMGAdmin.MasterInterfaces.CallCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">

    function highlightModFields() {

        // checking whether the Page is valid, to implement the below given check in the form
        if (Page_IsValid) {

            // variable declarations
            var bChanged = false;

            var CallCenterObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtCallCenter").ClientID%>');
            var CallCenterNotesObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtCallCenterNotes").ClientID%>');
            var ConnectionStringObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtConnectionString").ClientID%>');
            var activeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblActive").ClientID%>');

            var hdnCallCenterObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnCallCenterName").ClientID%>');
            var hdnCallCenterNotesObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnCallCenterNotes").ClientID%>');
            var hdnConnectionStringObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnConnectionString").ClientID%>');
            var hdnactiveObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnActive").ClientID%>');
           
            // comparing & identifying whether the changes has been donw in the screen
            if (CallCenterObj.value != hdnCallCenterObj.value) {
                bChanged = true;
                CallCenterObj.style.backgroundColor = '#FF3366';
            }
            else {
                CallCenterObj.style.backgroundColor = '#FFFFFF';
            }

            if (CallCenterNotesObj.value != hdnCallCenterNotesObj.value) {
                bChanged = true;
                CallCenterNotesObj.style.backgroundColor = '#FF3366';
            }
            else {
                CallCenterNotesObj.style.backgroundColor = '#FFFFFF';
            }

            if (ConnectionStringObj.value != hdnConnectionStringObj.value) {
                bChanged = true;
                ConnectionStringObj.style.backgroundColor = '#FF3366';
            }
            else {
                ConnectionStringObj.style.backgroundColor = '#FFFFFF';
            }

            // checking the Radiobutton selected value
            for (var i = 0; i < '<%= rblActive.Items.Count %>'; i++) {

                if (document.getElementById(activeObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(activeObj.id + "_" + [i].toString()).value != hdnactiveObj.value) {
                        bChanged = true;
                        activeObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        activeObj.style.backgroundColor = '#FFFFFF';
                    }
                    
                }
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
                <td style="background-color: #E9ECF1; width: 300px">
                    <asp:Label ID="lblCallCenterName" Text="CallCenter Name" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCallCenter" Width="250px" Font-Names="Arial" MaxLength="45"  
                        runat="server" CssClass="detailitem"  Text="" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvCallCenter" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtCallCenter" ErrorMessage="Please Enter CallCenter Name !"></asp:RequiredFieldValidator>
                </td>
            </tr>
           
            <tr>
                <td style="background-color: #E9ECF1; width: 300px">
                    <asp:Label ID="lblCallCenterNotes" Text="Notes" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCallCenterNotes" Width="250px" TextMode="MultiLine" 
                        Font-Names="Arial" MaxLength="1000" 
                        Height="50px" runat="server" CssClass="detailitem" Text="" 
                        ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCallCenterNotes" ForeColor="Red" Display="Dynamic"  runat="server" ControlToValidate="txtCallCenterNotes" ErrorMessage="Please Enter Notes Value !"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="regCallCenterNotes" runat="server" ControlToValidate="txtCallCenterNotes" Display="Dynamic"
                    ValidationExpression="(\s|.){0,1000}$" ForeColor="Red" ErrorMessage="Max Length is 1000 !"></asp:RegularExpressionValidator>
                    
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 300px">
                    <asp:Label ID="lblLDAPConnectionString" Text="LDAP ConnectionString Name" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtConnectionString" Width="250px" Font-Names="Arial" MaxLength="45"  
                        runat="server" CssClass="detailitem"  Text="" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvHolidayName" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtConnectionString" ErrorMessage="Please Enter Connection String Name !"></asp:RequiredFieldValidator>
                </td>
            </tr>
           
           <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblActive" Text="Active" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblActive" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rfvActive" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="rblActive" ErrorMessage="Please Select Active Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>
           
            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save Changes" Enabled="false" 
                        onclick="btnSave_Click" />
                </td>
                <td align="left">
                    <asp:Button ID="btnAddNew" runat="server" Text="AddNew" CausesValidation="false"  OnClientClick="return confirm('Do you want to clear the fields and add a new record?')"
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
                <asp:HiddenField ID="hdnCallCenterId" runat="server" />
                <asp:HiddenField ID="hdnCallCenterName" runat="server" />
                <asp:HiddenField ID="hdnCallCenterNotes" runat="server" />
                <asp:HiddenField ID="hdnConnectionString" runat="server" />
                <asp:HiddenField ID="hdnActive" runat="server" />
                <asp:HiddenField ID="hdnFlagType" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <h3>
                    <asp:Label ID="lblHeader" CssClass="detailheader" runat="server" Text="Call Center Details:"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvCallCenter" runat="server" AutoGenerateColumns="False" Font-Size="9pt"
                        AllowPaging="True" onpageindexchanging="gvCallCenter_PageIndexChanging" 
                        PageSize="100" >
                        <AlternatingRowStyle BackColor="#E9ECF1" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#5970A6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Edit"  >
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdEditCallCenter" runat="server"  AutoPostBack="true" OnCheckedChanged="rdEditCallCenter_CheckedChanged"
                                         Text='<%# Eval("CallCenterId") %>' TextAlign ="Right"  width="0px" Font-Size="0px" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="White" Font-Size="0px" Width="0px"/>
                                <ItemStyle CssClass="detailitem" Width="0px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CallCenterName" ReadOnly="true" HeaderText="CallCenter Name" HeaderStyle-CssClass="detailheader" SortExpression="State"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CallCenterNotes" ReadOnly="true" HeaderText="Notes" HeaderStyle-CssClass="detailheader" SortExpression="Month"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="350px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="350px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LDAPConnectStringName" ReadOnly="true" HeaderText="LDAP ConnectionString" HeaderStyle-CssClass="detailheader" SortExpression="Day"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="300px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="300px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Active" ReadOnly="true" HeaderText="Active" HeaderStyle-CssClass="detailheader" SortExpression="Day"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="50px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>

    </div>

</asp:Content>
