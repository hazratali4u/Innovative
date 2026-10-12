using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

public partial class pr_rptSalesSalary : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    GeoHierarchyController GeoCtl = new GeoHierarchyController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadRegion();            
            LoadTownZone();
            this.txtFromDate.Text = System.DateTime.Today.ToString("MMM-yyyy");
        }
    }

    protected void LoadRegion()
    {
        DrpTownRegion.Items.Clear();
        DataTable dt = GeoCtl.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        DrpTownRegion.Items.Add(new ListItem("---All---", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpTownRegion, dt, 0, 4);
    }

    /// <summary>
    /// Loads OrderBookers
    /// </summary>
    protected void DrpTownRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTownZone();
    }

    /// <summary>
    /// Loads Zones To Zone Combo Of Town Tab.
    /// </summary>
    private void LoadTownZone()
    {
        if (DrpTownRegion.Items.Count > 0)
        {
            DrpTownZone.Items.Clear();
            DataTable dt = GeoCtl.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTownRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            DrpTownZone.Items.Add(new ListItem("---All---", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTownZone, dt, 0, 4);
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


            DataSet dsSlip = PPC.GetSalesStaffSalary(Convert.ToInt32(Session["WorkingCompanyID"]), Constants.LongNullValue, Convert.ToInt32(DrpTownZone.SelectedValue), Convert.ToInt32(DrpTownRegion.SelectedValue), dtFrom, dtTo);


            SAMSBusinessLayer.Reports.PayrollReports.CrpSalesStaff CrpReport = new SAMSBusinessLayer.Reports.PayrollReports.CrpSalesStaff();
            CrystalDecisions.CrystalReports.Engine.ReportDocument srAllownces = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrystalDecisions.CrystalReports.Engine.ReportDocument srDeduction = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            srAllownces = CrpReport.OpenSubreport("srAllownces");
            srDeduction = CrpReport.OpenSubreport("srDeduction");

            srAllownces.SetDataSource(dsSlip);
            srDeduction.SetDataSource(dsSlip);
            CrpReport.SetDataSource(dsSlip);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Branch", DrpTownRegion.SelectedItem.Text);
            CrpReport.SetParameterValue("Department", DrpTownZone.SelectedItem.Text);            
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