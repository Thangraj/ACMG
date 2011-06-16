<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="ConfigSalesOffers.aspx.cs" Inherits="ACMGAdmin.Marketing.ConfigSalesOffers" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblSalesOfferName" runat="server" 
        Text="Select Sales Offer Name:"></asp:Label>
    <asp:DropDownList ID="DropDownListProductOffers" runat="server" Height="19px" 
        Width="280px">
    </asp:DropDownList>
    <cc1:Editor ID="Editor1" runat="server" Height="700px" />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="TextBox1">
    </asp:CalendarExtender>
    <br />
</asp:Content>
