using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmVoucherEditing : System.Web.UI.Page
{
    DataTable dtVoucher;
    private static int RowId = -1;

    private void CreatTable()
    {
        dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("Principal", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);

    }
    
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        this.DrpPrincipal.Items.Add(new ListItem("General Entry", "0"));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }
    
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    
    private void LoadEditVoucher()
    {
        LedgerController LController = new LedgerController();
        DataTable dtVoucher = (DataTable)this.Session["dtVoucher"];

        DataTable dt = LController.SelectVoucherNo(Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(this.Session["VoucherTypeId"]), int.Parse(this.Session["UserId"].ToString()), lblVoucherNo.Text);
        if (dt.Rows.Count > 0)
        {            
            drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();            
            if (dt.Rows[0]["VOUCHER_TYPE_ID"].ToString() == "14" || dt.Rows[0]["VOUCHER_TYPE_ID"].ToString() == "24")
            {                
                DrpVoucherSubType.Items.Add(new ListItem("Cash Receipt Voucher", "14"));
                DrpVoucherSubType.Items.Add(new ListItem("Cash Payment Voucher", "24"));

                DrpVoucherType.SelectedValue = "14";
                DrpVoucherSubType.SelectedValue = dt.Rows[0]["VOUCHER_TYPE_ID"].ToString();
            }
            else if (dt.Rows[0]["VOUCHER_TYPE_ID"].ToString() == "15" || dt.Rows[0]["VOUCHER_TYPE_ID"].ToString() == "17")
            {
                DrpVoucherSubType.Items.Add(new ListItem("Bank Receipt Voucher", "15"));
                DrpVoucherSubType.Items.Add(new ListItem("Bank Payment Voucher", "17"));

                DrpVoucherType.SelectedValue = "15";
                DrpVoucherSubType.SelectedValue = dt.Rows[0]["VOUCHER_TYPE_ID"].ToString();
            }
            else
            {
                DrpVoucherType.SelectedValue = "16";
            }
            
            txtVoucherDate.Text = DateTime.Parse(dt.Rows[0]["VOUCHER_DATE"].ToString()).ToString("dd-MMM-yyyy");
            if (dt.Rows[0]["DUE_DATE"].ToString() != "")
            {
                txtDueDate.Text = DateTime.Parse(dt.Rows[0]["DUE_DATE"].ToString()).ToString("dd-MMM-yyyy");
            }           
            txtChequeNo.Text = dt.Rows[0]["CHEQUE_NO"].ToString();
            txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            txtpayeesName.Text = dt.Rows[0]["PAYEES_NAME"].ToString();

            dtVoucher = LController.SelectVoucherDetail(int.Parse(drpDistributor.SelectedValue.ToString()), lblVoucherNo.Text, int.Parse(dt.Rows[0]["VOUCHER_TYPE_ID"].ToString()));

            this.Session.Add("dtVoucher", dtVoucher);
            GrdOrder.DataSource = dtVoucher;
            GrdOrder.DataBind();

            if (DrpVoucherSubType.SelectedIndex == 0)
            {
                if (DrpVoucherType.SelectedValue == "14")
                {
                    DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
                    DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
                    txtDebitAmount.Enabled = false;
                    txtCreditAmount.Enabled = true;
                }
                else if (DrpVoucherType.SelectedValue == "15")
                {
                    DrpPaymentMode.Items.Add(new ListItem("Cheque", "18"));
                    DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
                    txtDebitAmount.Enabled = false;
                    txtCreditAmount.Enabled = true;
                }
                else
                {
                    DrpPaymentMode.Items.Add(new ListItem("JV", Constants.CashPayment.ToString()));
                    txtDebitAmount.Enabled = true;
                    txtCreditAmount.Enabled = true;
                }
               
            }
            else
            {
                if (DrpVoucherType.SelectedValue == "14")
                {
                    DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
                    DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
                    txtDebitAmount.Enabled = true;
                    txtCreditAmount.Enabled = false;
                }
                else if (DrpVoucherType.SelectedValue == "15")
                {
                    DrpPaymentMode.Items.Add(new ListItem("Cheque", "18"));
                    DrpPaymentMode.Items.Add(new ListItem("Pay Order", "21"));
                    txtDebitAmount.Enabled = true;
                    txtCreditAmount.Enabled = false;
                }
                else
                {
                    DrpPaymentMode.Items.Add(new ListItem("JV", Constants.CashPayment.ToString()));
                    txtDebitAmount.Enabled = true;
                    txtCreditAmount.Enabled = true;
                }
            }

            DrpPaymentMode.SelectedValue = dt.Rows[0]["PAYMENT_MODE"].ToString();
        }

    }
    
    /// <summary>
    /// Loads Account Heads To ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
        // Load Account Head 
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME");
        this.Session.Add("dtHead", dtHead);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            this.LoadAccountDetail();
            this.LoadDistributor();
            this.LoadPrincipal();
            this.CreatTable();
            this.lblVoucherNo.Text = this.Session["VoucherNo"].ToString();
            this.drpDistributor.SelectedValue = this.Session["DistributorId"].ToString();            
            if (int.Parse(this.DrpVoucherType.SelectedValue.ToString()) == Constants.Journal_Voucher)
            {
                lblChequeNo.Text = "Invoice No";
            }

            this.LoadEditVoucher();

            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            RowId = -1;
            this.Session.Remove("VoucherNo");
            this.Session.Remove("DistributorId");
            this.Session.Remove("VoucherTypeId");

            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["mode"].ToString().ToLower() == "view")
                {
                    DrpPaymentMode.Enabled = false;
                    ddlAccountHead.Enabled = false;
                    DrpPrincipal.Enabled = false;
                    btnSave.Visible = false;
                    btnDone.Visible = false;
                }
            }
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
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
            dr["REMARKS"] = txtAccountDes.Text;
            dr["Principal"] = DrpPrincipal.SelectedItem.Text;
            dr["Principal_id"] = DrpPrincipal.SelectedItem.Value;


        }
        else
        {
            DataRow dr = dtVoucher.NewRow();
            dr["ACCOUNT_HEAD_ID"] = ddlAccountHead.SelectedValue;
            dr["ACCOUNT_CODE"] = "";
            dr["ACCOUNT_NAME"] = ddlAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
            dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
            dr["REMARKS"] = txtAccountDes.Text;
            dtVoucher.Rows.Add(dr);
            dr["Principal"] = DrpPrincipal.SelectedItem.Text;
            dr["Principal_id"] = DrpPrincipal.SelectedItem.Value;

        }
               

        #region Clear Txtbox

        txtDebitAmount.Text = "";
        txtCreditAmount.Text = "";
        txtAccountDes.Text = "";
        RowId = -1;

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        this.Session.Add("dtVoucher", dtVoucher);
        ScriptManager.GetCurrent(Page).SetFocus(ddlAccountHead);
    }
    
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
        txtTotalDebit.Text = decimal.Round(TotalDebit, 2).ToString();
        txtTotalCredit.Text = decimal.Round(TotalCredit, 2).ToString();


        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        RowId = -1;
    }
    
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        ddlAccountHead.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        txtAccountDes.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
        txtDebitAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
        txtCreditAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[6].Text;
        DrpPrincipal.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[7].Text;
    }
    
    protected void btnDone_Click(object sender, EventArgs e)
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;
        dtVoucher = (DataTable)this.Session["dtVoucher"];

        #region Set Total

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        }
        #endregion

        if (TotalDebit == TotalCredit)
        {
            LedgerController mLController = new LedgerController();
            DataControl dc = new DataControl();
            DateTime Voucherdate;
            DateTime DueDate;

            int VoucherTypeID = Constants.Journal_Voucher;
            if (DrpVoucherType.SelectedValue != Constants.Journal_Voucher.ToString())
            {
                VoucherTypeID = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
            }            

            Voucherdate = DateTime.Parse(txtVoucherDate.Text + " 00:00:00");

            if (txtDueDate.Text.Length > 0)
            {
                DueDate = DateTime.Parse(txtDueDate.Text + " 00:00:00");
            }
            else
            {
                DueDate = Constants.DateNullValue;
            }      

            dtVoucher = (DataTable)this.Session["dtVoucher"];

            string MaxDocumentId = lblVoucherNo.Text;

            bool MResualt = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), MaxDocumentId, VoucherTypeID,
                   Voucherdate, int.Parse(DrpPaymentMode.SelectedValue.ToString()), 0, txtRemarks.Text, DueDate, txtChequeNo.Text, dtVoucher, int.Parse(this.Session["UserId"].ToString()), null, DueDate, Convert.ToInt32(Session["IS_POSTED"]));

            if (MResualt == true)
            {                
                Response.Write("<script language='javascript'> { window.close();}</script>");

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some Error Occure');", true); 
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Debit Must Equal To Credit Balance');", true);
        }

    }
    
    protected void GrdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = txtTotalDebit.Text;
            e.Row.Cells[7].Text = txtTotalCredit.Text;
        }
    }
    
}
