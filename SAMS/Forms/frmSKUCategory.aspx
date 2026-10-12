<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUCategory.aspx.cs" Inherits="frmSKUCategory" Title="Inventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
<script language="JavaScript" type="text/javascript">
    function pageLoad() {
        //find the current popup
        var popUp = $find('ModelPopup');

        //check it exists so the script won't fail
        if (popUp) {
            //Add the function below as the event
            popUp.add_hidden(HidePopupPanel);
        }
    }

    function HidePopupPanel(source, args) {
        //find the panel associated with the extender
        objPanel = document.getElementById(source._PopupControlID);

        //check the panel exists
        if (objPanel) {
            //set the display attribute, so it remains hidden on postback
            objPanel.style.display = 'none';
        }
    }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="btnCancelQuality" DropShadow="true" 
                    PopupControlID="pnlParameters" TargetControlID="dummyLink1" BehaviorID="ModelPopup">
                </cc1:ModalPopupExtender>
                <div class="content-r w-50">
                <asp:Panel ID="pnlParameters" runat="server" Style="display: none; background-color: White;"
                                            ScrollBars="Vertical" Height="500px">
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
                                                <asp:Button ID="btnCancelQuality" runat="server" CssClass="btn" Text="Discard"/>
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
                                                        <asp:BoundField DataField="SKU_QUALITY_ID" HeaderText="SKU_QUALITY_ID">
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            <HeaderStyle CssClass="HidePanel" />
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
                 <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink1" runat="server">dummy</a>
                </ContentTemplate>
                </asp:UpdatePanel>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
            <ContentTemplate>
                <table style="width:100%;">
                <tr>
                <td style="height:20px;">
                </td>
                </tr>
                <tr>
                <td>
                <cc1:TabContainer ID="tbUserAssignment" runat="server" Width="100%" Height="450px"
                    AutoPostBack="True" OnActiveTabChanged="tbUserAssignment_ActiveTabChanged">
                    <cc1:TabPanel runat="server" ID="TabPanel1" TabIndex="0">
                        <HeaderTemplate>
                            Main Type
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="form-cont w-60">
                                <div class="content-r">
                                    <p>
                                        <label>
                                            Category Type
                                        </label>
                                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                            <asp:ListItem Text="Finish" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Raw" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                        <label>
                                            Category ID</label><asp:TextBox ID="txtCategoryCode" runat="server" Enabled="False"
                                                placeholder="Category Code"></asp:TextBox></p>
                                    <p>
                                        <label>
                                            Category Name</label><asp:TextBox ID="txtCategoryName" runat="server" Enabled="False"
                                                placeholder="Category Name"></asp:TextBox></p>
                                </div>
                                <div class="form-bottom">
                                    <p>
                                        <asp:Button ID="btnSaveCategory" OnClick="btnSaveCategory_Click" runat="server" Text="New"
                                            CssClass="btn" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Discard" CssClass="btn" />
                                    </p>
                                </div>
                                <hr />
                                <div class="form2">
                                    <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                                        <tr class="row-gray">
                                            <td style="width: 25%;">
                                                Code
                                            </td>
                                            <td style="width: 55%;">
                                                Name
                                            </td>
                                            <td colspan="2" align="center" style="width: 20%;">
                                                Action
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GrdCategory" runat="server" Width="97%" CssClass="table2" AutoGenerateColumns="False"
                                        ShowHeader="false" OnRowEditing="GrdCategory_RowEditing" OnRowDeleting="GrdCategory_RowDeleting"
                                        CellPadding="0" CellSpacing="0">
                                        <Columns>
                                            <asp:BoundField DataField="SKU_HIE_ID" HeaderText="Id" Visible="False">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                </ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                    Width="25%"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                    Width="55%"></ItemStyle>
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
                                    <p class="add-record">
                                        <a href="#">&nbsp;</a></p>
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" ID="TabPanel2" TabIndex="1">
                        <HeaderTemplate>
                            Sub Type
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="form-cont w-60">
                                <div class="content-r">
                                    <p>
                                        <label>
                                            Category Type
                                        </label>
                                        <asp:DropDownList ID="ddlCategoryType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryType_SelectedIndexChanged">
                                            <asp:ListItem Text="Finish" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Raw" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                        <label>
                                            Main Type
                                        </label>
                                        <asp:DropDownList ID="ddlMainType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMainType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </p>
                                    <p>
                                        <label>
                                            Category ID</label><asp:TextBox ID="txtSubCategoryCode" runat="server" Enabled="False"
                                                placeholder="Category Code"></asp:TextBox></p>
                                    <p>
                                        <label>
                                            Category Name</label><asp:TextBox ID="txtSubCategoryName" runat="server" Enabled="False"
                                                placeholder="Category Name"></asp:TextBox></p>
                                </div>
                                <div class="form-bottom">
                                    <p>
                                        <asp:Button ID="btnSaveSubCategory" OnClick="btnSaveSubCategory_Click" runat="server"
                                            Text="New" CssClass="btn" />
                                        <asp:Button ID="btnDiscardSubCategory" runat="server" Text="Discard" CssClass="btn" />
                                    </p>
                                </div>
                                <hr />
                                <div class="form2">
                                    <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                                        <tr class="row-gray">
                                            <td style="width: 25%;">
                                                Code
                                            </td>
                                            <td style="width: 55%;">
                                                Name
                                            </td>
                                            <td colspan="2" align="center" style="width: 20%;">
                                                Action
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="grdSubCategory" runat="server" Width="97%" CssClass="table2" AutoGenerateColumns="False"
                                        ShowHeader="false" OnRowEditing="grdSubCategory_RowEditing" OnRowDeleting="grdSubCategory_RowDeleting"
                                        CellPadding="0" CellSpacing="0">
                                        <Columns>
                                            <asp:BoundField DataField="SKU_HIE_ID" HeaderText="Id" Visible="False">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                </ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                    Width="25%"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                    Width="55%"></ItemStyle>
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
                                    <p class="add-record">
                                        <a href="#">&nbsp;</a></p>
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" ID="TabPanel3" TabIndex="2">
                        <HeaderTemplate>
                            Product
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="form-cont">
                                <div id="mainContent">
                                    <div class="clr">
                                    </div>
                                    <div class="content-r w-50">
                                        
                                        <p>
                                            <label>
                                                Select Type</label>
                                            <asp:DropDownList ID="ddlType_ProductDefination" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_ProductDefination_SelectedIndexChanged">
                                                <asp:ListItem Text="Finish" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Raw" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </p>
                                        <p>
                                            <label>
                                                Sub Category</label>
                                            <asp:DropDownList ID="ddlProductSubCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductSubCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </p>
                                        <p>
                                            <label>
                                                Product Code</label>
                                            <asp:TextBox ID="txtskucode" runat="server" placeholder="Product Code"></asp:TextBox>
                                        </p>
                                        <p>
                                            <label>
                                                Tax Type</label>
                                            <asp:DropDownList ID="ddlTax" runat="server">
                                                <asp:ListItem Value="T">Trade Price</asp:ListItem>
                                                <asp:ListItem Value="R">Retail Price</asp:ListItem>
                                                <asp:ListItem Value="E">Exempted</asp:ListItem>
                                            </asp:DropDownList>
                                        </p>
                                        <p>
                                            <label>
                                                Pack Size</label>
                                            <asp:TextBox ID="txtPackSize" runat="server" placeholder="Pack Size"></asp:TextBox>
                                        </p>
                                    </div>
                                    <div class="content-r w-50">
                                        <p>
                                            <label>
                                                Product Category</label>
                                            <asp:DropDownList ID="ddskucategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddskucategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </p>
                                        <p>
                                            <label>
                                                Product Name</label>
                                            <asp:TextBox ID="txtskuname" runat="server" placeholder="Product Name"></asp:TextBox>
                                        </p>
                                        <p>
                                            <label>
                                                Mini. Stock Level</label>
                                            <asp:TextBox ID="txtMinStock" runat="server" placeholder="Min. Stock Level"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                TargetControlID="txtMinStock">
                                            </cc1:FilteredTextBoxExtender>
                                        </p>
                                        <p>
                                            <label>
                                                Unit in Case</label>
                                            <asp:TextBox ID="txtunitincase" runat="server" placeholder="Unit in Case"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                TargetControlID="txtunitincase">
                                            </cc1:FilteredTextBoxExtender>
                                        </p>
                                        <p>
                                            <label>
                                                UOM</label>
                                            <asp:DropDownList ID="ddlUOM" runat="server">
                                            </asp:DropDownList>
                                        </p>
                                    </div>
                                    <div class="form-bottom">
                                        <p2> 
                        <asp:Button ID="btnSave_ProductDefination" runat="server" CssClass="btn" OnClick="btnSave_ProductDefination_Click" Text="Save" />
                        <asp:Button ID="btnCancel_ProductDefination" runat="server" CssClass="btn"  Text="Discard"/>
                    </p2>
                                    </div>
                                    <hr>
                                    <div class="form2">
                                        <table class="table1" border="0" cellpadding="0" cellspacing="0" width="97%">
                                            <tr class="row-gray">
                                                <td style="width: 15%;">
                                                    Code
                                                </td>
                                                <td style="width: 40%;">
                                                    Name
                                                </td>
                                                <td style="width: 5%;">
                                                    Tax Type
                                                </td>
                                                <td style="width: 5%;">
                                                    Min. Stock
                                                </td>
                                                <td style="width: 10%;">
                                                    Pack Size
                                                </td>
                                                <td style="width: 5%;">
                                                    Unit in Case
                                                </td>
                                                <td colspan="3" align="center" style="width: 20%;">
                                                    Action
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="grdSKUData" runat="server" Width="97%" CssClass="table2" AutoGenerateColumns="False"
                                            ShowHeader="false" CellPadding="0" CellSpacing="0" OnRowEditing="grdSKUData_RowEditing"
                                            OnRowDeleting="grdSKUData_RowDeleting" OnRowCommand="grdSKUData_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="Category_Id" HeaderText="Category_Id">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_CODE" HeaderText="Code">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                        Width="15%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_NAME" HeaderText="Name">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                        Width="40%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GST_ON" HeaderText="Name">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                        Width="5%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MIN_STOCK" HeaderText="Min Stock">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                        Width="5%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PACKSIZE" HeaderText="Pack Size">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                        Width="10%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UNITS_IN_CASE" HeaderText="Unit in Case">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                        Width="5%"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="UOM_ID" HeaderText="UOM_ID in Case">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                        Width="5%"></ItemStyle>
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
                                                <asp:TemplateField HeaderText="Detail">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDetail" runat="server" CommandName="detail" Text="View Detail"
                                                            CommandArgument='<%# Eval("SKU_ID")%>'>                                                    
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
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                </td>
                </tr>
                </table>
                
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
