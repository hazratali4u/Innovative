using System;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmGRNAdd : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable GRNSKU;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadDistributor();            
            this.LoadPrincipal();
            this.GetRequisition();
            if (Session["GRNID"] != null)
            {
                LoadGRNDetail();
            }
            else
            {
                this.LoadRequisitionDetail();
            }
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
    
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        GRNSKU = (DataTable)this.Session["GRNSKU"];
        gvProduct.DataSource = GRNSKU;
        gvProduct.DataBind();        
    }
        
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvProduct.Rows.Count > 0)
        {
            decimal totalOrdered = 0;
            decimal totalReceived = 0;
            decimal totalCurrent = 0;
            DataTable GRNSKUDetail = new DataTable();
            GRNSKUDetail.Columns.Add("SKU_ID", typeof(int));
            GRNSKUDetail.Columns.Add("QUANTITY", typeof(decimal));
            foreach (GridViewRow gvr in gvProduct.Rows)
            {
                TextBox txtOrdered = gvr.FindControl("txtOrdered") as TextBox;
                TextBox txtCurrent = gvr.FindControl("txtCurrent") as TextBox;
                TextBox txtReceived = gvr.FindControl("txtReceived") as TextBox;

                if (Convert.ToDecimal(dc.chkNull_0(txtCurrent.Text)) > 0)
                {
                    if (Convert.ToDecimal(dc.chkNull_0(txtCurrent.Text)) + Convert.ToDecimal(dc.chkNull_0(txtReceived.Text)) > Convert.ToDecimal(dc.chkNull_0(txtOrdered.Text)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Current received is greater than " + (Convert.ToDecimal(dc.chkNull_0(txtOrdered.Text)) - Convert.ToDecimal(dc.chkNull_0(txtReceived.Text))).ToString() + " for " + gvr.Cells[1].Text + ".');", true);
                        return;
                    }


                    DataRow dr = GRNSKUDetail.NewRow();
                    dr["SKU_ID"] = Convert.ToInt32(dc.chkNull_0(gvr.Cells[0].Text));
                    dr["QUANTITY"] = Convert.ToDecimal(dc.chkNull_0(txtCurrent.Text));
                    GRNSKUDetail.Rows.Add(dr);
                    totalOrdered += Convert.ToDecimal(dc.chkNull_0(txtOrdered.Text));
                    totalReceived += Convert.ToDecimal(dc.chkNull_0(txtReceived.Text));
                    totalCurrent += Convert.ToDecimal(dc.chkNull_0(txtCurrent.Text));
                }
            }

            if (totalCurrent < 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Current received quantity must be greater than 0.');", true);
                return;
            }

            PurchaseController PurCtl = new PurchaseController();
            if (btnSave.Text == "Save")
            {
                if (PurCtl.InsertGRN(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt64(ddlRequisition.SelectedValue), txtReviewedBy.Text, txtApprovedBy.Text, Convert.ToInt32(Session["UserID"]), GRNSKUDetail))
                {
                    if (totalOrdered == totalReceived + totalCurrent)
                    {
                        PurCtl.UpdateRequisition(Convert.ToInt64(ddlRequisition.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Constants.IntNullValue, Constants.DateNullValue, null, null, null, null, 1, Convert.ToInt32(Session["UserID"]), false);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "GRN saved successfully.');window.location.href = 'frmGRN.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
                }
            }
            else
            {
                if (PurCtl.UpdateGRN(Convert.ToInt64(Session["GRNID"]), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt64(ddlRequisition.SelectedValue), txtReviewedBy.Text, txtApprovedBy.Text,Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), false, GRNSKUDetail))
                {
                    Session.Remove("GRNID");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "GRN updated successfully.');window.location.href = 'frmGRN.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No product found');", true);
        }
    }

    private void LoadGRNDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataSet ds = PurCtl.GetGRN(1, Convert.ToInt64(Session["GRNID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSupplier.SelectedValue = ds.Tables[0].Rows[0]["PRINCIPAL_ID"].ToString();
            ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["DISTRIBUTOR_ID"].ToString();
            GetRequisition();
            ddlRequisition.SelectedValue = ds.Tables[0].Rows[0]["REQUISITION_MASTER_ID"].ToString();
            txtReviewedBy.Text = ds.Tables[0].Rows[0]["REVIEWED_BY"].ToString();
            txtApprovedBy.Text = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();
            btnSave.Text = "Update";
        }
        GRNSKU = ds.Tables[1];        
        this.Session.Add("GRNSKU", GRNSKU);
        LoadGird();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("GRNID");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "window.location.href = 'frmGRN.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
    }

    private void GetRequisition()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable GRNSKU = PurCtl.GetRequisition(2,0,Convert.ToInt32(ddlLocation.SelectedValue),Convert.ToInt32(ddlSupplier.SelectedValue));
        clsWebFormUtil.FillDropDownList(this.ddlRequisition, GRNSKU, "REQUISITION_MASTER_ID", "REQUISITION_MASTER_ID",true);
        if (GRNSKU.Rows.Count > 0)
        {
            ddlLocation.SelectedValue = GRNSKU.Rows[0]["DISTRIBUTOR_ID"].ToString();
        }
        Session.Add("GRNSKU", GRNSKU);
    }

    private void LoadRequisitionDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        if (ddlRequisition.Items.Count > 0)
        {
            DataSet ds = PurCtl.GetRequisition(1, Convert.ToInt64(ddlRequisition.SelectedValue));

            GRNSKU = ds.Tables[1];
            this.Session.Add("GRNSKU", GRNSKU);
            LoadGird();
        }
    }

    protected void ddlRequisition_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequisitionDetail();
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSupplier, m_dt, 0, 1, true);
    }

    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRequisition();
        LoadRequisitionDetail();
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRequisition();
        LoadRequisitionDetail();
    }

    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Text.StringBuilder atrCheckReceived = new System.Text.StringBuilder();
            TextBox txtOrdered = e.Row.FindControl("txtOrdered") as TextBox;
            TextBox txtReceived = e.Row.FindControl("txtReceived") as TextBox;
            TextBox txtCurrent = e.Row.FindControl("txtCurrent") as TextBox;
            atrCheckReceived.Append("CheckReceived(");
            atrCheckReceived.Append(txtOrdered.ClientID);
            atrCheckReceived.Append(", ");
            atrCheckReceived.Append(txtReceived.ClientID);
            atrCheckReceived.Append(",");
            atrCheckReceived.Append(txtCurrent.ClientID + ");");
            txtCurrent.Attributes.Add("onblur", atrCheckReceived.ToString());
        }
    }
}