<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmPartialInvoice.aspx.cs" Inherits="Forms_frmPartialInvoice" Title="Partial Supply" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      <script language="JavaScript" type="text/javascript">
        
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest( startRequest );

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest( endRequest );

        function startRequest( sender, e )
        { 
            
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = true;

        }

        function endRequest( sender, e ) 
        { 
           
            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = false;
        }

        function pageLoad() {
            jQuery('#<%=txtBillBookNo.ClientID %>').keydown(IntValidation);
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
                    var discount = parseFloat(document.getElementById('<%=txtDiscount.ClientID%>').value);
                    if (document.getElementById('<%=txtDiscount.ClientID%>').value == '') {
                        discount = 0;
                    }
                    var amount = ((qty * rate) - discount).toFixed(2)
                    document.getElementById("<%= txtAmount.ClientID %>").value = amount;

                    if (document.getElementById('<%=cbGST.ClientID%>').checked) {
                        document.getElementById("<%= txtGST.ClientID %>").value = item.GST_RATE_TP;
                    }
                    else {
                        document.getElementById("<%= txtGST.ClientID %>").value = 0;
                    }
                    

                    var gst = (amount * item.GST_RATE_TP / 100).toFixed(2);
                    document.getElementById("<%= txtGSTAmount.ClientID %>").value = gst;

                    document.getElementById("<%= txtSKUTotal.ClientID %>").value = (parseFloat(amount) + parseFloat(gst)).toFixed(2);
                    break;
                }
            }
        }
 
    function ValidateForm()
    {
		var str;
    
		str  = document.getElementById('<%=txtskuName.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must Select SKU Name');
			return false;
		}
		str  = document.getElementById('<%=txtQuantity.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must Enter Quantity');
			return false;
		}
		
        return true;
    }
    function SearcSKUList(e) {        
     var l =  document.getElementById('<%= lstCode.ClientID %>');
     var tb = document.getElementById('<%= txtskuCode.ClientID %>');
     if (e.keyCode == 27)
     {
        document.getElementById("<%= numtxtTotalExtraDiscnt.ClientID %>").focus();
     }
     else
     {
            if(tb.value == "")
            {
                ClearSelection(l);
            }
            else
            {
                for (var i=0; i < l.options.length; i++)
                {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase()))
                    {
                        l.options[i].selected = true;
                        return false;
                    }
                    else
                    {
                        ClearSelection(l);
                    }
                }
           }
     }
 }
 function SearchList() {
     
     var l =  document.getElementById('<%= ListCustomer.ClientID %>');
     var tb = document.getElementById('<%= txtOutletCode.ClientID %>');
       if(tb.value == "")
            {
                ClearSelection(l);
            }
            else
            {
                for (var i=0; i < l.options.length; i++)
                {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase()))
                    {
                        l.options[i].selected = true;
                        return false;
                    }
                    else
                    {
                        ClearSelection(l);
                    }
                }
           }
 }
 function SearchedCode() {

    var str  
    var stroption
    str  = document.getElementById("<%= ListCustomer.ClientID %>").value;
    stroption = document.getElementById("<%= txtOutletCode.ClientID %>").value;
        
     if(str.length > 0)
     {      
            document.getElementById("<%= txtOutletCode.ClientID %>").value =  'OT' + str.substring(str.indexOf('| ') + 2);
            document.getElementById("<%= txtOutletName.ClientID %>").value =  str.substring(0,str.indexOf('|'));
            document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
    
     }
     else if(stroption.length == 0)
     {
            document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
            document.getElementById("<%= ListCustomer.ClientID %>").focus();
     }
     ClearSelection(document.getElementById('<%= ListCustomer.ClientID %>'));
    
 }
 function SearchSKUCode()
 {
    var str  = document.getElementById("<%= lstCode.ClientID %>").value;
    var stroption  = document.getElementById("<%= txtskuCode.ClientID %>").value; 
    
    if(str.length > 0)
     {      
            document.getElementById("<%= txtskuCode.ClientID %>").value =  str.substring(0,str.indexOf('-'));
            document.getElementById("<%= txtskuName.ClientID %>").value =  str.substring(str.indexOf('-') + 1,str.indexOf(':'));
            document.getElementById("<%= txtUnitRate.ClientID %>").value =  str.substring(str.indexOf(':') + 1);
            document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";
    
     }
     else if(stroption.length == 0)
     {
            document.getElementById("<%= Panel3.ClientID %>").className = "ShowPanel";
            document.getElementById("<%= lstCode.ClientID %>").focus();
     }
     ClearSelection(document.getElementById('<%= lstCode.ClientID %>'));
    
 }
 function SelectSkuCode(e) {
     var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
     if (key == 13) {
         e.preventDefault();
        var str  = document.getElementById("<%= lstCode.ClientID %>").value;
        document.getElementById("<%= txtskuCode.ClientID %>").value =  str.substring(0,str.indexOf('-'));
        document.getElementById("<%= txtskuName.ClientID %>").value =  str.substring(str.indexOf('-') + 1,str.indexOf(':'));
        document.getElementById("<%= txtUnitRate.ClientID %>").value =  str.substring(str.indexOf(':') + 1);
        document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";
        
        document.getElementById("<%= txtQuantity.ClientID %>").focus();
      }
 }
 function SelectCode(e)
 {
     var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
     if (key == 13) {
         e.preventDefault();
        var str  = document.getElementById("<%= ListCustomer.ClientID %>").value;
        document.getElementById("<%= txtOutletCode.ClientID %>").value = "OT" + str.substring(str.indexOf('|') + 2);
        document.getElementById("<%= txtOutletName.ClientID %>").value =  str.substring(0,str.indexOf('|'));
        document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
        document.getElementById("<%= txtskuCode.ClientID %>").focus();
      }
 }
 function ClearSelection(lb)
 {
    lb.selectedIndex = -1;
 }
 
  </script>
  <div id="right_data">
    
     <div >
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
                                     Wait Update
                                 </ProgressTemplate>
                             </asp:UpdateProgress>
                         </asp:Panel>
                     </div>
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                         <ContentTemplate>
                         <div style="LEFT: 120px; POSITION: absolute; TOP: 190px;">
                 <asp:Panel id="Panel1" runat="server" Width="327px" Height="237px" BorderWidth="1px" BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                 <TABLE style="BORDER-RIGHT: #ffffff thin groove; BORDER-TOP: #ffffff thin groove; BORDER-LEFT: #ffffff thin groove; WIDTH: 99%; BORDER-BOTTOM: #ffffff thin groove">
                    <TBODY>
                        <TR>
                            <TD style="BORDER-BOTTOM: black thin solid" align=left colSpan=2>
                            &nbsp;<strong>Select Customer from List Press Enter</strong>
                            </TD>
                            <TD style="BORDER-BOTTOM: black thin solid" vAlign=top align=right>
                            <asp:Button accessKey="S" id="Button4" runat="server" Width="21px" Height="16px" Font-Size="8pt" Text="X" BorderWidth="1px" BorderStyle="Groove" CssClass="Button" />
                            </TD>
                         </TR>
                    </TBODY>
                 </TABLE>
                 <asp:ListBox id="ListCustomer" onkeypress="SelectCode(event)" runat="server" Width="95%" Height="87%"></asp:ListBox>
                 </asp:Panel>
                 </div>
                 <div style="LEFT: 15px; POSITION: absolute; TOP: 275px; height: 270px;">
                  <asp:Panel id="Panel3" runat="server" Width="327px" Height="237px" BorderWidth="1px" BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                    <TABLE style="BORDER-RIGHT: #ffffff thin groove; BORDER-TOP: #ffffff thin groove; BORDER-LEFT: #ffffff thin groove; WIDTH: 99%; BORDER-BOTTOM: #ffffff thin groove">
                        <TBODY>
                            <TR>
                                <TD style="BORDER-BOTTOM: black thin solid" align=left colSpan=2>
                                    &nbsp;<strong>Select SKU from List Press Enter</strong></td>
                                <TD style="BORDER-BOTTOM: black thin solid" vAlign=top align=right>
                                    <asp:Button ID="Button5" runat="server" AccessKey="S" BorderStyle="Groove" BorderWidth="1px"
                                        Font-Size="8pt" Height="16px" Text="X" Width="21px" CssClass="Button" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:ListBox id="lstCode" onkeypress="SelectSkuCode(event)" runat="server" Width="95%" Height="87%" SelectionMode="Multiple"></asp:ListBox>
                  </asp:Panel>
                         &nbsp;
                 </div>  
                 <table width="100%">
                    <tr>
                        <td style="width:100%">
                        <fieldset style="width:90%">
                         <table width="100%">
                         <tr>
                                <td style="width:20%" align="right">
                                <strong><asp:Label id="lblBillBook" tabIndex=1 runat="server" Text="Invoice No:" CssClass="lblbox"></asp:Label></strong> 
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:30%">
                                    <asp:TextBox id="txtBillBookNo" runat="server"  MaxLength="10" Width="200px"></asp:TextBox>
                                    <asp:DropDownList ID="ddlDocumentNo" runat="server" Width="200px" AutoPostBack="True" 
                                        OnSelectedIndexChanged="ddlDocumentNo_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnEdit" runat="server" CssClass="Button" Text="..." 
                                        onclick="btnEdit_Click" />
                                </td>
                                <td style="width:3%"></td>
                                <td style="width:15%" align="right">
                                    <asp:CheckBox ID="cbGST" runat="server" Checked="true" Text="GST Invoice" />
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:30%">
                                    
                                </td>
                            </tr>
                         <tr>
                                <td style="width:20%" align="right">
                                    <strong> <asp:Label id="lbltoLocation" runat="server" Text="Location:"></asp:Label></strong>
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:30%">
                                    <asp:DropDownList id="drpDistributor" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width:3%"></td>
                                <td style="width:15%" align="right">
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:30%">
                                 </td>
                            </tr>                            
                         </table>
                         <legend></legend>
                         </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:80%;padding-left:100px">                        
                            <table width="80%">
                            <tr>
                                <td style="width:15%" align="right">
                                    <strong><asp:Label id="lblInvoiceType" runat="server" Text="Invoice Type:" Visible="false"></asp:Label></strong>   
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:84%">
                                    <asp:RadioButtonList id="RblPayMode" runat="server" Width="200px" Height="1px" RepeatDirection="Horizontal" Visible="false">
                                        <asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
                                        <asp:ListItem Value="215">Credit</asp:ListItem>                                        
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                         </table>
                         
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%">
                            <fieldset style="width:90%">                         
                         <table width="80%">                         
                            <tr>
                                <td style="width:15%" align="right">
                                    <strong><asp:Label id="Label1" runat="server" Width="74px" Text="Customer:"></asp:Label></strong>
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:10%">
                                    <asp:TextBox id="txtOutletCode" onkeyup="SearchList()" runat="server" Width="76px"></asp:TextBox>
                                </td>
                                <td style="width:1%"></td>
                                <td style="width:73%">
                                    <asp:TextBox id="txtOutletName" onfocus="SearchedCode()" runat="server" Width="80%"></asp:TextBox>
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
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <contenttemplate>
                <table>
                    <tr>
                        <td style="height: 16px">
                            <asp:Label id="lblskuCode" runat="server" Width="68px" Height="16px" ForeColor="White" Font-Bold="True" Text="  SKU Code" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label id="lblskuname" runat="server" Width="187px" Height="16px" ForeColor="White" Font-Bold="True" Text="   SKU Name" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label ID="Label3" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                ForeColor="White" Height="16px" Text="Quantity" Width="61px"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label id="lblquantity" runat="server" Width="62px" Height="16px" ForeColor="White" Font-Bold="True" Text="Unit Price" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
                        <td align="center" style="height: 16px">
                            <asp:Label ID="Label7" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                ForeColor="White" Height="16px" Text="Discount" Width="100%"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label ID="Label2" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                ForeColor="White" Height="16px" Text="Total" Width="100%"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label ID="Label9" runat="server" BackColor="#006699"  Font-Bold="True"
                                ForeColor="White" Height="16px" Text="S.Tax(%)" Width="100%"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label ID="Label10" runat="server" BackColor="#006699"  Font-Bold="True" ForeColor="White" Height="16px" Text="S.Tax(Rs)" Width="100%"></asp:Label>
                        </td>
                        <td style="height: 16px">
                            <asp:Label ID="Label12" runat="server" BackColor="#006699"  Font-Bold="True" ForeColor="White" Height="16px" Text="G.Total(Rs)" Width="100%"></asp:Label>
                        </td>
                        <td style="height: 16px">
                            <asp:Label ID="Label4" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                ForeColor="White" Height="16px" Text="Add SKU" Width="100%"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox id="txtskuCode" onkeyup="SearcSKUList(event)" runat="server" Width="68px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox id="txtskuName" runat="server" Width="181px" Font-Bold="True" CssClass="txtBox" Enabled="False" ></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtQuantity" runat="server" onfocus="SearchSKUCode()" 
                                Width="55px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox id="txtUnitRate" runat="server" Width="56px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtDiscount" runat="server" Width="62px"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtAmount" runat="server" Enabled="False" Width="72px"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtGST" runat="server" Enabled="False" Width="72px"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtGSTAmount" runat="server" Enabled="False" Width="72px"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtSKUTotal" runat="server" Enabled="False" Width="72px"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add Sku"
                                ValidationGroup="vg" Width="100px" AccessKey="A" CssClass="Button" onfocus="CalcualteTotal()"/></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="10">
                            <asp:Panel ID="Panel2" runat="server" Width="880px" Height="130px" ScrollBars="Vertical" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                <asp:GridView id="GrdPurchase" runat="server" ForeColor="SteelBlue" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>
<Columns>
<asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="68px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
<ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="65px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}">
<ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="65px"></ItemStyle>
</asp:BoundField>
    <asp:BoundField DataField="EXTRA_DISCOUNT" HeaderText="Discount">
        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
    </asp:BoundField>
    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:F2}">
        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="80px" />
    </asp:BoundField>
    <asp:BoundField DataField="GST_RATE" HeaderText="GST" DataFormatString="{0:F2}">
        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="80px" />
    </asp:BoundField>
    <asp:BoundField DataField="GST_AMOUNT" HeaderText="GSTAmount" DataFormatString="{0:F2}">
        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="80px" />
    </asp:BoundField>
    <asp:BoundField DataField="GTotal" HeaderText="SKUTotal" DataFormatString="{0:F2}">
        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="80px" />
    </asp:BoundField>        
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
<ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px"></ItemStyle>
</asp:CommandField>
<asp:TemplateField HeaderText="Delete"><ItemTemplate>
<asp:LinkButton id="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" CommandName="Delete"></asp:LinkButton>
</ItemTemplate>

<ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="45px"></ItemStyle>
</asp:TemplateField>
</Columns>
</asp:GridView>
                            </asp:Panel>
                            <asp:HiddenField ID="hfSKUDetail" runat="server" />
                        </td>
                    </tr>
                </table>
</contenttemplate>
        </asp:UpdatePanel>
                     </asp:Panel>
                     <asp:HiddenField ID="hfBillBookNo" runat="server" />
                 </td>                 
             </tr>
         </table>
        
           </div>
    <div >
        <table width="91%">
            <tr>
            <td align="left">
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
        <contenttemplate>
        <table width="100%">
            <tr>
                <td valign="top" align="center" style="width:65%">
                    <asp:Button accessKey="C" id="btnCalculate" TabIndex="100" Width="120px" onclick="btnCalculate_Click" runat="server" Font-Size="8pt" Text="Calculate" Enabled="False" CssClass="Button" />
                    &nbsp;&nbsp;
                    <asp:Button accessKey="S" id="btnSaveOrder" TabIndex="101" Width="120px" onclick="btnSaveOrder_Click" runat="server" Font-Size="8pt" Text="Save" Enabled="False" CssClass="Button" /> 
                    &nbsp;&nbsp;
                    <asp:Button accessKey="S" id="btnInvoice" TabIndex="101" Width="120px" onclick="btnInvoice_Click" runat="server" Font-Size="8pt" Text="Convert to Invoice" Enabled="false" Visible="False" CssClass="Button" /> 
                </td>
                <td style="width:35%">
                    <fieldset>
                    <div style="float:right;width:270px;">
            <table width="90%">
                <tr>
                    <td style="width:25%">
                        <strong><asp:Label id="lblDocumentNo1" runat="server" Text="Gross Sale:"></asp:Label></strong>                                
                    </td>
                    <td style="width:3%"></td>
                    <td style="width:72%">
                        <asp:TextBox id="txtGrossAmount" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%">
                        <strong><asp:Label id="lblfromLocation1" runat="server" Width="94px" Text="Discount:"></asp:Label></strong>
                    </td>
                    <td style="width:3%"></td>
                    <td style="width:72%">
                        <asp:TextBox id="numtxtTotalExtraDiscnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%">
                        <strong><asp:Label id="Label81" runat="server" Width="93px" Text="GST Amount:"></asp:Label></strong>
                    </td>
                    <td style="width:3%"></td>
                    <td style="width:72%">
                        <asp:TextBox id="numTxtTotalGST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:25%">
                        <strong><asp:Label id="Label91" runat="server" Text="Net Amount:"></asp:Label></strong>
                    </td>
                    <td style="width:3%"></td>
                    <td style="width:72%">
                        <asp:TextBox id="numTxtTotlAmnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
            </table>    
        </div>
                    <legend></legend>
                    </fieldset>
                </td>
            </tr>
        </table>
</contenttemplate>
    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>   
   </div>
</asp:Content>


