<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="CallRecordingsSearch.aspx.cs" Inherits="ACMGAdmin.Compliance.CallRecordingsSearch" %>
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

    <div>Enter Start and End Dates and Agent OR Matmin OrderID OR Phone Number to View 
        and Pull Recordings:<br /><hr />
    
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
        <br /><hr /> <br />
        <asp:Label ID="lblAgent" runat="server" Text="Agents:"></asp:Label>
        <asp:DropDownList ID="DropDownListAgent" runat="server" 
            style="margin-left: 11px; margin-right: 20px;" DataSourceID="SqlDataSource2" 
            DataTextField="AgentName" DataValueField="Agent" Height="21px" Width="188px" 
            ondatabound="AgentsBound">
        </asp:DropDownList>
       
        <asp:Label ID="lblOrderID" runat="server" Text="Matmin OrderID:"></asp:Label>
        <asp:TextBox ID="txtMatminOrderId" runat="server" 
            style="margin-left: 16px; margin-right: 20px;"></asp:TextBox>
        
         <asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
        <asp:TextBox ID="txtPhone" runat="server" 
            style="margin-left: 16px; margin-right: 20px;"></asp:TextBox>


        <asp:Button ID="RunReport" runat="server" Text="Find Orders" 
            onclick="RunReport_Click" Width="130px" />
        <br /><br />  
     
     <hr />
     <br /><hr /><br />
        <span class="style2">Call Records:</span><br /><hr />
        <br />
    <asp:GridView ID="GridView1" runat="server" 
         DataKeyNames="CallDetailID" DataSourceID="CallRecordingList" 
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
            Font-Size="Small" ShowFooter="True" 
            ShowHeaderWhenEmpty="True" Width="100%" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" CellPadding="4" 
            ForeColor="#333333" GridLines="None" PageSize="25">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
        <asp:CommandField ShowSelectButton="True" ButtonType="Button" />
            <asp:BoundField DataField="CallDetailID" HeaderText="CallDetailID" 
                SortExpression="CallDetailID" InsertVisible="False" ReadOnly="True" 
                Visible="False" />
            <asp:BoundField DataField="AgentLoginID" HeaderText="AgentLoginID" 
                SortExpression="AgentLoginID" Visible="False" />
            <asp:BoundField DataField="PhoneExtensionID" HeaderText="PhoneExtensionID" 
                SortExpression="PhoneExtensionID" Visible="False" />
            
            <asp:BoundField DataField="PhoneExtensionAssignmentID" 
                HeaderText="PhoneExtensionAssignmentID" 
                SortExpression="PhoneExtensionAssignmentID" Visible="False" />
            <asp:BoundField DataField="PreviewCallID" HeaderText="PreviewCallID" 
                SortExpression="PreviewCallID" Visible="False" />
            <asp:BoundField DataField="Agent" HeaderText="Agent" 
                SortExpression="Agent" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" 
                SortExpression="PhoneNumber" />
            <asp:BoundField DataField="CallType" HeaderText="CallType" 
                SortExpression="CallType" Visible="False" />
            <asp:BoundField DataField="CallSeconds" HeaderText="Seconds" 
                SortExpression="CallSeconds" />
            <asp:BoundField DataField="StartTime" HeaderText="Start Time" 
                SortExpression="StartTime" />
            <asp:BoundField DataField="ConnectTime" HeaderText="Connect Time" 
                SortExpression="ConnectTime" Visible="False" />
            <asp:BoundField DataField="DisconnectTime" HeaderText="Disconnect Time" 
                SortExpression="DisconnectTime" Visible="False" />
            <asp:BoundField DataField="CallRecordingID" HeaderText="Recording ID" 
                SortExpression="CallRecordingID" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" 
                HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField DataField="Address1" HeaderText="Address1" 
                SortExpression="Address1" Visible="False" />
            <asp:BoundField DataField="Address2" HeaderText="Address2" 
                SortExpression="Address2" Visible="False" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="Country" HeaderText="Country" 
                SortExpression="Country" Visible="False" />
            <asp:BoundField DataField="Zip" HeaderText="Zip" SortExpression="Zip" 
                Visible="False" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" 
                Visible="False" />
            <asp:BoundField DataField="Last4CC" HeaderText="Last4CC" 
                SortExpression="Last4CC" />
            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />
            <asp:BoundField DataField="MatminOrderID" HeaderText="Matmin OrderID" 
                SortExpression="MatminOrderID" />
            <asp:BoundField DataField="OrderStatus" HeaderText="Order Status" 
                SortExpression="OrderStatus" />
            <asp:BoundField DataField="OrderStatusText" HeaderText="Order Text" 
                SortExpression="OrderStatusText" />
            <asp:CheckBoxField DataField="ProcessedSale" HeaderText="Good Sale" 
                SortExpression="ProcessedSale" />
            <asp:BoundField DataField="SubmitDateTime" HeaderText="Order Time" 
                SortExpression="SubmitDateTime" />
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

        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="CalldetailID" 
            DataSourceID="SqlDataSource1" Font-Size="Small" ShowHeaderWhenEmpty="True" 
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="PreviewCallID" HeaderText="PreviewCallID" 
                    SortExpression="PreviewCallID" Visible="False" />
                <asp:BoundField DataField="Extension" HeaderText="Extension" 
                    SortExpression="Extension" />
                <asp:BoundField DataField="CalldetailID" HeaderText="Call Detail ID" 
                    SortExpression="CalldetailID" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone " 
                    SortExpression="PhoneNumber" />
                <asp:BoundField DataField="starttime" HeaderText="Start Time" 
                    SortExpression="starttime" />
                <asp:BoundField DataField="ConnectTime" HeaderText="Connect Time" 
                    SortExpression="ConnectTime" />
                <asp:BoundField DataField="DisconnectTime" HeaderText="Disconnect Time" 
                    SortExpression="DisconnectTime" />
                <asp:BoundField DataField="recordingFilename" HeaderText="File Name" 
                    SortExpression="recordingFilename" />

                <asp:TemplateField HeaderText=" Call Recordings">
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ReportMySqlServer %>" 
            ProviderName="<%$ ConnectionStrings:ReportMySqlServer.ProviderName %>" 
            SelectCommand="Report_GetSelectedCallRecord" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="GridView1" DefaultValue="0" 
                    Name="theCallDetailID" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br /><hr /><br />
        

     <asp:SqlDataSource ID="CallRecordingList" runat="server" 
         ConnectionString="<%$ ConnectionStrings:ReportMySqlServer %>" 
         ProviderName="<%$ ConnectionStrings:ReportMySqlServer.ProviderName %>" 
         SelectCommand="Report_SearchCallRecordings" 
            SelectCommandType="StoredProcedure">
         <SelectParameters>
             <asp:ControlParameter ControlID="txtStartDate" Name="StartDate" 
                 PropertyName="Text" Type="DateTime" DefaultValue="1/15/2011" />
             <asp:ControlParameter ControlID="txtEndDate" Name="EndDate" PropertyName="Text" 
                 Type="DateTime" DefaultValue="1/24/2011" />
             <asp:ControlParameter ControlID="txtMatminOrderId" Name="MatminOrderID" 
                 PropertyName="Text" Type="String" DefaultValue="0" />
             <asp:ControlParameter ControlID="DropDownListAgent" DefaultValue="0" 
                 Name="theAgent" PropertyName="SelectedValue" Type="String" />
             <asp:ControlParameter ControlID="txtPhone" DefaultValue="0" Name="thePhone" 
                 PropertyName="Text" Type="String" />
         </SelectParameters>
     </asp:SqlDataSource>
    </div>

<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ReportMySqlServer %>" 
            ProviderName="<%$ ConnectionStrings:ReportMySqlServer.ProviderName %>" SelectCommand="SELECT concat(Agentfirstname, ' ' ,AgentlastName, ' (',agent,')') as AgentName,Agent FROM `acmgdialer`.`tbl_agentlogins`

group by agent order by Agentfirstname;
"></asp:SqlDataSource>


</asp:Content>
