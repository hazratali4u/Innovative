using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;

public partial class Forms_frmChequeBook : System.Web.UI.Page
{
    #region Variables

    ChequeEntryController objCheque = new ChequeEntryController();
    
    #endregion

    /// <summary>
    /// Page_Load Function Populates All Combos, Grids And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadAccountHead();
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Loads Account Heads To Bank Combo
    /// </summary>
    private void LoadAccountHead()
    {
        SAMSCommon.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
        clsWebFormUtil.FillDropDownList(ddlBank, dt, 0, 4);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            if (objCheque.InsertChequeBook(Convert.ToInt32(ddlBank.SelectedValue), txtAccNo.Text, txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["UserID"])))
            {
                txtAccNo.Text = "";
                txtEnd.Text = "";
                txtStart.Text = "";
                this.LoadGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Cheque Book added successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.')", true);
            }
        }
        else
        {
            if (objCheque.UpdateChequeBook(Convert.ToInt32(ddlBank.SelectedValue), txtAccNo.Text, txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(hfChequeBookID.Value)))
            {
                txtAccNo.Text = "";
                txtEnd.Text = "";
                txtStart.Text = "";
                btnSave.Text = "Save";
                this.LoadGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Cheque Book updated successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.')", true);
            }
        }
    }

    private void LoadGrid()
    {
        DataTable dtBook = objCheque.GetChequeBook(Convert.ToInt32(ddlBank.SelectedValue), Constants.IntNullValue);
        gvBook.DataSource = dtBook;
        gvBook.DataBind();
    }

    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
    }

    protected void gvBook_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "edits")
        {            
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            hfChequeBookID.Value = row.Cells[0].Text;
            ddlBank.SelectedValue = row.Cells[1].Text;
            txtAccNo.Text = row.Cells[2].Text;
            txtStart.Text = row.Cells[3].Text;
            txtEnd.Text = row.Cells[4].Text;
            btnSave.Text = "Update";
        }
        else if (e.CommandName.ToLower() == "view")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            DataTable dtDetail = objCheque.GetChequeBookDetail(Convert.ToInt32(row.Cells[0].Text));
            hfChequeBookID.Value = row.Cells[0].Text;
            gvBookDetail.DataSource = dtDetail;
            gvBookDetail.DataBind();

            ModalPopupExtender.Show();
        }
    }

    protected void gvBookDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "cancels")
        {
            ModalPopupExtender.Show();
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);    
            if(objCheque.UpdateChequeBookDetail(Convert.ToInt64(row.Cells[0].Text),2))
            {
                DataTable dtDetail = objCheque.GetChequeBookDetail(Convert.ToInt32(hfChequeBookID.Value));                
                gvBookDetail.DataSource = dtDetail;
                gvBookDetail.DataBind();
            }
        }        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hfChequeBookID.Value = "";
        txtAccNo.Text = "";
        txtStart.Text = "";
        txtEnd.Text = "";
        btnSave.Text = "Save";
    }
}