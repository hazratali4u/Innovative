using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

public partial class pr_rptSalarySlipLabour : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLocation();
            fillddlEmployee(Constants.IntNullValue,Constants.IntNullValue);
            fillddlParentDepartment();
            this.txtFromDate.Text = System.DateTime.Today.ToString("MMM-yyyy");
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

    /// <summary>
    /// Loads OrderBookers
    /// </summary>
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
    
    protected void fillddlParentDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);

    }
    
    protected void fillddlEmployee(int LocationID , int DepartmentID)
    {

        DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, DepartmentID, LocationID, false, 1, Convert.ToInt32(Session["UserID"]));
        ddlEmployee.Items.Clear();
        this.ddlEmployee.Items.Add(new ListItem("--All--", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlEmployee, dt, 0, 1, false);
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

            CalculateOverTime(dtFrom, dtTo);

            DataTable dtAttendance = PPC.GetPaySlipLabour(Convert.ToInt32(Session["WorkingCompanyID"]), Convert.ToInt64(ddlEmployee.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), dtFrom, dtTo,2,Convert.ToInt32(Session["UserID"]));


            SAMSBusinessLayer.Reports.PayrollReports.CrpSalarySlipLabour CrpReport = new SAMSBusinessLayer.Reports.PayrollReports.CrpSalarySlipLabour();
            CrpReport.SetDataSource(dtAttendance);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Branch", ddlLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("Department", ddlDepartment.SelectedItem.Text);
            CrpReport.SetParameterValue("Employee", ddlEmployee.SelectedItem.Text);
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

    private void CalculateOverTime(DateTime dtFrom, DateTime dtTo)
    {
        CreateTable();
        AttendanceController AC = new AttendanceController();
        int EmployeeID = Constants.IntNullValue;
        if (ddlEmployee.SelectedValue != Constants.LongNullValue.ToString())
        {
            EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
        }
        DataTable dtEmployeeAttendance = AC.GetEmployeeAttendance(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), EmployeeID, dtFrom, Convert.ToDateTime(dtTo.ToShortDateString() + " 23:59:59"), Convert.ToInt32(Session["UserID"]));

        DataView view = new DataView(dtEmployeeAttendance);
        DataTable distinctEmployeeID = view.ToTable(true, "EmployeeID");

        foreach (DataRow dr in distinctEmployeeID.Rows)
        {
            DataRow[] results = dtEmployeeAttendance.Select("EmployeeID = '" + dr["EmployeeID"].ToString() + "'");
            if (results.Length > 0)
            {
                for (DateTime OverTimeDate = dtFrom; OverTimeDate < dtTo.AddDays(1); OverTimeDate = OverTimeDate.AddDays(1))
                {
                    for (int i = 0; i < results.Length; i++)
                    {
                        if (!AttendanceIDInTable(results[i]["AttendanceID"].ToString()))
                        {
                            int OverTimeMinutes = 0;

                            string nextRecord = "";
                            try
                            {
                                nextRecord = results[i + 1]["AttendanceType"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }

                            if (results[i]["AttendanceType"].ToString() == "0" && nextRecord == "1")
                            {
                                if (Convert.ToDateTime(results[i]["DayofMonth"]) == OverTimeDate && Convert.ToDateTime(results[i + 1]["DayofMonth"]) == OverTimeDate)
                                {
                                    OverTimeMinutes = (int)Convert.ToDateTime(Convert.ToDateTime(results[i + 1]["DayofMonth"]).ToShortDateString() + " " + results[i + 1]["TimeOfDay"]).Subtract(Convert.ToDateTime(Convert.ToDateTime(results[i]["DayofMonth"]).ToShortDateString() + " " + results[i]["TimeOfDay"])).TotalMinutes;
                                    AC.InsertEmployeeOverTime(OverTimeDate, Convert.ToInt32(dr["EmployeeID"]), OverTimeMinutes);
                                    AddAttendanceIDToTable(Convert.ToInt64(results[i]["AttendanceID"]));
                                    AddAttendanceIDToTable(Convert.ToInt64(results[i + 1]["AttendanceID"]));
                                    i++;
                                }
                                else if (Convert.ToDateTime(Convert.ToDateTime(results[i + 1]["DayofMonth"]).ToShortDateString() + " " + results[i + 1]["TimeOfDay"]) < Convert.ToDateTime(OverTimeDate.AddDays(1).ToShortDateString() + " " + Convert.ToDateTime(results[i + 1]["ShiftTimeFrom"]).ToShortTimeString()))
                                {
                                    OverTimeMinutes = (int)Convert.ToDateTime(Convert.ToDateTime(results[i + 1]["DayofMonth"]).ToShortDateString() + " " + results[i + 1]["TimeOfDay"]).Subtract(Convert.ToDateTime(Convert.ToDateTime(results[i]["DayofMonth"]).ToShortDateString() + " " + results[i]["TimeOfDay"])).TotalMinutes;
                                    AC.InsertEmployeeOverTime(Convert.ToDateTime(results[i + 1]["DayofMonth"]).AddDays(-1), Convert.ToInt32(dr["EmployeeID"]), OverTimeMinutes);
                                    AddAttendanceIDToTable(Convert.ToInt64(results[i]["AttendanceID"]));
                                    AddAttendanceIDToTable(Convert.ToInt64(results[i + 1]["AttendanceID"]));
                                    i++;
                                }
                                else if (Convert.ToDateTime(Convert.ToDateTime(results[i]["DayofMonth"]).ToShortDateString() + " " + results[i]["TimeOfDay"]) < Convert.ToDateTime(OverTimeDate.AddDays(1).ToShortDateString() + " " + Convert.ToDateTime(results[i + 1]["ShiftTimeFrom"]).ToShortTimeString()) && Convert.ToDateTime(Convert.ToDateTime(results[i + 1]["DayofMonth"]).ToShortDateString() + " " + results[i + 1]["TimeOfDay"]) > Convert.ToDateTime(OverTimeDate.AddDays(1).ToShortDateString() + " " + Convert.ToDateTime(results[i + 1]["ShiftTimeFrom"]).ToShortTimeString()))
                                {
                                    if (Convert.ToDateTime(Convert.ToDateTime(results[i]["DayofMonth"]).ToShortDateString() + " " + Convert.ToDateTime(results[i]["ShiftTimeFrom"]).ToShortTimeString()).Subtract(Convert.ToDateTime(Convert.ToDateTime(results[i]["DayofMonth"]).ToShortDateString() + " " + results[i]["TimeOfDay"])).Minutes > 60)
                                    {
                                        OverTimeMinutes = (int)Convert.ToDateTime(Convert.ToDateTime(results[i + 1]["DayofMonth"]).ToShortDateString() + " " + results[i + 1]["TimeOfDay"]).Subtract(Convert.ToDateTime(Convert.ToDateTime(results[i]["DayofMonth"]).ToShortDateString() + " " + results[i]["TimeOfDay"])).TotalMinutes;
                                        AC.InsertEmployeeOverTime(Convert.ToDateTime(results[i + 1]["DayofMonth"]).AddDays(-1), Convert.ToInt32(dr["EmployeeID"]), OverTimeMinutes);
                                        AddAttendanceIDToTable(Convert.ToInt64(results[i]["AttendanceID"]));
                                        AddAttendanceIDToTable(Convert.ToInt64(results[i + 1]["AttendanceID"]));
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                AddAttendanceIDToTable(Convert.ToInt64(results[i]["AttendanceID"]));
                            }
                        }
                    }
                }
            }
        }
    }

    private void AddAttendanceIDToTable(long AttendanceID)
    {
        DataTable dtAttendanceID = (DataTable)Session["dtAttendanceID"];
        DataRow dr = dtAttendanceID.NewRow();
        dr["AttendanceID"] = AttendanceID;
        dtAttendanceID.Rows.Add(dr);
        Session.Add("dtAttendanceID", dtAttendanceID);
    }

    private bool AttendanceIDInTable(string AttendanceID)
    {
        bool flag = false;
        DataTable dtAttendanceID = (DataTable)Session["dtAttendanceID"];
        foreach (DataRow dr in dtAttendanceID.Rows)
        {
            if (dr["AttendanceID"].ToString() == AttendanceID)
            {
                flag = true;
                break;
            }
        }

        return flag;
    }

    private void CreateTable()
    {
        DataTable dtAttendanceID = new DataTable();
        dtAttendanceID.Columns.Add("AttendanceID", typeof(long));
        Session.Add("dtAttendanceID", dtAttendanceID);
    }
}