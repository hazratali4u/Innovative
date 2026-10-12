using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommon.Classes;

public partial class Forms_frmTemporaryGatePassView : System.Web.UI.Page
{
    TGPController Tgp_Ctrl = new TGPController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ltrAdd.Text = "<br>&nbsp;&nbsp;&nbsp;&nbsp;<a href='frmTemporaryGatePass.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'>Add New Temporary Gate Pass</a>";
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
  
    public DataTable GetGATEPASS(int p_USER_ID, int p_TGP_ID, int p_TYPE_ID)
    {

        Session.Remove("dtGatePass");
        DataTable dtGatePass = Tgp_Ctrl.UspSelectGATEPASS(Constants.IntNullValue, Constants.IntNullValue, 1, CurrentPage + 1, 20);
        this.Session.Add("dtGatePass", dtGatePass);

        return dtGatePass;
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

    protected void rGatePass_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int TGP_ID = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "view")
        {
            this.Session.Add("TGP_ID", TGP_ID);

            Response.Redirect("frmTemporaryGatePass.aspx?LevelType=3&LevelID=96");
        }
        else if (e.CommandName == "del")
        {
           
            

            DataSet ds = null;

            

            ds = Tgp_Ctrl.UspSelectGATEPASSREPORT(Constants.IntNullValue, Convert.ToInt32(TGP_ID), 3);

           
             SAMSBusinessLayer.Reports.CrpTemporaryGatePass CrpReport = new SAMSBusinessLayer.Reports.CrpTemporaryGatePass();

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
        else if (e.CommandName == "close")
        {
            if (Tgp_Ctrl.UpdateTGP(TGP_ID))
            {
                ltrlTGP.Text = Utility.ShowAlert(true, "TGP Close succesfully");
            }
            else
            {
                ltrlTGP.Text = Utility.ShowAlert(false, "Some error occurred");
            }
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

    protected void linkbtnprev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;

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
    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;

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
  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmTemporaryGatePassView.aspx?LevelType=3&LevelID=151");
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