using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports.PayrollReports;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;

public partial class pr_frmExpenseHead : System.Web.UI.Page
{
    Loan_LeaseController llc = new Loan_LeaseController();
    AssetsController ac = new AssetsController();
    PayRollExpenseController pec = new PayRollExpenseController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetExpenseHead();
        }
    }

    #region Expense Head Tab
    protected void fillrptExpenseHead()
    {
        DataTable dt = pec.SelectExpenseHead(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);        
        rptExpenseHead.DataSource = dt;
        rptExpenseHead.DataBind();
    
    }
    protected void InsertUpdateExpenseHead()
    {
        try
        {
            if (hdnExpenseHeadID.Value == "" || hdnExpenseHeadID.Value == null)
            {
                ExpenseHeadModel ehm = new ExpenseHeadModel();
                ehm.ExpenseHead_Name = txtExpenseHeadName.Text;
                ehm.CompanyID = Int32.Parse(Session["WorkingCompanyID"].ToString());
                ehm.User_ID = Int32.Parse(Session["UserID"].ToString());
                ehm.Remarks = "";
                pec.InsertExpenseHead(ehm);                
            }
            else
            {
                ExpenseHeadModel ehm = new ExpenseHeadModel();
                ehm.ExpenseHeadID = Convert.ToInt32(hdnExpenseHeadID.Value);
                ehm.ExpenseHead_Name = txtExpenseHeadName.Text;
                ehm.Remarks = "";
                ehm.User_ID = Int32.Parse(Session["UserID"].ToString());
                ehm.IS_DELETED = false;
                ehm.IS_ACTIVE = true;
                pec.UpdateExpenseHead(ehm);
            }
            ResetExpenseHead();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptExpenseHead_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = pec.SelectExpenseHead(Int32.Parse(Session["WorkingCompanyID"].ToString()), Convert.ToInt32(e.CommandArgument));
            txtExpenseHeadName.Text = dt.Rows[0]["ExpenseHead_Name"].ToString();
            hdnExpenseHeadID.Value = dt.Rows[0]["ExpenseHeadID"].ToString();
            imgExpenseHead.ImageUrl = "../images/btn-update.png";
            pnlExpenseHeadContent.Visible = true;
            pnlExpenseHeadGrid.Visible = false;
        }
        else if (e.CommandName == "delete")
        {
            ExpenseHeadModel ehm = new ExpenseHeadModel();
            ehm.ExpenseHeadID = Convert.ToInt32(e.CommandArgument);
            ehm.ExpenseHead_Name = null;
            ehm.Remarks = null;
            ehm.User_ID = Int32.Parse(Session["UserID"].ToString());
            ehm.IS_DELETED = true;
            ehm.IS_ACTIVE = true;
            if (pec.UpdateExpenseHead(ehm))
            {
                ResetExpenseHead();
            }
        }
    }
    protected void ResetExpenseHead()
    {
        hdnExpenseHeadID.Value = "";
        txtExpenseHeadName.Text = "";
        fillrptExpenseHead();
        pnlExpenseHeadContent.Visible = false;
        pnlExpenseHeadGrid.Visible = true;
        imgExpenseHead.ImageUrl = "../images/btn-save.png";
    }
    protected void btnSaveExpenseHead_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateExpenseHead();
        }
        catch
        {
        }
    }
    protected void btnDiscardExpenseHead_Click(object sender, EventArgs e)
    {
        try
        {
            ResetExpenseHead();
        }
        catch
        {
        }
    }
    protected void btnShowpnlExpenseHeadContent_Click(object sender, EventArgs e)
    {
        pnlExpenseHeadContent.Visible = true;
        pnlExpenseHeadGrid.Visible = false;
    }

    #endregion
}
