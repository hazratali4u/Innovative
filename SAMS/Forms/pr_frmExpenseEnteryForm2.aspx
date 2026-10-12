<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmExpenseEnteryForm2.aspx.cs" Inherits="pr_frmExpenseEnteryForm2" Title="Expense Entery Form" %>
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

    function onCalendarShown() {
        var cal = $find("calendar");
        cal._switchMode("months", true);
        if (cal._monthsBody) {
            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                var row = cal._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                }
            }
        }
    }

    function onCalendarHidden() {
        var cal = $find("calendar");

        if (cal._monthsBody) {
            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                var row = cal._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                }
            }
        }
    }

    function call(eventElement) {
        var target = eventElement.target;
        switch (target.mode) {
            case "month":
                var cal = $find("calendar");
                cal._visibleDate = target.date;
                cal.set_selectedDate(target.date);
                cal._switchMonth(target.date);
                cal._blur.post(true);
                cal.raiseDateSelectionChanged();
                break;
        }
    }
    </script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {
            $("select").searchable();
            //find the current popup
            var popUp = $find('ModelPopup');

            //check it exists so the script won't fail
            if (popUp) {
                //Add the function below as the event
                popUp.add_hidden(HidePopupPanel);
            }
        }

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
                                            <asp:Label ID="Label3" runat="server" Text="Location"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"  runat="server" Width="209px">
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
                                            <asp:DropDownList ID="ddlEmployee"   runat="server" Width="209px" 
                                                AutoPostBack="True" onselectedindexchanged="ddlEmployee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnExpenseID" runat="server" />
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
                                        <td width="10%">
                                            <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                        </td>
                                        <td width="30%">
                                         <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" BehaviorID="calendar" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden" Format="MMM-yyyy" PopupButtonID="ImgBntFromCalc" TargetControlID="txtDate" OnClientDateSelectionChanged="checkDate">
                                         </cc1:CalendarExtender>
                                            <asp:TextBox ID="txtDate" runat="server" Width="200px" AutoPostBack="True" 
                                                ontextchanged="txtDate_TextChanged"></asp:TextBox>
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
