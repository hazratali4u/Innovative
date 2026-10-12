using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommon.Classes;

public partial class Forms_frmPurchaseReturnOld : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadAccountHead();
            LoadDistributor();
        }
    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue,Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, Constants.DateNullValue);
        ddlSupplier.Items.Add(new ListItem("--------Select--------", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSupplier, m_dt, 0, 1);
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    private void LoadAccountHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.GetAssignAccountHead(Constants.IntNullValue, 52);
        clsWebFormUtil.FillDropDownList(ddlAccount, dt, 0, 4, true);
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // this.LoadGird();
    }

    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        //DataTable PurchasedPSKU = new DataTable();
        //PurchasedPSKU.Columns.Add("SKU_ID", typeof(int));
        //PurchasedPSKU.Columns.Add("QUANTITY", typeof(int));
        //PurchasedPSKU.Columns.Add("PRICE", typeof(decimal));
        //PurchasedPSKU.Columns.Add("DISCOUNT", typeof(decimal));
        //PurchasedPSKU.Columns.Add("GST", typeof(decimal));
        //PurchasedPSKU.Columns.Add("AMOUNT", typeof(decimal));

        //foreach (GridViewRow gvr in GrdPurchase.Rows)
        //{
        //    TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
        //    TextBox txtDiscount = gvr.FindControl("txtDiscount") as TextBox;
        //    TextBox txtRate = gvr.FindControl("txtRate") as TextBox;
        //    TextBox txtGross = gvr.FindControl("txtGross") as TextBox;
        //    TextBox txtGSTRate = gvr.FindControl("txtGSTRate") as TextBox;
        //    DateTime dtExpDate = Constants.DateNullValue;

        //    DataRow dr = PurchasedPSKU.NewRow();
        //    dr["SKU_ID"] = Convert.ToInt32(dc.chkNull_0(gvr.Cells[0].Text));
        //    dr["QUANTITY"] = Convert.ToInt32(dc.chkNull_0(gvr.Cells[3].Text));
        //    dr["PRICE"] = Convert.ToDecimal(dc.chkNull_0(txtRate.Text));
        //    dr["DISCOUNT"] = Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text));
        //    dr["AMOUNT"] = Convert.ToDecimal(dc.chkNull_0(txtGross.Text));
        //    dr["GST"] = Convert.ToDecimal(dc.chkNull_0(txtGSTRate.Text));
        //    PurchasedPSKU.Rows.Add(dr);
        //}

        //if (PurchasedPSKU.Rows.Count > 0)
        //{
        //    if (CalculatePurchase(DateTime.Parse(txtDate.Text), PurchasedPSKU))
        //    {
        //        txtRemarks.Text = string.Empty;
        //        txtAmount.Text = string.Empty;
        //        txtRefNo.Text = string.Empty;
        //        this.GetDocumentNo();
        //        this.LoadGRN();
        //        drpDocumentNo_SelectedIndexChanged(null, null);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Purchase Invoice saved successfully.');", true);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Some error occured.');", true);
        //    }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert(' At least one SKU must enter');", true);

        //}
    }

    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.GetDocumentNo();
        //this.LoadGRN();
        //drpDocumentNo_SelectedIndexChanged(null, null);
    }



    protected void GrdPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //TextBox txtRecQty = e.Row.FindControl("txtRecQty") as TextBox;
            //TextBox txtDiscount = e.Row.FindControl("txtDiscount") as TextBox;
            //TextBox txtRate = e.Row.FindControl("txtRate") as TextBox;
            //TextBox txtGross = e.Row.FindControl("txtGross") as TextBox;
            //TextBox txtGSTRate = e.Row.FindControl("txtGSTRate") as TextBox;
            //TextBox txtGSTAmount = e.Row.FindControl("txtGSTAmount") as TextBox;
            //TextBox txtGTotal = e.Row.FindControl("txtGTotal") as TextBox;

            //System.Text.StringBuilder atrCalculation = new System.Text.StringBuilder();

            //atrCalculation.Append("DoCalculation(");
            //atrCalculation.Append(txtRecQty.ClientID);
            //atrCalculation.Append(", ");
            //atrCalculation.Append(txtRate.ClientID);
            //atrCalculation.Append(", ");
            //atrCalculation.Append(txtGross.ClientID);
            //atrCalculation.Append(",");
            //atrCalculation.Append(txtDiscount.ClientID);
            //atrCalculation.Append(", ");
            //atrCalculation.Append(txtGSTRate.ClientID);
            //atrCalculation.Append(",");
            //atrCalculation.Append(txtGSTAmount.ClientID);
            //atrCalculation.Append(",");
            //atrCalculation.Append(txtGTotal.ClientID + ");");
            //txtRecQty.Attributes.Add("onChange", atrCalculation.ToString());
            //txtRate.Attributes.Add("onChange", atrCalculation.ToString());
            //txtDiscount.Attributes.Add("onChange", atrCalculation.ToString());
            //txtGSTRate.Attributes.Add("onChange", atrCalculation.ToString());
        }
    }


}