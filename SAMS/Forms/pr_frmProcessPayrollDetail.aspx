<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Forms/PageMaster.master" 
CodeFile="pr_frmProcessPayrollDetail.aspx.cs" Inherits="Forms_pr_frmProcessPayrollDetail" Title="Process Payroll Detail" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <div>
    <table>
    <tr>
    <td>
    <asp:Repeater ID="rptAllAllowances" runat="server">
                                 <ItemTemplate>
                                     <tr>
                                         <td width="20%">
                                             <label>
                                                 <%#Eval("AllowanceDescription")%>
                                             </label>
                                         </td>
                                         <td width="30%">
                                         
                                             <asp:CheckBox ID="chkAllowancePercentage" runat="server" Width="20px" Checked='<%#Eval("RatioType").ToString() == "1" ? true:false %>' /><label>
                                                 Percentage</label>
                                                 <br />
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtAllowanceAmount" runat="server" ControlToValidate="txtAllowanceAmount" ErrorMessage="Enter Allowance" ValidationGroup="grpSalaryStructure" ></asp:RequiredFieldValidator>
                                                 
                                             <asp:TextBox ID="txtAllowanceAmount" Text='<%# Eval("AllowanceRatio") %>' runat="server"></asp:TextBox>
                                             <asp:HiddenField ID="hdnAllAllownaceID" Value='<%# Eval("AllowanceID") %>' runat="server">
                                             </asp:HiddenField>
                                         </td>
                                         <td>
                                         </td>
                                         <td>
                                         </td>
                                     </tr>
                                 </ItemTemplate>
                             </asp:Repeater>
    </td>
    </tr>
    <tr>
    <td>
    <asp:Repeater ID="rptAllDeduction" runat="server">
                                 <ItemTemplate>
                                     <tr>
                                         <td width="20%">
                                             <label>
                                                 <%#Eval("DeductionDescription")%>
                                             </label>
                                         </td>
                                         <td width="30%">
                                             <asp:CheckBox ID="chkDeductionPercentage" runat="server" Width="20px" Checked='<%#Eval("RatioType").ToString() == "1" ? true:false %>' /><label>
                                                 Percentage</label>
                                                 <br />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtAllowanceAmount" runat="server" ControlToValidate="txtDeductionAmount" ErrorMessage="Enter Deductions" ValidationGroup="grpSalaryStructure" ></asp:RequiredFieldValidator>
                                             <asp:TextBox ID="txtDeductionAmount" Text='<%# Eval("DeductionRatio") %>' runat="server"></asp:TextBox>
                                             <asp:HiddenField ID="hdnAllDeductionID" Value='<%# Eval("DeductionID") %>' runat="server">
                                             </asp:HiddenField>
                                         </td>
                                         <td colspan="2">
                                         <asp:TextBox ID="txtComments" TextMode="MultiLine" Width="90%" Text='<%# Eval("Comments") %>'
                                                 placeholder="Enter Comments" runat="server"></asp:TextBox>
                                         </td>
                                     </tr>
                                 </ItemTemplate>
                             </asp:Repeater>
    </td>
    </tr>
    </table>
     
     
    </div>
</asp:Content>