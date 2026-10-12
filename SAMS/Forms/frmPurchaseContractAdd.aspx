<%@ Page Title="Purchase Requisition" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmPurchaseContractAdd.aspx.cs" Inherits="Forms_frmPurchaseContractAdd"
    EnableViewState="true" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {
            var str;
            var str1;
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            str1 = document.getElementById('<%=txtCost.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }
            else if (str1 == null || str1.length == 0) {
                alert('Must Enter Rate');
                return false;
            }
            return true;
        }
        function functionx(evt, txt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57) && evt.charCode != 46) {
                //alert("Allow Only Numbers");
                // alert(evt.charCode);
                return false;
            }
            else if (txt.value.indexOf('.') != -1 && evt.charCode == 46) {
                return false;
            }
            else {
                return true;
            }
        }
        function CalculateAmount() {
            var str;
            var str1;
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            str1 = document.getElementById('<%=txtCost.ClientID%>').value;
            document.getElementById('<%=txtAmount.ClientID%>').value = str * str1;
        }
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="btnCancelQuality" DropShadow="true" 
                    PopupControlID="pnlParameters" TargetControlID="dummyLink1" BehaviorID="ModelPopup">
                </ajaxToolkit:ModalPopupExtender>
                <div class="content-r w-50">
                    <asp:Panel ID="pnlParameters" runat="server" Style="display: none; background-color: White;" ScrollBars="Auto" CssClass="popu-View">
                        <p> 
                        <asp:LinkButton ID="btnCorss" runat="server" style="float:right;" ><img src="../images/cancel.png" alt="cancel" style="width:15px;height:15px;"  /> </asp:LinkButton>
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
                                                <asp:Button ID="btnSaveQuality" runat="server" CssClass="btn" Text="Save" OnClick="btnSaveQuality_Click"/>
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
                                ShowHeader="false" CellPadding="0" CellSpacing="0" OnRowEditing="gvQuality_RowEditing"
                                OnRowDeleting="gvQuality_RowDeleting">
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
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                Text="Delete">
                                                    <img src="../images/icon-del.jpg" height="15" width="15">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                        </ItemStyle>
                                    </asp:TemplateField>
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
                	<p><label style="width:20%">Supplier</label><asp:DropDownList ID="ddlSupplier" runat="server" style="width:70%;"></asp:DropDownList></p>
                    <p><label style="width:20%">Purchase Date</label>
                        <asp:TextBox ID="txtPurchaseDate" runat="server" style="width:68%"></asp:TextBox>
                        <asp:ImageButton ID="imgPurchaseDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" style="width:5%; height:20px;">
                        </asp:ImageButton>
                        <ajaxToolkit:CalendarExtender ID="cePurchaseDate" runat="server" EnableViewState="False"
                            Format="dd-MMM-yyyy" PopupButtonID="imgPurchaseDate" TargetControlID="txtPurchaseDate">
                        </ajaxToolkit:CalendarExtender></p>
              </div>
                <div class="content-r w-50">
                	<p><label style="width:20%">Location</label><asp:DropDownList ID="ddlLocation" 
                            runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlLocation_SelectedIndexChanged" style="width:70%"></asp:DropDownList></p>
                    <p>
                        <label style="width:20%">Delivery Date</label>
                        <asp:TextBox ID="txtDate" runat="server" style="width:68%"></asp:TextBox>
                        <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" style="width:5%;height:20px;">
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
                    <td width="30%">Product</td>
                    <td width="15%">Price</td>
                    <td width="15%">Quantity</td>
                    <td width="15%">Amount</td>
                    <td colspan="2" align="center" width="25%">Action</td>
                  </tr>
                  <tr>
                    <td><asp:DropDownList ID="ddlProduct" runat="server" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtCost" runat="server" onkeypress="return functionx(event,this)" onchange="CalculateAmount()" ></asp:TextBox> </td>
                    <td><asp:TextBox ID="txtQuantity" runat="server" onkeypress="return functionx(event,this)" onchange="CalculateAmount()"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtAmount" runat="server"></asp:TextBox> </td>
                    <td align="center" valign="middle" >
                        <asp:Button ID="btnQC" runat="server" CssClass="btn" Text="QC" onclick="btnQC_Click" />
                    </td>
                    <td align="center" valign="middle" >
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add" onclick="btnAdd_Click" />
                    </td>
                  </tr>
                  <tr>
                  	<td colspan="5" class="line-h"></td>
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
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="30%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRICE" HeaderText="PRICE"  DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                         
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" DataFormatString="{0:0.00}">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" DataFormatString="{0:0.00}">
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
                <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                    <tr>
                    <td colspan="5">                    
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" placeholder="Remarks / Terms &amp; Conditions"></asp:TextBox>
                    </td>
                  </tr>
                </table>
				
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
