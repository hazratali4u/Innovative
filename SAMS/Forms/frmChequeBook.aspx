<%@ Page Title="Cheque Book Entry" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmChequeBook.aspx.cs" Inherits="Forms_frmChequeBook" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphPage">
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
</script>
    <div id="right_data">
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
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                                CancelControlID="CancelButton" DropShadow="true" PopupControlID="pnlChequeBookDetail"
                                                TargetControlID="dummyLink" BehaviorID="ModelPopup">
                                            </ajaxToolkit:ModalPopupExtender>
                                            <asp:Panel ID="pnlChequeBookDetail" runat="server" Style="display: none; background-color: White;"
                                                ScrollBars="Vertical" Height="500px" Width="100%">
                                                <table width="100%">
                                                        <tr>
                                                            <td style="width: 95%">
                                                                <table width="100%">                                                                                                                                      
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="pnlDetail" runat="server" Width="550px" Height="100%" ScrollBars="Vertical">
                                                                                <asp:GridView ID="gvBookDetail" runat="server" Width="100%" ForeColor="SteelBlue" OnRowCommand="gvBookDetail_RowCommand"
                                                                            BorderColor="White" HorizontalAlign="Center" BackColor="White" AutoGenerateColumns="False">
                                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                                PreviousPageText="Previous" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="CHEQUE_BOOK_DETAIL_ID" HeaderText="CHEQUE_BOOK_DETAIL_ID">
                                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                                    <ItemStyle CssClass="HidePanel" />
                                                                                </asp:BoundField> 
                                                                                <asp:BoundField DataField="CHEQUE_BOOK_ID" HeaderText="CHEQUE_BOOK_ID">
                                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                                    <ItemStyle CssClass="HidePanel" />
                                                                                </asp:BoundField>                                                                                
                                                                                <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No">
                                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="50%" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Status" HeaderText="Status">
                                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="25%" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Cancel Cheque">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkCancel" runat="server" ToolTip="Cancel Cheque" CommandName="cancels" CommandArgument='<%# Eval("CHEQUE_BOOK_ID")%>'>
                                                                                            Cancel
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="25%"/>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td align="right" valign="bottom" style="width: 5%">
                                                                <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink"
                                                                    runat="server">dummy</a>
                                                                <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/App_Themes/Granite/Images/closelabel.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90%">
                                            <fieldset style="width: 73%">
                                                <table width="100%">                                                    
                                                    <tr>
                                                        <td style="width: 15%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblBank" runat="server" Text="Bank:"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:DropDownList ID="ddlBank" runat="server" Width="200px" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlBank_SelectedIndexChanged" TabIndex="0">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hfChequeBookID" runat="server" />
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 15%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblAccNo" runat="server" Text="Account No:"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 37%">                                                            
                                                            <asp:TextBox ID="txtAccNo" runat="server" Width="200px" TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblStart" runat="server" Text="Cheque Start No:"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:TextBox ID="txtStart" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 15%" align="right">
                                                            <strong>
                                                                <asp:Label ID="lblEnd" runat="server" Text="Cheque End No:"></asp:Label></strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 37%">                                                            
                                                            <asp:TextBox ID="txtEnd" runat="server" Width="200px" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <legend></legend>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90%">
                                            <fieldset style="width: 73%">
                                                <table width="100%">                                                    
                                                    <tr>
                                                        <td style="width:5%">
                                                        &nbsp;
                                                        </td>
                                                        <td style="width:95%">
                                                        <asp:Panel ID="pnlGrid" runat="server" Width="750px" Height="200px" ScrollBars="Vertical">
                                                            <asp:GridView ID="gvBook" runat="server" Width="100%" ForeColor="SteelBlue" OnRowCommand="gvBook_RowCommand"
                                                        BorderColor="White" HorizontalAlign="Center" BackColor="White" AutoGenerateColumns="False">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CHEQUE_BOOK_ID" HeaderText="CHEQUE_BOOK_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ACCOUNT_NO" HeaderText="Acc No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="25%" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="START_NO" HeaderText="Start No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="25%"/>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="END_NO" HeaderText="End No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="25%"/>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField> 
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" ToolTip="Edit" CommandName="edits" CommandArgument='<%# Eval("CHEQUE_BOOK_ID")%>'>
                                                                        Edit
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                 <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="10%"/>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>                                                           
                                                            <asp:TemplateField HeaderText="View Detail">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkView" runat="server" ToolTip="View Detail" CommandName="view" CommandArgument='<%# Eval("CHEQUE_BOOK_ID")%>'>
                                                                        View Detail
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                 <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="15%"/>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                        </asp:Panel>
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
                                            <asp:Button AccessKey="S" ID="btnSave" TabIndex="4" Width="100px" 
                                                runat="server" Font-Size="8pt" Text="Save" CssClass="Button" 
                                                onclick="btnSave_Click" />
                                            &nbsp;&nbsp;
                                            <asp:Button AccessKey="C" ID="btnCancel" TabIndex="5" Width="100px" 
                                                runat="server" Font-Size="8pt" Text="Cancel" CssClass="Button" 
                                                onclick="btnCancel_Click" />
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
