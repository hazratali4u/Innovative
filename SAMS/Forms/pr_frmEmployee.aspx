<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmEmployee.aspx.cs" Inherits="pr_frmEmployee" Title="Employee" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <style type="text/css">
        .tblheading
        {
            padding: 6px 3px !important;
            background-color: #e4e4e4;
            height: 40px;
        }
        .tblheading td
        {
            padding: 5px 3px 5px 10px !important;
            text-align: left;
            font-size: 18px !important;
            color: #006599;
        }
        .EditDelete
        {
            color: #0000ff;
        }
        .tblbody tr td
        {
            height: 25px;
            padding: 6px 3px;
            padding-top: 4px;
            text-align: left !important;
            padding-left: 10px !important;
        }
        .customerSerch label
        {
            width: 150px;
            padding: 5px 0px 0 0;
            float: left;
            font-weight: normal;
            font-size: 15px;
            font-family: Arial, Helvetica, sans-serif;
            color: #393939;
            margin-top: 10px;
            margin-left: 300px;
        }
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
            border-radius: 3px;
        }
        .customerSerch
        {
            clear: both;
        }
        .rightButton input
        {
            -moz-box-shadow: inset 0 0 6px #333;
            -webkit-box-shadow: inset 0 0 6px #333;
            box-shadow: inset 0 0 6px #333;
            -moz-border-radius: 3px;
            height: 26px;
            float: right;
            padding-left: 5px;
            border: none;
            color: #666;
            margin-top: 10px;
            margin-right: 10px;
            border-radius: 3px;
        }
        .odd
        {
            background-color: #e4e4e4;
        }
        .alternateRow tr
        {
            height: 22px;
        }
        .alternateRow tr td
        {
            padding-top: 4px;
            font-size: 14px;
            padding-left: 10px;
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //--------------set alternate Color------------------------------------------
            $("tbody.alternateRow tr:visible").each(function (index) {
                var $this = $(this);
                if (index & 1) {
                    $this.addClass("odd");
                } else {
                    $this.removeClass("odd");
                }
            });
            $("#<%=txtSearch.ClientID %>").keyup(function (e) {
                $("#table1 tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#<%=txtSearch.ClientID %>").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#table1 tr:has(td)").show();
                    return false;
                }
                $("#table1 tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });
        });
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td  class="customerSerch">
                    <asp:TextBox ID="txtSearch" name="n" runat="server" style="width: 250px;" placeholder="Enter Text To Search "></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="Button" 
                        onclick="btnSearch_Click"/>&nbsp;
                    <asp:Button ID="btnClear" runat="server" Text="Clear Search" CssClass="Button" 
                        onclick="btnClear_Click"/>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:LinkButton ID="btnAddEmployee" runat="server" OnClick="btnAddEmployee_Click">Add New Employee</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td  class="customerSerch">
                <div style="text-align: center; width: 100%; margin: 0 auto; overflow: hidden;">
                                <asp:Repeater ID="rptEmployees" runat="server" 
                    onitemcommand="rptEmployees_ItemCommand">
                                <HeaderTemplate>
                                <table style="width: 100%; margin-top: 0px">
                        <thead>
                            <tr class="tblheading">
                                <td width="30%">
                                    Employee Name 
                                </td>
                                <td width="15%">
                                    Department
                                </td>
                                <td width="15%">
                                    Designation 
                                </td>
                                <td width="10%">
                                    Shift 
                                </td>
                                 <td width="10%">
                                    Date Of Joining
                                </td>
                                <td width="20%">
                                    Company
                                </td>
                            </tr>
                        </thead>
                      <tbody class="alternateRow" id="table1">
                        </HeaderTemplate>
                        <ItemTemplate> 
                          <tr>
                             <td>
                             <asp:LinkButton ID="btnEditCompany"  CommandArgument='<%# Eval("EmployeeID")%>' CommandName="CompanyID" runat="server" ClientIDMode="AutoID" >
                                <%# Eval("EmployeeFullName")%>
                                </asp:LinkButton>
                                </td>
                                <td>
                                    <%# Eval("DepartmentName")%>
                                </td>
                                <td>
                                     <%# Eval("DesignationName")%>
                                </td>
                                <td>
                                    <%# Eval("ShiftName")%>
                                </td>
                                 <td>
                                    <%# Eval("EmployeeDurationFrom2")%>
                                </td>
                                <td>
                                    <%# Eval("CompanyName")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                          <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                    </asp:Repeater>
                </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="text-align: center; width: 100%; margin: 0 auto; overflow: hidden;">
                                <asp:Repeater ID="rptSearch" runat="server" 
                    onitemcommand="rptEmployees_ItemCommand">
                                <HeaderTemplate>
                                <table style="width: 100%; margin-top: 0px">
                        <thead>
                            <tr class="tblheading">
                                <td width="30%">
                                    Employee Name 
                                </td>
                                <td width="15%">
                                    Department
                                </td>
                                <td width="15%">
                                    Designation 
                                </td>
                                <td width="10%">
                                    Shift 
                                </td>
                                 <td width="10%">
                                    Date Of Joining
                                </td>
                                <td width="20%">
                                    Company
                                </td>
                            </tr>
                        </thead>
                      <tbody class="alternateRow" id="table1">
                        </HeaderTemplate>
                        <ItemTemplate> 
                          <tr>
                             <td>
                             <asp:LinkButton ID="btnEditCompany"  CommandArgument='<%# Eval("EmployeeID")%>' CommandName="CompanyID" runat="server" ClientIDMode="AutoID" >
                                <%# Eval("EmployeeFullName")%>
                                </asp:LinkButton>
                                </td>
                                <td>
                                    <%# Eval("DepartmentName")%>
                                </td>
                                <td>
                                     <%# Eval("DesignationName")%>
                                </td>
                                <td>
                                    <%# Eval("ShiftName")%>
                                </td>
                                 <td>
                                    <%# Eval("EmployeeDurationFrom2")%>
                                </td>
                                <td>
                                    <%# Eval("CompanyName")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                          <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                    </asp:Repeater>
                </div>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="right" width="56%">
                        <asp:LinkButton ID="linkbtnprev" runat="server" 
                    onclick="linkbtnprev_Click" CausesValidation="false" Visible="false" style="color: #6C5A10;">Previous</asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="linkbtnnext" runat="server" 
                    onclick="linkbtnnext_Click" CausesValidation="false" Visible="false" style="color: #6C5A10;">Next</asp:LinkButton>
                </td>
                <td align="right" width="44%">
                <asp:Label ID="lblCurrentPageNo" runat="server" Text="" Visible="false" style="color: #6C5A10;"></asp:Label>
                    <asp:Label ID="lblOf" runat="server" Text="Of" Visible="false" style="color: #6C5A10;"></asp:Label>
                    <asp:Label ID="lblTotalNoOfPages" runat="server" Text="" Visible="false" style="color: #6C5A10;"></asp:Label>
                    <asp:Label ID="lblDummy" runat="server" Text="| Total Records:" Visible="false" style="color: #6C5A10;"></asp:Label>
                    <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" style="color: #6C5A10;"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
