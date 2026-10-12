using System;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmMaintenanceAdd : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable MaintenanceSKU;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateTable();
            this.LoadDistributor();
            this.LoadStore();
            this.LoadPrincipal();
            this.LoadSKUDetail();
            btnAdd.Attributes.Add("onclick", "return ValidateForm();");                        
            if (Session["MaintenanceID"] != null)
            {
                LoadMaintenanceDetail();
            }
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreateTable()
    {
        MaintenanceSKU = new DataTable();
        MaintenanceSKU.Columns.Add("SKU_ID", typeof(int));
        MaintenanceSKU.Columns.Add("SKU_Name", typeof(string));
        MaintenanceSKU.Columns.Add("Quantity", typeof(int));
        this.Session.Add("MaintenanceSKU", MaintenanceSKU);

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
        MaintenanceSKU = (DataTable)this.Session["MaintenanceSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");

        if (foundRows.Length > 0)
        {
            if (btnAdd.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = MaintenanceSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    MaintenanceSKU.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('  " + ddlProduct.SelectedItem.Text + " Already Exists ');", true);
                    return;
                }
            }
            else if (btnAdd.Text == "Update")
            {
                DataRow dr = MaintenanceSKU.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["Quantity"] = int.Parse(txtQuantity.Text);
            }
            this.Session.Add("MaintenanceSKU", MaintenanceSKU);
            this.LoadGird();
            txtQuantity.Text = string.Empty;
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
        MaintenanceSKU = (DataTable)this.Session["MaintenanceSKU"];
        DataRow[] foundRows = MaintenanceSKU.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");
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
        MaintenanceSKU = (DataTable)this.Session["MaintenanceSKU"];
        gvProduct.DataSource = MaintenanceSKU;
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
        MaintenanceSKU = (DataTable)this.Session["MaintenanceSKU"];
        if (MaintenanceSKU.Rows.Count > 0)
        {
            MaintenanceSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("MaintenanceSKU", MaintenanceSKU);
            this.LoadGird();
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvProduct.Rows.Count > 0)
        {
            MaintenanceSKU = (DataTable)Session["MaintenanceSKU"];
            PurchaseController PurCtl = new PurchaseController();
            if (btnSave.Text == "Save")
            {
                if (PurCtl.InsertMaintenance(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]),Convert.ToInt32(ddlStore.SelectedValue), txtRemarks.Text,Convert.ToInt32(ddlSupplier.SelectedValue) ,Convert.ToInt32(Session["UserID"]), MaintenanceSKU))
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
                if (PurCtl.UpdateMaintenance(Convert.ToInt64(Session["MaintenanceID"]),  Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlStore.SelectedValue), txtRemarks.Text, Convert.ToInt32(ddlSupplier.SelectedValue) ,Convert.ToInt32(Session["UserID"]), false, MaintenanceSKU))
                {
                    Session.Remove("MaintenanceID");
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

    private void LoadMaintenanceDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataSet ds = PurCtl.GetMaintenance(1, Convert.ToInt64(Session["MaintenanceID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["DISTRIBUTOR_ID"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
            ddlSupplier.SelectedValue = ds.Tables[0].Rows[0]["PRINCIPAL_ID"].ToString();
            ddlStore.SelectedValue = ds.Tables[0].Rows[0]["STORE_ID"].ToString();
            btnSave.Text = "Update";
        }
        MaintenanceSKU = ds.Tables[1];        
        this.Session.Add("MaintenanceSKU", MaintenanceSKU);
        LoadGird();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("MaintenanceID");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "window.location.href = 'frmPurchaseReturn.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
    }
}