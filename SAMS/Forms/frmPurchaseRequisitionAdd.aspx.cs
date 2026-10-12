using System;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmPurchaseRequisitionAdd : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    SkuController mSKUController = new SkuController();
    private static int RowNo;
    DataTable RequisitionSKU;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateTable();
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadStore();
            this.createQualityTable();
            this.LoadSKUDetail();
            txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            btnAdd.Attributes.Add("onclick", "return ValidateForm();");                        
            if (Session["RequisitionID"] != null)
            {
                LoadRequisitionDetail();
            }
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreateTable()
    {
        RequisitionSKU = new DataTable();
        RequisitionSKU.Columns.Add("SKU_ID", typeof(int));
        RequisitionSKU.Columns.Add("SKU_Name", typeof(string));
        RequisitionSKU.Columns.Add("Quantity", typeof(decimal));
        RequisitionSKU.Columns.Add("PRICE", typeof(decimal));        
        this.Session.Add("RequisitionSKU", RequisitionSKU);

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
        int LocationID=0;
        if(ddlLocation.Items.Count>0)
        {
           LocationID= Convert.ToInt32(ddlLocation.SelectedValue);
        }
        DataTable dt = mController.GetStore(Constants.IntNullValue, LocationID);
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
        int LocationID = 0;
        if (ddlLocation.Items.Count > 0)
        {
            LocationID=int.Parse(ddlLocation.SelectedValue.ToString());
        }
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.FinishedGoods, LocationID, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 3, Constants.DateNullValue);
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
        RequisitionSKU = (DataTable)this.Session["RequisitionSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");

        if (foundRows.Length > 0)
        {
            if (btnAdd.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = RequisitionSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtCost.Text));
                    dr["Quantity"] = decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    RequisitionSKU.Rows.Add(dr);
                    DataTable dtAllQuality = Session["dtAllQuality"] as DataTable;
                    DataTable dtQuality = ViewState["dtQuality"] as DataTable;
                    foreach (DataRow drQuality in dtQuality.Rows)
                    {
                        DataRow drAllQuality = dtAllQuality.Rows.Add();
                        drAllQuality["SKU_ID"] = drQuality["SKU_ID"];
                        drAllQuality["QUALITY_VALUE"] = drQuality["QUALITY_VALUE"];
                        drAllQuality["QUALITY_DESCRIPTION"] = drQuality["QUALITY_DESCRIPTION"];
                    }
                    Session["dtAllQuality"] = dtAllQuality;
                    LoadSKUQC();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('  " + ddlProduct.SelectedItem.Text + " Already Exists ');", true);
                    return;
                }
            }
            else if (btnAdd.Text == "Update")
            {
                DataRow dr = RequisitionSKU.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtCost.Text));
                dr["Quantity"] = decimal.Parse(txtQuantity.Text);

                DataTable dtAllQuality = Session["dtAllQuality"] as DataTable;
                DataTable dtQuality = ViewState["dtQuality"] as DataTable;
                dtQuality.AcceptChanges();
                dtAllQuality.AcceptChanges();
                for (int i = dtAllQuality.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drAllQuality = dtAllQuality.Rows[i];
                    if (Convert.ToInt32(drAllQuality["SKU_ID"]) == Convert.ToInt32(foundRows[0]["SKU_ID"]))
                    {
                        drAllQuality.Delete();
                    }
                }
                dtQuality.AcceptChanges();
                dtAllQuality.AcceptChanges();
                foreach (DataRow drQuality in dtQuality.Rows)
                {
                    DataRow drAllQuality = dtAllQuality.Rows.Add();
                    drAllQuality["SKU_ID"] = drQuality["SKU_ID"];
                    drAllQuality["QUALITY_VALUE"] = drQuality["QUALITY_VALUE"];
                    drAllQuality["QUALITY_DESCRIPTION"] = drQuality["QUALITY_DESCRIPTION"];
                }
                Session["dtAllQuality"] = dtAllQuality;
            }
            this.Session.Add("RequisitionSKU", RequisitionSKU);
            this.LoadGird();
            txtQuantity.Text = string.Empty;
            txtCost.Text = string.Empty;
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
        RequisitionSKU = (DataTable)this.Session["RequisitionSKU"];
        DataRow[] foundRows = RequisitionSKU.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");
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
        RequisitionSKU = (DataTable)this.Session["RequisitionSKU"];
        gvProduct.DataSource = RequisitionSKU;
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
        txtCost.Text = gvProduct.Rows[e.NewEditIndex].Cells[3].Text;
        txtQuantity.Focus();
        btnAdd.Text = "Update";
        DataTable dtQuality = ViewState["dtQuality"] as DataTable;
        DataTable dtAllQuality = Session["dtAllQuality"] as DataTable;
        dtQuality.Rows.Clear();
        dtAllQuality.AcceptChanges();
        foreach (DataRow drAllQuality in dtAllQuality.Rows)
        {
            if (Convert.ToInt32(drAllQuality["SKU_ID"]) == Convert.ToInt32(gvProduct.Rows[e.NewEditIndex].Cells[0].Text))
            {
                DataRow drQuality = dtQuality.Rows.Add();
                drQuality["SKU_ID"] = drAllQuality["SKU_ID"];
                drQuality["QUALITY_VALUE"] = drAllQuality["QUALITY_VALUE"];
                drQuality["QUALITY_DESCRIPTION"] = drAllQuality["QUALITY_DESCRIPTION"];
            }
        }
        dtAllQuality.AcceptChanges();
        dtQuality.AcceptChanges();
        ViewState["dtQuality"] = dtQuality;
        gvQuality.DataSource = dtQuality;
        gvQuality.DataBind();
    }
    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        RequisitionSKU = (DataTable)this.Session["RequisitionSKU"];
        if (RequisitionSKU.Rows.Count > 0)
        {
            RequisitionSKU.Rows.RemoveAt(e.RowIndex);

            DataTable dtAllQuality = Session["dtAllQuality"] as DataTable;
            foreach (DataRow drAllQuality in dtAllQuality.Rows)
            {
                if (Convert.ToInt32(drAllQuality["SKU_ID"]) == Convert.ToInt32(gvProduct.Rows[e.RowIndex].Cells[0].Text))
                {
                    drAllQuality.Delete();
                }
            }
            Session["dtAllQuality"] = dtAllQuality;
            this.Session.Add("RequisitionSKU", RequisitionSKU);
            this.LoadGird();
            this.LoadSKUQC();
        }
        btnAdd.Text = "Add";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvProduct.Rows.Count > 0)
        {
            RequisitionSKU = (DataTable)Session["RequisitionSKU"];
            PurchaseController PurCtl = new PurchaseController();
            if (btnSave.Text == "Save")
            {
                DataTable dtAllQuality = Session["dtAllQuality"] as DataTable;
                if (PurCtl.InsertRequisiton(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlStore.SelectedValue), Convert.ToDateTime(txtDate.Text), txtOriginator.Text, txtReviewedBy.Text, txtApprovedBy.Text, txtRemarks.Text, Convert.ToInt32(Session["UserID"]), RequisitionSKU, dtAllQuality))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Requisition saved successfully.');window.location.href = 'frmPurchaseRequisition.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
                }
            }
            else
            {
                DataTable dtAllQuality = Session["dtAllQuality"] as DataTable;
                if (PurCtl.UpdateRequisition(Convert.ToInt64(Session["RequisitionID"]), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlStore.SelectedValue), Convert.ToDateTime(txtDate.Text), txtOriginator.Text, txtReviewedBy.Text, txtApprovedBy.Text, txtRemarks.Text, Convert.ToInt32(Session["UserID"]), false, RequisitionSKU, dtAllQuality))
                {
                    Session.Remove("RequisitionID");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Requisition updated successfully.');window.location.href = 'frmPurchaseRequisition.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
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

    private void LoadRequisitionDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataSet ds = PurCtl.GetRequisition(1, Convert.ToInt64(Session["RequisitionID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtOriginator.Text = ds.Tables[0].Rows[0]["ORIGINATOR_NAME"].ToString();
            ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["DISTRIBUTOR_ID"].ToString();
            ddlStore.SelectedValue = ds.Tables[0].Rows[0]["STORE_ID"].ToString();
            txtDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DELIVERY_DATE"]).ToString("dd-MMM-yyyy");
            txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
            txtReviewedBy.Text = ds.Tables[0].Rows[0]["REVIEWD_BY"].ToString();
            ddlSupplier.SelectedValue = ds.Tables[0].Rows[0]["PRINCIPAL_ID"].ToString();
            txtApprovedBy.Text = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();
            btnSave.Text = "Update";
        }
        RequisitionSKU = ds.Tables[1];        
        this.Session.Add("RequisitionSKU", RequisitionSKU);
        LoadGird();
        DataTable dtAllQuality = Session["dtAllQuality"] as DataTable;
        DataTable dtQuality1 = ds.Tables[2];
        dtAllQuality.AcceptChanges();
        foreach (DataRow drQuality in dtQuality1.Rows)
        {
            DataRow drAllQuality = dtAllQuality.Rows.Add();
            drAllQuality["SKU_ID"] = drQuality["SKU_ID"];
            drAllQuality["QUALITY_VALUE"] = drQuality["QUALITY_VALUE"];
            drAllQuality["QUALITY_DESCRIPTION"] = drQuality["QUALITY_DESCRIPTION"];
        }
        dtAllQuality.AcceptChanges();
        Session.Add("dtAllQuality", dtAllQuality);
        LoadSKUQC();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("RequisitionID");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "window.location.href = 'frmPurchaseRequisition.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
    }

    #region SKU Quality PPopup
    private void createQualityTable()
    {
        DataTable dtQuality = new DataTable();
        DataTable dtAllQuality = new DataTable();
        dtQuality.Columns.Add("SKU_ID", typeof(int));
        dtQuality.Columns.Add("QUALITY_VALUE", typeof(string));
        dtQuality.Columns.Add("QUALITY_DESCRIPTION", typeof(string));
        ViewState.Add("dtQuality", dtQuality);

        dtAllQuality.Columns.Add("SKU_ID", typeof(int));
        dtAllQuality.Columns.Add("QUALITY_VALUE", typeof(string));
        dtAllQuality.Columns.Add("QUALITY_DESCRIPTION", typeof(string));
        Session.Add("dtAllQuality", dtAllQuality);

    }
    protected void gvQuality_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        DataTable dtQuality = ViewState["dtQuality"] as DataTable;
        if (dtQuality.Rows.Count > 0)
        {
            dtQuality.Rows.RemoveAt(e.RowIndex);
            dtQuality.AcceptChanges();
            gvQuality.DataSource = dtQuality;
            gvQuality.DataBind();
            ViewState["dtQuality"] = dtQuality;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Some error occured.');", true);
        }
        ModalPopupExtender.Show();
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUQC();
    }
    protected void gvQuality_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ModalPopupExtender.Show();
        txtQualityDescription.Text = gvQuality.Rows[e.NewEditIndex].Cells[0].Text;
        txtQualityValue.Text = gvQuality.Rows[e.NewEditIndex].Cells[1].Text;
        hfSKUQualityID.Value = e.NewEditIndex.ToString();
        btnSaveQuality.Text = "Update";
    }
    protected void btnSaveQuality_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        if (txtQualityDescription.Text.Trim().Length > 0)
        {
            if (btnSaveQuality.Text == "Save")
            {
                DataTable dtQuality = ViewState["dtQuality"] as DataTable;
                DataRow dr = dtQuality.Rows.Add();
                dr["QUALITY_VALUE"] = txtQualityValue.Text;
                dr["QUALITY_DESCRIPTION"] = txtQualityDescription.Text;
                dr["SKU_ID"] = Convert.ToInt32(ddlProduct.SelectedValue);
                txtQualityDescription.Text = string.Empty;
                txtQualityValue.Text = string.Empty;
                gvQuality.DataSource = dtQuality;
                gvQuality.DataBind();
                ViewState["dtQuality"] = dtQuality;
            }
            else
            {
                int i = Convert.ToInt32(hfSKUQualityID.Value);
                DataTable dtQuality = ViewState["dtQuality"] as DataTable;
                dtQuality.Rows[i]["QUALITY_VALUE"] = txtQualityValue.Text;
                dtQuality.Rows[i]["QUALITY_DESCRIPTION"] = txtQualityDescription.Text;
                gvQuality.DataSource = dtQuality;
                gvQuality.DataBind();
                ViewState["dtQuality"] = dtQuality;
                txtQualityValue.Text = string.Empty;
                txtQualityDescription.Text = string.Empty;
                btnSaveQuality.Text = "Save";
                hfSKUQualityID.Value = string.Empty;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter Quality Description');", true);
        }
    }
    private void LoadSKUQC()
    {
        DataTable dt = mSKUController.GetSKUQuality(Convert.ToInt32(ddlProduct.SelectedValue), Constants.LongNullValue);
        DataTable dtQuality = ViewState["dtQuality"] as DataTable;
        dtQuality.Rows.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            DataRow drQuality = dtQuality.Rows.Add();
            drQuality["SKU_ID"] = dr["SKU_ID"];
            drQuality["QUALITY_VALUE"] = dr["QUALITY_VALUE"];
            drQuality["QUALITY_DESCRIPTION"] = dr["QUALITY_DESCRIPTION"];
        }
        gvQuality.DataSource = dtQuality;
        gvQuality.DataBind();
        ViewState["dtQuality"] = dtQuality;
        lblSKUName.Text = ddlProduct.SelectedItem.Text;
    }
    #endregion
    protected void btnQC_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
    }
}