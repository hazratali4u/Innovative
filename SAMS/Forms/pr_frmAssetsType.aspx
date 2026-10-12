<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pr_frmAssetsType.aspx.cs" Inherits="pr_frmAssetsType"
    MasterPageFile="~/Forms/PageMaster.master" Title="Assets Type" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
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
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                            <table width="100%">
                                                <tbody>
                                                    <tr>                                                       
                                                        <td>
                                                        <asp:Panel ID="pnlAssetsTypeContent" runat="server">                                                        
                                                            <table width="100%">
                                                                <tbody>                                                                
                                                                 <tr>
                                                                <td width="10%">
                                                                    <label>
                                                                            Assets Type
                                                                            <asp:HiddenField ID="hdnAssetsTypeID" runat="server" />
                                                                    </label>
                                                                </td>
                                                                <td width="30%" align="left">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtAssetsTypeName" ValidationGroup="grpAssetsType" runat="server" ControlToValidate="txtAssetsTypeName" ErrorMessage="Enter Assets Typee"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtAssetsTypeName" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td width="10%">
                                                                </td>
                                                                <td width="50%">
                                                                </td>
                                                            </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:LinkButton ID="btnSaveAssetsType" ValidationGroup="grpAssetsType" runat="server" OnClick="btnSaveAssetsType_Click" > 
                                                                                <asp:Image ID="imgAssetsType" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnDiscardAssetsType" runat="server"  OnClick="btnDiscardAssetsType_Click">Discard</asp:LinkButton>
                                                                        </td>
                                                                        <td width="10%"></td>
                                                                        <td width="50%"></td>
                                                                        </tr>                                                                       
                                                                    
                                                                </tbody>
                                                            </table>
                                                            </asp:Panel>
                                                        <asp:Panel ID="pnlAssetsTypeGrid" runat="server">
                                                            <asp:LinkButton ID="btnShowpnlAssetsTypeContent" OnClick="btnShowpnlAssetsTypeContent_Click" runat="server">Add New Assets Type</asp:LinkButton>
                                                            <asp:Repeater ID="rptAssetsType" runat="server" OnItemCommand="rptAssetsType_ItemCommand">

                                                          <HeaderTemplate>
                                                               <table style="width: 50%; margin-top: 0px">
                                                                  <tr class="tblheading">
                                                                        <td>
                                                                            Assets Type
                                                                        </td>          
                                                                        <td colspan="2">
                                                                            Action
                                                                        </td>                                                              
                                                                    </tr>
                                                             </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <tr>
                                                                      <td>
                                                                          <%# Eval("AssetTypeName")%>                                                                          
                                                                      </td>      
                                                                      <td>
                                                                        <asp:LinkButton ID="btnEditAssetsType" CommandName="edit" CommandArgument='<%# Eval("AssetTypeID")%>' runat="server" ClientIDMode="AutoID">
                                                                                     Edit
                                                                          </asp:LinkButton>
                                                                      </td>                                                                                                                                           
                                                                      <td>
                                                                      <asp:LinkButton ID="btnDeleteAssetsType" CommandName="delete" CommandArgument='<%# Eval("AssetTypeID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                          </asp:LinkButton>
                                                                      </td>                                                                                                                                           
                                                            
                                                                  </tr>
                                                              </ItemTemplate>
                                                             
                                                             <AlternatingItemTemplate>
                                                             <tr style="background-color:#E4E4E4">
                                                                         <td>
                                                                            <%# Eval("AssetTypeName")%>
                                                                      </td>     
                                                                       <td>
                                                                        <asp:LinkButton ID="btnEditAssetsType" CommandName="edit" CommandArgument='<%# Eval("AssetTypeID")%>' runat="server" ClientIDMode="AutoID">
                                                                                     Edit
                                                                          </asp:LinkButton>
                                                                      </td>                                                                                                                                           
                                                                      <td>
                                                                      <asp:LinkButton ID="btnDeleteAssetsType" CommandName="delete" CommandArgument='<%# Eval("AssetTypeID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                          </asp:LinkButton>
                                                                      </td>                                                                         
                                                                  </tr>
                                                             </AlternatingItemTemplate>
                                                             <FooterTemplate>
                                                              </table>
                                                             </FooterTemplate>
                                                               </asp:Repeater>
                                                               </asp:Panel>
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
