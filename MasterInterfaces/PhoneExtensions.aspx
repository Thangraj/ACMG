<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="PhoneExtensions.aspx.cs" Inherits="ACMGAdmin.MasterInterfaces.PhoneExtensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">

    function highlightModFields() {

        // checking whether the Page is valid, to implement the below given check in the form
        if (Page_IsValid) {

            // variable declarations
            var bChanged = false;

            var SwitchNameObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtSwitchName").ClientID%>');
            var CompanyObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtCompany").ClientID%>');
            var SwitchAddressObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtSwitchAddress").ClientID%>');
            var SwitchPortObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtSwitchPort").ClientID%>');
            var ExtensionObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtExtension").ClientID%>');
            var UserNameObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtUserName").ClientID%>');
            var PasswordObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtPassword").ClientID%>');

            var hdnSwitchNameObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnSwitchName").ClientID%>');
            var hdnCompanyObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnCompany").ClientID%>');
            var hdnSwitchAddressObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnSwitchAddress").ClientID%>');
            var hdnSwitchPortObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnSwitchPort").ClientID%>');
            var hdnExtensionObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnExtension").ClientID%>');
            var hdnUserNameObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnUserName").ClientID%>');
            var hdnPasswordObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnPassword").ClientID%>');

            // comparing & identifying whether the changes has been done in the screen
            if (SwitchNameObj.value != hdnSwitchNameObj.value) {
                bChanged = true;
                SwitchNameObj.style.backgroundColor = '#FF3366';
            }
            else {
                SwitchNameObj.style.backgroundColor = '#FFFFFF';
            }

            if (CompanyObj.value != hdnCompanyObj.value) {
                bChanged = true;
                CompanyObj.style.backgroundColor = '#FF3366';
            }
            else {
                CompanyObj.style.backgroundColor = '#FFFFFF';
            }

            if (SwitchAddressObj.value != hdnSwitchAddressObj.value) {
                bChanged = true;
                SwitchAddressObj.style.backgroundColor = '#FF3366';
            }
            else {
                SwitchAddressObj.style.backgroundColor = '#FFFFFF';
            }

            if (SwitchPortObj.value != hdnSwitchPortObj.value) {
                bChanged = true;
                SwitchPortObj.style.backgroundColor = '#FF3366';
            }
            else {
                SwitchPortObj.style.backgroundColor = '#FFFFFF';
            }

            if (ExtensionObj.value != hdnExtensionObj.value) {
                bChanged = true;
                ExtensionObj.style.backgroundColor = '#FF3366';
            }
            else {
                ExtensionObj.style.backgroundColor = '#FFFFFF';
            }

            if (UserNameObj.value != hdnUserNameObj.value) {
                bChanged = true;
                UserNameObj.style.backgroundColor = '#FF3366';
            }
            else {
                UserNameObj.style.backgroundColor = '#FFFFFF';
            }

            if (PasswordObj.value != hdnPasswordObj.value) {
                bChanged = true;
                PasswordObj.style.backgroundColor = '#FF3366';
            }
            else {
                PasswordObj.style.backgroundColor = '#FFFFFF';
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
                    <asp:Label ID="lblHeader" CssClass="detailheader" runat="server" Text="Phone Extension Details:"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvPhoneExtensions" runat="server" AutoGenerateColumns="False" Font-Size="9pt"
                        AllowPaging="True" OnPageIndexChanging="gvPhoneExtensions_PageIndexChanging" 
                        AllowSorting="true" >
                        <AlternatingRowStyle BackColor="#E9ECF1" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#5970A6" Font-Bold="True" ForeColor="White" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Edit"  >
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdEditPhoneExt" runat="server"  AutoPostBack="true"
                                         oncheckedchanged="rdEditPhoneExt_CheckedChanged" Text='<%# Eval("PhoneExtensionId") %>' TextAlign ="Right"  width="0px" Font-Size="0px" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="White" Font-Size="0px" Width="0px"/>
                                <ItemStyle CssClass="detailitem" Width="0px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SwitchName" ReadOnly="true" HeaderText="SwitchName" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Company" ReadOnly="true" HeaderText="Company" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="75px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SwitchAddress" ReadOnly="true" HeaderText="SwitchAddress" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="175px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="175px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SwitchPort" ReadOnly="true" HeaderText="SwitchPort"  
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="75px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Extension" ReadOnly="true" HeaderText="Extension" HeaderStyle-CssClass="detailheader"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="75px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="UserName" ReadOnly="true" HeaderText="UserName" HeaderStyle-CssClass="detailheader"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="75px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Password" ReadOnly="true" HeaderText="Password" HeaderStyle-CssClass="detailheader"
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
        <br />
        <table>
            <tr>
                <td style="background-color: #E9ECF1; width: 150px">
                    <asp:Label ID="lblSwitchName" Text="Switch Name" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSwitchName" Width="150px" Font-Names="Arial" MaxLength="45" 
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvSwitchName" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtSwitchName" ErrorMessage="Please Enter Switch Name !"></asp:RequiredFieldValidator>
                </td>
            </tr>
        
             <tr>
                <td style="background-color: #E9ECF1; width: 150px">
                    <asp:Label ID="lblCompany" Text="Company" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompany" Width="150px" Font-Names="Arial"  MaxLength="45" 
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvCompany" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtCompany" ErrorMessage="Please Enter Company !"></asp:RequiredFieldValidator>
                </td>
            </tr>

             <tr>
                <td style="background-color: #E9ECF1; width: 150px">
                    <asp:Label ID="lblSwitchAddress" Text="Switch Address" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSwitchAddress" Width="150px" Font-Names="Arial"  MaxLength="45"
                        runat="server" CssClass="detailitem" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvSwitchAddress" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtSwitchAddress" ErrorMessage="Please Enter Switch Address !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 150px">
                    <asp:Label ID="lblSwitchPort" Text="Switch Port" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSwitchPort" Width="150px" Font-Names="Arial"  MaxLength="45"
                        runat="server" CssClass="detailitem" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvSwitchPort" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtSwitchPort" ErrorMessage="Please Enter Switch Port !"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="rgvSwitchPort" Display="Dynamic" runat="server" ControlToValidate="txtSwitchPort" 
                            ValidationExpression="^\d*" ForeColor="Red" ErrorMessage="Please Enter Number Only!"></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 150px">
                    <asp:Label ID="lblExtension" Text="Extension" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExtension" Width="150px" Font-Names="Arial"  MaxLength="45"
                        runat="server" CssClass="detailitem" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvExtension" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtExtension" ErrorMessage="Please Enter Extension !"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgvExtension" Display="Dynamic" runat="server" ControlToValidate="txtExtension" 
                                ValidationExpression="^\d*" ForeColor="Red" ErrorMessage="Please Enter Number Only!"></asp:RegularExpressionValidator>

                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 150px">
                    <asp:Label ID="lblUserName" Text="User Name" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" Width="150px" Font-Names="Arial"  MaxLength="45"
                        runat="server" CssClass="detailitem" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvUserName" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please Enter UserName !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 150px">
                    <asp:Label ID="lblPassword" Text="Password" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" Width="150px" Font-Names="Arial"  MaxLength="45"
                        runat="server" CssClass="detailitem" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvPassword" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please Enter Password !"></asp:RequiredFieldValidator>
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
                    <asp:HiddenField ID="hdnPhoneExtensionId" runat="server" />
                    <asp:HiddenField ID="hdnFlagType" runat="server" />
                    <asp:HiddenField ID="hdnSwitchName" runat="server" />
                    <asp:HiddenField ID="hdnCompany" runat="server" />
                    <asp:HiddenField ID="hdnSwitchAddress" runat="server" />
                    <asp:HiddenField ID="hdnSwitchPort" runat="server" />
                    <asp:HiddenField ID="hdnExtension" runat="server" />
                    <asp:HiddenField ID="hdnUserName" runat="server" />
                    <asp:HiddenField ID="hdnPassword" runat="server" />
                </td>
            </tr>
        </table>
</div>




</asp:Content>
