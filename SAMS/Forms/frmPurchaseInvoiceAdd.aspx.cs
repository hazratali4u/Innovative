using System;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmPurchaseInvoiceAdd : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    private static int RowNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadDistributor();            
            this.LoadPrincipal();
            this.GetRequisition();
            this.LoadGRN(5);
            if (Session["PurchaseInvoiceID"] != null)
            {
                LoadPurchaseInvoiceDetail();
            }
        }
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
            
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvProduct.Rows.Count > 0)
        {
            DataTable PurchasedPSKU = new DataTable();
            PurchasedPSKU.Columns.Add("SKU_ID", typeof(int));
            PurchasedPSKU.Columns.Add("QUANTITY", typeof(decimal));
            PurchasedPSKU.Columns.Add("PRICE", typeof(decimal));
            PurchasedPSKU.Columns.Add("DISCOUNT", typeof(decimal));
            PurchasedPSKU.Columns.Add("GST", typeof(decimal));
            PurchasedPSKU.Columns.Add("AMOUNT", typeof(decimal));
            decimal NetAmount = 0;
            foreach (GridViewRow gvr in gvProduct.Rows)
            {
                TextBox txtDiscount = gvr.FindControl("txtDiscount") as TextBox;
                TextBox txtRate = gvr.FindControl("txtRate") as TextBox;
                TextBox txtGross = gvr.FindControl("txtGross") as TextBox;
                TextBox txtTaxPer = gvr.FindControl("txtTaxPer") as TextBox;
                TextBox txtReceived = gvr.FindControl("txtReceived") as TextBox;

                if (Convert.ToDecimal(dc.chkNull_0(txtRate.Text)) < 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Rate must be greater than 0.');", true);
                    return;
                }

                DataRow dr = PurchasedPSKU.NewRow();
                dr["SKU_ID"] = Convert.ToInt32(dc.chkNull_0(gvr.Cells[0].Text));
                dr["QUANTITY"] = Convert.ToDecimal(dc.chkNull_0(txtReceived.Text));
                dr["PRICE"] = Convert.ToDecimal(dc.chkNull_0(txtRate.Text));
                dr["DISCOUNT"] = Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text));
                dr["AMOUNT"] = Convert.ToDecimal(dc.chkNull_0(txtGross.Text));
                dr["GST"] = Convert.ToDecimal(dc.chkNull_0(txtTaxPer.Text));
                PurchasedPSKU.Rows.Add(dr);

                NetAmount += (Convert.ToDecimal(dr["QUANTITY"]) * Convert.ToDecimal(dr["PRICE"]) - Convert.ToDecimal(dr["DISCOUNT"])) + (Convert.ToDecimal(dr["QUANTITY"]) * Convert.ToDecimal(dr["PRICE"]) - Convert.ToDecimal(dr["DISCOUNT"])) * Convert.ToDecimal(dr["GST"]) / 100;
            }

            if (PurchasedPSKU.Rows.Count > 0 && checkRate())
            {
                if (CalculatePurchase(DateTime.Parse(Session["CurrentWorkDate"].ToString()), PurchasedPSKU,NetAmount))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Purchase Invoice saved successfully.');window.location.href = 'frmPurchaseInvoice.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Some error occured.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert(' At least one SKU must enter OR Rate not Entered');", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No product found');", true);
        }
    }

    private bool checkRate()
    {
        bool value = false;

        foreach (GridViewRow row in gvProduct.Rows)
        {
            TextBox txtRate = (TextBox)row.Cells[4].FindControl("txtRate");
            string a = Convert.ToString(txtRate.Text);
            if (a == "")
            {
                value = false;
            }
            else
            {
                value = true;
            }
        }
        return value;
    }

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool CalculatePurchase(DateTime MWorkDate, DataTable PurchasedPSKU, decimal NetAmount)
    {
        PurchaseController mController = new PurchaseController();

        DataTable dtGRNIDs = new DataTable();
        dtGRNIDs.Columns.Add("GRN_ID", typeof(long));

        foreach (ListItem li in lbGRN.Items)
        {
            if (li.Selected)
            {
                DataRow dr = dtGRNIDs.NewRow();
                dr["GRN_ID"] = Convert.ToInt32(li.Value);
                dtGRNIDs.Rows.Add(dr);

            }
        }
        if (btnSave.Text == "Save")
        {
            return mController.InsertPurchaseInvoice(Convert.ToInt64(ddlRequisition.SelectedValue), txtRef.Text, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(Session["UserID"]), NetAmount, txtRemarks.Text, PurchasedPSKU, dtGRNIDs);
        }
        else
        {
            return mController.UpdatePurchaseInvoice(Convert.ToInt64(Session["PurchaseInvoiceID"]), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt64(ddlRequisition.SelectedValue), txtRef.Text, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(Session["UserID"]), NetAmount, txtRemarks.Text, true, PurchasedPSKU, dtGRNIDs);
        }
    }

    private void LoadPurchaseInvoiceDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataSet ds = PurCtl.GetPurchaseInvoice(1, Convert.ToInt64(Session["PurchaseInvoiceID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSupplier.SelectedValue = ds.Tables[0].Rows[0]["PRINCIPAL_ID"].ToString();
            ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["DISTRIBUTOR_ID"].ToString();
            txtRef.Text = ds.Tables[0].Rows[0]["REF_NO"].ToString();
            GetRequisition();
            ddlRequisition.SelectedValue = ds.Tables[0].Rows[0]["REQUISITION_MASTER_ID"].ToString();
            ddlSupplier.Enabled = false;
            ddlRequisition.Enabled = false;
            this.LoadGRN(6);
            foreach(ListItem li in lbGRN.Items)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    if (dr["GRN_ID"].ToString() == li.Value)
                    {
                        li.Selected = true;
                    }
                }
            }
            btnSave.Text = "Update";           
        }
        gvProduct.DataSource = ds.Tables[1];
        gvProduct.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("PurchaseInvoiceID");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "window.location.href = 'frmGRN.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
    }

    private void GetRequisition()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable GRNSKU = PurCtl.GetRequisition(2,0,Convert.ToInt32(ddlLocation.SelectedValue),Convert.ToInt32(ddlSupplier.SelectedValue));
        clsWebFormUtil.FillDropDownList(this.ddlRequisition, GRNSKU, "REQUISITION_MASTER_ID", "REQUISITION_MASTER_ID",true);
        if (GRNSKU.Rows.Count > 0)
        {
            ddlLocation.SelectedValue = GRNSKU.Rows[0]["DISTRIBUTOR_ID"].ToString();
        }
    }

    protected void ddlRequisition_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGRN(5);
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSupplier, m_dt, 0, 1, true);
    }

    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRequisition();
        this.LoadGRN(5);
    }

    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Text.StringBuilder atrCalTotal = new System.Text.StringBuilder();
            TextBox txtReceived = e.Row.FindControl("txtReceived") as TextBox;
            TextBox txtRate = e.Row.FindControl("txtRate") as TextBox;
            TextBox txtGross = e.Row.FindControl("txtGross") as TextBox;
            TextBox txtDiscount = e.Row.FindControl("txtDiscount") as TextBox;
            TextBox txtTaxPer = e.Row.FindControl("txtTaxPer") as TextBox;
            TextBox txtTaxAmount = e.Row.FindControl("txtTaxAmount") as TextBox;
            TextBox txtNetAmount = e.Row.FindControl("txtNetAmount") as TextBox;
            
            atrCalTotal.Append("CalTotal(");
            atrCalTotal.Append(txtReceived.ClientID);
            atrCalTotal.Append(", ");
            atrCalTotal.Append(txtRate.ClientID);
            atrCalTotal.Append(",");
            atrCalTotal.Append(txtGross.ClientID);
            atrCalTotal.Append(",");
            atrCalTotal.Append(txtDiscount.ClientID);
            atrCalTotal.Append(",");
            atrCalTotal.Append(txtTaxPer.ClientID);
            atrCalTotal.Append(",");
            atrCalTotal.Append(txtTaxAmount.ClientID);
            atrCalTotal.Append(",");
            atrCalTotal.Append(txtNetAmount.ClientID + ");");
            txtRate.Attributes.Add("onblur", atrCalTotal.ToString());
            txtTaxPer.Attributes.Add("onblur", atrCalTotal.ToString());
            txtDiscount.Attributes.Add("onblur", atrCalTotal.ToString());
        }
    }

    private void LoadGRN(int TypeID)
    {
        if (ddlRequisition.Items.Count > 0)
        {
            PurchaseController PurCtl = new PurchaseController();
            lbGRN.Items.Clear();
            DataTable dt = PurCtl.GetGRN(TypeID, Constants.LongNullValue, Convert.ToInt64(ddlRequisition.SelectedValue));
            clsWebFormUtil.FillListBox(this.lbGRN, dt, 0, 0);
            if (lbGRN.Items.Count > 0)
            {
                btnShow.Visible = true;
            }
            else
            {
                btnShow.Visible = false;
            }
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.LoadGrid();
    }

    private void LoadGrid()
    {
        PurchaseController mPurchase = new PurchaseController();
        string GRNNos = string.Empty;
        foreach (ListItem li in lbGRN.Items)
        {
            if (li.Selected)
            {
                GRNNos += li.Value + ",";
            }
        }
        
        DataTable dtPurchase  = mPurchase.GetGRNDetail(GRNNos, Constants.IntNullValue, 2);
        gvProduct.DataSource = dtPurchase;
        gvProduct.DataBind();
        
    }
}