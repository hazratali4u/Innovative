using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class pr_rptAdvanceIssuanceSlip : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLocation();
            FillddlContractType();
            fillddlEmployee(Constants.IntNullValue, Constants.IntNullValue);
            fillddlParentDepartment();
            txtFromDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            txtToDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
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
        
    protected void fillddlParentDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);

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
            var EC = new EmployeController();
            var DPrint = new DocumentPrintController();            
            DataTable dt = DPrint.SelectReportTitle(int.Parse("0"));
            DataTable dtList = ec.GetAdvanceIssuanceSlip(Convert.ToInt32(Session["WorkingCompanyID"]), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlContractType.SelectedValue), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text + " 23:59:59"),Convert.ToInt32(Session["UserID"]));
            SAMSBusinessLayer.Reports.PayrollReports.CrpAdvanceIssuanceSlip CrpReport = new SAMSBusinessLayer.Reports.PayrollReports.CrpAdvanceIssuanceSlip();
            CrpReport.SetDataSource(dtList);
            CrpReport.Refresh();
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

    private void FillddlContractType()
    {
        DataTable dt = ec.SelectEmployee_Contract(Constants.IntNullValue, (Int32.Parse(Session["WorkingCompanyID"].ToString())));
        ddlContractType.Items.Clear();
        ddlContractType.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlContractType, dt, 0, 2, false);
    }

    protected void fillddlEmployee(int LocationID, int DepartmentID)
    {
        DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, DepartmentID, LocationID, false, 1, Convert.ToInt32(Session["UserID"]));
        ddlEmployee.Items.Clear();
        this.ddlEmployee.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlEmployee, dt, 0, 1, false);
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int32 LocationID = Constants.IntNullValue;
        Int32 DepartmentID = Constants.IntNullValue;
        if (ddlDepartment.SelectedIndex != 0)
        {
            DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
        }
        if (ddlLocation.SelectedIndex != 0)
        {
            LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
        }
        fillddlEmployee(LocationID, DepartmentID);
    }
}