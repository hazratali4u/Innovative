using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class pr_frmGasLunchDeduction : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    ProcessPayrollController ppc = new ProcessPayrollController();
    DataControl dc = new DataControl();
    AttendanceController AC = new AttendanceController();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            txtDate.Attributes.Add("readonly", "readonly");
            ResetForm();
        }
    }

    protected void btnSaveSalary_Click(object sender, EventArgs e)
    {
        bool flag = true;
        foreach (RepeaterItem item in rptEmployee.Items)
        {
            CheckBox cbEmployee = (CheckBox)item.FindControl("cbEmployee");            
            if (cbEmployee.Checked)
            {
                HiddenField hfGasLunchDedcutionID = (HiddenField)item.FindControl("hfGasLunchDedcutionID");
                HiddenField hdnEmployeeID = (HiddenField)item.FindControl("hdnEmployeeID");
                TextBox txtMinutes = (TextBox)item.FindControl("txtMinutes");

                decimal hours = 0;
                if (Convert.ToDecimal(txtHours.Text) > 0)
                {
                    hours = Convert.ToDecimal(txtHours.Text);
                }
                else
                {
                    hours = Convert.ToDecimal(txtMinutes.Text);
                }

                if (hfGasLunchDedcutionID.Value == "0")
                {
                    flag = AC.InsertGasLunchDeduction(Constants.LongNullValue, Convert.ToInt64(hdnEmployeeID.Value), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlType.SelectedValue), hours, Convert.ToInt32(Session["UserID"]));
                }
                else if (hfGasLunchDedcutionID.Value != "0")
                {
                    flag = AC.InsertGasLunchDeduction(Convert.ToInt64(hfGasLunchDedcutionID.Value), Convert.ToInt64(hdnEmployeeID.Value), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlType.SelectedValue), hours, Convert.ToInt32(Session["UserID"]));
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
        DepartmentID = Int32.Parse(ddlDepartment.SelectedValue);
        LocationID = Int32.Parse(ddlLocation.SelectedValue);
        fillrptEmployee(DepartmentID, LocationID, Convert.ToDateTime(txtDate.Text));
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
        txtHours.Text = "0.00";
        fillddlDepartment();
        fillddlLocation();
        fillrptEmployee(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToDateTime(txtDate.Text));
    }
    
    private void fillrptEmployee(int DepartmentID, int LocationID, DateTime  Date)
    {
        DataTable dt = new DataTable();
        dt = AC.GetGasLunchDeduction(Constants.LongNullValue, Convert.ToInt32(Session["WorkingCompanyID"]), DepartmentID, LocationID,Date, Constants.LongNullValue,Convert.ToInt32(ddlType.SelectedValue));
        rptEmployee.DataSource = dt;
        rptEmployee.DataBind();
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        ResetForm();
    }
}