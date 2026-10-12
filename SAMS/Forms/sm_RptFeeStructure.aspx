<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="sm_RptFeeStructure.aspx.cs" Inherits="Forms_RptFeeStructure" %>

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
                                            <asp:Label ID="lblProgram" runat="server" CssClass="lblbox" Text="Program" Width="73px"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="DrpStudyPrograms" runat="server" Width="200px" 
                                            CssClass="DropList" AutoPostBack="true" 
                                            onselectedindexchanged="DrpStudyPrograms_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblSession" runat="server" CssClass="lblbox" Text="Session" Width="78px"></asp:Label></strong>
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpSession" runat="server" Width="200px" 
                                            CssClass="DropList" AutoPostBack="true" 
                                            onselectedindexchanged="DrpSession_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <strong>Student Name</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearch" runat="server" Width="150px" placehholder="Plz enter Student Name"
                                            OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <fieldset>
                                            <legend>Student List</legend>
                                            <asp:Panel ID="pnl_student" runat="server" Height="320px" ScrollBars="Vertical" Width="270px">
                                                <table>
                                                    <tr valign="top">
                                                        <td>
                                                            <strong>
                                                                <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbAll_CheckedChanged"
                                                                    Text="Select All" Width="100px" />
                                                            </strong>
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td>
                                                            <asp:GridView ID="grd_Student" runat="server" AutoGenerateColumns="false"
                                                                 HeaderStyle-BackColor="SteelBlue" Width="240">
                                                               
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                                            Width="10px" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chb_student" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="STUDENT_ID" HeaderText="Student Id">
                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                        <ItemStyle CssClass="HidePanel" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="REG_NO" HeaderText="Reg No">
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="STUDENT_NAME" HeaderText="Name">
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <FooterStyle BackColor="White" />
                                                                <PagerStyle BackColor="Transparent" />
                                                                <HeaderStyle  Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                    VerticalAlign="Middle" />
                                                                <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp; &nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Width="90" Text="View PDF"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Width="90" Text="View Excel"
                        OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>
