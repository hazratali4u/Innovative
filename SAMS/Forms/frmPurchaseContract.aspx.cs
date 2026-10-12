using System;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;
using System.Web.UI;

public partial class Forms_frmPurchaseContract : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("PurchaseContractID");
            Session.Remove("dtAllQuality");
            Session.Remove("PurchaseContractSKU");
            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmPurchaseContractAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add Purchase Contract</a>";
            this.LoadGrid();
        }
    }
    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>  
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvPurchaseContract_RowEditing(object sender, GridViewEditEventArgs e)
    {
        long PurchaseContractID = Convert.ToInt64(gvPurchaseContract.Rows[e.NewEditIndex].Cells[0].Text);
        Session.Add("PurchaseContractID", PurchaseContractID);
        Response.Redirect("frmPurchaseContractAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());            
    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvPurchaseContract_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseContractController PurCtl = new PurchaseContractController();
        string PurchaseContractID = gvPurchaseContract.Rows[e.RowIndex].Cells[0].Text;
          
        if (PurCtl.UpdatePurchaseContract(Convert.ToInt32(PurchaseContractID), Constants.IntNullValue, Constants.IntNullValue,Constants.DateNullValue,Constants.DateNullValue,null, Constants.IntNullValue,  Constants.IntNullValue,Constants.DateNullValue,null,null,true));
        {            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Purchase Contract deleted successfully.')", true);
            this.LoadGrid();
        }
    }
    private void LoadGrid()
    {
        PurchaseContractController PurCtl = new PurchaseContractController();
        DataTable dtPurchaseContract = PurCtl.SelectPURCHASE_CONTRACT(0,0);
        gvPurchaseContract.DataSource = dtPurchaseContract;
        gvPurchaseContract.DataBind();
    }
}