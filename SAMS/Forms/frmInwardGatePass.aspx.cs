using System;
using System.Data;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;


public partial class Forms_frmInwardGatePass : System.Web.UI.Page
{
    SkuHierarchyController mController = new SkuHierarchyController();
    TGPController tgp = new TGPController();
    SkuController mSKUController = new SkuController();

    
    private void LoadPrincipal()
    {
        DataTable dt = mController.SelectPrincipal(Constants.SKUPrincipal, int.Parse(this.Session["CompanyId"].ToString()), true);
        clsWebFormUtil.FillDropDownList(ddlSupplier, dt, "SKU_HIE_ID", "SKU_HIE_NAME", true);
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadPrincipal();
           
            if (Session["TGP_ID"] != null)
            {
                hf_TGPID.Value = Convert.ToString(Session["TGP_ID"]);

                DataTable dt = tgp.UspSelectGATEPASS(int.Parse(hf_TGPID.Value), 1);

                ddlSupplier.SelectedValue = dt.Rows[0]["SUPPLIER_ID"].ToString();
               

                DataTable dt2 = tgp.UspSelectGATEPASS(int.Parse(hf_TGPID.Value), 3);

                DataTable dt3 = dt2.Clone();
                DataRow[] dr = dt2.Select("QUANTITY > 0");

                if (dr.Length > 0)
                {
                    for (int I = 0; I < dr.Length; I++)
                    {
                        dt3.ImportRow(dr[I]);
                    }
                }

                GrdOpeningInventory.DataSource = dt3;
                GrdOpeningInventory.DataBind();

            }
            

                FillIGPGrid();
            
        }
        btnSaveDocument.Attributes.Add("onclick", "return ValidateForm();");
    }

    #region Load
   
    #endregion

    #region Click Opearations
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmInwardGatePassView.aspx?LevelID=97&LevelType=3&TopID=30");
    }
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {

        if ((GrdOpeningInventory.Rows.Count > 0) && (Convert.ToDecimal(txtRecQty.Text) > 0))
        {
            if (btnSaveDocument.Text == "Save")
            {
                if (hf_TGPID.Value == "")
                {
                    hf_TGPID.Value = Convert.ToString(Constants.LongNullValue);
                }

                long IGP_ID = tgp.InsertIGP(Convert.ToInt32(Session["CompanyId"]),Constants.LongNullValue, int.Parse(hf_TGPID.Value), Convert.ToInt32(ddlSupplier.SelectedValue), txtGoodsInThrough.Text, txtTransportNo.Text, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), Convert.ToInt32(Session["UserID"].ToString()), 1);

                foreach (GridViewRow dr in GrdOpeningInventory.Rows)
                {
                    TextBox txtRcdQty = (TextBox)dr.Cells[4].FindControl("txtRcdQty");
                    if (decimal.Parse(txtRcdQty.Text) > 0)
                    {
                        tgp.InsertIGP_DETAIL(IGP_ID, int.Parse(hf_TGPID.Value), decimal.Parse(dr.Cells[3].Text), decimal.Parse(txtRcdQty.Text), int.Parse(dr.Cells[0].Text), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), Convert.ToString(dr.Cells[6].Text));
                    }
                }
                ResetForm();

                FillIGPGrid();
                // Response.Redirect("frmInwardGatePassView.aspx?LevelID=97&LevelType=3&TopID=30");
            }

            else
            {
                long IGP_ID = tgp.InsertIGP(Convert.ToInt32(Session["CompanyId"]),long.Parse(hf_IGPID.Value), int.Parse(hf_TGPID.Value), Convert.ToInt32(ddlSupplier.SelectedValue), txtGoodsInThrough.Text, txtTransportNo.Text, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), Convert.ToInt32(Session["UserID"].ToString()), 1);

                foreach (GridViewRow dr in GrdOpeningInventory.Rows)
                {
                    TextBox txtRcdQty = (TextBox)dr.Cells[4].FindControl("txtRcdQty");
                    if (decimal.Parse(txtRcdQty.Text) > 0)
                    {
                        tgp.InsertIGP_DETAIL(IGP_ID, int.Parse(hf_TGPID.Value), decimal.Parse(dr.Cells[3].Text), decimal.Parse(txtRcdQty.Text), int.Parse(dr.Cells[0].Text), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), Convert.ToString(dr.Cells[7].Text));
                    }
                }
                ResetForm();
                // Response.Redirect("frmInwardGatePassView.aspx?LevelID=97&LevelType=3&TopID=30");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this,this.GetType(),"msg","alert('Plz enter Recieved Qty!!')",true);
        }
    }

    #endregion

    #region Grid OPerations

    private void FillIGPGrid()
    {
       
        DataTable dt = tgp.UspSelectINWARDGATEPASS(Constants.LongNullValue, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), 1);
        
        Grd_IGP.DataSource = dt;
        Grd_IGP.DataBind();
    }
    private void FillDetailGrid()
    {
        DataTable dt2 = tgp.UspSelectINWARDGATEPASS(long.Parse(hf_IGPID.Value), 2);

        GrdOpeningInventory.DataSource = dt2;
        GrdOpeningInventory.DataBind();
    }

    protected void Grd_IGP_RowEditing(object sender, GridViewEditEventArgs e)
    {

            hf_IGPID.Value = Convert.ToString(Grd_IGP.Rows[e.NewEditIndex].Cells[0].Text);
            hf_TGPID.Value = Convert.ToString(Grd_IGP.Rows[e.NewEditIndex].Cells[1].Text);
           ddlSupplier.SelectedValue = Convert.ToString(Grd_IGP.Rows[e.NewEditIndex].Cells[3].Text);
           txtGoodsInThrough.Text = Convert.ToString(Grd_IGP.Rows[e.NewEditIndex].Cells[4].Text);
           txtTransportNo.Text = Convert.ToString(Grd_IGP.Rows[e.NewEditIndex].Cells[5].Text);
            FillDetailGrid();
            btnSaveDocument.Text = "Update";
  
    }

    protected void Grd_IGP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hf_IGPID.Value=Convert.ToString(Grd_IGP.Rows[e.RowIndex].Cells[0].Text);
        
        tgp.UspSelectINWARDGATEPASS(long.Parse(hf_IGPID.Value), 3);

        FillIGPGrid();
        Response.Redirect("frmInwardGatePassView.aspx?LevelID=97&LevelType=3&TopID=30");

    }

    protected void GrdOpeningInventory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtRcdQty = (TextBox)e.Row.FindControl("txtRcdQty");
                       //do something cool 
            ScriptManager mgr = ScriptManager.GetCurrent(this.Page);
            mgr.RegisterPostBackControl(txtRcdQty);
           
        }
    }
    #endregion

    protected void txtRcdQty_TextChanged(object sender, EventArgs e)
    {
        decimal RecQty = 0;
        decimal BalQty = 0;
        foreach (GridViewRow dr in GrdOpeningInventory.Rows)
        {

            TextBox txtRcdQty = (TextBox)dr.Cells[4].FindControl("txtRcdQty");
            TextBox txtBalQty = (TextBox)dr.Cells[5].FindControl("txtBalQty");
            BalQty = Convert.ToDecimal(dr.Cells[3].Text);
            if (decimal.Parse(txtRcdQty.Text) <= BalQty)
            {
                txtBalQty.Text = Convert.ToString((BalQty) - decimal.Parse(txtRcdQty.Text));

                RecQty += Convert.ToDecimal(txtRcdQty.Text);
                txtRcdQty.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Recieved Qty Can't Greater than Current Qty!')", true);
                break;
            }
        }
        txtRecQty.Text = RecQty.ToString();


    }

    private void ResetForm()
    {
       
        if (ddlSupplier.Items.Count > 0)
        {
            ddlSupplier.SelectedIndex = 0;
        }
       
        
       
        GrdOpeningInventory.DataSource = null;
        GrdOpeningInventory.DataBind();
        FillIGPGrid();

        txtGoodsInThrough.Text = "";
        txtTransportNo.Text = "";
        txtRecQty.Text = "0";
        Session.Remove("TGP_ID");
        btnSaveDocument.Text = "Save";
    }

  
}