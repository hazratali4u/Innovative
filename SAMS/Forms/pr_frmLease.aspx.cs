using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Models;
using SAMSCommon.Classes;

public partial class pr_frmLease : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    Loan_LeaseController llc = new Loan_LeaseController();
    DataControl dc = new DataControl();
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

        if (rptInstallment.Items.Count > 0)
        {
            decimal amount=(Convert.ToDecimal(txtEmployeeContribution.Text)+Convert.ToDecimal(txtCompanyContribution.Text))-(Convert.ToDecimal(txtAmount.Text));
            if (amount < 1)
            {
                try
                {
                    if (SaveLease() == 1)
                    {
                        ResetForm();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Lease Added Successfully!!');", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('sum of installment should be equal to total amount!!');", true);
                        return;
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('" + ex.Message + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Sum Of CompanyContribution And EmployeeContribution should be Equal to the Amount of Asset');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('First Calculate the Installments');", true);
        }
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        ResetForm();
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
        clsWebFormUtil.FillDropDownList(ddlEmployee, dt,0, 1, false);
    }
    private void fillddlLocation()
    {
        DataTable dt = cc.SelectCompany_lOCATIONS(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]));
        ddlLocation.Items.Clear();
        this.ddlLocation.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);

    }
    private void fillddlLeaseType()
    {
        DataTable dt = llc.SelectAssetType(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlLeaseType.Items.Clear();
        this.ddlLeaseType.Items.Add(new ListItem("--Select--",Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlLeaseType, dt, 0, 1, false);
    }
    private void fillddlAssets(int AssetTypeID)
    {
        DataTable dt = llc.SelectAsset(Int32.Parse(Session["WorkingCompanyID"].ToString()), AssetTypeID, Constants.IntNullValue);
        ddlAssets.Items.Clear();
        this.ddlAssets.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlAssets, dt, 0, 4, false);
    }
    private void fillmcbManagers()
    {
        DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, false, Convert.ToInt32(Session["UserID"]));
        mcbManagers.ClearAll();
        mcbManagers.AddItems(dt, 0, 1);
    }
    protected void fillddlDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add("--All--");
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);
    }
    private void ResetForm()
    {
        fillddlAssets(Constants.IntNullValue);
        fillddlLocation();
        fillddlDepartment();
        fillddlEmployee();        
        fillmcbManagers();
        fillddlLeaseType();
        txtApproveDate.Attributes.Add("readonly", "readyonly");
        txtDateFrom.Attributes.Add("readonly", "readonly");
        txtDateTo.Attributes.Add("readonly", "readonly");
        txtAmount.Attributes.Add("readonly", "readonly");
        rptInstallment.DataSource = null;
        rptInstallment.DataBind();
        txtAmount.Text = "";
        txtApproveDate.Text = "";
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        txtCompanyContribution.Text = "";
        txtEmployeeContribution.Text = "";
        fillrptLease(Constants.IntNullValue, 2);//TypeID = 2 (for Lease)
        hdnLeaseID.Value = string.Empty;
        if (Session["Add"] != null)
        {
            pnlLeaseContent.Visible = true;
            pnlLeaseGrid.Visible = false;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("LoanLeaseID");
        }
        else if (Session["Edit"] != null)
        {
            pnlLeaseContent.Visible = true;
            pnlLeaseGrid.Visible = false;

            DataTable dt = llc.SelectLoan_Lease(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(Session["LoanLeaseID"].ToString()), 2,Convert.ToInt32(Session["UserID"]));
            hdnLeaseID.Value = dt.Rows[0]["Loan_LeaseID"].ToString();
            ddlLocation.SelectedValue = dt.Rows[0]["EmployeeLocationID"].ToString();
            ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeID"].ToString();
            ddlLeaseType.SelectedValue = dt.Rows[0]["Loan_Lease_TypeID"].ToString();
            ddlAssets.SelectedValue = dt.Rows[0]["AssetID"].ToString();
            ddlAssets_SelectedIndexChanged(null, null);
            txtCompanyContribution.Text = String.Format("{0:0.00}", dt.Rows[0]["CompanyContributation"]);
            txtEmployeeContribution.Text = String.Format("{0:0.00}", dt.Rows[0]["EmployeeContributation"]);

            if (dt.Rows[0]["DateFrom"].ToString().Length > 0)
            {
                txtDateFrom.Text = Convert.ToDateTime(dt.Rows[0]["DateFrom"]).ToString("MMMM-yyyy");
            }
            if (dt.Rows[0]["DateTo"].ToString().Length > 0)
            {
                txtDateTo.Text = Convert.ToDateTime(dt.Rows[0]["DateTo"]).ToString("MMMM-yyyy");
            }
            mcbManagers.Text = dt.Rows[0]["ApprovedBy"].ToString();

            string[] mangertext = dt.Rows[0]["ApprovedBy"].ToString().Split(',');

            for (int i = 0; i < mangertext.Length; i++)
            {
                mcbManagers.selectItem(mangertext[i].Trim());
            }

            if (dt.Rows[0]["ApprovedDate"].ToString().Length > 0)
            {
                txtApproveDate.Text = Convert.ToDateTime(dt.Rows[0]["ApprovedDate"]).ToString("dd-MMM-yyyy");
            }
            imgLease.ImageUrl = "../images/btn-update.png";
            DataTable dtInstallment = llc.GetLoanLeaseInstallments(Convert.ToInt32(hdnLeaseID.Value));
            if (dtInstallment.Rows.Count > 0)
            {
                rptInstallment.DataSource = dtInstallment;
                rptInstallment.DataBind();
                btnCalculateInstallments.Visible = false;
            }
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("LoanLeaseID");
        }
        else
        {
            pnlLeaseContent.Visible = false;
            pnlLeaseGrid.Visible = true;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("LoanLeaseID");
        }
    }
    protected int SaveLease()
    {
        try
        {
            decimal amount = 0;
            foreach (RepeaterItem item in rptInstallment.Items)
            {
                TextBox txtamountTobePaid = (TextBox)item.FindControl("txtamountTobePaid");
                decimal currentamount = Convert.ToDecimal(txtamountTobePaid.Text);
                amount += currentamount;
            }
            amount =amount-Convert.ToDecimal(txtEmployeeContribution.Text);
            if (amount<1)
            {
                DataTable dt = CreatedtInstallment();
                foreach (RepeaterItem item in rptInstallment.Items)
                {
                    TextBox txtamountTobePaid = (TextBox)item.FindControl("txtamountTobePaid");
                    TextBox txtInstallmentDate = (TextBox)item.FindControl("txtInstallmentDate");
                    TextBox txtAmountPaid = (TextBox)item.FindControl("txtAmountPaid");
                    DataRow dr = dt.NewRow();
                    dr["AmountToBePaid"] = decimal.Parse(txtamountTobePaid.Text);
                    dr["InstallmentDate"] = DateTime.Parse(txtInstallmentDate.Text);
                    dr["AmountPaid"] = decimal.Parse(txtAmountPaid.Text);
                    dt.Rows.Add(dr);
                }
                List<ListItem> managers = mcbManagers.getSelectedItems();
                DataTable dtManagers = CreatedtManagers();
                foreach (ListItem manager in managers)
                {
                    DataRow dr = dtManagers.NewRow();
                    dr["ManagerID"] = manager.Value;
                    dr["LoanID"] = 0;
                    dtManagers.Rows.Add(dr);
                }
                Loan_LeaseModel llm = fillLoanModel();
                                
                if (hdnLeaseID.Value == "" || hdnLeaseID.Value == null)
                {
                    llc.InsertLoan_Lease(llm, dt, dtManagers);
                }
                else
                {
                    llc.UpdateLoan_Lease(llm, dt, dtManagers);
                }

                return 1;
            }
            else
            {
                return 0;
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private Loan_LeaseModel fillLoanModel()
    {
        Loan_LeaseModel llm = new Loan_LeaseModel();

        if (hdnLeaseID.Value != "")
        {
            llm.Loan_LeaseID = Convert.ToInt32(hdnLeaseID.Value);
        }

        llm.Amount = Convert.ToDecimal(txtAmount.Text);
        llm.CompanyContributation = decimal.Parse(txtCompanyContribution.Text);
        llm.EmployeeContributation = decimal.Parse(txtEmployeeContribution.Text);
        llm.ApprovedDate = Convert.ToDateTime(txtApproveDate.Text).Date;
        llm.ApprovelID = Constants.IntNullValue;
        llm.Comments = null;
        llm.CompanyID = Convert.ToInt32(Session["WorkingCompanyID"].ToString());
        llm.DateFrom = Convert.ToDateTime(txtDateFrom.Text).Date;
        llm.DateTo = Convert.ToDateTime(txtDateTo.Text).Date;
        llm.Document_Date = Convert.ToDateTime(Session["CurrentWorkDate"].ToString());
        llm.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
        llm.Loan_Lease_TypeID = Convert.ToInt32(ddlLeaseType.SelectedValue);
        llm.AssetID = Convert.ToInt32(ddlAssets.SelectedValue);
        int noofmonth = ((Convert.ToDateTime(txtDateTo.Text).Year - Convert.ToDateTime(txtDateFrom.Text).Year) * 12) + Convert.ToDateTime(txtDateTo.Text).Month - Convert.ToDateTime(txtDateFrom.Text).Month;
        llm.NoOfMonth = noofmonth;
        llm.TypeID = 2;
        llm.IS_DELETED = false;
        llm.IS_ACTIVE = true;
        llm.User_ID = Convert.ToInt32(Session["UserID"]);
        return llm;
    }
    protected void btnCalculateInstallments_Click(object sender, EventArgs e)
    {
        try
        {
            decimal balance=(Convert.ToDecimal(txtEmployeeContribution.Text)+Convert.ToDecimal(txtCompanyContribution.Text))-(Convert.ToDecimal(txtAmount.Text));
            if (balance < 1 &&  balance>-1)
            {
                DateTime dateFrom = Convert.ToDateTime(txtDateFrom.Text).Date;
                DateTime dateTo = Convert.ToDateTime(txtDateTo.Text).Date;
                if (dateTo >= dateFrom && decimal.Parse(txtAmount.Text) > 0)
                {
                    int year = dateFrom.Year;
                    int noofmonth = ((dateTo.Year - dateFrom.Year) * 12) + dateTo.Month - dateFrom.Month;
                    noofmonth = noofmonth + 1;
                    decimal amount = decimal.Parse(txtEmployeeContribution.Text) / noofmonth;
                    amount = Math.Round(amount, 2);
                    List<string> months = MonthsBetween(dateFrom, dateTo);
                    DataTable dt = CreatedtInstallment();
                    for (int i = 0; i < noofmonth; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AmountToBePaid"] = amount.ToString();
                        dr["AmountPaid"] = 0;
                        dr["InstallmentDate"] = months[i];
                        dt.Rows.Add(dr);
                    }
                    rptInstallment.DataSource = dt;
                    rptInstallment.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Wrong values please check values');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Sum Of CompanyContribution And EmployeeContribution should be Equal to the Amount of Asset');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Wrong values please check values');", true); 
        }
    }
    private DataTable CreatedtInstallment()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("InstallmentDate", typeof(DateTime));
        dt.Columns.Add("AmountToBePaid", typeof(decimal));
        dt.Columns.Add("AmountPaid", typeof(decimal));        
        return dt;
    }
    private DataTable CreatedtManagers()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ManagerID", typeof(int));
        dt.Columns.Add("LoanID", typeof(int));
        return dt;
    }
    public List<string> MonthsBetween(DateTime startDate, DateTime endDate)
    {
        List<string> lstDates = new List<string>();
        if (endDate > startDate)
        {
            while (startDate <= endDate)
            {
                lstDates.Add(startDate.ToShortDateString());
                startDate = startDate.AddMonths(1);
            }
        }
        else if (endDate == startDate)
        {
            lstDates.Add(startDate.ToShortDateString());
        }
        return lstDates;
    }
    protected void rptInstallment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TextBox txtInstallmentDate = (TextBox)e.Item.FindControl("txtInstallmentDate");
            TextBox txtamountTobePaid = (TextBox)e.Item.FindControl("txtamountTobePaid");
            TextBox txtAmountPaid = (TextBox)e.Item.FindControl("txtAmountPaid");
            if (Convert.ToDecimal(dc.chkNull_0(((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[2].ToString())) > 0)
            {
                txtInstallmentDate.Enabled = false;
                txtamountTobePaid.Enabled = false;
                txtAmountPaid.Enabled = false;
            }
        }
    }
    protected void ddlAssets_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAssets.SelectedIndex != 0)
        {
            DataTable dt = llc.SelectAsset(Int32.Parse(Session["WorkingCompanyID"].ToString()), Convert.ToInt32(ddlLeaseType.SelectedValue), Convert.ToInt32(ddlAssets.SelectedValue));
            txtAmount.Text = dt.Rows[0]["Amount"].ToString();
        }
        else
        {
            txtAmount.Text = "";
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlEmployee();
    }
    protected void ddlLeaseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlAssets(Int32.Parse(ddlLeaseType.SelectedValue));
        txtAmount.Text = "";
    }
    protected void btnShowpnLeaseContent_Click(object sender, EventArgs e)
    {
        Session.Add("Add", "Add");
        Response.Redirect("pr_frmLease.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    protected void rptLease_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Session.Add("Edit", "Edit");
            Session.Add("LoanLeaseID", e.CommandArgument.ToString());
            Response.Redirect("pr_frmLease.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
        }
        else if (e.CommandName == "delete")
        {
            Loan_LeaseModel llm = new Loan_LeaseModel();
            llm.Loan_LeaseID = Convert.ToInt32(e.CommandArgument);            
            llm.Amount = Constants.DecimalNullValue;
            llm.CompanyContributation = Constants.DecimalNullValue;
            llm.EmployeeContributation = Constants.DecimalNullValue;
            llm.ApprovedDate = Constants.DateNullValue;
            llm.ApprovelID = Constants.IntNullValue;
            llm.Comments = null;
            llm.CompanyID = Constants.IntNullValue;
            llm.DateFrom = Constants.DateNullValue;
            llm.DateTo = Constants.DateNullValue;
            llm.Document_Date = Constants.DateNullValue;
            llm.EmployeeID = Constants.IntNullValue;
            llm.Loan_Lease_TypeID = Constants.IntNullValue;
            llm.AssetID = Constants.IntNullValue;
            llm.NoOfMonth = Constants.IntNullValue;
            llm.TypeID = Constants.IntNullValue;
            llm.IS_DELETED = true;
            llm.IS_ACTIVE = false;
            llm.User_ID = Convert.ToInt32(Session["UserID"]);
            if (llc.UpdateLoan_Lease(llm))
            {
                fillrptLease(Constants.IntNullValue, 2);//TypeID = 2 (for Lease)
            }
        }
    }
    private void fillrptLease(int LoanLeaseTypeID, int TypeID)
    {
        DataTable dt = llc.SelectLoan_Lease(Int32.Parse(Session["WorkingCompanyID"].ToString()), LoanLeaseTypeID, Constants.IntNullValue, TypeID,Convert.ToInt32(Session["UserID"]));
        rptLease.DataSource = dt;
        rptLease.DataBind();
    }
    protected void ddlDepartment_Change(object sender, EventArgs e)
    {
        fillddlEmployee();
    }
}