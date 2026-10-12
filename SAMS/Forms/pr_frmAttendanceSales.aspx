<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pr_frmAttendanceSales.aspx.cs" Inherits="pr_frmAttendanceSales"
    MasterPageFile="~/Forms/PageMaster.master" Title="Attendance Sale Force" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
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

        function MonthChange() {
            document.getElementById("<%= btnGetAttendance.ClientID %>").click();
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
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
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
                            <table>
                                <tbody>   
                                    <tr>
                                        <td style="width:20%;">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:80%;">
                                            <asp:DropDownList ID="ddlLocation" runat="server" Width="210px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>                                        
                                        <td style="width:20%;">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Text="Designation" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:80%;">
                                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="DropList" Width="209px" AutoPostBack="True" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%;">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="59px" Text="For Month"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:80%;">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="153px" MaxLength="10" onchange="MonthChange();"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>                                    
                                    <tr>                                       
                                        <td colspan="3">
                                        &nbsp;
                                        </td>
                                    </tr>
                                    <tr>                                       
                                        <td colspan="3">
                                            <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" Width="100%" GridLines="None" 
                                                
                                                    EmptyDataText="<div style='padding:10px; margin:10px; background-color:#fbefef; border: Solid 1px #bb0000; color:#bb0000;'>There is no document to display.</div>" 
                                                    EnableModelValidation="True" ForeColor="#333333">
                                                    <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Employee">
                            <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                    <%# Eval("EmployeeFullName")%>
                                    <asp:HiddenField ID="hfEmployeeID" runat="server" Value='<%# Eval("EmployeeID")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="250px"/>
                            </asp:TemplateField>                             
                            <asp:TemplateField HeaderText="Attendance">
                                <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAttendance" runat="server" Width="100px" Text='<%# Eval("Attendance")%>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                               </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Absent">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <asp:TextBox ID="txtAbsent" runat="server" Width="100px" Text='<%# Eval("Absent")%>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns> 
                        <EditRowStyle BackColor="#7C6F57" />                        
                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                         <HeaderStyle BackColor="#006699" ForeColor="White"/>     
                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#E3EAEB" />
                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                                        </td>                                     
                                    </tr>                            
                                </tbody>
                            </table>               
                            <cc1:CalendarExtender id="CEStartMonth" runat="server" BehaviorID="calendar3" OnClientShown="onCalendarShown3" OnClientHidden="onCalendarHidden3" Format="MMM-yyyy" PopupButtonID="ImgBntFromCalc" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnSave" runat="server" CssClass="Button" Width="90" 
                        Text="Save" onclick="btnSave_Click"/>
                        <div style="display:none;">
                        <asp:Button ID="btnGetAttendance" runat="server" CssClass="Button" Width="90" 
                        Text="View Attendance" onclick="btnGetAttendance_Click"/>
                        </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
