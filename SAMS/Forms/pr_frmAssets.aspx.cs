using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Models;
using SAMSCommon.Classes;

public partial class pr_frmAssets : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    Loan_LeaseController llc = new Loan_LeaseController();
   
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
            AssetsModel model = fillAssetsModel();
            if (hdnAssetID.Value == "" || hdnAssetID.Value == null)
            {
                if (llc.InsertAssets(model))
                {
                    Response.Redirect("pr_frmAssets.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                }
            }
            else
            {
                if (llc.UpdateAssets(model))
                {
                    Response.Redirect("pr_frmAssets.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('"+ex.Message+"');", true);
        }
    }
    private AssetsModel fillAssetsModel()
    {
        try
        {
            AssetsModel model = new AssetsModel();
            if (hdnAssetID.Value != "")
            {
                model.AssetID = Convert.ToInt32(hdnAssetID.Value);
            }

            model.Amount = Convert.ToDecimal(txtAmount.Text);
            model.AssetTypeID = Convert.ToInt32(ddlAssetType.SelectedValue);
            model.ChassisNo = txtChassisNo.Text;
            model.Color = txtColor.Text;
            model.CompanyID = Int32.Parse(Session["WorkingCompanyID"].ToString());
            model.Document_Date = DateTime.Parse(Session["CurrentWorkDate"].ToString());
            model.EngineNo = txtEngineNo.Text;
            model.RegNo = txtReg.Text;
            model.Make = txtMake.Text;
            model.Model = txtModel.Text;
            model.Remarks = txtComments.Text;
            model.IS_ACTIVE = true;
            model.IS_DELETED = false;
            model.User_ID = Int32.Parse(Session["UserID"].ToString());
            return model;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        Response.Redirect("pr_frmAssets.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    private void fillddlAssetType()
    {
        DataTable dt = llc.SelectAssetType(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlAssetType.Items.Clear();
        this.ddlAssetType.Items.Add(new ListItem("--Select--","0"));
        clsWebFormUtil.FillDropDownList(ddlAssetType, dt, 0,1, false);
    }
    private void ResetForm()
    {
        pnlAssetsContent.Visible = false;
        pnlAssetsGrid.Visible = true;
        fillddlAssetType();
        fillrptAssets(Constants.IntNullValue);
        hdnAssetID.Value = string.Empty;
        txtAmount.Text = "";
        txtChassisNo.Text = "";
        txtColor.Text = "";
        txtComments.Text = "";
        txtEngineNo.Text = "";
        txtReg.Text = "";
        txtMake.Text = "";
        txtModel.Text = "";
        imgAssets.ImageUrl = "../images/btn-save.png";
    }
    private void fillrptAssets(int AssetTypeID)
    {
        DataTable dt = llc.SelectAsset(Int32.Parse(Session["WorkingCompanyID"].ToString()), AssetTypeID, Constants.IntNullValue);
        rptAssets.DataSource = dt;
        rptAssets.DataBind();
    }
    protected void btnShowpnAssetsContent_Click(object sender, EventArgs e)
    {
        pnlAssetsContent.Visible = true;
        pnlAssetsGrid.Visible = false;
    }
    protected void rptAssets_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = llc.SelectAsset(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(e.CommandArgument));
            ddlAssetType.SelectedValue = dt.Rows[0]["AssetTypeID"].ToString();
            txtMake.Text = dt.Rows[0]["Make"].ToString();
            txtReg.Text = dt.Rows[0]["RegNo"].ToString();
            txtAmount.Text = dt.Rows[0]["Amount"].ToString();
            txtModel.Text = dt.Rows[0]["Model"].ToString();
            txtColor.Text = dt.Rows[0]["Color"].ToString();
            txtEngineNo.Text = dt.Rows[0]["EngineNo"].ToString();
            txtChassisNo.Text = dt.Rows[0]["ChassisNo"].ToString();
            txtComments.Text = dt.Rows[0]["Remarks"].ToString();
            hdnAssetID.Value = dt.Rows[0]["AssetID"].ToString();
            imgAssets.ImageUrl = "../images/btn-update.png";
            pnlAssetsContent.Visible = true;
            pnlAssetsGrid.Visible = false;
        }
        else if (e.CommandName == "delete")
        {
            AssetsModel model = new AssetsModel();
            model.AssetID = Convert.ToInt32(e.CommandArgument);
            model.Amount = Constants.DecimalNullValue;
            model.AssetTypeID = Constants.IntNullValue;
            model.ChassisNo = null;
            model.Color = null;
            model.CompanyID = Constants.IntNullValue;
            model.Document_Date = Constants.DateNullValue;
            model.EngineNo = null;
            model.RegNo = null;
            model.Make = null;
            model.Model = null;
            model.Remarks = null;
            model.IS_ACTIVE = true;
            model.IS_DELETED = true;
            model.User_ID = Int32.Parse(Session["UserID"].ToString());

            if (llc.UpdateAssets(model))
            {
                Response.Redirect("pr_frmAssets.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
            }
        }
    }
}