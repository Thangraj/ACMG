<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="LeadCampaigns.aspx.cs" Inherits="ACMGAdmin.MasterInterfaces.LeadCampaigns" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">

function highlightModFields() {

        // checking whether the Page is valid, to implement the below given check in the form
        if (Page_IsValid) {

            // variable declarations
            var bChanged = false;

            var ProdCodeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtCampaignProdCode").ClientID%>');
            var ProductLineObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtProductLine").ClientID%>');
            var ChannelObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtChannel").ClientID%>');
            var TargusCodeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtTargusCode").ClientID%>');

            var hdnProdCodeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnCampaignProductCode").ClientID%>');
            var hdnProductLineObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnProductLine").ClientID%>');
            var hdnChannelObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnChannel").ClientID%>');
            var hdnTargusCodeObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnTargusCode").ClientID%>');
            
            // comparing & identifying whether the changes has been done in the screen
            if (ProdCodeObj.value != hdnProdCodeObj.value) {
                bChanged = true;
                ProdCodeObj.style.backgroundColor = '#FF3366';
            }
            else {
                ProdCodeObj.style.backgroundColor = '#FFFFFF';
            }

            if (ProductLineObj.value != hdnProductLineObj.value) {
                bChanged = true;
                ProductLineObj.style.backgroundColor = '#FF3366';
            }
            else {
                ProductLineObj.style.backgroundColor = '#FFFFFF';
            }

            if (ChannelObj.value != hdnChannelObj.value) {
                bChanged = true;
                ChannelObj.style.backgroundColor = '#FF3366';
            }
            else {
                ChannelObj.style.backgroundColor = '#FFFFFF';
            }

            if (TargusCodeObj.value != hdnTargusCodeObj.value) {
                bChanged = true;
                TargusCodeObj.style.backgroundColor = '#FF3366';
            }
            else {
                TargusCodeObj.style.backgroundColor = '#FFFFFF';
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
                    <asp:Label ID="lblHeader" CssClass="detailheader" runat="server" Text="Lead Campaign Details:"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvLeadCampaigns" runat="server" AutoGenerateColumns="False" Font-Size="9pt"
                        AllowPaging="True" OnPageIndexChanging="gvLeadCampaigns_PageIndexChanging" 
                        >
                        <AlternatingRowStyle BackColor="#E9ECF1" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#5970A6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Edit"  >
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdEditLeadCampaign" runat="server"  AutoPostBack="true"
                                         oncheckedchanged="rdEditLeadCampaign_CheckedChanged" Text='<%# Eval("LeadCampaignId") %>' TextAlign ="Right"  width="0px" Font-Size="0px" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="White" Font-Size="0px" Width="0px"/>
                                <ItemStyle CssClass="detailitem" Width="0px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CampaignProductCode" ReadOnly="true" HeaderText="Campaign ProductCode" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="150px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="150px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductLine" ReadOnly="true" HeaderText="Product Line" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="175px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="125px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Channel" ReadOnly="true" HeaderText="Channel" HeaderStyle-CssClass="detailheader"  
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="75px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TargusCode" ReadOnly="true" HeaderText="Targus Code"  
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader" Width="175px"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="175px" ></ItemStyle>
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
                <td style="background-color: #E9ECF1; width: 250px">
                    <asp:Label ID="lblCampaignProdCode" Text="Campaign Product Code" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCampaignProdCode" Width="175px" Font-Names="Arial" MaxLength="45" 
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvCampaignProdCode" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtCampaignProdCode" ErrorMessage="Please Enter Product Code !"></asp:RequiredFieldValidator>
                </td>
            </tr>
        
             <tr>
                <td style="background-color: #E9ECF1; width: 250px">
                    <asp:Label ID="lblProductLine" Text="Product Line" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtProductLine" Width="175px" Font-Names="Arial"  MaxLength="45" 
                        runat="server" CssClass="detailitem"  
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvProductLine" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtProductLine" ErrorMessage="Please Enter Product Line !"></asp:RequiredFieldValidator>
                </td>
            </tr>

             <tr>
                <td style="background-color: #E9ECF1; width: 250px">
                    <asp:Label ID="lblChannel" Text="Channel" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtChannel" Width="175px" Font-Names="Arial"  MaxLength="45"
                        runat="server" CssClass="detailitem" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvChannel" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtChannel" ErrorMessage="Please Enter Channel !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1; width: 250px">
                    <asp:Label ID="lblTargusCode" Text="Targus Code" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTargusCode" Width="175px" Font-Names="Arial"  MaxLength="45"
                        runat="server" CssClass="detailitem" 
                       ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvTargusCode" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtTargusCode" ErrorMessage="Please Enter Targus Code !"></asp:RequiredFieldValidator>
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
                    <asp:HiddenField ID="hdnLeadCampaignId" runat="server" />
                    <asp:HiddenField ID="hdnCampaignProductCode" runat="server" />
                    <asp:HiddenField ID="hdnProductLine" runat="server" />
                    <asp:HiddenField ID="hdnChannel" runat="server" />
                    <asp:HiddenField ID="hdnTargusCode" runat="server" />
                    <asp:HiddenField ID="hdnFlagType" runat="server" />
                </td>
            </tr>
        </table>

</div>


</asp:Content>
