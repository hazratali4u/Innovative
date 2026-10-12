using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
/// <summary>
/// Form To Add, Edit, Delete Busines Type
/// </summary>
public partial class pr_frmEmployee : System.Web.UI.Page
{
    EmployeeController ec = new EmployeeController();  
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
            LoadGrid();
        }
    }

    private void LoadGrid()
    {
        DataTable dtEmployee = new DataTable();
        dtEmployee = ec.SelectEmployee(Constants.LongNullValue, Convert.ToInt32(Session["WorkingCompanyID"]), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, false, 0, 15, CurrentPage + 1, Convert.ToInt32(Session["UserID"]));
        
        rptEmployees.DataSource = null;
        rptEmployees.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 15;

        if (dtEmployee.Rows.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;

            rptEmployees.DataSource = dtEmployee;
            rptEmployees.DataBind();

            linkbtnnext.Visible = true;
            linkbtnprev.Visible = true;
            lblTotalNoOfPages.Visible = true;
            lblCurrentPageNo.Visible = true;
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblOf.Visible = true;
            lblTotalNoOfRecords.Text = dtEmployee.Rows[0]["TotalRows"].ToString();
            lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
            lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dtEmployee.Rows[0]["TotalRows"]) / Convert.ToDecimal(15)));

            if (lblCurrentPageNo.Text == lblTotalNoOfPages.Text)
            {
                linkbtnnext.Enabled = false;
            }
            else
            {
                linkbtnnext.Enabled = true;
            }
            if (lblCurrentPageNo.Text == "1")
            {
                linkbtnprev.Enabled = false;
            }
            else
            {
                linkbtnprev.Enabled = true;
            }
        }
        else if (dtEmployee.Rows.Count == 0)
        {
            linkbtnnext.Visible = false;
            linkbtnprev.Visible = false;
            lblTotalNoOfPages.Visible = false;
            lblCurrentPageNo.Visible = false;
            lblTotalNoOfRecords.Visible = false;
            lblOf.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
    }
    
    protected void btnAddEmployee_Click(object sender, EventArgs e)
    {
        if (Session["EditEmployeeID"] != null)
        {
            Session.Remove("EditEmployeeID");
        }
        if (Session["LogoName"] != null)
        {
            Session.Remove("LogoName");
        }

        Response.Redirect("pr_frmEmployeeAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString());
    }
    
    protected void rptEmployees_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Session.Add("EditEmployeeID", e.CommandArgument.ToString());
        Response.Redirect("pr_frmEmployeeAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString());
    }

    public int CurrentPage
    {
        get
        {
            object objview = this.ViewState["_CurrentPage"];
            if (objview == null)
                return 0;
            else
                return (int)objview;
        }
        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }
    
    protected void linkbtnprev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        LoadGrid();
    }
    
    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        LoadGrid();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text.Length > 0)
        {
            DataTable dtSearch = ec.SearchEmployee(Convert.ToInt32(Session["WorkingCompanyID"]), txtSearch.Text);
            rptSearch.DataSource = dtSearch;
            rptSearch.DataBind();

            rptEmployees.DataSource = null;
            rptEmployees.DataBind();
            linkbtnnext.Visible = false;
            linkbtnprev.Visible = false;
            lblTotalNoOfPages.Visible = false;
            lblCurrentPageNo.Visible = false;
            lblTotalNoOfRecords.Visible = false;
            lblOf.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        rptSearch.DataSource = null;
        rptSearch.DataBind();
        txtSearch.Text = string.Empty;
        LoadGrid();
    }
}
