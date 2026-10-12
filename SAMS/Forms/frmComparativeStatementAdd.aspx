<%@ Page Title="Comparative Statement" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmComparativeStatementAdd.aspx.cs" Inherits="Forms_frmComparativeStatementAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<script language="JavaScript" type="text/javascript">
    function ValidateForm() {
        var str;

        str = document.getElementById('<%=txtQuantity.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Must Enter Quantity');
            return false;
        }

        str = document.getElementById('<%=txtQuotedPrice.ClientID%>').value;

        if (str == null || str.length == 0) {

            alert('Must Enter Quoted Price');

            return false;
        }

        return true;
    }
    function calcAmount() {
        var qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
        if (qty > 0) {
        }
        else {
            qty = 0;
        }
        var price = document.getElementById('<%=txtQuotedPrice.ClientID%>').value;
        if (price > 0) {
        }
        else {
            price = 0;
        }
        document.getElementById('<%=txtAmount.ClientID%>').value = parseFloat(qty * price).toFixed(2);        
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
                    <p>
                        <label>Requistion ID </label>
                        <asp:DropDownList ID="ddlRequisition" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlRequisition_SelectedIndexChanged"></asp:DropDownList>
                    </p>
                	<p><label>Orignitor</label><asp:TextBox ID="txtOriginator" runat="server" Enabled="false"></asp:TextBox> </p>
                    <p><label>Request for</label><asp:DropDownList ID="ddlStore" runat="server" Enabled="false"></asp:DropDownList></p>
              </div>
                <div class="content-r w-50">
                	<p><label>Delivery Location</label><asp:DropDownList ID="ddlLocation" 
                            runat="server" Enabled="false" ></asp:DropDownList></p>
                    <p>
                        <label>Delivery Date</label>
                        <asp:TextBox ID="txtDate" runat="server" Enabled="false"></asp:TextBox>
                        <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="20px" Height="20px" Enabled="false">
                        </asp:ImageButton>
                        <ajaxToolkit:CalendarExtender ID="ceDate" runat="server" EnableViewState="False"
                            Format="dd-MMM-yyyy" PopupButtonID="ibDate" TargetControlID="txtDate">
                        </ajaxToolkit:CalendarExtender>
                    </p>
              </div>
                <hr>               
              <div class="clr"></div>
                <div class="form2">
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                  <tbody>
                  <tr class="row-gray">
                    <td width="11%">Supplier</td>
                    <td width="12%">Product</td>
                    <td width="11%">Quantity</td>
                    <td width="11%">Previous Price</td>
                    <td width="11%">Quoted Unit Price</td>
                    <td width="11%">Total Amount</td>
                    <td width="11%">Payment Mode</td>
                    <td width="11%">Payment Term</td>
                    <td width="11%" colspan="2" align="center">Action</td>
                  </tr>
                  <tr>
                    <td><asp:DropDownList ID="ddlSupplier" runat="server"></asp:DropDownList></td>
                    <td><asp:DropDownList ID="ddlProduct" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtQuantity" runat="server" onblur="calcAmount();"></asp:TextBox> </td>
                    <td><asp:TextBox ID="txtPreviousPrice" runat="server" Text="0" Enabled="false"></asp:TextBox></td>                    
                    <td><asp:TextBox ID="txtQuotedPrice" runat="server" onblur="calcAmount();"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtAmount" runat="server" Enabled="false"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlPaymentMode" runat="server">
                            <asp:ListItem Text="Cash" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Credit" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td><asp:TextBox ID="txtPaymentTerm" runat="server"></asp:TextBox></td>
                    <td align="center" valign="middle" colspan="2">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add" onclick="btnAdd_Click" />
                    </td>
                  </tr>
                  <tr>
                  	<td colspan="5" class="line-h"></td>
                  </tr>
                </tbody>
                </table>
                <br />
                <asp:GridView ID="gvProduct" runat="server" Width="97%" CssClass="table2" 
                        AutoGenerateColumns="False" ShowHeader="false" 
                    CellPadding="0" CellSpacing="0" OnRowDeleting="gvProduct_RowDeleting" OnRowEditing="gvProduct_RowEditing" >                                    
                                    <Columns>
                                        <asp:BoundField DataField="SKU_HIE_ID" HeaderText="SKU_HIE_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>                                              
                                        <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Principal">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="11%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                                                          
                                        <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="12%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="11%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                         
                                        <asp:BoundField DataField="PRICE_PREVIOUS" HeaderText="Previous Price" DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="11%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRICE" HeaderText="Quoted Price" DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="11%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Total Amount" DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="11%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentModeID" HeaderText="PaymentModeID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="11%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaymentTerm" HeaderText="Payment Term">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="11%">
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
                <hr>
                <div class="content-r w-50">
                	<p><label>Reviewed by</label><asp:TextBox ID="txtReviewedBy" runat="server"></asp:TextBox></p>                    
                </div>
                <div class="content-r w-50">
                	<p><label>Approved by</label><asp:TextBox ID="txtApprovedBy" runat="server"></asp:TextBox></p>
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

