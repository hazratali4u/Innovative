using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;  
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For Print Sale Document Report
/// </summary>
public partial class Forms_frmDocumentPrinting : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadSaleForce();
            this.LoadRoute();
            this.LoadCustomer();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            ddlPrincipal.Visible = false;
            lblPrincipal.Visible = false;
        }
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
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        ddlPrincipal.Items.Clear();
        ddlPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlPrincipal, m_dt, 0, 1,false);
    }
    
    /// <summary>
    /// Loads Sale Forces To Sale Force Combo
    /// </summary>
    private void LoadSaleForce()
    {
        //DrpArea.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            Distributor_UserController UController = new Distributor_UserController();
            DataTable dt = UController.SelectDistributorUser(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
           // DrpArea.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));       
         //   clsWebFormUtil.FillDropDownList(this.DrpArea, dt, "USER_ID", "USER_NAME2");
        }
    }
    
    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadRoute()
    {
       // DrpRoute.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            GeoHierarchyController gController = new GeoHierarchyController();
            DataTable dt = gController.SelectGeoHierarchy(int.Parse(drpDistributor.SelectedValue.ToString()));
          //  DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
          //  clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 1);
        }
    }
    
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        CustomerDataController mController = new CustomerDataController();
        DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()),Constants.IntNullValue, Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);

        //if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpRoute.SelectedValue != Constants.IntNullValue.ToString())

        //{
        //    CustomerDataController mController = new CustomerDataController();
        //    DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue);
        //    clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);

        //}
    }

    /// <summary>
    /// Loads Sale Forces And Routes
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
        this.LoadRoute();
    }

    /// <summary>
    /// Loads Sale Forces
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpLedgerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
        if (DrpLedgerType.SelectedIndex > 2)
        {
            ddlPrincipal.Visible = true;
            lblPrincipal.Visible = true;
            DrpCustomer.Visible = false;
            Label5.Visible = false;
        }
        else
        {
            ddlPrincipal.Visible = false;
            lblPrincipal.Visible = false;
            DrpCustomer.Visible = true;
            Label5.Visible = true;
        }
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    /// <summary>
    /// Shows Print Sale Document in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    
    /// <summary>
    /// Gets Print Sale Document And Shows Either in Excel or PDF
    /// </summary>
    /// <param name="p_ReportType">Type</param>
    private void ShowReport(int p_ReportType)
    {
        int p_CustomerType = Constants.IntNullValue;
        p_CustomerType = Convert.ToInt32(rblCustomerType.SelectedValue);
        RptSaleController RptSaleCtl = new RptSaleController();
        DocumentPrintController DPrint = new DocumentPrintController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        if (DrpLedgerType.SelectedIndex == 0)
        {
            DataControl dc = new DataControl();
            ds = RptSaleCtl.SelectSaleInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 3);
            ReportDocument CrpReport = new ReportDocument();
            CrpReport = new SAMSBusinessLayer.Reports.CrpSaleTaxInvoice1();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
                        
            CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("NtnNumber", dt.Rows[0]["NTN_NO"].ToString());
            CrpReport.SetParameterValue("CompanyAddress", dt.Rows[0]["CompanyAddress"].ToString());
            CrpReport.SetParameterValue("UserName", Session["UserName2"].ToString());
            CrpReport.SetParameterValue("TAXREGISTERATION_NO", dt.Rows[0]["GST_NUMBER"].ToString());
            CrpReport.SetParameterValue("UserDesignation", Session["Designation"].ToString());
            CrpReport.SetParameterValue("CONTACT_NUMBER", dt.Rows[0]["CONTACT_NUMBER"].ToString());
            //   CrpReport.SetParameterValue("PrintType", "0");
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", p_ReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else if (DrpLedgerType.SelectedIndex == 1)
        {
        }

        else if (DrpLedgerType.SelectedIndex == 2)
        {
            DataControl dc = new DataControl();
            ds = RptSaleCtl.SelectDeliveryChallan(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpCustomer.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 3);
            ReportDocument CrpReport = new ReportDocument();
            CrpReport = new SAMSBusinessLayer.Reports.CrpDeliveryChallan();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("USER_NAME", this.Session["UserName2"].ToString());
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", p_ReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);

        }
        else if (DrpLedgerType.SelectedIndex == 3)// Chek If is Leduger Type is GRN
        {

            DataControl dc = new DataControl();
            ds = RptSaleCtl.SelectGRN(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(ddlPrincipal.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 2);
            ReportDocument CrpReport = new ReportDocument();
            CrpReport = new SAMSBusinessLayer.Reports.CrpGoodsReceiptNote_();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", p_ReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);

        }
        else
        {
            DataControl dc = new DataControl();
            //ds = RptSaleCtl.SelectDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpArea.SelectedValue), Convert.ToInt32(ddlPrincipal.SelectedValue),
            //    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpLedgerType.SelectedValue.ToString()), Constants.LongNullValue, p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpRoute.SelectedValue), 0);

            ReportDocument CrpReport = new ReportDocument();

            if (rblCustomerType.SelectedValue == "0") //non gst
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocument();
            }
            else
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentGST();
            }



            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("NtnNumber", "NTN :" + dt.Rows[0]["NTN_NO"].ToString());
            CrpReport.SetParameterValue("CompanyAddress", dt.Rows[0]["CompanyAddress"].ToString());
            CrpReport.SetParameterValue("BranchAddress", "Branch Address: " + dt.Rows[0]["ADDRESS1"].ToString());
            CrpReport.SetParameterValue("TAXREGISTERATION_NO", dt.Rows[0]["GST_NUMBER"].ToString());
            CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
            CrpReport.SetParameterValue("CONTACT_NUMBER", "Ph: " + dt.Rows[0]["CONTACT_NUMBER"].ToString());
            CrpReport.SetParameterValue("PrintType", "0");
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", p_ReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    /// <summary>
    /// Shows Print Sale Document in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }    
}
