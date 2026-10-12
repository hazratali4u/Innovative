<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmCreateTaxSlabs.aspx.cs" Inherits="pr_frmCreateTaxSlabs" Title="Create Tax Slabs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            //find the current popup
            var popUp = $find('ModelPopup');

            //check it exists so the script won't fail
            if (popUp) {
                //Add the function below as the event
                popUp.add_hidden(HidePopupPanel);
            }
        }

        function HidePopupPanel(source, args) {
            //find the panel associated with the extender
            objPanel = document.getElementById(source._PopupControlID);

            //check the panel exists
            if (objPanel) {
                //set the display attribute, so it remains hidden on postback
                objPanel.style.display = 'none';
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }

    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <div>
                            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="31px" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                                PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                        </div>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                <strong>
                                                    <asp:Label ID="lbldesignationID" runat="server" Width="97px" Text="Amount From" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                             <cc1:FilteredTextBoxExtender ID="txtAmountFrom_FilteredTextBoxExtender" runat="server" TargetControlID="txtAmountFrom"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                                <asp:TextBox ID="txtAmountFrom" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                <asp:HiddenField ID="hdnTaxRuleID" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px; height: 18px">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="69px" Text="Amount To" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 18px">
                                             <cc1:FilteredTextBoxExtender ID="txtAmountTo_FilteredTextBoxExtender" runat="server" TargetControlID="txtAmountTo"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                                <asp:TextBox ID="txtAmountTo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                <strong>
                                                    <asp:Label ID="lblNickName" runat="server" Width="68px" Text="Percentage" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                            <cc1:FilteredTextBoxExtender ID="txtPercentage_FilteredTextBoxExtender" runat="server" TargetControlID="txtPercentage"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                             <asp:TextBox ID="txtPercentage" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="82px" Text="Amount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                            <cc1:FilteredTextBoxExtender ID="txtAmount_FilteredTextBoxExtender" runat="server" TargetControlID="txtAmount"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px" align="right" colspan="1">
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="82px" Font-Size="8pt"
                                                    Text="Save" ValidationGroup="vg" CssClass="Button" />
                                                <asp:Button ID="btnCancel" runat="server" Width="73px" Font-Size="8pt" Text="Cancel"
                                                    OnClick="btnCancel_Click" CssClass="Button" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="z-index: 101; left: 481px; width: 100px; position: absolute; top: 282px;
                        height: 100px" id="DIV1" onclick="return DIV1_onclick()">
                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                                </asp:ImageButton>&nbsp; Loading....
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="250px" ScrollBars="Auto">
                        <asp:GridView ID="grdTaxSlab" runat="server" Width="99%" ForeColor="SteelBlue" CssClass="gridRow2"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="White"
                            OnRowEditing="grdTaxSlab_RowEditing">
                            <Columns>
                             <asp:BoundField DataField="TaxRuleID" HeaderText="TaxRuleID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel "></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AmountFrom" HeaderText="Amount From">
                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AmountTo" HeaderText="AmountTo">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Percentage" HeaderText="Percentage">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Amount" HeaderText="Amount">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                            </HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
