<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmQuotationAdd.aspx.cs" Inherits="Forms_frmQuotationAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="btnCancelQuality" DropShadow="true"
                    PopupControlID="pnlParameters" TargetControlID="dummyLink1" BehaviorID="ModelPopup">
                </ajaxToolkit:ModalPopupExtender>
                <div class="content-r w-50">
                    <asp:Panel ID="pnlParameters" runat="server" Style="display: none; background-color: White;"
                        ScrollBars="Auto" CssClass="popu-View">
                        <p>
                            <asp:LinkButton ID="btnCorss" runat="server" Style="float: right;"><img src="../images/cancel.png" alt="cancel" style="width:15px;height:15px;"  /> </asp:LinkButton>
                        </p>
                        <asp:HiddenField ID="hfSKUID" runat="server" />
                        <p>
                            <br />
                        </p>
                        <p>
                            <h1>
                                <asp:Label ID="lblSKUName" runat="server" Text="SKU Name"></asp:Label></h1>
                        </p>
                        <p>
                            <br />
                        </p>
                        <p>
                            <label>
                                Param Desc.</label>
                            <asp:TextBox ID="txtQualityDescription" runat="server" placeholder="Paramter Description"></asp:TextBox>
                        </p>
                        <p>
                            <label>
                                Param Value</label>
                            <asp:TextBox ID="txtQualityValue" runat="server" placeholder="Parameter Value"></asp:TextBox>
                        </p>
                        <div class="form-bottom">
                            <p2> 
                                               <asp:Button ID="btnCancelQuality" runat="server" CssClass="btn" Text="Finish "/>
                                            </p2>
                        </div>
                        <div class="form2">
                            <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                                <tr class="row-gray">
                                    <td style="width: 40%;">
                                        Description
                                    </td>
                                    <td style="width: 40%;">
                                        Value
                                    </td>
                                    <td colspan="3" align="center" style="width: 20%;">
                                        Action
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfSKUQualityID" runat="server" />
                            <asp:GridView ID="gvQuality" runat="server" Width="97%" CssClass="table2" AutoGenerateColumns="False"
                                ShowHeader="false" CellPadding="0" CellSpacing="0"
                                >
                                <Columns>
                                    <asp:BoundField DataField="QUALITY_DESCRIPTION" HeaderText="Description">
                                        <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                            Width="40%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QUALITY_VALUE" HeaderText="Value">
                                        <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                            Width="40%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                   
                                        
                                </Columns>
                            </asp:GridView>
                            <div>
                                <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink"
                                    runat="server">dummy</a>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink1"
                    runat="server">dummy</a>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="mainContent">
                    <div class="content">
                        <article class="topcontent">	
					<div class="form-cont">
            <div id="mainContent">
            	<div class="clr"></div>
				<div class="content-r w-50">
                <p><label>Purchase Req.No</label><asp:DropDownList ID="DrpPurchaseReq" AutoPostBack="true" runat="server"></asp:DropDownList> </p>
                	<p><label>Orignitor</label><asp:TextBox ID="txtOriginator" runat="server"></asp:TextBox> </p>
                    <p><label>Request for</label><asp:DropDownList ID="ddlStore" runat="server"></asp:DropDownList></p>
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
                    <td width="30%">Product</td>
                    <td width="22%">Quantity</td>
                    <td width="22%">Quot.Price</td>
                    <td colspan="2" align="center">
                     <asp:Button ID="btnQC" runat="server" CssClass="btn" Width="100px" Text=" View QC" onclick="btnQC_Click" /></td>
                  </tr>
                 <%-- <tr>
                    <td><asp:DropDownList ID="ddlProduct" runat="server" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox> </td>
                    <td><asp:TextBox ID="txtCost" runat="server"></asp:TextBox></td>
                   
                  </tr>--%>
                  <tr>
                  	<td colspan="5" class="line-h"></td>
                  </tr>
                </tbody>
                </table>
                <br />
                <asp:GridView ID="gvProduct" runat="server" Width="97%" CssClass="table2" AutoGenerateColumns="False" ShowHeader="false" 
                    CellPadding="0" CellSpacing="0" >                                    
                                    <Columns>
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>                                                                                
                                        <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="30%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="22%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                         
                                       <asp:TemplateField HeaderText="Quot. Price">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="100px" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRcdPrice" runat="server" Width="70px" Text='<%#Eval("QUOTATION_PRICE") %>'
                                                                             Height="20px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                
                                       
                                    </Columns>
                                </asp:GridView>
                <br />
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                    <tr>
                    <td colspan="5">                    
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" placeholder="Remarks / Terms &amp; Conditions"></asp:TextBox>
                    </td>
                  </tr>
                </table>
				
                </div>
                <hr>
                
                <div class="content-r w-50">
                	<p><label>Send RFQ to Supplier</label><asp:DropDownList ID="ddlSupplier" runat="server"></asp:DropDownList></p>
                   
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
