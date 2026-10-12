using System;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;
using System.Web.UI;

public partial class Forms_frmQC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("QCID");
            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmQCAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add QC</a>";
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvQC_RowEditing(object sender, GridViewEditEventArgs e)
    {
        long QCID = Convert.ToInt64(gvQC.Rows[e.NewEditIndex].Cells[0].Text);
        Session.Add("QCID", QCID);
        Response.Redirect("frmQCAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());            

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvQC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseController PurCtl = new PurchaseController();
        string QCID = gvQC.Rows[e.RowIndex].Cells[0].Text;

        if (PurCtl.UpdateQC(Convert.ToInt64(QCID),false,Convert.ToInt32(Session["UserID"])))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "QC deleted successfully.')", true);
            this.LoadGrid();
        }
    }

    private void LoadGrid()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable dtReq = PurCtl.GetQC(Constants.LongNullValue,Constants.LongNullValue,Constants.IntNullValue,0);
        gvQC.DataSource = dtReq;
        gvQC.DataBind();
    }
}