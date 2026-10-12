using System;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;
using System.Web.UI;

public partial class Forms_frmMaintenance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("MaintenanceID");
            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmMaintenanceAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add Repair/Maintenance</a>";
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvMaintenance_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        long MaintenanceID = Convert.ToInt64(gvMaintenance.Rows[e.NewEditIndex].Cells[0].Text);
        Session.Add("MaintenanceID", MaintenanceID);
        Response.Redirect("frmMaintenanceAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());            

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvMaintenance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseController PurCtl = new PurchaseController();
        string MaintenanceID = gvMaintenance.Rows[e.RowIndex].Cells[0].Text;

        if (PurCtl.UpdateMaintenance(Convert.ToInt64(MaintenanceID), Constants.IntNullValue, Constants.IntNullValue, null, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), true))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Maintenance deleted successfully.')", true);
            this.LoadGrid();
        }
    }

    private void LoadGrid()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable dtReq = PurCtl.GetMaintenance(0);
        gvMaintenance.DataSource = dtReq;
        gvMaintenance.DataBind();
    }
}