<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="CallRecordingsSales.aspx.cs" Inherits="ACMGAdmin.Compliance.CallRecordingsSales" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style2
        {
            font-weight: bold;
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>Enter Start and End Dates OR Matmin OrderID to View Orders and Pull Recordings:<br /><hr />
    
        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"></asp:Label>
        <asp:TextBox ID="txtStartDate" runat="server" 
            style="margin-left: 11px; margin-right: 20px;"></asp:TextBox>
        
        
        <asp:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtStartDate">
        </asp:CalendarExtender>
        
        
        <asp:Label ID="lblEndDate" runat="server" Text="End Date:"></asp:Label>
        <asp:TextBox ID="txtEndDate" runat="server" 
            style="margin-left: 16px; margin-right: 20px;"></asp:TextBox>
        
        <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtEndDate">
        </asp:CalendarExtender>
       
        <asp:Label ID="lblOrderID" runat="server" Text="Matmin OrderID:"></asp:Label>
        <asp:TextBox ID="txtMatminOrderId" runat="server" 
            style="margin-left: 16px; margin-right: 20px;"></asp:TextBox>
        
        <asp:Button ID="RunReport" runat="server" Text="Find Orders" 
            onclick="RunReport_Click" Width="130px" />
        <br /><br />  
     
     <hr />
     <br /><hr /><br />
        <span class="style2">Processed Orders:</span><br /><hr />
        <br />
    <asp:GridView ID="GridView1" runat="server" 
         DataKeyNames="SubmittedOrderID" DataSourceID="CallRecordingList" 
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
            Font-Size="Small" ShowFooter="True" 
            ShowHeaderWhenEmpty="True" Width="100%" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" CellPadding="4" 
            ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button" />
            <asp:BoundField DataField="Agent" HeaderText="Agent" SortExpression="Agent" />
            <asp:BoundField DataField="AgentfirstName" HeaderText="First" 
                SortExpression="AgentfirstName" />
            <asp:BoundField DataField="AgentLastName" HeaderText="Last" 
                SortExpression="AgentLastName" />
            
            <asp:BoundField DataField="OrderID" HeaderText="OrderID" InsertVisible="False" 
                ReadOnly="True" SortExpression="OrderID" Visible="False" />
            <asp:BoundField DataField="PreviewCallID" HeaderText="PreviewCallID" 
                SortExpression="PreviewCallID" Visible="False" />
            <asp:BoundField DataField="SubmittedOrderID" HeaderText="OrderID" 
                SortExpression="SubmittedOrderID" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:BoundField DataField="FirstNAme" HeaderText="Customer First" 
                SortExpression="FirstNAme" />
            <asp:BoundField DataField="LastName" HeaderText="Customer Last" 
                SortExpression="LastName" />
            <asp:BoundField DataField="Address1" HeaderText="Address" 
                SortExpression="Address1" />
            <asp:BoundField DataField="Address2" HeaderText="Address 2" 
                SortExpression="Address2" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="Zip" HeaderText="Zip" SortExpression="Zip" />
            <asp:BoundField DataField="Total" DataFormatString="{0:c}" 
                HeaderText="Order Total" SortExpression="Total" />
            <asp:BoundField DataField="OrderStatus" HeaderText="Status" 
                SortExpression="OrderStatus" />
            <asp:BoundField DataField="OrderStatustext" HeaderText="Status Text" 
                SortExpression="OrderStatustext" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle HorizontalAlign="Left" BackColor="#5D7B9D" Font-Bold="True" 
            ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
     </asp:GridView>

     <br /><hr /><br />
        <span class="style2">Call Detail Records:</span><br /><hr />
        <br />

        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderID,CalldetailID" 
            DataSourceID="SqlDataSource1" Font-Size="Small" ShowHeaderWhenEmpty="True" 
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="OrderID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="OrderID" Visible="False" />
                <asp:BoundField DataField="PreviewCallID" HeaderText="PreviewCallID" 
                    SortExpression="PreviewCallID" Visible="False" />
                <asp:BoundField DataField="CalldetailID" HeaderText="Call Detail ID" 
                    InsertVisible="False" ReadOnly="True" SortExpression="CalldetailID" />
                <asp:BoundField DataField="SubmittedOrderID" HeaderText="Matmin OrderID" 
                    SortExpression="SubmittedOrderID" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" 
                    SortExpression="PhoneNumber" />
                <asp:BoundField DataField="Extension" HeaderText="Extension" 
                    SortExpression="Extension" />
                <asp:BoundField DataField="starttime" HeaderText="Start" 
                    SortExpression="starttime" />
                <asp:BoundField DataField="ConnectTime" HeaderText="Connect" 
                    SortExpression="ConnectTime" />
                <asp:BoundField DataField="DisconnectTime" HeaderText="Disconnect" 
                    SortExpression="DisconnectTime" />
                <asp:BoundField DataField="recordingFilename" HeaderText="Recording Filename" 
                    SortExpression="recordingFilename" Visible="False" />
                
               
                <asp:TemplateField HeaderText="Recording File Name" 
                    SortExpression="recordingFilename">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" 
                            Text='<%# Eval("recordingFilename") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" 
                            Text='<%# Eval("recordingFilename") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Recording Files">
                <ItemTemplate>
                        <asp:Literal ID="Literal1" runat="server"
                            Text='<%# getRecordingFiles(Eval("recordingFilename")) %>'></asp:Literal>
                    </ItemTemplate>
                
                
                
                </asp:TemplateField>

                <asp:TemplateField HeaderText=" Call Recordings2">
                <ItemTemplate>
                        <asp:Literal ID="Literal1" runat="server"
                            Text='<%# getRecordingFiles2(Eval("recordingFilename")) %>'></asp:Literal>
                    </ItemTemplate>
                
                
                
                </asp:TemplateField>
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
        <br /><hr /><br />
        

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ReportMySqlServer %>" 
            ProviderName="<%$ ConnectionStrings:ReportMySqlServer.ProviderName %>" 
            SelectCommand="Report_AllCallsForOrder" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="GridView1" DefaultValue="0" 
                    Name="theMatminOrderID" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
     <asp:SqlDataSource ID="CallRecordingList" runat="server" 
         ConnectionString="<%$ ConnectionStrings:ReportMySqlServer %>" 
         ProviderName="<%$ ConnectionStrings:ReportMySqlServer.ProviderName %>" 
         SelectCommand="Report_GoodOrders" SelectCommandType="StoredProcedure">
         <SelectParameters>
             <asp:ControlParameter ControlID="txtStartDate" Name="StartDate" 
                 PropertyName="Text" Type="DateTime" DefaultValue="1/15/2011" />
             <asp:ControlParameter ControlID="txtEndDate" Name="EndDate" PropertyName="Text" 
                 Type="DateTime" DefaultValue="1/24/2011" />
             <asp:ControlParameter ControlID="txtMatminOrderId" Name="MatminOrderID" 
                 PropertyName="Text" Type="String" DefaultValue="0" />
         </SelectParameters>
     </asp:SqlDataSource>
    </div>




</asp:Content>
