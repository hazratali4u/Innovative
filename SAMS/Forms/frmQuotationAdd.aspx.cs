using System;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmQuotationAdd : Page
{
    DataControl _dc = new DataControl();
  
    private static int RowNo;
    DataTable _requisitionSku;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateTable();
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadStore();
           
            
             if (Session["RequisitionID"] != null)
            {
                LoadRequisitionDetail();
            }
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreateTable()
    {
        _requisitionSku = new DataTable();
        _requisitionSku.Columns.Add("SKU_ID", typeof(int));
        _requisitionSku.Columns.Add("SKU_Name", typeof(string));
        _requisitionSku.Columns.Add("Quantity", typeof(decimal));
        _requisitionSku.Columns.Add("PRICE", typeof(decimal));
        this.Session.Add("RequisitionSKU", _requisitionSku);

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

    private void LoadStore()
    {
        DistributorController mController = new DistributorController();
        int LocationID = 0;
        if (ddlLocation.Items.Count > 0)
        {
            LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
        }
        DataTable dt = mController.GetStore(Constants.IntNullValue, LocationID);
        clsWebFormUtil.FillDropDownList(this.ddlStore, dt, "STORE_ID", "STORE_NAME", true);
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStore();
       
    }

 
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSupplier, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        _requisitionSku = (DataTable)this.Session["RequisitionSKU"];
        gvProduct.DataSource = _requisitionSku;
        gvProduct.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (gvProduct.Rows.Count > 0)
        //{
        //    RequisitionSKU = (DataTable)Session["RequisitionSKU"];
        //    PurchaseController PurCtl = new PurchaseController();
        //    if (btnSave.Text == "Save")
        //    {
        //        DataTable dtAllQuality = Session["dtAllQuality"] as DataTable;
        //        if (PurCtl.InsertQuotation(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlStore.SelectedValue), Convert.ToDateTime(txtDate.Text), txtOriginator.Text, txtRemarks.Text, Convert.ToInt32(Session["UserID"]), RequisitionSKU))
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Requisition saved successfully.');window.location.href = 'frmPurchaseRequisition.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
        //        }
        //    }
        //    else
        //    {
        //        DataTable dtAllQuality = Session["dtAllQuality"] as DataTable;
        //        if (PurCtl.UpdateQuotation(Convert.ToInt64(Session["RequisitionID"]), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlStore.SelectedValue), Convert.ToDateTime(txtDate.Text), txtOriginator.Text, txtRemarks.Text, Convert.ToInt32(Session["UserID"]), false, RequisitionSKU))
        //        {
        //            Session.Remove("RequisitionID");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Requisition updated successfully.');window.location.href = 'frmPurchaseRequisition.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
        //        }
        //    }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No product found');", true);
        //}
    }

    private void LoadRequisitionDetail()
    {
        PurchaseController purCtl = new PurchaseController();
        DataSet ds = purCtl.GetRequisition(1, Convert.ToInt64(Session["RequisitionID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtOriginator.Text = ds.Tables[0].Rows[0]["ORIGINATOR_NAME"].ToString();
            ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["DISTRIBUTOR_ID"].ToString();
            ddlStore.SelectedValue = ds.Tables[0].Rows[0]["STORE_ID"].ToString();
            
            txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
            ddlSupplier.SelectedValue = ds.Tables[0].Rows[0]["PRINCIPAL_ID"].ToString();
            btnSave.Text = "Update";
        }
        
        this.Session.Add("RequisitionSKU", _requisitionSku);
        LoadGird();
       
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("RequisitionID");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "window.location.href = 'frmPurchaseRequisition.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
    }

    #region SKU Quality PPopup
  
   
   
    //private void LoadSKUQC()
    //{
    //    DataTable dt = mSKUController.GetSKUQuality(Convert.ToInt32(ddlProduct.SelectedValue), Constants.LongNullValue);
    //    DataTable dtQuality = ViewState["dtQuality"] as DataTable;
    //    dtQuality.Rows.Clear();
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        DataRow drQuality = dtQuality.Rows.Add();
    //        drQuality["SKU_ID"] = dr["SKU_ID"];
    //        drQuality["QUALITY_VALUE"] = dr["QUALITY_VALUE"];
    //        drQuality["QUALITY_DESCRIPTION"] = dr["QUALITY_DESCRIPTION"];
    //    }
    //    gvQuality.DataSource = dtQuality;
    //    gvQuality.DataBind();
    //    ViewState["dtQuality"] = dtQuality;
    //    lblSKUName.Text = ddlProduct.SelectedItem.Text;
    //}
    #endregion
    protected void btnQC_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
    }
}