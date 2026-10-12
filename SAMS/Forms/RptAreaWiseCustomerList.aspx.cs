using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using CrystalDecisions.Shared;

/// <summary>
/// Form For Route Wise Customer List Report
/// </summary>
public partial class Forms_RptAreaWiseCustomerList : System.Web.UI.Page
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
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);

    }    

    /// <summary>
    /// Shows Route Wise Customer List in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        SAMSBusinessLayer.Reports.CrpCustomerList CrpReport = new SAMSBusinessLayer.Reports.CrpCustomerList();
        DataSet ds = null;
        ds = RptSaleCtl.SelectPrincipalWiseCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, true,Constants.IntNullValue);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", drpDistributor.SelectedItem.Text);

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);    
    }

    /// <summary>
    /// Shows Route Wise Customer List in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        SAMSBusinessLayer.Reports.CrpCustomerList CrpReport = new SAMSBusinessLayer.Reports.CrpCustomerList();
        DataSet ds = null;

        ds = RptSaleCtl.SelectPrincipalWiseCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,Constants.IntNullValue, Constants.IntNullValue, true, Constants.IntNullValue);

        CrpReport.SetDataSource(ds);

        CrpReport.Refresh();

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", drpDistributor.SelectedItem.Text);

        string path = SAMSCommon.Classes.Configuration.GetAppInstallationPath() + "\\ChartOfAccount.xls";

        CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        CrpReport.ExportToDisk(ExportFormatType.Excel, path);


        System.IO.FileInfo file = new System.IO.FileInfo(path);

        if (file.Exists)
        {
            Response.Clear();

            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

            Response.AddHeader("Content-Length", file.Length.ToString());

            Response.ContentType = "application/octet-stream";

            Response.WriteFile(file.FullName);

            Response.End();

        }
        else
        {
            Response.Write("This file does not exist.");
        }        
    }
}
