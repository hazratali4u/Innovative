<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmStore.aspx.cs" Inherits="frmStore" Title="Store" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
            <ContentTemplate>
                <div class="mainContent">
                    <div class="content">
                        <article class="topcontent">	
					<div class="form-cont w-60">
                        <div id="mainContent">
				            <div class="content-r">                                
                                <p>                                    
                                    <label>Location </label>
                                    <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlLocation_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </p>
                	            <p><label>Store Code</label><asp:TextBox ID="txtStoreCode" runat="server" placeholder="Store Code"></asp:TextBox></p>
                                <p><label>Store Name</label><asp:TextBox ID="txtStoreName" runat="server" placeholder="Store Name"></asp:TextBox></p>                                
                            </div>
                            <div class="form-bottom">
					            <p> 
                                    <asp:Button ID="btnSaveStore" OnClick="btnSaveStore_Click" runat="server" Text="Save" CssClass="btn" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Discard" CssClass="btn" 
                                        onclick="btnCancel_Click" />                                
                                </p>
				            </div> 
                            <hr />
                            <div class="form2">
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                    <tr class="row-gray">                   
                    <td style="width:25%;">Code</td>
                    <td style="width:55%;">Name</td>
                    <td colspan="2" align="center" style="width:20%;">Action</td>
                  </tr>                                                      
                </table>
                <asp:GridView ID="GrdStore" runat="server" Width="97%" CssClass="table2" AutoGenerateColumns="False" ShowHeader="false"
                    OnRowEditing="GrdStore_RowEditing" OnRowDeleting="GrdStore_RowDeleting" CellPadding="0" CellSpacing="0">                                    
                                    <Columns>
                                        <asp:BoundField DataField="STORE_ID" HeaderText="STORE_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STORE_CODE" HeaderText="Code">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="25%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STORE_NAME" HeaderText="Name">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="55%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                         
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit">
                                                    <img src="../images/icon-edit.jpg" height="15" width="15">
                                                </asp:LinkButton>                                                
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                            </ItemStyle>
                                        </asp:TemplateField>               
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" Text="Delete">
                                                    <img src="../images/icon-del.jpg" height="15" width="15">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                
                </div>                           
			            </div>			
                    </div>
				</article>
                    </div>
                </div>                
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel21">
            <ProgressTemplate>
                <asp:Image ID="Image3" runat="server" Width="30px" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                </asp:Image>&nbsp; Loading .........
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
