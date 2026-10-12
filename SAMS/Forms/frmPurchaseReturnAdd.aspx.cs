using System;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmPurchaseReturnAdd : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable PurchaseReturnSKU;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateTable();
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadStore();
            this.LoadSKUDetail();
            btnAdd.Attributes.Add("onclick", "return ValidateForm();");                        
            if (Session["PurchaseReturnID"] != null)
            {
                LoadPurchaseReturnDetail();
            }
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreateTable()
    {
        PurchaseReturnSKU = new DataTable();
        PurchaseReturnSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseReturnSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseReturnSKU.Columns.Add("Quantity", typeof(int));
        PurchaseReturnSKU.Columns.Add("PRICE", typeof(decimal));
        PurchaseReturnSKU.Columns.Add("Amount", typeof(decimal));
        this.Session.Add("PurchaseReturnSKU", PurchaseReturnSKU);

    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, true);
    }

    private void LoadStore()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.GetStore(Constants.IntNullValue, Convert.ToInt32(ddlLocation.SelectedValue));
        clsWebFormUtil.FillDropDownList(this.ddlStore, dt, "STORE_ID", "STORE_NAME", true);
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadStore();
        this.LoadSKUDetail();
    }

    /// <summary>
    /// Loads SKU Prices
    /// </summary>
    private void LoadSKUDetail()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.FinishedGoods, int.Parse(ddlLocation.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 3, Constants.DateNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlProduct, Dtsku_Price, 0, 11, true);
        this.Session.Add("Dtsku_Price", Dtsku_Price);
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSupplier, m_dt, 0, 1, true);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseReturnSKU = (DataTable)this.Session["PurchaseReturnSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");

        if (foundRows.Length > 0)
        {
            if (btnAdd.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = PurchaseReturnSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));
                    dr["Amount"] = decimal.Parse(dc.chkNull_0(txtAmount.Text));
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    PurchaseReturnSKU.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('  " + ddlProduct.SelectedItem.Text + " Already Exists ');", true);
                    return;
                }
            }
            else if (btnAdd.Text == "Update")
            {
                DataRow dr = PurchaseReturnSKU.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));
                dr["Amount"] = decimal.Parse(dc.chkNull_0(txtAmount.Text));
                dr["Quantity"] = int.Parse(txtQuantity.Text);
            }
            this.Session.Add("PurchaseReturnSKU", PurchaseReturnSKU);
            this.LoadGird();
            txtQuantity.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            btnAdd.Text = "Add";
            ScriptManager.GetCurrent(Page).SetFocus(ddlProduct);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Wrong SKU Select');", true);

        }
    }

    /// <summary>
    /// Checkes Duplicate SKUS in Document Detail Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseReturnSKU = (DataTable)this.Session["PurchaseReturnSKU"];
        DataRow[] foundRows = PurchaseReturnSKU.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        PurchaseReturnSKU = (DataTable)this.Session["PurchaseReturnSKU"];
        gvProduct.DataSource = PurchaseReturnSKU;
        gvProduct.DataBind();        
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        ddlProduct.SelectedValue = gvProduct.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = gvProduct.Rows[e.NewEditIndex].Cells[2].Text;
        txtRate.Text = gvProduct.Rows[e.NewEditIndex].Cells[3].Text;
        txtAmount.Text = gvProduct.Rows[e.NewEditIndex].Cells[4].Text;
        txtQuantity.Focus();
        btnAdd.Text = "Update";
    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseReturnSKU = (DataTable)this.Session["PurchaseReturnSKU"];
        if (PurchaseReturnSKU.Rows.Count > 0)
        {
            PurchaseReturnSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("PurchaseReturnSKU", PurchaseReturnSKU);
            this.LoadGird();
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvProduct.Rows.Count > 0)
        {
            PurchaseReturnSKU = (DataTable)Session["PurchaseReturnSKU"];
            PurchaseController PurCtl = new PurchaseController();
            if (btnSave.Text == "Save")
            {
                if (PurCtl.InsertPurchaseReturn(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlStore.SelectedValue), Convert.ToInt64(dc.chkNull_0(txtPO.Text)), txtRemarks.Text, Convert.ToInt32(Session["UserID"]), PurchaseReturnSKU))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Purchase return saved successfully.');window.location.href = 'frmPurchaseReturn.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
                }
            }
            else
            {
                if (PurCtl.UpdatePurchaseReturn(Convert.ToInt64(Session["PurchaseReturnID"]),Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlStore.SelectedValue), Convert.ToInt64(txtPO.Text), txtRemarks.Text, Convert.ToInt32(Session["UserID"]), false, PurchaseReturnSKU))
                {
                    Session.Remove("PurchaseReturnID");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Purchase return updated successfully.');window.location.href = 'frmPurchaseReturn.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No product found');", true);
        }
    }

    private void LoadPurchaseReturnDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataSet ds = PurCtl.GetPurchaseReturn(1, Convert.ToInt64(Session["PurchaseReturnID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["DISTRIBUTOR_ID"].ToString();
            ddlStore.SelectedValue = ds.Tables[0].Rows[0]["STORE_ID"].ToString();
            txtPO.Text = ds.Tables[0].Rows[0]["PO"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
            ddlSupplier.SelectedValue = ds.Tables[0].Rows[0]["PRINCIPAL_ID"].ToString();
            btnSave.Text = "Update";
        }
        PurchaseReturnSKU = ds.Tables[1];        
        this.Session.Add("PurchaseReturnSKU", PurchaseReturnSKU);
        LoadGird();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("PurchaseReturnID");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "window.location.href = 'frmPurchaseReturn.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
    }
}