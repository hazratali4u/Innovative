using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class pr_rptPaymentReceipt : Page
{
    CompanyController cc = new CompanyController();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLocation();
            fillddlDistributor(Constants.IntNullValue, Constants.IntNullValue);
         
            txtFromDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("MMM-yyyy");
        }
    }

    protected void LoadLocation()
    {
        try
        {
            DataTable dt = cc.SelectCompany_lOCATIONS(Convert.ToInt32(Session["WorkingCompanyID"]), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]));
            ddlLocation.Items.Clear();
           // ddlLocation.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void fillddlDistributor(int DISTRIBUTOR_ID, int TOWN_ID)
    {
        var distCtl = new DistributorController();
        DataTable dt = distCtl.GetDistributor2(DISTRIBUTOR_ID, TOWN_ID);
       // ddlDistributor.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", false);

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
            var ppc = new ProcessPayrollController();
            var dPrint = new DocumentPrintController();
            var dt = dPrint.SelectReportTitle(int.Parse("1"));

            var dtFrom = new DateTime();
            var dtTo = new DateTime();

            DateTime dtFromMonth = DateTime.Parse(txtFromDate.Text);
            dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

            DateTime dtToMonth = DateTime.Parse(txtFromDate.Text);
            dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
            dtTo = dtTo.AddMonths(1).AddDays(-1);
            
            DataSet dsSlip = ppc.GetPaymentReceipt(Convert.ToInt32(Session["WorkingCompanyID"]), Constants.LongNullValue, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDistributor.SelectedValue), dtFrom, dtTo,Convert.ToInt32(Session["UserID"]));
            
            var crpReport = new SAMSBusinessLayer.Reports.PayrollReports.CrpPaymentReceipt();

            crpReport.SetDataSource(dsSlip);
            crpReport.Refresh();

            crpReport.SetParameterValue("Branch", ddlLocation.SelectedItem.Text);
            crpReport.SetParameterValue("Department", ddlDistributor.SelectedItem.Text);
            crpReport.SetParameterValue("Employee", "");
            crpReport.SetParameterValue("ForMonth", this.txtFromDate.Text);
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["Company_Name"].ToString() );


            this.Session.Add("CrpReport", crpReport);
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
