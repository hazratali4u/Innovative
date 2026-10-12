<%@ Page Title="Purchase Requisition" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmRequisitionView.aspx.cs" Inherits="Forms_frmRequisitionView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
    <div id="right_data">    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div class="mainContent">
	<div class="content">	
    <div class="form-cont" style="border-style:none;">
		<article class="topcontent">	
            <div id="mainContent">
              <div class="clr"></div>
                <div class="form2">
                <asp:GridView ID="gvRequisition" runat="server" Width="100%" CssClass="table2" AutoGenerateColumns="False"  
                    CellPadding="0" CellSpacing="0" onrowdatabound="gvRequisition_RowDataBound" onrowcommand="gvRequisition_RowCommand"
                    EmptyDataText="<div style='padding:10px; margin:10px; background-color:#fbefef; border: Solid 1px #bb0000; color:#bb0000;'>There is no purchase requisition to display.</div>">
                    <HeaderStyle CssClass="row-gray" Height="30px" /> 
                    <RowStyle CssClass="row-whit" />

                                                  
                                    <Columns>
                                        <asp:BoundField DataField="REQUISITION_MASTER_ID" HeaderText="REQUISITION_MASTER_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REQUISITION_VIEW_ID" HeaderText="REQUISITION_VIEW_ID">
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>                                                                                
                                        <asp:BoundField DataField="ORIGINATOR_NAME" HeaderText="Orignitor">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RequestFor" HeaderText="Request for">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                         
                                        <asp:BoundField DataField="RequestDate" HeaderText="Request Date" DataFormatString="{0:dd-MM-yyyy}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DELIVERY_DATE" HeaderText="Delivery Date" DataFormatString="{0:dd-MM-yyyy}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                             <asp:BoundField DataField="APPROVED_BY" HeaderText="Approved by" DataFormatString="{0:dd-MM-yyyy}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Allocate To" >
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAllocateTo" runat="server" CommandName="Allocate"  Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlAllocateTo_SelectedIndexChanged">
                                                   
                                                </asp:DropDownList>                                                
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Priority">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlPriority" runat="server" CommandName="Priority"  Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlPriority_SelectedIndexChanged">
                                                   <asp:ListItem Text="Default" Value="0"></asp:ListItem>
                                                   <asp:ListItem Text="High" Value="1"></asp:ListItem>
                                                   <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                                                   <asp:ListItem Text="Low" Value="3"></asp:ListItem>
                                                </asp:DropDownList>                                                
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="10%">
                                            </ItemStyle>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="View" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="View" CommandArgument='<%# Eval("REQUISITION_MASTER_ID") %>'  Width="100%" >
                                                    <img src="../images/view.gif" height="15" width="15">
                                                </asp:LinkButton>                                                
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="5%">
                                            </ItemStyle>
                                        </asp:TemplateField>               
                                    </Columns>
                                </asp:GridView>                  				
                </div>
			</div>
				</article>
                </div>
				
		</div>
</div>
 <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="true" PopupControlID="pnlView" TargetControlID="hdnDumy" CancelControlID="btnOK"  >
    </ajaxToolkit:ModalPopupExtender>
    <asp:HiddenField ID="hdnDumy" runat="server" />

 <asp:Panel ID="pnlView" runat="server" Style="display:none;" ScrollBars="Auto" CssClass="popu-View">
      <table width="100%">
      <tr>
      <td colspan="2" align="right">
      <asp:LinkButton ID="btnOK" runat="server" CausesValidation="false"><img src="../images/cancel.png" height="20" width="20"></asp:LinkButton>
      </td>
      </tr>
      <tr>
      <td align="center" colspan="2" style="font-size:20px; font-weight:bold;" >
 	Requisition Detailed View     
      </td>
      </tr>
      <tr>
      <td>
      <label style="font-weight:bold" >Originator : </label><asp:Label ID="lblOriginator" runat="server"></asp:Label>
      </td>
   <td>
      <label style="font-weight:bold" >Approved by : </label><asp:Label ID="lblApprovedBy" runat="server"></asp:Label>
      </td>
      </tr>

       <tr>
      <td>
      <label style="font-weight:bold" >Request for : </label><asp:Label ID="lblRequestFor" runat="server"></asp:Label>
      </td>
      <td>
      <label style="font-weight:bold" >Delivery Date : </label><asp:Label ID="lblDeliveryDate" runat="server"></asp:Label>
      </td>
      </tr>
      <tr>
      <td colspan="2">
      <asp:GridView ID="gvProduct" runat="server" Width="97%" CssClass="table2" AutoGenerateColumns="False" CellPadding="0" CellSpacing="0">                                    
                                    <HeaderStyle CssClass="row-gray" />
                                    <RowStyle CssClass="row-whit" />
                                    <Columns>                                                                        
                                        <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="50%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="25%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                         
                                        <asp:BoundField DataField="PRICE" HeaderText="PRICE" DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="25%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
      
      </td>
      </tr>
      <tr>
      <td colspan="2">
      <label style="font-weight:bold" >Remarks : </label><asp:Label ID="lblRemarks" runat="server"></asp:Label>
      </td>
      </tr>

       <tr>
      <td>
      <label style="font-weight:bold" >Reviewed by : </label><asp:Label ID="lblReviewedby" runat="server"></asp:Label>
      </td>
      </tr>
      </table>
        
        
    </asp:Panel>
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnOK" />
            </Triggers>
        </asp:UpdatePanel>
</div>
</asp:Content>

