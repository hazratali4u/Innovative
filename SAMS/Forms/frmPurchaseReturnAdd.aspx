<%@ Page Title="Purchase Return" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmPurchaseReturnAdd.aspx.cs" Inherits="Forms_frmPurchaseReturnAdd" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<script language="JavaScript" type="text/javascript">
    function ValidateForm() {
        var str;

        str = document.getElementById('<%=txtQuantity.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Must Enter Quantity');
            return false;
        }
        return true;
    }

    function CalcAmount() {
        var rate = document.getElementById('<%=txtRate.ClientID%>').value;
        var qty = document.getElementById('<%=txtQuantity.ClientID%>').value;

        if (rate > 0) {
        }
        else {
            rate = 0;
        }
        if (qty > 0) {
        }
        else {
            qty = 0;
        }

        document.getElementById('<%=txtAmount.ClientID%>').value = parseFloat(rate * qty).toFixed(2);
    }
</script>
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
                	<p><label>Supplier</label><asp:DropDownList ID="ddlSupplier" runat="server"></asp:DropDownList></p>                    
                    <p><label>Request for</label><asp:DropDownList ID="ddlStore" runat="server"></asp:DropDownList></p>
              </div>
                <div class="content-r w-50">
                	<p><label>Location</label><asp:DropDownList ID="ddlLocation" 
                            runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList></p>
                    <p>
                        <label>PO</label>
                        <asp:TextBox ID="txtPO" runat="server"></asp:TextBox>                        
                    </p>
              </div>
                <hr>
               
              <div class="clr"></div>
                <div class="form2">
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                    <tr>
                    <td colspan="5">                    
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" placeholder="Remarks / Terms &amp; Conditions"></asp:TextBox>
                    </td>
                  </tr>
                </table>
                <br />
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                  <tbody>
                  <tr class="row-gray">
                    <td width="40%">Product</td>
                    <td width="15%">Quantity</td>
                    <td width="15%">Rate</td>
                    <td width="15%">Amount</td>
                    <td colspan="2" align="center">Action</td>
                  </tr>
                  <tr>
                    <td><asp:DropDownList ID="ddlProduct" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtQuantity" runat="server" onblur="CalcAmount();"></asp:TextBox> </td>
                    <td><asp:TextBox ID="txtRate" runat="server" onblur="CalcAmount();"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtAmount" runat="server" Enabled="false"></asp:TextBox></td>
                    <td align="center" valign="middle" colspan="2">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add" onclick="btnAdd_Click" onfocus="CalcAmount();"/>
                    </td>
                  </tr>
                  <tr>
                  	<td colspan="6" class="line-h"></td>
                  </tr>
                </tbody>
                </table>
                <br />
                <asp:GridView ID="gvProduct" runat="server" Width="97%" CssClass="table2" AutoGenerateColumns="False" ShowHeader="false" 
                    CellPadding="0" CellSpacing="0" OnRowDeleting="gvProduct_RowDeleting" OnRowEditing="gvProduct_RowEditing">                                    
                                    <Columns>
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>                                                                                
                                        <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="40%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                         
                                        <asp:BoundField DataField="PRICE" HeaderText="PRICE" DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:0.00}">
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
                <br />
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

