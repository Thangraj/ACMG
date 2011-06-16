<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="Report_AgentSummaryRT.aspx.cs" Inherits="ACMGAdmin.Reports.Report_AgentSummaryRT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<meta http-equiv="refresh" content="20" > 



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
    AutoGenerateColumns="False" CellPadding="4" 
    DataKeyNames="LoginDate,Agent,CallCenterID" DataSourceID="SqlDataSource1" 
    ForeColor="#333333" GridLines="None">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="ProcessTime" HeaderText="ProcessTime" 
            SortExpression="ProcessTime" />
        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
        <asp:BoundField DataField="WeekNumber" HeaderText="WeekNumber" 
            SortExpression="WeekNumber" />
        <asp:BoundField DataField="WeekStartDate" HeaderText="WeekStartDate" 
            SortExpression="WeekStartDate" />
        <asp:BoundField DataField="WeekEndDate" HeaderText="WeekEndDate" 
            SortExpression="WeekEndDate" />
        <asp:BoundField DataField="LoginDate" HeaderText="LoginDate" ReadOnly="True" 
            SortExpression="LoginDate" />
        <asp:BoundField DataField="AgentLoginHours" HeaderText="AgentLoginHours" 
            SortExpression="AgentLoginHours" />
        <asp:BoundField DataField="FirstLoginTime" HeaderText="FirstLoginTime" 
            SortExpression="FirstLoginTime" />
        <asp:BoundField DataField="LastLogoutTime" HeaderText="LastLogoutTime" 
            SortExpression="LastLogoutTime" />
        <asp:CheckBoxField DataField="IsLoggedIn" HeaderText="IsLoggedIn" 
            SortExpression="IsLoggedIn" />
        <asp:CheckBoxField DataField="HasLead" HeaderText="HasLead" 
            SortExpression="HasLead" />
        <asp:BoundField DataField="Agent" HeaderText="Agent" ReadOnly="True" 
            SortExpression="Agent" />
        <asp:BoundField DataField="AgentFirstName" HeaderText="AgentFirstName" 
            SortExpression="AgentFirstName" />
        <asp:BoundField DataField="AgentLastName" HeaderText="AgentLastName" 
            SortExpression="AgentLastName" />
        <asp:BoundField DataField="CallCenterID" HeaderText="CallCenterID" 
            ReadOnly="True" SortExpression="CallCenterID" />
        <asp:BoundField DataField="CallCenterName" HeaderText="CallCenterName" 
            SortExpression="CallCenterName" />
        <asp:BoundField DataField="TotalCalls" HeaderText="TotalCalls" 
            SortExpression="TotalCalls" />
        <asp:BoundField DataField="TotalRingTimeHours" HeaderText="TotalRingTimeHours" 
            SortExpression="TotalRingTimeHours" />
        <asp:BoundField DataField="TotalTalkTimeHours" HeaderText="TotalTalkTimeHours" 
            SortExpression="TotalTalkTimeHours" />
        <asp:BoundField DataField="TotalPhoneTimeHours" 
            HeaderText="TotalPhoneTimeHours" SortExpression="TotalPhoneTimeHours" />
        <asp:BoundField DataField="TotalRingTimeSec" HeaderText="TotalRingTimeSec" 
            SortExpression="TotalRingTimeSec" />
        <asp:BoundField DataField="TotalTalkTimeSec" HeaderText="TotalTalkTimeSec" 
            SortExpression="TotalTalkTimeSec" />
        <asp:BoundField DataField="TotalPhoneTimeSec" HeaderText="TotalPhoneTimeSec" 
            SortExpression="TotalPhoneTimeSec" />
        <asp:BoundField DataField="AgentLeadHours" HeaderText="AgentLeadHours" 
            SortExpression="AgentLeadHours" />
        <asp:BoundField DataField="LeadsCalled" HeaderText="LeadsCalled" 
            SortExpression="LeadsCalled" />
        <asp:BoundField DataField="UniqueLeadsCalled" HeaderText="UniqueLeadsCalled" 
            SortExpression="UniqueLeadsCalled" />
        <asp:BoundField DataField="Contacts" HeaderText="Contacts" 
            SortExpression="Contacts" />
        <asp:BoundField DataField="SaleDispositions" HeaderText="SaleDispositions" 
            SortExpression="SaleDispositions" />
        <asp:BoundField DataField="LeadKills" HeaderText="LeadKills" 
            SortExpression="LeadKills" />
        <asp:BoundField DataField="UniqueANIDialed" HeaderText="UniqueANIDialed" 
            SortExpression="UniqueANIDialed" />
        <asp:BoundField DataField="GrossOrders" HeaderText="GrossOrders" 
            SortExpression="GrossOrders" />
        <asp:BoundField DataField="ProcessAttempts" HeaderText="ProcessAttempts" 
            SortExpression="ProcessAttempts" />
        <asp:BoundField DataField="ProcessedOrders" HeaderText="ProcessedOrders" 
            SortExpression="ProcessedOrders" />
        <asp:BoundField DataField="CoreSalesProcessed" HeaderText="CoreSalesProcessed" 
            SortExpression="CoreSalesProcessed" />
        <asp:BoundField DataField="CrossSellsProcessed" 
            HeaderText="CrossSellsProcessed" SortExpression="CrossSellsProcessed" />
        <asp:BoundField DataField="CrossSell2sProcessed" 
            HeaderText="CrossSell2sProcessed" SortExpression="CrossSell2sProcessed" />
        <asp:BoundField DataField="CrossSell3sProcessed" 
            HeaderText="CrossSell3sProcessed" SortExpression="CrossSell3sProcessed" />
        <asp:BoundField DataField="DownsellsProcessed" HeaderText="DownsellsProcessed" 
            SortExpression="DownsellsProcessed" />
        <asp:BoundField DataField="Downsell2sProcessed" 
            HeaderText="Downsell2sProcessed" SortExpression="Downsell2sProcessed" />
        <asp:BoundField DataField="Downsell3sProcessed" 
            HeaderText="Downsell3sProcessed" SortExpression="Downsell3sProcessed" />
        <asp:BoundField DataField="ClubsProcessed" HeaderText="ClubsProcessed" 
            SortExpression="ClubsProcessed" />
        <asp:BoundField DataField="ProcessAmountAttemptedACMG" 
            HeaderText="ProcessAmountAttemptedACMG" 
            SortExpression="ProcessAmountAttemptedACMG" />
        <asp:BoundField DataField="ProcessedAmountACMG" 
            HeaderText="ProcessedAmountACMG" SortExpression="ProcessedAmountACMG" />
        <asp:BoundField DataField="ProcessedNowACMG" HeaderText="ProcessedNowACMG" 
            SortExpression="ProcessedNowACMG" />
        <asp:BoundField DataField="ProcessedDelayedACMG" 
            HeaderText="ProcessedDelayedACMG" SortExpression="ProcessedDelayedACMG" />
        <asp:BoundField DataField="RecurringAmountACMG" 
            HeaderText="RecurringAmountACMG" SortExpression="RecurringAmountACMG" />
        <asp:BoundField DataField="ProcessAmountAttemptedExternal" 
            HeaderText="ProcessAmountAttemptedExternal" 
            SortExpression="ProcessAmountAttemptedExternal" />
        <asp:BoundField DataField="ProcessedAmountExternal" 
            HeaderText="ProcessedAmountExternal" SortExpression="ProcessedAmountExternal" />
        <asp:BoundField DataField="ProcessedNowExternal" 
            HeaderText="ProcessedNowExternal" SortExpression="ProcessedNowExternal" />
        <asp:BoundField DataField="ProcessedDelayedExternal" 
            HeaderText="ProcessedDelayedExternal" 
            SortExpression="ProcessedDelayedExternal" />
        <asp:BoundField DataField="RecurringAmountExternal" 
            HeaderText="RecurringAmountExternal" SortExpression="RecurringAmountExternal" />
        <asp:BoundField DataField="HydroxatoneMDAProcessed" 
            HeaderText="HydroxatoneMDAProcessed" SortExpression="HydroxatoneMDAProcessed" />
        <asp:BoundField DataField="InstantEffectFullPayTrialProcessed" 
            HeaderText="InstantEffectFullPayTrialProcessed" 
            SortExpression="InstantEffectFullPayTrialProcessed" />
        <asp:BoundField DataField="CeltrixaMDAProcessed" 
            HeaderText="CeltrixaMDAProcessed" SortExpression="CeltrixaMDAProcessed" />
        <asp:BoundField DataField="HydrolyzeFullPayProcessed" 
            HeaderText="HydrolyzeFullPayProcessed" 
            SortExpression="HydrolyzeFullPayProcessed" />
        <asp:BoundField DataField="HydroxatoneFullPayProcessed" 
            HeaderText="HydroxatoneFullPayProcessed" 
            SortExpression="HydroxatoneFullPayProcessed" />
        <asp:BoundField DataField="HydroxatoneDEProcessed" 
            HeaderText="HydroxatoneDEProcessed" SortExpression="HydroxatoneDEProcessed" />
        <asp:BoundField DataField="InstantEffectFullPayProcessed" 
            HeaderText="InstantEffectFullPayProcessed" 
            SortExpression="InstantEffectFullPayProcessed" />
        <asp:BoundField DataField="FunSourceClubProcessed" 
            HeaderText="FunSourceClubProcessed" SortExpression="FunSourceClubProcessed" />
        <asp:BoundField DataField="ValuePlusClubProcessed" 
            HeaderText="ValuePlusClubProcessed" SortExpression="ValuePlusClubProcessed" />
        <asp:BoundField DataField="InstantEffectTrialProcessed" 
            HeaderText="InstantEffectTrialProcessed" 
            SortExpression="InstantEffectTrialProcessed" />
        <asp:BoundField DataField="CeltrixaTrialProcessed" 
            HeaderText="CeltrixaTrialProcessed" SortExpression="CeltrixaTrialProcessed" />
        <asp:BoundField DataField="HydroxatoneTrialProcessed" 
            HeaderText="HydroxatoneTrialProcessed" 
            SortExpression="HydroxatoneTrialProcessed" />
        <asp:BoundField DataField="CeltrixaFullPayProcessed" 
            HeaderText="CeltrixaFullPayProcessed" 
            SortExpression="CeltrixaFullPayProcessed" />
        <asp:BoundField DataField="HydrolyzeTrialProcessed" 
            HeaderText="HydrolyzeTrialProcessed" SortExpression="HydrolyzeTrialProcessed" />
        <asp:BoundField DataField="HydroxatoneMDAAttempted" 
            HeaderText="HydroxatoneMDAAttempted" SortExpression="HydroxatoneMDAAttempted" />
        <asp:BoundField DataField="InstantEffectFullPayTrialAttempted" 
            HeaderText="InstantEffectFullPayTrialAttempted" 
            SortExpression="InstantEffectFullPayTrialAttempted" />
        <asp:BoundField DataField="CeltrixaMDAAttempted" 
            HeaderText="CeltrixaMDAAttempted" SortExpression="CeltrixaMDAAttempted" />
        <asp:BoundField DataField="HydrolyzeFullPayAttempted" 
            HeaderText="HydrolyzeFullPayAttempted" 
            SortExpression="HydrolyzeFullPayAttempted" />
        <asp:BoundField DataField="HydroxatoneFullPayAttempted" 
            HeaderText="HydroxatoneFullPayAttempted" 
            SortExpression="HydroxatoneFullPayAttempted" />
        <asp:BoundField DataField="HydroxatoneDEAttempted" 
            HeaderText="HydroxatoneDEAttempted" SortExpression="HydroxatoneDEAttempted" />
        <asp:BoundField DataField="InstantEffectFullPayAttempted" 
            HeaderText="InstantEffectFullPayAttempted" 
            SortExpression="InstantEffectFullPayAttempted" />
        <asp:BoundField DataField="FunSourceClubAttempted" 
            HeaderText="FunSourceClubAttempted" SortExpression="FunSourceClubAttempted" />
        <asp:BoundField DataField="ValuePlusClubAttempted" 
            HeaderText="ValuePlusClubAttempted" SortExpression="ValuePlusClubAttempted" />
        <asp:BoundField DataField="InstantEffectTrialAttempted" 
            HeaderText="InstantEffectTrialAttempted" 
            SortExpression="InstantEffectTrialAttempted" />
        <asp:BoundField DataField="CeltrixaTrialAttempted" 
            HeaderText="CeltrixaTrialAttempted" SortExpression="CeltrixaTrialAttempted" />
        <asp:BoundField DataField="HydroxatoneTrialAttempted" 
            HeaderText="HydroxatoneTrialAttempted" 
            SortExpression="HydroxatoneTrialAttempted" />
        <asp:BoundField DataField="CeltrixaFullPayAttempted" 
            HeaderText="CeltrixaFullPayAttempted" 
            SortExpression="CeltrixaFullPayAttempted" />
        <asp:BoundField DataField="HydrolyzeTrialAttempted" 
            HeaderText="HydrolyzeTrialAttempted" SortExpression="HydrolyzeTrialAttempted" />
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
    ConnectionString="<%$ ConnectionStrings:ACMGREPORTS %>" 
    ProviderName="<%$ ConnectionStrings:ACMGREPORTS.ProviderName %>" 
    SelectCommand="sp_Report_AgentSummary_RT" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>
</asp:Content>
