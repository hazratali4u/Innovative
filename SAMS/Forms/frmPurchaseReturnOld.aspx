<%@ Page Title="Purchase Return" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmPurchaseReturnOld.aspx.cs" Inherits="Forms_frmPurchaseReturnOld" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
<script language="JavaScript" type="text/javascript">
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
        var Expense = document.getElementById('<%=txtAmount.ClientID %>').value;
        if (parseFloat(Expense) > 0) {
        }
        else {
            Expense = 0;
        }
        document.getElementById('<%=txtNetAmount.ClientID %>').value = (parseFloat(TotalNetAmount) + parseFloat(Expense)).toFixed(2);
        document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat(TotalGST).toFixed(2);
        document.getElementById('<%=txtDiscount.ClientID %>').value = parseFloat(TotalDiscount).toFixed(2);
        document.getElementById('<%=txtTotalAmount.ClientID %>').value = parseFloat(TotalGross).toFixed(2);
    }

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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width:40%">
                                            <table>
                                    <tr>
                                        <td style="width: 35%" align="right">
                                            <strong>
                                                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
                                            </strong>   
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 64%">
                                             <asp:TextBox ID="txtDate" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                            <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" EnableViewState="False"
                                                Format="dd-MMM-yyyy" PopupButtonID="ibDate" TargetControlID="txtDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 35%" align="right">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Text="Supplier:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 64%">
                                            <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 35%" align="right">
                                            &nbsp;</td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 64%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 35%" align="right">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" Text="Purchase For:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 64%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 35%" align="right">
                                            &nbsp;</td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 64%">
                                            &nbsp;</td>
                                    </tr>            
                                    <tr>
                                        <td style="width: 35%" align="right">                                            
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 64%">
                                            &nbsp;</td>
                                    </tr>                                
                                    <tr>
                                        <td style="width: 35%" align="right">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Ref #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 64%">
                                            <asp:TextBox ID="txtRefNo" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>                                    
                                </table>
                                        </td>
                                        <td style="width:60%" valign="top">
                                              <table>
                                                <tr>
                                                    <td style="width: 35%" align="right">
                                                        <strong>
                                                            <asp:Label ID="lblAccountHead" runat="server" Text="Expense Head:"></asp:Label>
                                                        </strong>   
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td style="width: 64%">
                                                         <asp:DropDownList ID="ddlAccount" runat="server" Width="200px"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="width: 35%" align="right">
                                                        <strong>
                                                            <asp:Label ID="lblAmount" runat="server" Text="Amount:"></asp:Label>
                                                        </strong>   
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td style="width: 64%">
                                                         <asp:TextBox ID="txtAmount" runat="server" Width="200px" onchange="CalculateTotals();" ></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmount">
                                                         </ajaxToolkit:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 35%" align="right">
                                                        <strong>
                                                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks:"></asp:Label>
                                                        </strong>   
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td style="width: 64%">
                                                         <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                </table>
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
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlHeader" runat="server" Width="99%" Height="100%">
                                                    <div>
                                                        <table width="99%" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                        <asp:Label ID="lblskuCode" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="Code" Width="100%" BackColor="#006699"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 26%; height: 30px;" align="center">
                                                                        <asp:Label ID="lblskuname" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                                            Font-Bold="True" Text="SKU Name" BackColor="#006699"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 16%; height: 30px;" align="center">
                                                                        <asp:Label ID="lblquantity" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                                            Font-Bold="True" Text="Return Quantity" BackColor="#006699"></asp:Label>
                                                                    </td>
                                                                    
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                        <asp:Label ID="Label1" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                                            Font-Bold="True" Text="Rate" BackColor="#006699"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                        <asp:Label ID="Label2" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
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
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server" Width="100%" Height="250px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="99%"
                                                        ShowHeader="False" onrowdatabound="GrdPurchase_RowDataBound" 
                                                        EnableModelValidation="True">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
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
                                                            <asp:BoundField DataField="Quantity" HeaderText="Ord Qty">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="8%" />
                                                            </asp:BoundField>
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
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel3" runat="server" Width="99%" Height="100%">
                                                    <div>
                                                        <table width="99%" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                    </td>
                                                                    <td style="width: 26%; height: 30px;" align="right">
                                                                        <strong>Total &nbsp;&nbsp;&nbsp;</strong>
                                                                    </td>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                        <asp:TextBox ID="txtOrderedQty" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                        <asp:TextBox ID="txtReceivedQty" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                    </td>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                        <asp:TextBox ID="txtTotalAmount" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                        <asp:TextBox ID="txtDiscount" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                    </td>
                                                                    <td style="width: 8%; height: 30px;" align="center">
                                                                        <asp:TextBox ID="txtGSTAmount" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 10%; height: 30px;" align="center">
                                                                        <asp:TextBox ID="txtNetAmount" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="130px" Font-Size="8pt"
                                                    Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                                    CssClass="Button" />
                                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="75px" Font-Size="8pt"
                                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
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
    </div>
</asp:Content>
