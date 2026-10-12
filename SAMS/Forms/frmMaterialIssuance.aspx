<%@ Page Title="Material Issuance" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmMaterialIssuance.aspx.cs" Inherits="Forms_frmMaterialIssuance" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<script language="JavaScript" type="text/javascript">

    function ValidateForm() {
        var str;      
        str = document.getElementById('<%=txtQuantity.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Must Enter Quantity');
            return false;
        }

        return true;
    }

    function pageLoad() {

        $("select").searchable();

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
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
<div id="right_data">
    <asp:UpdatePanel ID="pnlBillOfMaterial" runat="server">
        <ContentTemplate>
            <div>
                <asp:UpdateProgress ID="UpdateProgress" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif" Width="31px" />
                    </ProgressTemplate>
                    </asp:UpdateProgress>
                <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>            
            </div>
            <table width="100%">
            <tr>
                <td style="width:10%" align="right">
                    <strong>
                        <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                    </strong>  
                </td>
                <td style="width:1%"></td>
                <td style="width:89%">
                    <asp:TextBox ID="txtDate" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                    <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                    </asp:ImageButton>
                    <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" EnableViewState="False"
                        Format="dd-MMM-yyyy" PopupButtonID="ibDate" TargetControlID="txtDate">
                    </ajaxToolkit:CalendarExtender>   
                </td>
            </tr>
            <tr>
                <td style="width:10%" align="right">
                    <strong> <asp:Label ID="Label1" runat="server" Text="Location:"></asp:Label> </strong>
                </td>
                <td style="width:1%"></td>
                <td style="width:89%">
                    <asp:DropDownList ID="ddlLocation" runat="server" Width="200px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlLocation_SelectedIndexChanged">                    
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:10%" align="right">
                    <strong> <asp:Label ID="lblDocumentNo" runat="server" Text="Document No:"></asp:Label> </strong>
                </td>
                <td style="width:1%"></td>
                <td style="width:89%">
                    <asp:DropDownList ID="ddlDocumentNo" runat="server" Width="200px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlDocumentNo_SelectedIndexChanged">                    
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:10%" align="right">
                    <strong> <asp:Label ID="lblCategory" runat="server" Text="Select Category:"></asp:Label> </strong>
                </td>
                <td style="width:1%"></td>
                <td style="width:89%">
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="200px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlCategory_SelectedIndexChanged">                    
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td style="width:5%">&nbsp;</td>
                            <td style="width:20%;">
                                <asp:Label ID="lblSKU" runat="server" Text="SKU" BackColor="#006699"  ForeColor="White" Font-Bold="True" Width="100%"></asp:Label>
                            </td>
                            <td style="width:5%">
                                <asp:Label ID="lblQuantity" runat="server" Text="Quantity" BackColor="#006699"  ForeColor="White" Font-Bold="True" Width="100%"></asp:Label>
                            </td>
                            <td style="width:5%">
                                <asp:Label ID="lblAdd" runat="server" Text="Add SKU" BackColor="#006699"  ForeColor="White" Font-Bold="True" Width="100%"></asp:Label>
                            </td>
                            <td style="width:65%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width:5%">&nbsp;</td>
                            <td style="width:20%">
                                <asp:DropDownList ID="ddlSKU" runat="server" Width="100%">                    
                                </asp:DropDownList>
                            </td>
                            <td style="width:5%">
                                <asp:TextBox ID="txtQuantity" runat="server" Width="100px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeQuantity" runat="server"
                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtQuantity">
                                </ajaxToolkit:FilteredTextBoxExtender>                                
                            </td>
                            <td style="width:5%">
                                <asp:Button ID="btnAdd" runat="server" Text="Add SKU" CssClass="Button" 
                                    onclick="btnAdd_Click" OnClientClick="return ValidateForm();" />
                            </td>
                             <td style="width:65%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width:5%">&nbsp;</td>
                            <td style="width:30%" colspan="3">
                                <asp:Panel ID="pnlSKU" runat="server" Width="100%" Height="150px" ScrollBars="Vertical" BorderWidth="1px">
                                    <asp:GridView id="gvSKU" runat="server" ForeColor="SteelBlue" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False" OnRowDeleting="gvSKU_RowDeleting" OnRowEditing="gvSKU_RowEditing">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>
                                        <Columns>
                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QUANTITY" HeaderText="Quantity">
                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="65px"></ItemStyle>
                                            </asp:BoundField>      
                                            <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px"></ItemStyle>
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton id="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="45px"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>                            
                            <td style="width:65%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;</td>
                        </tr>
                         <tr>
                            <td style="width:5%">&nbsp;</td>
                            <td style="width:30%;" colspan="3" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" 
                                    onclick="btnSave_Click" />
                            </td>
                            <td style="width:65%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>    
</div>
</asp:Content>

