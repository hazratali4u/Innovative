using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using System.Collections.Generic;

public partial class pr_frmTier : System.Web.UI.Page
{
    GeoHierarchyController GeoCtl = new GeoHierarchyController();
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
            if (hdnTierID.Value == "" || hdnTierID.Value == null)
            {
                if (GeoCtl.InsertTier(this.txtName.Text, Convert.ToDecimal(dc.chkNull_0(txtBike.Text)), txtFuel.Text, Convert.ToDecimal(dc.chkNull_0(txtCommission.Text)), Convert.ToDecimal(dc.chkNull_0(txtTarget1.Text)), Convert.ToDecimal(dc.chkNull_0(txtTarget2.Text)), Convert.ToDecimal(dc.chkNull_0(txtTarget3.Text)), Convert.ToDecimal(dc.chkNull_0(txtTarget4.Text)), Convert.ToInt32(Session["UserID"])))
                {
                    Response.Redirect("pr_frmTier.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
                    return;
                }
            }
            else
            {
                if (GeoCtl.UpdateTier(Convert.ToInt32(hdnTierID.Value), this.txtName.Text, Convert.ToDecimal(dc.chkNull_0(txtBike.Text)), txtFuel.Text, Convert.ToDecimal(dc.chkNull_0(txtCommission.Text)), Convert.ToDecimal(dc.chkNull_0(txtTarget1.Text)), Convert.ToDecimal(dc.chkNull_0(txtTarget2.Text)), Convert.ToDecimal(dc.chkNull_0(txtTarget3.Text)), Convert.ToDecimal(dc.chkNull_0(txtTarget4.Text)), Convert.ToInt32(Session["UserID"]),true))
                {
                    Response.Redirect("pr_frmTier.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
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
        Response.Redirect("pr_frmTier.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    private void ResetForm()
    {
        txtName.Text = "";
        txtCommission.Text = "";
        txtBike.Text = "";
        txtFuel.Text = "";
        fillrptTier(Constants.IntNullValue);
        hdnTierID.Value = string.Empty;
        if (Session["Add"] != null)
        {
            pnlTierContent.Visible = true;
            pnlTierGrid.Visible = false;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("TIER_ID");
        }
        else if (Session["Edit"] != null)
        {
            pnlTierContent.Visible = true;
            pnlTierGrid.Visible = false;

            DataTable dt = GeoCtl.GetTier(Convert.ToInt32(Session["TIER_ID"]));
            hdnTierID.Value = dt.Rows[0]["TIER_ID"].ToString();
            txtName.Text = dt.Rows[0]["TIER_NAME"].ToString();
            txtBike.Text = dt.Rows[0]["BIKE_MAINTENANCE"].ToString();
            txtFuel.Text = dt.Rows[0]["FUEL"].ToString();            
            txtCommission.Text = dt.Rows[0]["COMMISSION"].ToString();
            txtTarget1.Text = dt.Rows[0]["INCENTIVE1"].ToString();
            txtTarget2.Text = dt.Rows[0]["INCENTIVE2"].ToString();
            txtTarget3.Text = dt.Rows[0]["INCENTIVE3"].ToString();
            txtTarget4.Text = dt.Rows[0]["INCENTIVE4"].ToString();

            imgTier.ImageUrl = "../images/btn-update.png";

            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("TIER_ID");
        }
        else
        {
            pnlTierContent.Visible = false;
            pnlTierGrid.Visible = true;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("TIER_ID");
        }
    }   
    protected void btnShowpnlTierContent_Click(object sender, EventArgs e)
    {
        Session.Add("Add", "Add");
        Response.Redirect("pr_frmTier.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    protected void rptTier_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Session.Add("Edit", "Edit");
            Session.Add("TIER_ID", e.CommandArgument.ToString());
            Response.Redirect("pr_frmTier.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
        }
        else if (e.CommandName == "delete")
        {
            if (GeoCtl.UpdateTier(Convert.ToInt32(e.CommandArgument), null, Constants.DecimalNullValue, null, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, Convert.ToInt32(Session["UserID"]), false))
            {
                fillrptTier(Constants.IntNullValue);
            }
        }
    }
    private void fillrptTier(int TIER_ID)
    {
        DataTable dt = GeoCtl.GetTier(TIER_ID);
        rptTier.DataSource = dt;
        rptTier.DataBind();
    }
}