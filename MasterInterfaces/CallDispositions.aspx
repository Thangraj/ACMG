<%@ Page Title="" Language="C#" MasterPageFile="~/ACMG.Master" AutoEventWireup="true" CodeBehind="CallDispositions.aspx.cs" Inherits="ACMGAdmin.MasterInterfaces.CallDispositions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">

    function highlightModFields() {

        // checking whether the Page is valid, to implement the below given check in the form
        if (Page_IsValid) {
            // variable declarations
            var bChanged = false;
            
            var txtCallDispObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("txtCallDisposition").ClientID%>');
            var rblDoNotCallObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblDoNotCall").ClientID%>');
            var rblSaleObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblSale").ClientID%>');
            var rblContactObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblContact").ClientID%>');
            var rblBadLeadObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblBadLead").ClientID%>');
            var rblCallBackObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblCallBack").ClientID%>');
            var rblNotInterestedObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblNotInterested").ClientID%>');
            var rblSystemDefaultsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblSystemDefaults").ClientID%>');
            var rblCustomObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblCustom").ClientID%>');
            var rblNotCalledObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblNotCalled").ClientID%>');
            var rblDialedObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblDialed").ClientID%>');
            var rblNotDialedObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblNotDialed").ClientID%>');
            var rblProcessedGoodObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblProcessedGood").ClientID%>');
            var rblProcessedBadObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblProcessedBad").ClientID%>');
            var rblNotProcessedObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("rblNotProcessed").ClientID%>');

            var hdnCallDispositionObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnCallDisposition").ClientID%>');
            var hdnDoNotCallObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnDoNotCall").ClientID%>');
            var hdnSaleObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnSale").ClientID%>');
            var hdnContactObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnContact").ClientID%>');
            var hdnBadLeadObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnBadLead").ClientID%>');
            var hdnCallBackObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnCallBack").ClientID%>');
            var hdnNotInterestedObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnNotInterested").ClientID%>');
            var hdnSystemDefaultsObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnSystemDefaults").ClientID%>');
            var hdnCustomObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnCustom").ClientID%>');
            var hdnNotCalledObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnNotCalled").ClientID%>');
            var hdnDialedObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnDialed").ClientID%>');
            var hdnNotDialedObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnNotDialed").ClientID%>');
            var hdnProcessedGoodObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnProcessedGood").ClientID%>');
            var hdnProcessedBadObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnProcessedBad").ClientID%>');
            var hdnNotProcessedObj = document.getElementById('<%= Page.Master.FindControl("MainContent").FindControl("hdnNotProcessed").ClientID%>');
            

            // comparing & identifying whether the changes has been donw in the screen
            if (txtCallDispObj.value != hdnCallDispositionObj.value) {
                bChanged = true;
                txtCallDispObj.style.backgroundColor = '#FF3366';
            }
            else {
                txtCallDispObj.style.backgroundColor = '#FFFFFF';
            }

            
            // checking the Radiobutton selected value & verfying the modification and changing the color of the control..
            for (var i = 0; i < '<%= rblDoNotCall.Items.Count %>'; i++) {

                if (document.getElementById(rblDoNotCallObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblDoNotCallObj.id + "_" + [i].toString()).value != hdnDoNotCallObj.value) {
                        bChanged = true;
                        rblDoNotCallObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblDoNotCallObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }


            for (var i = 0; i < '<%= rblSale.Items.Count %>'; i++) {

                if (document.getElementById(rblSaleObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblSaleObj.id + "_" + [i].toString()).value != hdnSaleObj.value) {
                        bChanged = true;
                        rblSaleObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblSaleObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblContact.Items.Count %>'; i++) {

                if (document.getElementById(rblContactObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblContactObj.id + "_" + [i].toString()).value != hdnContactObj.value) {
                        bChanged = true;
                        rblContactObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblContactObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblBadLead.Items.Count %>'; i++) {

                if (document.getElementById(rblBadLeadObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblBadLeadObj.id + "_" + [i].toString()).value != hdnBadLeadObj.value) {
                        bChanged = true;
                        rblBadLeadObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblBadLeadObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblCallBack.Items.Count %>'; i++) {

                if (document.getElementById(rblCallBackObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblCallBackObj.id + "_" + [i].toString()).value != hdnCallBackObj.value) {
                        bChanged = true;
                        rblCallBackObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblCallBackObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblNotInterested.Items.Count %>'; i++) {

                if (document.getElementById(rblNotInterestedObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblNotInterestedObj.id + "_" + [i].toString()).value != hdnNotInterestedObj.value) {
                        bChanged = true;
                        rblNotInterestedObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblNotInterestedObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblSystemDefaults.Items.Count %>'; i++) {

                if (document.getElementById(rblSystemDefaultsObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblSystemDefaultsObj.id + "_" + [i].toString()).value != hdnSystemDefaultsObj.value) {
                        bChanged = true;
                        rblSystemDefaultsObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblSystemDefaultsObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblCustom.Items.Count %>'; i++) {

                if (document.getElementById(rblCustomObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblCustomObj.id + "_" + [i].toString()).value != hdnCustomObj.value) {
                        bChanged = true;
                        rblCustomObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblCustomObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblNotCalled.Items.Count %>'; i++) {

                if (document.getElementById(rblNotCalledObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblNotCalledObj.id + "_" + [i].toString()).value != hdnNotCalledObj.value) {
                        bChanged = true;
                        rblNotCalledObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblNotCalledObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblDialed.Items.Count %>'; i++) {

                if (document.getElementById(rblDialedObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblDialedObj.id + "_" + [i].toString()).value != hdnDialedObj.value) {
                        bChanged = true;
                        rblDialedObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblDialedObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblNotDialed.Items.Count %>'; i++) {

                if (document.getElementById(rblNotDialedObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblNotDialedObj.id + "_" + [i].toString()).value != hdnNotDialedObj.value) {
                        bChanged = true;
                        rblNotDialedObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblNotDialedObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblProcessedGood.Items.Count %>'; i++) {

                if (document.getElementById(rblProcessedGoodObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblProcessedGoodObj.id + "_" + [i].toString()).value != hdnProcessedGoodObj.value) {
                        bChanged = true;
                        rblProcessedGoodObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblProcessedGoodObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblProcessedBad.Items.Count %>'; i++) {

                if (document.getElementById(rblProcessedBadObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblProcessedBadObj.id + "_" + [i].toString()).value != hdnProcessedBadObj.value) {
                        bChanged = true;
                        rblProcessedBadObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblProcessedBadObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }

            for (var i = 0; i < '<%= rblNotProcessed.Items.Count %>'; i++) {

                if (document.getElementById(rblNotProcessedObj.id + "_" + [i].toString()).checked == true) {

                    if (document.getElementById(rblNotProcessedObj.id + "_" + [i].toString()).value != hdnNotProcessedObj.value) {
                        bChanged = true;
                        rblNotProcessedObj.style.backgroundColor = '#FF3366';
                    }
                    else {
                        rblNotProcessedObj.style.backgroundColor = '#FFFFFF';
                    }
                }
            }
            


            // checking whether any one changes has been applied, then to pop-up the alert msg...
            if (bChanged) {
                var out = confirm("Highlighted Fields have been modified, Do you want to save?");
                if (out) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                var outRes = confirm("No changes have been made, Do you want to save?");
                if (outRes) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }
    
    </script>


<div>

     <table>
            <tr>
                <td>
                    <h3>
                    <asp:Label ID="lblHeader" CssClass="detailheader" runat="server" Text="CallDisposition Details:"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvCallDisposition" runat="server" AutoGenerateColumns="false" Font-Size="9" 
                     AllowPaging="true" PageSize="10" 
                        onpageindexchanging="gvCallDisposition_PageIndexChanging" >
                        <AlternatingRowStyle BackColor="#E9ECF1" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#5970A6" Font-Bold="True" Height="20px" ForeColor="White" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdEditCallDisp" runat="server" AutoPostBack="true" Text='<%# Eval("CallDispositionID") %>'
                                        OnCheckedChanged="rdEditCallDisp_CheckedChanged" width="0px" Font-Size="0px"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CallDisposition" ReadOnly="true" HeaderText="Call Disposition" 
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="200px" ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DoNotCall" ReadOnly="true" HeaderText="DoNotCall" HeaderStyle-CssClass="detailheader"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Sale" ReadOnly="true" HeaderText="Sale"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Contact" ReadOnly="true" HeaderText="Contact"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="BadLead" ReadOnly="true" HeaderText="BadLead"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CallBack" ReadOnly="true" HeaderText="CallBack"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NotInterested" ReadOnly="true" HeaderText="Not Interested"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SystemDefaults" ReadOnly="true" HeaderText="SystemDefaults"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Custom" ReadOnly="true" HeaderText="Custom"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="50px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NotCalled" ReadOnly="true" HeaderText="Not Called"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Dialed" ReadOnly="true" HeaderText="Dialed" HeaderStyle-CssClass="detailheader"
                                ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="35px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NotDialed" ReadOnly="true" HeaderText="Not Dialed"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="75px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ProcessedGood" ReadOnly="true" HeaderText="Processed Good"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ProcessedBad" ReadOnly="true" HeaderText="Processed Bad"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NotProcessed" ReadOnly="true" HeaderText="Not Processed"
                                HeaderStyle-CssClass="detailheader" ItemStyle-CssClass="detailitem">
                                <HeaderStyle CssClass="detailheader"></HeaderStyle>
                                <ItemStyle CssClass="detailitem" Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <br />

        <table>
            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblCallDisposition" Text="Call Disposition" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCallDisposition" runat="server" Width="150px" MaxLength="45"
                        CssClass="detailitem" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvCallDisposition" ForeColor="Red"  runat="server" Display="Dynamic" ControlToValidate="txtCallDisposition" ErrorMessage="Please Enter Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblDoNotCall" Text="Do Not Call" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblDoNotCall" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvDoNotCall" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblDoNotCall" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblSale" Text="Sale" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblSale" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvSale" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblSale" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblContact" Text="Contact" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblContact" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvContact" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblContact" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblBadLead" Text="Bad Lead" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblBadLead" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvBadLead" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblBadLead" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblCallBack" Text="Call Back" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblCallBack" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvCallBack" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblCallBack" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblNotInterested" Text="Not Interested" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblNotInterested" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvNotInterested" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblNotInterested" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblSystemDefaults" Text="System Defaults" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblSystemDefaults" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvSystemDefaults" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblSystemDefaults" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblCustom" Text="Custom" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblCustom" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvCustom" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblCustom" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblNotCalled" Text="NotCalled" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblNotCalled" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvNotCalled" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblNotCalled" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblDialed" Text="Dialed" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblDialed" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvDialed" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblDialed" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblNotDialed" Text="Not Dialed" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblNotDialed" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvNotDialed" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblNotDialed" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblProcessedGood" Text="Processed Good" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblProcessedGood" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvProcessedGood" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblProcessedGood" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblProcessedBad" Text="Processed Bad" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblProcessedBad" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvProcessedBad" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblProcessedBad" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="background-color: #E9ECF1;width: 300px">
                    <asp:Label ID="lblNotProcessed" Text="Not Processed" CssClass="detailheader" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblNotProcessed" runat="server" RepeatDirection="Horizontal" Width="150px">
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvNotProcessed" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="rblNotProcessed" ErrorMessage="Please Select A Value !"></asp:RequiredFieldValidator>
                </td>
            </tr>

             <tr>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save Changes" Enabled="false" TabIndex="7"
                        onclick="btnSave_Click" />
                </td>
                <td align="left">
                    <asp:Button ID="btnAddNew" runat="server" Text="AddNew" TabIndex="8" CausesValidation="false"  OnClientClick="return confirm('Do you want to clear the fields and add a new record?')"
                        onclick="btnAddNew_Click" />
                </td>
            </tr>
            <tr>
	        <td colspan="2" align="center">
	            <div id="ConfirmationMessage" runat="server" class="alert" ></div>
	        </td>
            </tr>
            <tr>
	                    <td colspan="2" align="center">
	                        <div id="divErrorMsg" runat="server" style="color:Red"></div>
	                    </td>
            </tr>
            <tr>
                <td>
                <asp:HiddenField ID="hdnCallDispId" runat="server" />
                <asp:HiddenField ID="hdnCallDisposition" runat="server" />
                <asp:HiddenField ID="hdnDoNotCall" runat="server" />
                <asp:HiddenField ID="hdnSale" runat="server" />
                <asp:HiddenField ID="hdnContact" runat="server" />
                <asp:HiddenField ID="hdnBadLead" runat="server" />
                <asp:HiddenField ID="hdnCallBack" runat="server" />
                <asp:HiddenField ID="hdnNotInterested" runat="server" />
                <asp:HiddenField ID="hdnSystemDefaults" runat="server" />
                <asp:HiddenField ID="hdnCustom" runat="server" />
                <asp:HiddenField ID="hdnNotCalled" runat="server" />
                <asp:HiddenField ID="hdnDialed" runat="server" />
                <asp:HiddenField ID="hdnNotDialed" runat="server" />
                <asp:HiddenField ID="hdnProcessedGood" runat="server" />
                <asp:HiddenField ID="hdnProcessedBad" runat="server" />
                <asp:HiddenField ID="hdnNotProcessed" runat="server" />
                <asp:HiddenField ID="hdnFlagType" runat="server" />
                </td>
            </tr>
        </table>


</div>


</asp:Content>
