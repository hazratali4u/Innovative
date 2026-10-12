using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmPurchaseInvoice : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    PurchaseController mController = new PurchaseController();
    DataControl dc = new DataControl();
    DataTable OrderedSKU;

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadAccountHead();
          //  this.GetDocumentNo();
          //  this.LoadGRN();
        }
    }

    /// <summary>
    /// Gets Document Nos For Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo(Constants.LongNullValue, Constants.IntNullValue, 2,Convert.ToInt32(ddlSupplier.SelectedValue));
        drpDocumentNo.Items.Add(new clsListItems("--------Select--------", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.IntNullValue.ToString())
        {
            OrderedSKU = new DataTable();
            this.Session.Add("OrderedSKU", OrderedSKU);
            LoadGird();
        }
        else
        {
            GrdPurchase.DataSource = null;
            GrdPurchase.DataBind();
            this.LoadGRN();
            
        }
    }

    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
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
        if (drpDocumentNo.Items.Count > 0)
        {
            OrderedSKU = mPurchase.GetGRNDetail(GRNNos, Constants.IntNullValue,2);
            this.Session.Add("OrderedSKU", OrderedSKU);
            LoadGird();
        }
    }
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        decimal TotalOrdQty = 0;
        decimal TotalRecQty = 0;
        decimal TotalGross = 0;
        decimal TotalDiscount = 0;
        decimal TotalGSTAmount = 0;
        decimal TotalNetAmount = 0;
        OrderedSKU = (DataTable)this.Session["OrderedSKU"];
        for (int i = 0; i < OrderedSKU.Rows.Count; i++)
        {
            if (Convert.ToInt32(OrderedSKU.Rows[i][7]) == 0)
            {
                OrderedSKU.Rows.Remove(OrderedSKU.Rows[i]);
                i--;
            }
        }
        GrdPurchase.DataSource = OrderedSKU;
        GrdPurchase.DataBind();
        foreach (DataRow dr in OrderedSKU.Rows)
        {
            TotalOrdQty += decimal.Parse(dc.chkNull_0(dr["Quantity"].ToString()));
            TotalRecQty += decimal.Parse(dc.chkNull_0(dr["RcdQty"].ToString()));
            //TotalGross += decimal.Parse(dc.chkNull_0(dr["Gross"].ToString()));
            //TotalDiscount += decimal.Parse(dc.chkNull_0(dr["Discount"].ToString()));
            //TotalGSTAmount += decimal.Parse(dc.chkNull_0(dr["GSTAmount"].ToString()));
            //TotalNetAmount += decimal.Parse(dc.chkNull_0(dr["NetAmount"].ToString()));
        }
        txtOrderedQty.Text = TotalOrdQty.ToString();
        txtReceivedQty.Text = TotalRecQty.ToString();
        txtTotalAmount.Text = TotalGross.ToString();
        txtDiscount.Text = TotalDiscount.ToString();
        txtGSTAmount.Text = TotalGSTAmount.ToString();
        txtNetAmount.Text = TotalNetAmount.ToString();
    }
    private bool checkRate()
    {
        bool value = false;

        foreach (GridViewRow row in GrdPurchase.Rows)
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
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        DataTable PurchasedPSKU = new DataTable();
        PurchasedPSKU.Columns.Add("SKU_ID", typeof(int));
        PurchasedPSKU.Columns.Add("QUANTITY", typeof(decimal));
        PurchasedPSKU.Columns.Add("PRICE", typeof(decimal));
        PurchasedPSKU.Columns.Add("DISCOUNT", typeof(decimal));
        PurchasedPSKU.Columns.Add("GST", typeof(decimal));
        PurchasedPSKU.Columns.Add("AMOUNT", typeof(decimal));

        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtDiscount = gvr.FindControl("txtDiscount") as TextBox;
            TextBox txtRate = gvr.FindControl("txtRate") as TextBox;
            TextBox txtGross = gvr.FindControl("txtGross") as TextBox;
            TextBox txtGSTRate = gvr.FindControl("txtGSTRate") as TextBox;
            DateTime dtExpDate = Constants.DateNullValue;

            if (Convert.ToDecimal(dc.chkNull_0(txtRate.Text)) < 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Rate must be greater than 0.');", true);
                return;
            }

            DataRow dr = PurchasedPSKU.NewRow();
            dr["SKU_ID"] = Convert.ToInt32(dc.chkNull_0(gvr.Cells[0].Text));
            dr["QUANTITY"] = Convert.ToDecimal(dc.chkNull_0(gvr.Cells[3].Text));
            dr["PRICE"] = Convert.ToDecimal(dc.chkNull_0(txtRate.Text));
            dr["DISCOUNT"] = Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text));
            dr["AMOUNT"] = Convert.ToDecimal(dc.chkNull_0(txtGross.Text));
            dr["GST"] = Convert.ToDecimal(dc.chkNull_0(txtGSTRate.Text));
            PurchasedPSKU.Rows.Add(dr);
        }

        if (PurchasedPSKU.Rows.Count > 0 && checkRate())
        {
            if (CalculatePurchase(DateTime.Parse(txtDate.Text), PurchasedPSKU))
            {
                txtRemarks.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtRefNo.Text = string.Empty;
                this.GetDocumentNo();
                this.LoadGRN();
                drpDocumentNo_SelectedIndexChanged(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Purchase Invoice saved successfully.');", true);
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

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.LoadGird();
    }

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool CalculatePurchase(DateTime MWorkDate, DataTable PurchasedPSKU)
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

        return mController.InsertPurchaseInvoice(Convert.ToInt64(drpDocumentNo.SelectedValue), txtRefNo.Text, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(Session["UserID"]), Convert.ToDecimal(dc.chkNull_0(txtNetAmount.Text)), Convert.ToInt32(ddlAccount.SelectedValue), Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), txtRemarks.Text, PurchasedPSKU, dtGRNIDs);                
    }

    protected void GrdPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtRecQty = e.Row.FindControl("txtRecQty") as TextBox;
            TextBox txtDiscount = e.Row.FindControl("txtDiscount") as TextBox;
            TextBox txtRate = e.Row.FindControl("txtRate") as TextBox;
            TextBox txtGross = e.Row.FindControl("txtGross") as TextBox;
            TextBox txtGSTRate = e.Row.FindControl("txtGSTRate") as TextBox;
            TextBox txtGSTAmount = e.Row.FindControl("txtGSTAmount") as TextBox;
            TextBox txtGTotal = e.Row.FindControl("txtGTotal") as TextBox;

            System.Text.StringBuilder atrCalculation = new System.Text.StringBuilder();

            atrCalculation.Append("DoCalculation(");
            atrCalculation.Append(txtRecQty.ClientID);
            atrCalculation.Append(", ");
            atrCalculation.Append(txtRate.ClientID);
            atrCalculation.Append(", ");                       
            atrCalculation.Append(txtGross.ClientID);
            atrCalculation.Append(",");
            atrCalculation.Append(txtDiscount.ClientID);
            atrCalculation.Append(", ");
            atrCalculation.Append(txtGSTRate.ClientID);
            atrCalculation.Append(",");
            atrCalculation.Append(txtGSTAmount.ClientID);
            atrCalculation.Append(",");
            atrCalculation.Append(txtGTotal.ClientID + ");");
            txtRecQty.Attributes.Add("onChange", atrCalculation.ToString());
            txtRate.Attributes.Add("onChange", atrCalculation.ToString());
            txtDiscount.Attributes.Add("onChange", atrCalculation.ToString());
            txtGSTRate.Attributes.Add("onChange", atrCalculation.ToString());
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
       DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(txtDate.Text));
       
        ddlSupplier.Items.Add(new ListItem("--------Select--------", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSupplier, m_dt, 0, 1);
    }

    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetDocumentNo();
        this.LoadGRN();
        drpDocumentNo_SelectedIndexChanged(null, null);
    }

    private void LoadGRN()
    {
        lbGRN.Items.Clear();
        DataTable dt = mController.GetGRNNo(Constants.LongNullValue, long.Parse(drpDocumentNo.SelectedValue),2);
        clsWebFormUtil.FillListBox(this.lbGRN, dt, 0, 0);
        if (lbGRN.Items.Count > 0)
        {
            btnShow.Visible = true;
            lblGRN.Visible = true;
        }
        else
        {
            btnShow.Visible = false;
            lblGRN.Visible = false;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.LoadDocumentDetail();
    }

    /// <summary>
    /// Loads Account Heads To AccountHead Combo
    /// </summary>
    private void LoadAccountHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.GetAssignAccountHead(Constants.IntNullValue, 52);
        clsWebFormUtil.FillDropDownList(ddlAccount, dt, 0, 4, true);
    }
}