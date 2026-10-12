<%@ Page Title="Purchase Invoice" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmPurchaseInvoiceAdd.aspx.cs" Inherits="Forms_frmPurchaseInvoiceAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<script language="JavaScript" type="text/javascript">
    function CalTotal(received, rate,gross,discount,taxper,taxamount,netamount) {
    
        
        if (received.value > 0) {
        }
        else {
            received.value = 0;
        }
        if (rate.value > 0) {
        }
        else {
            rate.value = 0;
        }

        if (gross.value > 0) {
        }
        else {
            gross.value = 0;
        }

        if (discount.value > 0) {
        }
        else {
            discount.value = 0;
        }
        if (taxper.value > 0) {
        }
        else {
            taxper.value = 0;
        }
        if (taxamount.value > 0) {
        }
        else {
            taxamount.value = 0;
        }
        if (netamount.value > 0) {
        }
        else {
            netamount.value = 0;
        }

        gross.value = (parseFloat(received.value) * parseFloat(rate.value)).toFixed(2);
        taxamount.value = ((parseFloat(received.value) * parseFloat(rate.value) - discount.value) * taxper.value / 100).toFixed(2);
        netamount.value = (parseFloat(gross.value) - parseFloat(discount.value) + parseFloat(taxamount.value)).toFixed(2);
        
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

                            <p><label>Purchase for</label><asp:DropDownList ID="ddlLocation" runat="server" Enabled="false"></asp:DropDownList></p>                   
              </div>
                <div class="content-r w-50">
                	<p>
                        <label>PR Ref </label>
                        <asp:DropDownList ID="ddlRequisition" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlRequisition_SelectedIndexChanged"></asp:DropDownList>
                    </p>
                    <p>
                        <label>Ref #</label>
                        <asp:TextBox ID="txtRef" runat="server"></asp:TextBox>
                    </p>
              </div>
              <div class="content-r w-50">
                	<p>
                         <label>GRN # </label>                        
                        <asp:CheckBoxList ID="lbGRN" runat="server" Width="200px" BorderWidth="1px"></asp:CheckBoxList>                                                  
                        <asp:Button ID="btnShow" CssClass="btn" runat="server" Text="Get Data" Visible="false" onclick="btnShow_Click" Width="100px" />
                    </p>                    
              </div>

              <div class="content-r clr">
                	<p><label>Remarks</label><asp:TextBox ID="txtRemarks" runat="server" Width="84%" TextMode="MultiLine"></asp:TextBox> </p>
                    
                </div>
                <hr>               
              <div class="clr"></div>
                <div class="form2">
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                  <tbody>
                  <tr class="row-gray">
                    <td width="20%">Product</td>
                    <td width="10%">Ordered Quantity</td>
                    <td width="10%">Received Quantity</td>
                    <td width="10%">Rate</td>
                    <td width="10%">Gross Amount</td>
                    <td width="10%">Discount</td>
                    <td width="10%">S.Tax(%)</td>
                    <td width="10%">S.Tax(Rs)</td>
                    <td width="10%">Net Amount</td>
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
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="20%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Ordered Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOrdered" runat="server" Width="100%" Text='<%#Eval("Quantity") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                        </asp:TemplateField>                                                                 
                                        <asp:TemplateField HeaderText="Received Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReceived" runat="server" Width="100%" Text='<%#Eval("Received") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRate" runat="server" Width="100%" Text='<%#Eval("Rate") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                        </asp:TemplateField>                                                                                
                                        <asp:TemplateField HeaderText="Gross">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGross" runat="server" Width="100%" Text='<%#Eval("Gross") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDiscount" runat="server" Width="100%" Text='<%#Eval("Discount") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax(%)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTaxPer" runat="server" Width="100%" Text='<%#Eval("TaxPer") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax(Rs)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTaxAmount" runat="server" Width="100%" Text='<%#Eval("TaxAmount") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNetAmount" runat="server" Width="100%" Text='<%#Eval("NetAmount") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                
                </div>
                <hr>
                
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

