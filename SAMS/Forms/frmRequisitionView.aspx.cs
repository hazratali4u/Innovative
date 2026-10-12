using System;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;
using System.Web.UI;

public partial class Forms_frmRequisitionView : System.Web.UI.Page
{
    PurchaseController PurCtl = new PurchaseController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("RequisitionID");
            this.LoadGrid();
        }
    }
    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    
    private void LoadGrid()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable dtReq = PurCtl.SelectRequisitionView(Constants.LongNullValue, Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue);
        gvRequisition.DataSource = dtReq;
        gvRequisition.DataBind();
    }
    private void LoadRequisitionDetail()
    {
       
        DataSet ds = PurCtl.GetRequisition(1, Convert.ToInt64(Session["RequisitionID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblOriginator.Text = ds.Tables[0].Rows[0]["ORIGINATOR_NAME"].ToString();
            lblRequestFor.Text = ds.Tables[0].Rows[0]["STORE_NAME"].ToString();
            lblDeliveryDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DELIVERY_DATE"]).ToString("dd-MMM-yyyy");
            lblRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
            lblReviewedby.Text = ds.Tables[0].Rows[0]["REVIEWD_BY"].ToString();
            lblApprovedBy.Text = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();
        }
        DataTable RequisitionSKU = ds.Tables[1];
        this.Session.Add("RequisitionSKU", RequisitionSKU);
        LoadGirdView();
    }
    private void LoadGirdView()
    {
        DataTable RequisitionSKU = (DataTable)this.Session["RequisitionSKU"];
        gvProduct.DataSource = RequisitionSKU;
        gvProduct.DataBind();
    }
    protected void gvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;

        DropDownList ddlAllocateTo = row.FindControl("ddlAllocateTo") as DropDownList;
        if (ddlAllocateTo != null)
        {
            ddlAllocateTo.Items.Clear();
            DataTable dt = PurCtl.SelectEmployeeByDepartmentName("purchase");
            ddlAllocateTo.Items.Add(new ListItem("--SELECT--", "0"));
            clsWebFormUtil.FillDropDownList(ddlAllocateTo, dt, 1, 0, false);
            DataRow row1 = ((DataRowView)e.Row.DataItem).Row;
            ddlAllocateTo.SelectedValue = row1.Field<int>("ALLOCATED_USER_ID").ToString();
        }
        DropDownList ddlPriority = row.FindControl("ddlPriority") as DropDownList;
        if (ddlPriority != null)
        {
            DataRow row1 = ((DataRowView)e.Row.DataItem).Row;
            ddlPriority.SelectedValue = row1.Field<int>("PRIORITY_ID").ToString();
            setRowColor(Convert.ToInt32(ddlPriority.SelectedValue), row, ddlPriority, ddlAllocateTo);
        }
    }
    private void setRowColor(int priority, GridViewRow row, DropDownList ddlPriority,DropDownList  ddlAllocateTo)  
    {
        if (priority==1)
        {
            row.CssClass = "row-whit prt-high";
            ddlPriority.CssClass = "row-whit prt-high";
            ddlAllocateTo.CssClass = "row-whit prt-high";
        }
        else if (priority==2)
        {
            row.CssClass = "row-whit prt-medium";
            ddlPriority.CssClass = "row-whit prt-medium";
            ddlAllocateTo.CssClass = "row-whit prt-medium";
        }
        else if (priority == 3)
        {
            row.CssClass = "row-whit prt-low";
            ddlPriority.CssClass = "row-whit prt-low";
            ddlAllocateTo.CssClass = "row-whit prt-low";
        }
        else
        {
            row.CssClass = "row-whit prt-default";
            ddlPriority.CssClass = "row-whit prt-default";
            ddlAllocateTo.CssClass = "row-whit prt-default";
        }
    }
    protected void gvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            long RequisitionID = Convert.ToInt64(e.CommandArgument);
            Session.Add("RequisitionID", RequisitionID);
            LoadRequisitionDetail();
            ModalPopupExtender.Show();
            //Response.Redirect("frmPurchaseRequisitionAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());            

        }
        else
        {
        }

    }
    protected void ddlPriority_SelectedIndexChanged(object sender, EventArgs e)
    {
        var ddlPriority = ((DropDownList)(gvRequisition.FindControl("ddlPriority")));
        ddlPriority = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlPriority.NamingContainer;

        DropDownList ddlAllocateTo = row.FindControl("ddlAllocateTo") as DropDownList;

        PurCtl.UpdateREQUISITION_VIEW(Convert.ToInt32(row.Cells[1].Text), Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(ddlPriority.SelectedValue), int.Parse(Session["UserID"].ToString()), false);
        setRowColor(Convert.ToInt32(ddlPriority.SelectedValue), row, ddlPriority, ddlAllocateTo);

    }
    protected void ddlAllocateTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        var ddlAllocateTo = ((DropDownList)(gvRequisition.FindControl("ddlAllocateTo")));
        ddlAllocateTo = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlAllocateTo.NamingContainer;
        PurCtl.UpdateREQUISITION_VIEW(Convert.ToInt32(row.Cells[1].Text), Constants.LongNullValue,Convert.ToInt32(ddlAllocateTo.SelectedValue), Constants.IntNullValue , int.Parse(Session["UserID"].ToString()), false);
  
    }
}