using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class pr_frmGazettedHolidays : System.Web.UI.Page
{
    CompanyController CC = new CompanyController();
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    EmployeeController ec = new EmployeeController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            txtDate.Attributes.Add("readonly", "readonly");
            ResetForm();
        }
    }
    
    protected void fillddlParentDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        clsWebFormUtil.FillListBox(cblDepartment, dt, 1, 2, false);

        if (Session["Add"] != null)
        {
            foreach (ListItem li in cblDepartment.Items)
            {
                li.Selected = true;
            }
        }
    }
    
    private void FillddlContractType()
    {
        DataTable dt = ec.SelectEmployee_Contract(Constants.IntNullValue, (Int32.Parse(Session["WorkingCompanyID"].ToString())));
        ddlContractType.Items.Clear();
        ddlContractType.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlContractType, dt, 0, 2, false);
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No date selected.');", true);
                return;
            }

            bool DeptCount = false;
            foreach (ListItem li in cblDepartment.Items)
            {
                if (li.Selected)
                {
                    DeptCount = true;
                    break;
                }
            }
             
            if (!DeptCount)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No Department selected.');", true);
                return;
            }

            if (hdnGazettedID.Value == "" || hdnGazettedID.Value == null)
            {
                long GazettedHolidaysID = CC.InsertGazettedHoliday(Convert.ToDateTime(this.txtDate.Text), txtDescription.Text, Convert.ToInt32(Session["UserID"]));
                if (GazettedHolidaysID > 0)
                {
                    bool flag = false;
                    if (cbDepartment.Checked)
                    {
                        flag = CC.InsertGazettedHolidayDetail(GazettedHolidaysID, Constants.IntNullValue, Convert.ToInt32(ddlContractType.SelectedValue));
                    }
                    else
                    {
                        foreach (ListItem li in cblDepartment.Items)
                        {
                            if (li.Selected)
                            {
                                flag = CC.InsertGazettedHolidayDetail(GazettedHolidaysID, Convert.ToInt32(li.Value), Convert.ToInt32(ddlContractType.SelectedValue));
                            }
                        }
                    }
                    if (flag)
                    {
                        Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
                    return;
                }
            }
            else
            {
                if (CC.UpdateGazettedHoliday(Convert.ToInt32(hdnGazettedID.Value), Convert.ToDateTime(this.txtDate.Text), txtDescription.Text, Convert.ToInt32(Session["UserID"])))
                {
                    bool flag = false;
                    if (cbDepartment.Checked)
                    {
                        flag = CC.InsertGazettedHolidayDetail(Convert.ToInt32(hdnGazettedID.Value), Constants.IntNullValue, Convert.ToInt32(ddlContractType.SelectedValue));
                    }
                    else
                    {
                        foreach (ListItem li in cblDepartment.Items)
                        {
                            if (li.Selected)
                            {
                                flag = CC.InsertGazettedHolidayDetail(Convert.ToInt32(hdnGazettedID.Value), Convert.ToInt32(li.Value), Convert.ToInt32(ddlContractType.SelectedValue));
                            }
                        }
                    }
                    if (flag)
                    {
                        Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('" + ex.Message + "');", true);
        }
    }
    
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    
    private void ResetForm()
    {
        fillddlParentDepartment();
        FillddlContractType();
        fillrptGazetted(Constants.IntNullValue);
        hdnGazettedID.Value = string.Empty;
        if (Session["Add"] != null)
        {
            pnlGazettedContent.Visible = true;
            pnlGazettedGrid.Visible = false;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("GazettedHolidayID");
        }
        else if (Session["Edit"] != null)
        {
            pnlGazettedContent.Visible = true;
            pnlGazettedGrid.Visible = false;

            DataTable dt = CC.GetGazettedHoliday(Convert.ToInt32(Session["GazettedHolidayID"]));
            hdnGazettedID.Value = dt.Rows[0]["GazettedHolidayID"].ToString();
            txtDescription.Text = dt.Rows[0]["Description"].ToString();
            txtDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"]).ToString("dd-MMM-yyyy");

            DataTable dtDetail = CC.GetGazettedHolidayDetail(Convert.ToInt32(Session["GazettedHolidayID"]));
            if (dtDetail.Rows.Count > 0)
            {
                if(dtDetail.Rows[0]["EmployeeType"].ToString() != "")
                {
                    ddlContractType.SelectedValue = dtDetail.Rows[0]["EmployeeType"].ToString();
                }
                cbDepartment.Checked = false;
                if (dtDetail.Rows[0]["DepartmentID"].ToString() == "")
                {
                    foreach (ListItem li in cblDepartment.Items)
                    {
                        li.Selected = true;
                    }
                    cbDepartment.Checked = true;
                }
                else
                {
                    foreach (ListItem li in cblDepartment.Items)
                    {
                        li.Selected = false;
                        foreach (DataRow dr in dtDetail.Rows)
                        {
                            if (li.Value == dr["DepartmentID"].ToString())
                            {
                                li.Selected = true;
                            }
                        }
                    }
                }
            }

            imgGazetted.ImageUrl = "../images/btn-update.png";
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("GazettedHolidayID");
        }
        else
        {
            pnlGazettedContent.Visible = false;
            pnlGazettedGrid.Visible = true;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("GazettedHolidayID");
        }
    }
    
    protected void btnShowpnlGazettedContent_Click(object sender, EventArgs e)
    {
        Session.Add("Add", "Add");
        Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    
    protected void rptGazetted_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Session.Add("Edit", "Edit");
            Session.Add("GazettedHolidayID", e.CommandArgument.ToString());
            Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
        }
        else if (e.CommandName == "delete")
        {
            if (CC.DeleteGazettedHoliday(Convert.ToInt32(e.CommandArgument)))
            {
                fillrptGazetted(Constants.IntNullValue);
            }
        }
    }
    
    private void fillrptGazetted(int GazettedHolidayID)
    {
        DataTable dt = CC.GetGazettedHoliday(GazettedHolidayID);
        rptGazetted.DataSource = dt;
        rptGazetted.DataBind();
    }    
}