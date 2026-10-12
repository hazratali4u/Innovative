<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pr_frmLoanType.aspx.cs" Inherits="pr_frmLoanType"
    MasterPageFile="~/Forms/PageMaster.master" Title="Loan Type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>                                                       
                                                        <td>
                                                        <asp:Panel ID="pnlLoanTypeContent" runat="server">                                                        
                                                            <table width="100%">
                                                                <tbody>                                                                
                                                                 <tr>
                                                                <td width="10%">
                                                                    <label>
                                                                            Loan Type
                                                                            <asp:HiddenField ID="hdnLoanTypeID" runat="server" />
                                                                    </label>
                                                                </td>
                                                                <td width="30%" align="left">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLoanTypeName" ValidationGroup="grpLoanType" runat="server" ControlToValidate="txtLoanTypeName" ErrorMessage="Enter Loan Type"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtLoanTypeName" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td width="10%">
                                                                </td>
                                                                <td width="50%">
                                                                </td>
                                                            </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:LinkButton ID="btnSaveLoanType" ValidationGroup="grpLoanType" runat="server" OnClick="btnSaveLoanType_Click" > 
                                                                                <asp:Image ID="imgLoanType" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnDiscardLoanType" runat="server"  OnClick="btnDiscardLoanType_Click">Discard</asp:LinkButton>
                                                                        </td>
                                                                        <td width="10%"></td>
                                                                        <td width="50%"></td>
                                                                        </tr>                                                                       
                                                                    
                                                                </tbody>
                                                            </table>
                                                            </asp:Panel>
                                                        <asp:Panel ID="pnlLoanTypeGrid" runat="server">
                                                            <asp:LinkButton ID="btnShowpnlLoanTypeContent" OnClick="btnShowpnlLoanTypeContent_Click" runat="server">Add New Loan Type</asp:LinkButton>
                                                            <asp:Repeater ID="rptLoanType" runat="server" OnItemCommand="rptLoanType_ItemCommand">

                                                          <HeaderTemplate>
                                                               <table style="width: 50%; margin-top: 0px">
                                                                  <tr class="tblheading">
                                                                        <td>
                                                                         	Loan Type 		 	 	
                                                                        </td>                       
                                                                        <td colspan="2">
                                                                            Action
                                                                        </td>
                                                                    </tr>
                                                             </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <tr>
                                                                      <td>
                                                                        <%# Eval("LoanTypeName")%>                                                                        
                                                                      </td>
                                                                      <td>
                                                                        <asp:LinkButton ID="btnEditLoanType" CommandName="edit" CommandArgument='<%# Eval("LoanTypeID")%>' runat="server" ClientIDMode="AutoID">
                                                                                     Edit
                                                                          </asp:LinkButton>
                                                                      </td>                                                                                                                                           
                                                                      <td>
                                                                      <asp:LinkButton ID="btnDeleteLoanType" CommandName="delete" CommandArgument='<%# Eval("LoanTypeID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                          </asp:LinkButton>
                                                                      </td>
                                                            
                                                                  </tr>
                                                              </ItemTemplate>
                                                             
                                                             <AlternatingItemTemplate>
                                                             <tr style="background-color:#E4E4E4">
                                                                         <td>
                                                                          <%# Eval("LoanTypeName")%>
                                                                      </td>    
                                                                       <td>
                                                                        <asp:LinkButton ID="btnEditLoanType" CommandName="edit" CommandArgument='<%# Eval("LoanTypeID")%>' runat="server" ClientIDMode="AutoID">
                                                                                     Edit
                                                                          </asp:LinkButton>
                                                                      </td>                                                                                                                                           
                                                                      <td>
                                                                      <asp:LinkButton ID="btnDeleteLoanType" CommandName="delete" CommandArgument='<%# Eval("LoanTypeID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                          </asp:LinkButton>
                                                                      </td>                                                                    
                                                                  </tr>
                                                             </AlternatingItemTemplate>
                                                             <FooterTemplate>
                                                              </table>
                                                             </FooterTemplate>
                                                               </asp:Repeater>
                                                               </asp:Panel>
                                                        </td>                                                        
                                                    </tr>
                                                </tbody> 
                                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
