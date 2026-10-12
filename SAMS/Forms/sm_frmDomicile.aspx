<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="sm_frmDomicile.aspx.cs" Inherits="Forms_sm_frmDomicile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <asp:UpdatePanel ID="upPopup" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td width="80px">
                            <strong>
                                <label>
                                    City Name</label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCityName" runat="server" Width="150px" placeholder="Plz enter City Name"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCityName">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table><br/>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSaveDomicile" runat="server" CssClass="Button" Text="Save" CausesValidation="True"
                                OnClick="btnSaveDomicile_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelDomicile" runat="server" CssClass="Button" Text="Cancel" CausesValidation="False"/>
                        </td>
                    </tr>
                </table><br/>
              
                <asp:Panel ID="pnlParameters" runat="server"  BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" ScrollBars="Vertical" Height="200px" Width="40%">
                   
                    <asp:GridView ID="gvDomicile" runat="server" Width="99.9%"  AutoGenerateColumns="False"
                        OnRowEditing="gvDomicile_RowEditing" 
                        CssClass="gridRow2" BorderColor="White"  HorizontalAlign="Center"
                        OnRowDeleting="gvDomicile_RowDeleting" HeaderStyle="">
                        <HeaderStyle BackColor="SteelBlue" ForeColor="White"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="CITY_ID">
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                <HeaderStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CITY_NAME" HeaderText="City Name">
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                    Width="80%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit" CausesValidation="False"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" Visible="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                        Text="Delete">
                                                <img src="../images/icon-del.jpg" height="15" width="15" alt="">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                </ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
