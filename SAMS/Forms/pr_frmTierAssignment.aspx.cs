using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From to Assign Towns To Locations.
/// </summary>
public partial class Forms_pr_frmTierAssignment : System.Web.UI.Page
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
            this.LoadRegion();
            this.LoadTownZone();
            this.LoadTownTerritory();            
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
        clsWebFormUtil.FillDropDownList(ddlTier, dt, "TIER_ID", "TIER_NAME", true);
    }
        
    /// <summary>
    /// Loads UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    private void LoadUnAssignTown()
    {
        if (ddlTier.Items.Count > 0 && DrpTerritory.Items.Count > 0)
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.SelectAssignTown(Convert.ToInt32(DrpTerritory.SelectedValue), int.Parse(ddlTier.SelectedValue.ToString()), 0);
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

        if (ddlTier.Items.Count > 0 && DrpTerritory.Items.Count > 0)
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.SelectAssignTown(Convert.ToInt32(DrpTerritory.SelectedValue), int.Parse(ddlTier.SelectedValue.ToString()), 1);
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
    /// Loads Zones To Zone Combo, Territories To Territory Combo, Assigned Towns To Assigned ListBox And UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadUnAssignTown();
        this.LoadAssignTown();

    }
    
    /// <summary>
    /// Loads Territories To Territory Combo, Assigned Towns To Assigned ListBox And UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Loads Assigned Towns To Assigned ListBox And UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddTerritory_SelectedIndexChanged(object sender, EventArgs e)
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
                mTownController.InsertDistributorTown(int.Parse(ddlTier.SelectedValue.ToString()), int.Parse(lstUnAssignTown.Items[i].Value), bool.Parse("True"), 1, null, int.Parse(this.Session["UserId"].ToString()));
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

                mTownController.InsertDistributorTown(int.Parse(ddlTier.SelectedValue.ToString()), int.Parse(lstUnAssignTown.Items[i].Value), bool.Parse("True"), 1, null, int.Parse(this.Session["UserId"].ToString()));

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

                mTownController.DeletedDistributorTown(int.Parse(ddlTier.SelectedValue.ToString()), int.Parse(lstAssignTown.Items[i].Value));

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
                mTownController.DeletedDistributorTown(int.Parse(ddlTier.SelectedValue.ToString()), int.Parse(lstAssignTown.Items[i].Value));
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Loads Zones To Zone Combo,Territories To Territory Combo And Towns To Town Grid Of Town Tab.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpTownRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTownZone();
        this.LoadTownTerritory();
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }

    protected void LoadRegion()
    {
        DataTable dt = GeoCtl.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpTownRegion, dt, 0, 4, true);
    }

    /// <summary>
    /// Loads Territories To Territory Combo And Towns To Town Grid Of Town Tab.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpTownZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTownTerritory();
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }

    /// <summary>
    /// Loads Zones To Zone Combo Of Town Tab.
    /// </summary>
    private void LoadTownZone()
    {
        if (DrpTownRegion.Items.Count > 0)
        {
            DataTable dt = GeoCtl.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTownRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTownZone, dt, 0, 4, true);
        }
    }

    /// <summary>
    /// Loads Towns To Town Grid of Town Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }

    /// <summary>
    /// Loads Territories To Territory Grid Of Town Tab.
    /// </summary>
    private void LoadTownTerritory()
    {
        if (DrpTownZone.Items.Count > 0)
        {
            DataTable dt = GeoCtl.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTownZone.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTerritory, dt, 0, 4, true);
        }
    }
}
