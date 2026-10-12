<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmProcessPayroll.aspx.cs" Inherits="pr_frmProcessPayroll" Title="Process Payroll" %>
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

    <script language="javascript" type="text/javascript">
        function onCalendarShown3() {
            var cal = $find("calendar3");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call3);
                    }
                }
            }
        }

        function onCalendarHidden3() {
            var cal = $find("calendar3");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call3);
                    }
                }
            }
        }

        function call3(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar3");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
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
            <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="pnlDetail" TargetControlID="btnCancel" CancelControlID="btnCancel">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlDetail" runat="server" Width="50%" Height="500px" BackColor="White">
                <table width="100%">
                    <tr>
                        <td width="5px">
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="100%" colspan="5" align="center">
                                            &nbsp;
                                        </td>                                        
                                    </tr>     
                                    <tr>
                                        <td width="100%" colspan="5" align="center">
                                            <strong><asp:Label ID="lblEmployee" runat="server"></asp:Label></strong>
                                            <br />
                                        </td>                                        
                                    </tr>     
                                    <tr>
                                        <td width="100%" colspan="5" align="center">
                                            &nbsp;
                                        </td>                                        
                                    </tr> 
                                    <tr>
                                        <td width="5%">&nbsp;</td>
                                        <td width="43%">
                                           <strong><asp:Label ID="lblAllownces" runat="server" Text="Allownaces"></asp:Label></strong> 
                                        </td>
                                        <td width="4%">&nbsp;</td>
                                        <td width="43%">
                                           <strong>  <asp:Label ID="lblDeductions" runat="server"  Text="Deductions"></asp:Label></strong> 
                                        </td>       
                                        <td width="5%">&nbsp;</td>                                 
                                    </tr> 
                                    <tr>
                                        <td width="5%">&nbsp;</td>
                                        <td width="43%" valign="top">  
                                            <asp:Repeater ID="rAllwonce" runat="server" onitemdatabound="rAllwonce_ItemDataBound">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width:50%">
                                                            <asp:Label ID="lblAllowanceDescription" Text='<%# Eval("AllowanceDescription")%>' runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:50%" align="right">
                                                            <asp:Label ID="lblAllownceAmount" Text='<%# DataBinder.Eval(Container.DataItem,"Amount","{0:0.00}")%>' runat="server"></asp:Label>
                                                        </td>
                                                    </tr>                                                
                                                </table>                                            
                                            </ItemTemplate>
                                            </asp:Repeater>                                                                                     
                                        </td>
                                        <td style="width:4%">&nbsp;</td>
                                        <td width="43%" valign="top">                     
                                           <asp:Repeater ID="rDeduction" runat="server"  onitemdatabound="rDeduction_ItemDataBound">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width:50%">
                                                            <asp:Label ID="lblDeductionDescription" Text='<%# Eval("DeductionDescription")%>' runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:50%" align="right">
                                                            <asp:Label ID="lblDeductionAmount" Text='<%# DataBinder.Eval(Container.DataItem,"Amount","{0:0.00}")%>' runat="server"></asp:Label>
                                                        </td>
                                                    </tr>                                                
                                                </table>                                            
                                            </ItemTemplate>
                                            </asp:Repeater>                                                                                      
                                        </td>              
                                        <td width="5%">&nbsp;</td>                          
                                    </tr>    
                                    <tr>
                                        <td width="5%">&nbsp;</td>
                                        <td width="43%" valign="top" align="right">        
                                        <br />                                   
                                           <asp:Label ID="lblAllownceTotal" runat="server" Text="Total Allowance"></asp:Label>
                                           &nbsp;<asp:Label ID="lblAllownceTotal1" runat="server"></asp:Label>
                                        </td>
                                        <td width="4%">&nbsp;</td>
                                        <td width="43%" valign="top" align="right">      
                                        <br />               
                                           <asp:Label ID="lblDeductionTotal" runat="server" Text="Total Deduction"></asp:Label>
                                           &nbsp;<asp:Label ID="lblDeductionTotal1" runat="server"></asp:Label>
                                        </td>                     
                                        <td width="5%">&nbsp;</td>                   
                                    </tr>
                                     <tr>
                                        <td width="5%">&nbsp;</td>
                                        <td width="43%" valign="top">        
                                        <br />                                   
                                            <asp:Label ID="lblBasic" runat="server" Text="Basic Salary"></asp:Label>
                                           &nbsp;<asp:Label ID="lblBasic1" runat="server"></asp:Label>
                                        </td>
                                        <td width="4%">&nbsp;</td>
                                        <td width="43%" valign="top">      
                                        <br />               
                                          <asp:Label ID="lblNet" runat="server" Text="Net Salary"></asp:Label>
                                           &nbsp;<asp:Label ID="lblNet1" runat="server"></asp:Label>
                                        </td>                     
                                        <td width="5%">&nbsp;</td>                   
                                    </tr>                                         
                                     <tr>
                                        <td width="100%" colspan="5" align="center">
                                            <asp:LinkButton ID="btnCancel" runat="server">Ok</asp:LinkButton>
                                        </td>                                        
                                    </tr>                                   
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>                
            </asp:Panel>            
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
                                    <label style="display:-moz-box">Salary Behavior</label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSalaryBehaviour" runat="server" Width="209px" OnSelectedIndexChanged="ddlLocation_Change" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Text="--All--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Bank" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    <label style="display:-moz-box">Bonus</label>
                                    </td>
                                    <td>
                                     <asp:DropDownList ID="ddlBonus" runat="server" Width="209px" OnSelectedIndexChanged="ddlLocation_Change" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Text="Not Applicable" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Standard Pay" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Bonus On Net Pay" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Bonus On Basic Pay" Value="3"></asp:ListItem>
                                      </asp:DropDownList>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                    <label style="display:-moz-box">Month</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth" runat="server" Width="100px" AutoPostBack="true" 
                                            ontextchanged="txtMonth_TextChanged"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtMonth_CalendarExtender" runat="server" BehaviorID="calendar3" OnClientShown="onCalendarShown3" OnClientHidden="onCalendarHidden3"
                                            TargetControlID="txtMonth" PopupButtonID="txtMonth" Format="MMM-yyyy" OnClientDateSelectionChanged="checkDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
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
                        <label style="float:right; margin-right:30px;font-weight:bold;font-size:medium;">
                            Company Working Days:<asp:Label ID="lblCompanyWorkingDays" runat="server" style="float:right;"></asp:Label>
                        </label>
                    </td>
                </tr>
                </table>

                <asp:Repeater ID="rptEmployee" runat="server" 
                    onitemdatabound="rptEmployee_ItemDataBound" 
                    onitemcommand="rptEmployee_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%" id="tblEmployee" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">

                                <td  width="2%">
                                    <asp:CheckBox ID="cbAllEmployee" runat="server" onclick="checkAll(this);"></asp:CheckBox>
                                </td>
                                    <td width="15%">
                                        Employee Name
                                    </td>
                                   
                                    <td width="10%">
                                       B.Salary 	
                                    </td>                                                                                                        
                                    <td width="9%">
                                       	Allowances 
                                    </td>                                  
                                    <td width="9%">
                                        Deductions 	
                                    </td>                                    
                                     <td width="9%">
                                       Fine
                                    </td>
                                    <td width="10%">
                                       Net Pay 	
                                    </td>                                                                 
                                    <td width="5%">
                                       Outside Work Hours
                                    </td>                                  
                                    <td width="5%">
                                       Visit
                                    </td>        
                                     <td width="15%">
                                         Remarks
                                    </td>       
                                    <td width="11%">
                                        Salary Status
                                    </td>                   
                                </tr>
                            </thead>
                            <tbody id="table1">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                        <td align="center">
                                                
                            <asp:CheckBox ID="cbEmployee" runat="server" onclick="checkAllChecked()" ></asp:CheckBox>
                            <asp:HiddenField ID="hfSALARY_DETAIL_ID" runat="server" Value='<%# Eval("SALARY_DETAIL_ID")%>'/>
                            <asp:HiddenField ID="hfEmployeeWorkingDays" runat="server" Value='<%# Eval("WORK_DATES")%>'/>
                            <asp:HiddenField ID="hfBonus" runat="server" Value="0"/>
                            <asp:HiddenField ID="hfEmployeeContractTypeID" runat="server" Value='<%# Eval("EmployeeContractTypeID")%>'/>
                        </td>
                            <td>
                               <asp:LinkButton ID="btnEditCompany"  CommandArgument='<%# Eval("EmployeeID")%>' CommandName="EmployeeID" runat="server" ClientIDMode="AutoID" >
                              <asp:Label ID="lblName" Text='<%# Eval("EmployeeFullName")%>' runat="server"></asp:Label> 
                                </asp:LinkButton>
                             <asp:HiddenField ID="hdnEmployeeID" Value='<%# Eval("EmployeeID")%>' runat="server" />
                            </td>                            
                            <td>
                                <asp:Label ID="lblBasicPay" Text='<%# Eval("BasicPay")%>' runat="server"></asp:Label>
                            </td>                                                                                 
                            <td>
                                <asp:Label ID="lblAllowances" Text='<%#Convert.ToInt32(Eval("ALLOWANCE")) + Convert.ToInt32(Eval("OverTimeAmount"))+ Convert.ToInt32(Eval("Expense"))%>' runat="server"></asp:Label>
                            </td>                           
                            <td>
                                <asp:Label ID="lblDeduction" Text='<%# Convert.ToInt32(Eval("DEDUCTION")) + Convert.ToInt32(Eval("PFAmount")) +Convert.ToInt32(Eval("LOAN_AMOUNT"))+Convert.ToInt32(Eval("LEASE_AMOUNT"))+Convert.ToInt32(Eval("ADVANCE_AMOUNT"))+Convert.ToInt32(Eval("Leave_Deduction")) +Convert.ToInt32(Eval("AbsentAmount"))%>'
                                    runat="server"></asp:Label>
                            </td>                            
                            <td>
                                <asp:TextBox ID="txtFine" runat="server" Width="100%" Text='<%# Eval("FINE")%>' ></asp:TextBox>
                            </td>
                            <td>
                            <asp:Label ID="lblNetPay" Text=' <%#(Convert.ToInt32( Eval("ALLOWANCE"))+ Convert.ToInt32(Eval("Expense"))+Convert.ToInt32( Eval("BasicPay")))+ Convert.ToInt32(Eval("OverTimeAmount"))- Convert.ToInt32(Eval("DEDUCTION")) - Convert.ToInt32(Eval("LOAN_AMOUNT")) - Convert.ToInt32(Eval("LEASE_AMOUNT"))  - Convert.ToInt32(Eval("ADVANCE_AMOUNT")) - Convert.ToInt32(Eval("Leave_Deduction"))-Convert.ToInt32(Eval("AbsentAmount"))-Convert.ToInt32(Eval("PFAmount"))-Convert.ToInt32(Eval("FINE"))%>' runat="server"></asp:Label>
                            </td>
                              <td>
                                <asp:TextBox ID="txtOUTSIDE_HOURS" runat="server" Width="100%" Text='<%# Eval("OUTSIDE_HOURS")%>' ></asp:TextBox>
                            </td>
                              <td>
                                <asp:TextBox ID="txtVISITS" runat="server" Width="100%" Text='<%# Eval("VISITS")%>' ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" Width="100%" Text='<%# Eval("REMARKS")%>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbProcess" runat="server" Text="Process" Checked= '<%#Convert.ToBoolean(Eval("IsProcess"))%>' />
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
                            <asp:LinkButton ID="btnSaveSalary" runat="server" OnClick="btnSaveSalary_Click"><img src="../images/btn-Save.png" /></asp:LinkButton>
                            <asp:LinkButton ID="btnApprove" Visible="false" runat="server" OnClick="btnApprove_Click"><img src="../images/btn-appr-draft.png" /></asp:LinkButton>
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
