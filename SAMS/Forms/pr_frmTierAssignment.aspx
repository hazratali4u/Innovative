<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmTierAssignment.aspx.cs" Inherits="Forms_pr_frmTierAssignment" Title="Tier Assignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
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
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
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
                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="2" align="left">
                                                <asp:Label ID="lblmsg" runat="server" Visible="False" Width="175px" ForeColor="Red"></asp:Label>&nbsp;
                                            </td>
                                            <td style="height: 17px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="lblTier" runat="server" Width="58px" Text="Tier"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlTier" runat="server" Width="200px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlTier_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtTier" runat="server" Visible="False" Width="195px" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td style="height: 10px">                                            
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="58px" Text="Region"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                 <asp:DropDownList ID="DrpTownRegion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpTownRegion_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 10px">
                                            
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="58px" Text="Zone"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                 <asp:DropDownList ID="DrpTownZone" runat="server" AutoPostBack="True" CssClass="DropList" OnSelectedIndexChanged="DrpTownZone_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 10px">
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="58px" Text="Territory"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                 <asp:DropDownList id="DrpTerritory" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DrpTerritory_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 10px">
                                            
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
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 100px" rowspan="1">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="142px" Text="Unassigned Town" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 102px" valign="bottom" align="center">
                                            </td>
                                            <td style="width: 102px" rowspan="1">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Text="Assigned Town"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" rowspan="4">
                                                <asp:ListBox ID="lstUnAssignTown" runat="server" Width="150px" Height="200px">
                                                </asp:ListBox>
                                            </td>
                                            <td valign="middle" align="center">
                                                <asp:Button ID="btnAssign" runat="server" Width="30px" Font-Size="8pt" Text=">" OnClick="btnAssign_Click"
                                                    CssClass="Button" />
                                            </td>
                                            <td style="width: 102px" rowspan="4">
                                                <asp:ListBox ID="lstAssignTown" runat="server" Width="150px" Height="200px">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnAssignAll" runat="server" Width="30px" Font-Size="8pt" Text=">>"
                                                    OnClick="btnAssignAll_Click" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnUnAssign" runat="server" Width="30px" Font-Size="8pt" Text="<<"
                                                    OnClick="btnUnAssign_Click" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="center">
                                                <asp:Button ID="btnUnAssignAll" runat="server" Width="30px" Font-Size="8pt" Text="<"
                                                    OnClick="btnUnAssignAll_Click" CssClass="Button" />
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
