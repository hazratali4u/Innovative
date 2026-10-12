<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVoucherApprove.aspx.cs" Inherits="Forms_frmVoucherApprove" Title="Voucher Approve" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                <tr>
                                    <td class="tblhead" style="width: 10%;">
                                        Voucher No
                                    </td>
                                    <td class="tblhead" style="width: 10%;">
                                        Voucher Date
                                    </td>
                                    <td class="tblhead" style="width: 10%;">
                                        Amount
                                    </td>
                                    <td class="tblhead" style="width: 10%;">
                                        Cheque No
                                    </td>
                                    <td class="tblhead" style="width: 20%;">
                                        Remarks
                                    </td>
                                    <td class="tblhead" style="width: 10%;">
                                        User
                                    </td>
                                    <td class="tblhead" style="width: 5%;">
                                        View
                                    </td>
                                    <td class="tblhead" style="width: 30%;" align="center">
                                        Action
                                    </td>
                                </tr>
                            </table>
                            <asp:Repeater ID="rVoucher" runat="server" OnItemCommand="rVoucher_ItemCommand" OnItemDataBound="rVoucher_ItemDataBound">
                                <ItemTemplate>
                                    <div style="background: #EFF3FB">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lblVoucher" runat="server" Text='<%# Eval("VOUCHER_NO")%>'></asp:Label>
                                                </td>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# ((DateTime)Eval("VOUCHER_DATE")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                </td>
                                                <td style="width: 10%;">
                                                    <%# Eval("DEBIT","{0:0.00}")%>
                                                </td>
                                                <td style="width: 10%;">
                                                    <%# Eval("CHEQUE_NO")%>
                                                </td>
                                                <td style="width: 20%;">
                                                    <%# Eval("Remarks")%>
                                                </td>
                                                <td style="width: 10%;">
                                                    <%# Eval("User")%>
                                                </td>
                                                <td style="width: 5%;">
                                                    <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("VOUCHER_NO") %>'
                                                        ToolTip="Edit">
                                                        View
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:RadioButtonList ID="rblAction" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="-1" Text="No Action"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="Approve"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Disapprove"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:HiddenField ID="hfDistributorID" runat="server" Value='<%# Eval("DISTRIBUTOR_ID")%>' />
                                                    <asp:HiddenField ID="hfVoucherTypeId" runat="server" Value='<%# Eval("VOUCHER_TYPE_ID")%>' />
                                                    <asp:HiddenField ID="hfIS_POSTED" runat="server" Value='<%# Eval("IS_POSTED")%>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <div style="background: #ffffff">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lblVoucher" runat="server" Text='<%# Eval("VOUCHER_NO")%>'></asp:Label>
                                                </td>
                                                <td style="width: 10%;">                                                    
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# ((DateTime)Eval("VOUCHER_DATE")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                </td>
                                                <td style="width: 10%;">
                                                    <%# Eval("DEBIT","{0:0.00}")%>
                                                </td>
                                                <td style="width: 10%;">
                                                    <%# Eval("CHEQUE_NO")%>
                                                </td>
                                                <td style="width: 20%;">
                                                    <%# Eval("Remarks")%>
                                                </td>
                                                <td style="width: 10%;">
                                                    <%# Eval("User")%>
                                                </td>
                                                <td style="width: 5%;">
                                                    <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("VOUCHER_NO") %>'
                                                        ToolTip="Edit">
                                                        View
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:RadioButtonList ID="rblAction" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="-1" Text="No Action"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="Approve"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Disapprove"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:HiddenField ID="hfDistributorID" runat="server" Value='<%# Eval("DISTRIBUTOR_ID")%>' />
                                                    <asp:HiddenField ID="hfVoucherTypeId" runat="server" Value='<%# Eval("VOUCHER_TYPE_ID")%>' />
                                                    <asp:HiddenField ID="hfIS_POSTED" runat="server" Value='<%# Eval("IS_POSTED")%>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="Button" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
    </div>
</asp:Content>
