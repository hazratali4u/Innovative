<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmDocumentPrinting.aspx.cs" Inherits="Forms_frmDocumentPrinting" Title="Print Sale Document" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
     <script language="JavaScript" type="text/javascript">
    function ValidateForm()
	{
		return true;	  		
	}
    </script>
    <div id="right_data">
     <div >
        <table width="100%">
            <tr>
                <td >
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<TABLE><TBODY><TR><TD align=left colSpan=1></TD><TD align=left colSpan=4>
<asp:Label id="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label> </TD></TR><TR><TD style="HEIGHT: 23px" align=left colSpan=1></TD>
    <TD style="HEIGHT: 23px" align=left colSpan=4><DIV id="divFilter" class="containeRadioButtons"><TABLE width="100%"><TBODY><TR><TD align=left><asp:RadioButtonList id="rblCustomerType" runat="server" Width="300px" RepeatDirection="Horizontal">
<asp:ListItem Value="1" Selected="True">GST</asp:ListItem>
<asp:ListItem Value="0">Non GST</asp:ListItem>
</asp:RadioButtonList></TD></TR></TBODY></TABLE></DIV></TD></TR>
<TR><TD align=left></TD><TD align=left></TD><TD align=left>
<strong><asp:Label id="Label2" runat="server" Width="95px" Text="Document Type" CssClass="lblbox"></asp:Label></strong></TD><TD align=left></TD><TD style="HEIGHT: 25px" align=left><asp:DropDownList id="DrpLedgerType" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="DrpLedgerType_SelectedIndexChanged" AutoPostBack="True">            
            <asp:ListItem Value="1">Sales Tax Invoice</asp:ListItem>
            <asp:ListItem Value="2">Sale Return</asp:ListItem>
            <asp:ListItem Value="3">Delivery Challan</asp:ListItem> 
            <asp:ListItem Value=" 	4">Goods Receipt Note [GRN]</asp:ListItem>
</asp:DropDownList></TD></TR><TR><TD align=left></TD><TD align=left></TD><TD align=left>
<strong><asp:Label id="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong></TD><TD align=left></TD><TD style="HEIGHT: 25px" align=left><asp:DropDownList id="drpDistributor" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList></TD></TR>
    <tr>
        <td align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
        <td align="left">
            <strong>
            <asp:Label ID="lblPrincipal" runat="server" CssClass="lblbox" Text="Supplier" 
                Width="94px"></asp:Label>
            </strong>
        </td>
        <td align="left">
            &nbsp;</td>
        <td align="left" style="HEIGHT: 25px">
            <asp:DropDownList ID="ddlPrincipal" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <TR><TD align=left></TD><TD align=left></TD><TD align=left>
            <strong>
            <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Customer" 
                Width="94px"></asp:Label>
            </strong></TD><TD align=left></TD><TD style="HEIGHT: 25px" align=left>
        <asp:DropDownList ID="DrpCustomer" runat="server" CssClass="DropList" 
            Width="200px">
        </asp:DropDownList>
        </TD></TR><TR><TD align=left></TD><TD align=left></TD><TD align=left>
            <strong>
            <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" 
                Width="90px"></asp:Label>
            </strong></TD><TD align=left></TD><TD style="HEIGHT: 25px" align=left>
        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10" 
            onkeyup="BlockStartDateKeyPress()" Width="150px"></asp:TextBox>
        <asp:ImageButton ID="ibtnStartDate" runat="server" 
            ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
        </TD></TR><TR><TD align=left></TD><TD align=left></TD><TD align=left>
            <strong>
            <asp:Label ID="Label4" runat="server" Height="16px" 
                style="margin-top: 0px; margin-bottom: 0px" Text="To Date" Width="80px"></asp:Label>
            </strong></TD><TD align=left></TD><TD style="HEIGHT: 25px" align=left>
        <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10" 
            onkeyup="BlockEndDateKeyPress()" Width="150px"></asp:TextBox>
        <asp:ImageButton ID="ibnEndDate" runat="server" 
            ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
        </TD></TR>
      <TR><TD> <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy"
                PopupButtonID="ibtnStartDate" TargetControlID="txtStartDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                TargetControlID="txtEndDate">
            </cc1:CalendarExtender></TD></TR>
    </TBODY></TABLE>
</contenttemplate>
 </asp:UpdatePanel>
                    &nbsp; &nbsp;
        <asp:Button ID="btnViewPDF" runat="server" Width="90" Text="View PDF" OnClick="btnViewPDF_Click" CssClass="Button" />
        <asp:Button ID="btnViewExcel" runat="server" Width="90" Text="View Excel" OnClick="btnViewExcel_Click" CssClass="Button" /></td>
            </tr>
        </table>
         &nbsp;
        
           </div>
    </div>
</asp:Content>
