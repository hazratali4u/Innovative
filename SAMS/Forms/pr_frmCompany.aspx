<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmCompany.aspx.cs" Inherits="Forms_pr_frmCompany" Title="Company" %>

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
            $("#text").keyup(function (e) {
                $("#table1 tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#text").val(); //Get the search box value
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
    <div id="right_data" class="customerSerch">
        <table width="100%">
            <tr>
                <td>
                    <input type="text" id="text" name="n" style="width: 250px;" placeholder="Enter Text To Search " />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:LinkButton ID="lnkAddCompany" runat="server" OnClick="lnkAddCompany_Click">Add New Company</asp:LinkButton>
                </td>
            </tr>            
            <tr>
                <td>                 
                <div style="text-align: center; width: 100%; margin: 0 auto; overflow: hidden;">
                                <asp:Repeater ID="prtCompanies" runat="server" 
                    onitemcommand="prtCompanies_ItemCommand">
                                <HeaderTemplate>
                                <table style="width: 100%; margin-top: 0px">
                        <thead>
                            <tr class="tblheading">
                                <td width="60%">
                                    Company Name
                                </td>
                                <td width="10%">
                                    Employees
                                </td>
                                <td width="10%">
                                    Departments
                                </td>
                                <td width="10%">
                                    Designations
                                </td>
                                <td width="10%">
                                    Status
                                </td>
                            </tr>
                        </thead>
                      <tbody class="alternateRow" id="table1">
                        </HeaderTemplate>
                        <ItemTemplate> 
                          <tr>
                                <td width="60%">
                             <asp:LinkButton ID="btnEditCompany"  CommandArgument='<%# Eval("CompanyID")%>' CommandName="CompanyID" runat="server" ClientIDMode="AutoID" >
                                <%# Eval("CompanyName")%>
                                </asp:LinkButton>
                                </td>
                                <td width="10%">
                                 <%# Eval("Employees")%>
                                </td>
                                <td width="10%">                                    
                                    <%# Eval("Departments")%>
                                </td>
                                <td width="10%">
                                    <%# Eval("Designations")%>
                                </td>
                                <td width="10%">
                                <%# Eval("Status")%>                                    
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
    </div>
</asp:Content>
