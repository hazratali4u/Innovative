using System;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;
using System.Data;

public partial class Forms_frmDesignation : System.Web.UI.Page
{
    SLASHCodesController CodesCtl = new SLASHCodesController();
    private static int m_Designation_ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadGrid();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDesignation.Text.Length > 0)
        {
            if (btnSave.Text == "Save")
            {
                string msg = CodesCtl.InsertSlashCodes("", Constants.SaleForce, txtDesignation.Text, 1, true);
                if (msg == "Record Inserted")
                {
                    lblError.Text = "Designation inserted successfully.";
                }
                else
                {
                    lblError.Text = msg;
                }
            }
            else
            {
                string msg = "";
                msg = CodesCtl.UpdateSlashCodes(m_Designation_ID, "", Constants.SaleForce, txtDesignation.Text, 1, true, DateTime.Now);
                if (msg == "Record Updated")
                {
                    lblError.Text = "Designation updated successfully.";
                }
                else
                {
                    lblError.Text = msg;
                }
            }

            this.LoadGrid();
            txtDesignation.Text = "";
            btnSave.Text = "Save";
        }
        else
        {
            lblError.Text = "Please enter Designation";
        }
    }

    /// <summary>
    /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvDesignation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        m_Designation_ID = int.Parse(gvDesignation.Rows[e.NewEditIndex].Cells[0].Text);
        txtDesignation.Text = gvDesignation.Rows[e.NewEditIndex].Cells[1].Text.Replace("&nbsp;", "");
        btnSave.Text = "Update";
    }

    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvDesignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string result = CodesCtl.UpdateSlashCodes(Convert.ToInt32(gvDesignation.Rows[e.RowIndex].Cells[0].Text), "", Constants.SaleForce, txtDesignation.Text, 1, false, DateTime.Now);
        this.LoadGrid();
    }

    /// <summary>
    /// Sets PageIndex Of SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void gvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvDesignation.PageIndex = e.NewPageIndex;
        this.LoadGrid();
    }

    private void LoadGrid()
    {
        DataTable dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.SaleForce, null, Constants.IntNullValue, true);
        gvDesignation.DataSource = dt;
        gvDesignation.DataBind();

        if (gvDesignation.Rows.Count > 0)
        {
            Panel1.Visible = true;
        }
    }

}