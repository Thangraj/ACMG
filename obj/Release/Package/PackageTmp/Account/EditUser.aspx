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
    <p>
        <asp:Label ID="LabelFname" runat="server" Text="First Name:"></asp:Label>
        <asp:TextBox ID="TextBoxFname" runat="server" Height="22px" 
            style="margin-left: 16px; margin-right: 4px" Width="204px"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="LabelLname" runat="server" Text="Last Name:"></asp:Label>
        <asp:TextBox ID="TextBoxLname" runat="server" Height="22px" 
            style="margin-left: 19px; margin-right: 0px" Width="202px"></asp:TextBox>
    </p>
    
 <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                             CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                             ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                    </p>
                </fieldset>
                
   


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
<p class="submitButton">
                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel" onclick="CancelPushButton_Click"/>
                    <asp:Button ID="ChangePasswordPushButton" runat="server" 
                        CommandName="ChangePassword" Text="Change Password" 
                         ValidationGroup="ChangeUserPasswordValidationGroup" 
                        onclick="ChangePasswordPushButton_Click" 
                        style="margin-left: 17px; margin-right: 27px"/>                   

<asp:Button ID="Button3" runat="server" Text="Update Roles and Name For This User" 
        OnClick="EditUserRoles" 
        OnClientClick="return confirm('Click OK to Update  This User.')" />
&nbsp;&nbsp;&nbsp;
<asp:Button ID="Button1" runat="server" Text="Unlock User" OnClick="UnlockUser" OnClientClick="return confirm('Click OK to unlock this user.')" />
&nbsp;&nbsp;&nbsp;
<asp:Button ID="Button2" runat="server" Text="Delete User" OnClick="DeleteUser" OnClientClick="return confirm('You Do Not Have Permission to Delete This User!')" />
</div>


<%--<asp:CommandField ButtonType="button" ShowEditButton="true" EditText="Edit User Info" />--%>
</td>

</tr></table>
</asp:Content>
