using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From to Assign Towns To Locations.
/// </summary>
public partial class Forms_pr_frmTownAssignment : System.Web.UI.Page
{
    GeoHierarchyController GeoCtl = new GeoHierarchyController();

    /// <summary>
    /// Page_Load Function Populates All Combos and ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadTier();
            this.LoadUnAssignTown();
            this.LoadAssignTown();

        }
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadTier()
    {
        DataTable dt = GeoCtl.GetTier(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddlTier, dt, "TIER_ID", "TIER_NAME");

    }
        
    /// <summary>
    /// Loads UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    private void LoadUnAssignTown()
    {
        if (ddlTier.Items.Count > 0)
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.GetAssignSaleForce(0, int.Parse(ddlTier.SelectedValue.ToString()));
            clsWebFormUtil.FillListBox(lstUnAssignTown, dt, 0, 1, true);
        }
        else
        {
            lstUnAssignTown.Items.Clear();
        }

    }
    
    /// <summary>
    /// Loads Assigned Towns To Assigned ListBox
    /// </summary>
    private void LoadAssignTown()
    {

        if (ddlTier.Items.Count > 0)
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.GetAssignSaleForce(1, int.Parse(ddlTier.SelectedValue.ToString()));
            clsWebFormUtil.FillListBox(lstAssignTown, dt, 0, 1, true);
        }
        else
        {
            lstAssignTown.Items.Clear();
        }
    }

    /// <summary>
    /// Loads Assigned Towns To Assigned ListBox And UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddlTier_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }

    
    /// <summary>
    /// Inserts Towns Assignment To Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        DistributorTownController mTownController = new DistributorTownController();
        for (int i = 0; i < lstUnAssignTown.Items.Count; i++)
        {
            if (lstUnAssignTown.Items[i].Selected == true && ddlTier.Items.Count > 0)
            {
                mTownController.InsertTierAssignment(int.Parse(ddlTier.SelectedValue.ToString()), int.Parse(lstUnAssignTown.Items[i].Value),Convert.ToInt32(Session["UserID"]), true);
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Inserts All Towns Assignment To Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignAll_Click(object sender, EventArgs e)
    {
        DistributorTownController mTownController = new DistributorTownController();
        if (ddlTier.Items.Count > 0)
        {
            for (int i = 0; i < lstUnAssignTown.Items.Count; i++)
            {                
                mTownController.InsertTierAssignment(int.Parse(ddlTier.SelectedValue.ToString()), int.Parse(lstUnAssignTown.Items[i].Value), Convert.ToInt32(Session["UserID"]), true);
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Deletes Towns Assignment To Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssign_Click(object sender, EventArgs e)
    {
        DistributorTownController mTownController = new DistributorTownController();
        if (ddlTier.Items.Count > 0)
        {
            for (int i = 0; i < lstAssignTown.Items.Count; i++)
            {
                mTownController.InsertTierAssignment(int.Parse(ddlTier.SelectedValue.ToString()), int.Parse(lstAssignTown.Items[i].Value), Convert.ToInt32(Session["UserID"]), false);
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Deletes All Towns Assignment To Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignAll_Click(object sender, EventArgs e)
    {
        DistributorTownController mTownController = new DistributorTownController();
        for (int i = 0; i < lstAssignTown.Items.Count; i++)
        {
            if (lstAssignTown.Items[i].Selected == true && ddlTier.Items.Count > 0)
            {                
                mTownController.InsertTierAssignment(int.Parse(ddlTier.SelectedValue.ToString()), int.Parse(lstAssignTown.Items[i].Value), Convert.ToInt32(Session["UserID"]), false);
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
}
