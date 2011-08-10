<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="RoutingGroups.aspx.cs" Inherits="ACMGAdmin.MasterInterfaces.RoutingGroups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    // function which will highlight the field values whcih have been changed in Edit Mode and intimate the user with the
    // message box...
    function highlightModFields() {

        // checking whether the Page is valid, to implement the below given check in the form
        if (Page_IsValid) {

            // variable declarations
            var bChanged = false;

            var txtRoutingGroupObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtRoutingGroup").ClientID%>');
            var txtRoutingGroupDescObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtRoutingGroupDesc").ClientID%>');

            var hdnRoutingGroupObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnRoutingGroup").ClientID%>');
            var hdRoutingGroupDescObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnRoutingGroupDesc").ClientID%>');

            // comparing & identifying whether the changes has been donw in the screen
            if (txtRoutingGroupObj.value != hdnRoutingGroupObj.value) {
                bChanged = true;
                txtRoutingGroupObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtRoutingGroupObj.style.backgroundColor = '#FFFFFF';
            }

            if (txtRoutingGroupDescObj.value != hdRoutingGroupDescObj.value) {
                bChanged = true;
                txtRoutingGroupDescObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtRoutingGroupDescObj.style.backgroundColor = '#FFFFFF';
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
                    <asp:Label ID="lblRoutingGroup" Text="Routing Group" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRoutingGroup" Width="200px" Font-Names="Arial" MaxLength="255" TextMode="MultiLine"
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvRoutingGroup" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtRoutingGroup" ErrorMessage="Please Enter Routing Group !"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="rgvRoutingGroup" runat="server" ControlToValidate="txtRoutingGroup" Display="Dynamic"
                        ValidationExpression="(\s|.){0,255}$" ForeColor="Red" ErrorMessage="Max Length is 255!"></asp:RegularExpressionValidator>
                </td>
            </tr>
        
             <tr>
                <td style="background-color: #E9ECF1; width: 200px">
                    <asp:Label ID="lblRoutingGroupDesc" Text="Routing Group Description" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRoutingGroupDesc" Width="200px" Font-Names="Arial"  MaxLength="255" TextMode="MultiLine"
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvRoutingGroupDesc" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtRoutingGroupDesc" ErrorMessage="Please Enter Description !"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="rgvRoutingGroupDesc" runat="server" ControlToValidate="txtRoutingGroupDesc" Display="Dynamic"
                        ValidationExpression="(\s|.){0,255}$" ForeColor="Red" ErrorMessage="Max Length is 255!"></asp:RegularExpressionValidator>

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
                    <asp:HiddenField ID="hdnRoutingGroupId" runat="server" />
                    <asp:HiddenField ID="hdnFlagType" runat="server" />
                    <asp:HiddenField ID="hdnRoutingGroup" runat="server" />
                    <asp:HiddenField ID="hdnRoutingGroupDesc" runat="server" />
                </td>
            </tr>
        </table>

        <br />
       <table>
            <tr>
                <td>
                    <h3>
                    <asp:Label ID="lblHeader" CssClass="detailheader" runat="server" Text="Routing Group Details:"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvRoutingGroups" runat="server" AutoGenerateColumns="False" Font-Size="9pt"
                        AllowPaging="True" OnPageIndexChanging="gvRoutingGroups_PageIndexChanging" 
                        AllowSorting="true" PageSize="100" >
                        <AlternatingRowStyle BackColor="#E9ECF1" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#5970A6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Edit"  >
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdEditRoutingGroup" runat="server"  AutoPostBack="true"
                                         oncheckedchanged="rdEditRoutingGroup_CheckedChanged" Text='<%# Eval("RoutingGroupId") %>' TextAlign ="Right"  width="0px" Font-Size="0px" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="White" Font-Size="0px" Width="0px"/>
                                <ItemStyle CssClass="detailitem" Width="0px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="RoutingGroup" ReadOnly="true" HeaderText="Routing Group" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="200px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="200px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RoutingGroupDesc" ReadOnly="true" HeaderText="Routing Group Description" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="200px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="200px" ></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>

</div>

</asp:Content>
