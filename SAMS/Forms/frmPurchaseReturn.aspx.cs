using System;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;
using System.Web.UI;

public partial class Forms_frmPurchaseReturn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("PurchaseReturnID");
            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmPurchaseReturnAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add Purchase Return</a>";
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvPurchaseReturn_RowEditing(object sender, GridViewEditEventArgs e)
    {
        long PurchaseReturnID = Convert.ToInt64(gvPurchaseReturn.Rows[e.NewEditIndex].Cells[0].Text);
        Session.Add("PurchaseReturnID", PurchaseReturnID);
        Response.Redirect("frmPurchaseReturnAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());            

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvPurchaseReturn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseController PurCtl = new PurchaseController();
        string PurchaseReturnID = gvPurchaseReturn.Rows[e.RowIndex].Cells[0].Text;

        if (PurCtl.UpdatePurchaseReturn(Convert.ToInt64(PurchaseReturnID),Constants.DateNullValue,Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, Convert.ToInt32(Session["UserID"]), true))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Purchase return deleted successfully.')", true);
            this.LoadGrid();
        }
    }

    private void LoadGrid()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable dtReq = PurCtl.GetPurchaseReturn(0, 0);
        gvPurchaseReturn.DataSource = dtReq;
        gvPurchaseReturn.DataBind();
    }
}