using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_pr_frmProcessPayrollDetail : System.Web.UI.Page
{
    CompanyConfigrationController ccc=new CompanyConfigrationController ();
    protected void Page_Load(object sender, EventArgs e)
    {
      DataTable dt1 = ccc.SelectSalaryStructure(Constants.IntNullValue, null, Constants.IntNullValue, 2, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.IntNullValue);
      fillSalaryStructure(dt1);
    }
       protected void fillSalaryStructure(DataTable dt)
    {
        DataTable dtDeduction = CreateDeductionTable();
        DataTable dtAllowances = CreateAllowanceTable();
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["AllowanceID"].ToString() != null && dr["AllowanceID"].ToString() != "")
            {
                DataRow drAllowance = dtAllowances.NewRow();
                drAllowance["RatioType"] = dr["AmountTypeID"];
                drAllowance["AllowanceRatio"] = dr["Amount"];
                drAllowance["AllowanceID"] = dr["AllowanceID"];
                drAllowance["AllowanceDescription"] = dr["AllowanceDescription"];
                dtAllowances.Rows.Add(drAllowance);

            }
            if (dr["DeductionID"].ToString() != null && dr["DeductionID"].ToString() != "")
            {
                DataRow drDeduction = dtDeduction.NewRow();
                drDeduction["RatioType"] = dr["AmountTypeID"];
                drDeduction["DeductionRatio"] = dr["Amount"];
                drDeduction["DeductionID"] = dr["DeductionID"];
                drDeduction["Comments"] = dr["Comment"];
                drDeduction["DeductionDescription"] = dr["DeductionDescription"];
                dtDeduction.Rows.Add(drDeduction);
            }
        }
        rptAllAllowances.DataSource = dtAllowances;
        rptAllAllowances.DataBind();
        rptAllDeduction.DataSource = dtDeduction;
        rptAllDeduction.DataBind();
    }
       protected DataTable CreateDeductionTable()
    {
        DataTable dtDeduction = new DataTable();
        dtDeduction.Columns.Add("RatioType", typeof(int));
        dtDeduction.Columns.Add("DeductionRatio", typeof(decimal));
        dtDeduction.Columns.Add("DeductionID", typeof(int));
        dtDeduction.Columns.Add("DeductionDescription", typeof(string));
        dtDeduction.Columns.Add("Comments", typeof(string));
        return dtDeduction;
    }
       protected DataTable CreateAllowanceTable()
    {
        DataTable dtAllowance = new DataTable();
        dtAllowance.Columns.Add("RatioType", typeof(int));
        dtAllowance.Columns.Add("AllowanceRatio", typeof(decimal));
        dtAllowance.Columns.Add("AllowanceID", typeof(int));
        dtAllowance.Columns.Add("AllowanceDescription", typeof(string));
        return dtAllowance;

    }
}