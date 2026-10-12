using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmPurchaseOrderEntryAdd : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable PurchaseSKU;

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
            this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadSKUDetail();
            this.CreatTable();
            this.GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");                        
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("PURCHASE_ORDER_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("Quantity", typeof(decimal));
        PurchaseSKU.Columns.Add("PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        this.Session.Add("PurchaseSKU", PurchaseSKU);

    }

    /// <summary>
    /// Gets Document Nos For Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo(Constants.LongNullValue, int.Parse(drpDistributor.SelectedValue), 0, Convert.ToDateTime(txtDate.Text));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            this.CreatTable();
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            this.ClearAll();
            txtExchangeRate.Text = "";
            txtRefNo.Text = "";
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
        }
        else
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            this.LoadDocumentDetail();
            this.LoadSKUDetail();
        }
    }

    /// <summary>
    /// Loads Principals To Principal Comb
    /// </summary>
    private void LoadPrincipal()
    {
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(txtDate.Text));
        clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        this.LoadSKUDetail();
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
        DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo(Convert.ToInt64(drpDocumentNo.SelectedValue), Convert.ToInt32(drpDistributor.SelectedValue), 0,Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            txtRefNo.Text = dt.Rows[0]["REF_NO"].ToString();
            ddlCurrency.SelectedValue = dt.Rows[0]["CURRENCY_ID"].ToString();
            txtExchangeRate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
            PurchaseSKU = mPurchase.SelectPurchaseOrderDetail(long.Parse(dt.Rows[0][0].ToString()), Convert.ToInt32(drpDistributor.SelectedValue));
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        decimal TotalValue = 0;
        decimal TotalAmountFc = 0;
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            TotalValue += decimal.Parse(dr["Quantity"].ToString());
            TotalAmountFc += Convert.ToDecimal(dr["AMOUNT"]);

        }

        txtTotalQuantity.Text = TotalValue.ToString();
        txtAmountFC.Text = Math.Round(TotalAmountFc, 3).ToString();
        txtAmountRs.Text = Math.Round((TotalAmountFc * Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text))), 3).ToString();
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
        txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        txtskuCode.Enabled = false;
        txtQuantity.Focus();
        btnSave.Text = "Update Sku";

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            PurchaseSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.LoadGird();
        }
    }

    /// <summary>
    /// Loads SKU Prices
    /// </summary>
    private void LoadSKUDetail()
    {
        SkuController SKUCtl = new SkuController();
        if (drpPrincipal.Items.Count > 0)
        {
            DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.RawMetarial, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 3, Constants.DateNullValue);
            clsWebFormUtil.FillListBox(lstCode, Dtsku_Price, 10, 10, true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
    }

    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_CODE  = '" + txtskuCode.Text + "'");
        
        if (Convert.ToDecimal(dc.chkNull_0(txtRate.Text)) < 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Rate must be greater than 0.');", true);
            return;
        }
        if (foundRows.Length > 0)
        {
            if (btnSave.Text == "Add Sku")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));
                    dr["Quantity"] = decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["AMOUNT"] = decimal.Parse(txtRate.Text) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    PurchaseSKU.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('  " + txtskuCode.Text + " Already Exists ');", true);
                    txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    return;
                }
            }
            else if (btnSave.Text == "Update Sku")
            {
                DataRow dr = PurchaseSKU.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));
                dr["Quantity"] = decimal.Parse(txtQuantity.Text);
                dr["AMOUNT"] = decimal.Parse(txtRate.Text) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
            }
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.ClearAll();
            this.LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(txtskuCode);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Wrong SKU Select');", true);

        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {        
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            if (CalculatePurchase(DateTime.Parse(txtDate.Text)))
            {
                PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
                PurchaseSKU.Rows.Clear();
                this.Session.Add("PurchaseSKU", PurchaseSKU);
                this.LoadGird();
                this.GetDocumentNo();
                drpDistributor.Enabled = true;
                drpPrincipal.Enabled = true;
                this.ClearAll();
                txtExchangeRate.Text = "";
                txtRefNo.Text = "";
                DisAbaleOption(false);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert(' At least one SKU must enter');", true);

        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        this.CreatTable();
        this.LoadGird();
        this.ClearAll();
        txtRefNo.Text = "";
        txtExchangeRate.Text = "";
    }

    /// <summary>
    /// Checkes Duplicate SKUS in Document Detail Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
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
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool CalculatePurchase(DateTime MWorkDate)
    {
        decimal mTotalAmount = 0;

        PurchaseController mController = new PurchaseController();
        DataTable dtPurchaseDetail = (DataTable)this.Session["PurchaseSKU"];
        foreach (DataRow dr in dtPurchaseDetail.Rows)
        {
            mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());

        }

        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            bool mResult = mController.InsertPurchaseOrder(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), txtRefNo.Text, 0
            , Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text)), MWorkDate, mTotalAmount, int.Parse(this.Session["UserId"].ToString()), dtPurchaseDetail);
            return mResult;
        }
        else
        {
            bool mResult = mController.UpdatePurchaseOrderDetail(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtRefNo.Text, Convert.ToInt32(ddlCurrency.SelectedValue)
            , mTotalAmount, Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text)), int.Parse(this.Session["UserId"].ToString()), dtPurchaseDetail);
            return mResult;
        }
    }

    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="IsDisable">bool</param>
    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
            txtRefNo.Enabled = false;
            ddlCurrency.Enabled = false;
            txtExchangeRate.Enabled = false;
        }
        else
        {
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
            txtRefNo.Enabled = true;
            ddlCurrency.Enabled = true;
            txtExchangeRate.Enabled = true;
        }
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtskuCode.Text = "";
        txtskuName.Text = "";
        txtQuantity.Text = "";
        txtRate.Text = "0.00";
        txtAmount.Text = "0.00";
        txtskuCode.Enabled = true;
        btnSave.Text = "Add Sku";
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        this.GetDocumentNo();
    }
}