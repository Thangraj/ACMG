<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="LiveCallMonitor.aspx.cs" Inherits="ACMGAdmin.Compliance.LiveCallMonitor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   
    
    
    
  
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2">
        <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
        <HeaderTemplate>
                Agents Logged In
        </HeaderTemplate>
        <ContentTemplate>
        <iframe src="ActiveExtensions.aspx" width="100%" height="600px">
  <p>Your browser does not support iframes.</p>
</iframe></ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
        <HeaderTemplate>
                Call Monitor Control
        </HeaderTemplate>
        <ContentTemplate>
            <asp:Literal ID="LiteralFOP" runat="server"></asp:Literal>
         </ContentTemplate>
        </asp:TabPanel>
        
    </asp:TabContainer>  


</asp:Content>
