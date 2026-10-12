<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmRecipts.aspx.cs" Inherits="Forms_frmVoucherEntry" Title="Receipts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
        }
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

        function AccountHeadSelected(source, eventArgs) {
            document.getElementById("<%=txtAccountDes.ClientID %>").focus();
        }

        function ValidateForm() {
            var Debit = document.getElementById("<%= txtDebitAmount.ClientID %>").value;
            var Credit = document.getElementById("<%= txtCreditAmount.ClientID %>").value;
            var VoucherType = document.getElementById("<%= DrpVoucherType.ClientID %>").value;

            if (Debit.length == 0 && Credit.length == 0) {
                alert('Debit or Credit Must Enter');
                return false;
            }


            if (Debit > 0 && Credit > 0 && VoucherType != 16) {
                alert('Only Debit or Credit Can Enter');
                return false;
            }
            return true;
        }
 
    </script>
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 60%" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblTransactionDate" runat="server" Text="Voucher Date"></asp:Label>
                                                </strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDate" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                </asp:ImageButton>
                                                <cc1:CalendarExtender ID="ceDate" runat="server" EnableViewState="False"
                                                    Format="dd-MMM-yyyy" PopupButtonID="ibDate" TargetControlID="txtDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="width: 99px" align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="61px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="206px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="width: 99px" align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="Label16" runat="server" CssClass="lblbox" Height="16px" Text="Voucher Type"
                                                        Width="83px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" style="height: 20px">
                                                <asp:DropDownList ID="DrpVoucherType" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged" Width="205px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td style="width: 99px;" align="left">
                                                <strong>
                                                    <asp:Label ID="lblSubType" runat="server" CssClass="lblbox" Height="16px" Text="Voucher Sub Type"
                                                        Width="120px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpVoucherSubType" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpVoucherSubType_SelectedIndexChanged" Width="205px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblPaymentMode" runat="server" CssClass="lblbox" Text="Payment Mode"
                                                        Width="87px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpPaymentMode" runat="server" CssClass="DropList" Width="206px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="width: 99px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblBankHead" runat="server" Width="98px" Text="Bank/Cash Head" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="drpBanks" runat="server" CssClass="DropList" Width="206px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblChequeNo" runat="server" Width="76px" Text="Cheque No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="width: 99px;" align="left">
                                                <strong>
                                                    <asp:Label ID="lblSlipNo" runat="server" CssClass="lblbox" Text="Drawn on/ A.C No." Width="91px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSlipNo" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblDueDate" runat="server" Width="85px" Text="Due Date"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDueDate" runat="server" Width="180px" onkeyup="BlockEndDateKeyPress()"
                                                    ReadOnly="true"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                    TargetControlID="txtDueDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td style="width: 99px;" align="left">
                                               
                                            </td>
                                            <td align="left">
                                          
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" CssClass="lblbox" Text="Customer" Width="91px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" style="height: 25px">
                                                <asp:DropDownList ID="drpCustomer" runat="server" CssClass="DropList" Width="206px">
                                                </asp:DropDownList>
                                                <%-- <asp:TextBox ID="txtpayeesName" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>--%>
                                            </td>
                                            <td align="left" style="height: 25px">
                                            </td>
                                            <td align="left" style="width: 99px; height: 25px">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Received from" Width="85px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left" style="height: 25px">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " MaxLength="250" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                              
                                                    <strong>
                                                <asp:Label ID="lblWHTAX" runat="server" CssClass="lblbox" 
                                                    Text="WH-TAX" Width="98px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 99px;" align="left">
                                            <asp:DropDownList ID="ddlWHTax" runat="server" CssClass="DropList" Width="206px">
                                                </asp:DropDownList>
                                               
                                            </td>
                                             <td align="left">
                                               
                                            </td>
                                            <td align="left">
                                                 <strong>
                                                <asp:Label ID="lblParcentage" runat="server" CssClass="lblbox" 
                                                    Text="WH-TAX %" Width="98px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left">
                                             <asp:TextBox ID="txtPercentage" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender3" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtPercentage">
                                                    </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:TextBox ID="txtTotalDebit" runat="server" Width="100px" CssClass="HidePanel"></asp:TextBox><asp:TextBox
                                    ID="txtTotalCredit" runat="server" Width="73px" CssClass="HidePanel"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 40%" valign="top">
                        <asp:UpdatePanel ID="upCustomer" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                <tr>
                                        <td style="width: 20%;">
                                            <strong>
                                                <asp:Label ID="lblRealization" runat="server" Text="Realization"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 79%;">
                                            <asp:CheckBox ID="cbRealization" runat="server" AutoPostBack="true" 
                                                oncheckedchanged="cbRealization_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;">
                                            <strong>
                                                <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 79%;">
                                            <asp:DropDownList ID="ddlCustomer" runat="server" Width="195px" Enabled="False" 
                                                AutoPostBack="True" onselectedindexchanged="ddlCustomer_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" BorderColor="Silver"
                                                BorderStyle="Groove" BorderWidth="1px" Width="265px">
                                                <asp:GridView ID="GrdCredit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="250px"
                                                    OnRowDeleting="GrdOrder_RowDeleting" DataKeyNames="SALE_INVOICE_ID" 
                                                    Enabled="False">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChbIsAssigned" runat="server" Width="14px" />
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="Invoice No">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CURRENT_CREDIT_AMOUNT" HeaderText="Credit Amount" DataFormatString="{0:F2}">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:GridView>
                                            </asp:Panel>
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
            <table width="60%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table style="border-right: #007395 1px solid; border-top: #007395 1px solid; border-left: #007395 1px solid;
                                    border-bottom: #007395 1px solid" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td class="tblhead">
                                                <asp:Label ID="lblskuCode" runat="server" ForeColor="White" Font-Bold="True" Text="Account Description"></asp:Label>
                                            </td>
                                            <td class="tblhead">
                                                <asp:Label ID="lblskuname" runat="server" ForeColor="White" Font-Bold="True" Text="Remarks"></asp:Label>
                                            </td>
                                            <td class="tblhead">
                                                <asp:Label ID="Label11" runat="server" ForeColor="White" Font-Bold="True" Text="Debit"></asp:Label>
                                            </td>
                                            <td class="tblhead">
                                                <asp:Label ID="Label12" runat="server" ForeColor="White" Font-Bold="True" Text="Credit"></asp:Label>
                                            </td>
                                            <td>
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
                                                <asp:TextBox ID="txtAccountDes" runat="server" Width="140px"></asp:TextBox>
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
                                            <td align="center" colspan="8" rowspan="1">
                                                <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" BackColor="White"
                                                    AutoGenerateColumns="False" BorderColor="White" CaptionAlign="Left" HorizontalAlign="Center"
                                                    OnRowDataBound="GrdOrder_RowDataBound" OnRowDeleting="GrdOrder_RowDeleting" OnRowEditing="GrdOrder_RowEditing"
                                                    ShowFooter="True" ShowHeader="False">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Account_Head_Id" HeaderText="Account Head Id">
                                                            <FooterStyle CssClass="HidePanel" />
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Account_Name" HeaderText="Account Name">
                                                            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="295px" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Account_Name">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                            <FooterStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="REMARKS" HeaderText="Account Description">
                                                            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                Width="140px" />
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
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="tblhead" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCreditAmount"
                                    FilterType="Custom" ValidChars=".0123456789">
                                </cc1:FilteredTextBoxExtender>
                                <div style="z-index: 101; left: 706px; width: 100px; position: absolute; top: 364px;
                                    height: 100px">
                                    <asp:Panel ID="Panel21" runat="server">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                            <ProgressTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="35px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                    Width="39px" />
                                                Wait Update
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </asp:Panel>
                                </div>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDebitAmount"
                                    FilterType="Custom" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <table width="50%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnDone" runat="server" AccessKey="S" OnClick="btnDone_Click" Text="Save"
                            CssClass="Button" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" AccessKey="S" OnClick="btnCancel_Click"
                            Text="Cancel" CssClass="Button" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>
</asp:Content>
