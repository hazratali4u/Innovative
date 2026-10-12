using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Add, Edit Vochers
/// </summary>
public partial class Forms_frmVoucherEntry : System.Web.UI.Page
{
    DataTable dtVoucher;
    private static int RowId = -1;
    readonly AccountHeadController _mAccountController = new AccountHeadController();
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadAccountDetail();  
            LoadAccountHeads();
            CreatTable();
            LoadAccountHead();
            LoadAccountNos();
            LoadChequeNos();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            txtDueDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            RowId = -1;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;
        }
    }

    /// <summary>
    /// Loads Account Heads To Bank Combo
    /// </summary>
    private void LoadAccountHead()
    {
        Configuration.GetAccountHead();
        
        DataTable dt = _mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
        clsWebFormUtil.FillDropDownList(drpBanks, dt, 0, 4, true);
    }
    
    /// <summary>
    /// Creates Datatable To Hold Voucher Data
    /// </summary>
    private void CreatTable()
    {
        dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("InvoiceNo", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("Principal", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);
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
    /// Loads Account Heads To ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
        // Load Account Head 
       
        DataTable dtHead = _mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME");
        this.Session.Add("dtHead", dtHead);
    }
    private void LoadAccountHeads()
    {
        if (ddlAccountHead.Items.Count <= 0) return;
        var dtAcc = _mAccountController.SelectAccountDetailHead(Convert.ToInt32(ddlAccountHead.SelectedValue),null);

        grdAccountHeads.DataSource = dtAcc;
        grdAccountHeads.DataBind();
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtTotalCredit.Text = "0";
        txtTotalDebit.Text = "0";
        txtRemarks.Text = "";
        txtAccountDes.Text = "";
        GrdOrder.DataSource = null;
        GrdOrder.DataBind();
        this.Session.Remove("dtVoucher");
        this.CreatTable();
        drpDistributor.Enabled = true;
        DrpVoucherSubType.Enabled = true;
    }
    
    /// <summary>
    /// Shows Vouchers in Crystal Report For Print Purpose
    /// </summary>
    /// <param name="voucherNo">Voucher</param>
    private void PrintVoucher(string voucherNo)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        int VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);

        if (VoucherType == Constants.Bank_Voucher)
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpVoucherViewBankReceipt();
        }
        else if (VoucherType == Constants.Bank_PaymentVoucher)
        {
            CrpReport = new SAMSBusinessLayer.Reports.CrpVoucherViewBankPayment();
        }
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), voucherNo, VoucherType);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("VoucherType", DrpVoucherSubType.SelectedItem.Text);
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    
    /// <summary>
    /// Adds Voucher Data To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;

        DataControl dc = new DataControl();
        DataTable dtHead = (DataTable)this.Session["dtHead"];
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        if (RowId >= 0)
        {
            DataRow dr = dtVoucher.Rows[RowId];
            dr["ACCOUNT_HEAD_ID"] = ddlAccountHead.SelectedValue;
            dr["ACCOUNT_CODE"] = "";
            dr["ACCOUNT_NAME"] = ddlAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
            dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dr["REMARKS"] = txtAccountDes.Text;

        }
        else
        {
            DataRow dr = dtVoucher.NewRow();
            dr["Ledger_ID"] = Constants.LongNullValue.ToString();
            dr["ACCOUNT_HEAD_ID"] = ddlAccountHead.SelectedValue;
            dr["ACCOUNT_CODE"] = "";
            dr["ACCOUNT_NAME"] = ddlAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
            dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
            dr["REMARKS"] = txtAccountDes.Text;
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dtVoucher.Rows.Add(dr);
        }

        #region Set Total

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        }
        txtTotalDebit.Text = decimal.Round(TotalDebit, 2).ToString();
        txtTotalCredit.Text = decimal.Round(TotalCredit, 2).ToString();

        #endregion

        #region Clear Txtbox
                
        txtDebitAmount.Text = "";
        txtCreditAmount.Text = "";
        txtAccountDes.Text = "";
        RowId = -1;

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        if (GrdOrder.Rows.Count > 0)
        {
            DrpVoucherSubType.Enabled = false;
        }
        this.Session.Add("dtVoucher", dtVoucher);
        ScriptManager.GetCurrent(Page).SetFocus(ddlAccountHead);        
    }

    /// <summary>
    /// Deletes Voucher
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        dtVoucher.Rows.RemoveAt(e.RowIndex);

        #region Set Total

        //foreach (DataRow dr in dtVoucher.Rows)
        //{
        //    TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        //    TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        //}
        txtTotalDebit.Text = TotalDebit.ToString();
        txtTotalCredit.Text = TotalCredit.ToString();

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        RowId = -1;
    }

    /// <summary>
    /// Sets Voucher For Edit. This Function Runs When An Existing Voucher Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        ddlAccountHead.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        txtAccountDes.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
        txtDebitAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[4].Text;
        txtCreditAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;        
    }

    /// <summary>
    /// Saves Voucher
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnDone_Click(object sender, EventArgs e)
    {
        LedgerController mLController = new LedgerController();
        DataControl dc = new DataControl();
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        DataTable dtHead = (DataTable)this.Session["dtHead"];

        decimal TotalDebit = 0;
        decimal TotalCredit = 0;

        #region Set Total

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        }
        #endregion

        #region In Case of Bank & Cash Voucher

        if (drpBanks.SelectedItem.Text != "N/A")
        {
            decimal differnceAmt = TotalDebit - TotalCredit;

            if (differnceAmt > 0)
            {
                DataRow dr1 = dtVoucher.NewRow();
                DataRow[] foundSubRows = dtHead.Select("ACCOUNT_HEAD_ID  = " + drpBanks.SelectedValue.ToString());
                dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                dr1["ACCOUNT_HEAD_ID"] = foundSubRows[0]["ACCOUNT_HEAD_ID"];
                dr1["ACCOUNT_CODE"] = foundSubRows[0]["ACCOUNT_CODE"];
                dr1["ACCOUNT_NAME"] = foundSubRows[0]["ACCOUNT_NAME"];
                dr1["REMARKS"] = txtAccountDes.Text;
                dr1["CREDIT"] = differnceAmt;
                dr1["DEBIT"] = 0;
                dr1["Principal"] = "";
                dr1["Principal_id"] = dtVoucher.Rows[0]["Principal_id"].ToString();
                dtVoucher.Rows.Add(dr1);

            }
            else if (differnceAmt < 0)
            {
                DataRow dr1 = dtVoucher.NewRow();
                DataRow[] foundSubRows = dtHead.Select("ACCOUNT_HEAD_ID  = " + drpBanks.SelectedValue.ToString());
                dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                dr1["ACCOUNT_HEAD_ID"] = foundSubRows[0]["ACCOUNT_HEAD_ID"];
                dr1["ACCOUNT_CODE"] = foundSubRows[0]["ACCOUNT_CODE"];
                dr1["ACCOUNT_NAME"] = foundSubRows[0]["ACCOUNT_NAME"];
                dr1["REMARKS"] = txtAccountDes.Text;
                dr1["CREDIT"] = 0;
                dr1["DEBIT"] = -(differnceAmt);
                dr1["Principal"] = "";
                dr1["Principal_id"] = dtVoucher.Rows[0]["Principal_id"].ToString();
                dtVoucher.Rows.Add(dr1);
            }
        }

        #endregion

        int VoucherType = Constants.IntNullValue;
        string SlipNo = "";
        string ChequeNo = "";
        VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);

        if (VoucherType == Constants.Bank_Voucher)
        {
            ChequeNo = txtChequeNo.Text;
            SlipNo = txtSlipNo.Text;
        }
        else
        {
            ChequeNo = ddlChequeNo.SelectedItem.Text;
            SlipNo = ddlAccountNo.SelectedItem.Text;
        }

        DateTime ChequeDate;
        DateTime Voucherdate;
        DateTime DueDate;

        Voucherdate = Convert.ToDateTime(Session["CurrentWorkDate"]);
        ChequeDate = Constants.DateNullValue;

        if (txtDueDate.Text.Length > 0 && txtDueDate.Visible)
        {
            DueDate = DateTime.Parse(txtDueDate.Text + " 00:00:00");
        }
        else
        {
            DueDate = Constants.DateNullValue;
        }

        dtVoucher = (DataTable)this.Session["dtVoucher"];
        string MaxDocumentId = mLController.SelectMaxVoucherId(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), Voucherdate);

        bool IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, VoucherType,
               Voucherdate, Constants.ChequePayment, Constants.IntNullValue, txtRemarks.Text, DueDate, ChequeNo, dtVoucher, int.Parse(this.Session["UserId"].ToString()), SlipNo, DueDate);

        if (IsSaveVoucher == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Voucher No : " + MaxDocumentId + " has been Saved');", true);
            this.ClearAll();
            this.PrintVoucher(MaxDocumentId);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occur Wrong Entry');", true);
        }
    }

    /// <summary>
    /// Sets Grid Footer Columns
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = txtTotalDebit.Text;
            e.Row.Cells[7].Text = txtTotalCredit.Text;
        }
    }
    
    protected void DrpVoucherSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpVoucherSubType.SelectedIndex == 0)
        {
            Label3.Text = "Received from";

            lblAccountNo0.Visible = true;
            lblAccountNo.Visible = false;
            txtSlipNo.Visible = true;
            ddlAccountNo.Visible = false;
            lblChequeNo0.Visible = true;
            lblChequeNo.Visible = false;
            txtChequeNo.Visible = true;
            ddlChequeNo.Visible = false;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;
        }
        else if (DrpVoucherSubType.SelectedIndex == 1)
        {
            Label3.Text = "Paid To";
            
            lblAccountNo0.Visible = false;
            lblAccountNo.Visible = true;
            txtSlipNo.Visible = false;
            ddlAccountNo.Visible = true;
            lblChequeNo0.Visible = false;
            lblChequeNo.Visible = true;
            txtChequeNo.Visible = false;
            ddlChequeNo.Visible = true;

            txtDebitAmount.Enabled = true;
            txtCreditAmount.Enabled = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }

    protected void ddlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadChequeNos();
    }
    protected void ddlAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountHeads();
    }
    private void LoadChequeNos()
    {
        ddlChequeNo.Items.Clear();
        if (ddlAccountNo.Items.Count > 0)
        {
            ChequeEntryController objCheque = new ChequeEntryController();
            DataTable dtCheques = objCheque.GetChequeNo(ddlAccountNo.SelectedItem.Text);

            clsWebFormUtil.FillDropDownList(ddlChequeNo, dtCheques, "CHEQUE_BOOK_DETAIL_ID", "CHEQUE_NO");
        }
    }

    private void LoadAccountNos()
    {
        ChequeEntryController objCheque = new ChequeEntryController();
        DataTable dtAccounts = objCheque.GetChequeBook(Convert.ToInt32(drpBanks.SelectedValue), Constants.IntNullValue);

        DataView view = new DataView(dtAccounts);
        dtAccounts = view.ToTable(true, "ACCOUNT_NO");

        clsWebFormUtil.FillDropDownList(ddlAccountNo, dtAccounts, "ACCOUNT_NO", "ACCOUNT_NO", true);
    }
}