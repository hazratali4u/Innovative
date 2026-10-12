<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="sm_frmStudentFeeView.aspx.cs" Inherits="Forms_frmStudentFeeView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
<style type="text/css">        
    .open div
    {
        display: none;
        float: left;
        position: absolute;
        padding-top:10px;
        padding-left: 5px;
        padding-right: 10px;
    }
    .open:hover div
    {
        display: block;
    }
    .open div a
    {
        display: block;
        height: 25px;
    }
    .open div a:hover
    {
        text-decoration: none;
    }
</style>
<script language="javascript" type="text/javascript">
    function pageLoad() {
        $('#<%=txtDate.ClientID %>').datepicker(
                {
                    showOn: 'both',
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true
                });

        $('#<%=txtMonth.ClientID %>').datepicker(
                {
                    dateFormat: 'MM yy',
                    changeMonth: true,
                    changeYear: true,
                    showButtonPanel: true,

                    onClose: function (dateText, inst) {
                        var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                        $(this).val($.datepicker.formatDate('MM yy', new Date(year, month, 1)));
                    }
                });


        $('#<%=txtMonth.ClientID %>').focus(function () {
            $(".ui-datepicker-calendar").hide();
            $("#ui-datepicker-div").position({
                my: "center top",
                at: "center bottom",
                of: $(this)
            });
        });
    }

    function ShowDateCalander() {
        $("#divDate").show();
        $("#divMonth").hide();
        $("#divYear").hide();
        $('#<%=hfDateType.ClientID %>').val('D');
        $('#<%=txtDate.ClientID %>').focus();
    }

    function ShowMonthCalander() {
        $("#divDate").hide();
        $("#divMonth").show();
        $("#divYear").hide();
        $('#<%=hfDateType.ClientID %>').val('M');
        $('#<%=txtMonth.ClientID %>').focus();
    }

    function ShowYearCalander() {
        $("#divDate").hide();
        $("#divMonth").hide();
        $("#divYear").show();
        $('#<%=hfDateType.ClientID %>').val('Y');
        $('#<%=txtMonth.ClientID %>').focus();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">

<div id="right_data">
    <div >
        <table width="98%">
            <tr>               
                <td>
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
                            <asp:Panel ID="pnEnquiryList" runat="server" Width="98%">
                            <table width="100%">
                                <tbody>
                                     <tr>
                                        <td>
                                            <asp:Literal ID="ltrlEnquiry" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                         <asp:Label ID="ltrAdd" runat="server"></asp:Label>
                                            <%--<a href="sm_frmStudentFee.aspx?LevelType=3&LevelID=150">Add New Fee</a> --%>
                                        </td>                                    
                                    </tr>                                  
                                    <tr>
                                        <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" > 
                                                <tr>
                                                    <th class="tblhead">
                                                        <td class="tblhead" style="width:40%;">
                                                            <%--Search Text--%> <asp:TextBox ID="txtCustomer" runat="server" Width="150px"></asp:TextBox>
                                                            <asp:Button ID="btnFilter" runat="server" Text="Filter" 
                                                                onclick="btnFilter_Click" Visible="false"/>
                                                         </td>
                                                         <td class="tblhead" style="width:25%;">
                                                            <div id="divDate">
                                                                <%--Document Date--%> <asp:TextBox ID="txtDate" runat="server" Width="100px" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div id="divMonth" style="display:none;">
                                                                Document Month <asp:TextBox ID="txtMonth" runat="server" Width="100px"></asp:TextBox>
                                                            </div>
                                                             <div id="divYear" style="display:none;">
                                                                Document Year <asp:DropDownList ID="ddlYear" runat="server">
                                                                                <asp:ListItem Text="2006" Value="2006"></asp:ListItem>
                                                                                <asp:ListItem Text="2007" Value="2007"></asp:ListItem>
                                                                                <asp:ListItem Text="2008" Value="2008"></asp:ListItem>
                                                                                <asp:ListItem Text="2009" Value="2009"></asp:ListItem>
                                                                                <asp:ListItem Text="2010" Value="2010"></asp:ListItem>
                                                                                <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                                                                <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                                                                <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                                                                <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                                                                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                                                                <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                                                                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                                                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                                                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                                                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>                                                                                
                                                                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                                                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                                                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                                                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                                                                <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                                                                                <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                                                                                <asp:ListItem Text="2027" Value="2027"></asp:ListItem>
                                                                                <asp:ListItem Text="2028" Value="2028"></asp:ListItem>
                                                                                <asp:ListItem Text="2029" Value="2029"></asp:ListItem>
                                                                                <asp:ListItem Text="2030" Value="2030"></asp:ListItem>
                                                                                <asp:ListItem Text="2031" Value="2031"></asp:ListItem>
                                                                                <asp:ListItem Text="2032" Value="2032"></asp:ListItem>
                                                                                <asp:ListItem Text="2033" Value="2033"></asp:ListItem>
                                                                                <asp:ListItem Text="2034" Value="2034"></asp:ListItem>
                                                                                <asp:ListItem Text="2035" Value="2035"></asp:ListItem>
                                                                             </asp:DropDownList>
                                                            </div>
                                                            </td>
                                                            <td valign="middle" class="tblhead" style="width:10%;">
                                                            <span class="open"> 
                                                             <img src="" />
                                                                <div style="background-color: #f5f5f6; border: solid 1px #a0a0a0; width:100px;margin-top:-10px;">                                                                     
                                                                    <a href='javascript:void()' style="text-decoration:none;" onclick="ShowDateCalander();">Date</a>                                                                        
                                                                    <a href='javascript:void()' style="text-decoration:none;" onclick="ShowMonthCalander();">Month</a>
                                                                    <a href='javascript:void()' style="text-decoration:none;" onclick="ShowYearCalander();">Year</a>                                                                              
                                                                </div>
                                                             </span>
                                                             &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnFilterDate" runat="server" Text="Filter" onclick="btnFilterDate_Click" />  
                                                          
                                                            </div>                                                              
                                                         </td>
                                                         <td align="right" class="tblhead" style="width:25%;">
                                                           <asp:DropDownList ID="dllEnquiryStatus" runat="server" AutoPostBack="True" 
                                                                 onselectedindexchanged="dllEnquiryStatus_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                            
                                                        </td>                                                        
                                                    </th>
                                                </tr>
                                            </table>

                                            <br />
                                            <table width="100%" border="0" cellspacing="0" cellpadding="2" > 
                                                <tr>
                                                        <td class="tblhead" style="width:5%;">
                                                            ID</td>
                                                        <td class="tblhead" colspan="3" style="width:15%;">
                                                        Program Name
                                                        </td>
                                                        <td class="tblhead" style="width:9%;">
                                                             Session
                                                         </td>
                                                          <td class="tblhead" style="width:8%;">
                                                            No.of Student
                                                        </td>
                                                        <td class="tblhead" style="width:15%;">
                                                            Updated By
                                                        </td>
                                                         <td class="tblhead" style="width:12%;">
                                                            Last Update
                                                        </td>
                                                                                                                 
                                                       
                                                        <td class="tblhead" colspan="3" style="width:10%;"></td>
                                                </tr>
                                            </table>
                                            <asp:Repeater ID="rEnquiryPrice" runat="server" 
                                                OnItemCommand="rEnquiryPrice_ItemCommand" 
                                                onitemdatabound="rEnquiryPrice_ItemDataBound">      
                                                <ItemTemplate>
                                                    <div style="background:#EFF3FB">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                        <td style="width:5%;"><%# Eval("FEE_ID")%> </td>                                                                        
                                                                        <td style="width:15%;"><%# Eval("PROGRAM_NAME")%> </td>
                                                                        <td style="width:9%;"><%# Eval("PSession")%></td>
                                                                        <td style="width:8%;"><%# Eval("NoOfStudent")%> </td> 
                                                                        <td style="width:15%;"><%# Eval("USER_ID")%></td>
                                                                        <td style="width:15%;"><%# ((DateTime)Eval("LAST_UPDATE")).ToString("dd-MMM-yyyy")%></td>
                                                                        <td style="width:2.5%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("FEE_ID") %>' ToolTip="View Fee">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                         <td style="width:2.5%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("FEE_ID") %>' ToolTip="Delete Fee" OnClientClick="return confirm('Are you sure? you are going to delete.');">
                                                                                <img src="../images/delete.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                      
                                                                    </tr>
                                                                </table>                                                            
                                                    </div>
                                                </ItemTemplate> 
                                                <AlternatingItemTemplate>
                                                    <div style="background:#ffffff">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                       <td style="width:5%;"><%# Eval("FEE_ID")%> </td>                                                                        
                                                                        <td style="width:15%;"><%# Eval("PROGRAM_NAME")%> </td>
                                                                        <td style="width:9%;"><%# Eval("PSession")%></td>
                                                                        <td style="width:8%;"><%# Eval("NoOfStudent")%> </td> 
                                                                        <td style="width:15%;"><%# Eval("USER_ID")%></td>
                                                                        <td style="width:15%;"><%# ((DateTime)Eval("LAST_UPDATE")).ToString("dd-MMM-yyyy")%></td>
                                                                        <td style="width:2.5%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("FEE_ID") %>' ToolTip="View Fee">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                         <td style="width:2.5%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("FEE_ID") %>' ToolTip="Delete Fee" OnClientClick="return confirm('Are you sure? you are going to delete.');">
                                                                                <img src="../images/delete.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>                                                           
                                                    </div>
                                                </AlternatingItemTemplate>                                                                                                
                                            </asp:Repeater>
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
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>    
                          <asp:HiddenField ID="hfDateType" Value = "D" runat="server"/>                    
                        </contenttemplate>
                    </asp:UpdatePanel>
                    
                    <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 10px;
                        height: 50px">
                        &nbsp;<asp:Panel ID="Panel21" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="23px" />
                                    Wait Update</ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                </td>               
            </tr>
        </table>
           </div>
    <div >
</div>
</div>
</asp:Content>
