using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU Hierarchy
/// </summary>
public partial class frmStore : System.Web.UI.Page
{
    DistributorController mController = new DistributorController();
    static int StoreId;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadDistributor();
            this.LoadStore();
        }
    }

    /// <summary>
    /// Loads Categories To Store Grid On Store Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpStorePrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadStore();
    }
        
    private void LoadStore()
    {
        DataTable dt = mController.GetStore(Constants.IntNullValue, Convert.ToInt32(ddlLocation.SelectedValue));
        GrdStore.DataSource = dt;
        GrdStore.DataBind();
    }

    /// <summary>
    /// Save Or Updates a Store
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveStore_Click(object sender, EventArgs e)
    {        
        if (btnSaveStore.Text == "Save")
        {
            if (txtStoreName.Text.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter Store');", true);
                return;
            }
            if (mController.InsertStore(Convert.ToInt32(ddlLocation.SelectedValue), txtStoreCode.Text, txtStoreName.Text,Convert.ToInt32(Session["UserID"])))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Store added successfully.');", true);
                txtStoreCode.Text = "";
                txtStoreName.Text = "";
                this.LoadStore();
            }           
        }
        else if (btnSaveStore.Text == "Update")
        {
            if (txtStoreName.Text.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Must Enter Store');", true);
                return;
            }
            if (mController.UpdateStore(StoreId, Convert.ToInt32(ddlLocation.SelectedValue), txtStoreCode.Text, txtStoreName.Text, true))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Store updated successfully.');", true);
                btnSaveStore.Text = "Save";
                txtStoreCode.Text = "";
                txtStoreName.Text = "";
                this.LoadStore();
            }
        }
    }
    
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadStore();
    }

    /// <summary>
    /// Sets Store Data For Edit. This Function Runs When An Existing Store Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdStore_RowEditing(object sender, GridViewEditEventArgs e)
    {
        StoreId = int.Parse(GrdStore.Rows[e.NewEditIndex].Cells[0].Text);
        ddlLocation.SelectedValue = GrdStore.Rows[e.NewEditIndex].Cells[1].Text;
        txtStoreCode.Text = GrdStore.Rows[e.NewEditIndex].Cells[2].Text;
        txtStoreName.Text = GrdStore.Rows[e.NewEditIndex].Cells[3].Text;
        btnSaveStore.Text = "Update";
    }

    /// <summary>
    /// Deletes A Store
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdStore_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ClearAll();
        StoreId = int.Parse(GrdStore.Rows[e.RowIndex].Cells[0].Text);
        if (mController.UpdateStore(StoreId, Convert.ToInt32(ddlLocation.SelectedValue), null, null, false))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Store deleted successfully.');", true);
            this.LoadStore();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Some error occured.');", true);
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ClearAll()
    {
        txtStoreCode.Text = "";
        txtStoreName.Text = "";
        btnSaveStore.Text = "Save";
    }
}
