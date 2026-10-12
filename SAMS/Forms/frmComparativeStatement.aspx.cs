using System;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;
using System.Web.UI;

public partial class Forms_frmComparativeStatement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("ComparativeStatementID");
            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmComparativeStatementAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add Comparative Statement</a>";
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvComparativeStatement_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        long ComparativeStatementID = Convert.ToInt64(gvComparativeStatement.Rows[e.NewEditIndex].Cells[0].Text);
        Session.Add("ComparativeStatementID", ComparativeStatementID);
        Response.Redirect("frmComparativeStatementAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());            

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvComparativeStatement_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseController PurCtl = new PurchaseController();
        string ComparativeStatementID = gvComparativeStatement.Rows[e.RowIndex].Cells[0].Text;

        if (PurCtl.UpdateComparativeStatement(Convert.ToInt64(ComparativeStatementID), Constants.IntNullValue, Constants.IntNullValue, Constants.DateNullValue, null, null, null, Convert.ToInt32(Session["UserID"]), true))
        {            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Comparative Statement deleted successfully.')", true);
            this.LoadGrid();
        }
    }

    private void LoadGrid()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable dtReq = PurCtl.GetComparativeStatement(0);
        gvComparativeStatement.DataSource = dtReq;
        gvComparativeStatement.DataBind();
    }
}