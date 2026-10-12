<%@ Page Title="Goods Receipt Note [GRN]" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseReceivedEntryAdd.aspx.cs" Inherits="Forms_frmPurchaseReceivedEntryAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">

    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
                                            </strong>                                                
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:TextBox ID="txtDate" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                            <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" EnableViewState="False"
                                                Format="dd-MMM-yyyy" PopupButtonID="ibDate" TargetControlID="txtDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text="P.O. Ref #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" Text="Purchase For:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Transfer To:" Visible="False"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:DropDownList ID="DrpTransferFor" runat="server" Visible="False" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="left: 10px; position: absolute; top: 345px">
                                                <asp:Panel ID="Panel1" runat="server" BackColor="Silver" BorderColor="Silver" BorderStyle="Groove"
                                                    BorderWidth="1px" CssClass="HidePanel" Height="170px" Width="250px">
                                                    SKU List<br />
                                                    <asp:ListBox ID="lstCode" runat="server" Height="151px" onkeyup="SelectCode(event)"
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
                                                                            Font-Bold="True" Text="Received Quantity" BackColor="#006699"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 15%; height: 30px;" align="center">
                                                                        <asp:Label ID="Label14" runat="server" Width="100%" Height="100%" ForeColor="White"
                                                                            Font-Bold="True" Text="Current Received" BackColor="#006699"></asp:Label>
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
                                                            <asp:BoundField DataField="Quantity" HeaderText="Ord Qty">
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
                                                    Text="Save Document" UseSubmitBehavior="TRUE" OnClick="btnSaveDocument_Click"
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
    </div>
</asp:Content>
