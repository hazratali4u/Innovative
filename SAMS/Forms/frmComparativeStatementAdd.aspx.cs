using System;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmComparativeStatementAdd : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable ComparativeStatementSKU;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateTable();
            this.LoadDistributor();
            this.LoadStore();
            this.GetRequisition();
            this.LoadPrincipal();  
            this.LoadSKUDetail();
            txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            btnAdd.Attributes.Add("onclick", "return ValidateForm();");                        
            if (Session["ComparativeStatementID"] != null)
            {
                LoadComparativeStatementDetail();
            }
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreateTable()
    {
        ComparativeStatementSKU = new DataTable();
        ComparativeStatementSKU.Columns.Add("SKU_ID", typeof(int));
        ComparativeStatementSKU.Columns.Add("SKU_HIE_ID", typeof(int));
        ComparativeStatementSKU.Columns.Add("SKU_HIE_NAME", typeof(string));
        ComparativeStatementSKU.Columns.Add("SKU_Name", typeof(string));
        ComparativeStatementSKU.Columns.Add("Quantity", typeof(decimal));
        ComparativeStatementSKU.Columns.Add("PRICE_PREVIOUS", typeof(decimal));        
        ComparativeStatementSKU.Columns.Add("PRICE", typeof(decimal));
        ComparativeStatementSKU.Columns.Add("Amount", typeof(decimal));
        ComparativeStatementSKU.Columns.Add("PaymentMode", typeof(string));
        ComparativeStatementSKU.Columns.Add("PaymentModeID", typeof(int));
        ComparativeStatementSKU.Columns.Add("PaymentTerm", typeof(string));
        this.Session.Add("ComparativeStatementSKU", ComparativeStatementSKU);

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
        ComparativeStatementSKU = (DataTable)this.Session["ComparativeStatementSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");

        if (foundRows.Length > 0)
        {
            if (btnAdd.Text == "Add")
            {
                if (CheckDublicateSKU())
                {                    
                    DataRow dr = ComparativeStatementSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_HIE_ID"] = ddlSupplier.SelectedValue;
                    dr["SKU_HIE_NAME"] = ddlSupplier.SelectedValue;
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["Quantity"] = decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["PRICE_PREVIOUS"] = decimal.Parse(dc.chkNull_0(txtPreviousPrice.Text));
                    dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtQuotedPrice.Text));
                    dr["Amount"] = decimal.Parse(dc.chkNull_0(txtQuotedPrice.Text)) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["PaymentMode"] = ddlPaymentMode.SelectedItem.Text;
                    dr["PaymentModeID"] = ddlPaymentMode.SelectedValue;
                    dr["PaymentTerm"] = txtPaymentTerm.Text;
                    ComparativeStatementSKU.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('  " + ddlProduct.SelectedItem.Text + " Already Exists ');", true);
                    return;
                }
            }
            else if (btnAdd.Text == "Update")
            {
                DataRow dr = ComparativeStatementSKU.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_HIE_ID"] = ddlSupplier.SelectedValue;
                dr["SKU_HIE_NAME"] = ddlSupplier.SelectedValue;
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["Quantity"] = decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                dr["PRICE_PREVIOUS"] = decimal.Parse(dc.chkNull_0(txtPreviousPrice.Text));
                dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtQuotedPrice.Text));
                dr["Amount"] = decimal.Parse(dc.chkNull_0(txtQuotedPrice.Text)) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                dr["PaymentMode"] = ddlPaymentMode.SelectedItem.Text;
                dr["PaymentModeID"] = ddlPaymentMode.SelectedValue;
                dr["PaymentTerm"] = txtPaymentTerm.Text;
            }
            this.Session.Add("ComparativeStatementSKU", ComparativeStatementSKU);
            this.LoadGird();
            txtQuantity.Text = string.Empty;
            txtPreviousPrice.Text = string.Empty;
            txtQuotedPrice.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtPaymentTerm.Text = string.Empty;
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
        ComparativeStatementSKU = (DataTable)this.Session["ComparativeStatementSKU"];
        DataRow[] foundRows = ComparativeStatementSKU.Select("SKU_ID = '" + ddlProduct.SelectedValue + "' AND SKU_HIE_ID='" + ddlSupplier.SelectedValue + "'");
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
        ComparativeStatementSKU = (DataTable)this.Session["ComparativeStatementSKU"];
        gvProduct.DataSource = ComparativeStatementSKU;
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
        ddlSupplier.SelectedValue = gvProduct.Rows[e.NewEditIndex].Cells[0].Text;
        ddlProduct.SelectedValue = gvProduct.Rows[e.NewEditIndex].Cells[1].Text;
        txtQuantity.Text = gvProduct.Rows[e.NewEditIndex].Cells[4].Text;
        txtPreviousPrice.Text = gvProduct.Rows[e.NewEditIndex].Cells[5].Text;
        txtQuotedPrice.Text = gvProduct.Rows[e.NewEditIndex].Cells[6].Text;
        txtAmount.Text = gvProduct.Rows[e.NewEditIndex].Cells[7].Text;
        ddlPaymentMode.SelectedValue = gvProduct.Rows[e.NewEditIndex].Cells[8].Text;
        txtPaymentTerm.Text = gvProduct.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
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
        ComparativeStatementSKU = (DataTable)this.Session["ComparativeStatementSKU"];
        if (ComparativeStatementSKU.Rows.Count > 0)
        {
            ComparativeStatementSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("ComparativeStatementSKU", ComparativeStatementSKU);
            this.LoadGird();
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvProduct.Rows.Count > 0)
        {
            ComparativeStatementSKU = (DataTable)Session["ComparativeStatementSKU"];
            PurchaseController PurCtl = new PurchaseController();
            if (btnSave.Text == "Save")
            {
                if (PurCtl.InsertComparativeStatement(Convert.ToInt64(ddlRequisition.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlStore.SelectedValue), Convert.ToDateTime(txtDate.Text), txtOriginator.Text, txtReviewedBy.Text, txtApprovedBy.Text, Convert.ToInt32(Session["UserID"]), ComparativeStatementSKU))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Comparative Statement saved successfully.');window.location.href = 'frmComparativeStatement.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
                }
            }
            else
            {
                if (PurCtl.UpdateComparativeStatement(Convert.ToInt64(Session["ComparativeStatementID"]), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlStore.SelectedValue), Convert.ToDateTime(txtDate.Text), txtOriginator.Text, txtReviewedBy.Text, txtApprovedBy.Text, Convert.ToInt32(Session["UserID"]),false, ComparativeStatementSKU))
                {
                    Session.Remove("ComparativeStatementID");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Comparative Statement updated successfully.');window.location.href = 'frmComparativeStatement.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
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

    private void LoadComparativeStatementDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataSet ds = PurCtl.GetComparativeStatement(1, Convert.ToInt64(Session["ComparativeStatementID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtOriginator.Text = ds.Tables[0].Rows[0]["ORIGINATOR_NAME"].ToString();
            ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["DISTRIBUTOR_ID"].ToString();
            ddlStore.SelectedValue = ds.Tables[0].Rows[0]["STORE_ID"].ToString();
            txtDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DELIVERY_DATE"]).ToString("dd-MMM-yyyy");
            txtReviewedBy.Text = ds.Tables[0].Rows[0]["REVIEWD_BY"].ToString();
            txtApprovedBy.Text = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();
            btnSave.Text = "Update";
        }
        ComparativeStatementSKU = ds.Tables[1];        
        this.Session.Add("ComparativeStatementSKU", ComparativeStatementSKU);
        LoadGird();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("ComparativeStatementID");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "window.location.href = 'frmComparativeStatement.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
    }

    private void GetRequisition()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable dtReq = PurCtl.GetRequisition(2,0);
        clsWebFormUtil.FillDropDownList(this.ddlRequisition, dtReq, "REQUISITION_MASTER_ID", "REQUISITION_MASTER_ID");
        if (dtReq.Rows.Count > 0)
        {
            ddlLocation.SelectedValue = dtReq.Rows[0]["DISTRIBUTOR_ID"].ToString();
            this.LoadStore();
            ddlStore.SelectedValue = dtReq.Rows[0]["STORE_ID"].ToString();
            txtDate.Text = Convert.ToDateTime(dtReq.Rows[0]["DELIVERY_DATE"]).ToString("dd-MMM-yyyy");
            txtOriginator.Text = dtReq.Rows[0]["ORIGINATOR_NAME"].ToString();
        }
        Session.Add("dtReq", dtReq);
    }

    protected void ddlRequisition_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtReq = (DataTable)Session["dtReq"];
        if (dtReq.Rows.Count > 0)
        {
            foreach (DataRow dr in dtReq.Rows)
            {
                if (dr["REQUISITION_MASTER_ID"].ToString() == ddlRequisition.SelectedValue)
                {
                    ddlLocation.SelectedValue = dr["DISTRIBUTOR_ID"].ToString();
                    this.LoadStore();
                    ddlStore.SelectedValue = dr["STORE_ID"].ToString();
                    txtDate.Text = Convert.ToDateTime(dr["DELIVERY_DATE"]).ToString("dd-MMM-yyyy");
                    txtOriginator.Text = dr["ORIGINATOR_NAME"].ToString();
                }
            }
        }
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSupplier, m_dt, 0, 1, true);
    }
}