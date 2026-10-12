<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOrderEntry.aspx.cs" Inherits="Forms_frmOrderEntry" Title="Sales Invoice"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

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

        function pageLoad() {

            jQuery('#<%=txtBillBookNo.ClientID %>').keydown(IntValidation);
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
   
</script>
    <script language="JavaScript" type="text/javascript">
    
        function IntValidation(event) {
            // Allow: backspace, delete, tab , escape
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 ||
            // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
            // Allow: Dash
            (event.keyCode == 189 && event.shiftKey === false) ||
            //Allow 0-9
            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
            //Keypad numbers
            (event.keyCode >= 96 && event.keyCode <= 105)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }
        function DoCalculation(objRecQty, objRate, objGross, objDiscount, objGSTRate, objGSTAmount, objNetAmount) {
            if (parseFloat(objRecQty.value.replace(',', '')) > 0) {
            }
            else {
                objRecQty.value = 0;
            }

            if (parseFloat(objRate.value.replace(',', '')) > 0) {
            }
            else {
                objRate.value = 0;
            }

            if (parseFloat(objDiscount.value.replace(',', '')) > 0) {
            }
            else {
                objDiscount.value = 0;
            }

            if (parseFloat(objRate.value.replace(',', '')) > 0) {
            }
            else {
                objRate.value = 0;
            }

            if (parseFloat(objGSTRate.value.replace(',', '')) > 0) {
            }
            else {
                objGSTRate.value = 0;
            }

            objGross.value = parseFloat(objRecQty.value * objRate.value).toFixed(2);
            objGSTAmount.value = parseFloat((parseFloat(objGross.value) - parseFloat(objDiscount.value)) * parseFloat(objGSTRate.value) / 100).toFixed(2);

            objNetAmount.value = parseFloat(parseFloat(objGross.value) - parseFloat(objDiscount.value) + parseFloat(objGSTAmount.value)).toFixed(2);

            CalculateTotals();
        }

        function CalculateTotals() {
            var gv = document.getElementById('<%= GrdPurchase.ClientID %>');
            var tb = gv.getElementsByTagName("input");

            var TotalNetAmount = 0
            var TotalGST = 0;
            var TotalDiscount = 0;
            var TotalGross = 0;
            for (var i = 0; i < tb.length; i++) {
                if (tb[i].type == "text") {
                    if (tb[i].name.indexOf("txtGTotal") !== -1) {
                        var objValue = parseFloat(tb[i].value.replace(',', ''));
                        if (parseFloat(objValue) > 0) {
                            TotalNetAmount += parseFloat(objValue);
                        }
                    }
                    else if (tb[i].name.indexOf("txtGSTAmount") !== -1) {
                        var objValue = parseFloat(tb[i].value.replace(',', ''));
                        if (parseFloat(objValue) > 0) {
                            TotalGST += parseFloat(objValue);
                        }
                    }
                    else if (tb[i].name.indexOf("txtDiscount") !== -1) {
                        var objValue = parseFloat(tb[i].value.replace(',', ''));
                        if (parseFloat(objValue) > 0) {
                            TotalDiscount += parseFloat(objValue);
                        }
                    }
                    else if (tb[i].name.indexOf("txtGross") !== -1) {
                        var objValue = parseFloat(tb[i].value.replace(',', ''));
                        if (parseFloat(objValue) > 0) {
                            TotalGross += parseFloat(objValue);
                        }
                    }
                }
            }
           
            document.getElementById("<%= txtGrossAmount.ClientID %>").value = numberWithCommas(parseFloat(TotalGross).toFixed(2));
            document.getElementById("<%= numtxtTotalExtraDiscnt.ClientID %>").value = numberWithCommas(parseFloat(TotalDiscount).toFixed(2));
            document.getElementById("<%= numTxtTotalGST.ClientID %>").value = numberWithCommas(parseFloat(TotalGST).toFixed(2));
            document.getElementById("<%= numTxtTotlAmnt.ClientID %>").value = numberWithCommas(parseFloat(TotalNetAmount).toFixed(2));
        }

        function numberWithCommas(x) {
            x = x.toString();
            var pattern = /(-?\d+)(\d{3})/;
            while (pattern.test(x))
                x = x.replace(pattern, "$1,$2");
            return x;
        }

    </script>
    <div id="right_data">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <div>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                Width="31px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                        PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                </div>                                                                                        
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div style="z-index: 101; left: 812px; width: 100px; position: absolute; top: 369px;
                            height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="31px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>                                                                
                                <table width="108%">
                                    <tr>
                                        <td style="width: 100%">
                                            <fieldset style="width: 90%">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 10%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:TextBox ID="txtDate" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                            </asp:ImageButton>
                                                            <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" EnableViewState="False"
                                                                Format="dd-MMM-yyyy" PopupButtonID="ibDate" TargetControlID="txtDate">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 10%" align="right">
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 47%">                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lbltoLocation" runat="server" Text="Location:"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 3%">
                                                        </td>
                                                        <td style="width: 15%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblBillBook" TabIndex="1" runat="server" Text="Invoice No:" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:TextBox ID="txtBillBookNo" runat="server" MaxLength="10" Width="180px" Enabled="false"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlDocumentNo" runat="server" Width="180px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlDocumentNo_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:Button ID="btnEdit" runat="server" CssClass="Button" Text="..." 
                                                                OnClick="btnEdit_Click" TabIndex="1" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <legend></legend>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 80%; padding-left: 100px">
                                            <table width="80%">
                                                <tr>
                                                    <td style="width: 15%" align="right">
                                                        <strong>
                                                            <asp:Label ID="lblInvoiceType" runat="server" Text="Invoice Type:"></asp:Label></strong>
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td style="width: 34%">
                                                        <asp:RadioButtonList ID="RblPayMode" runat="server" Width="200px" Height="1px" 
                                                            RepeatDirection="Horizontal" TabIndex="2">
                                                            <asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
                                                            <asp:ListItem Value="215">Credit</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="width:50%;">
                                                        <asp:CheckBox ID="cbGST" runat="server" Checked="true" Text="GST Invoice" OnCheckedChanged="cbGST_CheckedChanged"
                                                            AutoPostBack="true" TabIndex="3" />
                                                    </td>
                                                </tr>                                                
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <fieldset style="width: 90%">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 8%">
                                                        </td>
                                                        <td style="width: 12%" align="right">
                                                            <strong>
                                                                <asp:Label ID="Label1" runat="server" Width="44px" Text="Customer:"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 3%">
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:DropDownList ID="ddlOrderedCustomer" runat="server" Width="200px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlOrderedCustomer_SelectedIndexChanged" 
                                                                TabIndex="4">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtOutletCode" onkeyup="SearchList()" runat="server" Width="76px"
                                                                Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 7%">
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="Label15" runat="server" Text="Order # :" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 33%">
                                                            <asp:DropDownList ID="ddlCustomerOrder" runat="server" Width="200px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlCustomerOrder_SelectedIndexChanged" 
                                                                TabIndex="5">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 8%">
                                                        </td>
                                                        <td style="width: 12%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblDC" runat="server" Text="DC #:" Visible="false"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td style="width: 3%">
                                                        </td>
                                                        <td style="width: 30%">
                                                             <asp:CheckBoxList ID="cbDC" runat="server" Width="200px" BorderWidth="1px">
                                                             </asp:CheckBoxList>  
                                                        </td>
                                                        <td style="width: 7%">
                                                        </td>
                                                        <td style="width: 10%">
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 33%">                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 8%">
                                                        </td>
                                                        <td style="width: 12%" align="right">
                                                           
                                                        </td>
                                                        <td style="width: 3%">
                                                        </td>
                                                        <td style="width: 30%">
                                                              <asp:Button ID="btnShow" runat="server" Text="Get Data" CssClass="button" 
                                                                Visible="false" onclick="btnShow_Click" />
                                                        </td>
                                                        <td style="width: 7%">
                                                        </td>
                                                        <td style="width: 10%">
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 33%">                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                                <legend></legend>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>                   
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel5" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 8%; height: 30px;" align="center">
                                                <asp:Label ID="lblskuCode" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                    Text="Code" Width="100%" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="width: 26%; height: 30px;" align="center">
                                                <asp:Label ID="lblskuname" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                    Font-Bold="True" Text="SKU Name" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="width: 8%; height: 30px;" align="center">
                                                <asp:Label ID="lblquantity" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                    Font-Bold="True" Text="Ordered Quantity" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="width: 8%; height: 30px;" align="center">
                                                <asp:Label ID="Label14" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                    Font-Bold="True" Text="Received Quantity" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="width: 8%; height: 30px;" align="center">
                                                <asp:Label ID="Label2" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                    Font-Bold="True" Text="Rate" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="width: 8%; height: 30px;" align="center">
                                                <asp:Label ID="Label3" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="100%" Text="Gross Amount" Width="100%"></asp:Label>
                                            </td>
                                            <td style="width: 8%; height: 30px;" align="center">
                                                <asp:Label ID="Label20" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="100%" Text="Discount" Width="100%"></asp:Label>
                                            </td>
                                            <td style="width: 8%; height: 30px;" align="center">
                                                <asp:Label ID="Label9" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                    Height="100%" Text="S.Tax(%)" Width="100%"></asp:Label>
                                            </td>
                                            <td style="width: 8%; height: 30px;" align="center">
                                                <asp:Label ID="Label10" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                    Height="100%" Text="S.Tax(Rs)" Width="100%"></asp:Label>
                                            </td>
                                            <td style="width: 10%; height: 30px;" align="center">
                                                <asp:Label ID="Label12" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                    Height="100%" Text="Net Amount" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="10">                                            
                                                <asp:Panel ID="Panel2" runat="server" Width="100%" Height="130px" ScrollBars="Vertical"
                                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" BackColor="White"
                                                        HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" 
                                                        OnRowEditing="GrdPurchase_RowEditing" Width="100%" 
                                                        onrowdatabound="GrdPurchase_RowDataBound" TabIndex="6">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                    Width="8%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                    Width="26%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Ord Qty">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="8%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Rcd Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRecQty" runat="server" Width="100%" Text='<%# Eval("RcdQty")%>' Enabled="false"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        FilterType="Custom" ValidChars="0123456789." TargetControlID="txtRecQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" runat="server" Width="100%"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gross">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtGross" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDiscount" runat="server" Width="100%"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sales Tax(%)">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtGSTRate" runat="server" Width="100%"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sales Tax(Rs)">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtGSTAmount" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Grand Total(Rs)">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtGTotal" runat="server" Width="100%"  Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="10%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>                                                 
                                                <asp:HiddenField ID="hfSKUDetail" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <asp:HiddenField ID="hfBillBookNo" runat="server" />
                    </td>
                    <td>
                        <asp:UpdatePanel ID="upRight" runat="server">
                            <ContentTemplate>
                                <table width="2.5%">
                                <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="Label11" runat="server" Text="Gross Sale" Font-Bold="true" Font-Size="Medium"></asp:Label></strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtGrossAmount" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                        Height="29px" Font-Size="Medium" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Width="60px" Text="Discount" Font-Bold="true"
                                                    Font-Size="Medium"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="numtxtTotalExtraDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                Height="29px" Font-Size="Medium" Font-Bold="True" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="120px" Text="GST Amount" Font-Bold="true"
                                                    Font-Size="Medium" Enabled="false"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="numTxtTotalGST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                Height="29px" Font-Size="Medium" Enabled = "false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label13" runat="server" Text="Net Amount" Font-Bold="true" Font-Size="Medium"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="numTxtTotlAmnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                Font-Size="Medium" Height="29px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="85%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td valign="top" align="center" style="width: 65%">
                                            
                                            <asp:Button AccessKey="S" ID="btnSaveOrder" TabIndex="0" Width="100px" OnClick="btnSaveOrder_Click"
                                                runat="server" Font-Size="8pt" Text="Save" CssClass="Button" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
