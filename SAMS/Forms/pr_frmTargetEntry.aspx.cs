using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

public partial class pr_frmTargetEntry : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    TargetContoller tc = new TargetContoller();
    DataControl dc = new DataControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.txtFromDate.Text = System.DateTime.Today.ToString("MMM-yyyy");
            LoadLocation();            
            fillddlDesignation();
            fillgvEmployee();            
        }
    }

    protected void LoadLocation()
    {
        try
        {
            DataTable dt = cc.SelectCompany_lOCATIONS(Convert.ToInt32(Session["WorkingCompanyID"]), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]));
            ddlLocation.Items.Clear();
            clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads OrderBookers
    /// </summary>
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgvEmployee();
    }
    
    protected void fillddlDesignation()
    {
        DataTable dt = ccc.SelectDesignation(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDesignation.Items.Clear();
        ddlDesignation.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlDesignation, dt, 0, 1, false);

    }

    protected void fillgvEmployee()
    {

        DataTable dt = ec.GettEmployeeTarget(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Convert.ToInt32(ddlDesignation.SelectedValue), Constants.IntNullValue, Convert.ToInt32(ddlLocation.SelectedValue),Convert.ToDateTime(txtFromDate.Text));
        gvTarget.DataSource = dt;
        gvTarget.DataBind();
    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgvEmployee();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool flag = false;
        foreach (GridViewRow gvr in gvTarget.Rows)
        {
            HiddenField hfEmployeeID = (HiddenField)gvr.FindControl("hfEmployeeID");
            TextBox txtTarget = (TextBox)gvr.FindControl("txtTarget");
            TextBox txtAchievement = (TextBox)gvr.FindControl("txtAchievement");

            flag = tc.InsertTarget(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(hfEmployeeID.Value), Convert.ToInt32(dc.chkNull_0(txtTarget.Text)), Convert.ToInt32(dc.chkNull_0(txtAchievement.Text)), Convert.ToInt32(Session["UserID"]));
            if (!flag)
            {
                break;
            }
        }

        if (flag)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Target entered successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured or no data found.');", true);
        }
    }

    protected void btnGetTarget_Click(object sender, EventArgs e)
    {
        fillgvEmployee();
    }
}