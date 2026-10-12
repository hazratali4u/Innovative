<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmExpenseEnteryForm.aspx.cs" Inherits="pr_frmExpenseEnteryForm" Title="Expense Entery Form" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<%@ Register src="UserControl/MultiCheckCombo.ascx" tagname="MultiCheckCombo" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <style>
        .customerSerch input
        {
            -moz-box-shadow: inset 0 0 6px #333;
            -webkit-box-shadow: inset 0 0 6px #333;
            box-shadow: inset 0 0 6px #333;
            -moz-border-radius: 3px;
            height: 26px;
            float: left;
            padding-left: 5px;
            border: none;
            color: #666;
            margin-top: 10px;
            margin-left: 10px;
            border-radius: 3px;
        }
        .customerSerch
        {
            clear: both;
        }
</style>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
        }
        function ValidateForm() {
              return true;
          }
        $(document).ready(function () {
        });
    </script>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <div id="right_data">
                <table width="100%">
                    <tr>
                        <td width="5px">
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label3" runat="server" Text="Location"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"  runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="Label5" runat="server"  Text="Employee"></asp:Label>
                                        </td>
                                        <td width="50%">
                                            <asp:DropDownList ID="ddlEmployee"   runat="server" Width="209px">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnExpenseID" runat="server" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table> 
                <hr />
                <table width="100%">
                    <tr>
                        <td width="5px">
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>                                
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                        </td>
                                        <td width="30%">
                                         <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate" PopupButtonID="txtDate" Format="MMMM-yyyy"></cc1:CalendarExtender>
                                            <asp:TextBox ID="txtDate" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                         <td  width="10%"></td>
                                         <td  width="50%"></td>
                                        </tr>
                                        <tr>
                                        <td >
                                            <asp:Label ID="Label2" runat="server"  Text="Expense Head"></asp:Label>
                                        </td>
                                        <td>
                                       
                                            <asp:DropDownList ID="ddlExpenseHead" runat="server" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                        <td  width="10%"></td>
                                         <td  width="50%"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Amount"></asp:Label>
                                        </td>
                                        <td>
                                          <cc1:FilteredTextBoxExtender ID="txtAmount_FilteredTextBoxExtender" runat="server" TargetControlID="txtAmount"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                         <asp:TextBox ID="txtAmount" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td  width="10%"></td>
                                         <td  width="50%"></td>
                                        </tr>
                                        <tr>
                                        <td >
                                            <asp:Label ID="Label6" runat="server"  Text="Remarks" ></asp:Label>
                                        </td>
                                        <td >
                                         
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td  width="10%"></td>
                                         <td  width="50%"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
                <table style="margin-top:10px;">
                    <tr>
                    <td width="5px"></td>
                        <td>
                            <asp:ImageButton ID="btnSave" ImageUrl="~/images/btn-save.png" OnClick="btnSave_Click"  runat="server" />
                            <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:Repeater ID="rptExpense" runat="server" 
                    onitemcommand="rptExpense_ItemCommand">
                                <HeaderTemplate>
                                <table style="width: 100%; margin-top: 0px">
                        <thead>
                            <tr class="tblheading">
                                <td width="50%">
                                    Employee Name  
                                </td>
                                <td width="25%">
                                    	Expense Head 
                                </td>
                                <td width="25%">
                                    Amount 
                                </td>
                            </tr>
                        </thead>
                      <tbody>
                        </HeaderTemplate>
                        <ItemTemplate> 
                          <tr>
                             <td>
                             <asp:LinkButton ID="btnEditCompany"  CommandArgument='<%# Eval("ExpenseID")%>' CommandName="ExpenseID" runat="server" ClientIDMode="AutoID" >
                                <%# Eval("EmployeeFullName")%>
                                </asp:LinkButton>
                                </td>
                                <td>
                                    <%# Eval("Amount")%>
                                </td>
                                <td>
                                     <%# Eval("ExpenseHead_Name")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                          <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                    </asp:Repeater>             
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
