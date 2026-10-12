<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUData.aspx.cs" Inherits="Forms_frmSKUData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
   <script language="JavaScript" type="text/javascript">
            function pageLoad() {
                $("#ctl00_ctl00_mainCopy_cphPage_ddlSKuCde").searchable();
            }
    </script>
    <div id="right_data">
            <table width="100%">
                <tr>

                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <asp:HiddenField ID="hdnOPENING_INVENTORY_ID" runat="server" />
                             <div class="form-cont" style="width:60%;  padding-left:70px;">
                             
                            <div class="content-r">
                                    <p>
                                        <label>
                                            Category Type
                                        </label>
                                        <asp:DropDownList ID="ddlCategoryType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryType_SelectedIndexChanged" Width="250px">
                                            <asp:ListItem Text="Finish" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Raw" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                        <label>
                                            Main Type
                                        </label>
                                        <asp:DropDownList ID="ddlMainType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMainType_SelectedIndexChanged" Width="250px">
                                        </asp:DropDownList>
                                    </p>
                                     <p>
                                        <label>
                                            Sub Type
                                        </label>
                                        <asp:DropDownList ID="ddlSubType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged" Width="250px">
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                   
                                    
                                    <table>
                                <tr>
                                <td style="height:30px;">
                                
                                </td>
                                </tr>
                                        <tr  class="row-gray">
                                            <td style="height: 16px">
                                                Description
                                            </td>
                                            <td style="height: 16px">
                                               Quantity
                                            </td>
                                           
                                            <td style="height: 16px">
                                            Add SKU
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                    <asp:DropDownList ID="ddlSKuCde" runat="server" Width="300px" onfocus="ddlFocus(this);" onblur="ddlBlur(this);" >
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtBox " onfocus="SearchSKUCode();" 
                                                    Width="100px"></asp:TextBox>
                                            </td>
                                           
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add Sku"
                                                    ValidationGroup="vg" Width="100px" AccessKey="A" CssClass="Button" 
                                                     Height="28px" />
                                            </td>
                                        </tr>
                                        <tr>
                                        <td colspan ="3">
                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="200px">
                                        <asp:GridView ID="GrdOpeningInventory" runat="server" Width="100%" CssClass="table2" AutoGenerateColumns="False"
                                        ShowHeader="false" OnRowEditing="GrdOpeningInventory_RowEditing" OnRowDeleting="GrdOpeningInventory_RowDeleting"
                                        CellPadding="0" CellSpacing="0" >
                                        <Columns>
                                            <asp:BoundField DataField="OPENING_INVENTORY_ID" HeaderText="OPENING_INVENTORY_ID" Visible="False">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                </ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_NAME" HeaderText="Name">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                    Width="55%"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID" Visible="False">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                </ItemStyle>
                                            </asp:BoundField>                                
                                            <asp:BoundField DataField="QUANTITY" HeaderText="Quantity">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                    Width="20%"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit">
                                                    <img src="../images/icon-edit.jpg" height="15" width="40%">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                        Text="Delete">
                                                    <img src="../images/icon-del.jpg" height="15" width="40%">
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
                                   </div>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                                </asp:ImageButton>&nbsp; Loading....
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
    </div>
</asp:Content>
