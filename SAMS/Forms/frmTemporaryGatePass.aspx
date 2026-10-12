<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmTemporaryGatePass.aspx.cs" Inherits="Forms_frmTemporaryGatePass" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../AjaxLibrary/jquery.maskedinput.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            $("#ctl00_ctl00_mainCopy_cphPage_txtReturnDateTime").mask("99/99/9999, 99:99 aa");
        });

        function ValidateForm() {
           var str;
           str = document.getElementById("<%= txtIssueDate.ClientID %>").value;
           if (str.length == 0 || str == null){
           alert('Must enter Issue Date.');
               return false;
           }
           str = document.getElementById("<%= txtReturnDateTime.ClientID %>").value;
           if (str.length == 0 || str == null) {
               alert('Must enter Return Date.');
               return false;
           }
           str = document.getElementById("<%= txtGoodsOutThrough.ClientID %>").value;
           if (str.length == 0 || str == null) {
               alert('Must enter Goods out through.');
               return false;
           }
           str = document.getElementById("<%= txtTransportNo.ClientID %>").value;
           if (str.length == 0 || str == null) {
               alert('Must enter Transport No.');
               return false;
           }
           return true;
       }

     
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnOPENING_INVENTORY_ID" runat="server" />
                            <div class="form-cont" style="width: 80%; padding-left: 30px;">
                                <div class="content-r">
                                    <p>
                                        <label>
                                            Issue Date
                                        </label>
                                        <asp:TextBox ID="txtIssueDate" runat="server" Width="229px" ></asp:TextBox>
                                        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                         <asp:ImageButton ID="ibtnIssueDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" TargetControlID="txtIssueDate"
                                            PopupButtonID="ibtnIssueDate" Format="dd/MMM/yyyy">
                                        </cc1:CalendarExtender>
                                        <label>
                                            Return Date Time
                                        </label>
                                        <asp:TextBox ID="txtReturnDateTime" runat="server" Width="250px" placeholder="DD/MM/YYYY, HH:MM AM/PM"></asp:TextBox>
                                        <%--  <cc1:MaskedEditExtender  TargetControlID="txtReturnDateTime" Mask="99/99/9999, 99:99" runat="server" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"  OnInvalidCssClass="MaskedEditError" MaskType="DateTime" InputDirection="RightToLeft" AcceptNegative="Left"  DisplayMoney="Left"  ErrorTooltipEnabled="True" AcceptAMPM></cc1:MaskedEditExtender>--%>
                                        <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtReturnDateTime" Mask="99/99/9999, 99:99 tt" UserDateFormat="DayMonthYear"  ClearMaskOnLostFocus="false" ClearTextOnInvalid="false">
                                </cc1:MaskedEditExtender>--%>
                                    </p>
                                    <p>
                                        <label>
                                            Goods out through
                                        </label>
                                        <asp:TextBox ID="txtGoodsOutThrough" runat="server" Width="250px"></asp:TextBox>
                                        <label>
                                            Supplier
                                        </label>
                                        <asp:DropDownList ID="ddlSupplier" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                        <label>
                                            Transport No
                                        </label>
                                        <asp:TextBox ID="txtTransportNo" runat="server" Width="250px"></asp:TextBox>
                                        <%--<cc1:TimeSelector ID="txtReturnTime"  runat="server" DisplaySeconds="false" SelectedTimeFormat="Twelve" EnableTheming="true">
                                                        </cc1:TimeSelector>--%>
                                    </p>
                                </div>
                            </div>
                            <div class="form-cont" style="width: 80%; padding-left: 30px;">
                                <div class="content-r">
                                    <p>
                                        <label>
                                            Category Type
                                        </label>
                                        <asp:DropDownList ID="ddlCategoryType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryType_SelectedIndexChanged"
                                            Width="250px">
                                            <asp:ListItem Text="Finish" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Raw" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                        <label>
                                            Main Type
                                        </label>
                                        <asp:DropDownList ID="ddlMainType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMainType_SelectedIndexChanged"
                                            Width="250px">
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                        <label>
                                            Sub Type
                                        </label>
                                        <asp:DropDownList ID="ddlSubType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged"
                                            Width="250px">
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                        <table>
                                            <tr>
                                                <td style="height: 30px;">
                                                </td>
                                            </tr>
                                            <tr class="row-gray">
                                                <td style="height: 16px">
                                                    Description
                                                </td>
                                                <td style="height: 16px">
                                                    Unit
                                                </td>
                                                <td style="height: 16px">
                                                    Quantity
                                                </td>
                                                <td style="height: 16px">
                                                    Remarks
                                                </td>
                                                <td style="height: 16px">
                                                    Action
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlSKuCde" runat="server" Width="230px" OnSelectedIndexChanged="ddlSKuCde_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUOM" runat="server" Width="100px" Enabled="false">
                                                    </asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Width="270px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add"
                                                        ValidationGroup="vg" Width="100px" AccessKey="A" CssClass="Button" Height="28px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="200px">
                                                        <asp:GridView ID="GrdOpeningInventory" runat="server" Width="100%" CssClass="table2"
                                                            AutoGenerateColumns="False" ShowHeader="false" OnRowEditing="GrdOpeningInventory_RowEditing"
                                                            OnRowDeleting="GrdOpeningInventory_RowDeleting" CellPadding="0" CellSpacing="0">
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_NAME" HeaderText="Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="235px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_UOM" HeaderText="Unit" Visible="true">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="103px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="QUANTITY" HeaderText="Remarks">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="103px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="REMARKS" HeaderText="Quantity">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="270px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit">
                                                                             <img src="../images/icon-edit.jpg" height="15" >
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                    </ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            Text="Delete">
                                                                                  <img src="../images/icon-del.jpg" height="15" >
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                    </ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </p>
                                    <p>
                                        <asp:Button ID="btnSaveDocument" runat="server" CssClass="btn" OnClick="btnSaveDocument_Click"
                                            Text="Save" Width="100px" Height="30px" Style="margin-right: 3px;" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnCancel_Click" Text="Discard" Width="100px"
                                            Height="30px" />
                                    </p>
                                </div>
                            </div>
                            <asp:HiddenField ID="hf_TGPID" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--  <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                                </asp:ImageButton>&nbsp; Loading....
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
