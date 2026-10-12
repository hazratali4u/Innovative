<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmTier.aspx.cs" Inherits="pr_frmTier" Title="Tier" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Forms/UserControl/MultiCheckCombo.ascx" tagname="MultiCheckCombo" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <style>
        .customerSerch input
        {
            -moz-box-shadow: inset 0 0 6px #333;
            -webkit-box-shadow: inset 0 0 6px #333;
            box-shadow: inset 0 0 6px #333;
            -moz-border-radius: 3px;
            height: 26px;
            float: left;
            padding-left: 5px;
            border: none;
            color: #666;
            margin-top: 10px;
            margin-left: 10px;
            border-radius: 3px;
        }
        .customerSerch
        {
            clear: both;
        }
</style>    
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
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <div id="right_data">
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
                <asp:Panel ID="pnlTierContent" runat="server">
                <table width="100%">
                    <tr>
                        <td width="5px">
                            <asp:HiddenField ID="hdnTierID" runat="server" />
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                <tr>
                                <td height="20px">
                                </td>
                                </tr>
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="Label2" runat="server"  Text="Tier Name"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>    
                                        </td>
                                        <td width="5%">
                                            
                                        </td>
                                        <td width="45%">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="Label4" runat="server" Text="Bike Maintenance"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtBike" runat="server" Width="200px"></asp:TextBox>
                                         <cc1:FilteredTextBoxExtender ID="txtBike_FilteredTextBoxExtender" runat="server" TargetControlID="txtBike"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td width="5%">
                                            
                                        </td>
                                        <td width="45%">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="Label6" runat="server"  Text="Fuel"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtFuel" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td width="5%">
                                            
                                        </td>
                                        <td width="45%">
                                        
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="Label7" runat="server" Text="Commission (%)"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtCommission" runat="server" Width="200px"></asp:TextBox>
                                             <cc1:FilteredTextBoxExtender ID="txtCommission_FilteredTextBoxExtender" runat="server" TargetControlID="txtCommission"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td width="5%">
                                            
                                        </td>
                                        <td width="45%">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="Label1" runat="server"  Text="Target Inc on 80%"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtTarget1" runat="server" Width="200px"></asp:TextBox>    
                                        <cc1:FilteredTextBoxExtender ID="txtTarget1_FilteredTextBoxExtender" runat="server" TargetControlID="txtTarget1"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td width="5%">
                                            
                                        </td>
                                        <td width="45%">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="Label9" runat="server"  Text="Target Inc on 90%"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtTarget2" runat="server" Width="200px"></asp:TextBox>    
                                        <cc1:FilteredTextBoxExtender ID="txtTarget2_FilteredTextBoxExtender" runat="server" TargetControlID="txtTarget2"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td width="5%">
                                            
                                        </td>
                                        <td width="45%">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="Label5" runat="server"  Text="Target Inc on 100%"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtTarget3" runat="server" Width="200px"></asp:TextBox>    
                                        <cc1:FilteredTextBoxExtender ID="txtTarget3_FilteredTextBoxExtender" runat="server" TargetControlID="txtTarget3"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td width="5%">
                                            
                                        </td>
                                        <td width="45%">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <asp:Label ID="Label8" runat="server"  Text="Target Inc on above 100%"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtTarget4" runat="server" Width="200px"></asp:TextBox>    
                                        <cc1:FilteredTextBoxExtender ID="txtTarget4_FilteredTextBoxExtender" runat="server" TargetControlID="txtTarget4"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td width="5%">
                                            
                                        </td>
                                        <td width="45%">
                                        
                                        </td>
                                    </tr>
                                </tbody>
                            </table>                            
                        </td>
                    </tr>
                </table>                 
                <table style="margin-top:10px;">
                    <tr>
                    <td width="5px"></td>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">
                                <asp:Image ID="imgTier" runat="server" ImageUrl="../images/btn-save.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <asp:Panel ID="pnlTierGrid" runat="server">
                    <asp:LinkButton ID="btnShowpnlTierContent" OnClick="btnShowpnlTierContent_Click" runat="server">Add New Tier</asp:LinkButton>
                <asp:Repeater ID="rptTier" runat="server" OnItemCommand="rptTier_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">
                                    <td width="30%">
                                        Name
                                    </td>
                                    <td width="20%">
                                         Bike Maintenance
                                    </td>
                                    <td width="20%">
                                        Fuel
                                    </td>
                                    <td width="20%">
                                       Commission
                                    </td>                             
                                    <td width="10%" colspan="2">
                                        Action
                                    </td>
                                </tr>
                            </thead>
                            <tbody id="table1">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                             <%# Eval("TIER_NAME")%>                             
                            </td>
                            <td>
                             <%# Eval("BIKE_MAINTENANCE")%>
                            </td>
                             <td>
                             <%# Eval("FUEL")%>
                            </td>
                            <td>
                                 <%# Eval("COMMISSION")%>                                
                            </td>                                                        
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("TIER_ID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("TIER_ID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            Delete
                                </asp:LinkButton>
                            </td>                      
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                         <tr style="background-color:#E4E4E4">
                            <td>
                             <%# Eval("TIER_NAME")%>                             
                            </td>
                            <td>
                             <%# Eval("BIKE_MAINTENANCE")%>
                            </td>
                             <td>
                             <%# Eval("FUEL")%>
                            </td>
                            <td>
                                 <%# Eval("COMMISSION")%>                                
                            </td>                                                        
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("TIER_ID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("TIER_ID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            Delete
                                </asp:LinkButton>
                            </td>                     
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
