using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU Hierarchy
/// </summary>
public partial class frmSKUHierarchyData : System.Web.UI.Page
{

    DataTable dtEmailAtt = new DataTable();
    SkuController mSKUController = new SkuController();
    SkuHierarchyController mController = new SkuHierarchyController();
    static int PrincipalId;

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager mgr = ScriptManager.GetCurrent(this.Page);
        mgr.RegisterPostBackControl(btnUpload);
    }
    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadPrincipal();
            txtDateofExpiry.Attributes.Add("readonly", "readonly");
            CreateEmailAttatchment();
            GvEmail.DataSource = dtEmailAtt;
            GvEmail.DataBind();
            fillddlSupplier();
            fillddlProduct();
            LoadlstUNAssignProducts();
            LoadlstAssignProducts();
        }
    }
    private void CreateEmailAttatchment()
    {
        dtEmailAtt = new DataTable();
        dtEmailAtt.Columns.Add("FILENAME", typeof(string));
        dtEmailAtt.Columns.Add("PATH", typeof(string));
        Session.Add("dtEmailAtt", dtEmailAtt);
    }
    /// <summary>
    /// Loads Principals To Principal Grid On Principal Tab And To All Principal Combos On Form
    /// </summary>
    private void LoadPrincipal()
    {
        DataTable dt = mController.SelectPrincipal(Constants.SKUPrincipal, int.Parse(this.Session["CompanyId"].ToString()),true);
        GrdPrincipal.DataSource = dt;
        GrdPrincipal.DataBind();
    }
    /// <summary>
    /// Sets Principal Data For Edit. This Function Runs When An Existing Principal Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PrincipalId = int.Parse(GrdPrincipal.Rows[e.NewEditIndex].Cells[0].Text);
        txtPrincipalCode.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[1].Text.Replace("&nbsp;", ""); 
        txtPrincipalName.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[2].Text.Replace("&nbsp;", ""); 
        txtContact.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", "");
        txtAddress.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
        txtNTN.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[5].Text.Replace("&nbsp;", ""); 
        txtSTRN.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[6].Text.Replace("&nbsp;", ""); 
        txtPhone.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;", ""); 
        txtFax.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", ""); 
        txtMobile.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[9].Text.Replace("&nbsp;", "");
        txtCertificateName.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
        string date=GrdPrincipal.Rows[e.NewEditIndex].Cells[11].Text.Replace("&nbsp;", "");
        if (date != "" && date != null)
        {
            txtDateofExpiry.Text = Convert.ToDateTime(GrdPrincipal.Rows[e.NewEditIndex].Cells[11].Text.Replace("&nbsp;", "")).ToString("dd-MMM-yyyy");
        }
        txtEmailID.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[12].Text.Replace("&nbsp;", "");

        string ISrEGISTERED = GrdPrincipal.Rows[e.NewEditIndex].Cells[13].Text.Replace("&nbsp;", "");
        if (ISrEGISTERED != "" && ISrEGISTERED != null)
        {
            chkIsRegistered.Checked = Convert.ToBoolean(GrdPrincipal.Rows[e.NewEditIndex].Cells[13].Text.Replace("&nbsp;", ""));
        }
        else
        {
            chkIsRegistered.Checked = false;
        }
       DataTable dt= mController.selectPrincipal_Images(PrincipalId);
       dtEmailAtt = (DataTable)Session["dtEmailAtt"];
       dtEmailAtt.Rows.Clear();
       foreach (DataRow dr in dt.Rows)
       {
           DataRow dtRow = dtEmailAtt.NewRow();
           dtRow["FILENAME"] = dr["IMAGENAME"];
           dtRow["PATH"] = dr["PATH"];
           dtEmailAtt.Rows.Add(dtRow);
       }
       GvEmail.DataSource = dtEmailAtt;
       GvEmail.DataBind();
       Session.Add("dtEmailAtt", dtEmailAtt);
        txtPrincipalName.Enabled = true;
        txtAddress.Enabled = true;
        txtNTN.Enabled = true;
        txtSTRN.Enabled = true;
        txtContact.Enabled = true;
        txtFax.Enabled = true;
        txtPhone.Enabled = true;
        txtMobile.Enabled = true;
        txtCertificateName.Enabled = true;
        txtEmailID.Enabled = true;
        chkIsRegistered.Enabled = true;
        fuImage.Enabled = true;
        txtDateofExpiry.Enabled = true;
        btnSavePrincipal.Text = "Update";
    }
    /// <summary>
    /// Deletes A Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PrincipalId = int.Parse(GrdPrincipal.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUDivision, PrincipalId);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsg.Text = "Wrong Command: first delete associated division";
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUPrincipal, PrincipalId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadPrincipal();
            lblErrorMsg.Text = "";
        }
    }
    /// <summary>
    /// Sets PageIndex Of Principal Grid On Principal Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdPrincipal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdPrincipal.PageIndex = e.NewPageIndex;
        this.LoadPrincipal();

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        if (fuImage.HasFile)
        {
            dtEmailAtt = (DataTable)Session["dtEmailAtt"];
            HttpFileCollection uploadedFiles = Request.Files;
            for (int i = 0; i < uploadedFiles.Count; i++)
            {
                DataRow DR = dtEmailAtt.NewRow();
                HttpPostedFile userPostedFile = uploadedFiles[i];
                try
                {
                    if (userPostedFile.ContentLength > 0)
                    {

                      
                        string fileName = Path.GetFileName(userPostedFile.FileName);

                        if (!Directory.Exists((Server.MapPath("~/Docs/"))))
                        {
                            Directory.CreateDirectory((Server.MapPath("~/Docs/")));
                        }
                        fuImage.SaveAs(Server.MapPath("~/Docs/" + fileName));
                        DR["FILENAME"] = fileName;
                        DR["PATH"] = Server.MapPath("~/Docs/" + fileName);
                        dtEmailAtt.Rows.Add(DR);
                    }
                }
                catch
                {

                }
            }
            GvEmail.DataSource = dtEmailAtt;
            GvEmail.DataBind();
            Session.Add("dtEmailAtt", dtEmailAtt);
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        dtEmailAtt = (DataTable)Session["dtEmailAtt"];

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int r = gvr.RowIndex;
        //Get rowindex

        //Delete row
        dtEmailAtt.Rows[r].Delete();


        GvEmail.DataSource = dtEmailAtt;
        GvEmail.DataBind();

        dtEmailAtt.AcceptChanges();
        Session.Add("dtEmailAtt", dtEmailAtt);
    }
    /// <summary>
    /// Save Or Updates a Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSavePrincipal_Click(object sender, EventArgs e)
    {

        dtEmailAtt = (DataTable)Session["dtEmailAtt"];
        DateTime DateofExpiry = Convert.ToDateTime(txtDateofExpiry.Text != "" ? txtDateofExpiry.Text : Constants.DateNullValue.ToString());
        lblErrorMsg.Text = "";
        if (btnSavePrincipal.Text == "New")
        {
            txtPrincipalCode.Text = this.GetAutoCode("SC", 0);
            txtPrincipalName.Enabled = true;
            txtAddress.Enabled = true;
            txtNTN.Enabled = true;
            txtSTRN.Enabled = true;
            txtContact.Enabled = true;
            txtFax.Enabled = true;
            txtPhone.Enabled = true;
            txtMobile.Enabled = true;
            txtCertificateName.Enabled = true;
            txtEmailID.Enabled = true;
            chkIsRegistered.Enabled = true;
            txtDateofExpiry.Enabled = true;
            fuImage.Enabled = true;
            txtPrincipalName.Focus();
            btnSavePrincipal.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(txtPrincipalName);
        }
        else if (btnSavePrincipal.Text == "Save")
        {
            if (txtPrincipalName.Text.Length == 0)
            {
                lblErrorMsg.Text = "Must Enter Supplier Name";
                return;
            }
            string fileName = null;

            mController.InsertPrincipal(Constants.SKUPrincipal, Constants.IntNullValue, txtPrincipalCode.Text, txtPrincipalName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()), true, txtAddress.Text, txtNTN.Text, txtSTRN.Text, txtContact.Text, txtPhone.Text, txtFax.Text, txtMobile.Text, txtCertificateName.Text, fileName, txtEmailID.Text, DateofExpiry, chkIsRegistered.Checked, dtEmailAtt);
            //this.SetAutoCode("SC", long.Parse(txtPrincipalCode.Text.Substring(2)));
            btnSavePrincipal.Text = "New";
            txtPrincipalCode.Text = "";
            txtPrincipalName.Text = "";
            txtAddress.Text = "";
            txtNTN.Text = "";
            txtSTRN.Text = "";
            txtContact.Text = "";
            txtFax.Text = "";
            txtPhone.Text = "";
            txtMobile.Text = "";
            txtCertificateName.Text = String.Empty;
            txtEmailID.Text = String.Empty;
            txtDateofExpiry.Text = String.Empty;

            txtPrincipalName.Enabled = false;
            txtAddress.Enabled = false;
            txtNTN.Enabled = false;
            txtSTRN.Enabled = false;
            txtContact.Enabled = false;
            txtFax.Enabled = false;
            txtPhone.Enabled = false;
            txtMobile.Enabled = false;
            txtCertificateName.Enabled = false;
            txtEmailID.Enabled = false;
            chkIsRegistered.Enabled =false;
            fuImage.Enabled =  false;
            txtDateofExpiry.Enabled = false;
            this.LoadPrincipal();
            dtEmailAtt = (DataTable)Session["dtEmailAtt"];
            dtEmailAtt.Rows.Clear();
            GvEmail.DataSource = dtEmailAtt;
            GvEmail.DataBind();
            dtEmailAtt.AcceptChanges();
            Session.Add("dtEmailAtt", dtEmailAtt);
        }
        else if (btnSavePrincipal.Text == "Update")
        {
            if (txtPrincipalName.Text.Length == 0)
            {
                lblErrorMsg.Text = "Must Enter Supplier Name";
                return;
            }
            string fileName = null;
            if (fuImage.HasFile)
            {
                string path = Server.MapPath("~/CompanyLogos");
                string fExtension = "";
                FileInfo oFileInfo = new FileInfo(fuImage.PostedFile.FileName);
                fExtension = Path.GetExtension(fuImage.FileName);
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fuImage.FileName;
                string fullFileName = path + "\\" + fileName;
                string imagePath = "CompanyLogos/" + fileName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fuImage.PostedFile.SaveAs(fullFileName);
            }
            mController.UpdatePrincipal(Constants.SKUPrincipal, PrincipalId, Constants.IntNullValue, txtPrincipalCode.Text, txtPrincipalName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()), true, txtAddress.Text, txtNTN.Text, txtSTRN.Text, txtContact.Text, txtPhone.Text, txtFax.Text, txtMobile.Text, txtCertificateName.Text, fileName, txtEmailID.Text,DateofExpiry, chkIsRegistered.Checked,dtEmailAtt);
            btnSavePrincipal.Text = "New";
            txtPrincipalCode.Text = "";
            txtPrincipalName.Text = "";
            txtAddress.Text = "";
            txtNTN.Text = "";
            txtSTRN.Text = "";
            txtContact.Text = "";
            txtFax.Text = "";
            txtPhone.Text = "";
            txtMobile.Text = "";
            txtCertificateName.Text = String.Empty;
            txtEmailID.Text = String.Empty;
            txtDateofExpiry.Text = String.Empty;
            this.LoadPrincipal();
            txtPrincipalName.Enabled = false;
            txtAddress.Enabled = false;
            txtNTN.Enabled = false;
            txtSTRN.Enabled = false;
            txtContact.Enabled = false;
            txtFax.Enabled = false;
            txtPhone.Enabled = false;
            txtMobile.Enabled = false;
            txtCertificateName.Enabled = false;
            txtEmailID.Enabled = false;
            chkIsRegistered.Enabled = false;
            fuImage.Enabled = false;
            txtDateofExpiry.Enabled = false;
            this.LoadPrincipal();
            dtEmailAtt = (DataTable)Session["dtEmailAtt"];
            dtEmailAtt.Rows.Clear();
            GvEmail.DataSource = dtEmailAtt;
            GvEmail.DataBind();
            dtEmailAtt.AcceptChanges();
            Session.Add("dtEmailAtt", dtEmailAtt);
        }
    }
    /// <summary>
    /// Gets Code For New Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CodeType">Type</param>
    /// <returns>Code As String</returns>
    private string GetAutoCode(string PreeFix,int CodeType)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(PreeFix, CodeType, Constants.LongNullValue);
    }
    /// <summary>
    /// Sets Code For Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CValue">Value</param>
    private void SetAutoCode(string PreeFix, long CValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        string result = AutoCode.GetAutoCode(PreeFix, 1, CValue);
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (lstUnAssignProduct.Items.Count > 0 && lstUnAssignProduct.SelectedValue != null && lstUnAssignProduct.SelectedValue != "" && ddlSupplier.SelectedValue != null && ddlSupplier.SelectedValue != "")
        {
            mController.InsertSupplier_Products(Convert.ToInt32(ddlSupplier.SelectedValue),Convert.ToInt32(lstUnAssignProduct.SelectedValue));
            LoadlstUNAssignProducts();
            LoadlstAssignProducts();
        }
    }
    protected void btnUnAssign_Click(object sender, EventArgs e)
    {
        if (this.lstAssignProduct.Items.Count > 0 && lstAssignProduct.SelectedValue != null && lstAssignProduct.SelectedValue != "" && ddlSupplier.SelectedValue != null && ddlSupplier.SelectedValue != "")
        {
            mController.DeleteSupplier_Products(Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(lstAssignProduct.SelectedValue));
            LoadlstUNAssignProducts();
            LoadlstAssignProducts();
        }
    }
    private void fillddlSupplier()  
    {
        DataTable dt = mController.SelectPrincipal(Constants.SKUPrincipal, int.Parse(this.Session["CompanyId"].ToString()), true);
        clsWebFormUtil.FillDropDownList(ddlSupplier, dt, "SKU_HIE_ID", "SKU_HIE_NAME", true);
      
    }
    private void fillddlProduct()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlProductCategory, dt, "SKU_HIE_ID", "SKU_HIE_NAME", true);
    }
    private void LoadlstUNAssignProducts()
    {
        if (ddlProductCategory.SelectedValue != null && ddlProductCategory.SelectedValue != "" && ddlSupplier.SelectedValue != null && ddlSupplier.SelectedValue != "")
        {
            DataTable m_SKUDt = mSKUController.SelectSku_AssignUnAssign(Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlProductCategory.SelectedValue), int.Parse(this.Session["CompanyId"].ToString()),0);
            clsWebFormUtil.FillListBox(lstUnAssignProduct, m_SKUDt, "SKU_ID", "SKU_NAME", true);
        }
    }
    private void LoadlstAssignProducts()
    {
        if (ddlProductCategory.SelectedValue != null && ddlProductCategory.SelectedValue != "" && ddlSupplier.SelectedValue != null && ddlSupplier.SelectedValue != "")
        {
            DataTable m_SKUDt = mSKUController.SelectSku_AssignUnAssign(Convert.ToInt32(ddlSupplier.SelectedValue), Convert.ToInt32(ddlProductCategory.SelectedValue), int.Parse(this.Session["CompanyId"].ToString()), 1);
            clsWebFormUtil.FillListBox(lstAssignProduct, m_SKUDt, "SKU_ID", "SKU_NAME", true);
        }
    }
    protected void ddlProductCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadlstUNAssignProducts();
        LoadlstAssignProducts();
    }
    protected void tbUserAssignment_ActiveTabChanged(object sender, EventArgs e)
    {
         fillddlSupplier();
    }
}
