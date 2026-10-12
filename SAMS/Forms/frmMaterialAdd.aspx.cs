using System;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmMaterial : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable MaterialSKU;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateTable();
            this.LoadDistributor();
            this.LoadStore();
            this.LoadSKUDetail();
            btnAdd.Attributes.Add("onclick", "return ValidateForm();");                        
            if (Session["MaterialID"] != null)
            {
                LoadMaterialDetail();
            }
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreateTable()
    {
        MaterialSKU = new DataTable();
        MaterialSKU.Columns.Add("SKU_ID", typeof(int));
        MaterialSKU.Columns.Add("SKU_Name", typeof(string));
        MaterialSKU.Columns.Add("Quantity", typeof(int));
        this.Session.Add("MaterialSKU", MaterialSKU);

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
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        MaterialSKU = (DataTable)this.Session["MaterialSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");

        if (foundRows.Length > 0)
        {
            if (btnAdd.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = MaterialSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    MaterialSKU.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('  " + ddlProduct.SelectedItem.Text + " Already Exists ');", true);
                    return;
                }
            }
            else if (btnAdd.Text == "Update")
            {
                DataRow dr = MaterialSKU.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["Quantity"] = int.Parse(txtQuantity.Text);
            }
            this.Session.Add("MaterialSKU", MaterialSKU);
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
        MaterialSKU = (DataTable)this.Session["MaterialSKU"];
        DataRow[] foundRows = MaterialSKU.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");
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
        MaterialSKU = (DataTable)this.Session["MaterialSKU"];
        gvProduct.DataSource = MaterialSKU;
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
        MaterialSKU = (DataTable)this.Session["MaterialSKU"];
        if (MaterialSKU.Rows.Count > 0)
        {
            MaterialSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("MaterialSKU", MaterialSKU);
            this.LoadGird();
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvProduct.Rows.Count > 0)
        {
            MaterialSKU = (DataTable)Session["MaterialSKU"];
            PurchaseController PurCtl = new PurchaseController();
            if (btnSave.Text == "Save")
            {
                if (PurCtl.InsertMaterial(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]),Convert.ToInt32(ddlStore.SelectedValue),txtRemarks.Text, Convert.ToInt32(ddlType.SelectedValue) ,Convert.ToInt32(Session["UserID"]), MaterialSKU))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Material Issue/Return saved successfully.');window.location.href = 'frmMaterial.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
                }
            }
            else
            {
                if (PurCtl.UpdateMaterial(Convert.ToInt64(Session["MaterialID"]), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlStore.SelectedValue),txtRemarks.Text,Convert.ToInt32(ddlType.SelectedValue) ,Convert.ToInt32(Session["UserID"]), false, MaterialSKU))
                {
                    Session.Remove("MaterialID");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Material Issue/Return updated successfully.');window.location.href = 'frmMaterial.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
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

    private void LoadMaterialDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataSet ds = PurCtl.GetMaterial(1, Convert.ToInt64(Session["MaterialID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["DISTRIBUTOR_ID"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
            ddlStore.SelectedValue = ds.Tables[0].Rows[0]["STORE_ID"].ToString();
            ddlType.SelectedValue = ds.Tables[0].Rows[0]["TYPE_ID"].ToString();
            btnSave.Text = "Update";
        }
        MaterialSKU = ds.Tables[1];        
        this.Session.Add("MaterialSKU", MaterialSKU);
        LoadGird();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("MaterialID");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "window.location.href = 'frmMaterial.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
    }
}