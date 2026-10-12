using System;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;
using System.Web.UI;

public partial class Forms_frmPurchaseRequisition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("RequisitionID");
            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmPurchaseRequisitionAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add Requisition</a>";
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvRequisition_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        long RequisitionID = Convert.ToInt64(gvRequisition.Rows[e.NewEditIndex].Cells[0].Text);
        Session.Add("RequisitionID", RequisitionID);
        Response.Redirect("frmPurchaseRequisitionAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());            

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvRequisition_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseController PurCtl = new PurchaseController();
        string RequisitionID = gvRequisition.Rows[e.RowIndex].Cells[0].Text;

        if (PurCtl.UpdateRequisition(Convert.ToInt64(RequisitionID), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.DateNullValue, null, null, null, null,Constants.IntNullValue,Convert.ToInt32(Session["UserID"]), true))
        {            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Requisition deleted successfully.')", true);
            this.LoadGrid();
        }
    }

    private void LoadGrid()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable dtReq = PurCtl.GetRequisition(0,0);
        gvRequisition.DataSource = dtReq;
        gvRequisition.DataBind();
    }
}