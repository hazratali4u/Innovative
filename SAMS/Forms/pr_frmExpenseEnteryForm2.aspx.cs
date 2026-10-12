using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;

public partial class pr_frmExpenseEnteryForm2 : System.Web.UI.Page
{
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    PayRollExpenseController pec = new PayRollExpenseController();
    CompanyConfigrationController ccc = new CompanyConfigrationController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetForm();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            txtDate.Text = System.DateTime.Now.ToString("MMMM-yyyy");
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
        fillddlLocation();
        fillddlDepartment();
        fillddlEmployee();
        fillddlExpenseHead();
        fillrptExpense();
        txtAmount.Text = "";
        txtDate.Text = "";
        txtRemarks.Text = "";
        hdnExpenseID.Value = "";
        btnSave.ImageUrl = "~/images/btn-save.png";
        txtDate.Attributes.Add("readonly", "readonly");
    }
    
    protected void fillddlEmployee()
    {
        int DepartmentID = Constants.IntNullValue;
        int LocationID = Constants.IntNullValue;
        if (ddlDepartment.SelectedIndex != 0)
        {

            DepartmentID = Int32.Parse(ddlDepartment.SelectedValue);
        }
        if (ddlLocation.SelectedIndex != 0)
        {
            LocationID = Int32.Parse(ddlLocation.SelectedValue);
        }
        DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, DepartmentID, LocationID, false,1, Convert.ToInt32(Session["UserID"]));
        ddlEmployee.Items.Clear();
        this.ddlEmployee.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlEmployee, dt, 0, 1, false);
    }
    
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlEmployee();
       
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
        DataTable dtHead = pec.SelectExpenseHead(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlExpenseHead, dtHead, "ExpenseHeadID", "ExpenseHead_Name");
    }
    
    private void fillrptExpense()
    {
        rptExpense.DataSource = null;
        rptExpense.DataBind();
        if (ddlEmployee.SelectedIndex != 0 && txtDate.Text.Length > 0)
        {
            DataTable dt = pec.SelectExpense(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlEmployee.SelectedValue), DateTime.Parse(txtDate.Text).Date, Int32.Parse(Session["WorkingCompanyID"].ToString()),Convert.ToInt32(Session["UserID"]));
            rptExpense.DataSource = dt;
            rptExpense.DataBind();
        }
    }

    protected void fillddlDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add("--All--");
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);
    }

    protected void rptExpense_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = pec.SelectExpense(Int32.Parse(e.CommandArgument.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.DateNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()),Convert.ToInt32(Session["UserID"]));
        txtAmount.Text = dt.Rows[0]["Amount"].ToString();
        txtDate.Text = Convert.ToDateTime(dt.Rows[0]["Month"]).ToString("MMM-yyyy");
        txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
        ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeID"].ToString();
        ddlExpenseHead.SelectedValue = dt.Rows[0]["ExpenseHeadID"].ToString();
        ddlLocation.SelectedValue = dt.Rows[0]["EmployeeLocationID"].ToString();
        hdnExpenseID.Value = dt.Rows[0]["ExpenseID"].ToString();
        btnSave.ImageUrl = "~/images/btn-update.png"; 
          
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillrptExpense();
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        fillrptExpense();
    }

    protected void ddlDepartment_Change(object sender, EventArgs e)
    {
        fillddlEmployee();
    }
}