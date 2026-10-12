<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptAreaWiseCustomerList.aspx.cs" Inherits="Forms_RptAreaWiseCustomerList"
    Title="Route Wise Customer List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }

    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td align="left" colspan="4">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="61px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>      
                                <tr>
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                </tr>                                                                                                                                                          
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" OnClick="btnViewPDF_Click"
                        CssClass="Button" Width="90" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" OnClick="btnViewExcel_Click"
                        CssClass="Button" Width="90" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
