
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Add, Edit Customer
/// </summary>
public partial class Forms_frmDistributorCustomer : System.Web.UI.Page
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
           // this.LoadTown();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            btnSearch.Attributes.Add("onclick", "return SearchRecord()");
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtRegdate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, 0, 2, true);
    }
    
    /// <summary>
    /// Loads Towns To Town Combo
    ///// </summary>
    //protected void LoadTown()
    //{
    //    if (DrpDistributor.Items.Count > 0)
    //    {
    //        GeoHierarchyController gController = new GeoHierarchyController();
    //        DataTable dt = gController.SelectGeoHierarchy(int.Parse(DrpDistributor.SelectedValue.ToString()));
    //        clsWebFormUtil.FillDropDownList(DrpTown, dt, 0, 1, true);
    //    }
    //}

    /// <summary>
    /// Loads Customers To Customer Grid
    /// </summary>
    private void LoadCustomer()
    {
        if (DrpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.UspSelectCustomer(int.Parse(DrpDistributor.SelectedValue.ToString()), ddSearchType.SelectedValue.ToString(), txtSeach.Text);
            this.Grid_users.DataSource = dt;
            this.Grid_users.DataBind();
        }
    }

    /// <summary>
    /// Sets Customer Data For Edit. This Function Runs When An Existing Customer Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.Session.Add("CustomerId", long.Parse(Grid_users.Rows[e.NewEditIndex].Cells[0].Text));
        DrpDistributor.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[1].Text;
        //this.LoadTown();        
        txtCustomerName.Text = Grid_users.Rows[e.NewEditIndex].Cells[3].Text.Replace("amp;", "");
        txtContactPerson.Text = Grid_users.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
        txtPhoneNo.Text = Grid_users.Rows[e.NewEditIndex].Cells[5].Text.Replace("&nbsp;", "");
        txtAddress.Text = Grid_users.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;", "");
        txtIsRegister.Text = Grid_users.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", "");        
        chkIsActive.Checked = bool.Parse(Grid_users.Rows[e.NewEditIndex].Cells[9].Text);
        txtRegdate.Text = Grid_users.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
        txtCNIC.Text = Grid_users.Rows[e.NewEditIndex].Cells[11].Text.Replace("&nbsp;", "");
        txtNTN.Text = Grid_users.Rows[e.NewEditIndex].Cells[12].Text.Replace("&nbsp;", "");
        txtCreditLimit.Text = Grid_users.Rows[e.NewEditIndex].Cells[13].Text.Replace("&nbsp;", "");
        txtAllowedDays.Text = Grid_users.Rows[e.NewEditIndex].Cells[14].Text.Replace("&nbsp;", "");
        btnSave.Text = "Update";
        this.SetTableSorter();
    }

    /// <summary>
    /// Sets PageIndex Of Customer Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void Grid_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_users.PageIndex = e.NewPageIndex;
        this.LoadCustomer();
        this.SetTableSorter();
    }

    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        CustomerDataController mController = new CustomerDataController();
        bool IsRegister = false;
        if (txtIsRegister.Text.Length > 0)
        {
            IsRegister = true;
        }
        if (btnSave.Text == "Save")
        {
            SETTINGS_TABLE_Controller mSettingsTableControl = new SETTINGS_TABLE_Controller();
            DataTable dtSettingsTable = mSettingsTableControl.Select_SETTINGS_TABLE("CUSTOMER", "CUSTOMER_ID", int.Parse(DrpDistributor.SelectedValue.ToString()));

            if (dtSettingsTable.Rows.Count > 0)
            {
                long CustomerId = long.Parse(dtSettingsTable.Rows[0]["Value"].ToString()) + 1;
                string StrCode = "";


                if (CustomerId.ToString().Length == 1)
                {
                    StrCode = "OT0000" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 2)
                {
                    StrCode = "OT000" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 3)
                {
                    StrCode = "OT00" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 4)
                {
                    StrCode = "OT0" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 5)
                {
                    StrCode = "OT" + CustomerId.ToString();
                }
               
                mController.InsertCustomer(CustomerId, IsRegister, chkIsActive.Checked,
                1, 1,1,1, 1,Constants.IntNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), txtIsRegister.Text, txtContactPerson.Text,
                txtPhoneNo.Text, "", StrCode, txtCustomerName.Text, txtAddress.Text, DateTime.Parse(txtRegdate.Text), 1, 1, txtCNIC.Text, txtNTN.Text,decimal.Parse(dc.chkNull_0( txtCreditLimit.Text)),int.Parse(dc.chkNull_0(txtAllowedDays.Text)));
                this.Session.Add("CustomerId", CustomerId);
            }
        }
        else
        {
            mController.UpdateCustomer(long.Parse(this.Session["CustomerId"].ToString()), IsRegister, chkIsActive.Checked,
            1, 1,1, 1,1,Constants.IntNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), txtIsRegister.Text, txtContactPerson.Text,
            txtPhoneNo.Text, "", null, txtCustomerName.Text, txtAddress.Text, Constants.DateNullValue, 1, 1, txtCNIC.Text, txtNTN.Text, decimal.Parse(dc.chkNull_0( txtCreditLimit.Text)),int.Parse(dc.chkNull_0(txtAllowedDays.Text)));
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Record Update');", true);
        this.ClearAll();
    }

    /// <summary>
    /// Clears All Controls Through ClearAll() Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }

    /// <summary>
    /// Filters Customer From Customer Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.LoadCustomer();
        this.SetTableSorter();
    }

    /// <summary>
    /// Set Customer Grid For JQuery Sorting
    /// </summary>
    private void SetTableSorter()
    {
        if (Grid_users.Rows.Count > 1)
        {
            Grid_users.UseAccessibleHeader = true;
            Grid_users.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    /// <summary>
    /// Clears All  Constrols
    /// </summary>
    private void ClearAll()
    {
        txtCustomerName.Text = "";
        txtContactPerson.Text = "";
        txtAddress.Text = "";
        txtPhoneNo.Text = "";
        txtSeach.Text = "";
        txtIsRegister.Text = "";
        txtNTN.Text = string.Empty;
        txtCNIC.Text = string.Empty;
        btnSave.Text = "Save";
        Grid_users.DataSource = null;
        txtCreditLimit.Text = "";
        txtAllowedDays.Text = "";
        Grid_users.DataBind();

    }
}
