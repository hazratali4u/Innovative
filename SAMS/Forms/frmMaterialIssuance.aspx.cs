using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmMaterialIssuance : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    PurchaseController PurchCtl = new PurchaseController();
    SkuHierarchyController mHer_Controller = new SkuHierarchyController();
    PhaysicalStockController mController = new PhaysicalStockController();
    SkuController SKUCtl = new SkuController();

    private static int RowId;
    private static int PrivouseQty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            this.LoadLocation();
            this.LoadCategory();
            this.LoadSKU();
            this.CreateSKUDataTable();
            this.LoadDocumentNo();
        }
    }

    /// <summary>
    /// Load Finished SKUs To SKU DropdownList
    /// </summary>
    private void LoadCategory()
    {
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlCategory, m_dt, 0, 3, true);
    }

    /// <summary>
    /// Load SKUs To SKU DropdownList
    /// </summary>
    private void LoadSKU()
    {        
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlCategory.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 3, Constants.DateNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlSKU, Dtsku_Price, 0, 10, true);
    }

    /// <summary>
    /// Sets Order SKU For Edit. This Function Runs When An Existing Order SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvSKU_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        ddlSKU.SelectedValue = gvSKU.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = gvSKU.Rows[e.NewEditIndex].Cells[2].Text;
        PrivouseQty = int.Parse(gvSKU.Rows[e.NewEditIndex].Cells[2].Text);        
        btnAdd.Text = "Update SKU";
    }

    /// <summary>
    /// Deletes An SKU From Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvSKU_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtSKU = (DataTable)this.Session["dtSKU"];
        dtSKU.Rows.RemoveAt(e.RowIndex);

        gvSKU.DataSource = dtSKU;
        gvSKU.DataBind();
    }

    /// <summary>
    /// Adds SKUs to Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dtSKU = (DataTable)this.Session["dtSKU"];
        if (txtQuantity.Text.Length > 0)
        {
            int CurrentStock = 0;
            if (ddlCategory.SelectedValue != Constants.FinishedGoods.ToString())
            {
                CurrentStock = CheckStockStatus(Convert.ToInt32(ddlSKU.SelectedValue));
            }
            else
            {
                //Check Raw Material for Finished Goods
                DataTable dtSKURaw = GetFinishedDetail();

                foreach (DataRow dr in dtSKURaw.Rows)
                {
                    CurrentStock = CheckStockStatus(Convert.ToInt32(dr["SKU_ID"]));
                    if (CurrentStock > -1)
                    {
                        break;
                    }
                }
            }
            
            if (btnAdd.Text == "Add SKU")
            {
                if (CheckDublicateSKU())
                {
                    if (CurrentStock == -1)
                    {
                        DataRow dr = dtSKU.NewRow();
                        dr["SKU_ID"] = Convert.ToInt32(ddlSKU.SelectedValue);
                        dr["QUANTITY"] = int.Parse(txtQuantity.Text);
                        dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;
                        dtSKU.Rows.Add(dr);
                        txtQuantity.Text = string.Empty;
                        ddlCategory.Enabled = false;
                        ddlLocation.Enabled = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('  " + ddlSKU.SelectedItem.Text + " or some of its raw materail Current closing Stock is " + CurrentStock.ToString() + "');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('SKU Already Exists')", true);
                }                
            }
            else
            {
                if (CurrentStock == -1)
                {
                    DataRow dr = dtSKU.Rows[RowId];
                    dr["SKU_ID"] = Convert.ToInt32(ddlSKU.SelectedValue);
                    dr["QUANTITY"] = int.Parse(txtQuantity.Text);
                    dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;

                    ddlCategory.Enabled = false;
                    ddlLocation.Enabled = false;
                    txtQuantity.Text = string.Empty;
                    btnAdd.Text = "Add SKU";
                    RowId = 0;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('  " + ddlSKU.SelectedItem.Text + " or some of its raw materail Current closing Stock is " + CurrentStock.ToString() + "');", true);
                    return;
                }
            }

            ScriptManager.GetCurrent(Page).SetFocus(ddlSKU);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Must Enter Quantity');", true);            
            return;
        }

        LoadGrid();
    }

    /// <summary>
    /// Creates Datatable For SKUs
    /// </summary>
    private void CreateSKUDataTable()
    {
        DataTable  dtSKU = new DataTable();
        dtSKU.Columns.Add("SKU_ID", typeof(int));
        dtSKU.Columns.Add("QUANTITY", typeof(int));
        dtSKU.Columns.Add("SKU_NAME",typeof(string));
        this.Session.Add("dtSKU", dtSKU);
    }

    /// <summary>
    /// Checks SKU in Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool CheckDublicateSKU()
    {
        DataTable dtSKU = (DataTable)this.Session["dtSKU"];
        DataRow[] foundRows = dtSKU.Select("SKU_ID  = '" + ddlSKU.SelectedValue + "'");
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
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        if (dtSKU.Rows.Count > 0)
        {
            if (ddlDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                if (PurchCtl.InsertMaterailIssuance(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(Session["UserID"]), dtSKU))
                {
                    CreateSKUDataTable();
                    LoadGrid();                   
                    this.LoadDocumentNo();
                    ddlCategory.Enabled = true;
                    ddlLocation.Enabled = true;
                    PrivouseQty = 0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Material Issuance added succesfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured')", true);
                }
            }
            else
            {
                if (PurchCtl.UpdateMaterailIssuance(Convert.ToInt64(ddlDocumentNo.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(Session["UserID"]), dtSKU))
                {
                    CreateSKUDataTable();
                    LoadGrid();
                    this.LoadDocumentNo();
                    ddlCategory.Enabled = true;
                    ddlLocation.Enabled = true;
                    PrivouseQty = 0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Material Issuance updated succesfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured')", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No SKU found.')", true);
        }
    }

    private void LoadGrid()
    {
        DataTable dtSKU = (DataTable)Session["dtSKU"];
        gvSKU.DataSource = dtSKU;
        gvSKU.DataBind();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKU();
    }

    protected void ddlDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            CreateSKUDataTable();
            LoadGrid();
            ddlCategory.Enabled = true;
            ddlLocation.Enabled = true;
        }
        else
        {
            DataTable dtIssuanceDetail = PurchCtl.GetMaterialIssuanceDetail(Convert.ToInt32(ddlDocumentNo.SelectedValue));
            if (dtIssuanceDetail.Rows.Count > 0)
            {
                ddlCategory.SelectedValue = dtIssuanceDetail.Rows[0]["CATEGORY_ID"].ToString();
                ddlLocation.SelectedValue = dtIssuanceDetail.Rows[0]["DISTRIBUTOR_ID"].ToString();
                gvSKU.DataSource = dtIssuanceDetail;
                gvSKU.DataBind();

                Session.Add("dtSKU", dtIssuanceDetail);
                this.LoadSKU();
                ddlCategory.Enabled = false;
                ddlLocation.Enabled = false;
            }
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDocumentNo();
        CreateSKUDataTable();
        LoadGrid();
    }

    private void LoadDocumentNo()
    {
        ddlDocumentNo.Items.Clear();
        ddlDocumentNo.Items.Add(new ListItem("New", Constants.LongNullValue.ToString()));
        DataTable dtIssuanceMaster = PurchCtl.GetMaterialIssuanceMaster(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToDateTime(txtDate.Text));
        clsWebFormUtil.FillDropDownList(this.ddlDocumentNo, dtIssuanceMaster, 0, 0);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadLocation()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, true);
    }

    /// <summary>
    /// Checks SKU Stock
    /// </summary>
    /// <param name="SKU_ID">SKU</param>
    /// <returns>SKU Stock as Integer</returns>
    private int CheckStockStatus(int SKU_ID)
    {        
        DataTable dt = mController.SelectSKUClosingStock(int.Parse(ddlLocation.SelectedValue.ToString()), SKU_ID, "", DateTime.Parse(txtDate.Text));
        if (dt.Rows.Count > 0)
        {
            if (int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty >= int.Parse(txtQuantity.Text))
            {
                return -1;
            }
            else
            {
                return int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty;
            }
        }
        return 0;
    }

    private DataTable GetFinishedDetail()
    {
        DataTable dtSKU = SKUCtl.GetFinshedDetail(Convert.ToInt32(ddlSKU.SelectedValue));
        return dtSKU;
    }
}