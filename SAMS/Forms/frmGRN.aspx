<%@ Page Title="Goods Receipt Note" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmGRN.aspx.cs" Inherits="Forms_frmGRN" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div class="mainContent">
	<div class="content">	
				<article class="topcontent">	
					<div class="form-cont">
                    <asp:Label ID="ltrAdd" runat="server"></asp:Label>
            <div id="mainContent">
              <div class="clr"></div>
                <div class="form2">
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                  <tbody>
                  <tr class="row-gray">
                    <td width="5%">ID</td>
                    <td width="5%">PR ID</td>
                    <td width="30%">Location</td>
                    <td width="30%">Supplier</td>
                    <td width="10%">Received Qty</td>
                    <td width="10%">GRN Date</td>
                    <td colspan="2" align="center" width="10%">Action</td>
                  </tr>
                </tbody>
                </table>              
                <br />
                <asp:GridView ID="gvGRN" runat="server" Width="97%" CssClass="table2" AutoGenerateColumns="False" ShowHeader="false" 
                    CellPadding="0" CellSpacing="0" OnRowDeleting="gvGRN_RowDeleting" OnRowEditing="gvGRN_RowEditing">                                    
                                    <Columns>
                                        <asp:BoundField DataField="GRN_MASTER_ID" HeaderText="GRN_MASTER_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>                                                                                
                                        <asp:BoundField DataField="GRN_MASTER_ID" HeaderText="ID">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="5%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REQUISITION_MASTER_ID" HeaderText="PR ID">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="5%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="30%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                         
                                        <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="PRICE">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="30%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                       
                                        <asp:BoundField DataField="ReceiveQty" HeaderText="Received">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="GRN Date" DataFormatString="{0:dd-MM-yyyy}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
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
</div>
</asp:Content>

