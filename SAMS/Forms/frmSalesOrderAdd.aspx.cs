using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Collections.Generic;

/// <summary>
/// Form To Take Order, Invoice And Sale Return(Step2)
/// </summary>
public partial class Forms_frmSalesOrderAdd : System.Web.UI.Page
{
    #region Variables

    DataTable PurchaseSKU;
    private static int mCustomerTypeId;
    private static int mCustomerVolClassId;
    private int mTownId;
    private static int RowId;
    private static int PrivouseQty;

    private long totalQuantity=0;
    private long totalAmount=0; 
    #endregion

    /// <summary>
    /// Page_Load Function Populates All Combos, Grids And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            this.GetMaxInvoiceNo(2);
            this.LoadDistributor();
            this.LoadCustomerData();
            this.LoadSKUDetail();
            this.CreatTable();
            ScriptManager.GetCurrent(Page).SetFocus(txtBillBookNo);            
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            cbGST.Visible = false;
            lblBillBook.Text = "Order No:";
        }
    }

    /// <summary>
    /// Creates Datatable For Order, Invoice And Sale Return
    /// </summary>
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_UNIT", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("Total", typeof(decimal));        
        PurchaseSKU.Columns.Add("GTotal",typeof(decimal));
        PurchaseSKU.Columns.Add("GST_RATE", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("EXTRA_DISCOUNT", typeof(decimal));        
        PurchaseSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("NET_AMOUNT", typeof(decimal));
        this.Session.Add("PurchaseSKU", PurchaseSKU);
    }
        
    /// <summary>
    /// Loads Customers To Customer ListBox
    /// </summary>
    private void LoadCustomerData()
    {
        if (drpDistributor.Items.Count > 0)
        {
            ListCustomer.Items.Clear();
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue), 1, Constants.IntNullValue,Constants.IntNullValue);
            clsWebFormUtil.FillListBox(this.ListCustomer, dtCustomer, "CUSTOMER_DETAIL", "CUSTOMER_DETAIL", true);
            this.Session.Add("dtCustomer", dtCustomer);
        }
        else
        {
            ListCustomer.Items.Clear();
        }
    }

    /// <summary>
    /// Loads SKU Data To SKU ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 0, int.Parse(drpDistributor.SelectedValue), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 3, DateTime.Parse(txtDate.Text));
        clsWebFormUtil.FillListBox(this.lstCode, Dtsku_Price, "SkuPriceDetail", "SkuPriceDetail", true);
        this.Session.Add("Dtsku_Price", Dtsku_Price);
        hfSKUDetail.Value = GetJson(Dtsku_Price);
    }
    
    /// <summary>
    /// Loads Order To Order Grid
    /// </summary>
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();

        int Qty = 0;
        decimal Amt = 0;
        
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            Qty += Convert.ToInt32(dr["QUANTITY_UNIT"]);
            Amt += Convert.ToDecimal(dr["AMOUNT"]);
        }

        txtTotalQuantity.Text = Qty.ToString();
        txtTotalAmount.Text = Amt.ToString();
    }
        
    /// <summary>
    /// Sets Order SKU For Edit. This Function Runs When An Existing Order SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text.Replace("&quot;","\"");
        txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        PrivouseQty = int.Parse(GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text);        
        txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;       
        txtskuCode.Enabled = false;
        btnSave.Text = "Update SKU";

    }

    /// <summary>
    /// Deletes An SKU From Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        PurchaseSKU.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.LoadGird();
        btnCancel.Enabled = true;        
    }
        
    /// <summary>
    /// Verifies Customer Code
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool FindCustomer()
    {
        DataTable dtCustomer = (DataTable)this.Session["dtCustomer"];
        DataRow[] foundRows = dtCustomer.Select("CUSTOMER_CODE  = '" + txtOutletCode.Text.Trim() + "'");
        if (foundRows.Length > 0)
        {
            this.Session.Add("CUSTOMER_ID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));
            mCustomerTypeId = int.Parse(foundRows[0]["CHANNEL_TYPE_ID"].ToString());
            mCustomerVolClassId = int.Parse(foundRows[0]["VOLUME_CLASS_ID"].ToString());
            //mTownId = int.Parse(foundRows[0]["TOWN_ID"].ToString());
            return true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Please Select the Customer...!')", true);
            return false;

        }
        
    }

    /// <summary>
    /// Checks SKU in Order Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool CheckDublicateSKU()
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_CODE  = '" + txtskuCode.Text + "'");
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
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="CValue">Value</param>
    private void EnableDisableController(bool CValue)
    {
        DataControl dc = new DataControl();
         
        if (CValue == true)
        {
            //if (decimal.Parse(dc.chkNull_0(txtGrossAmount.Text)) > 0)
            //{               
            //    txtOutletCode.Enabled = true;
            //    txtOutletName.Enabled = true;
            //    cbGST.Enabled = true;
            //}
        }
        else
        {            
            txtOutletCode.Enabled = true;
            txtOutletName.Enabled = true;
            cbGST.Enabled = false;
        }
    }
    
    /// <summary>
    /// Calcualtes Discount And Free SKU For Order
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {

#region Code for Calculate
        //if (FindCustomer())
        //{
        //    decimal ObjGrossSales = 0;
        //    decimal ObjExtraDiscount = 0;
        //    decimal ObjTotalGST = 0;

        //    DataControl dc = new DataControl();
        //    DataTable dt = (DataTable)this.Session["PurchaseSKU"];
            
        //    if (dt.Rows.Count > 0 && txtOutletCode.Text.Length > 0)
        //    {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["QUANTITY_UNIT"].ToString())) * decimal.Parse(dc.chkNull_0(dr["UNIT_PRICE"].ToString()));
        //                ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
        //                ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
        //            }

        //        btnSaveOrder.Enabled = true;                
        //    }
        //}
#endregion
       
    }
    
    /// <summary>
    /// Adds/Updates Row To Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_CODE  = '" + txtskuCode.Text + "'");
        decimal mTradePrice = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));

        int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));
        int CurrentStock = CheckStockStatus(int.Parse(dc.chkNull_0(foundRows[0]["SKU_ID"].ToString())));

        if (Convert.ToDecimal(dc.chkNull_0(txtUnitRate.Text)) < 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Rate must be greater than 0.');", true);
            return;
        }

        if (btnSave.Text == "Add Sku")
        {
            if (CheckDublicateSKU())
            {
                //if (CurrentStock == -1)
                //{
                DataRow dr = PurchaseSKU.NewRow();
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                dr["UNIT_PRICE"] = Convert.ToDecimal(dc.chkNull_0(txtUnitRate.Text)).ToString();

                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                    dr["EXTRA_DISCOUNT"] = 0;
                    dr["BATCH_NO"] = "T";

                }
                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["EXTRA_DISCOUNT"] = 0;
                    dr["GST_RATE"] = 0;
                    dr["BATCH_NO"] = "R";

                }
                else
                {
                    dr["AMOUNT"] = Convert.ToDecimal(dr["UNIT_PRICE"]) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["EXTRA_DISCOUNT"] = 0;
                    //dr["Total"] = Math.Round(((Convert.ToDecimal(dc.chkNull_0(txtQuantity.Text)) * Convert.ToDecimal(dc.chkNull_0(txtUnitRate.Text))) - Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text))), 2).ToString();
                    dr["Total"] = Math.Round(((Convert.ToDecimal(dc.chkNull_0(txtQuantity.Text)) * Convert.ToDecimal(dc.chkNull_0(txtUnitRate.Text))) - Convert.ToDecimal(0)), 2).ToString();
                    if (cbGST.Checked)
                    {
                        dr["GST_RATE"] = 0;
                        //dr["GST_AMOUNT"] = Math.Round((Convert.ToDecimal(dr["Total"]) * Convert.ToDecimal(dc.chkNull_0(txtGST.Text)) / 100), 2).ToString();
                        dr["GST_AMOUNT"] = Math.Round((Convert.ToDecimal(dr["Total"]) * Convert.ToDecimal(0) / 100), 2).ToString();
                    }
                    else
                    {
                        dr["GST_RATE"] = 0;
                        dr["GST_AMOUNT"] = 0;
                    }
                    dr["GTotal"] = Math.Round((Convert.ToDecimal(dr["Total"]) + Convert.ToDecimal(dr["GST_AMOUNT"])), 2).ToString();
                    dr["BATCH_NO"] = "E";
                }

                dr["STANDARD_DISCOUNT"] = 0;
                dr["NET_AMOUNT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
                PurchaseSKU.Rows.Add(dr);                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('SKU Already Exists')", true);
            }
        }
        else
        {
            DataRow dr = PurchaseSKU.Rows[RowId];
            dr["SKU_ID"] = foundRows[0]["SKU_ID"];
            dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
            dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
            dr["EXTRA_DISCOUNT"] = Convert.ToDecimal(0);
            dr["EXTRA_DISCOUNT"] = Convert.ToDecimal(0);
            dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
            dr["UNIT_PRICE"] = Convert.ToDecimal(dc.chkNull_0(txtUnitRate.Text)).ToString();
            dr["AMOUNT"] = Convert.ToDecimal(dr["UNIT_PRICE"]) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());

            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
            {
                dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["EXTRA_DISCOUNT"] = 0;
                dr["BATCH_NO"] = "T";

            }
            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["EXTRA_DISCOUNT"] = Convert.ToDecimal(0);
                dr["EXTRA_DISCOUNT"] = Convert.ToDecimal(0);
                dr["GST_RATE"] = 0;
                dr["BATCH_NO"] = "R";

            }
            else
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["EXTRA_DISCOUNT"] = Convert.ToDecimal(0);
                dr["Total"] = Math.Round(((Convert.ToDecimal(dc.chkNull_0(txtQuantity.Text)) * Convert.ToDecimal(dc.chkNull_0(txtUnitRate.Text))) - Convert.ToDecimal(0)), 2).ToString();
                if (cbGST.Checked)
                {
                    dr["GST_RATE"] = Convert.ToDecimal(0);
                    dr["GST_AMOUNT"] = Math.Round((Convert.ToDecimal(dr["Total"]) * Convert.ToDecimal(0) / 100), 2).ToString();
                }
                else
                {
                    dr["GST_RATE"] = 0;
                    dr["GST_AMOUNT"] = 0;
                }
                dr["GTotal"] = Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString();
            }

            dr["STANDARD_DISCOUNT"] = 0;
            dr["NET_AMOUNT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));

        }

        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.EnableDisableController(false);
        this.ClearAll();
        this.LoadGird();        
        ScriptManager.GetCurrent(Page).SetFocus(txtskuCode);
    }

    /// <summary>
    /// Saves/Updates Order, Invoice And Sale Return
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        if (FindCustomer())
        {
            string ManualID = null;
            if (txtBillBookNo.Text.Trim().Length > 0)
            {
                ManualID = txtBillBookNo.Text.ToUpper();
            }
            
            DataControl DC = new DataControl();
            PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
            OrderEntryController mOrderController = new OrderEntryController();

            int OrderBookerID = 0;
            if (cbGST.Checked)
            {
                OrderBookerID = 1;
            }
            if (btnSaveOrder.Text == "Save")
            {
                bool IsValidInsert = mOrderController.Add_Order(int.Parse(drpDistributor.SelectedValue), ManualID, mTownId, 1, 0, long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), OrderBookerID, 1, int.Parse(RblPayMode.SelectedValue.ToString()),
                            decimal.Parse(DC.chkNull_0(txtTotalAmount.Text)), 0, 0, 0, decimal.Parse(DC.chkNull_0(txtTotalAmount.Text)), 0, Constants.Order_Pending_Id, PurchaseSKU, null, int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(txtDate.Text), 0, 0);
                if (IsValidInsert)
                {
                    this.ClearMasterALL();
                    this.Session.Remove("hfBillBookNo");
                    GetMaxInvoiceNo(2);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Order saved successfully.');", true);
                }
            }
            else
            {
                bool IsValidInsert = mOrderController.Update_Order(Convert.ToInt64(ddlDocumentNo.SelectedValue), DateTime.Parse(txtDate.Text), int.Parse(drpDistributor.SelectedValue), ManualID, long.Parse(this.Session["CUSTOMER_ID"].ToString()), 1,
                   decimal.Parse(DC.chkNull_0(txtTotalAmount.Text)), 0, 0, 0, decimal.Parse(DC.chkNull_0(txtTotalAmount.Text)), 0, int.Parse(RblPayMode.SelectedValue.ToString()), PurchaseSKU, int.Parse(this.Session["UserId"].ToString()), 0, 0, 0);
                if (IsValidInsert)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Order updated successfully.');", true);
                }
            }            
        }
    }

    /// <summary>
    /// Clears Some Of Controls
    /// </summary>
    private void ClearAll()
    {
        txtskuCode.Text = "";
        txtskuName.Text = "";
        txtUnitRate.Text = "";
        txtQuantity.Text = "";
        txtAmount.Text = "";
        btnSave.Text = "Add Sku";        
        btnCancel.Enabled = true;
        txtskuCode.Enabled = true;
        PrivouseQty = 0;

    }

    /// <summary>
    /// Clears All Controls
    /// </summary>
    private void ClearMasterALL()
    {
        this.EnableDisableController(true);
        this.Session.Remove("PurchaseSKU");
        this.CreatTable();
        txtOutletCode.Text = "";
        txtOutletName.Text = "";
        this.LoadGird();
        RowId = 0;
        mCustomerTypeId = 0;
        mCustomerVolClassId = 0;        
        if (btnSaveOrder.Text != "Sale Return")
        {
            ScriptManager.GetCurrent(Page).SetFocus(txtBillBookNo);
        }
        else
        {
            ScriptManager.GetCurrent(Page).SetFocus(txtOutletCode);
        }
    }

    public string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    /// <summary>
    /// Checks Bill Book No in System
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private void GetMaxInvoiceNo(int p_Type)
    {
        OrderEntryController OEC = new OrderEntryController();
        DataTable dtMaxInvoiceNo = OEC.GetMaxInvoiceNoGstNonGst(p_Type, 0);
        txtBillBookNo.Text = dtMaxInvoiceNo.Rows[0]["MaxInvoiceNo"].ToString();
    }

    /// <summary>
    /// Loads Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddlDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDocumentNo.Items.Count > 0)
        {
            DataTable dt = (DataTable)this.Session["dtDoc"];
            DataRow[] foundRows = dt.Select("DocID  = '" + ddlDocumentNo.SelectedValue + "'");
            if (foundRows.Length > 0)
            {                
                drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
                txtOutletCode.Text = dt.Rows[0]["CUSTOMER_CODE"].ToString();
                txtOutletName.Text = dt.Rows[0]["CUSTOMER_NAME"].ToString();                
                this.LoadDocumentDetail(Convert.ToInt64(ddlDocumentNo.SelectedValue));
                btnSaveOrder.Text = "Update";
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ClearAll();
        ClearMasterALL();
        if (ddlDocumentNo.Visible)
        {
            ddlDocumentNo.Visible = false;
            txtBillBookNo.Visible = true;
            btnSaveOrder.Text = "Save";
            GetMaxInvoiceNo(2);
        }
        else
        {
            LoadDocumentNo(4);
            ddlDocumentNo_SelectedIndexChanged(null, null);
            ddlDocumentNo.Visible = true;
            txtBillBookNo.Visible = false;
        }
    }

    /// <summary>
    /// Loads Document Nos To Docuemnt No Combo
    /// </summary>
    private void LoadDocumentNo(int pType)
    {
        OrderEntryController or = new OrderEntryController();
        this.ddlDocumentNo.Items.Clear();
        DataTable dtDoc = or.GetDocumentNo(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(Session["UserID"]), pType);
        clsWebFormUtil.FillDropDownList(this.ddlDocumentNo, dtDoc, "DocID", "DocNo");
        this.Session.Add("dtDoc", dtDoc);
    }

    /// <summary>
    /// Loads Order And Free SKUS Grids
    /// </summary>
    /// <param name="OrderId">Order</param>
    private void LoadDocumentDetail(long DocID)
    {
        OrderEntryController ord = new OrderEntryController();
        PurchaseSKU = ord.GetDocumentDetail(DocID,3);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.LoadGird();
    }

    /// <summary>
    /// Checks SKU Stock
    /// </summary>
    /// <param name="SKU_ID">SKU</param>
    /// <returns>SKU Stock as Integer</returns>
    private int CheckStockStatus(int SKU_ID)
    {
        PhaysicalStockController mController = new PhaysicalStockController();
        DataTable dt = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue.ToString()), SKU_ID, "", DateTime.Parse(txtDate.Text));
        if (dt.Rows.Count > 0)
        {
            if (int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty >= int.Parse(txtQuantity.Text))
            {
                return -1;
            }
            else
            {
                return int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty;
            }
        }
        return 0;
    }

    #region Step1

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Routes To Route Combo, Orderbookers To Orderbooker Combo And Deliverymen To Deliveryman Comob
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCustomerData();
        this.LoadSKUDetail();
        this.GetMaxInvoiceNo(1);
    }

    #endregion        
    
    protected void cbGST_CheckedChanged(object sender, EventArgs e)
    {
        GetMaxInvoiceNo(0);
    }
        
    protected void btnTotal_Click(object sender, EventArgs e)
    {
        int totalQty = 0;
        int totalAmnt = 0;
        totalQty = Convert.ToInt32(txtQuantity);
        totalAmount = Convert.ToInt32(txtAmount);
        totalQuantity = totalQty;
        totalAmount = totalAmnt;
        txtTotalQuantity.Text = Convert.ToString(totalQuantity);
        txtTotalAmount.Text = Convert.ToString(txtTotalAmount);
    }
}
