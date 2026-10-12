<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmInwardGatePass.aspx.cs" Inherits="Forms_frmInwardGatePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript">

        function ValidateForm() {
            var str;

            str = document.getElementById("<%= txtGoodsInThrough.ClientID %>").value;
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
                                            Goods in through
                                        </label>
                                        <asp:TextBox ID="txtGoodsInThrough" runat="server" Width="250px"></asp:TextBox>
                                        <label>
                                            Supplier
                                        </label>
                                        <asp:DropDownList ID="ddlSupplier" runat="server" Width="250px" Enabled="false">
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                        <label>
                                            Transport No
                                        </label>
                                        <asp:TextBox ID="txtTransportNo" runat="server" Width="250px"></asp:TextBox>
                                    </p>
                                </div>
                            </div>
                            <div class="form-cont" style="width: 90%; padding-left: 1px;">
                                <div class="content-r">
                                    <p>
                                        <table>
                                            <tr class="row-gray">
                                                <td style="height: 16px; width: 235px">
                                                    Description
                                                </td>
                                                <td style="height: 16px; width: 103px">
                                                    Unit
                                                </td>
                                                <td style="height: 16px; width: 103px">
                                                    Quantity
                                                </td>
                                                <td style="height: 16px; width: 103px">
                                                    Recieved Quantity
                                                </td>
                                                <td style="height: 16px; width: 103px">
                                                    Balance Quantity
                                                </td>
                                                <td style="height: 16px; width: 270px">
                                                    Remarks
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="200px">
                                                        <asp:GridView ID="GrdOpeningInventory" runat="server" Width="100%" CssClass="table2"
                                                            AutoGenerateColumns="False" ShowHeader="false" CellPadding="2" CellSpacing="0">
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_NAME" HeaderText="Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="210px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_UOM" HeaderText="Unit" Visible="true">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="100px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="QUANTITY" HeaderText="Qty">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="97px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Rec. Qty">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRcdQty" runat="server" Width="70px" Text='<%#Eval("QUANTITY_RECIEVED") %>'
                                                                            OnTextChanged="txtRcdQty_TextChanged" AutoPostBack="true" Height="20px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bal Qty">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtBalQty" runat="server" Width="70px" ReadOnly="true" Height="20px"
                                                                            Text='<%#Eval("QUANTITY_BALANCE") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="237px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </p>
                                    <p>
                                        <table >
                                            <tr>
                                                <td align="right">
                                                    <strong> Total Recieved </strong>
                                                </td>
                                                <td>
                                                <asp:TextBox ID="txtRecQty" runat="server" ReadOnly="true" Text="0"
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                                <tr>
                                                    <td colspan="2">
                                                       
                                                    <%--</td>
                                                    <td align="left">--%>
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnCancel_Click"
                                                            Text="Discard" Width="100px" Height="30px" />
                                                             <asp:Button ID="btnSaveDocument" runat="server" CssClass="btn" OnClick="btnSaveDocument_Click"
                                                            Text="Save" Width="100px" Height="30px" Style="margin-right: 3px;" />
                                                    </td>
                                                </tr>
                                        </table>
                                    </p>
                                </div>
                            </div>
                            <asp:HiddenField ID="hf_TGPID" runat="server" />
                            <asp:HiddenField ID="hf_IGPID" runat="server" />
                            <div class="form-cont" style="width: 90%; padding-left: 10px;">
                                <div class="content-r">
                                    <p>
                                        <table>
                                            <tr class="row-gray">
                                                <td style="height: 16px; width: 103px">
                                                    IGP ID
                                                </td>
                                                <td style="height: 16px; width: 103px">
                                                    TGP ID
                                                </td>
                                                <td style="height: 16px; width: 103px">
                                                    Document Date
                                                </td>
                                                <td style="height: 16px; width: 120px">
                                                    Goods In Through
                                                </td>
                                                <td style="height: 16px; width: 103px">
                                                    Transport No
                                                </td>
                                                <td style="height: 16px; width: 50px">
                                                    Edit
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:Panel ID="pnl_Update" runat="server" Height="200px" ScrollBars="Auto">
                                                        <asp:GridView ID="Grd_IGP" runat="server" Width="100%" CssClass="table2" AutoGenerateColumns="False"
                                                            ShowHeader="false" OnRowEditing="Grd_IGP_RowEditing" OnRowDeleting="Grd_IGP_RowDeleting"
                                                            CellPadding="2" CellSpacing="0">
                                                            <Columns>
                                                                <asp:BoundField DataField="IGP_MASTER_ID" HeaderText="IGP ID">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Height="20px" Width="100px"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TGP_ID" HeaderText="Gate Pass ID">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Height="20px" Width="100px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Document Ddate" DataFormatString="{0:dd-MMM-yyyy}">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Height="20px" Width="100px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SUPPLIER_ID" HeaderText="SUPPLIER_ID">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="GOODS_OUT_THROUGH" HeaderText="GOODS_OUT_THROUGH">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Height="20px" Width="115px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TRANSPORT_NO" HeaderText="TRANSPORT_NO">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Height="20px" Width="100px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit">
                                                                             <img src="../images/icon-edit.jpg" height="15" >
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" Width="50px"
                                                                        Height="20px" BorderStyle="Solid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            Text="Delete">
                                                                                  <img src="../images/icon-del.jpg" height="15" >
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" Width="50px"
                                                                        Height="20px" BorderStyle="Solid" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </p>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
