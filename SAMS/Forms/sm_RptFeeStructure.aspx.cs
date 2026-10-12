using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommon.Classes;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.SchoolManagementReports;

public partial class Forms_RptFeeStructure : System.Web.UI.Page
{
    Student_Controller mS_Ctrl = new Student_Controller();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadProgram();
            LoadSession();
            LoadGrid();
        }
    }
    #region LOad

    private void LoadProgram()
    {
        DataTable dt = mS_Ctrl.UspSelectProgram(Constants.IntNullValue, null,0);
        clsWebFormUtil.FillDropDownList(DrpStudyPrograms, dt, 0, 1);
    }
    private void LoadSession()
    {
        DataTable dt = mS_Ctrl.UspSelectSession(null,null);
        clsWebFormUtil.FillDropDownList(DrpSession, dt, 0, 1);
    }

    private void LoadGrid()
    {
        if (DrpStudyPrograms.SelectedValue != null && DrpSession.SelectedValue != null && DrpStudyPrograms.SelectedValue != "" && DrpSession.SelectedValue != "")
        {
            DataTable dt = mS_Ctrl.UspSelectFEE2(Constants.LongNullValue, int.Parse(DrpStudyPrograms.SelectedValue), int.Parse(DrpSession.SelectedValue), null, 0);
            grd_Student.DataSource = dt;
            grd_Student.DataBind();
        }
    }

    #endregion

    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        if (cbAll.Checked == true)
        {
            foreach (GridViewRow dr in grd_Student.Rows)
            {
                CheckBox chb_student = (CheckBox)dr.FindControl("chb_student");
                chb_student.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in grd_Student.Rows)
            {
                CheckBox chb_student = (CheckBox)dr.FindControl("chb_student");
                chb_student.Checked = false;
            }
        }
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtSearch.Text != "")
        {
            DataTable dt = mS_Ctrl.UspSelectFEE2(Constants.LongNullValue, int.Parse(DrpStudyPrograms.SelectedValue), int.Parse(DrpSession.SelectedValue), txtSearch.Text,0);

            grd_Student.DataSource = dt;
            grd_Student.DataBind();
        }
        else
        {
            LoadGrid();
        }
    }

    #region On Click

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptStudent_Controller RptStdCtl = new RptStudent_Controller();
        DataSet ds = null;
       // DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        string id = "";
        foreach (GridViewRow dr in grd_Student.Rows)
        {
            CheckBox chb_student = (CheckBox)dr.FindControl("chb_student");
            if (chb_student.Checked == true)
            {
                id += dr.Cells[1].Text;
                id += ",";
            }
        }
        if (id != "")
        {
            id = id.Substring(0, id.Length - 1);

            ds = RptStdCtl.rptSelectFEE(id.ToString());

            CrpFeeStructure CrpReport = new CrpFeeStructure();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();


            CrpReport.SetParameterValue("Program", DrpStudyPrograms.SelectedItem.Text);
            CrpReport.SetParameterValue("Session", DrpSession.SelectedItem.Text);

            this.Session.Add("ReportType", 0);
            this.Session.Add("CrpReport", CrpReport);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            
        }
    }

    /// <summary>
    /// Shows NCS vs Bank Deposit in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        //DocumentPrintController DPrint = new DocumentPrintController();
        //RptSaleController RptSaleCtl = new RptSaleController();
        //DataSet ds = null;
        //DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        //DataControl dc = new DataControl();
        //ds = RptSaleCtl.SelectDailyBankDeposit(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
        //    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()));

        //SAMSBusinessLayer.Reports.CrpDailyBankDeposit CrpReport = new SAMSBusinessLayer.Reports.CrpDailyBankDeposit();
        //CrpReport.SetDataSource(ds);
        //CrpReport.Refresh();

        //CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        //CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        //CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        //CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        //CrpReport.SetParameterValue("DISTRIBUTOR_NAME", drpDistributor.SelectedItem.Text);

        //string path = SAMSCommon.Classes.Configuration.GetAppInstallationPath() + "\\ExportedFile.xls";

        //CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        //CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, path);

        //System.IO.FileInfo file = new System.IO.FileInfo(path);

        //if (file.Exists)
        //{
        //    Response.Clear();

        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

        //    Response.AddHeader("Content-Length", file.Length.ToString());

        //    Response.ContentType = "application/octet-stream";

        //    Response.WriteFile(file.FullName);

        //    Response.End();

        //}
        //else
        //{
        //    Response.Write("This file does not exist.");
        //}
    }

    #endregion

    protected void DrpStudyPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void DrpSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
}