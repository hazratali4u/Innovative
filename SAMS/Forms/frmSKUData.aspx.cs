using System;
using System.Data;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;

/// <summary>
/// From To Add, Edit, Delete SKU
/// </summary>
public partial class Forms_frmSKUData : System.Web.UI.Page
{
    SkuHierarchyController mController = new SkuHierarchyController();
      SkuController mSKUController = new SkuController();
          
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillddlMainType();
            fillddlSubType();
            fillddlProduct();
            LoadOpeningInventory();
        }
    }
    private void fillddlMainType()
    {
        if (ddlCategoryType.Items.Count > 0)
        {
            DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlCategoryType.SelectedValue), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlMainType, dt, "SKU_HIE_ID", "SKU_HIE_NAME", true);
        }
    }
    private void fillddlSubType()  
    {
        if (ddlMainType.Items.Count > 0)
        {
            DataTable dt = mController.SelectSkuHierarchy(Constants.SKUSubCategory, Constants.IntNullValue, Convert.ToInt32(ddlMainType.SelectedValue), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlSubType, dt, "SKU_HIE_ID", "SKU_HIE_NAME", true);
        }
    }
    private void fillddlProduct()
    {
        if (ddlSubType.Items.Count > 0)
        {
            DataTable m_SKUDt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddlSubType.SelectedValue.ToString()), Convert.ToInt32(ddlCategoryType.SelectedValue), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlSKuCde, m_SKUDt, "SKU_ID", "SKUDETAIL", true);
        }
    }
    private void LoadOpeningInventory()  
    {
        if (ddlSubType.Items.Count > 0)
        {
            DataTable dt = mSKUController.SelectOpeningInventory(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(ddlSubType.SelectedValue));
            GrdOpeningInventory.DataSource = dt;
            GrdOpeningInventory.Columns[0].Visible = true;
            GrdOpeningInventory.Columns[2].Visible = true;
            GrdOpeningInventory.DataBind();
            GrdOpeningInventory.Columns[0].Visible = false;
            GrdOpeningInventory.Columns[2].Visible = false;
        }
    }
    protected void ddlCategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlMainType();
        fillddlSubType();
        fillddlProduct();
        LoadOpeningInventory();
    }
    protected void ddlMainType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlSubType();
        fillddlProduct();
        LoadOpeningInventory();
    }
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
       fillddlProduct();
       LoadOpeningInventory();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Update")
        {
            if (ddlSKuCde.Items.Count > 0)
            {
                mSKUController.UpdateOPENING_INVENTORY((Convert.ToInt32(hdnOPENING_INVENTORY_ID.Value)),(Convert.ToInt32(ddlSKuCde.SelectedValue)), Convert.ToInt32(Session["CompanyId"]), Convert.ToDecimal(txtQuantity.Text),true);
                LoadOpeningInventory();
                ResetForm();
            }
        }
        else
        {
            if (ddlSKuCde.Items.Count > 0)
            {
                mSKUController.InsertOpening_Inventory(Convert.ToInt32(ddlSKuCde.SelectedValue), Convert.ToInt32(Session["CompanyId"]), Convert.ToDecimal(txtQuantity.Text));
                LoadOpeningInventory();
                ResetForm();
            }
        }
    }
    private void ResetForm()
    {
        ddlCategoryType.SelectedIndex = 0;
        if (ddlSKuCde.Items.Count > 0)
        {
            ddlSKuCde.SelectedIndex = 0;
        }
        txtQuantity.Text = String.Empty;
        btnSave.Text = "Add Sku";
    }
    protected void GrdOpeningInventory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int OPENING_INVENTORY_ID   = int.Parse(GrdOpeningInventory.Rows[e.NewEditIndex].Cells[0].Text);
        hdnOPENING_INVENTORY_ID.Value = OPENING_INVENTORY_ID.ToString();
        ddlSKuCde.SelectedValue = GrdOpeningInventory.Rows[e.NewEditIndex].Cells[2].Text;
        txtQuantity.Text = GrdOpeningInventory.Rows[e.NewEditIndex].Cells[3].Text;
        btnSave.Text = "Update";
    }
    /// <summary>
    /// Deletes A Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOpeningInventory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int OPENING_INVENTORY_ID = int.Parse(GrdOpeningInventory.Rows[e.RowIndex].Cells[0].Text);
        mSKUController.UpdateOPENING_INVENTORY(OPENING_INVENTORY_ID, Constants.IntNullValue,  Constants.IntNullValue,  Constants.DecimalNullValue, false);
        LoadOpeningInventory();
        ResetForm();
    }
}
