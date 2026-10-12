<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" 
    CodeFile="pr_frmEmployeeAdd.aspx.cs" Inherits="pr_frmEmployeeAdd" Title="Add Employee" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <style type="text/css">
        .cmp input
        {
        }
        .cmp select
        {
            width: 90%;
        }
        .list input
        {
            width: 20%;
        }
        .list
        {
            width: 90%;
        }
         input:not([type]), input[type="text"]
        {
            width: 230px;
        }
         select
          {
            width: 230px;
        }
    </style>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>             
        <asp:LinkButton ID="btnSalaryStructure" style="float:right;" OnClick="btnSalaryStructure_Click" runat="server">Salary Structure</asp:LinkButton>
         <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="true"  PopupControlID="pnlSalaryStructure"
        TargetControlID="btnSalaryStructure">
    </cc1:ModalPopupExtender>
     <asp:Panel ID="pnlSalaryStructure" runat="server" Style="display: none; background-color: White;"
                    ScrollBars="Auto" Height="500px" Width="100%">
             <table width="100%" style="padding-left:50px;">
                 <tbody>
                     <tr>
                         <td width="20%">
                             Basic Salary
                         </td>
                         <td width="30%">
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtBasicSalary" runat="server" ControlToValidate="txtBasicSalary" ErrorMessage="Enter Basic Pay" ValidationGroup="grpSalaryStructure" ></asp:RequiredFieldValidator>
                             <asp:TextBox ID="txtBasicSalary" runat="server"></asp:TextBox>
                             <asp:HiddenField ID="hdnSalaryStructureID" runat="server" />
                         </td>
                          <td width="20%">
                           <label>Salary Template</label> 
                         </td>
                         <td width="30%">
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator_ddlSalaryTemplate" runat="server" InitialValue="0" ControlToValidate="ddlSalaryTemplate" ErrorMessage="Select Salary Template" ValidationGroup="grpSalaryStructure" ></asp:RequiredFieldValidator>
                             <asp:DropDownList ID="ddlSalaryTemplate" runat="server" OnSelectedIndexChanged="ddlSalaryTemplate_Change" Width="100%" AutoPostBack="true"></asp:DropDownList>
                             <asp:HiddenField ID="HiddenField1" runat="server" />
                         </td>
                     </tr>
                     <tr>
                         <td>
                             <h2>
                                 Allowancs</h2>
                         </td>
                     </tr>
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
                             <h2>
                                 Deductions</h2>
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
                     <tr>
                         <td>

                          <%if (Session["EditEmployeeID"] != null)
                                  {%>
                                <asp:LinkButton ID="btnSaveSalaryStructure" ValidationGroup="grpSalaryStructure" runat="server" OnClick="btnSaveSalaryStructure_Click"><img src="../images/btn-update.png" /></asp:LinkButton>
                                <%}
                                  else
                                  { %>
                                <asp:LinkButton ID="btnUpdateSalaryStructure" ValidationGroup="grpSalaryStructure" runat="server" OnClick="btnSaveSalaryStructure_Click"><img src="../images/btn-save.png"/></asp:LinkButton>
                                <%} %>
                         </td>
<%--                         <td>
                          <asp:LinkButton ID="btnDiscardSalaryStructure" runat="server" OnClick="btnDiscardSalaryStructure_Click">Discard</asp:LinkButton>
                         </td>--%>
                     </tr>
                 </tbody>
             </table>
         </asp:Panel>         
     <table width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <table width="100%" class="cmp">
                                    <tbody>
                                        <tr>
                                            <td width="10%" rowspan="3">
                                                <label for="ctl00_ctl00_mainCopy_cphPage_FUCompanyLogo" id="lbl" runat="server" style="cursor: pointer;">
                                                    <asp:Image runat="server" ID="iItem" Width="90" Height="90" ImageUrl="~/images/noimage.jpg" />
                                                </label>
                                                <asp:FileUpload ID="FUCompanyLogo" onchange="readURL(this);" Style="display: none;"
                                                    runat="server"></asp:FileUpload>
                                            </td>
                                            <td width="7%">
                                            <br />
                                                <label>
                                                    Employee Name</label>
                                            </td>
                                            <td width="50%" align ="left" >
                                                <asp:RequiredFieldValidator style="float:left" ID="RequiredFieldValidator_txtEmployeeName" ValidationGroup="SaveEmployee" runat="server" ErrorMessage="Enter Employee Name" ControlToValidate="txtEmployeeName"></asp:RequiredFieldValidator>
                                              <br />
                                               <asp:TextBox ID="txtEmployeeName" runat="server"  style="width:230px; float:left" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td  >
                                        <label>
                                                    Location</label>
                                        </td>
                                        <td>
                                        <asp:DropDownList ID="ddlLocation" runat="server" style="width:230px; float:left" 
                                                AutoPostBack="True" onselectedindexchanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        </tr>
                                        
                                          <tr>
                                        <td  >
                                        <label>
                                                    Unit</label>
                                        </td>
                                        <td>
                                        <asp:DropDownList ID="ddlUnit" runat="server" style="width:230px; float:left"></asp:DropDownList>
                                        </td>
                                        <td>
                                         <div style="z-index: 101; left: 612px; width: 100px; position: absolute; top: 369px;
                            height: 100px">
                            &nbsp;
                        </div>
                                        </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 225px">
                                <cc1:TabContainer ID="tbUserAssignment" runat="server" Width="1000px" Height="210px"
                                    AutoPostBack="True" ActiveTabIndex="0">
                                    <cc1:TabPanel runat="server" ID="TabPanel1">
                                        <HeaderTemplate>
                                            Public Information
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td width="20%">
                                                                            <label>
                                                                            Depatrment
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:HiddenField ID="hdnEmployeeID" runat="server" />
                                                                            <asp:DropDownList ID="ddlDepartment" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td width="20%">
                                                                            <label>
                                                                            Salary Behaviour
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:DropDownList ID="ddlSalaryBehaviour" runat="server">
                                                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Bank" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                            Working Address
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtWorkAddress" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                            Blood Group
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBloodGroup" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    Work Email
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtWorkEmail" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    Social Security Number
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtSocialSecurityNumber" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    Work Phone
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtWorkPhone" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    Employee Gender
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlEmployeeGender" runat="server">
                                                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    Work Mobile
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtWorkMobileNo" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    Exception
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlException" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    Active/Inactive
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlisActive" runat="server">
                                                                                    <asp:ListItem Selected="True" Text="Active" Value="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="InActive" Value="False"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                    Employee Role
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlEmployeeRole" runat="server">
                                                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="Manager" Value="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="Non-Manager" Value="False"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" TabIndex="1" ID="TabPanel2">
                                        <HeaderTemplate>
                                            Private Information
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr height="15px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="20%">
                                                                            <label>
                                                                                Designation
                                                                              
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:DropDownList ID="ddlDesignation" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td width="20%">
                                                                            <label>
                                                                                Employee CNIC
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:TextBox ID="txtEmployeeCNIC" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Employee NTN
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtEmployeeNTN" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Driving License Number
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDrivingLicenseNumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Driving License Valid Upto
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                        <cc1:CalendarExtender ID="txtDrivingLicenseValidUpto_CalendarExtender" runat="server" TargetControlID="txtDrivingLicenseValidUpto" PopupButtonID="txtDrivingLicenseValidUpto" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                                                            <asp:TextBox ID="txtDrivingLicenseValidUpto" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Passport Number
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPassportNumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Visa Information</label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtVisaInformation" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Nationality
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNationality" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Bank Account Number
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBankAccountNumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Bank Account Title
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBankAccountTitle" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Bank Name
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBankName" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Date Of Birth
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                        <cc1:CalendarExtender ID="txtDateOfBirth_CalendarExtender" runat="server" TargetControlID="txtDateOfBirth" PopupButtonID="txtDateOfBirth" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                                                            <asp:TextBox ID="txtDateOfBirth" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Marital Status
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlMaritalStatus" runat="server">
                                                                            <asp:ListItem Selected="True" Text="--Select--" Value="False"></asp:ListItem>
                                                                                <asp:ListItem Value="False" Text="Un Married"></asp:ListItem>
                                                                                <asp:ListItem Value="False" Text="Married"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Home Address
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtHomeAddress" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="2" ID="TabPanel3">
                                        <HeaderTemplate>
                                            Others
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td width="20%">
                                                                            <label>
                                                                            EOBI number
                                                                            <asp:HiddenField ID="hdnCompanyBankID" runat="server" />
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:TextBox ID="txtEOBInumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td width="20%">
                                                                            <label>
                                                                            Emergency Contact Number
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:TextBox ID="txtEmergencyContactNumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                Emergency Contact Name
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtEmergencyContactName" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                Employee Code
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtEmployeeTag" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                Shift
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlShift" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                Probation From
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <cc1:CalendarExtender ID="txtProbationFrom_CalendarExtender" runat="server" 
                                                                                    Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="txtProbationFrom" 
                                                                                    TargetControlID="txtProbationFrom">
                                                                                </cc1:CalendarExtender>
                                                                                <asp:TextBox ID="txtProbationFrom" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                Employee Duration From
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <cc1:CalendarExtender ID="txtEmployeeDurationFrom_CalendarExtender" 
                                                                                    runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                                                                                    PopupButtonID="txtEmployeeDurationFrom" 
                                                                                    TargetControlID="txtEmployeeDurationFrom">
                                                                                </cc1:CalendarExtender>
                                                                                <asp:TextBox ID="txtEmployeeDurationFrom" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                Probation To
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <cc1:CalendarExtender ID="txtProbationTo_CalendarExtender" runat="server" 
                                                                                    Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="txtProbationTo" 
                                                                                    TargetControlID="txtProbationTo">
                                                                                </cc1:CalendarExtender>
                                                                                <asp:TextBox ID="txtProbationTo" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                Employee Duration To
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <cc1:CalendarExtender ID="txtEmployeeDurationTo_CalendarExtender" 
                                                                                    runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                                                                                    PopupButtonID="txtEmployeeDurationTo" TargetControlID="txtEmployeeDurationTo">
                                                                                </cc1:CalendarExtender>
                                                                                <asp:TextBox ID="txtEmployeeDurationTo" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                Overtime
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlOvertime" runat="server">
                                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="Enabled" Value="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="Disabled" Value="False"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                Employee Type
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlContractType" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <label>
                                                                                Provident Fund
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlPF" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPF_SelectedIndexChanged">
                                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="Enabled" Value="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="Disabled" Value="False"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                Distributor
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlDistributor" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                             <asp:Label ID="lblPFund" runat="server" Text="Opening P.Fund" Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                             <asp:TextBox ID="txtPFundOpening" runat="server" Visible="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblLoanOpening" runat="server" Text="Opening Loan"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtLoanOpening" runat="server" ></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblLeaseOpening" runat="server" Text="Opening Lease"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtLeaseOpening" runat="server" ></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblLeaveOpening" runat="server" Text="Opening Leave"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtLeaveOpening" runat="server" ></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                
                                                                            </td>
                                                                            <td>
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="3" ID="TabPanel4" Visible="false">
                                        <HeaderTemplate>
                                            Documents
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td align="right" style="width: 15%; ">
                                                            <strong>
                                                            <asp:Label ID="lblSubject" runat="server" Text="Subject:"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 84%; ">
                                                            <asp:TextBox ID="txtSubject" runat="server" Width="40%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 15%">
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="width: 84%">
                                                            <br />
                                                            <asp:FileUpload ID="fuNotification" runat="server" />
                                                            <asp:Button ID="btnUpload" runat="server" CssClass="Button" 
                                                                OnClick="btnUpload_Click" Text="Upload" />                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:GridView ID="gvDocument" runat="server" AutoGenerateColumns="False" 
                                                                CellPadding="2" DataKeyNames="DOCUMENT_ID" 
                                                                EmptyDataText="&lt;div style='padding:10px; margin:10px; background-color:#fbefef; border: Solid 1px #bb0000; color:#bb0000;'&gt;There is no document to display.&lt;/div&gt;" 
                                                                GridLines="Horizontal" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Subject">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkViewDoc" runat="server" AlternateText="download" 
                                                                                CommandArgument='<%# Eval("DOCUMENT_ID")%>' CommandName="download" 
                                                                                ToolTip="Download Document">
                                                                            <%# Eval("SUBJECT")%>
                                                                        </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Document Name">
                                                                        <ItemTemplate>
                                                                            <%# Eval("DOCUMENT_NAME")%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Uploaded By">
                                                                        <ItemTemplate>
                                                                            <%# Eval("USER_NAME")%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Upload DateTime">
                                                                        <ItemTemplate>
                                                                            <%# Eval("TIME_STAMP")%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="btnViewDoc" runat="server" AlternateText="View" 
                                                                                            CommandArgument='<%# Eval("DOCUMENT_ID")%>' CommandName="view" 
                                                                                            ImageUrl="~/images/view.gif" ToolTip="View Detail" />
                                                                                    </td>
                                                                                    <td width="2%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="btnDel" runat="server" AlternateText="Delete" 
                                                                                            CausesValidation="false" CommandArgument='<%# Eval("DOCUMENT_ID")%>' 
                                                                                            CommandName="del" ImageUrl="~/images/delete.gif" 
                                                                                            OnClientClick="return confirm('Are you sure you want to permanently delete this document?');" 
                                                                                            ToolTip="Delete Document" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <HeaderStyle BackColor="#006699" ForeColor="White" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%if (Session["EditEmployeeID"] != null)
                                  {%>
                                <asp:LinkButton ID="btnSave" ValidationGroup="SaveEmployee" runat="server" OnClick="btnSave_Click"
                                    OnClientClick="return postbackButtonClick();" ClientIDMode="Static"><img src="../images/btn-update.png" /></asp:LinkButton>
                                <%}
                                  else
                                  { %>
                                <asp:LinkButton ID="btnUpdate" ValidationGroup="SaveEmployee" runat="server" OnClick="btnSave_Click"
                                    OnClientClick="return postbackButtonClick();" ClientIDMode="Static"><img src="../images/btn-save.png"/></asp:LinkButton>
                                <%} %>
                                <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
     </table>     
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>            
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function pageLoad() {
            var val = '<%=Session["LogoName"] %>';
            if (val == '') {
            } else {
                var logo = '<%= Session["LogoName"]%>';
                $('#pic').attr('src', '../CompanyLogos/' + logo).width(75).height(68);

                //find the current popup
                var popUp = $find('ModelPopup');

                //check it exists so the script won't fail
                if (popUp) {
                    //Add the function below as the event
                    popUp.add_hidden(HidePopupPanel);
                }
            }
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#ctl00_ctl00_mainCopy_cphPage_iItem').attr('src', e.target.result).width(90).height(90);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
        
    </script>
</asp:Content>
