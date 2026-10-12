<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pr_rptSalarySlip.aspx.cs" Inherits="pr_rptSalarySlip"
    MasterPageFile="~/Forms/PageMaster.master" Title="Leaves Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 1px" align="left" colspan="1">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="height: 25px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlLocation" runat="server" Width="210px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px;" align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Department" Width="94px"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="DropList" Width="209px" AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Employee" Width="61px"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="width: 203px; height: 25px">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="210px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="59px" Text="From Date" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="153px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Width="90" Text="View PDF"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" CssClass="Button"
                        Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
