using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports.PayrollReports;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;

public partial class pr_frmLoanType : System.Web.UI.Page
{
    Loan_LeaseController llc = new Loan_LeaseController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetLoanType();
        }
    }

    #region LoanType Tab
    protected void fillrptLoanType()
    {
        DataTable dt = llc.SelectLoanType(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        rptLoanType.DataSource = dt;
        rptLoanType.DataBind();
    
    }
    protected void InsertUpdateLoanType()
    {
        try
        {
            if (hdnLoanTypeID.Value == "" || hdnLoanTypeID.Value == null)
            {
                LoanModel lm = new LoanModel();
                lm.LoanTypeName = txtLoanTypeName.Text;
                lm.CompanyID = Int32.Parse(Session["WorkingCompanyID"].ToString());
                lm.MaxAllow = Constants.IntNullValue;
                lm.User_ID = Int32.Parse(Session["UserID"].ToString());
                llc.InsertLoanType(lm);                
                
            }
            else
            {
                LoanModel lm = new LoanModel();
                lm.LoanTypeID = Convert.ToInt32(hdnLoanTypeID.Value);
                lm.LoanTypeName = txtLoanTypeName.Text;                
                lm.MaxAllow = Constants.IntNullValue;
                lm.User_ID = Int32.Parse(Session["UserID"].ToString());
                lm.IS_DELETED = false;
                lm.IS_ACTIVE = true;
                llc.UpdateLoanType(lm);  
            }
            ResetLoanType();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptLoanType_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = llc.SelectLoanType(Int32.Parse(Session["WorkingCompanyID"].ToString()), Convert.ToInt32(e.CommandArgument));
            txtLoanTypeName.Text = dt.Rows[0]["LoanTypeName"].ToString();
            hdnLoanTypeID.Value = dt.Rows[0]["LoanTypeID"].ToString();
            imgLoanType.ImageUrl = "../images/btn-update.png";
            pnlLoanTypeContent.Visible = true;
            pnlLoanTypeGrid.Visible = false;
        }
        else if (e.CommandName == "delete")
        {
            LoanModel lm = new LoanModel();
            lm.LoanTypeID = Convert.ToInt32(e.CommandArgument);
            lm.LoanTypeName = null;
            lm.MaxAllow = Constants.IntNullValue;
            lm.User_ID = Int32.Parse(Session["UserID"].ToString());
            lm.IS_DELETED = true;
            lm.IS_ACTIVE = true;
            if (llc.UpdateLoanType(lm))
            {
                ResetLoanType();
            }
        }
    }
    protected void ResetLoanType()
    {
        hdnLoanTypeID.Value = "";
        txtLoanTypeName.Text = "";
        fillrptLoanType();
        pnlLoanTypeContent.Visible = false;
        pnlLoanTypeGrid.Visible = true;
        imgLoanType.ImageUrl = "../images/btn-save.png";
    }
    protected void btnSaveLoanType_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateLoanType();
        }
        catch
        {
        }
    }
    protected void btnDiscardLoanType_Click(object sender, EventArgs e)
    {
        try
        {
            ResetLoanType();
        }
        catch
        {
        }
    }
    protected void btnShowpnlLoanTypeContent_Click(object sender, EventArgs e)
    {
        pnlLoanTypeContent.Visible = true;
        pnlLoanTypeGrid.Visible = false;
    }

    #endregion
}
