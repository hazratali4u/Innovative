<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmGasLunchDeduction.aspx.cs" Inherits="pr_frmGasLunchDeduction" Title="Gas / Lunch OverTime" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
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
<script type="text/javascript">
    function checkDate(sender, args) {
        var chkDate = new Date();
        var firstDay = new Date(chkDate.getFullYear(), chkDate.getMonth(), 1);
        firstDay.setMonth(firstDay.getMonth() - 1);        
        if (sender._selectedDate < firstDay) {
            alert("You can only select current or previous");
            sender._selectedDate = new Date();            
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    }
    </script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
            checkAllChecked();
            $("tbody.alternateRow tr:visible").each(function (index) {
                var $this = $(this);
                if (index & 1) {
                    $this.addClass("odd");
                } else {
                    $this.removeClass("odd");
                }

                //find the current popup
                var popUp = $find('ModelPopup');

                //check it exists so the script won't fail
                if (popUp) {
                    //Add the function below as the event
                    popUp.add_hidden(HidePopupPanel);
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
        }
        function ValidateForm() {
            return true;
        }
        $(document).ready(function () {
        });
        function checkAll(CheckBox) {
            var GridVwHeaderChckbox = document.getElementById("tblEmployee");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = CheckBox.checked;
                }
        }
        function checkAllChecked() {
            var GridVwHeaderChckbox = document.getElementById("tblEmployee");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                if (GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == false) {
                    GridVwHeaderChckbox.rows[0].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                    break;
                }
                else {
                    GridVwHeaderChckbox.rows[0].cells[0].getElementsByTagName("INPUT")[0].checked = true;
                }
            }
        }
    </script>

    <script language="JavaScript" type="text/javascript">

        function HidePopupPanel(source, args) {
            //find the panel associated with the extender
            objPanel = document.getElementById(source._PopupControlID);

            //check the panel exists
            if (objPanel) {
                //set the display attribute, so it remains hidden on postback
                objPanel.style.display = 'none';
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <div id="right_data">
            <div>
                <asp:UpdateProgress ID="UpdateProgress" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="31px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                    PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
            </div>                        
                <table width="100%">
                    <tr>
                        <td width="5px">
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_Change" AutoPostBack="true" runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="Label5" runat="server"  Text="Department"></asp:Label>
                                        </td>
                                        <td width="50%">
                                            <asp:DropDownList ID="ddlDepartment"  OnSelectedIndexChanged="ddlLocation_Change" AutoPostBack="true" runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                    <label style="display:-moz-box">OverTime Type</label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlType" runat="server" Width="209px" OnSelectedIndexChanged="ddlLocation_Change" AutoPostBack="true">
                                            <asp:ListItem Text="Lunch Deduction" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Gas Addition" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                    <label style="display:-moz-box">Date</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" Width="100px" AutoPostBack="true" 
                                            ontextchanged="txtDate_TextChanged"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate" PopupButtonID="txtDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="checkDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <label style="display:-moz-box">OverTime Hours</label>
                                    </td>
                                    <td>
                                    <asp:TextBox ID="txtHours" runat="server" Width="100px" Text="0.00"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fteHours" runat="server" FilterType="Numbers, Custom"
                                        ValidChars="123456789." TargetControlID="txtHours">
                                    </cc1:FilteredTextBoxExtender>
                                    </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                <tr>
                    <td class="customerSerch" style="width:50%">
                        <input type="text" id="text" name="n" style="width: 250px;" placeholder="Enter Text To Search " />                            
                    </td>
                    <td style="width:50%;" align="right">                        
                    </td>
                </tr>
                </table>

                <asp:Repeater ID="rptEmployee" runat="server">
                    <HeaderTemplate>
                        <table width="50%" id="tblEmployee" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">

                                <td  width="2%">
                                    <asp:CheckBox ID="cbAllEmployee" runat="server" onclick="checkAll(this);"></asp:CheckBox>
                                </td>
                                    <td width="65%">
                                        Employee Name
                                    </td>
                                   
                                    <td width="33%">
                                       Hours
                                    </td>                                                                                                                                            
                                </tr>
                            </thead>
                            <tbody id="table1">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                        <td align="center">                                                
                            <asp:CheckBox ID="cbEmployee" runat="server" onclick="checkAllChecked()" ></asp:CheckBox>
                            <asp:HiddenField ID="hfGasLunchDedcutionID" runat="server" Value='<%# Eval("GasLunchDedcutionID")%>'/>                            
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEditCompany"  CommandArgument='<%# Eval("EmployeeID")%>' CommandName="EmployeeID" runat="server" ClientIDMode="AutoID" >
                            <asp:Label ID="lblName" Text='<%# Eval("EmployeeFullName")%>' runat="server"></asp:Label> 
                            </asp:LinkButton>
                            <asp:HiddenField ID="hdnEmployeeID" Value='<%# Eval("EmployeeID")%>' runat="server" />
                        </td>                            
                        <td>
                            <asp:TextBox ID="txtMinutes" runat="server" Width="100%" Text='<%# Eval("Minutes")%>' ></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="fteMinutes" runat="server" FilterType="Numbers, Custom"
                                ValidChars="123456789." TargetControlID="txtMinutes">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        </tr>
                    </ItemTemplate>                    
                </asp:Repeater>
                <table style="margin-top:10px;">
                    <tr>
                    <td width="5px"></td>
                        <td>
                            <asp:LinkButton ID="btnSaveSalary" runat="server" OnClick="btnSaveSalary_Click"><img src="../images/btn-Save.png" /></asp:LinkButton>
                            <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
