using System;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmQCAdd : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable QCSKU;
    DataTable QCParamter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateTable();
            this.GetGRN();
            this.LoadGRNProduct();
            if (Session["QCID"] != null)
            {
                LoadQCDetail();
            }
            else
            {
                this.LoadProductDetail();
            }
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreateTable()
    {
        QCSKU = new DataTable();
        QCSKU.Columns.Add("SKU_ID", typeof(int));
        QCSKU.Columns.Add("STATUS_ID", typeof(int));
        QCSKU.Columns.Add("Status", typeof(string));
        QCSKU.Columns.Add("SKU_Name", typeof(string));
        QCSKU.Columns.Add("Quantity", typeof(decimal));        
        this.Session.Add("QCSKU", QCSKU);

        QCParamter = new DataTable();
        QCParamter.Columns.Add("SKU_ID", typeof(int));
        QCParamter.Columns.Add("SKU_QUALITY_ID", typeof(int));
        QCParamter.Columns.Add("QUALITY_DESCRIPTION", typeof(string));
        QCParamter.Columns.Add("QUALITY_VALUE", typeof(string));
        QCParamter.Columns.Add("Value", typeof(decimal));
        QCParamter.Columns.Add("Remarks", typeof(string));
        this.Session.Add("QCParamter", QCParamter);

    }

    /// <summary>
    /// Checkes Duplicate SKUS in Document Detail Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
        QCSKU = (DataTable)this.Session["QCSKU"];
        DataRow[] foundRows = QCSKU.Select("SKU_ID = '" + ddlProduct.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGrid()
    {
        DataTable dtParamter = (DataTable)this.Session["dtParamter"];
        gvParamter.DataSource = dtParamter;
        gvParamter.DataBind();        
    }
        
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvParamter.Rows.Count > 0)
        {
            QCSKU = (DataTable)Session["QCSKU"];
            QCParamter = (DataTable)Session["QCParamter"];
            PurchaseController PurCtl = new PurchaseController();
            if (QCSKU.Rows.Count > 0)
            {
                if (btnSave.Text == "Save")
                {
                    if (PurCtl.InsertQC(Convert.ToInt64(ddlGRN.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), QCSKU, QCParamter))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "QC saved successfully.');window.location.href = 'frmQC.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured');", true);
                    }
                }
                else
                {
                    if (PurCtl.UpdateQC(Convert.ToInt64(Session["QCID"]),true, Convert.ToInt32(Session["UserID"]), QCSKU, QCParamter))
                    {
                        Session.Remove("QCID");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "QC updated successfully.');window.location.href = 'frmQC.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
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
    }

    private void LoadQCDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataSet ds = PurCtl.GetQCDataSet(Convert.ToInt64(Session["QCID"]), Constants.LongNullValue, Constants.IntNullValue, 1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGRN.Enabled = false;            
            btnSave.Text = "Update";
        }
        QCSKU = ds.Tables[1];        
        this.Session.Add("QCSKU", QCSKU);
        LoadGrid2();

        QCParamter = ds.Tables[2];
        this.Session.Add("QCParamter", QCParamter);       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("QCID");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "window.location.href = 'frmQC.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
    }

    private void GetGRN()
    {
        PurchaseController PurCtl = new PurchaseController();
        DataTable dtGRN = PurCtl.GetGRN(2,0,Constants.IntNullValue,Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlGRN, dtGRN, "GRN_MASTER_ID", "GRN_MASTER_ID", true);        
    }

    private void LoadProductDetail()
    {
        PurchaseController PurCtl = new PurchaseController();
        if (ddlGRN.Items.Count > 0)
        {
            DataSet ds = PurCtl.GetGRN(4, Convert.ToInt64(ddlGRN.SelectedValue));

            DataTable dtParamter = ds.Tables[0].Clone();

            DataRow[] foundSubRows = ds.Tables[0].Select("SKU_ID  = " + ddlProduct.SelectedValue.ToString());
            foreach (DataRow dr2 in foundSubRows)
            {
                DataRow dr = dtParamter.NewRow();
                dr["SKU_QUALITY_ID"] = dr2["SKU_QUALITY_ID"];
                dr["QUALITY_DESCRIPTION"] = dr2["QUALITY_DESCRIPTION"];
                dr["QUALITY_VALUE"] = dr2["QUALITY_VALUE"];
                dr["Value"] = dr2["Value"];
                dr["Remarks"] = dr2["Remarks"];
                dtParamter.Rows.Add(dr);
            }
            this.Session.Add("dtParamter", dtParamter);
            LoadGrid();
        }
    }

    protected void ddlGRN_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGRNProduct();
        LoadProductDetail();
    }

    private void LoadGRNProduct()
    {
        if (ddlGRN.Items.Count > 0)
        {
            PurchaseController PurCtl = new PurchaseController();
            DataTable m_dt = PurCtl.GetGRN(3, Convert.ToInt64(ddlGRN.SelectedValue)).Tables[0];
            clsWebFormUtil.FillDropDownList(this.ddlProduct, m_dt, 0, 1, true);
        }
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductDetail();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        QCSKU = (DataTable)this.Session["QCSKU"];
        QCParamter = (DataTable)this.Session["QCParamter"];

        if (btnAdd.Text == "Add")
        {
            if (CheckDublicateSKU())
            {
                foreach (GridViewRow gvr in gvParamter.Rows)
                {
                    TextBox txtValue = (TextBox)gvr.FindControl("txtValue");
                    TextBox txtRemarks = (TextBox)gvr.FindControl("txtRemarks");

                    DataRow drParam = QCParamter.NewRow();
                    drParam["SKU_ID"] = ddlProduct.SelectedValue;
                    drParam["SKU_QUALITY_ID"] = gvr.Cells[1].Text;
                    drParam["QUALITY_DESCRIPTION"] = gvr.Cells[2].Text;
                    drParam["QUALITY_VALUE"] = gvr.Cells[3].Text;
                    drParam["Value"] = Convert.ToDecimal(dc.chkNull_0(txtValue.Text));
                    drParam["Remarks"] = txtRemarks.Text;
                    QCParamter.Rows.Add(drParam);
                }

                DataRow dr = QCSKU.NewRow();
                dr["SKU_ID"] = ddlProduct.SelectedValue;
                dr["STATUS_ID"] = ddlStatus.SelectedValue;
                dr["SKU_Name"] = ddlProduct.SelectedItem.Text;
                dr["Quantity"] = Convert.ToDecimal(dc.chkNull_0(txtQuantity.Text));
                dr["Status"] = ddlStatus.SelectedItem.Text;
                QCSKU.Rows.Add(dr);
                ddlGRN.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('  " + ddlProduct.SelectedItem.Text + " Already Exists ');", true);
                return;
            }
        }
        else if (btnAdd.Text == "Update")
        {
            for (int i = 0; i < QCParamter.Rows.Count; i++)
            {
                if (QCParamter.Rows[i]["SKU_ID"].ToString() == QCSKU.Rows[RowNo]["SKU_ID"].ToString())
                {
                    QCParamter.Rows.Remove(QCParamter.Rows[i]);
                    i--;
                }
            }

            foreach (GridViewRow gvr in gvParamter.Rows)
            {
                TextBox txtValue = (TextBox)gvr.FindControl("txtValue");
                TextBox txtRemarks = (TextBox)gvr.FindControl("txtRemarks");

                DataRow drParam = QCParamter.NewRow();
                drParam["SKU_ID"] = ddlProduct.SelectedValue;
                drParam["SKU_QUALITY_ID"] = gvr.Cells[1].Text;
                drParam["QUALITY_DESCRIPTION"] = gvr.Cells[2].Text;
                drParam["QUALITY_VALUE"] = gvr.Cells[3].Text;
                drParam["Value"] = Convert.ToDecimal(dc.chkNull_0(txtValue.Text));
                drParam["Remarks"] = txtRemarks.Text;
                QCParamter.Rows.Add(drParam);
            }

            DataRow dr = QCSKU.Rows[RowNo];
            dr["SKU_ID"] = ddlProduct.SelectedValue;
            dr["STATUS_ID"] = ddlStatus.SelectedValue;
            dr["SKU_Name"] = ddlProduct.SelectedItem.Text;
            dr["Quantity"] = Convert.ToDecimal(dc.chkNull_0(txtQuantity.Text));
            dr["Status"] = ddlStatus.SelectedItem.Text;
        }

        this.Session.Add("QCSKU", QCSKU);
        this.LoadGrid2();
        txtQuantity.Text = string.Empty;
        btnAdd.Text = "Add";
        ScriptManager.GetCurrent(Page).SetFocus(ddlProduct);

    }

    private void LoadGrid2()
    {
        QCSKU = (DataTable)this.Session["QCSKU"];
        gvProduct.DataSource = QCSKU;
        gvProduct.DataBind();
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        ddlProduct.SelectedValue = gvProduct.Rows[e.NewEditIndex].Cells[0].Text;
        ddlStatus.SelectedValue = gvProduct.Rows[e.NewEditIndex].Cells[1].Text;
        txtQuantity.Text = gvProduct.Rows[e.NewEditIndex].Cells[4].Text;

        DataTable QCParamter = (DataTable)Session["QCParamter"];
        DataTable dtParamter = QCParamter.Clone();

        DataRow[] foundSubRows = QCParamter.Select("SKU_ID  = " + ddlProduct.SelectedValue.ToString());
        foreach (DataRow dr2 in foundSubRows)
        {
            DataRow dr = dtParamter.NewRow();
            dr["SKU_QUALITY_ID"] = dr2["SKU_QUALITY_ID"];
            dr["QUALITY_DESCRIPTION"] = dr2["QUALITY_DESCRIPTION"];
            dr["QUALITY_VALUE"] = dr2["QUALITY_VALUE"];
            dr["Value"] = dr2["Value"];
            dr["Remarks"] = dr2["Remarks"];
            dtParamter.Rows.Add(dr);
        }
        this.Session.Add("dtParamter", dtParamter);
        LoadGrid();
        
        txtQuantity.Focus();
        btnAdd.Text = "Update";
    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        QCSKU = (DataTable)this.Session["QCSKU"];
        QCParamter = (DataTable)this.Session["QCParamter"];
        if (QCSKU.Rows.Count > 0)
        {
            for (int i = 0; i < QCParamter.Rows.Count; i++)
            {
                if (QCParamter.Rows[i]["SKU_ID"].ToString() == QCSKU.Rows[e.RowIndex]["SKU_ID"].ToString())
                {
                    QCParamter.Rows.Remove(QCParamter.Rows[i]);
                    i--;
                }
            }

            QCSKU.Rows.RemoveAt(e.RowIndex);

            this.Session.Add("QCSKU", QCSKU);
            this.LoadGrid2();
        }
    }
}