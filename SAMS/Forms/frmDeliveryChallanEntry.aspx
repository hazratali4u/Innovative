<%@ Page Title="Delivery Challan" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDeliveryChallanEntry.aspx.cs" Inherits="Forms_frmDeliveryChallanEntry" %>

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
                                                <td>
                                                   <asp:Label ID="ltrAdd" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                        <tr>
                                                            <td class="tblhead" style="width: 5%;">                                                                
                                                                 <asp:Label ID="lblID" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="DC ID" Width="100%" BackColor="#006699"></asp:Label>
                                                            </td>
                                                            <td class="tblhead" style="width: 5%;">                                                                
                                                                 <asp:Label ID="Label1" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="SO ID" Width="100%" BackColor="#006699"></asp:Label>
                                                            </td>
                                                             <td class="tblhead" style="width: 15%;">                                                                
                                                                <asp:Label ID="lblLocation" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="Location" Width="100%" BackColor="#006699"></asp:Label>
                                                            </td>
                                                            <td class="tblhead" style="width: 20%;">                                                                
                                                                <asp:Label ID="lblCustomer" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="Customer" Width="100%" BackColor="#006699"></asp:Label>
                                                            </td>
                                                            <td class="tblhead" style="width: 10%;">                                                                
                                                                <asp:Label ID="lblDocumentDate" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="Document Date" Width="100%" BackColor="#006699"></asp:Label>
                                                            </td>
                                                            <td class="tblhead" style="width: 10%;">                                                                
                                                                <asp:Label ID="lblOrderQty" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="Ordered Qty" Width="100%" BackColor="#006699"></asp:Label>
                                                            </td>
                                                            <td class="tblhead" style="width: 10%;">                                                                
                                                                <asp:Label ID="lblReceivedQty" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="Received Qty" Width="100%" BackColor="#006699"></asp:Label>
                                                            </td>
                                                            <td class="tblhead" style="width: 15%;">                                                                
                                                                <asp:Label ID="lblUserName" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="User Name" Width="100%" BackColor="#006699"></asp:Label>
                                                            </td>                                                            
                                                            <td class="tblhead" style="width: 10%;">                                                                
                                                                <asp:Label ID="lblAction" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                                            Text="Action" Width="100%" BackColor="#006699"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Repeater ID="rPurchaseOrder" runat="server" OnItemCommand="rPurchaseOrder_ItemCommand">
                                                        <ItemTemplate>
                                                            <div style="background: #EFF3FB">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                       <td style="width: 5%;">
                                                                            <%# Eval("ORDER_NUMBER")%>
                                                                        </td>
                                                                         <td style="width: 5%;">
                                                                            <%# Eval("PO_ID")%>
                                                                        </td>
                                                                         <td style="width: 15%;">
                                                                            <%# Eval("DISTRIBUTOR_NAME")%>
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <%# Eval("SKU_HIE_NAME")%>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <%# Eval("DOCUMENT_DATE")%>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <%# Eval("OrdereQty")%>
                                                                        </td>
                                                                         <td style="width: 10%;">
                                                                            <%# Eval("ReceivedQty")%>
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <%# Eval("USER_NAME")%>
                                                                        </td>                                                                       
                                                                        <td style="width: 10%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("PURCHASE_MASTER_ID") %>'
                                                                                ToolTip="View" Text="View">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <div style="background: #ffffff">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                        <td style="width: 5%;">
                                                                            <%# Eval("ORDER_NUMBER")%>
                                                                        </td>
                                                                         <td style="width: 5%;">
                                                                            <%# Eval("PO_ID")%>
                                                                        </td>
                                                                         <td style="width: 15%;">
                                                                            <%# Eval("DISTRIBUTOR_NAME")%>
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <%# Eval("SKU_HIE_NAME")%>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <%# Eval("DOCUMENT_DATE")%>
                                                                        </td>
                                                                        <td style="width: 10%;">
                                                                            <%# Eval("OrdereQty")%>
                                                                        </td>
                                                                          <td style="width: 10%;">
                                                                            <%# Eval("ReceivedQty")%>
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                            <%# Eval("USER_NAME")%>
                                                                        </td>                                                                       
                                                                        <td style="width: 10%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("PURCHASE_MASTER_ID") %> '
                                                                                ToolTip="View" Text="View">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right" width="56%">
                                                                <asp:LinkButton ID="linkbtnprev" runat="server" Enabled="False" OnClick="linkbtnprev_Click"
                                                                    CausesValidation="false" Visible="false" Style="color: #6C5A10;">Previous</asp:LinkButton>
                                                                &nbsp;<asp:LinkButton ID="linkbtnnext" runat="server" Enabled="False" OnClick="linkbtnnext_Click"
                                                                    CausesValidation="false" Visible="false" Style="color: #6C5A10;">Next</asp:LinkButton>
                                                            </td>
                                                            <td align="right" width="44%">
                                                                <asp:Label ID="lblCurrentPageNo" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                                <asp:Label ID="lblOf" runat="server" Text="Of" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                                <asp:Label ID="lblTotalNoOfPages" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                                <asp:Label ID="lblDummy" runat="server" Text="| Total Records:" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                                <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
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
