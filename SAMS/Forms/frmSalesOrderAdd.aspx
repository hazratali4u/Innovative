<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSalesOrderAdd.aspx.cs" Inherits="Forms_frmSalesOrderAdd" Title="Sales Order" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;
        }


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

        function CalcualteTotal() {
            var str;
            str = document.getElementById('<%=txtskuName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select SKU Name');
                return false;
            }
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }

            var obj = jQuery.parseJSON($("#<%= hfSKUDetail.ClientID %>").val());
            for (var i = 0; i < obj.length; i++) {
                var item = obj[i];
                if (item.SKU_CODE == document.getElementById('<%=txtskuCode.ClientID%>').value) {
                    var qty = parseFloat(document.getElementById('<%=txtQuantity.ClientID%>').value);
                    var rate = parseFloat(document.getElementById('<%=txtUnitRate.ClientID%>').value);
                    var discount = 0;
                    var amount = ((qty * rate) - discount).toFixed(2)
                    document.getElementById("<%= txtAmount.ClientID %>").value = amount;
                    var gst = (amount * item.GST_RATE_TP / 100).toFixed(2);

                    break;
                }
            }
        }

        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtskuName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select SKU Name');
                return false;
            }
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }

            return true;
        }
        function SearcSKUList(e) {
            var l = document.getElementById('<%= lstCode.ClientID %>');
            var tb = document.getElementById('<%= txtskuCode.ClientID %>');
            if (e.keyCode == 27) {

            }
            else {
                if (tb.value == "") {
                    ClearSelection(l);
                }
                else {
                    for (var i = 0; i < l.options.length; i++) {
                        if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                            l.options[i].selected = true;
                            return false;
                        }
                        else {
                            ClearSelection(l);
                        }
                    }
                }
            }
        }
        function SearchList() {

            var l = document.getElementById('<%= ListCustomer.ClientID %>');
            var tb = document.getElementById('<%= txtOutletCode.ClientID %>');
            if (tb.value == "") {
                ClearSelection(l);
            }
            else {
                for (var i = 0; i < l.options.length; i++) {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                        l.options[i].selected = true;
                        return false;
                    }
                    else {
                        ClearSelection(l);
                    }
                }
            }
        }
        function SearchedCode() {

            var str
            var stroption
            str = document.getElementById("<%= ListCustomer.ClientID %>").value;
            stroption = document.getElementById("<%= txtOutletCode.ClientID %>").value;

            if (str.length > 0) {
                document.getElementById("<%= txtOutletCode.ClientID %>").value = 'OT' + str.substring(str.indexOf('| ') + 2);
                document.getElementById("<%= txtOutletName.ClientID %>").value = str.substring(0, str.indexOf('|'));
                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";

            }
            else if (stroption.length == 0) {
                document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= ListCustomer.ClientID %>").focus();
            }
            ClearSelection(document.getElementById('<%= ListCustomer.ClientID %>'));

        }
        function SearchSKUCode() {
            var str = document.getElementById("<%= lstCode.ClientID %>").value;
            var stroption = document.getElementById("<%= txtskuCode.ClientID %>").value;

            if (str.length > 0) {
                document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0, str.indexOf('-'));
                document.getElementById("<%= txtskuName.ClientID %>").value = str.substring(str.indexOf('-') + 1, str.indexOf(':'));
                document.getElementById("<%= txtUnitRate.ClientID %>").value = str.substring(str.indexOf(':') + 1);
                document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";

            }
            else if (stroption.length == 0) {
                document.getElementById("<%= Panel3.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= lstCode.ClientID %>").focus();
            }
            ClearSelection(document.getElementById('<%= lstCode.ClientID %>'));

        }
        function SelectSkuCode(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                var str = document.getElementById("<%= lstCode.ClientID %>").value;
                document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0, str.indexOf('-'));
                document.getElementById("<%= txtskuName.ClientID %>").value = str.substring(str.indexOf('-') + 1, str.indexOf(':'));
                document.getElementById("<%= txtUnitRate.ClientID %>").value = str.substring(str.indexOf(':') + 1);
                document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";

                document.getElementById("<%= txtQuantity.ClientID %>").focus();
            }
        }
        function SelectCode(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                var str = document.getElementById("<%= ListCustomer.ClientID %>").value;
                document.getElementById("<%= txtOutletCode.ClientID %>").value = "OT" + str.substring(str.indexOf('|') + 2);
                document.getElementById("<%= txtOutletName.ClientID %>").value = str.substring(0, str.indexOf('|'));
                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
                document.getElementById("<%= txtskuCode.ClientID %>").focus();
            }
        }
        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }
 
    </script>
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
                        <div style="z-index: 101; left: 612px; width: 100px; position: absolute; top: 369px;
                            height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="31px" />
                                        Wait Update</ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div style="left: 120px; position: absolute; top: 190px;">
                                    <asp:Panel ID="Panel1" runat="server" Width="327px" Height="237px" BorderWidth="1px"
                                        BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                                        <table style="border-right: #ffffff thin groove; border-top: #ffffff thin groove;
                                            border-left: #ffffff thin groove; width: 99%; border-bottom: #ffffff thin groove">
                                            <tbody>
                                                <tr>
                                                    <td style="border-bottom: black thin solid" align="left" colspan="2">
                                                        &nbsp;<strong>Select Customer from List Press Enter</strong>
                                                    </td>
                                                    <td style="border-bottom: black thin solid" valign="top" align="right">
                                                        <asp:Button AccessKey="S" ID="Button4" runat="server" Width="21px" Height="16px"
                                                            Font-Size="8pt" Text="X" BorderWidth="1px" BorderStyle="Groove" CssClass="Button" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:ListBox ID="ListCustomer" onkeypress="SelectCode(event)" runat="server" Width="95%"
                                            Height="87%"></asp:ListBox>
                                    </asp:Panel>
                                </div>
                                <div style="left: 15px; position: absolute; top: 275px; height: 270px;">
                                    <asp:Panel ID="Panel3" runat="server" Width="327px" Height="237px" BorderWidth="1px"
                                        BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                                        <table style="border-right: #ffffff thin groove; border-top: #ffffff thin groove;
                                            border-left: #ffffff thin groove; width: 99%; border-bottom: #ffffff thin groove">
                                            <tbody>
                                                <tr>
                                                    <td style="border-bottom: black thin solid" align="left" colspan="2">
                                                        &nbsp;<strong>Select SKU from List Press Enter</strong>
                                                    </td>
                                                    <td style="border-bottom: black thin solid" valign="top" align="right">
                                                        <asp:Button ID="Button5" runat="server" AccessKey="S" BorderStyle="Groove" BorderWidth="1px"
                                                            Font-Size="8pt" Height="16px" Text="X" Width="21px" CssClass="Button" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:ListBox ID="lstCode" onkeypress="SelectSkuCode(event)" runat="server" Width="95%"
                                            Height="87%" SelectionMode="Multiple"></asp:ListBox>
                                    </asp:Panel>
                                    &nbsp;
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%">
                                            <fieldset style="width: 73%">
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
                                                        <td style="width: 10%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lbltoLocation0" runat="server" Text="Location:"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 10%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblBillBook" TabIndex="1" runat="server" Text="Invoice No:" CssClass="lblbox"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 47%">
                                                            <asp:TextBox ID="txtBillBookNo" runat="server" MaxLength="10" Width="200px" Enabled="false"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlDocumentNo" runat="server" Width="200px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlDocumentNo_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:Button ID="btnEdit" runat="server" CssClass="Button" Text="..." OnClick="btnEdit_Click" />
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
                                                            <asp:Label ID="lblInvoiceType" runat="server" Text="Order Type:"></asp:Label></strong>
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td style="width: 84%">
                                                        <asp:RadioButtonList ID="RblPayMode" runat="server" Width="200px" Height="1px" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
                                                            <asp:ListItem Value="215">Credit</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" align="right">
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td style="width: 84%">
                                                        <asp:CheckBox ID="cbGST" runat="server" Checked="true" Text="GST Invoice" OnCheckedChanged="cbGST_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <fieldset style="width: 73%">
                                                <table width="80%">
                                                    <tr>
                                                        <td style="width: 15%" align="right">
                                                            <strong>
                                                                <asp:Label ID="Label1" runat="server" Width="74px" Text="Customer:"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox ID="txtOutletCode" onkeyup="SearchList()" runat="server" Width="76px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 73%">
                                                            <asp:TextBox ID="txtOutletName" onfocus="SearchedCode()" runat="server" Width="80%"></asp:TextBox>
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
                        <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSave">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="height: 16px; width: 110px">
                                                <asp:Label ID="lblskuCode" runat="server" Width="95px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="  SKU Code" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px; width: 250px">
                                                <asp:Label ID="lblskuname" runat="server" Width="240px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="   SKU Name" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px; width: 70px; height: 16px">
                                                <asp:Label ID="Label3" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Quantity" Width="60px"></asp:Label>
                                            </td>
                                            <td style="height: 16px; width: 70px; height: 16px">
                                                <asp:Label ID="lblquantity" runat="server" Width="60px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Unit Price" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="width: 150px; height: 16px">
                                                <asp:Label ID="Label2" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Amount" Width="135px"></asp:Label>
                                            </td>
                                            <td style="height: 16px; width: 145px">
                                                <asp:Label ID="Label4" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Add SKU" Width="69%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px">
                                                <asp:TextBox ID="txtskuCode" onkeyup="SearcSKUList(event)" runat="server" Width="94px"></asp:TextBox>
                                            </td>
                                            <td style="width: 250px">
                                                <asp:TextBox ID="txtskuName" runat="server" Width="240px" Font-Bold="True" CssClass="txtBox"
                                                    Enabled="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtQuantity" runat="server" onfocus="SearchSKUCode()" Width="60px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUnitRate" onchange="CalcualteTotal()" runat="server" Width="56px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add Sku"
                                                    ValidationGroup="vg" Width="100px" AccessKey="A" CssClass="Button" onfocus="CalcualteTotal()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="6">
                                                <asp:Panel ID="Panel2" runat="server" Width="94%" Height="150px" ScrollBars="Vertical"
                                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" BackColor="White"
                                                        HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="109px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="250px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="68.5px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="72.5px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="144px" />
                                                            </asp:BoundField>
                                                            <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px"></ItemStyle>
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        CommandName="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="45px">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <asp:HiddenField ID="hfSKUDetail" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; width: 110px">
                                                &nbsp;
                                            </td>
                                            <td style="height: 16px; width: 250px">
                                                <asp:Label ID="lblTotal" runat="server" Width="100px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Total" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px; width: 70px; height: 16px">
                                                <asp:TextBox ID="txtTotalQuantity" runat="server" Width="60px" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td style="height: 16px; width: 70px; height: 16px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 150px; height: 16px">
                                                <asp:TextBox ID="txtTotalAmount" runat="server" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td style="height: 16px; width: 145px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <asp:HiddenField ID="hfBillBookNo" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="70%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <br />
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td valign="top" align="center" style="width: 60%">
                                            <asp:Button AccessKey="S" ID="btnSaveOrder" TabIndex="101" Width="100px" OnClick="btnSaveOrder_Click"
                                                runat="server" Font-Size="8pt" Text="Save" CssClass="Button" />
                                            &nbsp;&nbsp;
                                            <asp:Button AccessKey="C" ID="btnCancel" TabIndex="100" Width="100px" OnClick="btnCancel_Click"
                                                runat="server" Font-Size="8pt" Text="Cancel" CssClass="Button" />
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
