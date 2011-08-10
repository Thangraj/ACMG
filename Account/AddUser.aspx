<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="ACMGAdmin.Account.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .webparts
        {
            width: 264px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div>
    <table class="webparts">
<tr>
	<th><h3><asp:Label ID="LabelUserList" runat="server" Text="Label"></asp:Label>
  </h3></th>
</tr>
<tr>
<td class="details" valign="top">

<h3>Roles:</h3>
<asp:CheckBoxList ID="UserRoles" runat="server" />

<h3>Main Info:</h3>

<table>
<tr>
	<td class="detailheader">Active User</td>
	<td>
		<asp:CheckBox ID="isapproved" runat="server" Checked="true" />
	</td>
</tr>
<tr>
	<td class="detailheader">User Name</td>
	<td>
		<asp:TextBox ID="username" runat="server"></asp:TextBox>
	</td>
</tr>
<tr>
	<td class="detailheader">Password</td>
	<td>
		<asp:TextBox ID="password" runat="server"></asp:TextBox>
	</td>
</tr>
<tr>
	<td class="detailheader">First Name</td>
	<td>
		<asp:TextBox ID="txtfname" runat="server"></asp:TextBox>
	</td>
</tr>
<tr>
	<td class="detailheader">Last Name</td>
	<td>
		<asp:TextBox ID="txtlname" runat="server"></asp:TextBox>
	</td>
</tr>
<tr>
	<td class="detailheader">Email</td>
	<td>
		<asp:TextBox ID="email" runat="server"></asp:TextBox>
	</td>
</tr>
<tr>
	<td class="detailheader">Comment</td>
	<td>
		<asp:TextBox ID="comment" runat="server"></asp:TextBox>
	</td>
</tr>
<tr>
	<td colspan="2"><br />
		<input type="submit" value="Add User" />&nbsp;
		<input type="reset" />
	</td>
</tr>
<tr>
	<td colspan="2">
	<div id="ConfirmationMessage" runat="server" class="alert"></div>
	</td>
</tr>
</table>

<%--<asp:ObjectDataSource ID="MemberData" runat="server" DataObjectTypeName="System.Web.Security.MembershipUser" SelectMethod="GetUser" UpdateMethod="UpdateUser" TypeName="System.Web.Security.Membership">
	<SelectParameters>
		<asp:QueryStringParameter Name="username" QueryStringField="username" />
	</SelectParameters>
</asp:ObjectDataSource> --%>
</td>

</tr></table>

    </div>
  </asp:Content>
