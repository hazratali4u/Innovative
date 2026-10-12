<%@ Page Title="Quality Control" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmQCAdd.aspx.cs" Inherits="Forms_frmQCAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div class="mainContent">
	<div class="content">	
				<article class="topcontent">	
					<div class="form-cont">
            <div id="mainContent">                
            	<div class="clr"></div>                                 
				<div class="content-r w-50">                    
                    <p>
                    <label>GRN</label>
                        <asp:DropDownList ID="ddlGRN" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlGRN_SelectedIndexChanged"></asp:DropDownList>                    
                    <p>
                        <label>Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server"> 
                            <asp:ListItem Text="Reject" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                    </p>
              </div>
                <div class="content-r w-50">
                	<p>
                        <label>Product</label><asp:DropDownList ID="ddlProduct" runat="server" 
                            AutoPostBack="True" onselectedindexchanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList>                        
                    </p>     
                    <p>
                        <label>Quantity Acceptable</label>
                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                    </p>                   
              </div>
                <hr>               
              <div class="clr"></div>
                <div class="form2">
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                  <tbody>
                  <tr class="row-gray">
                    <td width="10%">Paramter ID</td>
                    <td width="30%">Paramter Description</td>
                    <td width="15%">Paramter Value</td>
                    <td width="15%">Value</td>
                    <td width="30%">Remarks</td>
                  </tr>                  
                  <tr>
                  	<td colspan="5" class="line-h"></td>
                  </tr>
                </tbody>
                </table>
                <br />
                <asp:GridView ID="gvParamter" runat="server" Width="97%" CssClass="table2" 
                        AutoGenerateColumns="False" ShowHeader="false" 
                    CellPadding="0" CellSpacing="0">                                    
                                    <Columns>                                        
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>                                                                                                                                                                
                                        <asp:BoundField DataField="SKU_QUALITY_ID" HeaderText="Paramter ID">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QUALITY_DESCRIPTION" HeaderText="Paramter Description">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="30%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>      
                                        <asp:BoundField DataField="QUALITY_VALUE" HeaderText="Paramter Value">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                                                                                                                           
                                        <asp:TemplateField HeaderText="Value">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValue" runat="server" Width="100%" Text='<%#Eval("Value") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                        </asp:TemplateField>                                                                                
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="100%" Text='<%#Eval("Remarks") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="30%">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                
                </div>
                <br />
                <div class="form-bottom">
					<p> 
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add" 
                            onclick="btnAdd_Click" />                
                    </p>
				</div>

                <hr>
                <br />
                <div class="form2">
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                  <tbody>
                  <tr class="row-gray">
                    <td width="45%">Product</td>
                    <td width="20%">Status</td>
                    <td width="15%">Quantity Acceptable</td>
                    <td width="20%" colspan="2">Action</td>
                  </tr>                  
                  <tr>
                  	<td colspan="5" class="line-h"></td>
                  </tr>
                </tbody>
                </table>
                <br />
                <asp:GridView ID="gvProduct" runat="server" Width="97%" CssClass="table2" 
                        AutoGenerateColumns="False" ShowHeader="false" OnRowDeleting="gvProduct_RowDeleting" OnRowEditing="gvProduct_RowEditing"
                    CellPadding="0" CellSpacing="0">                                    
                                    <Columns>                                        
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STATUS_ID" HeaderText="STATUS_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>                                                                                                                                                                
                                        <asp:BoundField DataField="SKU_Name" HeaderText="Product">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status" HeaderText="Status">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>      
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
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

                <div class="form-bottom">
					<p> 
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Discard" 
                            onclick="btnCancel_Click" />
                        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" 
                            onclick="btnSave_Click" />                        
                    </p>
				</div>
			</div>
    </div>
				</article>
				
		</div>
</div>
            </ContentTemplate>
        </asp:UpdatePanel>
</div>
</asp:Content>

