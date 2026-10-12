using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Add, Edit, Delete Busines Type
/// </summary>
public partial class Forms_pr_frmCompany : System.Web.UI.Page
{
    CompanyController cc = new CompanyController();
    /// <summary>
    /// Page_Load Function Populates All Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            prtCompanies.DataSource = cc.SelectCompany_User(Constants.IntNullValue, Constants.IntNullValue);
            prtCompanies.DataBind();
        }
    }
    protected void lnkAddCompany_Click(object sender, EventArgs e)
    {
        if (Session["EditCompanyID"] != null)
        {
            Session.Remove("EditCompanyID");
        }
        if (Session["LogoName"] != null)
        {
            Session.Remove("LogoName");
        }

        Response.Redirect("pr_frmCompanyAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString());
    }
    protected void prtCompanies_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Session.Add("EditCompanyID", e.CommandArgument.ToString());
        Response.Redirect("pr_frmCompanyAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString());
    }
}
