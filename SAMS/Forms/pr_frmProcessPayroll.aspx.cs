using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class pr_frmProcessPayroll : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    ProcessPayrollController ppc = new ProcessPayrollController();
    DataControl dc = new DataControl();
    decimal Allownce = 0;
    decimal Deduction = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.txtMonth.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("MMM-yyyy");
            txtMonth.Attributes.Add("readonly", "readonly");
            Session.Add("PageLoad","1");
            ResetForm();
            Session.Add("PageLoad", "0");            
        }
    }
    
    protected void btnSaveSalary_Click(object sender, EventArgs e)
    {
        bool flag = true;
        DateTime StartDate = Convert.ToDateTime(txtMonth.Text);
        DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
        foreach (RepeaterItem item in rptEmployee.Items)
        {
            CheckBox cbEmployee = (CheckBox)item.FindControl("cbEmployee");
            HiddenField hfSALARY_DETAIL_ID = (HiddenField) item.FindControl("hfSALARY_DETAIL_ID");
            HiddenField hdnEmployeeID = (HiddenField)item.FindControl("hdnEmployeeID");            
            Label lblDeduction = (Label)item.FindControl("lblDeduction");
            Label lblAllowances = (Label)item.FindControl("lblAllowances");
            Label lblBasicPay = (Label)item.FindControl("lblBasicPay");            
            TextBox txtOUTSIDE_HOURS = (TextBox)item.FindControl("txtOUTSIDE_HOURS");
            TextBox txtFine = (TextBox)item.FindControl("txtFine");
            TextBox txtVISITS = (TextBox)item.FindControl("txtVISITS");
            TextBox txtRemarks = (TextBox)item.FindControl("txtRemarks");
            CheckBox cbProcess = (CheckBox)item.FindControl("cbProcess");            
            if (hfSALARY_DETAIL_ID.Value == "0" && cbEmployee.Checked)
            {
                long SALARY_DETAIL_ID = Constants.LongNullValue;
                SALARY_DETAIL_ID = ppc.InsertSalary(Convert.ToDateTime(txtMonth.Text), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlSalaryBehaviour.SelectedValue), Convert.ToInt32(ddlBonus.SelectedValue), Convert.ToInt32(hdnEmployeeID.Value),
                    Convert.ToDecimal(dc.chkNull_0(lblBasicPay.Text)), Convert.ToDecimal(dc.chkNull_0(lblAllowances.Text)), Convert.ToDecimal(dc.chkNull_0(lblDeduction.Text)), Convert.ToDecimal(dc.chkNull_0(txtFine.Text)), Convert.ToInt32(Session["UserID"]), 0, Convert.ToInt32(dc.chkNull_0(txtOUTSIDE_HOURS.Text)), Convert.ToInt32(dc.chkNull_0(txtVISITS.Text)), txtRemarks.Text, cbProcess.Checked);
                if(SALARY_DETAIL_ID != Constants.LongNullValue)
                {
                    DataSet dsDetail = ppc.GetPayRollForPayPeriodDetail(Convert.ToInt32(Session["WorkingCompanyID"]), Convert.ToInt64(hdnEmployeeID.Value), Constants.IntNullValue, Constants.IntNullValue, StartDate, EndDate, Constants.IntNullValue);
                    foreach (DataRow dr in dsDetail.Tables[1].Rows)
                    {
                        if (Convert.ToDecimal(dr["Amount"]) > 0)
                        {
                            int DeductionType = Constants.IntNullValue;
                            int DeductionID = Constants.IntNullValue;

                            if (dr["DeductionDescription"].ToString() == "Absent")
                            {
                                DeductionType = 1;
                            }
                            else if (dr["DeductionDescription"].ToString() == "Loan")
                            {
                                DeductionType = 2;
                            }
                            else if (dr["DeductionDescription"].ToString() == "Lease")
                            {
                                DeductionType = 3;
                            }
                            else if (dr["DeductionDescription"].ToString() == "Advance")
                            {
                                DeductionType = 4;
                            }
                            else if (dr["DeductionDescription"].ToString() == "Fine")
                            {
                                DeductionType = 5;
                            }
                            else if (dr["DeductionDescription"].ToString() == "PF")
                            {
                                DeductionType = 6;
                                DeductionID = Convert.ToInt32(dr["DeductionID"]);
                            }
                            else
                            {
                                DeductionType = 0;
                                DeductionID = Convert.ToInt32(dr["DeductionID"]);
                            }
                            ppc.InsertDeduction(SALARY_DETAIL_ID, StartDate, Convert.ToInt32(hdnEmployeeID.Value), DeductionID, DeductionType, Convert.ToDecimal(dr["Amount"]));
                        }
                    }

                    foreach (DataRow dr in dsDetail.Tables[2].Rows)
                    {
                        if (Convert.ToDecimal(dr["Amount"]) > 0)
                        {
                            ppc.InsertAllowance(SALARY_DETAIL_ID, StartDate, Convert.ToInt32(hdnEmployeeID.Value), Convert.ToInt32(dr["AllowanceID"]), Convert.ToInt32(dr["TypeID"]), Convert.ToDecimal(dr["Amount"]));
                        }
                    }
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            else if(hfSALARY_DETAIL_ID.Value != "0")
            {
                if (ppc.UpdateSalary(Convert.ToInt64(hfSALARY_DETAIL_ID.Value), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlSalaryBehaviour.SelectedValue), Convert.ToInt32(ddlBonus.SelectedValue), Convert.ToInt32(hdnEmployeeID.Value),
                    Convert.ToDecimal(dc.chkNull_0(lblBasicPay.Text)), Convert.ToDecimal(dc.chkNull_0(lblAllowances.Text)), Convert.ToDecimal(dc.chkNull_0(lblDeduction.Text)), Convert.ToDecimal(dc.chkNull_0(txtFine.Text)), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), !cbEmployee.Checked, 0, Convert.ToInt32(dc.chkNull_0(txtOUTSIDE_HOURS.Text)), Convert.ToInt32(dc.chkNull_0(txtVISITS.Text)), txtRemarks.Text, cbProcess.Checked))
                {
                    ppc.DeleteDeduction(Convert.ToInt64(hfSALARY_DETAIL_ID.Value), Convert.ToInt32(hdnEmployeeID.Value));
                    ppc.DeleteAllowance(Convert.ToInt64(hfSALARY_DETAIL_ID.Value), Convert.ToInt32(hdnEmployeeID.Value));
                    DataSet dsDetail = ppc.GetPayRollForPayPeriodDetail(Convert.ToInt32(Session["WorkingCompanyID"]), Convert.ToInt64(hdnEmployeeID.Value), Constants.IntNullValue, Constants.IntNullValue, StartDate, EndDate, Constants.IntNullValue);
                    foreach (DataRow dr in dsDetail.Tables[1].Rows)
                    {
                        if (Convert.ToDecimal(dr["Amount"]) > 0)
                        {
                            int DeductionType = Constants.IntNullValue;
                            int DeductionID = Constants.IntNullValue;

                            if (dr["DeductionDescription"].ToString() == "Absent")
                            {
                                DeductionType = 1;
                            }
                            else if (dr["DeductionDescription"].ToString() == "Loan")
                            {
                                DeductionType = 2;
                            }
                            else if (dr["DeductionDescription"].ToString() == "Lease")
                            {
                                DeductionType = 3;
                            }
                            else if (dr["DeductionDescription"].ToString() == "Advance")
                            {
                                DeductionType = 4;
                            }
                            else if (dr["DeductionDescription"].ToString() == "Fine")
                            {
                                DeductionType = 5;
                            }
                            else if (dr["DeductionDescription"].ToString() == "PF")
                            {
                                DeductionType = 6;
                                DeductionID = Convert.ToInt32(dr["DeductionID"]);
                            }
                            else
                            {
                                DeductionType = 0;
                                DeductionID = Convert.ToInt32(dr["DeductionID"]);
                            }
                            ppc.InsertDeduction(Convert.ToInt64(hfSALARY_DETAIL_ID.Value), StartDate, Convert.ToInt32(hdnEmployeeID.Value), DeductionID, DeductionType, Convert.ToDecimal(dr["Amount"]));
                        }
                    }
                    foreach (DataRow dr in dsDetail.Tables[2].Rows)
                    {
                        if (Convert.ToDecimal(dr["Amount"]) > 0)
                        {
                            ppc.InsertAllowance(Convert.ToInt64(hfSALARY_DETAIL_ID.Value), StartDate, Convert.ToInt32(hdnEmployeeID.Value), Convert.ToInt32(dr["AllowanceID"]), Convert.ToInt32(dr["TypeID"]), Convert.ToDecimal(dr["Amount"]));
                        }
                    }
                }
                else
                {
                    flag = false;
                    break;
                }
            }
        }
        if (flag)
        {
            ResetForm();
        }
    }
    
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        ResetForm();
    }

    protected void ddlLocation_Change(object sender, EventArgs e)
    {
        int DepartmentID = Constants.IntNullValue;
        int LocationID = Constants.IntNullValue;
        int SalaryBehavoiurID = Constants.IntNullValue;
        DepartmentID = Int32.Parse(ddlDepartment.SelectedValue);
        if (ddlSalaryBehaviour.SelectedIndex != 0)
        {
            SalaryBehavoiurID = Int32.Parse(ddlSalaryBehaviour.SelectedValue);
        }
        LocationID = Int32.Parse(ddlLocation.SelectedValue);

        DateTime StartDate = Convert.ToDateTime(txtMonth.Text);
        DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
        fillrptEmployee(DepartmentID, LocationID, StartDate, EndDate, SalaryBehavoiurID);
    }
    
    protected void fillddlDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);
    }
    
    private void fillddlLocation()
    {
        DataTable dt = cc.SelectCompany_lOCATIONS(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]));
        ddlLocation.Items.Clear();
        clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);
    }

    private void ResetForm()
    {
        fillddlDepartment();
        fillddlLocation();
        DateTime StartDate = Convert.ToDateTime(txtMonth.Text);
        DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);        
        lblCompanyWorkingDays.Text = GetWorkingDays().ToString();        
        fillrptEmployee(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), StartDate, EndDate, Constants.IntNullValue);        
    }
    
    private void fillrptEmployee(int DepartmentID, int LocationID, DateTime  StartDate, DateTime EndDate, int  SalaryBehavoiurID)
    {
        if (Session["PageLoad"].ToString() == "1")
        {
            DataTable dt = new DataTable();
            dt = ppc.SELECT_PayRollForPayPeriod(Convert.ToInt32(Session["WorkingCompanyID"]), Constants.LongNullValue, LocationID, DepartmentID, true, StartDate, Convert.ToDateTime(EndDate.ToShortDateString() + " 23:59:59"), SalaryBehavoiurID);
            rptEmployee.DataSource = dt;
            rptEmployee.DataBind();
        }
        else
        {
            DataTable dt = new DataTable();
            dt = ppc.SELECT_PayRollForPayPeriod2(Convert.ToInt32(Session["WorkingCompanyID"]), Constants.LongNullValue, LocationID, DepartmentID, true, StartDate, Convert.ToDateTime(EndDate.ToShortDateString() + " 23:59:59"), SalaryBehavoiurID);
            rptEmployee.DataSource = dt;
            rptEmployee.DataBind();
        }
    }
    
    public int GetWorkingDays()
    {
        int totalDays = 0;

        DateTime StartDate = Convert.ToDateTime(txtMonth.Text);
        DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
        List<int> lstOffdays= getCompanyOffDays();
        for (var date = StartDate; date <= EndDate; date = date.AddDays(1))
        {
            if(!lstOffdays.Contains((int)date.DayOfWeek))
            {
                 totalDays++;
            }
        }
        return totalDays;
    }
    
    private List<int> getCompanyOffDays()
    {
        List<int> lstOffdays = new List<int>();
        DataTable dtOfDays = cc.SelectOffDays(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        if (dtOfDays != null)
        {
            foreach (DataRow dr in dtOfDays.Rows)
            {
                lstOffdays.Add(Int32.Parse(dr["OffDayValue"].ToString()));
            }
        }
        return lstOffdays;
    }
    
    protected void SavePayroll()
    {

        DataTable dt = ppc.SELECT_PAY_PERIOD(Convert.ToInt32(Session["WorkingCompanyID"]), false);
        int PayPeriodID = Int32.Parse(dt.Rows[0]["PayPeriodID"].ToString());
        foreach (RepeaterItem item in rptEmployee.Items)
        {
            HiddenField hdnEmployeeID = (HiddenField)item.FindControl("hdnEmployeeID");
            Label lblBasicPay = (Label)item.FindControl("lblBasicPay");
            HiddenField hfEmployeeWorkingDays = (HiddenField)item.FindControl("hfEmployeeWorkingDays");
            Label lblAllowances = (Label)item.FindControl("lblAllowances");
            HiddenField hfBonus = (HiddenField)item.FindControl("hfBonus");
            Label lblDeduction = (Label)item.FindControl("lblDeduction");
            Label lblIncomeTax = (Label)item.FindControl("lblIncomeTax");
            Label lblNetPay = (Label)item.FindControl("lblNetPay");
            ppc.InsertPayPeriod_Detail(PayPeriodID, Convert.ToInt32(hdnEmployeeID.Value), Convert.ToDecimal(lblBasicPay.Text), Convert.ToDecimal(hfEmployeeWorkingDays.Value), Convert.ToDecimal(lblAllowances.Text), Convert.ToDecimal(lblDeduction.Text), 0, 0, Convert.ToDecimal(lblNetPay.Text), 0, 0, Convert.ToDecimal(hfBonus.Value));
            ec.ApproveEmployee(0, Convert.ToInt32(hdnEmployeeID.Value));
        }
        DateTime StartDate = Convert.ToDateTime(txtMonth.Text);
        DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
        DateTime NewStartDate = Convert.ToDateTime(StartDate).AddMonths(1);
        DateTime NewEndDate = new DateTime(NewStartDate.Year, NewStartDate.Month, DateTime.DaysInMonth(NewStartDate.Year, NewStartDate.Month));
        DataTable dtInstallments = ppc.Select_Installments_ProcessPayroll(StartDate, EndDate, Convert.ToInt32(Session["WorkingCompanyID"]));
        if (dtInstallments != null)
        {
            foreach (DataRow dr in dtInstallments.Rows)
            {
                ppc.UpdateInstallments(Convert.ToInt32(dr["InstallmentID"]), Convert.ToDecimal(dr["AmountToBePaid"]));
            }
        }
        ppc.PostPayPeriod(PayPeriodID);
        ppc.InsertPayPeriod_Master(Convert.ToInt32(Session["WorkingCompanyID"]), NewStartDate, NewEndDate, Convert.ToInt32(Session["UserID"]), Convert.ToDateTime(Session["CurrentWorkDate"]));
        
        ResetForm();
    }
    
    protected void rptEmployee_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblBonus = e.Item.FindControl("lblBonus") as Label;
        Label lblNetPay = e.Item.FindControl("lblNetPay") as Label;
        Label lblBasicPay = e.Item.FindControl("lblBasicPay") as Label;
        Label lblApproved = e.Item.FindControl("lblApproved") as Label;
        CheckBox cbEmployee = e.Item.FindControl("cbEmployee") as CheckBox;
        CheckBox cbAllEmployee = e.Item.FindControl("cbAllEmployee") as CheckBox;
        if (cbAllEmployee != null)
        {
            cbAllEmployee.Checked = true;
        }
        if (lblBonus != null)
        {
            if (ddlBonus.SelectedValue == "0")
            {
                lblBonus.Text = 0.ToString();
            }
            else if (ddlBonus.SelectedValue == "1")
            {
                lblBonus.Text = lblNetPay.Text;
            }
            else
            {
                lblBonus.Text = lblBasicPay.Text;
            }

            if (lblApproved.Text != "Draft")
            {
                cbEmployee.Checked = true;
            }
        }
    }
    
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //foreach (RepeaterItem item in rptEmployee.Items)
        //{
        //    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        //    {
                
        //        HiddenField hdnEmployeeID = item.FindControl("hdnEmployeeID") as HiddenField;
        //        CheckBox cbEmployee = item.FindControl("cbEmployee") as CheckBox;
        //        if (cbEmployee.Checked == true)
        //        {
        //            ec.ApproveEmployee(1, Convert.ToInt32(hdnEmployeeID.Value));
        //        }
        //        else
        //        {
        //            ec.ApproveEmployee(0, Convert.ToInt32(hdnEmployeeID.Value));
        //        }
        //    }
        //}
        //ResetForm();
    }

    protected void rptEmployee_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        HiddenField hfEmployeeContractTypeID = (HiddenField)e.Item.FindControl("hfEmployeeContractTypeID");
        DateTime StartDate = Convert.ToDateTime(txtMonth.Text);
        DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
        CalculateOverTime(StartDate, EndDate,Convert.ToInt32(ddlLocation.SelectedValue),Convert.ToInt32(ddlDepartment.SelectedValue),Convert.ToInt32(e.CommandArgument));
        DataSet dsDetail = ppc.GetPayRollForPayPeriodDetail(Convert.ToInt32(Session["WorkingCompanyID"]), Convert.ToInt64(e.CommandArgument), Constants.IntNullValue, Constants.IntNullValue, StartDate, EndDate, Constants.IntNullValue);

        rDeduction.DataSource = dsDetail.Tables[1];
        rDeduction.DataBind();

        rAllwonce.DataSource = dsDetail.Tables[2];
        rAllwonce.DataBind();

        if (dsDetail.Tables[0].Rows.Count > 0)
        {
            lblBasic1.Text = dsDetail.Tables[0].Rows[0]["BasicPay"].ToString();
            lblEmployee.Text = dsDetail.Tables[0].Rows[0]["EmployeeFullName"].ToString();
            lblAllownceTotal1.Text = Allownce.ToString();
            lblDeductionTotal1.Text = Deduction.ToString();
            lblNet1.Text = (Convert.ToDecimal(dc.chkNull_0(lblBasic1.Text)) + Allownce - Deduction).ToString();
        }

        ModalPopupExtender.Show();
    }

    protected void rAllwonce_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblAllownceAmount = e.Item.FindControl("lblAllownceAmount") as Label;
        Allownce += Convert.ToDecimal(dc.chkNull_0(lblAllownceAmount.Text));        
    }

    protected void rDeduction_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblDeductionAmount = e.Item.FindControl("lblDeductionAmount") as Label;
        Deduction += Convert.ToDecimal(dc.chkNull_0(lblDeductionAmount.Text));
    }

    protected void txtMonth_TextChanged(object sender, EventArgs e)
    {
        Session.Add("PageLoad", "1");
        DateTime StartDate = Convert.ToDateTime(txtMonth.Text);
        DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
        lblCompanyWorkingDays.Text = GetWorkingDays().ToString();
        fillrptEmployee(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), StartDate, EndDate, Constants.IntNullValue);
        Session.Add("PageLoad", "0");
    }

    private void CalculateOverTime(DateTime dtFrom, DateTime dtTo,int intLocationID, int intDepartmentID, int intEmployeeID)
    {
        CreateTable();
        AttendanceController AC = new AttendanceController();
        DataTable dtEmployeeAttendance = AC.GetEmployeeAttendance(intLocationID, intDepartmentID, intEmployeeID, dtFrom, Convert.ToDateTime(dtTo.ToShortDateString() + " 23:59:59"), Convert.ToInt32(Session["UserID"]));

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