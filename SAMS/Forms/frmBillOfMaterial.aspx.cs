using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmBillOfMaterial : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    SkuController SKUCtl = new SkuController();
    private static int RowId;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadSKUFinished();
            this.LoadSKU();
            this.CreateSKUDataTable();
            this.GetFinishedDetail();
        }
    }

    /// <summary>
    /// Load Finished SKUs To SKU DropdownList
    /// </summary>
    private void LoadSKUFinished()
    {
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.FinishedGoods, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 3, Constants.DateNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlSKUFinished, Dtsku_Price, 0, 10, true);
    }

    /// <summary>
    /// Load SKUs To SKU DropdownList
    /// </summary>
    private void LoadSKU()
    {
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.RawMetarial, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 3, Constants.DateNullValue);
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
            if (btnAdd.Text == "Add SKU")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = dtSKU.NewRow();
                    dr["SKU_ID"] = Convert.ToInt32(ddlSKU.SelectedValue);
                    dr["QUANTITY"] = int.Parse(txtQuantity.Text);
                    dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;
                    dtSKU.Rows.Add(dr);
                    txtQuantity.Text = string.Empty;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('SKU Already Exists')", true);
                }                
            }
            else
            {
                DataRow dr = dtSKU.Rows[RowId];
                dr["SKU_ID"] = Convert.ToInt32(ddlSKU.SelectedValue);
                dr["QUANTITY"] = int.Parse(txtQuantity.Text);
                dr["SKU_NAME"] = ddlSKU.SelectedItem.Text;

                txtQuantity.Text = string.Empty;
                btnAdd.Text = "Add SKU";
                RowId = 0;
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
            if (SKUCtl.InsertFinishedSKU(Convert.ToInt32(ddlSKUFinished.SelectedValue), Convert.ToDateTime(System.DateTime.Now.ToShortDateString()), Convert.ToInt32(Session["UserID"]), dtSKU))
            {
                this.CreateSKUDataTable();
                this.GetFinishedDetail();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Bill of material added succesfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured')", true);
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

    private void GetFinishedDetail()
    {
        DataTable dtSKU = SKUCtl.GetFinshedDetail(Convert.ToInt32(ddlSKUFinished.SelectedValue));        
        Session.Add("dtSKU", dtSKU);
        LoadGrid();
    }

    protected void ddlSKUFinished_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CreateSKUDataTable();
        this.GetFinishedDetail();
    }
}