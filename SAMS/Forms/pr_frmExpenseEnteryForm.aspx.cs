using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using MKB.TimePicker;
using System.Globalization;
using SAMSBusinessLayer.Models;
using System.Collections.Generic;

public partial class pr_frmExpenseEnteryForm : System.Web.UI.Page
{
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    PayRollExpenseController pec = new PayRollExpenseController();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetForm();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            PayRollExpenseModel model = fillExepenseModel();
            if (hdnExpenseID.Value == "" || hdnExpenseID.Value == null)
            {
                pec.InsertExpense(model);
                ResetForm();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Expense Saved');", true);
            }
            else
            {
                pec.UpdateExpense(model);
                ResetForm();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Expense Updated');", true);
            }
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('"+ex.Message+"');", true);
        }
    }
    private PayRollExpenseModel fillExepenseModel()
    {
        try
        {
            PayRollExpenseModel pem = new PayRollExpenseModel();
            pem.Amount = decimal.Parse(txtAmount.Text);
            pem.Document_Date = DateTime.Parse(Session["CurrentWorkDate"].ToString());
            pem.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
            pem.ExpenseHeadID = Int32.Parse(ddlExpenseHead.SelectedValue);
            pem.Month = DateTime.Parse(txtDate.Text).Date;
            pem.Remarks = txtRemarks.Text;
            pem.User_ID = Int32.Parse(Session["UserID"].ToString());
            if(hdnExpenseID.Value!=null && hdnExpenseID.Value!="")
            {
                pem.ExpenseID = Int32.Parse(hdnExpenseID.Value);
            }
            pem.IS_ACTIVE = true;
            pem.IS_DELETED = false;
            return pem;
        }
        catch (Exception exp)
        {
            throw exp;
        }

    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        ResetForm();
    }
    private void ResetForm()
    {
        fillddlEmployee(Constants.IntNullValue);
        fillddlLocation();
        fillddlExpenseHead();
        fillrptExpense();
        txtAmount.Text = "";
        txtDate.Text = "";
        txtRemarks.Text = "";
        hdnExpenseID.Value = "";
        btnSave.ImageUrl = "~/images/btn-save.png"; 
        txtDate.Attributes.Add("readonly", "readonly");
    }
    protected void fillddlEmployee(int LocationID)
    {
        DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, LocationID, false, Convert.ToInt32(Session["UserID"]));
        ddlEmployee.Items.Clear();
        this.ddlEmployee.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlEmployee, dt, 0, 1, false);
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlEmployee(Int32.Parse(ddlLocation.SelectedValue));
       
    }
    private void fillddlLocation()
    {
        DataTable dt = cc.SelectCompany_lOCATIONS(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]));
        ddlLocation.Items.Clear();
        this.ddlLocation.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);

    }
    private void fillddlExpenseHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlExpenseHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME");
    }
    private void fillrptExpense()
    {
        DataTable dt = pec.SelectExpense(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.DateNullValue,Int32.Parse(Session["WorkingCompanyID"].ToString()),Convert.ToInt32(Session["UserID"]));
        rptExpense.DataSource = dt;
        rptExpense.DataBind();
    }
    protected void rptExpense_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = pec.SelectExpense(Int32.Parse(e.CommandArgument.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.DateNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()),Convert.ToInt32(Session["UserID"]));
        txtAmount.Text = dt.Rows[0]["Amount"].ToString();
        txtDate.Text = dt.Rows[0]["Month"].ToString();
        txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
        ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeID"].ToString();
        ddlExpenseHead.SelectedValue = dt.Rows[0]["ExpenseHeadID"].ToString();
        ddlLocation.SelectedValue = dt.Rows[0]["EmployeeLocationID"].ToString();
        hdnExpenseID.Value = dt.Rows[0]["ExpenseID"].ToString();
        btnSave.ImageUrl = "~/images/btn-update.png"; 
          
    }
}