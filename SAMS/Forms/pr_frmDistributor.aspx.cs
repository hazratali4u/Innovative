using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class pr_frmDistributor : System.Web.UI.Page
{
    GeoHierarchyController GeoCtl = new GeoHierarchyController();
    DistributorController DistCtl = new DistributorController();
    DataControl dc = new DataControl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetForm();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text == "" || ddlTown.Items.Count < 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Either Tier name not entered or town not selected.');", true);
                return;
            }
            if (hdnDistributorID.Value == "" || hdnDistributorID.Value == null)
            {
                if (DistCtl.InsertDistributor2(this.txtName.Text, Convert.ToInt32(ddlTown.SelectedValue), Convert.ToInt32(Session["UserID"])))
                {
                    Response.Redirect("pr_frmDistributor.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
                    return;
                }
            }
            else
            {
                if (DistCtl.UpdateDistributor2(Convert.ToInt32(hdnDistributorID.Value), this.txtName.Text, Convert.ToInt32(ddlTown.SelectedValue), Convert.ToInt32(Session["UserID"]), true))
                {
                    Response.Redirect("pr_frmDistributor.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('" + ex.Message + "');", true);
        }
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        Response.Redirect("pr_frmDistributor.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    private void ResetForm()
    {
        txtName.Text = "";
        LoadTown();
        fillrptDistributor(Constants.IntNullValue,Constants.IntNullValue);
        hdnDistributorID.Value = string.Empty;
        if (Session["Add"] != null)
        {
            pnlDistributorContent.Visible = true;
            pnlDistributorGrid.Visible = false;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("DISTRIBUTOR_ID");
        }
        else if (Session["Edit"] != null)
        {
            pnlDistributorContent.Visible = true;
            pnlDistributorGrid.Visible = false;

            DataTable dt = DistCtl.GetDistributor2(Convert.ToInt32(Session["DISTRIBUTOR_ID"]),Constants.IntNullValue);
            hdnDistributorID.Value = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            txtName.Text = dt.Rows[0]["DISTRIBUTOR_NAME"].ToString();
            ddlTown.SelectedValue = dt.Rows[0]["GEO_ID"].ToString();

            imgDistributor.ImageUrl = "../images/btn-update.png";

            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("DISTRIBUTOR_ID");
        }
        else
        {
            pnlDistributorContent.Visible = false;
            pnlDistributorGrid.Visible = true;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("DISTRIBUTOR_ID");
        }
    }
    protected void btnShowpnlDistributorContent_Click(object sender, EventArgs e)
    {
        Session.Add("Add", "Add");
        Response.Redirect("pr_frmDistributor.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    protected void rptDistributor_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Session.Add("Edit", "Edit");
            Session.Add("DISTRIBUTOR_ID", e.CommandArgument.ToString());
            Response.Redirect("pr_frmDistributor.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
        }
        else if (e.CommandName == "delete")
        {
            if (DistCtl.UpdateDistributor2(Convert.ToInt32(e.CommandArgument), null, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), false))
            {
                fillrptDistributor(Constants.IntNullValue,Constants.IntNullValue);
            }
        }
    }
    private void fillrptDistributor(int DISTRIBUTOR_ID, int TOWN_ID)
    {
        DataTable dt = DistCtl.GetDistributor2(DISTRIBUTOR_ID, TOWN_ID);
        rptDistributor.DataSource = dt;
        rptDistributor.DataBind();
    }
    /// <summary>
    /// Loads Existing Towns To Town Grid of Town Tab
    /// </summary>
    protected void LoadTown()
    {
        DataTable dt = GeoCtl.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlTown, dt, 0, 4, true);
    }
}