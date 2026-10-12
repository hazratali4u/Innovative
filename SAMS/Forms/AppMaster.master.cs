using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Text;

public partial class AppMaster : System.Web.UI.MasterPage
{
    MenuController MenuCtl = new MenuController();
    RoleManagementController ObjRole = new RoleManagementController();
    CompanyController cc = new CompanyController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int RoleID = (int)Session["RoleID"];
            if (RoleID == 1)
            {
                StringBuilder strUser = new StringBuilder();
                strUser.Append("<li><a href='frmChangePassword.aspx'>Change Password</a></li>");
                strUser.Append("<li><a href='frmMenu.aspx'>Arrange Menu</a></li>");
                strUser.Append("<li><a href='Logout.aspx'>Log Out</a></li>");
                ltrlUser.Text = strUser.ToString();
               // ltrlRole.Text = "<a href='frmRoleManagement.aspx'>Role Mangement</a>";

            }
            else
            {
                StringBuilder strUser = new StringBuilder();
                strUser.Append("<li><p>&nbsp;</p></li>");
                strUser.Append("<li><a href='frmChangePassword.aspx'>Change Password</a></li>");
                strUser.Append("<li><a href='Logout.aspx'>Log Out</a></li>");
                ltrlUser.Text = strUser.ToString();
            }
            lblUser.Text = Session["UserName"].ToString();
            lblWorkingDate.Text = ((DateTime)this.Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            lblWorkingCompany.Text = Session["WorkingCompanyName"].ToString();
            ltrlMenu.Text = GetTopMenu();
            LoadLeftMenu(RoleID);
            DataTable dt = Session["dtCompanies"] as DataTable;
            clsWebFormUtil.FillListBox(lstCompanies, dt, 0, 2, true);
            lstCompanies.SelectedValue = Session["WorkingCompanyID"].ToString();
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Session["WorkingCompanyName"] = lstCompanies.SelectedItem.Text;
        Session["WorkingCompanyID"] = lstCompanies.SelectedValue;
        DataTable dtCompanies = cc.SelectCompany_User(Int32.Parse(lstCompanies.SelectedValue), Constants.IntNullValue);
        Session["CompanyLogoPath"] = dtCompanies.Rows[0]["CompanyLogoPath"];
        lblWorkingCompany.Text = Session["WorkingCompanyName"].ToString();
        lstCompanies.SelectedValue = Session["WorkingCompanyID"].ToString();
        Response.Redirect(Request.Url.PathAndQuery);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lstCompanies.SelectedValue = Session["WorkingCompanyID"].ToString();
    }
    private void LoadMainMenu(int RoleID, int TypeID, int ModuleID)
    {
        RoleManagementController ObjRole = new RoleManagementController();
        DataTable dtParent = ObjRole.SelectRoleWiseModule(RoleID, TypeID, ModuleID);
        if (dtParent != null)
        {
            gvLeftMenu.DataSource = dtParent;
            gvLeftMenu.DataBind();
        }
    }
    private string GetTopMenu()
    {
        DataTable dtBreadCrumb = MenuCtl.GetMenu(Constants.IntNullValue, Constants.IntNullValue, 12, (int)Session["RoleID"]);
        StringBuilder strBreadCrumb = new StringBuilder();
        if (dtBreadCrumb != null && dtBreadCrumb.Rows.Count > 0)
        {
            foreach (DataRow row in dtBreadCrumb.Rows)
            {
                if (row["FirstLink"].ToString() != "")
                {
                    strBreadCrumb.Append("<a href='" + row["FirstLink"].ToString() + "?LevelType=3&LevelID=" + row["FirstLinkID"] + "&TopID=" + row["MENU_ID"] + "'>" + row["MENU_DESCRIPTION"].ToString() + "</a>");
                }               
            }
        }
        return strBreadCrumb.ToString();
    }
    private void LoadLeftMenu(int RoleID)
    {
        if (Request.QueryString["TopID"] != null)
        {
            DataTable dtParent = ObjRole.SelectRoleWiseModule(RoleID, 3, Convert.ToInt32(Request.QueryString["TopID"]));
            if (dtParent != null)
            {
                gvLeftMenu.DataSource = dtParent;
                gvLeftMenu.DataBind();
            }
        }
    }
    protected void gvLeftMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvLeftMenuSub = (GridView)e.Row.FindControl("gvLeftMenuSub");
            int ParentID = int.Parse(((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString());
            DataTable dtChild = ObjRole.SelectRoleWiseModule(Convert.ToInt32(Session["RoleID"]), 4, ParentID);
            if (dtChild != null)
            {
                gvLeftMenuSub.DataSource = dtChild;
                gvLeftMenuSub.DataBind();
            }
        }
    }
}
