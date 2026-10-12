<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmLease.aspx.cs" Inherits="pr_frmLease" Title="Lease" %>
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
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
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
            <asp:Panel ID="pnlLeaseContent" runat="server">
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
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"   runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="Label12" runat="server"  Text="Department"></asp:Label>
                                        </td>
                                        <td width="50%">
                                            <asp:DropDownList ID="ddlDepartment"  OnSelectedIndexChanged="ddlDepartment_Change" AutoPostBack="true" runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label5" runat="server"  Text="Employee"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlEmployee"   runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            
                                        </td>
                                        <td width="50%">
                                            
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
                                <td height="20px">
                                <asp:HiddenField ID="hdnLeaseID" runat="server" />
                                </td>
                                </tr>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label1" runat="server" Text="LeaseFor"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlLeaseType" OnSelectedIndexChanged="ddlLeaseType_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="Label2" runat="server"  Text="Assets"></asp:Label>
                                        </td>
                                        <td width="50%">
                                        
                                            <asp:DropDownList ID="ddlAssets"  runat="server" Width="205px" OnSelectedIndexChanged="ddlAssets_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                     <asp:Label ID="Label9" runat="server"  Text="Amount"></asp:Label>
                                    </td>
                                    <td>
                                    <cc1:FilteredTextBoxExtender ID="txtAmount_FilteredTextBoxExtender" runat="server" TargetControlID="txtAmount"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                   <asp:TextBox ID="txtAmount" runat="server" Width="200px"></asp:TextBox >
                                    </td>
                                    </tr>
                                     <tr>
                                    <td>
                                     <asp:Label ID="Label10" runat="server"  Text="CompanyContribution"></asp:Label>
                                    </td>
                                    <td>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCompanyContribution"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                   <asp:TextBox ID="txtCompanyContribution" runat="server" Width="200px"></asp:TextBox >
                                    </td>
                                    <td>
                                     <asp:Label ID="Label11" runat="server"  Text="EmployeeContribution"></asp:Label>
                                    </td>
                                    <td>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtEmployeeContribution"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                   <asp:TextBox ID="txtEmployeeContribution" runat="server" Width="200px"></asp:TextBox >
                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="DateFrom"></asp:Label>
                                        </td>
                                        <td>
                                         <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="MMMM-yyyy"></cc1:CalendarExtender>
                                         <asp:TextBox ID="txtDateFrom" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td >
                                            <asp:Label ID="Label6" runat="server"  Text="DateTo" ></asp:Label>
                                        </td>
                                        <td >
                                         <cc1:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="MMMM-yyyy"></cc1:CalendarExtender>
                                            <asp:TextBox ID="txtDateTo" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Approved By"></asp:Label>
                                        </td>
                                        <td>
                                             <uc1:MultiCheckCombo ID="mcbManagers" runat="server" />
                                        </td>
                                        <td>
                                        
                                            <asp:Label ID="Label8" runat="server"  Text="ApproveDate"></asp:Label>
                                        </td>
                                        <td>
                                         <cc1:CalendarExtender ID="txtApproveDate_CalendarExtender" runat="server" TargetControlID="txtApproveDate" PopupButtonID="txtApproveDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                           <asp:TextBox ID="txtApproveDate" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>  
                <table>
                <tr>
                <td>
                <asp:LinkButton ID="btnCalculateInstallments" runat="server"  OnClick="btnCalculateInstallments_Click">Calculate Installments</asp:LinkButton>
                </td>
                </tr>
                </table>            
                <asp:Repeater ID="rptInstallment" runat="server" 
                    onitemdatabound="rptInstallment_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">
                                    <td width="40%">
                                       Installment Date 
                                    </td>
                                    <td width="40%">
                                        Installment amount 
                                    </td>
                                    <td width="20%">
                                       Paid amount 
                                    </td>
                                </tr>
                            </thead>
                            <tbody id="table1">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                            <asp:TextBox ID="txtInstallmentDate" value=' <%# Eval("InstallmentDate")%>' runat="server"></asp:TextBox>
                            </td>
                            <td>
                             <cc1:FilteredTextBoxExtender ID="txtamountTobePaid_FilteredTextBoxExtender" runat="server" TargetControlID="txtamountTobePaid"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                          <asp:TextBox ID="txtamountTobePaid" value='<%# Eval("AmountToBePaid")%>' runat="server"></asp:TextBox>
                            </td>
                            <td>
                             <asp:TextBox ID="txtAmountPaid" value='<%# Eval("AmountPaid")%>' runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
                <table style="margin-top:10px;">
                    <tr>
                    <td width="5px"></td>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">
                                <asp:Image ID="imgLease" runat="server" ImageUrl="../images/btn-save.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlLeaseGrid" runat="server">
                    <asp:LinkButton ID="btnShowpnLeaseContent" OnClick="btnShowpnLeaseContent_Click" runat="server">Add New Lease</asp:LinkButton>
                <asp:Repeater ID="rptLease" runat="server" OnItemCommand="rptLease_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">
                                    <td width="25%">
                                        Employee
                                    </td>
                                    <td width="15%">
                                         Type
                                    </td>
                                    <td width="15%">
                                        Amount
                                    </td>
                                    <td width="15%">
                                       No of Installments
                                    </td>
                                     <td width="20%">
                                       Approved By
                                    </td>                                  
                                    <td width="10%" colspan="2">
                                        Action
                                    </td>
                                </tr>
                            </thead>
                            <tbody id="table1">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                             <%# Eval("EmployeeFullName")%>                             
                            </td>
                            <td>
                                <%# Eval("LoanTypeName")%>
                            </td>
                             <td>
                             <%# Eval("Amount")%>
                            </td>
                            <td>
                                 <%# Eval("NoOfInstallments")%>                                
                            </td>
                             <td>
                                 <%# Eval("ApprovedBy")%>                                  
                            </td>                               
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("Loan_LeaseID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("Loan_LeaseID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            Delete
                                </asp:LinkButton>
                            </td >
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                         <tr style="background-color:#E4E4E4">
                            <td>
                             <%# Eval("EmployeeFullName")%>                             
                            </td>
                            <td>
                             <%# Eval("LoanTypeName")%>
                            </td>
                             <td>
                             <%# Eval("Amount")%>
                            </td>
                            <td>
                                 <%# Eval("NoOfInstallments")%>                                
                            </td>
                             <td>
                                 <%# Eval("ApprovedBy")%>                                  
                            </td>                              
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("Loan_LeaseID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>                  
                             <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("Loan_LeaseID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            Delete
                                </asp:LinkButton>
                            </td>                       
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
