<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="ACMGAdmin.Account.EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="webparts">
<tr>
	<th>User Information</th>
</tr>
<tr>
<td class="details" valign="top">

<h3>Roles:</h3>
<asp:CheckBoxList ID="UserRoles" runat="server" />

<h3><asp:Label ID="LabelFullName" runat="server"   Text=""></asp:Label></h3>

   


<asp:DetailsView AutoGenerateRows="False"
  ID="UserInfo" runat="server" OnItemUpdating="UserInfo_ItemUpdating" 
        Width="601px" CellPadding="4" ForeColor="#333333" GridLines="None"  
  >
  
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
    <EditRowStyle BackColor="#999999" />
    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
  
<Fields>

	<asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
<HeaderStyle CssClass="detailheader"></HeaderStyle>

<ItemStyle CssClass="detailitem"></ItemStyle>
	</asp:BoundField>
	<asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
<HeaderStyle CssClass="detailheader"></HeaderStyle>

<ItemStyle CssClass="detailitem"></ItemStyle>
    </asp:BoundField>
	<asp:BoundField DataField="Comment" HeaderText="Comment" HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
<HeaderStyle CssClass="detailheader"></HeaderStyle>

<ItemStyle CssClass="detailitem"></ItemStyle>
    </asp:BoundField>
	<asp:CheckBoxField DataField="IsApproved" HeaderText="Active User" 
        HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem" >
<HeaderStyle CssClass="detailheader"></HeaderStyle>

<ItemStyle CssClass="detailitem"></ItemStyle>
    </asp:CheckBoxField>
	<asp:CheckBoxField DataField="IsLockedOut" HeaderText="Is Locked Out" 
        ReadOnly="true" HeaderStyle-CssClass="detailheader" 
        ItemStyle-CssClass="detailitem" >
<HeaderStyle CssClass="detailheader"></HeaderStyle>

<ItemStyle CssClass="detailitem"></ItemStyle>
    </asp:CheckBoxField>
	<asp:BoundField DataField="CreationDate" HeaderText="CreationDate" ReadOnly="True"
	 HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
<HeaderStyle CssClass="detailheader"></HeaderStyle>

<ItemStyle CssClass="detailitem"></ItemStyle>
    </asp:BoundField>
	<asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" ReadOnly="True" HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
<HeaderStyle CssClass="detailheader"></HeaderStyle>

<ItemStyle CssClass="detailitem"></ItemStyle>
    </asp:BoundField>
	<asp:BoundField DataField="LastPasswordChangedDate" HeaderText="LastPasswordChangedDate"
	ReadOnly="True" HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
<HeaderStyle CssClass="detailheader"></HeaderStyle>

<ItemStyle CssClass="detailitem"></ItemStyle>
    </asp:BoundField>
	<%--<asp:CommandField ButtonType="button" ShowEditButton="true" EditText="Edit User Info" />--%>
</Fields>
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
</asp:DetailsView>
<div class="alert" style="padding: 5px;">
<asp:Literal ID="UserUpdateMessage" runat="server">&nbsp;</asp:Literal>
</div>


<div style="text-align: right; width: 100%; margin: 20px 0px;">
<asp:Button ID="Button3" runat="server" Text="Update Roles For This User" OnClick="EditUserRoles" OnClientClick="return confirm('Click OK to Update Roles For This User.')" />
&nbsp;&nbsp;&nbsp;
<asp:Button ID="Button1" runat="server" Text="Unlock User" OnClick="UnlockUser" OnClientClick="return confirm('Click OK to unlock this user.')" />
&nbsp;&nbsp;&nbsp;
<asp:Button ID="Button2" runat="server" Text="Delete User" OnClick="DeleteUser" OnClientClick="return confirm('You Do Not Have Permission to Delete This User!')" />
</div>


<%--<asp:ObjectDataSource ID="MemberData" runat="server" DataObjectTypeName="System.Web.Security.MembershipUser" SelectMethod="GetUser" UpdateMethod="UpdateUser" TypeName="System.Web.Security.Membership">
	<SelectParameters>
		<asp:QueryStringParameter Name="username" QueryStringField="username" />
	</SelectParameters>
</asp:ObjectDataSource> --%>
</td>

</tr></table>
</asp:Content>
