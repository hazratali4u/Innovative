<%@ Page Title="Delivery Challan" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDeliveryChallanEntryAdd.aspx.cs" Inherits="Forms_frmDeliveryChallanEntryAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
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
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">                                    
                                    <tr>
                                        <td style="width: 20%" align="left">
                                            <strong>
                                                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
                                            </strong>                                                
                                        </td>
                                        <td style="width: 1%" align="left">
                                        </td>
                                        <td style="width: 20%" align="left">
                                            <asp:TextBox ID="txtDate" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                            <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" EnableViewState="False"
                                                Format="dd-MMM-yyyy" PopupButtonID="ibDate" TargetControlID="txtDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                         <td style="width: 10%" align="left">
                                           
                                               
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width:48%" align="left">
                                          
                                        </td>
                                    </tr>                                                                        
                                       </table>
                                  <table width="100%">
                                    <tr>
                                        <td>
                                            <fieldset style="width: 78%">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 15%" align="left">
                                                            <strong>
                                                                <asp:Label ID="Label2" runat="server"  Text="Customer:"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 36%" align="left">
                                                            <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px" AutoPostBack="true"
                                                                
                                                                TabIndex="4" onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtOutletCode" onkeyup="SearchList()" runat="server" Width="76px"
                                                                Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 15%" align="left">
                                                            <asp:Label ID="Label15" runat="server" Text="Order # :" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 38%" align="left">
                                                            <asp:DropDownList ID="drpDocumentNo" runat="server" Width="200px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                                TabIndex="5">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 15%" align="left">
                                                            <strong>
                                                                <asp:Label ID="lblVehicalNo" runat="server"  Text="Vehicle Number:"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width:36%" align="left">
                                                            <asp:TextBox ID="txtVehicleNo" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        
                                                        <td style="width: 15%" align="left">
                                                           <strong> <asp:Label ID="lblfromLocation" runat="server" Text="Location:"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 38%" align="left">
                                                             <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                                    <td style="width: 1%">
                                                        </td>
                                                    <td style="width: 15%" align="left">
                                                     <asp:Label ID="Label3" runat="server" Text="Reference/ Job Name:" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 1%">
                                                        </td>
                                                     <td style="width:36%" align="left">
                                                     <asp:TextBox ID="txtReference" runat="server" Width="200px"></asp:TextBox>
                                                    </td>
                                                     <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 15%" align="left">
                                                            <asp:Label ID="lblDriverName" runat="server" Text="Driver Name:" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 38%" align="left">
                                                            <asp:TextBox ID="txtDriverName" runat="server" Width="200px"></asp:TextBox>
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
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table width="80%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlHeader" runat="server" Width="99%" Height="100%">
                                                    <div>
                                                        <table width="99%" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 15%; height: 30px;" align="center">
                                                                        <asp:Label ID="lblskuCode" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="Code" Width="100%" BackColor="#006699"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 40%; height: 30px;" align="center">
                                                                        <asp:Label ID="lblskuname" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                                            Font-Bold="True" Text="SKU Name" BackColor="#006699"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 15%; height: 30px;" align="center">
                                                                        <asp:Label ID="lblquantity" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                                            Font-Bold="True" Text="Ordered Quantity" BackColor="#006699"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 15%; height: 30px;" align="center">
                                                                        <asp:Label ID="Label1" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                                            Font-Bold="True" Text="Delivered Quantity" BackColor="#006699"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 15%; height: 30px;" align="center">
                                                                        <asp:Label ID="Label14" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                                            Font-Bold="True" Text="Current Delivered" BackColor="#006699"></asp:Label>
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
                                                        ShowHeader="false">
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
                                                                    Width="15%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                    Width="40%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Ord Qty">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="15%" />
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="Received" HeaderText="Received">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="15%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Current Received">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRecQty" runat="server" Width="100%" Text='<%#Eval("FREE_SKU") %>'></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        FilterType="Custom" ValidChars="0123456789." TargetControlID="txtRecQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="15%" />
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
                                                                    <td style="width: 15%; height: 30px;" align="center">
                                                                    </td>
                                                                    <td style="width: 55%; height: 30px;" align="right">
                                                                        <strong>Total &nbsp;&nbsp;&nbsp;</strong>
                                                                    </td>
                                                                    <td style="width: 15%; height: 30px;" align="center">
                                                                        <asp:TextBox ID="txtOrderedQty" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 15%; height: 30px;" align="center">
                                                                        <asp:TextBox ID="txtReceivedQty" runat="server" Enabled="false" Width="100%"></asp:TextBox>
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
</asp:Content>
