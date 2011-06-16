<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="Edit_Site_Roles.aspx.cs" Inherits="ACMGAdmin.Account.Edit_Site_Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <table id="Table1" cellspacing="1" cellpadding="1" style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid; border-bottom: black thin solid">
            <tr>
                <td>
                    <b>Enter new role:</b>
                </td>
                <td >
                  <asp:textbox id="txtCreateRole" runat="server"  Width="170px" ></asp:textbox>
                </td>
            
                <td  style="TEXT-ALIGN: center;">
                    <asp:button id="btnCreateRole" runat="server" text="Create Role" 
                        onclick="btnCreateRole_Click1" Height="21px" Width="150px" />
                </td>
            </tr>
            <tr>
              <td>&nbsp;</td>
            </tr>
            <tr>
                <td  >
                    <b><asp:label id="lblRoleInfoText" runat="server" Visible=true /></b>
                </td>
            
                <td >
                    <asp:listbox id="lbxAvailableRoles" runat="server" Width="185px">
                    </asp:listbox>
                </td>
            
            
                <td colspan=2 style="TEXT-ALIGN: center;">
                    <asp:button id="btnDeleteRole" runat="server" text="Delete Selected Role" 
                        onclick="btnDeleteRole_Click1"  Width="150px" Height="21px" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:label id="lblResults" runat="server" Visible=false ForeColor=Red>Results:</asp:label>
        <br />
</asp:Content>
