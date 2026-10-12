using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmPurchaseReceivedEntryAdd : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    OrderEntryController mOrderController = new OrderEntryController();
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
            if (Session["PurchaseID"] != null)
            {
                LoadGRNDetail();
            }
            else
            {
                this.GetDocumentNo();
                this.LoadDocumentDetail();
            }
        }
    }

    /// <summary>
    /// Gets Document Nos For Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        DataTable dt = mController.SelectPurchaseOrderDocumentNo(Constants.LongNullValue, Constants.IntNullValue, 0,Constants.IntNullValue);
        this.drpDocumentNo.Items.Add(new ListItem("--------Select--------", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 3);
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
            Session.Remove("PRINCIPAL_ID");
            this.Session.Add("OrderedSKU", OrderedSKU);
            LoadGird();
        }
        else
        {
            this.LoadDocumentDetail();
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
    /// Loads Locations To Location To Combo
    /// </summary>
    private void LoadToDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpTransferFor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        if (drpDocumentNo.Items.Count > 0)
        {
            DataTable dt = mController.SelectPurchaseOrderDocumentNo(Convert.ToInt64(drpDocumentNo.SelectedValue), Constants.IntNullValue, 0,Constants.IntNullValue);
            if (dt.Rows.Count > 0)
            {
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                OrderedSKU = mController.SelectPurchaseOrderDetail(long.Parse(dt.Rows[0][0].ToString()), Constants.IntNullValue);
                Session.Add("PRINCIPAL_ID",dt.Rows[0]["PRINCIPAL_ID"]);
                this.Session.Add("OrderedSKU", OrderedSKU);
                LoadGird();
            }
        }
    }

    private void LoadGRNDetail()
    {
        OrderedSKU = mController.GetGRNDetail(Session["PurchaseID"].ToString(), Constants.IntNullValue,2);
        if (OrderedSKU.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = OrderedSKU.Rows[0]["DISTRIBUTOR_ID"].ToString();
            drpDocumentNo.Items.Add(new ListItem(OrderedSKU.Rows[0]["PO_ID"].ToString(), OrderedSKU.Rows[0]["PO_ID"].ToString()));
            drpDocumentNo.Enabled = false;
            btnSaveDocument.Text = "Update Document";
        }
        this.Session.Add("OrderedSKU", OrderedSKU);
        LoadGird();
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        decimal TotalOrdQty = 0;
        decimal TotalRecQty = 0;
      
        OrderedSKU = (DataTable)this.Session["OrderedSKU"];
        GrdPurchase.DataSource = OrderedSKU;
        GrdPurchase.DataBind();
        foreach (DataRow dr in OrderedSKU.Rows)
        {
            TotalOrdQty += decimal.Parse(dc.chkNull_0(dr["Quantity"].ToString()));
            TotalRecQty += decimal.Parse(dc.chkNull_0(dr["RcdQty"].ToString()));
        }
        txtOrderedQty.Text = TotalOrdQty.ToString();
        txtReceivedQty.Text = TotalRecQty.ToString();
    }
    
    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        DataTable PurchasedPSKU = new DataTable();
        PurchasedPSKU.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchasedPSKU.Columns.Add("SKU_ID", typeof(int));
        PurchasedPSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchasedPSKU.Columns.Add("PRICE", typeof(decimal));
        PurchasedPSKU.Columns.Add("Quantity", typeof(decimal));
        PurchasedPSKU.Columns.Add("FREE_SKU", typeof(decimal));
        PurchasedPSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchasedPSKU.Columns.Add("DISCOUNT", typeof(decimal));
        PurchasedPSKU.Columns.Add("TIME_STAMP", typeof(DateTime));
        decimal  TotalQty = 0;
        decimal TotalReceived = 0;
        decimal TotalCurrentReceived = 0;

        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            DateTime dtExpDate = Constants.DateNullValue;

            decimal  TotalQty2 = 0;
            decimal TotalReceived2 = 0;
            decimal TotalCurrentReceived2 = 0;

            DataRow dr = PurchasedPSKU.NewRow();
            dr["SKU_ID"] = Convert.ToInt32(dc.chkNull_0(gvr.Cells[0].Text));
            dr["BATCH_NO"] = "";
            dr["FREE_SKU"] = Convert.ToDecimal(dc.chkNull_0(txtRecQty.Text));
            dr["PRICE"] = 0;
            dr["Quantity"] = Convert.ToDecimal(dc.chkNull_0(gvr.Cells[3].Text));
            dr["AMOUNT"] = 0;
            dr["TIME_STAMP"] = dtExpDate;
            dr["DISCOUNT"] = 0;
            PurchasedPSKU.Rows.Add(dr);

            ///
            /// Check if other Order Completed
            ///
           
            TotalQty += Convert.ToDecimal(dc.chkNull_0(gvr.Cells[3].Text));
            TotalReceived += Convert.ToDecimal(dc.chkNull_0(gvr.Cells[4].Text));
            TotalCurrentReceived += Convert.ToDecimal(dc.chkNull_0(txtRecQty.Text));

            TotalQty2 = Convert.ToDecimal(dc.chkNull_0(gvr.Cells[3].Text));
            TotalReceived2 = Convert.ToDecimal(dc.chkNull_0(gvr.Cells[4].Text));
            TotalCurrentReceived2 = Convert.ToDecimal(dc.chkNull_0(txtRecQty.Text));

            if (TotalCurrentReceived2 + TotalReceived2 > TotalQty2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Current received is greater than " + (TotalQty2 - TotalReceived2).ToString() + " for " + gvr.Cells[2].Text + ".');", true);
                return;
            }

        }

        if (TotalCurrentReceived < 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Current received quantity must be greater than 0.');", true);
            return;
        }

        if (PurchasedPSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            if (CalculatePurchase(DateTime.Parse(txtDate.Text), PurchasedPSKU))
            {
                if (TotalQty == TotalReceived + TotalCurrentReceived)
                {
                    mController.UpdatePurchaseOrder(Convert.ToInt64(drpDocumentNo.SelectedValue), 2);//TypeID=2 for Completed
                }
                else
                {
                    mController.UpdatePurchaseOrder(Convert.ToInt64(drpDocumentNo.SelectedValue), 0);//TypeID=0 for Open
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "GRN saved successfully.');window.location.href = 'frmPurchaseReceivedEntry.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Some error occured.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert(' At least one SKU must enter');", true);

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
        decimal mTotalAmount = 0;

        foreach (DataRow dr in PurchasedPSKU.Rows)
        {
            mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());
        }

        if (btnSaveDocument.Text == "Save Document")
        {
            bool mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), "", 2
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0
                , mTotalAmount, false, PurchasedPSKU, 0, "", int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(Session["PRINCIPAL_ID"]), Convert.ToInt64(drpDocumentNo.SelectedValue));
            return mResult;
        }
        else
        {
            bool mResult = mController.UpdatePurchaseDocument(Convert.ToInt64(Session["PurchaseID"]), int.Parse(drpDistributor.SelectedValue.ToString()), null, 2
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0
                , mTotalAmount, false, PurchasedPSKU, 0, "", int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(Session["PRINCIPAL_ID"]));
            return mResult;
        }
    }
}