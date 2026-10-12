using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Models;

/// <summary>
/// Form to Add, Edit Users
/// </summary>
public partial class pr_frmCreateTaxSlabs : System.Web.UI.Page
{
    TaxRuleController trc = new TaxRuleController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillgrdTaxSlab();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
        Response.Expires = 0;
        Response.Cache.SetNoStore();
    }
    private void resetForm()
    {
        fillgrdTaxSlab();
        hdnTaxRuleID.Value = "";
        txtAmount.Text = "";
        txtAmountFrom.Text = "";
        txtAmountTo.Text = "";
        txtPercentage.Text = "";
        btnSave.Text = "Save";
    }

    private void fillgrdTaxSlab()
    {
        DataTable dt = trc.SelectTaxRule(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        grdTaxSlab.DataSource = dt;
        grdTaxSlab.DataBind();
    }
    protected void grdTaxSlab_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewRow gvr = this.grdTaxSlab.Rows[e.NewEditIndex];
            hdnTaxRuleID.Value = gvr.Cells[0].Text;
            DataTable dt = trc.SelectTaxRule(Int32.Parse(Session["WorkingCompanyID"].ToString()), Int32.Parse(hdnTaxRuleID.Value));
            txtAmountFrom.Text = dt.Rows[0]["AmountFrom"].ToString();
            txtAmountTo.Text = dt.Rows[0]["AmountTo"].ToString();
            txtPercentage.Text = dt.Rows[0]["Percentage"].ToString();
            txtAmount.Text = dt.Rows[0]["Amount"].ToString();
            btnSave.Text = "Update";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnTaxRuleID.Value == null || hdnTaxRuleID.Value == "")
            {
                TaxRuleModel model = fillTaxRuleMode();
                int taxRuleID = trc.InsertTaxRule(model);
                model.TaxRuleID = taxRuleID;
                trc.InsertTaxRuleArchive(model);
                resetForm();
            }
            else
            {
                TaxRuleModel model = fillTaxRuleMode();
                trc.UpdateTaxRule(model);
                model.TaxRuleID = Int32.Parse(hdnTaxRuleID.Value);
                trc.InsertTaxRuleArchive(model);
                resetForm();
            }
        }
        catch (Exception exp)
        {
            
        }

    }
    private TaxRuleModel fillTaxRuleMode()
    {
        try
        {
            TaxRuleModel model = new TaxRuleModel();
            model.Amount = Decimal.Parse(txtAmount.Text);
            model.AmountFrom = Decimal.Parse(txtAmountFrom.Text);
            model.AmountTo = Decimal.Parse(txtAmountTo.Text);
            model.CompanyID = Int32.Parse(Session["WorkingCompanyID"].ToString());
            model.Document_Date = DateTime.Parse(Session["CurrentWorkDate"].ToString());
            model.IS_ACTIVE = true;
            model.IS_DELETED = false;
            model.Percentage = Decimal.Parse(txtPercentage.Text);
            if (hdnTaxRuleID.Value != null && hdnTaxRuleID.Value != "")
            {
                model.TaxRuleID = Int32.Parse(hdnTaxRuleID.Value);
            }
            model.User_ID = Int32.Parse(Session["UserID"].ToString());
            return model;
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        resetForm();
    }   
}
