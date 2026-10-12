<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmAssets.aspx.cs" Inherits="pr_frmAssets" Title="Assets" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<%@ Register src="UserControl/MultiCheckCombo.ascx" tagname="MultiCheckCombo" tagprefix="uc1" %>
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
                <asp:Panel ID="pnlAssetsContent" runat="server">
                <table width="100%">
                    <tr>
                        <td width="5px">
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label3" runat="server" Text="AssetType"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:RequiredFieldValidator ID="ddlAssetType_RequiredFieldValidator" InitialValue="0" runat="server" ControlToValidate="ddlAssetType" ErrorMessage="Select Asset Type" ValidationGroup="grpAssets"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlAssetType"   runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td width="50%">
                                            <asp:HiddenField ID="hdnAssetID" runat="server" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table> 
                <hr />
                <table width="100%">
                    <tr>
                        <td width="5px">
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                <tr>
                                <td height="20px">
                                </td>
                                </tr>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="lblRegistration" runat="server" Text="Reg. No"></asp:Label>
                                        </td>
                                        <td width="30%">
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Registration No" ControlToValidate="txtReg" ID="txReg_RequiredFieldValidator" ValidationGroup="grpAssets"></asp:RequiredFieldValidator>
                                          <br />
                                            <asp:TextBox ID="txtReg" runat="server" Width="200px"></asp:TextBox>
                                        </td >
                                        <td width="10%">
                                            
                                        </td>
                                        <td width="50%">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Make"></asp:Label>
                                        </td>
                                        <td>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Enter Make" ControlToValidate="txtMake" ID="txtMake_RequiredFieldValidator" ValidationGroup="grpAssets"></asp:RequiredFieldValidator>
                                          <br />
                                            <asp:TextBox ID="txtMake" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td >
                                            <asp:Label ID="Label2" runat="server"   Text="Amount"></asp:Label>
                                        </td>
                                        <td>
                                        <cc1:FilteredTextBoxExtender ID="txtAmount_FilteredTextBoxExtender" runat="server" TargetControlID="txtAmount"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                         <asp:RequiredFieldValidator ErrorMessage="Enter Amount" runat="server" ControlToValidate="txtAmount" ID="RequiredFieldValidator_txtAmount" ValidationGroup="grpAssets"></asp:RequiredFieldValidator>
                                         <br />
                                         <asp:TextBox ID="txtAmount" runat="server" Width="200px"></asp:TextBox>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Model"></asp:Label>
                                        </td>
                                        <td>
                                         <asp:TextBox ID="txtModel" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td >
                                            <asp:Label ID="Label6" runat="server"  Text="Color" ></asp:Label>
                                        </td>
                                        <td >                                         
                                            <asp:TextBox ID="txtColor" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="EngineNo"></asp:Label>
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtEngineNo" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td>                                        
                                            <asp:Label ID="Label8" runat="server"  Text="ChassisNo"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:TextBox ID="txtChassisNo" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                    <asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="473px" Height="50px"></asp:TextBox>
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
                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" ValidationGroup="grpAssets">
                            <asp:Image ID="imgAssets" runat="server" ImageUrl="../images/btn-save.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <asp:Panel ID="pnlAssetsGrid" runat="server">
                <asp:LinkButton ID="btnShowpnAssetsContent" OnClick="btnShowpnAssetsContent_Click" runat="server">Add New Assets</asp:LinkButton>
                <asp:Repeater ID="rptAssets" runat="server" OnItemCommand="rptAssets_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">
                                    <td width="15%">
                                        Reg. No
                                    </td>
                                    <td width="15%">
                                         Make
                                    </td>
                                    <td width="15%">
                                        Model
                                    </td>
                                    <td width="15%">
                                       Engine No
                                    </td>
                                     <td width="15%">
                                       Color
                                    </td>
                                    <td width="15%">
                                        Amount
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
                             <%# Eval("RegNo")%>                             
                            </td>
                            <td>
                             <%# Eval("Make")%>
                            </td>
                             <td>
                             <%# Eval("Model")%>
                            </td>
                            <td>
                                 <%# Eval("EngineNo")%>                                
                            </td>
                             <td>
                                 <%# Eval("Color")%>                                  
                            </td> 
                            <td>
                                 <%# Eval("Amount")%>                                  
                            </td>     
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("AssetID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("AssetID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            Delete
                                </asp:LinkButton>
                            </td>                       
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                         <tr style="background-color:#E4E4E4">
                            <td>
                             <%# Eval("RegNo")%>                             
                            </td>
                            <td>
                             <%# Eval("Make")%>
                            </td>
                             <td>
                             <%# Eval("Model")%>
                            </td>
                            <td>
                                 <%# Eval("EngineNo")%>                                
                            </td>
                             <td>
                                 <%# Eval("Color")%>                                  
                            </td>  
                             <td>
                                 <%# Eval("Amount")%>                                  
                            </td>   
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("AssetID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>                  
                             <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("AssetID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
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
