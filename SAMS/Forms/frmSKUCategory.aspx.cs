using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
/// <summary>
/// From To Add, Edit, Delete SKU Hierarchy
/// </summary>
public partial class frmSKUCategory : System.Web.UI.Page
{
    SkuHierarchyController mController = new SkuHierarchyController();
    SkuController mSkuController = new SkuController();
    static int CategoryId; 
    static int SubCategoryId;
    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadCategroy();
            Populate_drpSKUCategory();
            LoadSubCategroy_ProductDefination();
            LoadData();
            LoadGrid();
            filldllUOMS();
        }
    }
    #region Main Type
    /// <summary>
    /// Loads Categories To Category Grid On Category Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCategoryPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCategroy();
    }
    private void LoadCategroy()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlType.SelectedValue), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        GrdCategory.DataSource = dt;
        GrdCategory.Columns[0].Visible = true;
        GrdCategory.DataBind();
        GrdCategory.Columns[0].Visible = false;
    }
    /// <summary>
    /// Save Or Updates a Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {        
        if (btnSaveCategory.Text == "New")
        {
            txtCategoryCode.Text = this.GetAutoCode("CA", 0);
            txtCategoryName.Enabled = true;
            txtCategoryName.Focus();
            btnSaveCategory.Text = "Save";
        }
        else if (btnSaveCategory.Text == "Save")
        {
            if (txtCategoryName.Text.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter Category');", true);
                return;
            }
            mController.InsertHierarchy(Constants.SKUCategory, Convert.ToInt32(ddlType.SelectedValue), txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("CA", long.Parse(txtCategoryCode.Text.Substring(2)));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            this.LoadCategroy();
        }
        else if (btnSaveCategory.Text == "Update")
        {
            if (txtCategoryName.Text.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter Category');", true);
                return;
            }
            mController.UpdateHierarchy(Constants.SKUCategory, CategoryId, Convert.ToInt32(ddlType.SelectedValue), txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            this.LoadCategroy();
        }
    }
    /// <summary>
    /// Gets Code For New Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CodeType">Type</param>
    /// <returns>Code As String</returns>
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCategroy();
    }
    /// <summary>
    /// Sets Category Data For Edit. This Function Runs When An Existing Category Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CategoryId = int.Parse(GrdCategory.Rows[e.NewEditIndex].Cells[0].Text);
        txtCategoryCode.Text = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
        txtCategoryName.Text = GrdCategory.Rows[e.NewEditIndex].Cells[2].Text;
        txtCategoryName.Enabled = true;
        btnSaveCategory.Text = "Update";
    }
    /// <summary>
    /// Deletes A Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CategoryId = int.Parse(GrdCategory.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUBrand, CategoryId);
        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Wrong Command: first delete associated Brand');", true);            
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUCategory, CategoryId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadCategroy();
        }
    }
    #endregion
    #region Common 
    private string GetAutoCode(string PreeFix, int CodeType)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(PreeFix, CodeType, Constants.LongNullValue);
    }
    /// <summary>
    /// Sets Code For Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CValue">Value</param>
    private void SetAutoCode(string PreeFix, long CValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        string result = AutoCode.GetAutoCode(PreeFix, 1, CValue);
    }
    protected void tbUserAssignment_ActiveTabChanged(object sender, EventArgs e)
    {
        if (tbUserAssignment.ActiveTab.TabIndex == 1)
        {
            fillddlMainType();
            LoadSubCategroy();
        }
    }
    #endregion
    #region Sub Type
    /// <summary>
    /// Sets Category Data For Edit. This Function Runs When An Existing Category Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    /// 
    protected void grdSubCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        SubCategoryId = int.Parse(grdSubCategory.Rows[e.NewEditIndex].Cells[0].Text);
        txtSubCategoryCode.Text = grdSubCategory.Rows[e.NewEditIndex].Cells[1].Text;
        txtSubCategoryName.Text = grdSubCategory.Rows[e.NewEditIndex].Cells[2].Text;
        txtSubCategoryName.Enabled = true;
        btnSaveSubCategory.Text = "Update"; 
    }
    /// <summary>
    /// Deletes A Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void grdSubCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SubCategoryId = int.Parse(grdSubCategory.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUBrand, SubCategoryId);
        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Wrong Command: first delete associated Brand');", true);
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUSubCategory, SubCategoryId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadSubCategroy();
        }
    }
    protected void ddlCategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlMainType();
        LoadSubCategroy();
    }
    private void fillddlMainType()
    {

        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlCategoryType.SelectedValue), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlMainType, dt, "SKU_HIE_ID", "SKU_HIE_NAME", true);
    }
    protected void btnSaveSubCategory_Click(object sender, EventArgs e)
    {
        if (btnSaveSubCategory.Text == "New")
        {
            txtSubCategoryCode.Text = this.GetAutoCode("SCA", 0);
            txtSubCategoryName.Enabled = true;
            txtSubCategoryName.Focus();
            btnSaveSubCategory.Text = "Save";
        }
        else if (btnSaveSubCategory.Text == "Save")
        {
            if (txtSubCategoryName.Text.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter Category');", true);
                return;
            }
            mController.InsertHierarchy(Constants.SKUSubCategory, Convert.ToInt32(ddlMainType.SelectedValue), txtSubCategoryCode.Text, txtSubCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("SCA", long.Parse(txtSubCategoryCode.Text.Substring(3)));
            btnSaveSubCategory.Text = "New";
            txtSubCategoryCode.Text = "";
            txtSubCategoryName.Text = "";
            txtSubCategoryName.Enabled = false;
            this.LoadSubCategroy();
        }
        else if (btnSaveSubCategory.Text == "Update")
        {
            if (txtSubCategoryName.Text.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter Category');", true);
                return;
            }
            mController.UpdateHierarchy(Constants.SKUSubCategory, SubCategoryId, Convert.ToInt32(ddlMainType.SelectedValue), txtSubCategoryCode.Text, txtSubCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveSubCategory.Text = "New";
            txtSubCategoryCode.Text = "";
            txtSubCategoryName.Text = "";
            txtSubCategoryName.Enabled = false;
            this.LoadSubCategroy();
        }
    }
    private void LoadSubCategroy()
    {
        
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUSubCategory, Constants.IntNullValue, Convert.ToInt32(ddlMainType.SelectedValue), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        grdSubCategory.DataSource = dt;
        grdSubCategory.Columns[0].Visible = true;
        grdSubCategory.DataBind();
        grdSubCategory.Columns[0].Visible = false;
    }
    protected void ddlMainType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSubCategroy();
    }
    #endregion
    #region Product Defination
    SkuController mSKUController = new SkuController();
    DataControl dc = new DataControl();
    SkuHierarchyController mHer_Controller = new SkuHierarchyController();
    SLASHCodesController CodesCtl = new SLASHCodesController();
    private DataTable m_dt, m_SKUDt;
    private static int m_sku_id;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>

    /// <summary>
    /// Loads Divisions To Division Combo, Categories To Category Combo, Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskuPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {

        Populate_drpSKUCategory();
        this.LoadData();
        this.LoadGrid();
    }
    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Convert.ToInt32(ddlType_ProductDefination.SelectedValue), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddskucategory, m_dt, 0, 3, true);
    }

    /// <summary>
    /// Loads Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskucategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSubCategroy_ProductDefination();
        this.LoadData();
        this.LoadGrid();

    }
    protected void ddlProductSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadData();
        this.LoadGrid();
    }
    /// <summary>
    /// Loads SKU Data To Session
    /// </summary>
    private void LoadData()
    {
        if (ddlProductSubCategory.Items.Count > 0)
        {
            SkuController mSKUController = new SkuController();
            m_SKUDt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddlProductSubCategory.SelectedValue.ToString()), Convert.ToInt32(ddlType_ProductDefination.SelectedValue), int.Parse(this.Session["CompanyId"].ToString()));
            this.Session.Add("m_SKUDt", m_SKUDt);
        }
        else
        {
            m_SKUDt = Session["m_SKUDt"] as DataTable;
            if (m_SKUDt != null)
            {
                m_SKUDt.Rows.Clear();
            }
            this.Session.Add("m_SKUDt", m_SKUDt);
        }
    }
    /// <summary>
    /// Loads Data From Session To Grid
    /// </summary>
    private void LoadGrid()
    {
        m_SKUDt = (DataTable)this.Session["m_SKUDt"];
        if (m_SKUDt != null)
        {
            grdSKUData.DataSource = m_SKUDt.DefaultView;
            grdSKUData.DataBind();
        }
    }
    /// <summary>
    /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void grdSKUData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ddlProductSubCategory.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[0].Text;
        m_sku_id = int.Parse(grdSKUData.Rows[e.NewEditIndex].Cells[1].Text);
        txtskucode.Text = grdSKUData.Rows[e.NewEditIndex].Cells[2].Text;
        txtskuname.Text = grdSKUData.Rows[e.NewEditIndex].Cells[3].Text;
        ddlTax.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[4].Text.Trim();
        txtMinStock.Text = dc.chkNull_0(grdSKUData.Rows[e.NewEditIndex].Cells[5].Text);
        txtPackSize.Text = grdSKUData.Rows[e.NewEditIndex].Cells[6].Text;
        txtunitincase.Text = grdSKUData.Rows[e.NewEditIndex].Cells[7].Text;
        ddlUOM.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[8].Text;
        btnSave_ProductDefination.Text = "Update";
    }
    protected void gvQuality_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ModalPopupExtender.Show();
        txtQualityDescription.Text = gvQuality.Rows[e.NewEditIndex].Cells[0].Text;
        txtQualityValue.Text = gvQuality.Rows[e.NewEditIndex].Cells[1].Text;
        hfSKUQualityID.Value = gvQuality.Rows[e.NewEditIndex].Cells[4].Text;
        btnSaveQuality.Text = "Update";
    }
    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void grdSKUData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bool IsExemted = true;
        if (grdSKUData.Rows[e.RowIndex].Cells[4].Text != "E")
        {
            IsExemted = false;
        }

        string result = mSKUController.UpdateSKUS(IsExemted, false, Constants.CharNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
        Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.ShortNullValue, int.Parse(grdSKUData.Rows[e.RowIndex].Cells[1].Text), null, null, null, null, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), Constants.IntNullValue, Constants.IntNullValue);
        this.LoadData();
        this.LoadGrid();
    }
    protected void gvQuality_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (mSKUController.UpdateSKUQuality(Convert.ToInt64(gvQuality.Rows[e.RowIndex].Cells[4].Text), Constants.IntNullValue, null, null, false))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Product Quality deleted successfully.');", true);
            DataTable dtQuality = mSKUController.GetSKUQuality(Convert.ToInt32(hfSKUID.Value), Constants.LongNullValue);
            if (dtQuality.Rows.Count > 0)
            {
                gvQuality.DataSource = dtQuality;
                gvQuality.DataBind();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Some error occured.');", true);
        }
        ModalPopupExtender.Show();
    }
    /// <summary>
    /// Sets PageIndex Of SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void grdSKUData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSKUData.PageIndex = e.NewPageIndex;
        this.LoadGrid();

    }
    /// <summary>
    /// Save Or Updates an SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_ProductDefination_Click(object sender, EventArgs e)
    {
        bool IsExemted = true;
        if (ddlTax.SelectedValue != "E")
        {
            IsExemted = false;
        }

        if (txtskucode.Text.Length <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter SKU Code');", true);
            return;
        }
        if (txtskuname.Text.Length <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter SKU Name');", true);
            return;
        }

        //Used BrandID for UOM
        if (btnSave_ProductDefination.Text == "Save")
        {
            mSKUController.InsertSKUS(IsExemted, true, char.Parse(ddlTax.SelectedValue),1, 1, int.Parse(ddlProductSubCategory.SelectedValue.ToString()), Convert.ToInt32(ddlType_ProductDefination.SelectedValue), 1, 0, 0, short.Parse(dc.chkNull_0(txtunitincase.Text)),
            txtskucode.Text.ToUpper(), txtskuname.Text, null, txtPackSize.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(dc.chkNull_0(txtMinStock.Text)),Convert.ToInt32(ddlUOM.SelectedValue));
            this.CLearAll();
        }
        else if (btnSave_ProductDefination.Text == "Update")
        {
            mSKUController.UpdateSKUS(IsExemted, true, char.Parse(ddlTax.SelectedValue), 1, 1,
            int.Parse(ddlProductSubCategory.SelectedValue.ToString()), Convert.ToInt32(ddlType_ProductDefination.SelectedValue), 1, 0, 0, short.Parse(dc.chkNull_0(txtunitincase.Text)), m_sku_id, txtskucode.Text.ToUpper(), txtskuname.Text, null, txtPackSize.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(dc.chkNull_0(txtMinStock.Text)), Convert.ToInt32(ddlUOM.SelectedValue));
            this.CLearAll();
        }
        this.LoadData();
    }
    protected void btnSaveQuality_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        if (txtQualityDescription.Text.Trim().Length > 0)
        {
            if (btnSaveQuality.Text == "Save")
            {
                if (mSKUController.InsertSKUQuality(Convert.ToInt32(hfSKUID.Value), txtQualityDescription.Text, txtQualityValue.Text))
                {
                    txtQualityDescription.Text = string.Empty;
                    txtQualityValue.Text = string.Empty;
                    DataTable dtQuality = mSKUController.GetSKUQuality(Convert.ToInt32(hfSKUID.Value), Constants.LongNullValue);
                    if (dtQuality.Rows.Count > 0)
                    {
                        gvQuality.DataSource = dtQuality;
                        gvQuality.DataBind();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Product Quality added successfully.');", true);
                }
            }
            else
            {
                if (mSKUController.UpdateSKUQuality(Convert.ToInt64(hfSKUQualityID.Value), Constants.IntNullValue, txtQualityDescription.Text, txtQualityValue.Text, true))
                {
                    txtQualityDescription.Text = string.Empty;
                    txtQualityValue.Text = string.Empty;
                    btnSaveQuality.Text = "Save";
                    DataTable dtQuality = mSKUController.GetSKUQuality(Convert.ToInt32(hfSKUID.Value), Constants.LongNullValue);
                    if (dtQuality.Rows.Count > 0)
                    {
                        gvQuality.DataSource = dtQuality;
                        gvQuality.DataBind();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Product Quality update successfully.');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter Quality Description');", true);
        }
    }
    /// <summary>
    /// Filters SKU From SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGrid();
    }
    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void CLearAll()
    {
        txtskucode.Text = "";
        txtunitincase.Text = "";
        txtMinStock.Text = "";
        txtPackSize.Text = "";
        txtskuname.Text = "";
        btnSave_ProductDefination.Text = "Save";
        this.LoadData();
        this.LoadGrid();
    }
    protected void ddlType_ProductDefination_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Populate_drpSKUCategory();
        LoadSubCategroy_ProductDefination();
        this.LoadData();
        this.LoadGrid();
    }
    protected void grdSKUData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "detail")
        {
            gvQuality.DataSource = null;
            gvQuality.DataBind();

            txtQualityDescription.Text = string.Empty;
            txtQualityValue.Text = string.Empty;
            btnSaveQuality.Text = "Save";

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            lblSKUName.Text = row.Cells[3].Text;

            hfSKUID.Value = e.CommandArgument.ToString();
            DataTable dtQuality = mSKUController.GetSKUQuality(Convert.ToInt32(hfSKUID.Value), Constants.LongNullValue);
            if (dtQuality.Rows.Count > 0)
            {
                gvQuality.DataSource = dtQuality;
                gvQuality.DataBind();
            }
            ModalPopupExtender.Show();
        }
    }
    private void LoadSubCategroy_ProductDefination()
    {
        if (ddskucategory.Items.Count > 0)
        {
            SkuHierarchyController mSKUController = new SkuHierarchyController();
            DataTable dt = mSKUController.SelectSkuHierarchy(Constants.SKUSubCategory, Constants.IntNullValue, Convert.ToInt32(ddskucategory.SelectedValue), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlProductSubCategory, dt, "SKU_HIE_ID", "SKU_HIE_NAME", true);
        }
    }
    private void filldllUOMS()
    {
      DataTable dt=  mSKUController.SelectUOMs(Constants.IntNullValue, null);
      clsWebFormUtil.FillDropDownList(ddlUOM, dt, "UOM_ID", "UOM_DESC", true);

    }
    #endregion
}
