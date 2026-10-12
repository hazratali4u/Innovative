using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmSalesOrder : System.Web.UI.Page
{
    OrderEntryController OrderCtl = new OrderEntryController();

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillRepeater();
            ltrAdd.Text = "<a href='frmSalesOrderAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add Sales Order</a>";
        }
    }

    protected void rPurchaseOrder_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "close")
        {
            long SOID = Convert.ToInt64(e.CommandArgument);
            if (OrderCtl.UpdateSaleOrder(SOID, Convert.ToInt32(Session["UserID"]), 0))//STATUS_ID=0 for Close
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('PO closed successfully.');", true);
                FillRepeater();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
            }
        }      
    }

    private void FillRepeater()
    {
        rPurchaseOrder.DataSource = null;
        rPurchaseOrder.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 20;

        DataTable dt = OrderCtl.GetDocumentNo(Constants.DateNullValue, Convert.ToInt32(Session["UserID"]), 6);
        
        PagedResults.DataSource = dt.DefaultView;
        if (PagedResults.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !PagedResults.IsLastPage;
            linkbtnprev.Enabled = !PagedResults.IsFirstPage;
            rPurchaseOrder.DataSource = PagedResults;
            rPurchaseOrder.DataBind();

            linkbtnnext.Visible = true;
            linkbtnprev.Visible = true;
            lblTotalNoOfPages.Visible = true;
            lblCurrentPageNo.Visible = true;
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblOf.Visible = true;
            lblTotalNoOfRecords.Text = dt.Rows.Count.ToString();
            lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
            lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / Convert.ToDecimal(20)));

        }
        else if (PagedResults.Count == 0)
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
        FillRepeater();
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        FillRepeater();
    }
}