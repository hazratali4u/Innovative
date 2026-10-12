<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmInwardGatePassView.aspx.cs" Inherits="Forms_frmInwardGatePassView" %>

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
                                <asp:Panel ID="pnEnquiryList" runat="server" Width="98%">
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Literal ID="ltrlEnquiry" runat="server"></asp:Literal>
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
                                                        <tr class="row-gray">
                                                            <th >
                                                                <td  style="width: 25%;">
                                                                    <%--Search Text--%>
                                                                   
                                                                    <asp:DropDownList ID="ddlSearch" runat="server" Height="25px" Width="150px">
                                                                  
                                                                    </asp:DropDownList>
                                                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn" Visible="true" OnClick="btnFilter_Click" />
                                                                </td>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                    <asp:Repeater ID="rGatePass" runat="server" OnItemCommand="rGatePass_ItemCommand"
                                                        OnItemDataBound="rGatePass_ItemDataBound">
                                                       <HeaderTemplate>
                                                        <table  width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr class="row-gray">
                                                            <td  style="width: 3%;">
                                                             <strong>   ID </strong>
                                                            </td>
                                                            <td  style="width: 13%;">
                                                               <strong> Type </strong>
                                                            </td>
                                                          
                                                            <td  style="width: 13%;">
                                                               <strong> Supplier </strong>
                                                            </td>
                                                          
                                                            <td  style="width: 10%;">
                                                                <strong> Document Date </strong>
                                                            </td>
                                                            <td  style="width: 15%;">
                                                                <strong> Products </strong>
                                                            </td>
                                                            <td  style="width: 10%;">
                                                               <strong>  Updated By </strong>
                                                            </td>
                                                            <td colspan="2"  style="width: 7%;">
                                                            <strong> Actions </strong>
                                                            </td>
                                                        </tr>
                                                       </HeaderTemplate>
                                                        <ItemTemplate>
                                                          
                                                                    <tr>
                                                                        <td style="width: 3%; border:1px solid silver;">
                                                                            <%# Eval("TGP_ID")%>
                                                                        </td>
                                                                        <td style="width: 13%; border:1px solid silver;">
                                                                            <%# Eval("TYPE_NAME")%>
                                                                        </td>
                                                                        <td style="width: 13%; border:1px solid silver;">
                                                                            <%# Eval("SUPPLIER_NAME")%>
                                                                        </td>
                                                                       
                                                                        <td style="width: 10%; border:1px solid silver;">
                                                                            <%# ((DateTime)Eval("DOCUMENT_DATE")).ToString("dd/MMM/yyyy")%>
                                                                        </td>
                                                                        <td style="width: 15%; border:1px solid silver;">
                                                                            <%# Eval("NoOfProducts")%>
                                                                        </td>
                                                                        <td style="width: 10%; border:1px solid silver;">
                                                                            <%# Eval("USER_ID")%>
                                                                        </td>
                                                                        <td style="width: 2%; border:1px solid silver;" align="center">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("TGP_ID") %>'
                                                                                ToolTip="View GatePass">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 5%; border:1px solid silver;" align="center">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("TGP_ID") %>'
                                                                                ToolTip="View Report" >View Report
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </table>
                                                        </FooterTemplate>
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

