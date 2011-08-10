<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActiveExtensions.aspx.cs" Inherits="ACMGAdmin.Compliance.ActiveExtensions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="Extension" HeaderText="Extension" 
                        SortExpression="Extension" />
                    <asp:BoundField DataField="AgentFirstName" HeaderText="First" 
                        SortExpression="AgentFirstName" />
                    <asp:BoundField DataField="AgentLastName" HeaderText="Last" 
                        SortExpression="AgentLastName" />
                    <asp:BoundField DataField="Agent" HeaderText="Agent" SortExpression="Agent" />
                    <asp:BoundField DataField="StartDateTime" DataFormatString="{0:h:mm:ss tt}" 
                        HeaderText="Start Time" SortExpression="StartDateTime" />
                    <asp:BoundField DataField="LastCallDateTime" DataFormatString="{0:h:mm:ss tt}" 
                        HeaderText="Last Call " SortExpression="LastCallDateTime" />
                    <asp:BoundField DataField="TotalCalls" HeaderText="Total Calls" 
                        SortExpression="TotalCalls" />
                    <asp:BoundField DataField="CallCenterName" HeaderText="Call Center" 
                        SortExpression="CallCenterName" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
            <asp:Timer ID="Timer1" runat="server" Interval="5000">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="server=localhost;User Id=root;password=Tony8866478;Persist Security Info=True;database=acmgdialer" 
            ProviderName="MySql.Data.MySqlClient" 
            SelectCommand="sp_ReportAssignedExtensions" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
