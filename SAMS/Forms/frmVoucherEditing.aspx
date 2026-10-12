<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVoucherEditing.aspx.cs"
    Inherits="Forms_frmVoucherEditing" Title="DMS: Voucher Editing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnDone.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnDone.ClientID%>').disabled = false;

        }

        function ValidateForm() {
            var str;

           
            str = document.getElementById("<%= txtVoucherDate.ClientID %>").value;
            if (str == null || str.length < 9) {
                alert('Must Enter Voucher Date');
                return false;
            }
            var Debit = document.getElementById("<%= txtDebitAmount.ClientID %>").value;
            var Credit = document.getElementById("<%= txtCreditAmount.ClientID %>").value;

            if (Debit.length == 0 && Credit.length == 0) {
                alert('Debit or Credit Must Enter');
                return false;
            }

            if (Debit > 0 && Credit > 0) {
                alert('Only Debit or Credit Can Enter');
                return false;
            }
            return true;
        }
    </script>
</head>
<body style="background: White">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="VoucherScriptManager" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <h2>
                    Voucher Editing</h2>
            </td>
        </tr>
    </table>
    <table width="100%" style="font-size: 0.78em">
        <tr>
            <td style="height: 13px">
            </td>
            <td align="center" style="height: 13px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="height: 25px; width: 120px;">
                                    <strong>
                                        <asp:Label ID="Label4" runat="server" Text="Location"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 20px;">
                                    <asp:DropDownList ID="drpDistributor" runat="server" Width="206px" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                </td>
                                <td align="left" style="width: 99px">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <strong>
                                        <asp:Label ID="Label5" runat="server" Text="Voucher Type"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 20px;">
                                    <asp:DropDownList ID="DrpVoucherType" runat="server" CssClass="DropList" Enabled="False"
                                        Width="205px">
                                        <asp:ListItem Value="14">Cash Voucher</asp:ListItem>
                                        <asp:ListItem Value="15">Bank Voucher</asp:ListItem>
                                        <asp:ListItem Value="16">Journal Voucher</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="height: 18px">
                                    &nbsp;
                                </td>
                                <td align="left" style="height: 18px; width: 130px;">
                                    <strong>
                                        <asp:Label ID="Label16" runat="server" Text="Voucher Sub Type"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 18px">
                                    <asp:DropDownList ID="DrpVoucherSubType" runat="server" Enabled="False" Width="205px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <strong>
                                        <asp:Label ID="Label6" runat="server" Text="Voucher Date"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 20px;">
                                    <asp:TextBox ID="txtVoucherDate" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
                                </td>
                                <td align="left" style="height: 18px">
                                </td>
                                <td align="left" style="height: 18px; width: 99px;">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Text="Voucher No"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 18px">
                                    <asp:TextBox ID="lblVoucherNo" runat="server" CssClass="txtBox " ReadOnly="True"
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <strong>
                                        <asp:Label ID="Label9" runat="server" CssClass="lblbox" Text="Payment Mode"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 20px;">
                                    <asp:DropDownList ID="DrpPaymentMode" runat="server" Width="206px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                </td>
                                <td align="left" style="width: 99px">
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Due Date"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 20px">
                                    <asp:TextBox ID="txtDueDate" runat="server" onkeyup="BlockEndDateKeyPress()" ReadOnly="true"
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <strong>
                                        <asp:Label ID="lblChequeNo" runat="server" CssClass="lblbox" Text="Cheque No"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 20px;">
                                    <asp:TextBox ID="txtChequeNo" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                </td>
                                <td align="left" style="height: 20px">
                                </td>
                                <td align="left" style="height: 20px; width: 99px;">
                                    <strong>
                                        <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Slip No"></asp:Label></strong>
                                    &nbsp;
                                </td>
                                <td align="left" style="height: 20px">
                                    <asp:TextBox ID="txtSlipNo" runat="server" Width="200px"></asp:TextBox>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 25px;">
                                    <strong>
                                        <asp:Label ID="Label10" runat="server" CssClass="lblbox" Text="Payee's Name"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:TextBox ID="txtpayeesName" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:Label ID="Label14" runat="server" CssClass="lblbox" Width="10px"></asp:Label>
                                </td>
                                <td align="left" style="height: 25px; width: 99px;">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Narration" Width="85px"></asp:Label></strong>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " Width="200px" MaxLength="255"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div style="z-index: 101; left: 471px; width: 100px; position: absolute; top: 291px;
                            height: 100px">
                            <asp:Panel ID="Panel212" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="32px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="39px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:TextBox ID="txtTotalDebit" runat="server" CssClass="HidePanel" Width="100px"></asp:TextBox>
                        <asp:TextBox ID="txtTotalCredit" runat="server" CssClass="HidePanel" Width="73px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                &nbsp;
            </td>
            <td style="height: 13px">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 100px">
            </td>
            <td align="center">
                &nbsp;<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" style="border-right: #007395 1px solid; border-top: #007395 1px solid;
                            border-left: #007395 1px solid; border-bottom: #007395 1px solid">
                            <tr>
                                <td class="tblhead">
                                    <asp:Label ID="lblskuCode" runat="server" ForeColor="White" Font-Bold="True" Text="Account Description"></asp:Label>
                                </td>
                                <td class="tblhead">
                                    <asp:Label ID="lblskuname" runat="server" ForeColor="White" Font-Bold="True" Text="Remarks"></asp:Label>
                                </td>
                                <td class="tblhead">
                                    <asp:Label ID="Label15" runat="server" ForeColor="White" Font-Bold="True" Text="Supplier"></asp:Label>
                                </td>
                                <td class="tblhead">
                                    <asp:Label ID="Label11" runat="server" ForeColor="White" Font-Bold="True" Text="Debit"></asp:Label>
                                <td class="tblhead">
                                    <asp:Label ID="Label12" runat="server" ForeColor="White" Font-Bold="True" Text="Credit"></asp:Label>
                                </td>
                                <td class="tblhead">
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Width="70px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 12px">                                    
                                    <asp:DropDownList ID="ddlAccountHead" runat="server" Width="300">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 12px">
                                    <asp:TextBox id="txtAccountDes" runat="server" Width="140px"></asp:TextBox>
                                </td>
                                <td style="height: 12px">
                                    <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 12px">
                                    <asp:TextBox ID="txtDebitAmount" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td style="height: 12px">
                                    <asp:TextBox ID="txtCreditAmount" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                <td style="height: 12px">
                                </td>
                                <td style="width: 79px; height: 12px">
                                    <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="70px"
                                        Font-Size="8pt" Text="Add New" CssClass="Button" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="7" rowspan="1">
                                    <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" CaptionAlign="Left" ForeColor="SteelBlue" HorizontalAlign="Center"
                                        OnRowDataBound="GrdOrder_RowDataBound" OnRowDeleting="GrdOrder_RowDeleting" OnRowEditing="GrdOrder_RowEditing"
                                        ShowFooter="True" ShowHeader="False" Width="100%" Font-Size="X-Small">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" />
                                        <RowStyle ForeColor="Black" />
                                        <Columns>
                                            <asp:BoundField DataField="Account_Head_Id" HeaderText="Account Head Id">
                                                <FooterStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Account_Name" HeaderText="Account Name">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    Width="230px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Account_Name">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                                <FooterStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REMARKS" HeaderText="Account Description">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                    Width="140px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    Width="140px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Principal" HeaderText="Supplier">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="debit" DataFormatString="{0:F2}" HeaderText="Debit">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                    Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="credit" DataFormatString="{0:F2}" HeaderText="Credit">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                    Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Principal_id" HeaderText="Supplier">
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                        Text="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" />
                                        <PagerStyle BackColor="Transparent" />
                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                            TargetControlID="txtCreditAmount" ValidChars=".0123456789">
                        </cc1:FilteredTextBoxExtender>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                            TargetControlID="txtDebitAmount" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
                &nbsp;<asp:Button ID="btnDone" runat="server" AccessKey="S" Font-Size="8pt" OnClick="btnDone_Click"
                    Text="Save" Width="105px" CssClass="button" />
            </td>
            <td style="width: 93px">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
