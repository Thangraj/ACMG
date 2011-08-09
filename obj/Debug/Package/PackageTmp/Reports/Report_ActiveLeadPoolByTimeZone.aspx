﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="Report_ActiveLeadPoolByTimeZone.aspx.cs" Inherits="ACMGAdmin.Reports.Report_ActiveLeadPoolByTimeZone" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
    
        <asp:Label ID="LabelReportTitle" runat="server" Font-Bold="True" 
            Font-Size="X-Large" Text="Active Lead Pool By Time Zone" 
            Font-Underline="True"></asp:Label>
        <br />
        <br />


    
        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" Visible="False"></asp:Label>
        <asp:TextBox ID="txtStartDate" runat="server" 
            style="margin-left: 11px; margin-right: 20px;" Height="20px" Width="97px" 
            Visible="False"></asp:TextBox>
        
        
        <asp:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtStartDate">
        </asp:CalendarExtender>
        
        
        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" Visible="False"></asp:Label>
        <asp:TextBox ID="txtEndDate" runat="server" 
            style="margin-left: 16px; margin-right: 20px;" Height="20px" Width="93px" 
            Visible="False"></asp:TextBox>
        
        <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtEndDate">
        </asp:CalendarExtender>
        

        <asp:Label ID="lblAgent" runat="server" Text="Agents:" Visible="False"></asp:Label>
        <asp:DropDownList ID="DropDownListAgent" runat="server" 
            style="margin-left: 11px; margin-right: 20px;" DataSourceID="SqlDataSource2" 
            DataTextField="AgentName" DataValueField="Agent" Height="21px" Width="188px" 
            ondatabound="AgentsBound" Visible="False">
        </asp:DropDownList>
        

          
        

          <asp:Label ID="lblCallCenter" runat="server" Text="Call Centers:" 
            Visible="False"></asp:Label>
        <asp:DropDownList ID="DropDownListCallCenter" runat="server" 
            style="margin-left: 11px; margin-right: 20px;" Height="21px" Width="169px" 
            DataSourceID="SqlDataSource3" DataTextField="CallCenterName" 
            DataValueField="CallCenterID" ondatabound="CallCenterBound" 
            Visible="False">
        </asp:DropDownList>


        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ReportMySqlServer %>" 
            ProviderName="<%$ ConnectionStrings:ReportMySqlServer.ProviderName %>" 
            SelectCommand="SELECT [CallCenterID], [CallCenterName] FROM [tbl_callcenter]">
        </asp:SqlDataSource>


        <asp:Button ID="RunReport" runat="server" Text="Refresh Report" 
            onclick="RunReport_Click" Width="130px" />
        <br /><br />  <hr />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="700px" 
            PageCountMode="Actual">
            <localreport reportpath="Reports\Report_ActiveLeadPoolByTimeZone.rdlc">
                <datasources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" 
                        Name="DataSet1" />
                </datasources>
            </localreport>
        </rsweb:ReportViewer>
      
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ACMGREPORTS %>" 
            ProviderName="<%$ ConnectionStrings:ACMGREPORTS.ProviderName %>" 
            SelectCommand="sp_Report_ActiveLeadPoolByTimeZone" 
            SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>


        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ReportMySqlServer %>" 
            ProviderName="<%$ ConnectionStrings:ReportMySqlServer.ProviderName %>" SelectCommand="SELECT concat(Agentfirstname, ' ' ,AgentlastName, ' (',agent,')') as AgentName,Agent FROM `acmgdialer`.`tbl_agentlogins`

group by agent order by Agentfirstname;
"></asp:SqlDataSource>
    
    </div>






























</asp:Content>
