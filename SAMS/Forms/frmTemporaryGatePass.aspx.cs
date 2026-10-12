using System;
using System.Data;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;

/// <summary>
/// From To Add, Edit, Delete SKU
/// </summary>
public partial class Forms_frmTemporaryGatePass : System.Web.UI.Page
{
    SkuHierarchyController mController = new SkuHierarchyController();
    TGPController tgp = new TGPController();
    SkuController mSKUController = new SkuController();
    
    private static int TGP_DETAILID;
   
    private void CreateDataTable()
    {
        DataTable dt_TGP_Detail = new DataTable();

        dt_TGP_Detail.Columns.Add("SKU_ID", typeof(int));
        dt_TGP_Detail.Columns.Add("SKU_NAME", typeof(string));
        dt_TGP_Detail.Columns.Add("SKU_UOM", typeof(string));
        dt_TGP_Detail.Columns.Add("QUANTITY", typeof(decimal));
        dt_TGP_Detail.Columns.Add("REMARKS", typeof(string));

        Session.Add("dt_TGP_Detail", dt_TGP_Detail);
    }
    private void Disable()
    {
        btnSaveDocument.Visible = false;
        btnSave.Enabled = false;
        btnCancel.Visible = false;
        txtIssueDate.Enabled = false;
        txtGoodsOutThrough.Enabled = false;
        txtReturnDateTime.Enabled = false;
        txtTransportNo.Enabled = false;
        ddlSupplier.Enabled = false;
        ddlMainType.Enabled = false;
        ddlSubType.Enabled = false;
        ddlCategoryType.Enabled = false;
        ddlSKuCde.Enabled = false;
        GrdOpeningInventory.Enabled = false;
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtIssueDate.Attributes.Add("readonly","readonly");
            LoadPrincipal();
            fillddlMainType();
            fillddlSubType();
            fillddlProduct();
            LoadOpeningInventory();
            CreateDataTable();
            SetUOMS();

            if (Session["TGP_ID"] != null)
            {
                hf_TGPID.Value = Convert.ToString(Session["TGP_ID"]);

                DataTable dt = tgp.UspSelectGATEPASS(int.Parse(hf_TGPID.Value),1);
                ddlSupplier.SelectedValue = dt.Rows[0]["SUPPLIER_ID"].ToString();
                
                txtIssueDate.Text = Convert.ToDateTime(dt.Rows[0]["ISSUE_DATE"]).ToString("dd/MMM/yyyy");
                txtReturnDateTime.Text = Convert.ToDateTime(dt.Rows[0]["RETURN_DATE"]).ToString("dd/MM/yyyy, hh:mm tt");
                txtGoodsOutThrough.Text = dt.Rows[0]["GOODS_OUT_THROUGH"].ToString();
                txtTransportNo.Text = dt.Rows[0]["TRANSPORT_NO"].ToString();
                bool Status=Convert.ToBoolean(dt.Rows[0]["STATUS"]);
                if (Status)
                {
                    Disable();
                    
                }

                DataTable dt2 = tgp.UspSelectGATEPASS(int.Parse(hf_TGPID.Value), 2);

                DataTable dt_TGP_Detail = (DataTable)Session["dt_TGP_Detail"];

                dt_TGP_Detail = dt2;

                GrdOpeningInventory.DataSource = dt_TGP_Detail;
                GrdOpeningInventory.DataBind();

                Session.Add("dt_TGP_Detail", dt_TGP_Detail);
            }
            Session.Remove("TGP_ID");
        }
        btnSaveDocument.Attributes.Add("onclick", "return ValidateForm();");
    }

    #region Load 
    private void LoadPrincipal()
    {
        DataTable dt = mController.SelectPrincipal(Constants.SKUPrincipal, int.Parse(this.Session["CompanyId"].ToString()), true);
        clsWebFormUtil.FillDropDownList(ddlSupplier, dt, "SKU_HIE_ID", "SKU_HIE_NAME", true);
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
            Session.Add("SKUS", m_SKUDt);
        }
    }
    private void LoadOpeningInventory()
    {
        //if (ddlSubType.Items.Count > 0)
        //{
        //    DataTable dt = mSKUController.SelectOpeningInventory(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(ddlSubType.SelectedValue));
        //    GrdOpeningInventory.DataSource = dt;
        //    GrdOpeningInventory.Columns[0].Visible = true;
        //    GrdOpeningInventory.Columns[2].Visible = true;
        //    GrdOpeningInventory.DataBind();
        //    GrdOpeningInventory.Columns[0].Visible = false;
        //    GrdOpeningInventory.Columns[2].Visible = false;
        //}
    }

    private void SetUOMS()
    {
        if (ddlSKuCde.Items.Count > 0)
        {
            DataTable dt = Session["SKUS"] as DataTable;
            DataRow[] result = dt.Select("SKU_ID = " + Convert.ToInt32(ddlSKuCde.SelectedValue));
            txtUOM.Text = result[0]["UOM_DESC"].ToString();
        }
    }
    #endregion

    #region Sel/Index Change

    protected void ddlCategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlMainType();
        fillddlSubType();
        fillddlProduct();
        LoadOpeningInventory();
        ResetGridArea2();
    }
    protected void ddlMainType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlSubType();
        fillddlProduct();
        LoadOpeningInventory();
        ResetGridArea2();
    }
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlProduct();
        LoadOpeningInventory();
        ResetGridArea2();
    }
    protected void ddlSKuCde_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetUOMS();
    }
    #endregion

    #region Grid OPerations

    private void fillGrid()
    {
        DataTable dt_TGP_Detail = Session["dt_TGP_Detail"] as DataTable;
        GrdOpeningInventory.DataSource = dt_TGP_Detail;
        GrdOpeningInventory.DataBind();
        Session.Add("dt_TGP_Detail", dt_TGP_Detail);


    }
    protected void GrdOpeningInventory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (ddlSKuCde.Items.Count > 0)
        {
            DataTable dt_TGP_Detail = (DataTable)Session["dt_TGP_Detail"];
            TGP_DETAILID = e.NewEditIndex;

            ddlSKuCde.SelectedValue = GrdOpeningInventory.Rows[e.NewEditIndex].Cells[0].Text;
            txtUOM.Text = GrdOpeningInventory.Rows[e.NewEditIndex].Cells[2].Text;
            txtQuantity.Text = GrdOpeningInventory.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", "");
            txtRemarks.Text = GrdOpeningInventory.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");

            btnSave.Text = "Update";

            ddlSKuCde.Enabled = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz Fill Products in Dropdown!')", true);
        }
    }

    protected void GrdOpeningInventory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt_TGP_Detail = (DataTable)this.Session["dt_TGP_Detail"];
        dt_TGP_Detail.Rows.RemoveAt(e.RowIndex);

        this.Session.Add("dt_TGP_Detail", dt_TGP_Detail);

        GrdOpeningInventory.DataSource = dt_TGP_Detail;
        GrdOpeningInventory.DataBind();

    }

    #endregion

    #region Click Opearations
    private bool CheckDublicateSKU()
    {
        DataTable m_SKUDt = (DataTable)this.Session["SKUS"];
        DataTable dt_TGP_Detail = (DataTable)this.Session["dt_TGP_Detail"];
        DataRow[] foundRows = dt_TGP_Detail.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataControl DC = new DataControl();

        DataTable dt_TGP_Detail = Session["dt_TGP_Detail"] as DataTable;
        if (btnSave.Text == "Update")
        {
            if (ddlSKuCde.Items.Count > 0)
            {
                if (Convert.ToDecimal(DC.chkNull_0(txtQuantity.Text)) < 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Plz enter Quantity.');", true);
                    return;
                }
                DataRow dr = dt_TGP_Detail.Rows[TGP_DETAILID];

                dr["SKU_ID"] = ddlSKuCde.SelectedValue;
                dr["SKU_NAME"] = ddlSKuCde.SelectedItem.Text;
                dr["SKU_UOM"] = txtUOM.Text.ToString();
                dr["QUANTITY"] = Convert.ToDecimal(txtQuantity.Text);
                dr["REMARKS"] = txtRemarks.Text;
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz Select Product')", true);
            }
        }
        else
        {
            if (ddlSKuCde.Items.Count > 0)
            {
                if (CheckDublicateSKU())
                {
                    if (Convert.ToDecimal(DC.chkNull_0(txtQuantity.Text)) < 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Plz enter Quantity.');", true);
                        return;
                    }
                    DataRow dr = dt_TGP_Detail.NewRow();
                    dr["SKU_ID"] = ddlSKuCde.SelectedValue;
                    dr["SKU_NAME"] = ddlSKuCde.SelectedItem.Text;
                    dr["SKU_UOM"] = txtUOM.Text.ToString();
                    dr["QUANTITY"] = Convert.ToDecimal(txtQuantity.Text);
                    dr["REMARKS"] = txtRemarks.Text;
                    dt_TGP_Detail.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Product Already Exists')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz Select Product')", true);
            }
        }
        Session.Add("dt_TGP_Detail", dt_TGP_Detail);
        fillGrid();
        ResetGridArea();
        txtReturnDateTime.Text = txtReturnDateTime.Text;
       
    }
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
       
        try
        {
            DataTable dt_TGP_Detail = Session["dt_TGP_Detail"] as DataTable;

            if (dt_TGP_Detail.Rows.Count > 0)
            {
                if (hf_TGPID.Value == "")
                {
                    hf_TGPID.Value = Convert.ToString(Constants.IntNullValue);
                }

                DateTime issueDate = DateTime.ParseExact(txtIssueDate.Text, "dd/MMM/yyyy", null);

                DateTime returnDateTime = DateTime.ParseExact(txtReturnDateTime.Text, "dd/MM/yyyy, hh:mm tt", null);

                tgp.InsertTGP(Convert.ToInt32(Session["CompanyId"]), int.Parse(hf_TGPID.Value), Convert.ToInt32(ddlSupplier.SelectedValue), txtGoodsOutThrough.Text, txtTransportNo.Text, issueDate, Convert.ToInt32(Session["UserID"].ToString()), returnDateTime, dt_TGP_Detail, 1);
                LoadOpeningInventory();
                ResetForm();
                Response.Redirect("frmTemporaryGatePassView.aspx?LevelID=96&LevelType=3&TopID=30");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz enter Product Detail!')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Return Date format is not Correct')", true);
        }
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ResetForm();

        Response.Redirect("frmTemporaryGatePassView.aspx?LevelID=96&LevelType=3&TopID=30");
    }
    #endregion

    private void ResetForm()
    {
        ddlCategoryType.SelectedIndex = 0;
        if (ddlSKuCde.Items.Count > 0)
        {
            ddlSKuCde.SelectedIndex = 0;
        }
        if (ddlMainType.Items.Count > 0)
        {
            ddlMainType.SelectedIndex = 0;
        }
        if (ddlSubType.Items.Count > 0)
        {
            ddlSubType.SelectedIndex = 0;
        }
        if (ddlSupplier.Items.Count > 0)
        {
            ddlSupplier.SelectedIndex = 0;
        }
        //txtQuantity.Text = String.Empty;
        btnSave.Text = "Add";
        Session.Remove("dt_TGP_Detail");
        fillGrid();
        txtGoodsOutThrough.Text = "";
        txtIssueDate.Text = "";
        txtQuantity.Text = "";
        txtRemarks.Text = "";
        txtReturnDateTime.Text = "";
        txtTransportNo.Text = "";
        txtUOM.Text = "";
        Session.Remove("TGP_ID");
    }

    private void ResetGridArea()
    {
        ddlCategoryType.SelectedIndex = 0;
        if (ddlSKuCde.Items.Count > 0)
        {
            ddlSKuCde.SelectedIndex = 0;
        }
        txtQuantity.Text = "";
        txtRemarks.Text = "";
        btnSave.Text = "Add";
        ddlSKuCde.Enabled = true;
    }
    private void ResetGridArea2()
    {
      
        txtQuantity.Text = "";
        txtRemarks.Text = "";
        btnSave.Text = "Add";
        ddlSKuCde.Enabled = true;
    }
    
}
