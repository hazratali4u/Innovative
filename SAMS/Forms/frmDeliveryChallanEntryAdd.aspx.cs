using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmDeliveryChallanEntryAdd : System.Web.UI.Page
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
            if (Session["DCID"] != null)
            {
                this.LoadDocumentDetail(Convert.ToInt64(Session["DCID"]));
            }
            else
            {
                this.LoadCustomerData();
                this.GetDocumentNo();
                #region LoadCustomerOrderDetail

                if (drpDocumentNo.Items.Count > 0)
                {
                    DataTable dt = (DataTable)this.Session["dtDoc"];
                    DataRow[] foundRows = dt.Select("DocID  = '" + drpDocumentNo.SelectedValue + "'");
                    if (foundRows.Length > 0)
                    {
                        drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
                        txtOutletCode.Text = dt.Rows[0]["CUSTOMER_CODE"].ToString();
                        this.LoadSODetail(drpDocumentNo.SelectedValue);
                        btnSaveDocument.Text = "Save Document";
                    }
                }

                #endregion
            }
        }
    }

    /// <summary>
    /// Gets Document Nos For Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </summary>
    private void GetDocumentNo()
    {
        if (drpDistributor.Items.Count > 0)
        {
            drpDocumentNo.Items.Clear();
            OrderEntryController or = new OrderEntryController();
            if (ddlCustomer.Items.Count > 0)
            {
                DataTable dtDoc = or.GetDocumentNo(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(Session["UserID"]), 4, Convert.ToInt32(ddlCustomer.SelectedValue));
                clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtDoc, "DocID", "DocID");
                this.Session.Add("dtDoc", dtDoc);
            }
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region LoadCustomerOrderDetail

        if (drpDocumentNo.Items.Count > 0)
        {
            DataTable dt = (DataTable)this.Session["dtDoc"];
            DataRow[] foundRows = dt.Select("DocID  = '" + drpDocumentNo.SelectedValue + "'");
            if (foundRows.Length > 0)
            {
                drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
                txtOutletCode.Text = dt.Rows[0]["CUSTOMER_CODE"].ToString();
                this.LoadSODetail(drpDocumentNo.SelectedValue);
                btnSaveDocument.Text = "Save Document";
            }
        }

        #endregion
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
    private void LoadDocumentDetail(long SaleOrderID)
    {
        OrderedSKU = mController.GetGRNDetail(Session["DCID"].ToString(), Constants.IntNullValue, 3);
        if (OrderedSKU.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = OrderedSKU.Rows[0]["DISTRIBUTOR_ID"].ToString();
            drpDocumentNo.Items.Add(new ListItem(OrderedSKU.Rows[0]["PO_ID"].ToString(), OrderedSKU.Rows[0]["PO_ID"].ToString()));
            ddlCustomer.Items.Add(new ListItem(OrderedSKU.Rows[0]["Customer"].ToString(), OrderedSKU.Rows[0]["Customer"].ToString()));
            drpDocumentNo.Enabled = false;
            ddlCustomer.Enabled = false;
            btnSaveDocument.Text = "Update Document";
        }
        this.Session.Add("OrderedSKU", OrderedSKU);
        LoadGird();
    }

    private void LoadSODetail(string SaleOrderID)
    {
        OrderedSKU = mOrderController.GetSODetail(SaleOrderID, Constants.IntNullValue);        
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
            TotalOrdQty += decimal.Parse(dc.chkNull_0(dr["QUANTITY_UNIT"].ToString()));
            TotalRecQty += decimal.Parse(dc.chkNull_0(dr["Received"].ToString()));
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
        //--------------------------------------------- Insert into Deliver Chalan-----------------------------------
        PurchasedPSKU.Columns.Add("DRIVER_NAME", typeof(string));
        PurchasedPSKU.Columns.Add("VEHICLE_NO", typeof(string));
        PurchasedPSKU.Columns.Add("REFERENCES", typeof(string));
        //------------------------------------------------------------------------------------------------------------
        
        decimal TotalQty = 0;
        decimal TotalReceived = 0;
        decimal TotalCurrentReceived = 0;

        string DRIVER_NAME = txtDriverName.Text;
        string VEHICLE_NO = txtVehicleNo.Text;
        string REFERENCES = txtReference.Text;
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            DateTime dtExpDate = Constants.DateNullValue;

            decimal TotalQty2 = 0;
            decimal TotalReceived2 = 0;
            decimal TotalCurrentReceived2 = 0;
            if (Convert.ToDecimal(dc.chkNull_0(txtRecQty.Text)) > 0)
            {
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
            }
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Current delivered is greater than " + (TotalQty2 - TotalReceived2).ToString() + " for " + gvr.Cells[2].Text + ".');", true);
                return;
            }

        }

        if (TotalCurrentReceived < 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Current delivered quantity must be greater than 0.');", true);
            return;
        }

        if (PurchasedPSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();

            // Calculate insert the data into the Deliver Chalan Table and one Other
            if (CalculatePurchase(DateTime.Parse(txtDate.Text), PurchasedPSKU,DRIVER_NAME,VEHICLE_NO,REFERENCES))
            {
                if (TotalQty == TotalReceived + TotalCurrentReceived)
                {
                    mOrderController.UpdateSaleOrder(Convert.ToInt64(drpDocumentNo.SelectedValue), Convert.ToInt32(Session["UserID"]), 2);//STATUS_ID=2 for Completed
                }
                else
                {
                    mOrderController.UpdateSaleOrder(Convert.ToInt64(drpDocumentNo.SelectedValue), Convert.ToInt32(Session["UserID"]), 111);//STATUS_ID=111 for Open
                }
                this.LoadCustomerData();
                this.GetDocumentNo();

                #region LoadCustomerOrderDetail

                if (drpDocumentNo.Items.Count > 0)
                {
                    DataTable dt = (DataTable)this.Session["dtDoc"];
                    DataRow[] foundRows = dt.Select("DocID  = '" + drpDocumentNo.SelectedValue + "'");
                    if (foundRows.Length > 0)
                    {
                        drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
                        txtOutletCode.Text = dt.Rows[0]["CUSTOMER_CODE"].ToString();
                        this.LoadSODetail(drpDocumentNo.SelectedValue);
                        btnSaveDocument.Text = "Save Document";
                    }
                }

                #endregion

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "DC saved successfully.');window.location.href = 'frmDeliveryChallanEntry.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
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
    private bool CalculatePurchase(DateTime MWorkDate, DataTable PurchasedPSKU,string driverName,string vehicleNo,string reference)
    {
        decimal mTotalAmount = 0;

        foreach (DataRow dr in PurchasedPSKU.Rows)
        {
            mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());
        }

        if (btnSaveDocument.Text == "Save Document")
        {
            bool mResult = mController.InsertPurchaseDocument(driverName,vehicleNo,reference, int.Parse(drpDistributor.SelectedValue.ToString()), "", 3
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0
                , mTotalAmount, false, PurchasedPSKU, 0, "", int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(ddlCustomer.SelectedValue), Convert.ToInt64(drpDocumentNo.SelectedValue));
            return mResult;
        }
        else
        {
            bool mResult = mController.UpdatePurchaseDocument(driverName, vehicleNo, reference, Convert.ToInt64(Session["DCID"]), int.Parse(drpDistributor.SelectedValue.ToString()), null, 3
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0
                , mTotalAmount, false, PurchasedPSKU, 0, "", int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue);
            return mResult;
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetDocumentNo();
        this.drpDocumentNo_SelectedIndexChanged(null,null);
    }

    /// <summary>
    /// Loads Customers To Customer ListBox
    /// </summary>
    private void LoadCustomerData()
    {
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectSaleOrderCustomer(int.Parse(drpDistributor.SelectedValue), 1, Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.ddlCustomer, dtCustomer, "CUSTOMER_ID", "CUSTOMER_DETAIL", true);
        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCustomerData();
        this.GetDocumentNo();
    }
}