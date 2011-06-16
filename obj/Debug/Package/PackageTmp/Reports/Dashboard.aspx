<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ACMGAdmin.Reports.Dashboard" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ACMGREPORTS %>" 
    ProviderName="<%$ ConnectionStrings:ACMGREPORTS.ProviderName %>" 
    SelectCommand="sp_Report_DashBoardSalesToday" 
        SelectCommandType="StoredProcedure">
</asp:SqlDataSource>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="944px">
        <LocalReport ReportPath="Reports\Report_Dashboard.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>
