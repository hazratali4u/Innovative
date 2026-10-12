using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using System.Collections;

/// <summary>
/// Form To Edit Vouchers
/// </summary>
public partial class Forms_frmVoucherVerify : System.Web.UI.Page
{
    LedgerController mController = new LedgerController();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetVouchers();
        }
    }
       
    /// <summary>
    /// Loads UnPosted Vouchers To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    /// 

    private void GetVouchers()
    {
        string StatusIDs = Constants.VoucherCheckApprove.ToString() + "," + Constants.VoucherDisapprove.ToString();
        DataTable dt = mController.SelectUnPostVoucherNo2(null, Constants.IntNullValue, Constants.IntNullValue, StatusIDs, Constants.DateNullValue, Constants.DateNullValue, 20,1);
        rVoucher.DataSource = dt;
        rVoucher.DataBind();
    }

    protected void rVoucher_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            HiddenField hfDistributorID = (HiddenField) e.Item.FindControl("hfDistributorID");
            HiddenField hfVoucherTypeId = (HiddenField)e.Item.FindControl("hfVoucherTypeId");
            HiddenField hfIS_POSTED = (HiddenField)e.Item.FindControl("hfIS_POSTED");
            this.Session.Add("VoucherNo", e.CommandArgument.ToString());
            this.Session.Add("DistributorId", hfDistributorID.Value);
            this.Session.Add("VoucherTypeId", hfVoucherTypeId.Value);
            this.Session.Add("IS_POSTED", hfIS_POSTED.Value);

            string url = "'frmVoucherEditing.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=900,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);

        }        
    }

    protected void rVoucher_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnView = e.Item.FindControl("btnView") as LinkButton;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnView);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rVoucher.Items)
        {
            RadioButtonList rblAction = (RadioButtonList)ri.FindControl("rblAction");
            if (rblAction.SelectedValue != "-1")
            {
                HiddenField hfDistributorID = (HiddenField)ri.FindControl("hfDistributorID");
                HiddenField hfVoucherTypeId = (HiddenField)ri.FindControl("hfVoucherTypeId");
                Label lblVoucher = (Label) ri.FindControl("lblVoucher");
                Label lblDate = (Label)ri.FindControl("lblDate");
                mController.PostSelectVoucher2(Convert.ToInt32(hfDistributorID.Value), lblVoucher.Text, Convert.ToInt32(hfVoucherTypeId.Value), 0, Convert.ToDateTime(lblDate.Text), Convert.ToInt32(rblAction.SelectedValue), Convert.ToInt32(Session["UserID"]));
            }
        }

        GetVouchers();
    }
}
