<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="ViewUsers.aspx.cs" Inherits="ACMGAdmin.Account.ViewUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3><asp:Label ID="LabelUserList" runat="server" Text="Label"></asp:Label>
  </h3>



  <asp:Panel id="NavigationPanel" Visible="false" runat="server">
    <table border="0" cellpadding="3" cellspacing="3">
      <tr>
        <td style="width:100">Page <asp:Label id="CurrentPageLabel" runat="server" />
            of <asp:Label id="TotalPagesLabel" runat="server" /></td>
        <td style="width:60"><asp:LinkButton id="PreviousButton" Text="< Prev"
                            OnClick="PreviousButton_OnClick" runat="server" /></td>
        <td style="width:60"><asp:LinkButton id="NextButton" Text="Next >"
                            OnClick="NextButton_OnClick" runat="server" /></td>
      </tr>
    </table>
  </asp:Panel>

  

   <asp:GridView runat="server" ID="UserGrid" AutoGenerateColumns="False" 
      CellPadding="4" ForeColor="#333333" GridLines="None"
	
	>
       <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
<Columns>
	<asp:TemplateField>
		<HeaderTemplate>User Name</HeaderTemplate>
		<ItemTemplate>
		<a href="EditUser.aspx?username=<%# Eval("UserName") %>"><%# Eval("UserName") %></a>
		</ItemTemplate>
	</asp:TemplateField>
	

    <asp:BoundField DataField="email" HeaderText="Email" />
	<asp:BoundField DataField="comment" HeaderText="Comments" />
	<asp:BoundField DataField="creationdate" HeaderText="Creation Date" />
	<asp:BoundField DataField="isapproved" HeaderText="Is Active" />
	<asp:BoundField DataField="islockedout" HeaderText="Is Locked Out" />
    
   

    <%--<asp:TemplateField HeaderText="Full Name">
    
    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# getFullname(Eval("UserName")) %>'></asp:Label>
                    </ItemTemplate>
                    
                    
                    </asp:TemplateField>--%>
    
    
   

</Columns>
       <EditRowStyle BackColor="#999999" />
       <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
       <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
       <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
       <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
       <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
       <SortedAscendingCellStyle BackColor="#E9E7E2" />
       <SortedAscendingHeaderStyle BackColor="#506C8C" />
       <SortedDescendingCellStyle BackColor="#FFFDF8" />
       <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView>

</asp:Content>
