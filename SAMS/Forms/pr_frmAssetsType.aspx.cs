using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports.PayrollReports;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;

public partial class pr_frmAssetsType : System.Web.UI.Page
{
    Loan_LeaseController llc = new Loan_LeaseController();
    AssetsController ac = new AssetsController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetAssetsType();
        }
    }

    #region AssetsType Tab
    protected void fillrptAssetsType()
    {
        DataTable dt = llc.SelectAssetType(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);        
        rptAssetsType.DataSource = dt;
        rptAssetsType.DataBind();
    
    }
    protected void InsertUpdateAssetsType()
    {
        try
        {
            if (hdnAssetsTypeID.Value == "" || hdnAssetsTypeID.Value == null)
            {
                AssetsTypeModel atm = new AssetsTypeModel();
                atm.AssetTypeName = txtAssetsTypeName.Text;
                atm.CompanyID = Int32.Parse(Session["WorkingCompanyID"].ToString());
                atm.User_ID = Int32.Parse(Session["UserID"].ToString());
                ac.InsertAssetsType(atm);                
                
            }
            else
            {
                AssetsTypeModel atm = new AssetsTypeModel();
                atm.AssetTypeID = Convert.ToInt32(hdnAssetsTypeID.Value);
                atm.AssetTypeName = txtAssetsTypeName.Text;                
                atm.User_ID = Int32.Parse(Session["UserID"].ToString());
                atm.IS_DELETED = false;
                atm.IS_ACTIVE = true;
                ac.UpdateAssetsType(atm);  
            }
            ResetAssetsType();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptAssetsType_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = llc.SelectAssetType(Int32.Parse(Session["WorkingCompanyID"].ToString()), Convert.ToInt32(e.CommandArgument));
            txtAssetsTypeName.Text = dt.Rows[0]["AssetTypeName"].ToString();
            hdnAssetsTypeID.Value = dt.Rows[0]["AssetTypeID"].ToString();
            imgAssetsType.ImageUrl = "../images/btn-update.png";
            pnlAssetsTypeContent.Visible = true;
            pnlAssetsTypeGrid.Visible = false;
        }
        else if (e.CommandName == "delete")
        {
            AssetsTypeModel atm = new AssetsTypeModel();
            atm.AssetTypeID = Convert.ToInt32(e.CommandArgument);
            atm.AssetTypeName = null;
            atm.User_ID = Int32.Parse(Session["UserID"].ToString());
            atm.IS_DELETED = true;
            atm.IS_ACTIVE = true;
            if (ac.UpdateAssetsType(atm))
            {
                ResetAssetsType();
            }
        }
    }
    protected void ResetAssetsType()
    {
        hdnAssetsTypeID.Value = "";
        txtAssetsTypeName.Text = "";
        fillrptAssetsType();
        pnlAssetsTypeContent.Visible = false;
        pnlAssetsTypeGrid.Visible = true;
        imgAssetsType.ImageUrl = "../images/btn-save.png";
    }
    protected void btnSaveAssetsType_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateAssetsType();
        }
        catch
        {
        }
    }
    protected void btnDiscardAssetsType_Click(object sender, EventArgs e)
    {
        try
        {
            ResetAssetsType();
        }
        catch
        {
        }
    }
    protected void btnShowpnlAssetsTypeContent_Click(object sender, EventArgs e)
    {
        pnlAssetsTypeContent.Visible = true;
        pnlAssetsTypeGrid.Visible = false;
    }

    #endregion
}
