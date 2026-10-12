using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_sm_frmDomicile : Page
{
   
    private int _rowId = Constants.IntNullValue;

    readonly Student_Controller _mSCtrl = new Student_Controller();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDomicile();
        }
    }
  

    private void LoadDomicile()//For grid
    {
        var dt = _mSCtrl.UspSelectDomicile(Constants.IntNullValue, null);
        if (dt.Rows.Count <= 0) return;
        gvDomicile.DataSource = dt;
        gvDomicile.DataBind();
    }

    protected void gvDomicile_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


    }
    protected void gvDomicile_RowEditing(object sender, GridViewEditEventArgs e)
    {
        _rowId =Convert.ToInt32(gvDomicile.Rows[e.NewEditIndex].Cells[0].Text);
        txtCityName.Text = gvDomicile.Rows[e.NewEditIndex].Cells[1].Text;

        btnSaveDomicile.Text = "Update";
    }

    protected void btnSaveDomicile_Click(object sender, EventArgs e)
    {
      
        if (_mSCtrl.InsertDomicile(_rowId, txtCityName.Text, true))
        {
           Clear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "script",
                "javascript:alert('Some erroe occurred');", true);
        }
    }

    private void Clear()
    {
        txtCityName.Text = "";
        LoadDomicile();
        btnSaveDomicile.Text = "Save";
    }

    
}