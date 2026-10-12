<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmCompanyAdd.aspx.cs" Inherits="Forms_pr_frmCompanyAdd" Title="Add Company" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
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
    <style type="text/css">
        .cmp input
        {
            width: 90%;
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
    <div id="right_data">
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
                <table width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <table width="100%" class="cmp">
                                    <tbody>
                                    <tr>
                            <td width="10%">
                              <label for="ctl00_ctl00_mainCopy_cphPage_FUCompanyLogo" id="lbl" runat="server" style="cursor: pointer;">
                                <img src="../images/select-logo.jpg" id="pic" name="pic" width="75" height="68" />
                              </label>
                              <asp:FileUpload ID="FUCompanyLogo" onchange = "readURL(this);" style="display:none;" runat="server" ></asp:FileUpload>
                                         </td>
                                            <td width="10%">
                                            <br />                                                <label>
                                                    Company Name</label>
                                            </td>
                                            <td width="50%">
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator_txtCompanyName" runat="server" ErrorMessage="Enter Company Name" ControlToValidate="txtCompanyName" ValidationGroup="grpCompany" ></asp:RequiredFieldValidator>
                                                <asp:TextBox style="width:700px;"  ID="txtCompanyName" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 345px">
                                <cc1:TabContainer ID="tbUserAssignment" runat="server" Width="1000px" Height="400px"
                                    AutoPostBack="True" ActiveTabIndex="0" OnActiveTabChanged="tbUserAssignment_ActiveTabChanged">
                                    <cc1:TabPanel runat="server" ID="TabPanel1">
                                        <HeaderTemplate>
                                            General Information
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
                                                                 <tr height="15px"></tr>                                                                 
                                                                    <tr>
                                                                        <td width="13%">
                                                                            <label>
                                                                                Company Tag Line
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:TextBox ID="txtCompanyTagLine" runat="server"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdnCompanyID" runat="server" />
                                                                        </td>
                                                                        <td width="20%">
                                                                            <label>
                                                                                Chamber Ref Number
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:TextBox ID="txtChamberRefNumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Service Tax Number
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtServiceTaxNumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Import/Export License Number
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtImport_ExportLicenseNumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                NTN Number
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNTNNumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Company Form Footer
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCompanyFormFooter" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                GST Number
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtGSTNumber" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Company Report Footer
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCompanyReportFooter" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Company UAN
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCompanyUAN" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Pay Period From
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlPayPeriodFrom" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Company URL
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCompanyURL" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Pay Period To
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlPayPeriodTo" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Fiscal Year From
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtYearFrom" runat="server"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="txtYearFrom_CalendarExtender" 
                                                                                    runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                                                                                    PopupButtonID="txtYearFrom" 
                                                                                    TargetControlID="txtYearFrom">
                                                                                </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                Fiscal Year To
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtYearTo" runat="server"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="txtYearTo_CalendarExtender" 
                                                                                    runat="server" Enabled="True" Format="dd-MMM-yyyy" 
                                                                                    PopupButtonID="txtYearTo" 
                                                                                    TargetControlID="txtYearTo">
                                                                                </cc1:CalendarExtender>
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
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                Off Days
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBoxList ID="lstOffDays" runat="server" CssClass="list">
                                                                                <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                                                                                <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                                                                            </asp:CheckBoxList>
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
                                            Company Contact
                                            
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
                                                                <tr height="15px"></tr>
                                                                  <tr>
                                                                        <td width="5%" >
                                                                            <label>
                                                                                Phone
                                                                                <asp:HiddenField ID="hdnCompanyContactID" runat="server" />
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%" >
                                                                            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td width="5%" >
                                                                            <label>
                                                                               Address 1
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td >
                                                                            <label>
                                                                                Fax
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td >
                                                                            <label>
                                                                                Address 2
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td >
                                                                            <label>
                                                                                Email
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td >
                                                                            <label>
                                                                                City
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td> 
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td >
                                                                            <label>
                                                                               Country
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <asp:Panel ID="pnlContacts" runat="server" ScrollBars="Auto" Height="275px">
                                                          <asp:Repeater ID="rptContents" runat="server" OnItemCommand="rptContents_ItemCommand">
                                                          <HeaderTemplate>
                                                               <table style="width: 100%; margin-top: 0px">
                                                                  <tr class="tblheading">
                                                                        <td>
                                                                            
                                                                         Phone 
                                                                                
                                                                        </td>
                                                                        <td>
                                                                           Fax
                                                                        </td>
                                                                       
                                                                        <td>
                                                                           Address
                                                                        </td>
                                                                         <td>
                                                                            
                                                                             Email 
                                                                           
                                                                        </td>
                                                                         <td>
                                                                            
                                                                              City 
                                                                           
                                                                        </td>
                                                                         <td>
                                                                            
                                                                               Country 
                                                                            
                                                                        </td>
                                                                    </tr>
                                                             </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <tr>
                                                                      <td>
                                                                          <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("Phone")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                        <%# Eval("Fax")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                          <%# Eval("AddressL1")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                 <%# Eval("Email")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton4" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                          <%# Eval("City")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton7" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                            <%# Eval("Country")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                  </tr>
                                                              </ItemTemplate>
                                                             
                                                             <AlternatingItemTemplate>
                                                             <tr style="background-color:#E4E4E4">
                                                                      <td>
                                                                          <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("Phone")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                        <%# Eval("Fax")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                          <%# Eval("AddressL1")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                 <%# Eval("Email")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton4" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                          <%# Eval("City")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton7" CommandArgument='<%# Eval("CompanyContactID")%>' CommandName="CompanyContactID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                            <%# Eval("Country")%>
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
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="2" ID="TabPanel3">
                                        <HeaderTemplate>
                                            Company Bank Info
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
                                                                <tr height="15px"></tr>
                                                                        <tr>
                                                                        <td width="8%">
                                                                            <label>
                                                                                  Bank Name
                                                                                  <asp:HiddenField ID="hdnCompanyBankID" runat="server" />
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:TextBox ID="txtBankName" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td width="12%">
                                                                            <label>
                                                                               Bank Account Number
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:TextBox ID="txtBankAccountNo" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td >
                                                                            <label>
                                                                                Bank Branch
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBankBranch" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                 Bank Account Type
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBapenkAccountType" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                               Bank Code
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBankCode" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <label>
                                                                                 Contact Person
                                                                            </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtContactPerson" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <asp:Panel ID="pnlBankInfo" runat="server" ScrollBars="Auto" Height="300px">
                                                            <asp:Repeater ID="rptBanks" runat="server" OnItemCommand="rptBanks_ItemCommand">
                                                          <HeaderTemplate>
                                                               <table style="width: 100%; margin-top: 0px">
                                                                  <tr class="tblheading">
                                                                        <td>
                                                                            
                                                                         	Bank Name 		 	 	
                                                                                
                                                                        </td>
                                                                        <td>
                                                                           Bank Account Number 
                                                                        </td>
                                                                       
                                                                        <td>
                                                                           Bank Account Type 
                                                                        </td>
                                                                         <td>
                                                                            
                                                                             Bank Code 
                                                                           
                                                                        </td>
                                                                         <td>
                                                                            
                                                                              Bank Branch 
                                                                           
                                                                        </td>
                                                                         <td>
                                                                            
                                                                               Contact Person 
                                                                            
                                                                        </td>
                                                                    </tr>
                                                             </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <tr>
                                                                      <td>
                                                                          <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("BankName")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                        <%# Eval("BankAccountNumber")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                          <%# Eval("BankAccountType")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                 <%# Eval("BankCode")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton4" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                          <%# Eval("BankBranch")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton7" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                            <%# Eval("ContactPerson")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                  </tr>
                                                              </ItemTemplate>
                                                             
                                                             <AlternatingItemTemplate>
                                                             <tr style="background-color:#E4E4E4">
                                                                      <td>
                                                                          <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("BankName")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                        <%# Eval("BankAccountNumber")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                          <%# Eval("BankAccountType")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                 <%# Eval("BankCode")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton4" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                          <%# Eval("BankBranch")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="LinkButton7" CommandArgument='<%# Eval("CompanyBankInfoID")%>' CommandName="CompanyBankInfoID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                            <%# Eval("ContactPerson")%>
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
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="2" ID="TabPanel4">
                                        <HeaderTemplate>
                                            Company Locations
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
                                                                <tr height="15px"></tr>
                                                                 <tr>
                                                                        <td width="16%">
                                                                            <label>
                                                                                  Location / Branch Name
                                                                                  <asp:HiddenField ID="hdnCompanyLocationID" runat="server" />
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                                                                        </td>
                                                                         </td>
                                                                            <td>
                                                                            
                                                                            </td>
                                                                    <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="10%">
                                                                                <label>
                                                                                    Active/Inactive
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:DropDownList ID="ddlLocationIsActive" runat="server">
                                                                                    <asp:ListItem Selected="True" Text="Active" Value="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="InActive" Value="False"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                            
                                                                            </td>
                                                                    <td></td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>
                                                            <asp:Panel ID="pnlLocations" runat="server" ScrollBars="Auto" Height="325px">
                                                            <asp:Repeater ID="rptLocations" runat="server" OnItemCommand="rptLocations_ItemCommand">
                                                          <HeaderTemplate>
                                                               <table style="width: 100%; margin-top: 0px">
                                                                  <tr class="tblheading">
                                                                        <td>
                                                                            
                                                                         	Location / Branch Name 		 	 	
                                                                                
                                                                        </td>
                                                                        <td>
                                                                           
                                                                        </td>
                                                                    </tr>
                                                             </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <tr>
                                                                      <td>
                                                                          <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("CompanyLocationID")%>' CommandName="CompanyLocationID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("LocationName")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                         
                                                                      </td>
                                                            
                                                                  </tr>
                                                              </ItemTemplate>
                                                             
                                                             <AlternatingItemTemplate>
                                                             <tr style="background-color:#E4E4E4">
                                                                     <td>
                                                                          <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("CompanyLocationID")%>' CommandName="CompanyLocationID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("LocationName")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                         
                                                                      </td>
                                                                  </tr>
                                                             </AlternatingItemTemplate>
                                                             <FooterTemplate>
                                                              </table>
                                                             </FooterTemplate>
                                                               </asp:Repeater>
                                                               </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="2" ID="TabPanel5">
                                        <HeaderTemplate>
                                            Units
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
                                                                <tr height="15px"></tr>
                                                                 <tr>
                                                                        <td width="20%">
                                                                            <label>
                                                                                  Location / Branch Name
                                                                                  <asp:HiddenField ID="hdnUnitID" runat="server" />
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                            <asp:DropDownList ID="ddlLocation" runat="server"  OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                        </td>
                                                                         <td width="20%">
                                                                        </td>
                                                                       <td width="30%">
                                                                       </td>
                                                                        </tr>
                                                                        <tr>
                                                                        <td width="20%">
                                                                            <label>
                                                                             UnitName 
                                                                            </label>
                                                                        </td>
                                                                        <td width="30%">
                                                                           <asp:TextBox ID="txtUnitName" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        <label>
                                                                        Active/Inactive
                                                                        </label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlUnitIsActive" runat="server">
                                                                                <asp:ListItem Selected="True" Text="Active" Value="True"></asp:ListItem>
                                                                                <asp:ListItem Text="InActive" Value="False"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <asp:Panel ID="pnlUnits" runat="server" ScrollBars="Auto" Height="300px">
                                                            <asp:Repeater ID="rptUnits" runat="server" OnItemCommand="rptUnits_ItemCommand">
                                                          <HeaderTemplate>
                                                               <table style="width: 100%; margin-top: 0px">
                                                                  <tr class="tblheading">
                                                                        <td>
                                                                            
                                                                         	Unit Name 		 	 	
                                                                                
                                                                        </td>
                                                                        <td>
                                                                           
                                                                        </td>
                                                                    </tr>
                                                             </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <tr>
                                                                      <td>
                                                                          <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("UnitID")%>' CommandName="UnitID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("UnitName")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                         
                                                                      </td>
                                                            
                                                                  </tr>
                                                              </ItemTemplate>
                                                             
                                                             <AlternatingItemTemplate>
                                                             <tr style="background-color:#E4E4E4">
                                                                      <td>
                                                                          <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("UnitID")%>' CommandName="UnitID"
                                                                              runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("UnitName")%>
                                                                          </asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                         
                                                                      </td>
                                                                  </tr>
                                                             </AlternatingItemTemplate>
                                                             <FooterTemplate>
                                                              </table>
                                                             </FooterTemplate>
                                                               </asp:Repeater>
                                                               </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
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
                              <%if (Session["EditCompanyID"] != null)
                                {%>
                                <asp:LinkButton ID="btSave" ValidationGroup="grpCompany" runat="server" OnClick="btSave_Click"><img src="../images/btn-update.png" /></asp:LinkButton>
                                <%}
                                else
                                { %>
                                <asp:LinkButton ID="btnUpdate" runat="server" ValidationGroup="grpCompany" OnClick="btSave_Click"><img src="../images/btn-save.png"/></asp:LinkButton>
                                <%} %>
                                <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>      
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
                    $('#pic').attr('src', e.target.result).width(75).height(68);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
</script>
</asp:Content>
