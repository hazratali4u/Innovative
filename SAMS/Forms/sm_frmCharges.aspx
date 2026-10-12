<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="sm_frmCharges.aspx.cs" Inherits="Forms_sm_frmCharges" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
     <div id="right_data">
        <asp:UpdatePanel ID="upPopup" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                         <td>
                            </td>
                        <td width="100px">
                            <strong>
                                <label>
                                   Charge Type</label></strong>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbChargestype" runat="server" RepeatDirection="Horizontal"
                                onselectedindexchanged="rbChargestype_SelectedIndexChanged" >
                                 
                            <asp:ListItem Text="Transport" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Hostel"  Value="2"></asp:ListItem>

                            </asp:RadioButtonList >
                        </td>
                    </tr>
                     <tr>
                            <td>
                            </td>
                            <td>
                                <strong>
                                    <label>Program Study</label></strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpStudyPrograms" runat="server" Width="200px" AutoPostBack="true"
                                    OnSelectedIndexChanged="DrpStudyPrograms_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                               <strong>
                                    <label>Session</label></strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpSession" runat="server" Width="120px" AutoPostBack="true"
                                    OnSelectedIndexChanged="DrpSession_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <td>
                            </td>
                             <td>
                                <strong>
                                    <label>Student</label></strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpStudent" runat="server" Width="200px" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <td>
                            </td>
                             <td>
                                <strong>
                                    <label>Amount</label></strong>
                            </td>
                            <td>
                                
                                <asp:TextBox ID="txtAmount" runat="server" Width="200px" >
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtAmount"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                </table><br/>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSaveCharges" runat="server" CssClass="Button" Text="Save" CausesValidation="True"
                                OnClick="btnSaveCharges_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelCharges" runat="server" CssClass="Button" Text="Cancel " CausesValidation="False"/>
                        </td>
                    </tr>
                </table><br/>
              
                <asp:Panel ID="pnlParameters" runat="server"  BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" ScrollBars="Vertical" Height="200px" Width="70%">
                   
                    <asp:GridView ID="grdStudentCharge" runat="server" Width="99.9%"  AutoGenerateColumns="False"
                        OnRowEditing="grdStudentCharge_RowEditing" 
                        CssClass="gridRow2" BorderColor="White"  HorizontalAlign="Center"
                        OnRowDeleting="grdStudentCharge_RowDeleting" HeaderStyle="">
                        <HeaderStyle BackColor="SteelBlue" ForeColor="White"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="CHARGE_ID">
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                <HeaderStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STUDENT_ID">
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                <HeaderStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SESSION_ID">
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                <HeaderStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PROGRAM_ID">
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                <HeaderStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STUDENT_NAME" HeaderText="Student Name">
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                    Width="40%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PROGRAM_NAME" HeaderText="Program Name">
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                    Width="30%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SESSION" HeaderText="Session">
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                    Width="30%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CHARGE_AMOUNT" HeaderText="AMOUNT" DataFormatString="{0:F2}">
                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                    Width="15%"></ItemStyle>
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

