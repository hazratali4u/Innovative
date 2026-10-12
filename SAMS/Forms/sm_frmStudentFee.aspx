<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="sm_frmStudentFee.aspx.cs" Inherits="Forms_frmStudentFee" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <asp:UpdatePanel ID="up_head" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="Label28" runat="server" Text="Program Study"></asp:Label></strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpStudyPrograms" runat="server" Width="200px" AutoPostBack="true"
                                    OnSelectedIndexChanged="DrpStudyPrograms_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="Label32" runat="server" Text="Session"></asp:Label></strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpSession" runat="server" Width="120px" AutoPostBack="true"
                                    OnSelectedIndexChanged="DrpSession_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 85px;">
                                <strong>Search</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="150px" OnTextChanged="txtSearch_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" valign="baseline">
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
                                                    <asp:GridView ID="grd_Student" runat="server" AutoGenerateColumns="false" Width="240"
                                                        HeaderStyle-BackColor="SteelBlue">
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
                                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </fieldset>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <table style="vertical-align: top;">
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="grd_Accounts" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="steelblue">
                                                <Columns>
                                                    <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ACCOUNT_NAME" HeaderStyle-ForeColor="White" HeaderText="ACCOUNT NAME">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Fee Amount">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAccountFee" runat="server" Text='<%#Eval("FEE_VALUE")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Text="Fine"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFine" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Text="Per Day Fine Till"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFineTill" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Text="Per Day Fine From"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFineFrom" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label9" runat="server" Text="Last Date of Payment"></asp:Label>
                                            </strong>
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtPaymentDate" runat="server" Width="158px" Skin="Outlook"
                                                ShowAnimation-Type="Slide" ToolTip="Open the Calender popup." CssClass>
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <table>
                <tr>
                    <td style="width: 400px;" align="right">
                        <asp:Button ID="btn_Save" runat="server" CssClass="Button" OnClick="btn_Save_Click"
                            Text="Save" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="up_hiddenfld" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hf_FeeID" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
