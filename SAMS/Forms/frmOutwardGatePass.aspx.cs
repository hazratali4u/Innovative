using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommon.Classes;


public partial class Forms_frmOutwardGatePass : System.Web.UI.Page
{

    TGPController mS_Ctrl = new TGPController();

    SLASHCodesController CodesCtl = new SLASHCodesController();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           // ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmTemporaryGatePass.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add New Temporary Gate Pass</a>";
        }
        if (Session["RoleID"].ToString() == "1")
        {
            this.GetGATEPASS(Constants.IntNullValue, Constants.IntNullValue, 1);
        }
        else
        {
            this.GetGATEPASS(Convert.ToInt32(Session["UserID"]), Constants.IntNullValue, 1);
        }
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

        DataTable dt = new DataTable();
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
        if (Session["SearchType"].ToString() == "All")
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
        else if (Session["SearchType"].ToString() == "Customer")
        {
            SearchText();
        }
        else if (Session["SearchType"].ToString() == "Status")
        {
            SearchStatus();
        }
        else if (Session["SearchType"].ToString() == "Date")
        {
            SearchDate();
        }
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        if (Session["SearchType"].ToString() == "All")
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
        else if (Session["SearchType"].ToString() == "Customer")
        {
            SearchText();
        }
        else if (Session["SearchType"].ToString() == "Status")
        {
            SearchStatus();
        }
        else if (Session["SearchType"].ToString() == "Date")
        {
            SearchDate();
        }
    }

    public DataTable GetGATEPASS(int p_USER_ID, int p_TGP_ID, int p_TYPE_ID)
    {

        Session.Remove("dtGatePass");
        DataTable dtGatePass = mS_Ctrl.UspSelectGATEPASS(Constants.IntNullValue, Constants.IntNullValue, 1, CurrentPage + 1, 20);
        this.Session.Add("dtGatePass", dtGatePass);

        return dtGatePass;
    }

    protected void rGatePass_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "view")
        {
            this.Session.Add("TGP_ID", Convert.ToInt64(e.CommandArgument));

            Response.Redirect("frmTemporaryGatePass.aspx?LevelType=3&LevelID=96");
        }
        else if (e.CommandName == "del")
        {

            TGPController TGPCtl = new TGPController();

            DataSet ds = null;

            int TGP_ID = Convert.ToInt32(e.CommandArgument);

            ds = TGPCtl.UspSelectGATEPASSREPORT(Constants.IntNullValue, Convert.ToInt32(TGP_ID), 5);


            SAMSBusinessLayer.Reports.CrpOutwardGatePass CrpReport = new SAMSBusinessLayer.Reports.CrpOutwardGatePass();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);


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

        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    if (!CanEdit(Request.QueryString["LevelID"].ToString()))
        //    {
        //        LinkButton btnView = (LinkButton)e.Item.FindControl("btnView");
        //        LinkButton btnCustomer = (LinkButton)e.Item.FindControl("btnCustomer");

        //        btnView.Visible = false;
        //        btnCustomer.Enabled = false;
        //    }
        //    else
        //    {
        //        LinkButton lbApprive = (LinkButton)e.Item.FindControl("lbApprive");
        //        if (DataBinder.Eval(e.Item.DataItem, "Status").ToString() == "Quotation Sent" || DataBinder.Eval(e.Item.DataItem, "Status").ToString() == "Quotation Export")
        //        {
        //            lbApprive.Visible = true;
        //        }
        //    }

        //    if (!CanDelete(Request.QueryString["LevelID"].ToString()))
        //    {
        //        LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
        //        btnDelete.Visible = false;
        //    }
        //}
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTemporaryGatePassView.aspx?LevelType=3&LevelID=151");
    }

    protected void btnCustomerSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtGatePass"];
        DataView dv = new DataView(dt);

        if (Session["Customer"] != null)
        {
            if (Session["Customer"].ToString() == "DESC")
            {
                dv.Sort = "CUSTOMER DESC";
                Session.Add("Customer", "ASC");
            }
            else
            {
                dv.Sort = "CUSTOMER ASC";
                Session.Add("Customer", "DESC");
            }
        }
        else
        {
            dv.Sort = "CUSTOMER ASC";
            Session.Add("Customer", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtGatePass", dt);
        FillRepeater();

    }

    protected void btnDateSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtGatePass"];
        DataView dv = new DataView(dt);

        if (Session["Date"] != null)
        {
            if (Session["Date"].ToString() == "DESC")
            {
                dv.Sort = "DOCUMENT_DATE DESC";
                Session.Add("Date", "ASC");
            }
            else
            {
                dv.Sort = "DOCUMENT_DATE ASC";
                Session.Add("Date", "DESC");
            }
        }
        else
        {
            dv.Sort = "DOCUMENT_DATE ASC";
            Session.Add("Date", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtGatePass", dt);
        FillRepeater();

    }

    protected void btnStatusSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtGatePass"];
        DataView dv = new DataView(dt);

        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "DESC")
            {
                dv.Sort = "Status DESC";
                Session.Add("Status", "ASC");
            }
            else
            {
                dv.Sort = "Status ASC";
                Session.Add("Status", "DESC");
            }
        }
        else
        {
            dv.Sort = "Status ASC";
            Session.Add("Status", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtGatePass", dt);
        FillRepeater();

    }

    protected void btnLastUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtGatePass"];
        DataView dv = new DataView(dt);

        if (Session["LAST_UPDATE"] != null)
        {
            if (Session["LAST_UPDATE"].ToString() == "DESC")
            {
                dv.Sort = "LAST_UPDATE DESC";
                Session.Add("LAST_UPDATE", "ASC");
            }
            else
            {
                dv.Sort = "LAST_UPDATE ASC";
                Session.Add("LAST_UPDATE", "DESC");
            }
        }
        else
        {
            dv.Sort = "LAST_UPDATE ASC";
            Session.Add("LAST_UPDATE", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtGatePass", dt);
        FillRepeater();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        Session["SearchType"] = "Customer";
        SearchText();
    }

    protected void btnFilterDate_Click(object sender, EventArgs e)
    {
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        if (hfDateType.Value == "D")
        {
            if (txtDate.Text.Length > 0)
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
                String datetime = txtDate.Text.Trim();
                dtFrom = DateTime.Parse(datetime, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                dtFrom = dtFrom.AddDays(-1);
                dtTo = DateTime.Parse(datetime, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                dtTo = dtTo.AddDays(1);
            }
        }
        else if (hfDateType.Value == "M")
        {
            if (txtMonth.Text.Length > 0)
            {
                DateTime dtFromMonth = DateTime.Parse(txtMonth.Text);
                dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

                DateTime dtToMonth = DateTime.Parse(txtMonth.Text);
                dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
                dtTo = dtTo.AddMonths(1).AddDays(-1);
            }
        }
        else if (hfDateType.Value == "Y")
        {
            dtFrom = new DateTime(Convert.ToInt32(this.ddlYear.SelectedValue), 1, 1);
            dtTo = new DateTime(Convert.ToInt32(this.ddlYear.SelectedValue), 12, 31);
        }

        CurrentPage = 0;
        Session["SearchType"] = "Date";
        Session.Add("dtFrom", dtFrom);
        Session.Add("dtTo", dtTo);
        SearchDate();
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
            //  dt = EnqPriceCtl.SearchEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier, StatusIDs, Constants.DateNullValue, Constants.DateNullValue, null, 20, CurrentPage + 1);
        }
        else
        {
            //   dt = EnqPriceCtl.SearchEnquiry(Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), Constants.EnquirySupplier, StatusIDs, Constants.DateNullValue, Constants.DateNullValue, null, 20, CurrentPage + 1);
        }

        Session.Add("dtEnqPrice", dt);
        FillRepeater();
    }

    private void SearchDate()
    {
        DataTable dt = new DataTable();
        DataTable dtStatusRighs = (DataTable)Session["dtStatusRighs"];
        string StatusIDs = string.Empty;
        // if (dllEnquiryStatus.SelectedValue != Constants.IntNullValue.ToString())
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
            //   dt = EnqPriceCtl.SearchEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier, StatusIDs, Convert.ToDateTime(Session["dtFrom"]), Convert.ToDateTime(Session["dtTo"]), null, 20, CurrentPage + 1);
        }
        else
        {
            // dt = EnqPriceCtl.SearchEnquiry(Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), Constants.EnquirySupplier, StatusIDs, Convert.ToDateTime(Session["dtFrom"]), Convert.ToDateTime(Session["dtTo"]), null, 20, CurrentPage + 1);
        }

        Session.Add("dtEnqPrice", dt);
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