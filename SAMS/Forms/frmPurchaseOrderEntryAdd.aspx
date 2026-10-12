<%@ Page Title="Add Purchase Order" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmPurchaseOrderEntryAdd.aspx.cs" Inherits="Forms_frmPurchaseOrderEntryAdd" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<script language="JavaScript" type="text/javascript">

    function CalcualeAmount() {
        var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
        var Rate = document.getElementById('<%=txtRate.ClientID%>').value;

        document.getElementById("<%= txtAmount.ClientID %>").value = (Qty * Rate);
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


        str = document.getElementById('<%=txtRate.ClientID%>').value;

        if (str == null || str.length == 0) {

            alert('Must Enter Rate');

            return false;

        }

        return true;
    }
    function SearchList() {
        var l = document.getElementById('<%= lstCode.ClientID %>');
        var tb = document.getElementById('<%= txtskuCode.ClientID %>');

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
        var l = document.getElementById('<%= lstCode.ClientID %>');
        var str;
        for (var i = 0; i < l.options.length; i++) {
            if (l.options[i].selected) {
                str = l.options[i].value;
                ClearSelection(l);
                break;
            }
            else {
                str = "";
            }
        }
        var stroption = document.getElementById("<%= txtskuCode.ClientID %>").value;
        if (str.length > 0) {
            document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0, str.indexOf('-'));
            document.getElementById("<%= txtskuName.ClientID %>").value = str.substring(str.indexOf('-') + 1);
            document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
        }
        else if (stroption.length == 0) {
            document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
            document.getElementById("<%= lstCode.ClientID %>").focus();
        }

    }
    function SelectCode(e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var str = document.getElementById("<%= lstCode.ClientID %>").value;
            document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0, str.indexOf('-'));
            document.getElementById("<%= txtskuName.ClientID %>").value = str.substring(str.indexOf('-') + 1);
            document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
            document.getElementById("<%= txtQuantity.ClientID %>").focus();
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
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
                                            </strong>                                                
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtDate" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                            <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" EnableViewState="False"
                                                Format="dd-MMM-yyyy" PopupButtonID="ibDate" TargetControlID="txtDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" Text="Order For:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" Width="200px"
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblRefNo" runat="server" Text="Reference #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtRefNo" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblPrincipal" runat="server" Text="Principal:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblCurrencty" runat="server" Text="Currency:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="ddlCurrency" runat="server" Width="200px">
                                                <asp:ListItem Value="0" Text="PKR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblRate" runat="server" Text="Exch Rate:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtExchangeRate" runat="server" Width="195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="left: 10px; position: absolute; top: 270px">
                                                <asp:Panel ID="Panel1" runat="server" BackColor="Silver" BorderColor="Silver" BorderStyle="Groove"
                                                    BorderWidth="1px" CssClass="HidePanel" Height="170px" Width="250px">
                                                    SKU List<br />
                                                    <asp:ListBox ID="lstCode" runat="server" Height="151px" onkeypress="SelectCode(event)"
                                                        SelectionMode="Multiple" Width="239px"></asp:ListBox>
                                                </asp:Panel>
                                            </div>
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
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblskuCode" runat="server" Width="88px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="  SKU Code" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="lblskuname" runat="server" Width="206px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="   SKU Name" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="73px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFreeSKU" runat="server" Width="75px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Rate" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px" align="center">
                                                <asp:Label ID="lblBatchNo" runat="server" Width="100%" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Amount" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label41" runat="server" Width="100%" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Add SKU" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtskuCode" onkeyup="SearchList()" runat="server" Width="83px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtskuName" runat="server" Width="200px" Font-Bold="True" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" onfocus="SearchedCode()" runat="server" Width="70px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtQuantity">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRate" runat="server" Width="70px" onblur="CalcualeAmount();">0.00</asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtRate">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="76px" Enabled="false">0.00</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Add Sku" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="6">
                                                <asp:Panel ID="Panel2" runat="server" Width="640px" Height="130px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        ShowHeader="False" Width="620px">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="205px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRICE" HeaderText="Rate" DataFormatString="{0:F3}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:F3}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px" />
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Height="16px" Text="Total Quantity:"></asp:Label></strong>
                                                <asp:TextBox ID="txtTotalQuantity" runat="server" Width="88px" ReadOnly="True"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<strong><asp:Label ID="Label1" runat="server" Height="16px"
                                                    Text="Amount [FC]:"></asp:Label></strong>
                                                <asp:TextBox ID="txtAmountFC" runat="server" Width="120px" ReadOnly="True"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<strong><asp:Label ID="Label2" runat="server" Height="16px"
                                                    Text="[Rs.]:"></asp:Label></strong>
                                                <asp:TextBox ID="txtAmountRs" runat="server" Width="120px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document"  OnClick="btnSaveDocument_Click"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel"  OnClick="btnCancel_Click" CssClass="Button" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

