<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="sm_frmStudentProgram.aspx.cs" Inherits="Forms_frmStudentProgram" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" language="javascript">
        function ValidateForm() {
            var str;

            str = document.getElementById("<%=txtProgramCode.ClientID %>").value;
            if (str.length == 0 || str == null) {
                alert('Plz enter Program Code');
                return false;
            }
            str = document.getElementById("<%=txtProgramName.ClientID %>").value;
            if (str.length == 0 || str == null) {
                alert('Plz enter Program Name');
                return false;
            }

            return true;
        }
    
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="up_header" runat="server">
            <ContentTemplate>
                <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                <cc1:TabContainer ID="tbProgram" runat="server" Width="999px" Height="380px" AutoPostBack="True"
                    ActiveTabIndex="0">
                    <cc1:TabPanel runat="server" ID="TabPanel1">
                        <HeaderTemplate>
                            Program
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table width="80%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblPCode" Text="Program Code" runat="server" Width="100px"></asp:Label></strong>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProgramCode" runat="server" Width="180px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblPName" Text="Program Name" runat="server" Width="100px"></asp:Label></strong>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProgramName" runat="server" Width="180px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="ChbIsActive" runat="server" Width="120px" Text="Is Active" Checked="true"
                                                                Enabled="false" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSaveProgram" runat="server" Text="Save" CssClass="Button" OnClick="btnSaveProgram_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image2" runat="server" Width="30px" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                                                </asp:Image>&nbsp; Loading ......
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <br />
                                        <asp:Panel ID="Panel2" runat="server" Height="233px" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" ScrollBars="Vertical" Width="85%">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GrdProgram" runat="server" Width="99.7%" ForeColor="SteelBlue"
                                                        Font-Size="9pt" CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                                        AutoGenerateColumns="False" OnRowEditing="GrdProgram_RowEditing">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <Columns>
                                                            <asp:BoundField DataField="PROGRAM_ID" HeaderText="Id">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PROGRAM_CODE" HeaderText="Program Code">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="100px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PROGRAM_NAME" HeaderText="Program Name">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="275px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="STATUS" HeaderText="Status">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="100px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" ID="TabPanel2">
                        <HeaderTemplate>
                            Courses
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="Label28" runat="server" Text="Program Study"></asp:Label></strong>
                                            </td>
                                            <td width="250px">
                                                <asp:DropDownList ID="DrpStudyPrograms" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <div>
                                        <fieldset>
                                            <legend>Course Information</legend>
                                            <table>
                                                <tr>
                                                    <td style="height: 16px">
                                                        <asp:Label ID="lblskuname" runat="server" Width="100px" Height="16px" ForeColor="White"
                                                            Font-Bold="True" Text="Theory" BackColor="#006699"></asp:Label>
                                                    </td>
                                                    <td style="height: 16px">
                                                        <asp:Label ID="Label20" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                            Height="16px" Text="Course Title" Width="200px" Enabled="False"></asp:Label>
                                                    </td>
                                                    <td style="height: 16px">
                                                        <asp:Label ID="Label21" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                            Height="16px" Text="Marks" Width="100px"></asp:Label>
                                                    </td>
                                                    <td style="height: 16px">
                                                        <asp:Label ID="Label1" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                            Height="16px" Text="Professional" Width="200px"></asp:Label>
                                                    </td>
                                                    <td style="height: 16px">
                                                        <asp:Label ID="Label24" runat="server" BackColor="#006699" Font-Bold="True" ForeColor="White"
                                                            Height="16px" Text="Add" Width="100px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtTheory" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCourseName" runat="server" Width="200px">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMarks" runat="server" Width="100px" Text="0"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fte_Marks" TargetControlID="txtMarks" runat="server"
                                                            ValidChars="0123456789.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DrpProfessional" runat="server" Width="200px">
                                                            <asp:ListItem Text="1st Professional" Selected="True" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="2nd Professional" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="3rd Professional" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="4th Professional" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="5th Professional" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="6th Professional" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="7th Professional" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="8th Professional" Value="8"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAdd" runat="server" Font-Size="8pt" Text="Add" Width="100px" AccessKey="A"
                                                            CssClass="Button" OnClick="btnAdd_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="7">
                                                        <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical" Width="800px"
                                                            BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                            <asp:GridView ID="GrdCourse" runat="server" Width="99%" ForeColor="SteelBlue" BackColor="White"
                                                                HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False"
                                                                OnRowDeleting="GrdCourse_RowDeleting" OnRowEditing="GrdCourse_RowEditing">
                                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                    PreviousPageText="Previous"></PagerSettings>
                                                                <Columns>
                                                                    <asp:BoundField DataField="THEORY" HeaderText="Theory">
                                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                            Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="COURSENAME" HeaderText="Course Title">
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="MARKS" HeaderText="Marks">
                                                                        <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                            Width="120px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PROFESSIONAL" HeaderText="Professional">
                                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                            Width="200px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PROFESSIONAL_ID" HeaderText="PROFESSIONALID">
                                                                        <ItemStyle CssClass="HidePanel" />
                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                    </asp:BoundField>
                                                                    <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="35px" />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="btnSaveCourse" runat="server" Text="Save" OnClick="btnSaveCourse_Click"
                                CssClass="Button" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" ID="TabPanel3">
                        <HeaderTemplate>
                            Session
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table width="80%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="Label2" Text="From" runat="server" Width="60px"></asp:Label></strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DrpSessionFrom" Width="150px" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="150px">
                                                        </td>
                                                        <td colspan="3">
                                                            <strong>
                                                                <asp:Label ID="Label4" Text="Session" runat="server" Width="100px"></asp:Label></strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DrpSession" runat="server" Width="150px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="DrpSession_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="Label3" Text="To" runat="server" Width="60px"></asp:Label></strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DrpSessionTo" Width="150px" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSaveSession" runat="server" Text="Save" CssClass="Button" OnClick="btnSaveSession_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image2" runat="server" Width="30px" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                                                </asp:Image>&nbsp; Loading .........
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <br />
                                        <asp:Panel ID="Panel3" runat="server" Height="233px" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" ScrollBars="Vertical" Width="42%">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GrdSession" runat="server" Width="99.7%" ForeColor="SteelBlue"
                                                        Font-Size="9pt" CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                                        AutoGenerateColumns="False" OnRowEditing="GrdSession_RowEditing">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <Columns>
                                                            <asp:BoundField DataField="SESSION_ID" HeaderText="Id">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PSession" HeaderText="Session">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="160px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </td>
                                    <td width="100px">
                                    </td>
                                    <td>
                                        <fieldset>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 40%" rowspan="4">
                                                                                            <asp:ListBox ID="lstUnAssignProgram" runat="server" Width="200px" Height="200px"
                                                                                                CssClass="DropList"></asp:ListBox>
                                                                                        </td>
                                                                                        <td style="width: 52px" align="center">
                                                                                        </td>
                                                                                        <td style="width: 40%" rowspan="4">
                                                                                            <asp:ListBox ID="lstAssignProgram" runat="server" Width="200px" Height="200px" CssClass="DropList">
                                                                                            </asp:ListBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 52px" align="center">
                                                                                            <asp:Button ID="btnAssignProgram" OnClick="btnAssignProgram_Click" runat="server"
                                                                                                Width="30px" Font-Size="8pt" Text=">" CssClass="Button" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 52px" align="center">
                                                                                            <asp:Button ID="btnUnAssignProgram" OnClick="btnUnAssignProgram_Click" runat="server"
                                                                                                Width="30px" Font-Size="8pt" Text="<" CssClass="Button" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 52px" align="center">
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
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
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" ID="TabPanel4">
                        <HeaderTemplate>
                            Fee
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table width="80%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="Label5" Text="Session" runat="server" Width="70px"></asp:Label></strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DrpFeeSession" Width="150px" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="DrpFeeSession_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="50px">
                                                        </td>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="Label6" Text="Program" runat="server" Width="70px"></asp:Label></strong>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DrpFeeProgram" runat="server" Width="200px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <table style="vertical-align: top;">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:GridView ID="grd_Accounts" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="steelblue">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID">
                                                                                <HeaderStyle CssClass="HidePanel" />
                                                                                <ItemStyle CssClass="HidePanel" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ACCOUNT_NAME" HeaderStyle-ForeColor="White" HeaderText="ACCOUNT NAME">
                                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderStyle-ForeColor="White" HeaderText="Fee Amount">
                                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAccountFee" runat="server" Text='<%#Eval("FEE_VALUE")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSaveFee" runat="server" Text="Save" CssClass="Button" OnClick="btnSaveFee_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel7">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image2" runat="server" Width="30px" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                                                </asp:Image>&nbsp; Loading .......
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
