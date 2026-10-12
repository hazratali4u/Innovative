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
public partial class Forms_frmOrderEntry : System.Web.UI.Page
{
    #region Variables

   DataTable PurchaseSKU;
    private static int mCustomerTypeId;
    private static int mCustomerVolClassId;
    private int mTownId;
    private static int RowId;
    private static int PrivouseQty;

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
            this.GetMaxInvoiceNo(1);
            this.LoadDistributor();
            this.LoadCustomerData();
            this.LoadOrderCombo();
            this.CreatTable();
            this.LoadDC();
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
        PurchaseSKU.Columns.Add("QUANTITY_UNIT", typeof(decimal));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("Total", typeof(decimal));
        PurchaseSKU.Columns.Add("GTotal", typeof(decimal));
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
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectSaleOrderCustomer(int.Parse(drpDistributor.SelectedValue), 0, Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.ddlOrderedCustomer, dtCustomer, "CUSTOMER_ID", "CUSTOMER_DETAIL", true);
        }
    }

    private void LoadOrderCombo()
    {
        if (drpDistributor.Items.Count > 0)
        {
            ddlCustomerOrder.Items.Clear();
            OrderEntryController or = new OrderEntryController();
            this.ddlDocumentNo.Items.Clear();
            if (ddlOrderedCustomer.Items.Count > 0)
            {
                DataTable dtDoc = or.GetDocumentNo(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(Session["UserID"]), 5, Convert.ToInt32(ddlOrderedCustomer.SelectedValue));
                clsWebFormUtil.FillDropDownList(this.ddlCustomerOrder, dtDoc, "DocID", "DocID");
                this.Session.Add("dtDoc", dtDoc);
            }
        }
    }       

    /// <summary>
    /// Loads Order To Order Grid
    /// </summary>
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        for (int i = 0; i < PurchaseSKU.Rows.Count; i++)
        {
            if (Convert.ToInt32(PurchaseSKU.Rows[i][7])==0)
            {
                PurchaseSKU.Rows.Remove(PurchaseSKU.Rows[i]);
                i--;
            }
        }
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();        
    }

    /// <summary>
    /// Sets Order SKU For Edit. This Function Runs When An Existing Order SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        //txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
        //txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        //txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        //PrivouseQty = int.Parse(GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text);        
        //txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        //txtDiscount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        //txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        //txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[6].Text;
        //txtGST.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[7].Text;
        //txtGSTAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[8].Text;
        //txtSKUTotal.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[9].Text;
        //txtskuCode.Enabled = false;
        //btnSave.Text = "Update SKU";

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
    }
    
    /// <summary>
    /// Checks Bill Book No in System
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>   
    private bool IsBillBookNoExist()
    {
        bool flag = false;
        //if (txtBillBookNo.Text.Trim().Length > 0)
        //{
        //    OrderEntryController OEC = new OrderEntryController();            
        //    DataTable dtBillBookNo2 = OEC.SelectBillBookNo(Convert.ToInt32(drpDistributor.SelectedValue), txtBillBookNo.Text, 1);
        //    if (dtBillBookNo2.Rows.Count > 0)
        //    {
        //        flag = true;
        //    }
        //}
        return flag;
    }
             
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        if (GrdPurchase.Rows.Count > 0 )
        {
            string ManualID = null;
            if (txtBillBookNo.Text.Trim().Length > 0)
            {
                ManualID = txtBillBookNo.Text.ToUpper();
            }
            DataControl DC = new DataControl();
            CreatTable();
            PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
            OrderEntryController mOrderController = new OrderEntryController();

            int OrderBookerID = 0;//Using Order Booker Clumn for IsGST Invoice(0 for Non Gst Invoice and 1 for GST Invoice)
            if (cbGST.Checked)
            {
                OrderBookerID = 1;
            }
            if (btnSaveOrder.Text == "Save")
            {                
                foreach (GridViewRow gvr in GrdPurchase.Rows)
                {                    
                    TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
                    TextBox txtDiscount = gvr.FindControl("txtDiscount") as TextBox;
                    TextBox txtRate = gvr.FindControl("txtRate") as TextBox;
                    TextBox txtGross = gvr.FindControl("txtGross") as TextBox;
                    TextBox txtGSTRate = gvr.FindControl("txtGSTRate") as TextBox;
                    TextBox txtGTotal = gvr.FindControl("txtGTotal") as TextBox;

                    if (Convert.ToDecimal(DC.chkNull_0(txtRate.Text)) < 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Rate must be greater than 0.');", true);
                        return;
                    }

                    DataRow dr = PurchaseSKU.NewRow();
                    dr["SKU_ID"] = gvr.Cells[0].Text;
                    //dr["QUANTITY_UNIT"] = Convert.ToDecimal(DC.chkNull_0(gvr.Cells[3].Text));
                    dr["QUANTITY_UNIT"] = Convert.ToDecimal(DC.chkNull_0(txtRecQty.Text));
                    dr["UNIT_PRICE"] = Convert.ToDecimal(DC.chkNull_0(txtRate.Text));
                    dr["AMOUNT"] = Convert.ToDecimal(DC.chkNull_0(txtGross.Text));
                    dr["EXTRA_DISCOUNT"] = Convert.ToDecimal(DC.chkNull_0(txtDiscount.Text));
                    if (cbGST.Checked)
                    {
                        dr["GST_RATE"] = Convert.ToDecimal(DC.chkNull_0(txtGSTRate.Text));
                        dr["GST_AMOUNT"] = Math.Round(((Convert.ToDecimal(dr["AMOUNT"]) - Convert.ToDecimal(dr["EXTRA_DISCOUNT"])) * Convert.ToDecimal(DC.chkNull_0(txtGSTRate.Text)) / 100), 2);
                    }
                    else
                    {
                        dr["GST_RATE"] = 0;
                        dr["GST_AMOUNT"] = 0;
                    }
                    dr["BATCH_NO"] = "E";
                    dr["STANDARD_DISCOUNT"] = 0;
                    dr["NET_AMOUNT"] = Convert.ToDecimal(DC.chkNull_0(txtGTotal.Text));
                    PurchaseSKU.Rows.Add(dr);                    
                }

                DataTable dtDCIDs = new DataTable();
                dtDCIDs.Columns.Add("DC_ID", typeof(long));
                dtDCIDs.Columns.Add("DC_ID_MANUAL", typeof(long));

                foreach (ListItem li in cbDC.Items)
                {
                    if (li.Selected)
                    {
                        DataRow dr = dtDCIDs.NewRow();
                        dr["DC_ID"] = Convert.ToInt32(li.Value);
                        dr["DC_ID_MANUAL"] = Convert.ToInt32(li.Text);
                        dtDCIDs.Rows.Add(dr);
                    }
                }

                if (!IsBillBookNoExist())// aLWAYS TRUE THER IS NO LOGIC IMPLIMENT IN BILL BOOK METHOD
                {
                    bool IsValidInsert = mOrderController.Add_Invoice(int.Parse(drpDistributor.SelectedValue), ManualID, mTownId, 1, 0, long.Parse(ddlOrderedCustomer.SelectedValue), long.Parse(ddlOrderedCustomer.SelectedValue), OrderBookerID, 1, Convert.ToInt64(ddlCustomerOrder.SelectedValue),
                    decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), 0, decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, int.Parse(RblPayMode.SelectedValue.ToString()),
                    PurchaseSKU, null, int.Parse(this.Session["UserId"].ToString()), 0, DateTime.Parse(txtDate.Text), 0, 0, Convert.ToInt64(ddlCustomerOrder.SelectedValue), dtDCIDs);
                    if (IsValidInsert)
                    {          
                        this.GetMaxInvoiceNo(1);
                        this.LoadCustomerData();
                        this.LoadOrderCombo();
                        ClearMasterALL();
                        this.LoadDC();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('This Invoice No already exist,Kindly enter different Invoice No');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No product selected.');", true);
        }
    }
    
    /// <summary>
    /// Clears All Controls
    /// </summary>
    private void ClearMasterALL()
    {
        this.Session.Remove("PurchaseSKU");
        this.CreatTable();
        txtOutletCode.Text = "";
        this.LoadGird();
        txtGrossAmount.Text = "";
        numtxtTotalExtraDiscnt.Text = "";
        numTxtTotalGST.Text = "";
        numTxtTotlAmnt.Text = "";        
        RowId = 0;
        mCustomerTypeId = 0;
        mCustomerVolClassId = 0;        
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
        int p_Gst;

        if (cbGST.Checked)
        {
            p_Gst = 1;
        }
        else
        {
            p_Gst = 0;
        }
        OrderEntryController OEC = new OrderEntryController();
        DataTable dtMaxInvoiceNo = OEC.GetMaxInvoiceNoGstNonGst(p_Type, p_Gst);
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

                txtBillBookNo.Text = dt.Rows[0]["MANUAL_INVOICE_ID"].ToString();
                RblPayMode.SelectedValue = dt.Rows[0]["PayMode"].ToString();
                if (dt.Rows[0]["ORDERBOOKER_ID"] == "1")
                {
                    cbGST.Checked = true;
                }
                else
                {
                    cbGST.Checked = false;
                }
                btnSaveOrder.Text = "Save";
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ClearMasterALL();
        if (ddlDocumentNo.Visible)
        {
            ddlDocumentNo.Visible = false;
            txtBillBookNo.Visible = true;
            btnSaveOrder.Text = "Save";
            GetMaxInvoiceNo(1);
        }
        else
        {
            LoadDocumentNo(1);
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
        this.GetMaxInvoiceNo(1);
    }

    protected void cbGST_CheckedChanged(object sender, EventArgs e)
    {
        GetMaxInvoiceNo(1);
    }

    protected void txtUnitRate_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlOrderedCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlCustomerOrder.Items.Clear();
        this.LoadOrderCombo();
        this.ddlCustomerOrder_SelectedIndexChanged(null, null);
    }

    protected void ddlCustomerOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();
        this.LoadDC();
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

    private void LoadDC()
    {
        PurchaseController mController = new PurchaseController();
        cbDC.Items.Clear();
        if (ddlCustomerOrder.Items.Count > 0)
        {
            DataTable dt = mController.GetGRNNo(Constants.LongNullValue, long.Parse(ddlCustomerOrder.SelectedValue),3);
            clsWebFormUtil.FillListBox(this.cbDC, dt, 0, 2);
            if (cbDC.Items.Count > 0)
            {
                btnShow.Visible = true;
                lblDC.Visible = true;
            }
            else
            {
                btnShow.Visible = false;
                lblDC.Visible = false;
            }
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.LoadDocumentDetail();
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        PurchaseController mPurchase = new PurchaseController();
        string DCNos = string.Empty;
        foreach (ListItem li in cbDC.Items)
        {
            if (li.Selected)
            {
                DCNos += li.Value + ",";
            }
        }
        if (ddlCustomerOrder.Items.Count > 0)
        {
            PurchaseSKU = mPurchase.GetGRNDetail(DCNos, Constants.IntNullValue, 3);
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }
}

