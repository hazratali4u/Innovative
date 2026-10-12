using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For General Ledger Report
/// </summary>
public partial class Forms_RptLedgerReport : System.Web.UI.Page
{
    readonly AccountHeadController _mAccountController = new AccountHeadController();
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            LoadDistributor();
            LoadAccountDetail();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        this.drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Accont Heads To Account ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
        
        DataTable dtHead = _mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        DataView dv = new DataView(dtHead);
        dv.Sort = "ACCOUNT_DETAIL";
        dtHead = dv.ToTable();
        clsWebFormUtil.FillListBox(LstAccountHead, dtHead, "ACCOUNT_DETAIL", "ACCOUNT_DETAIL", true);
    }
    private void LoadAccountHeads()
    {
        if (txtFromAccount.Text=="") return;
        var dtAcc = _mAccountController.SelectAccountDetailHead(Constants.IntNullValue, txtFromAccount.Text.Substring(0,10));

        grdAccountHeads.DataSource = dtAcc;
        grdAccountHeads.DataBind();
    }
    private void LoadAccountHeads2()
    {
        if (txttoAccount.Text == "") return;
        var dtAcc = _mAccountController.SelectAccountDetailHead(Constants.IntNullValue, txttoAccount.Text.Substring(0, 10));

        grdAccountHeads2.DataSource = dtAcc;
        grdAccountHeads2.DataBind();
    }
    /// <summary>
    /// Shows General Ledger in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        DataSet ds = null;

        DataControl dc = new DataControl();

        ds = RptAccountCtl.GeneralLedger_ViewRange(Constants.IntNullValue, txtFromAccount.Text.Substring(0, 10), txttoAccount.Text.Substring(0, 10), int.Parse(drpDistributor.SelectedValue.ToString()),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), Constants.VoucherPost);

        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

       CrpLedgerViewRange CrpReport = new CrpLedgerViewRange();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", "All");

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows General Ledger in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        DataSet ds = null;

        DataControl dc = new DataControl();

        ds = RptAccountCtl.GeneralLedger_ViewRange(Constants.IntNullValue, txtFromAccount.Text.Substring(0, 10), txttoAccount.Text.Substring(0, 10), int.Parse(drpDistributor.SelectedValue),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0);

        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
                
        CrpLedgerViewRange CrpReport = new CrpLedgerViewRange();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Principal", "All");

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 1);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Checks All Account Heads In Account Head ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbSelectAll.Checked == true)
        {
            txtFromAccount.Text = "0000000000";
            txttoAccount.Text = "9999999999";
            txtFromAccount.ReadOnly = true;
            txttoAccount.ReadOnly = true;
        }
        else
        {
            txtFromAccount.Text = "";
            txttoAccount.Text = "";
            txtFromAccount.ReadOnly = false;
            txttoAccount.ReadOnly = false;
        }
    }

    protected void btnfromAccount_Click(object sender, EventArgs e)
    {
        LoadAccountHeads();
    }
    protected void btntoAccount_Click(object sender, EventArgs e)
    {
        LoadAccountHeads2();
    }
}
