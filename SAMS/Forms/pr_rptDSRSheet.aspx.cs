using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

public partial class pr_rptDSRSheet : System.Web.UI.Page
{
    GeoHierarchyController GeoCtl = new GeoHierarchyController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadTier();
            this.txtFromDate.Text = System.DateTime.Today.ToString("MMM-yyyy");
        }
    }

    protected void LoadTier()
    {
        try
        {
            DataTable dt = GeoCtl.GetTier(Constants.IntNullValue);
            ddlTier.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlTier, dt, "TIER_ID", "TIER_NAME", true);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
        
    /// <summary>
    /// Shows Order Booker Reports in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Order Booker Reports in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    
    protected void ShowReport(int ReportType)
    {
        try
        {
            ProcessPayrollController PPC = new ProcessPayrollController();
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();            
            DataTable dt = DPrint.SelectReportTitle(int.Parse("0"));

            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            DateTime dtFromMonth = DateTime.Parse(txtFromDate.Text);
            dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

            DateTime dtToMonth = DateTime.Parse(txtFromDate.Text);
            dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
            dtTo = dtTo.AddMonths(1).AddDays(-1);

            DataSet dsSlip = PPC.GetDSRSheet(Convert.ToInt32(ddlTier.SelectedValue), dtFrom, dtTo);

            SAMSBusinessLayer.Reports.PayrollReports.CrpDSRSheet CrpReport = new SAMSBusinessLayer.Reports.PayrollReports.CrpDSRSheet();
            CrystalDecisions.CrystalReports.Engine.ReportDocument srAllowance = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            
            srAllowance = CrpReport.OpenSubreport("srAllowance");

            srAllowance.SetDataSource(dsSlip);
            CrpReport.SetDataSource(dsSlip);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("ForMonth", this.txtFromDate.Text);            
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", ReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}
