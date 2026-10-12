using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommon.Classes;


public partial class Forms_frmInwardGatePassView : System.Web.UI.Page
{
    TGPController mS_Ctrl = new TGPController();

    SLASHCodesController CodesCtl = new SLASHCodesController();
    private void FillCombo()
    {
         ddlSearch.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
         ddlSearch.Items.Add(new ListItem("Temporary GatePass", "1"));
         ddlSearch.Items.Add(new ListItem("Purchase Req's", "2"));
         ddlSearch.Items.Add(new ListItem("Purchase Contract's", "3"));

    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {

            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmInwardGatePass.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Update Inward Gate Pass</a>";
            FillCombo();
        }
            
        this.GetGATEPASS(Constants.IntNullValue, Constants.IntNullValue, 2);
        
        Session.Add("SearchType", "All");
        FillRepeater();
    }
    private void FillRepeater()
    {
        rGatePass.DataSource = null;
        rGatePass.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 20;

        DataTable dt;
        dt = (DataTable)Session["dtGatePass"];

        if (dt.Rows.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;

            rGatePass.DataSource = dt;
            rGatePass.DataBind();

            linkbtnnext.Visible = true;
            linkbtnprev.Visible = true;
            lblTotalNoOfPages.Visible = true;
            lblCurrentPageNo.Visible = true;
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblOf.Visible = true;
            lblTotalNoOfRecords.Text = dt.Rows[0]["TotalRows"].ToString();
            lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
            lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows[0]["TotalRows"]) / Convert.ToDecimal(20)));

            if (lblCurrentPageNo.Text == lblTotalNoOfPages.Text)
            {
                linkbtnnext.Enabled = false;
            }
            else
            {
                linkbtnnext.Enabled = true;
            }
            if (lblCurrentPageNo.Text == "1")
            {
                linkbtnprev.Enabled = false;
            }
            else
            {
                linkbtnprev.Enabled = true;
            }

        }
        else if (dt.Rows.Count == 0)
        {
            linkbtnnext.Visible = false;
            linkbtnprev.Visible = false;
            lblTotalNoOfPages.Visible = false;
            lblCurrentPageNo.Visible = false;
            lblTotalNoOfRecords.Visible = false;
            lblOf.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
    }

    public int CurrentPage
    {
        get
        {
            object objview = this.ViewState["_CurrentPage"];
            if (objview == null)
                return 0;
            else
                return (int)objview;
        }
        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }

    protected void linkbtnprev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;

        if (ViewState["SearchType"].ToString() == "All")
        {
            if (Session["RoleID"].ToString() == "1")
            {
                this.GetGATEPASS(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            }
            else
            {
                this.GetGATEPASS(Convert.ToInt32(Session["UserID"]), Constants.IntNullValue, Constants.IntNullValue);
            }
            FillRepeater();
        }
       
        else
        {
            SearchStatus();
        }
       
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        if (ViewState["SearchType"].ToString() == "All")
        {
            if (Session["RoleID"].ToString() == "1")
            {
                this.GetGATEPASS(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            }
            else
            {
                this.GetGATEPASS(Convert.ToInt32(Session["UserID"]), Constants.IntNullValue, Constants.IntNullValue);
            }
            FillRepeater();
        }
       
        else
        {
            SearchStatus();
        }
       
    }

    public DataTable GetGATEPASS(int p_USER_ID, int p_TGP_ID, int p_TYPE_ID)
    {

        Session.Remove("dtGatePass");
        DataTable dtGatePass = mS_Ctrl.UspSelectGATEPASS(Constants.IntNullValue, Constants.IntNullValue,2,CurrentPage + 1, 20);
        this.Session.Add("dtGatePass", dtGatePass);

        return dtGatePass;
    }

    protected void rGatePass_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int TGP_ID = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "view")
        {
            this.Session.Add("TGP_ID", TGP_ID);

            Response.Redirect("frmInwardGatePass.aspx?LevelType=3&LevelID=97");
        }
        else if (e.CommandName == "del")
        {
            
            TGPController TGPCtl = new TGPController();

            DataSet ds = null;

            

            ds = TGPCtl.UspSelectGATEPASSREPORT(Constants.IntNullValue, Convert.ToInt32(TGP_ID), 4);


            SAMSBusinessLayer.Reports.CrpInwardGatePass CrpReport = new SAMSBusinessLayer.Reports.CrpInwardGatePass();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
           
           

            //Response.Redirect("RptGatePass.aspx?LevelType=3&LevelID=100&TGP_ID=" + TGP_ID + "&TGP_TYPE=4");
        }
       
    }

    protected void rGatePass_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            //do something cool 
            ScriptManager mgr = ScriptManager.GetCurrent(this.Page);
            mgr.RegisterPostBackControl(btnDelete);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTemporaryGatePassView.aspx?LevelType=3&LevelID=151");
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ViewState.Add("SearchType", ddlSearch.SelectedItem.Text);
        CurrentPage = 0;
        SearchStatus();
    }

    
    private void SearchText()
    {
        DataTable dt = new DataTable();
        DataTable dtStatusRighs = (DataTable)Session["dtStatusRighs"];
        string StatusIDs = string.Empty;
        //if (dllEnquiryStatus.SelectedValue != Constants.IntNullValue.ToString())
        //{
        //    StatusIDs = dllEnquiryStatus.SelectedValue;
        //}
        //else
        //{
        //    foreach (DataRow dr in dtStatusRighs.Rows)
        //    {
        //        if (dr["CAN_VIEW"].ToString() == "True")
        //        {
        //            StatusIDs += dr["STATUS_ID"].ToString() + ",";
        //        }
        //    }

        //}
        if (StatusIDs.Contains(","))
        {
            StatusIDs = StatusIDs.Remove(StatusIDs.Length - 1, 1);
        }


        if (Session["RoleID"].ToString() == "1")
        {
            // dt = EnqPriceCtl.SearchEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier, StatusIDs, Constants.DateNullValue, Constants.DateNullValue, txtCustomer.Text, 20, CurrentPage + 1);
        }
        else
        {
            // dt = EnqPriceCtl.SearchEnquiry(Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), Constants.EnquirySupplier, StatusIDs, Constants.DateNullValue, Constants.DateNullValue, txtCustomer.Text, 20, CurrentPage + 1);
        }

        Session.Add("dtEnqPrice", dt);
        FillRepeater();
    }

    private void SearchStatus()
    {
        DataTable dt = new DataTable();
        dt = mS_Ctrl.SearchGatePass(Constants.IntNullValue, Constants.IntNullValue, 20, CurrentPage + 1, Convert.ToInt32(ddlSearch.SelectedValue));
        Session.Add("dtGatePass", dt);
        FillRepeater();
    }

    #region Role Rights

    private bool CanView(string mModuleID)
    {
        DataTable dtRights = (DataTable)Session["dtRights"];
        bool flag = false;
        if (dtRights != null)
        {
            foreach (DataRow dr in dtRights.Rows)
            {
                if (dr["MODULE_ID"].ToString() == mModuleID)
                {
                    if (Convert.ToBoolean(dr["CAN_VIEW"]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return flag;
    }

    private bool CanEdit(string mModuleID)
    {
        DataTable dtRights = (DataTable)Session["dtRights"];
        bool flag = false;
        if (dtRights != null)
        {
            foreach (DataRow dr in dtRights.Rows)
            {
                if (dr["MODULE_ID"].ToString() == mModuleID)
                {
                    if (Convert.ToBoolean(dr["CAN_EDIT"]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return flag;
    }

    private bool CanDelete(string mModuleID)
    {
        DataTable dtRights = (DataTable)Session["dtRights"];
        bool flag = false;
        if (dtRights != null)
        {
            foreach (DataRow dr in dtRights.Rows)
            {
                if (dr["MODULE_ID"].ToString() == mModuleID)
                {
                    if (Convert.ToBoolean(dr["CAN_DELETE"]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return flag;
    }

    #endregion
}