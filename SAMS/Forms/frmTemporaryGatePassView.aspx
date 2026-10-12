<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmTemporaryGatePassView.aspx.cs" Inherits="Forms_frmTemporaryGatePassView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
    <style type="text/css">
        .open div
        {
            display: none;
            float: left;
            position: absolute;
            padding-top: 10px;
            padding-left: 5px;
            padding-right: 10px;
        }
        .open:hover div
        {
            display: block;
        }
        .open div a
        {
            display: block;
            height: 25px;
        }
        .open div a:hover
        {
            text-decoration: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <div>
            <table width="98%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnEnquiryList" runat="server" Width="99%">
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Literal ID="ltrlTGP" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="ltrAdd" runat="server"></asp:Label>
                                                    <%--<a href="sm_frmStudentFee.aspx?LevelType=3&LevelID=150">Add New Fee</a> --%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table width="100%" border="0px" cellspacing="0"  cellpadding="2">
                                                        <tr class="row-gray">
                                                            <td  style="width: 5%;">
                                                               <strong> ID </strong>
                                                            </td>
                                                            <td  colspan="3" style="width: 13%;">
                                                              <strong>  Supplier </strong>
                                                            </td>
                                                            <td  style="width: 10%;">
                                                               <strong> Goods out through </strong>
                                                            </td>
                                                            <td  style="width: 10%;">
                                                               <strong> Return Date </strong>
                                                            </td>
                                                            <td  style="width: 15%;">
                                                               <strong> Products </strong>
                                                            </td>
                                                            <td  style="width: 10%;">
                                                               <strong> Updated By </strong>
                                                            </td>
                                                            <td  colspan="3" style="width: 11%;">
                                                           <strong> Actions </strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <asp:Repeater ID="rGatePass" runat="server" OnItemCommand="rGatePass_ItemCommand"
                                                        OnItemDataBound="rGatePass_ItemDataBound">
                                                        <ItemTemplate>
                                                            <div >
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                        <td style="width: 5%;border:1px solid silver;">
                                                                            <%# Eval("TGP_ID")%>
                                                                        </td>
                                                                        <td style="width: 13%; border:1px solid silver;">
                                                                            <%# Eval("SUPPLIER_NAME")%>
                                                                        </td>
                                                                        <td style="width: 10%; border:1px solid silver;">
                                                                            <%# Eval("GOODS_OUT_THROUGH")%>
                                                                        </td>
                                                                        <td style="width: 10%; border:1px solid silver;">
                                                                            <%# ((DateTime)Eval("RETURN_DATE")).ToString("dd/MM/yyyy, hh:mm tt")%>
                                                                        </td>
                                                                        <td style="width: 15%; border:1px solid silver;">
                                                                            <%# Eval("NoOfProducts")%>
                                                                        </td>
                                                                        <td style="width: 10%; border:1px solid silver;">
                                                                            <%# Eval("USER_ID")%>
                                                                        </td>
                                                                        <td style="width: 1%; border:1px solid silver;" align="center">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("TGP_ID") %>'
                                                                                ToolTip="View GatePass">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 6%; border:1px solid silver;" align="center">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("TGP_ID") %>'
                                                                                ToolTip="View Report">View Report
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 1%; border:1px solid silver;" align="center">
                                                                            <asp:LinkButton ID="btnClose" runat="server" CommandName="close" CommandArgument='<%# Eval("TGP_ID") %>'
                                                                                ToolTip="Close TGP">Close
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <div >
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                        <td style="width: 5%; border:1px solid silver;">
                                                                            <%# Eval("TGP_ID")%>
                                                                        </td>
                                                                        <td style="width: 13%; border:1px solid silver;">
                                                                            <%# Eval("SUPPLIER_NAME")%>
                                                                        </td>
                                                                        <td style="width: 10%; border:1px solid silver;">
                                                                            <%# Eval("GOODS_OUT_THROUGH")%>
                                                                        </td>
                                                                        <td style="width: 10%; border:1px solid silver;">
                                                                            <%# ((DateTime)Eval("RETURN_DATE")).ToString("dd/MM/yyyy, hh:mm tt")%>
                                                                        </td>
                                                                        <td style="width: 15%; border:1px solid silver;">
                                                                            <%# Eval("NoOfProducts")%>
                                                                        </td>
                                                                        <td style="width: 10%; border:1px solid silver;">
                                                                            <%# Eval("USER_ID")%>
                                                                        </td>
                                                                        <td style="width: 1%; border:1px solid silver;" align="center">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("TGP_ID") %>'
                                                                                ToolTip="View GatePass">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 6%; border:1px solid silver;" align="center">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("TGP_ID") %>'
                                                                                ToolTip="View Report">View Report
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 1%; border:1px solid silver;" align="center">
                                                                            <asp:LinkButton ID="btnClose" runat="server" CommandName="close" CommandArgument='<%# Eval("TGP_ID") %>'
                                                                                ToolTip="Close TGP">Close
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
                                                                <asp:LinkButton ID="linkbtnprev" runat="server" OnClick="linkbtnprev_Click" CausesValidation="false"
                                                                    Visible="false" Style="color: #6C5A10;">Previous</asp:LinkButton>
                                                                &nbsp;<asp:LinkButton ID="linkbtnnext" runat="server" OnClick="linkbtnnext_Click"
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
                                        </tbody>
                                    </table>
                                </asp:Panel>
                                <asp:HiddenField ID="hfDateType" Value="D" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 10px;
                            height: 50px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update</ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
        </div>
    </div>
</asp:Content>
