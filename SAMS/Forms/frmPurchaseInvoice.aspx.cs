using System;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;
using System.Web.UI;

public partial class Forms_frmPurchaseInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("PurchaseInvoiceID");
            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmPurchaseInvoiceAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add Purchase Invoice </a>";
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvPurchaseInvoice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        long PurchaseInvoiceID = Convert.ToInt64(gvPurchaseInvoice.Rows[e.NewEditIndex].Cells[0].Text);
        Session.Add("PurchaseInvoiceID", PurchaseInvoiceID);
        Response.Redirect("frmPurchaseInvoiceAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());            

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvPurchaseInvoice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseController PurCtl = new PurchaseController();
        string PurchaseInvoiceID = gvPurchaseInvoice.Rows[e.RowIndex].Cells[0].Text;

        if (PurCtl.UpdatePurchaseInvoice(Convert.ToInt64(PurchaseInvoiceID),Constants.DateNullValue,Constants.LongNullValue, null, Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]),Constants.DecimalNullValue,null, false))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Purchase Invoice deleted successfully.')", true);
            this.LoadGrid();
        }
    }

    private void LoadGrid()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable dtReq = PurCtl.GetPurchaseInvoice(0);
        gvPurchaseInvoice.DataSource = dtReq;
        gvPurchaseInvoice.DataBind();
    }
}