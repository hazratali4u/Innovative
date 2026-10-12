using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Rollback Order, Invoice, Sale Return And Realized Cheque
/// </summary>
public partial class Forms_frmRollBackForm : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();            
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    
    /// <summary>
    /// Loads Rollback Order, Invoice And Sale Return Data To Grid
    /// </summary>
    private void LoadRollbackDocument()
    {
        OrderEntryController or = new OrderEntryController();
        DataTable dtOrder = or.SelectRollBackDocument(int.Parse(drpDistributor.SelectedValue.ToString()),Constants.IntNullValue,
            Constants.IntNullValue,1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        GrdOrder.DataSource = dtOrder;
        GrdOrder.DataBind();
    }
        
    /// <summary>
    /// Rollbacks Order, Invoice, Sale Return And Realized Cheque
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPost_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow dr in GrdOrder.Rows)
        {
            CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
            if (ChbInvoice.Checked == true)
            {
                OrderEntryController ORD = new OrderEntryController();
                DataControl dc = new DataControl();
                ORD.UpdateRollBackDocument(Convert.ToInt64(GrdOrder.DataKeys[dr.RowIndex].Values["Document_ID"]), 1, 1);
            }
        }
        this.LoadRollbackDocument();
    }

    /// <summary>
    /// Loads Order, Invoice, Sale Return And Realized Cheque Data To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnGetOrder_Click(object sender, EventArgs e)
    {
        GrdOrder.Visible = true;        
        this.LoadRollbackDocument();
    }
}
