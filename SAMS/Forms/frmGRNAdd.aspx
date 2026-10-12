<%@ Page Title="Goods Receipt Note" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmGRNAdd.aspx.cs" Inherits="Forms_frmGRNAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<script language="JavaScript" type="text/javascript">
    function CheckReceived(ordered, received, current) {
    
        
        if (ordered.value > 0) {
        }
        else {
            ordered.value = 0;
        }
        if (received.value > 0) {
        }
        else {
            received.value = 0;
        }

        if (current.value > 0) {
        }
        else {
            current.value = 0;
        }
        if (parseFloat(received.value) + parseFloat(current.value) > parseFloat(ordered.value)) {
            alert('Received quantity can not be greater than ' + (parseFloat(ordered.value) - parseFloat(received.value)));
            current.value = '0';            
        }        
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
                    <p><label>Supplier</label><asp:DropDownList ID="ddlSupplier" runat="server" 
                            AutoPostBack="True" onselectedindexchanged="ddlSupplier_SelectedIndexChanged"></asp:DropDownList></p>
                    <p>
                        <label>PR Ref </label>
                        <asp:DropDownList ID="ddlRequisition" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlRequisition_SelectedIndexChanged"></asp:DropDownList>
                    </p>
              </div>
                <div class="content-r w-50">
                	<p><label>Delivery Location</label><asp:DropDownList ID="ddlLocation" 
                            runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList></p>                   
              </div>
                <hr>               
              <div class="clr"></div>
                <div class="form2">
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                  <tbody>
                  <tr class="row-gray">
                    <td width="40%">Product</td>
                    <td width="20%">Ordered Quantity</td>
                    <td width="20%">Received Quantity</td>
                    <td width="20%">Current Received</td>
                  </tr>                  
                  <tr>
                  	<td colspan="5" class="line-h"></td>
                  </tr>
                </tbody>
                </table>
                <br />
                <asp:GridView ID="gvProduct" runat="server" Width="97%" CssClass="table2" 
                        AutoGenerateColumns="False" ShowHeader="false" 
                    CellPadding="0" CellSpacing="0" onrowdatabound="gvProduct_RowDataBound" >                                    
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
                                        <asp:TemplateField HeaderText="Ordered Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOrdered" runat="server" Width="100%" Text='<%#Eval("Quantity") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20%">
                                            </ItemStyle>
                                        </asp:TemplateField>                                                                 
                                        <asp:TemplateField HeaderText="Received Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReceived" runat="server" Width="100%" Text='<%#Eval("Received") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20%">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Received">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCurrent" runat="server" Width="100%" Text='<%#Eval("Current") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20%">
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

