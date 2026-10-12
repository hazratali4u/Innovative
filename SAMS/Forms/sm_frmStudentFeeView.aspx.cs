using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommon.Classes;

public partial class Forms_frmStudentFeeView : System.Web.UI.Page
{
   

    Student_Controller mS_Ctrl = new Student_Controller();

    SLASHCodesController CodesCtl = new SLASHCodesController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='sm_frmStudentFee.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add New Fee</a>";
           
                this.LoadStatusRight();
                this.LoadStatus();

                if (Session["RoleID"].ToString() == "1")
                {
                    this.GetFEE(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,Constants.IntNullValue);
                }
                else
                {
                    this.GetFEE(Convert.ToInt32(Session["UserID"]),Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
                }
                Session.Add("SearchType", "All");
                FillRepeater();
            
           
        }
    }

    private void FillRepeater()
    {
        rEnquiryPrice.DataSource = null;
        rEnquiryPrice.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 20;

        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtStudent"];

        if (dt.Rows.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;

            rEnquiryPrice.DataSource = dt;
            rEnquiryPrice.DataBind();

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
                this.GetFEE(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            }
            else
            {
                this.GetFEE(Convert.ToInt32(Session["UserID"]), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
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
                this.GetFEE(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            }
            else
            {
                this.GetFEE(Convert.ToInt32(Session["UserID"]), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
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

    public DataTable GetFEE(int p_USER_ID, int p_PRGRAM_ID, int p_SESSION_ID, int p_TYPE_ID)
    {

        Session.Remove("dtStudent");
        DataTable dtStudent = mS_Ctrl.UspSelectFEE(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, CurrentPage + 1, 20);
        this.Session.Add("dtStudent", dtStudent);

        return dtStudent;
    }

    protected void rEnquiryPrice_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            this.Session.Add("FEE_ID", Convert.ToInt64(e.CommandArgument));
            
            Response.Redirect("sm_frmStudentFee.aspx?LevelType=3&LevelID=150");
        }
        //else if (e.CommandName == "del")
        //{
        //    if (EnqCtl.UpdateEnquiry(Convert.ToInt64(e.CommandArgument), Constants.IntNullValue, Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.EnquiryClosed, false))
        //    {
        //        this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier, Convert.ToInt32(dllEnquiryStatus.SelectedValue));
        //        FillRepeater();
        //        ltrlEnquiry.Text = Utility.ShowAlert(true, "Enquiry deleted successfully.");
        //    }
        //}
        //else if (e.CommandName == "approve")
        //{
        //    if (EnqCtl.UpdateEnquiry(Convert.ToInt64(e.CommandArgument), Constants.IntNullValue, Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.QuotationApproved, false))
        //    {
        //        this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier, Convert.ToInt32(dllEnquiryStatus.SelectedValue));
        //        FillRepeater();
        //        ltrlEnquiry.Text = Utility.ShowAlert(true, "Quotation approved successfully.");
        //    }
        //}
    }

    protected void rEnquiryPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
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
        Response.Redirect("frmStudentFeeView.aspx?LevelType=3&LevelID=151");
    }

    protected void btnCustomerSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtStudent"];
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
        Session.Add("dtStudent", dt);
        FillRepeater();

    }

    protected void btnDateSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtStudent"];
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
        Session.Add("dtStudent", dt);
        FillRepeater();

    }

    protected void btnStatusSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtStudent"];
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
        Session.Add("dtStudent", dt);
        FillRepeater();

    }

    protected void btnLastUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtStudent"];
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
        Session.Add("dtStudent", dt);
        FillRepeater();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        Session["SearchType"] = "Customer";
        SearchText();
    }

    private void LoadStatus()
    {
        //DataTable m_dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.EnquiryStatus, null, Constants.IntNullValue, true);
        //DataTable dtStatusRighs = (DataTable)Session["dtStatusRighs"];

        //DataTable dtFilter = new DataTable();
        //dtFilter = m_dt.Clone();

        //foreach (DataRow dr in dtStatusRighs.Rows)
        //{
        //    foreach (DataRow dr2 in m_dt.Rows)
        //    {
        //        if (dr2["REF_ID"].ToString() == dr["STATUS_ID"].ToString() && dr2["REF_ID"].ToString() != "668" && dr2["REF_ID"].ToString() != "728" && dr2["REF_ID"].ToString() != "683" && dr2["REF_ID"].ToString() != "738")
        //        {
        //            if (dr["CAN_VIEW"].ToString() == "True")
        //            {
        //                dtFilter.ImportRow(dr2);
        //                break;
        //            }
        //        }
        //    }
        //}

        //dllEnquiryStatus.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(this.dllEnquiryStatus, dtFilter, "REF_ID", "SLASH_DESC");

    }

    private void LoadStatusRight()
    {
        //RoleManagementController mController = new RoleManagementController();
        //int RoleID = Convert.ToInt32(Session["RoleID"]);
        //DataTable dtStatusRighs = mController.GetStatusRight(Constants.IntNullValue, Convert.ToInt32(RoleID));
        //Session.Add("dtStatusRighs", dtStatusRighs);
    }

    protected void dllEnquiryStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentPage = 0;
        Session["SearchType"] = "Status";
        SearchStatus();
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
        if (dllEnquiryStatus.SelectedValue != Constants.IntNullValue.ToString())
        {
            StatusIDs = dllEnquiryStatus.SelectedValue;
        }
        else
        {
            foreach (DataRow dr in dtStatusRighs.Rows)
            {
                if (dr["CAN_VIEW"].ToString() == "True")
                {
                    StatusIDs += dr["STATUS_ID"].ToString() + ",";
                }
            }

        }
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
        if (dllEnquiryStatus.SelectedValue != Constants.IntNullValue.ToString())
        {
            StatusIDs = dllEnquiryStatus.SelectedValue;
        }
        else
        {
            foreach (DataRow dr in dtStatusRighs.Rows)
            {
                if (dr["CAN_VIEW"].ToString() == "True")
                {
                    StatusIDs += dr["STATUS_ID"].ToString() + ",";
                }
            }

        }
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
        if (dllEnquiryStatus.SelectedValue != Constants.IntNullValue.ToString())
        {
            StatusIDs = dllEnquiryStatus.SelectedValue;
        }
        else
        {
            foreach (DataRow dr in dtStatusRighs.Rows)
            {
                if (dr["CAN_VIEW"].ToString() == "True")
                {
                    StatusIDs += dr["STATUS_ID"].ToString() + ",";
                }
            }

        }
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