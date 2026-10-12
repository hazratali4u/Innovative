<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUHierarchyData.aspx.cs" Inherits="frmSKUHierarchyData" Title="Supplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $('#<%=txtNTN.ClientID %>').mask("9999999-9");
            $('#<%=txtSTRN.ClientID %>').mask("99-99-9999-999-99");
        }
 
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <cc1:TabContainer ID="tbUserAssignment" runat="server" Width="100%" Height="500px"  ActiveTabIndex="0" OnActiveTabChanged="tbUserAssignment_ActiveTabChanged" AutoPostBack="true">
                    <cc1:TabPanel runat="server" ID="TabPanel1">
                        <HeaderTemplate>
                            Add Supplier
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="8">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%" align="right">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Text="Code:"></asp:Label></strong>
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPrincipalCode" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 10%" align="right">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Text="Name:"></asp:Label></strong>
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPrincipalName" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td rowspan="8" style="width: 25%">
                                        <asp:Panel runat="server" ID="pnlUpload" ScrollBars="Vertical" BorderStyle="Solid"
                                            BorderWidth="1px" Height="224px" Width="100%">
                                            <asp:GridView ID="GvEmail" runat="server" CellPadding="2" Width="100%" GridLines="Horizontal"
                                                EmptyDataText="<div style='padding:10px; margin:10px; background-color:#fbefef; border: Solid 1px #bb0000; color:#bb0000;'>There is no Image to display.</div>">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btn_delete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to Remove this Document?');"
                                                                CommandName="btn_delete_Click" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Text="Remove" OnClick="btn_delete_Click" CssClass="Button" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <HeaderStyle BackColor="#006699" ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label11" runat="server" Text="Contact Person:"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label13" runat="server" Text="NTN:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNTN" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label14" runat="server" Text="STRN:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSTRN" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label15" runat="server" Text="Phone:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhone" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label16" runat="server" Text="Fax:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFax" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label17" runat="server" Text="Mobile:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label4" runat="server" Text="Certificate Name:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCertificateName" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Text="Date of Expiry:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtDateofExpiry" Width="85%" runat="server" CssClass="txtBox" MaxLength="10"
                                            Enabled="false"></asp:TextBox>
                                        <asp:ImageButton ID="imgDateofExpiry" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                        </asp:ImageButton>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            EnableViewState="False" PopupButtonID="imgDateofExpiry" TargetControlID="txtDateofExpiry">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" Text="Is Registered:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsRegistered" runat="server" Enabled="false"></asp:CheckBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" Text="EMail ID:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailID" runat="server" Width="85%" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label18" runat="server" Text="Address:"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtAddress" runat="server" Width="94%" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>
                                            <asp:Label ID="Label7" runat="server" Text="Certificate Image:"></asp:Label></strong>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="5">
                                        <asp:FileUpload ID="fuImage" Multiple="Multiple" runat="server" Width="200px" Enabled="False">
                                        </asp:FileUpload>
                                        <asp:Button ID="btnUpload" OnClick="btnUpload_Click" runat="server" Text="Upload"
                                            CssClass="Button" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="5">
                                        <asp:Button ID="btnSavePrincipal" OnClick="btnSavePrincipal_Click" runat="server"
                                            Text="New" CssClass="Button" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" Width="100%"
                                            BorderWidth="1px">
                                            <asp:GridView ID="GrdPrincipal" runat="server" Width="100%" ForeColor="SteelBlue"
                                                Font-Size="9pt" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                                AutoGenerateColumns="False" OnRowEditing="GrdPrincipal_RowEditing" OnRowDeleting="GrdPrincipal_RowDeleting"
                                                OnPageIndexChanging="GrdPrincipal_PageIndexChanging">
                                                <PagerSettings PreviousPageText="Previous" Mode="NextPrevious" LastPageText="" FirstPageText=""
                                                    NextPageText="Next"></PagerSettings>
                                                <Columns>
                                                    <asp:BoundField DataField="SKU_HIE_ID" HeaderText="ID">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                        </ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Cont. Person">
                                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                        </ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NTN" HeaderText="NTN">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="STRN" HeaderText="STRN">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PHONE" HeaderText="PHONE">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FAX" HeaderText="FAX">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MOBILE" HeaderText="MOBILE">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CERTIFICATE_NAME" HeaderText="MOBILE">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DATE_OF_EXPIRY" HeaderText="MOBILE">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EMIAL_ID" HeaderText="MOBILE">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IS_REGISTERED" HeaderText="MOBILE">
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                        </ItemStyle>
                                                    </asp:CommandField>
                                                    <asp:TemplateField HeaderText="Inactive">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Inactive?');return false;"
                                                                Text="Inactive"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                <AlternatingRowStyle CssClass="GridAlternateRowStyle"></AlternatingRowStyle>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" ID="TabPanel2">
                        <HeaderTemplate>
                            Assign Product
                        </HeaderTemplate>
                        <ContentTemplate>
                        <table>
                        <tr>
                        <td style="width:50px;">

                        </td>
                                                <td>
                                                 <table>
                                <tbody>
                                <tr>
                                <td colspan="4" style="height:30px;">
                                </td>
                                </tr>
                                    <tr>
                                        <td align="right">
                                            <strong>
                                                <asp:Label ID="Label10" runat="server" Width="112px" Text="Product Category" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProductCategory" runat="server" Width="200px" CssClass="DropList"  OnSelectedIndexChanged="ddlProductCategory_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            <strong>
                                                <asp:Label ID="Label12" runat="server" Width="112px" Text="Supplier" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSupplier" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="ddlProductCategory_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                <td colspan="4" style="height:30px;">
                                </td>
                                </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>
                                                <asp:Label ID="Label19" runat="server" Width="142px" Text="Product Assignment" CssClass="lblbox"></asp:Label></strong>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 182px; width:100%;" colspan="4">
                                            <table style="width:100%;">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 40%;" rowspan="4">
                                                            <asp:ListBox ID="lstUnAssignProduct" runat="server" Width="100%" Height="200px" CssClass="DropList">
                                                            </asp:ListBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width:40%;" rowspan="4">
                                                            <asp:ListBox ID="lstAssignProduct" runat="server" Width="100%" Height="200px" CssClass="DropList">
                                                            </asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnAssign" runat="server" Width="30px" Font-Size="8pt" Text=">>"
                                                                OnClick="btnAssign_Click" CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnUnAssign" runat="server" Width="30px" Font-Size="8pt" Text="<<"
                                                                OnClick="btnUnAssign_Click" CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 75px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="1">
                                                        </td>
                                                        <td style="width: 75px; height: 24px">
                                                        </td>
                                                        <td rowspan="1">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        </tr>
                        </table>
                           
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                
            </ContentTemplate>
           
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel6">
            <ProgressTemplate>
                <asp:ImageButton ID="ImageButton1" runat="server" Width="33px" Height="31px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                </asp:ImageButton>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
