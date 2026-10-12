<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmMenu.aspx.cs" Inherits="Forms_frmMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">
<table width="100%">
 <tr>
    <td>
        <h2>Arrange Menu</h2>
    </td>
</tr>  
<tr>
    <td>&nbsp;</td>
</tr>
    <tr>
        <td>
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="400px"
                         Width="450px">
                         <cc1:TabPanel ID="TabPanel1" runat="server">
                             <HeaderTemplate>
                                 Top Menu
                             </HeaderTemplate>
                             <ContentTemplate>
                                <asp:UpdatePanel ID="upTop" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                    <tr>
                                        <td style="width:15%" align="right">
                                            <strong><asp:Label ID="lblMenu"  runat="server" Text="Name:"></asp:Label></strong>
                                        </td>
                                        <td style="width:1%"></td>
                                        <td style="width:84%">
                                            <asp:TextBox ID="txtMenu" runat="server" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMenu" runat="server" ControlToValidate="txtMenu"
                                                ErrorMessage="Please enter Menu Name" ValidationGroup="vgMenu"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>                                                                       
                                    <tr>
                                        <td style="width:15%" align="right">                                            
                                        </td>
                                        <td style="width:1%"></td>
                                        <td style="width:84%">                                            
                                            <asp:Button ID="btnAdd" runat="server" Text="Save" OnClick="btnAdd_Click" CssClass="Button"
                                               ValidationGroup="vgMenu" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr>                                        
                                        <td colspan="3">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:10%;"></td>
                                                    <td style="width:80%">
                                                        <asp:ListBox ID="lstTopMenu" runat="server" Width="100%" Height="200">
                                                        </asp:ListBox>
                                                    </td>
                                                    <td style="width:10%">
                                                        <asp:ImageButton ID="ibUp" runat="server" OnClick="ibUp_Click"
                                                        ToolTip="Move Up" Height="20" Width="20" ImageUrl="~/images/Up_Button.png"></asp:ImageButton>
                                                        <br />
                                                        <br />
                                                        <asp:ImageButton ID="ibDown" runat="server" ToolTip="Move down" Height="20"
                                                        Width="20" ImageUrl="~/images/ButtonDown.png" OnClick="ibDown_Click">
                                                    </asp:ImageButton>
                                                        <br />
                                                        <br />
                                                        <asp:ImageButton ID="ibEdit" runat="server" ToolTip="Edit Menu" Height="20"
                                                            Width="20" ImageUrl="~/images/edit.gif" OnClick="ibEdit_Click"></asp:ImageButton>
                                                            <br />
                                                            <br />
                                                            <asp:ImageButton ID="ibDelete" runat="server" ToolTip="Delete Menu" Height="20"
                                                                 Width="20" ImageUrl="~/images/delete.gif" OnClick="ibDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" ></asp:ImageButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                    &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:10%;"></td>
                                                    <td style="width:80%">
                                                        <asp:Button ID="btnUpdateTopMenu" runat="server" Text="Update" OnClick="btnUpdateTopMenu_Click"
                                                            CssClass="Button" />
                                                    </td>
                                                    <td style="width:10%">                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                 </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>                                 
                             </ContentTemplate>
                         </cc1:TabPanel>
                         <cc1:TabPanel ID="TabPanel2" runat="server">
                             <HeaderTemplate>
                                 Left Menu
                             </HeaderTemplate>
                             <ContentTemplate>      
                                <asp:UpdatePanel ID="upLeft" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                    <tr>
                                        <td style="width:25%" align="right">
                                            <strong><asp:Label ID="lblTop" runat="server" Text="Select Top:"></asp:Label></strong>
                                        </td>
                                        <td style="width:1%"></td>
                                        <td style="width:74%">
                                            <asp:DropDownList ID="ddlTop" runat="server" Width="90%" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTop_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:25%" align="right">
                                            <strong><asp:Label ID="lblParent" runat="server" Text="Select Parent:"></asp:Label></strong>
                                        </td>
                                        <td style="width:1%"></td>
                                        <td style="width:74%">
                                            <asp:DropDownList ID="ddlParent" runat="server" Width="90%" 
                                                AutoPostBack="True" onselectedindexchanged="ddlParent_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:25%" align="right">
                                            <strong><asp:Label ID="lblName2" runat="server" Text="Name:"></asp:Label></strong>
                                        </td>
                                        <td style="width:1%"></td>
                                        <td style="width:74%">
                                            <asp:TextBox ID="txtMenu2" runat="server" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtMenu2"
                                                ErrorMessage="Please enter Menu Name" ValidationGroup="vgMenu2"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:25%" align="right">
                                            <strong><asp:Label ID="lblLink" runat="server" Text="Chose Link:" Visible="false"></asp:Label></strong>
                                        </td>
                                        <td style="width:1%"></td>
                                        <td style="width:74%">
                                           <asp:DropDownList ID="ddlLink" runat="server" Width="90%" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:25%" align="right">                                            
                                        </td>
                                        <td style="width:1%"></td>
                                        <td style="width:74%">
                                           <asp:Button ID="btnAdd2" runat="server" Text="Save" OnClick="btnAdd2_Click"
                                            CssClass="Button" ValidationGroup="vgMenu2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>                                        
                                        <td colspan="3">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:10%;"></td>
                                                    <td style="width:80%">
                                                        <asp:ListBox ID="lstLeft" runat="server" Width="100%" Height="170" CssClass="DropList">
                                                        </asp:ListBox>
                                                    </td>
                                                    <td style="width:10%">
                                                        <asp:ImageButton ID="ibUp2"  runat="server" OnClick="ibUp2_click" ToolTip="Move Up"
                                                             Height="20" Width="20" ImageUrl="~/images/Up_Button.png"></asp:ImageButton>
                                                        <br />
                                                        <br />
                                                       <asp:ImageButton ID="ibDown2" runat="server" ToolTip="Move down" Height="20"
                                                            Width="20" ImageUrl="~/images/ButtonDown.png" OnClick="ibDown2_Click">
                                                        </asp:ImageButton>                                                    
                                                        <br />
                                                        <br />
                                                        <asp:ImageButton ID="ibEdit2" runat="server" ToolTip="Edit Menu" Height="20"
                                                        Width="20" ImageUrl="~/images/edit.gif" OnClick="ibEdit2_Click"></asp:ImageButton>
                                                            <br />
                                                            <br />
                                                                 <asp:ImageButton ID="ibDelete2" runat="server" ToolTip="Delete Menu" Height="20"
                                                                      Width="20" ImageUrl="~/images/delete.gif" OnClick="ibDelete2_Click" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                    &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:10%;"></td>
                                                    <td style="width:80%">
                                                        <asp:Button ID="btnUpdateLeftMenu" runat="server" Text="Update" OnClick="btnUpdateLeftMenu_Click"
                                                            CssClass="Button" />
                                                    </td>
                                                    <td style="width:10%">                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                 </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>                                                            
                             </ContentTemplate>
                         </cc1:TabPanel>                         
                     </cc1:TabContainer>
        </td>
    </tr>
</table>
</div>
</asp:Content>

