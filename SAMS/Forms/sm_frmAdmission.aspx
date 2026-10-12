<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="sm_frmAdmission.aspx.cs" Inherits="Forms_frmAdmision" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../AjaxLibrary/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            var val = '<%=Session["Image"] %>';
            if (val == '') {
            } else {
                var logo = '<%= Session["Image"]%>';
                $('#pic').attr('src', '../StudentImages/' + logo).width(80).height(85);
            }
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#pic').attr('src', e.target.result).width(80).height(85);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
       

    </script>
    <div id="right_data">
      
        <asp:UpdatePanel ID="up_head" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div style="float: left; vertical-align: middle; width: 785px; margin-top: 63px">
                    <table>
                        <tr>
                            <td width="120px">
                                <asp:Label ID="lbl" runat="server" Text="Reg No"></asp:Label>
                            </td>
                            <td width="120px">
                                <strong>
                                    <asp:Label ID="lblRegNo" runat="server" Font-Underline="true"></asp:Label></strong>
                            </td>
                            <td>
                                <asp:Label ID="Label28" runat="server" Text="Program Study"></asp:Label>
                            </td>
                            <td width="235px">
                                <asp:DropDownList ID="DrpStudyPrograms" runat="server" Width="200px" AutoPostBack="true"
                                    OnSelectedIndexChanged="DrpStudyPrograms_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label32" runat="server" Text="Session"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpSession" runat="server" Width="120px" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
                <div>
                    <table>
                        <tr>
                            <td>
                                <label for="ctl00_ctl00_mainCopy_cphPage_fuStudent" id="Label30"  runat="server" style="cursor: pointer;border:1px thin black;">
                                    <img src="../images/BlankPerson.jpg" id="pic" name="pic" width="80" height="85"
                                        alt="" />
                                </label>
                                <asp:FileUpload ID="fuStudent" onchange="readURL(this);" Style="display: none;" runat="server">
                                </asp:FileUpload>
                            </td>
                        </tr>
                    </table>
                </div>
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                 <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblStudentName" runat="server" Text="Name of Student"></asp:Label>
                            </td>
                            <td colspan="10">
                                <asp:TextBox ID="txtStudentName" runat="server" Width="615px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfv_StudentName" ControlToValidate="txtStudentName"
                                    runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:123px;">
                                <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                            </td>
                            <td style="width:340px;">
                                <asp:RadioButtonList ID="rdbGender" runat="server" Width="200px" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Male" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Marital Status"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdbM_Status" runat="server" Width="140px" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Single" Value="false" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Married" Value="true"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Religion"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReligion" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Nationality"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNationality" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Domicile"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpDomicile" runat="server" Width="200px">
                                </asp:DropDownList>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Date of Birth"></asp:Label>
                            </td>
                            <td>
                                <%-- <asp:TextBox ID="txtBirthDate" runat="server"></asp:TextBox>--%>
                                <telerik:RadDatePicker ID="txtBirthDate" runat="server" Width="138px" Skin="Outlook">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Mobile No"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMobileNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Blood Group"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpBloodGroup" runat="server" Width="200px">
                                    <asp:ListItem Selected="True" Text="A-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="A+" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="B-" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="B+" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="O-" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="O+" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="AB-" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="AB+" Value="7"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="CNIC"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtCNIC" runat="server" Width="338px"></asp:TextBox>
                               <%-- <cc1:MaskedEditExtender ID="txtHomePhone_MaskedEditExtender" runat="server" AutoComplete="False"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True" Mask="99999-9999999-9" TargetControlID="txtCNIC">
                                </cc1:MaskedEditExtender>--%>
                            </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Email"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="200"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Permanent Address"></asp:Label>
                            </td>
                            <td colspan="6">
                                <asp:TextBox ID="txtPermanent_Address" runat="server" TextMode="MultiLine" Width="335px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Postal Address"></asp:Label>
                            </td>
                            <td colspan="6">
                                <asp:TextBox ID="txtPostal_Address" runat="server" TextMode="MultiLine" Width="335px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Father/Guadian Name"></asp:Label>
                            </td>
                            <td colspan="10">
                                <asp:TextBox ID="txtFatherName" runat="server" Width="338px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Occupation"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtOccupation" runat="server" Width="200"></asp:TextBox>
                            </td>
                            
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Email"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtF_Email" runat="server" Width="200"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtF_Email"
                                    ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Office Address"></asp:Label>
                            </td>
                            <td colspan="6">
                                <asp:TextBox ID="txtFO_Address" runat="server" Width="335px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label16" runat="server" Text="CNIC"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtF_CNIC" runat="server" Width="338px"></asp:TextBox>
                               <%-- <cc1:MaskedEditExtender ID="me_CNIC" runat="server" AutoComplete="False" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" Mask="99999-9999999-9" TargetControlID="txtF_CNIC">
                                </cc1:MaskedEditExtender>--%>
                            </td>
                            <td>
                                <asp:Label ID="Label17" runat="server" Text="Mobile No"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtF_MobileNo" runat="server" Width="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Phone Office"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtFO_No" runat="server" Width="338px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label19" runat="server" Text="Residence"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtResidence" runat="server" Width="200"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--    </ContentTemplate>
        </asp:UpdatePanel>
            <asp:UpdatePanel ID="upAcademic_qlf" runat="server">
                <ContentTemplate>--%>
                <div>
                    <fieldset>
                        <legend style="font-size:small;">Academic Qualification</legend>
                        <table>
                            <tr>
                                <td style="height: 16px" align="center">
                                    <asp:Label ID="lblskuname" runat="server" Width="200px" Height="16px" ForeColor="White"
                                        Font-Bold="True" Text="Certificate/Degree" BackColor="#006699"></asp:Label>
                                </td>
                                <td align="center" style="height: 16px">
                                    <asp:Label ID="Label20" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                        Height="16px" Text="Session" Width="100%" Enabled="False"></asp:Label>
                                </td>
                                <td style="height: 16px" align="center">
                                    <asp:Label ID="Label21" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                        Height="16px" Text="Marks Obtained" Width="120px"></asp:Label>
                                </td>
                                <td style="height: 16px" align="center">
                                    <asp:Label ID="lblCGPA" runat="server" Width="100px" Height="16px" ForeColor="White"
                                        Font-Bold="True" Text="% CGPA" BackColor="#006699"></asp:Label>
                                </td>
                                <td align="center" style="height: 16px">
                                    <asp:Label ID="Label22" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                        Height="16px" Text="Borad/University" Width="100%" Enabled="False"></asp:Label>
                                </td>
                                <td style="height: 16px" align="center">
                                    <asp:Label ID="Label24" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                        Height="16px" Text="Add" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCertificate" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSession" runat="server" Width="100px">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMarks" runat="server" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCGPA" runat="server" Width="100px" Text="0"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fte_CGPA" TargetControlID="txtCGPA" runat="server"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBoard" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnAdd" runat="server" Font-Size="8pt" Text="Add" Width="100px" AccessKey="A"
                                        CssClass="Button" OnClick="btnAdd_Click" CausesValidation="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="8">
                                    <asp:Panel ID="Panel2" runat="server" Height="100px" ScrollBars="Vertical" Width="847px"
                                        BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                        <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" BackColor="White"
                                            HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False"
                                            OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing">
                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                PreviousPageText="Previous"></PagerSettings>
                                            <Columns>
                                                <asp:BoundField DataField="CERTIFICATE" HeaderText="Certificate">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                        Width="200px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SESSION" HeaderText="Session">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MARKS" HeaderText="Marks Obtained">
                                                    <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                        Width="120px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CGPA" HeaderText="%age CGPA">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BOARD" HeaderText="Board/University">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="200px" />
                                                </asp:BoundField>
                                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="35px"></ItemStyle>
                                                </asp:CommandField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            CommandName="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="40px">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
                <div>
                    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <fieldset>
                        <legend style="font-size:small;">Sublings</legend>
                        <table width="400px">
                            <tr>
                                <td style="height: 18px" align="center">
                                    <asp:Label ID="Label23" runat="server" Width="200px" Height="16px" ForeColor="White"
                                        Font-Bold="True" Text="Name" BackColor="#006699"></asp:Label>
                                </td>
                                <td style="height: 18px" align="center">
                                    <asp:Label ID="Label25" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                        Height="16px" Text="Roll No" Width="100px" Enabled="False"></asp:Label>
                                </td>
                                <td style="height: 18px" align="center">
                                    <asp:Label ID="Label26" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                        Height="16px" Text="Program" Width="120px"></asp:Label>
                                </td>
                                <td style="height: 18px" align="center">
                                    <asp:Label ID="Label27" runat="server" Width="100px" Height="16px" ForeColor="White"
                                        Font-Bold="True" Text="Year" BackColor="#006699"></asp:Label>
                                </td>
                                <td style="height: 18px" align="center">
                                    <asp:Label ID="Label29" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                        Height="16px" Text="Add" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSubling_Name" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubling_RollNo" runat="server" Width="100px">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubling_Program" runat="server" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubling_Year" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn_Add2" CausesValidation="false" runat="server" Font-Size="8pt"
                                        Text="Add" Width="100px" AccessKey="A" CssClass="Button" OnClick="btn_Add2_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="8">
                                    <asp:Panel ID="Panel1" runat="server" Height="50px" ScrollBars="Vertical" Width="640px"
                                        BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                        <asp:GridView ID="grdSubling" runat="server" ForeColor="SteelBlue" BackColor="White"
                                            HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False"
                                            OnRowDeleting="grdSubling_RowDeleting" OnRowEditing="grdSubling_RowEditing">
                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                PreviousPageText="Previous"></PagerSettings>
                                            <Columns>
                                                <asp:BoundField DataField="NAME" HeaderText="Name">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                        Width="200px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ROLLNO" HeaderText="Roll No">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PROGRAM" HeaderText="Program">
                                                    <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                        Width="120px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="YEAR" HeaderText="Year">
                                                    <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                        Width="90px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="35px"></ItemStyle>
                                                </asp:CommandField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            CommandName="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="40px">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <table>
        <tr>
            <td>
                <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="Button" Width="90px"
                    OnClick="btn_Save_Click" />
                <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CssClass="Button" Width="90px"
                    OnClick="btn_Cancel_Click" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <div>
        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>--%>
        <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
            width: 100%; border-bottom: silver thin inset; background-color: silver">
            <tbody>
                <tr>
                    <td style="height: 21px" align="left">
                        <asp:Label ID="Label31" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                    </td>
                    <td style="width: 170px; height: 21px" align="left">
                        <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                            <asp:ListItem Value="REGNO">Reg No</asp:ListItem>
                            <asp:ListItem Value="STUDENT_NAME">Student Name</asp:ListItem>
                            <asp:ListItem Value="MOBILE_NO">Contact Number</asp:ListItem>
                            <asp:ListItem Value="EMAIL">Email Address</asp:ListItem>
                            <asp:ListItem Value="CNIC">CNIC</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 224px; height: 21px" align="left">
                        <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                    </td>
                    <td style="height: 21px" align="left" width="250">
                        <asp:Button ID="btnSearch" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                            OnClick="btnSearch_Click" CausesValidation="false"></asp:Button>
                    </td>
                </tr>
            </tbody>
        </table>
        <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        <asp:Panel ID="Panel3" runat="server" Width="100%" Height="200px" ScrollBars="Vertical">
            <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter"
                OnRowEditing="Grid_users_RowEditing" HorizontalAlign="Center" AutoGenerateColumns="False"
                BackColor="White" BorderColor="White">
                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                    PreviousPageText="Previous"></PagerSettings>
                <RowStyle ForeColor="Black"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="STUDENT_ID" HeaderText="Student Id">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PROGRAM_ID" HeaderText="PROGRAM_ID">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                        </ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DOMICILE_ID" HeaderText="DOMICILE_ID">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="BLOOD_ID" HeaderText="BLOOD_ID">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="REG_NO" HeaderText="Code">
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="STUDENT_NAME" HeaderText="Name">
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PROGRAM_NAME" HeaderText="PROGRAM STUDY">
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MOBILE_NO" HeaderText="Contact Number">
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="EMAIL" HeaderText="Email">
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CNIC" HeaderText="CNIC">
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="FATHER_NAME" HeaderText="FATHER_NAME">
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="DOCUMENT_DATE" DataFormatString="{0:dd-MMM-yyyy}">
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="GENDER" HeaderText="GENDER">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MARITAL_STATUS" HeaderText="MARITAL_STATUS">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NATIONALITY" HeaderText="NATIONALITY">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="RELIGION" HeaderText="RELIGION">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PERMANENT_ADDRESS" HeaderText="PERMANENT_ADDRESS">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="POSTAL_ADDRESS" HeaderText="POSTAL_ADDRESS">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="BIRTH_DATE" HeaderText="BIRTH_DATE">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="F_EMAIL" HeaderText="F_EMAIL">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="F_CNIC" HeaderText="F_CNIC">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="F_MOBILE_NO" HeaderText="F_MOBILE_NO">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="F_OFFICE_ADDRESS" HeaderText="F_OFFICE_ADDRESS">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="F_OFFICE_PHONE" HeaderText="F_OFFICE_PHONE">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="OCCUPATION" HeaderText="OCCUPATION">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="RESIDENCE" HeaderText="RESIDENCE">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IMAGE_NAME" HeaderText="IMAGE_NAME">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SESSION_ID" HeaderText="SESSION_ID">
                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                    </asp:BoundField>
                    <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="White"></FooterStyle>
                <PagerStyle BackColor="Transparent"></PagerStyle>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#007395"
                    Font-Bold="True" ForeColor="White"></HeaderStyle>
                <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333">
                </AlternatingRowStyle>
            </asp:GridView>
        </asp:Panel>
    </div>
    <asp:UpdatePanel ID="up_hfiedls" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_CustomerID" runat="server" />
            <asp:HiddenField ID="hf_Image" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
