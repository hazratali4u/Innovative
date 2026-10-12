using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class pr_rptHR : Page
{

    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLocation();
            FillddlContractType();
            txtFromDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("MMM-yyyy");
            txtFromDate.Attributes.Add("readonly", "readonly");
        }
    }

    protected void LoadLocation()
    {
        try
        {
            DataTable dt = cc.SelectCompany_lOCATIONS(Convert.ToInt32(Session["WorkingCompanyID"]), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]));
            ddlLocation.Items.Clear();
            ddlLocation.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    
    private void FillddlContractType()
    {
        DataTable dt = ec.SelectEmployee_Contract(Constants.IntNullValue, (Int32.Parse(Session["WorkingCompanyID"].ToString())));
        ddlContractType.Items.Clear();
        ddlContractType.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlContractType, dt, 0, 2, false);
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

            DataTable  dsSlip = ppc.GetHRINNDW(Convert.ToInt32(Session["WorkingCompanyID"]), Constants.LongNullValue, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlContractType.SelectedValue), dtFrom, dtTo,Convert.ToInt32(Session["UserID"]));

            var crpReport = new SAMSBusinessLayer.Reports.PayrollReports.CrpHRInnDw() ;
            crpReport.SetDataSource(dsSlip);
            crpReport.Refresh();

            crpReport.SetParameterValue("Branch", ddlLocation.SelectedItem.Text);
            crpReport.SetParameterValue("Department", ddlContractType.SelectedItem.Text);
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