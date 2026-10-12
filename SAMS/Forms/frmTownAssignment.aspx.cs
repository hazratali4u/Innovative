using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From to Assign Towns To Locations.
/// </summary>
public partial class Forms_frmTownAssignment : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos and ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadUnAssignTown();
            this.LoadAssignTown();

        }
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 1, 3, true);

    }
        
    /// <summary>
    /// Loads UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    private void LoadUnAssignTown()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.SelectAssignTown(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), 0);
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

        if (drpDistributor.Items.Count > 0)
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.SelectAssignTown(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), 1);
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
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
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
            if (lstUnAssignTown.Items[i].Selected == true && drpDistributor.Items.Count > 0)
            {
                mTownController.InsertDistributorTown(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(lstUnAssignTown.Items[i].Value), bool.Parse("True"), 1, null, int.Parse(this.Session["UserId"].ToString()));
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
        if (drpDistributor.Items.Count > 0)
        {
            for (int i = 0; i < lstUnAssignTown.Items.Count; i++)
            {

                mTownController.InsertDistributorTown(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(lstUnAssignTown.Items[i].Value), bool.Parse("True"), 1, null, int.Parse(this.Session["UserId"].ToString()));

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
        if (drpDistributor.Items.Count > 0)
        {
            for (int i = 0; i < lstAssignTown.Items.Count; i++)
            {

                mTownController.DeletedDistributorTown(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(lstAssignTown.Items[i].Value));

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
            if (lstAssignTown.Items[i].Selected == true && drpDistributor.Items.Count > 0)
            {
                mTownController.DeletedDistributorTown(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(lstAssignTown.Items[i].Value));
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
}
