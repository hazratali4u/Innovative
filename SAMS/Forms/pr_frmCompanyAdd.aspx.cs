using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// From To Assign/UnAssign Location, Principals And VoucherTypes To Users
/// </summary>
public partial class Forms_pr_frmCompanyAdd : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    /// 
    CompanyController cc = new CompanyController();
    private void EditCompany()
    {
        DataTable dt = cc.SelectCompany_User(Int32.Parse(Session["EditCompanyID"].ToString()), Constants.IntNullValue);
        txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
        txtCompanyTagLine.Text = dt.Rows[0]["CompanyTagline"].ToString();
        txtServiceTaxNumber.Text = dt.Rows[0]["CompanyServiceTaxNumber"].ToString();
        txtNTNNumber.Text = dt.Rows[0]["CompanyNTNnumber"].ToString();
        txtGSTNumber.Text = dt.Rows[0]["CompanyGSTNumber"].ToString();
        txtCompanyUAN.Text = dt.Rows[0]["CompanyUAN"].ToString();
        txtCompanyURL.Text = dt.Rows[0]["CompanyURL"].ToString();
        txtChamberRefNumber.Text = dt.Rows[0]["CompanyChamberRefNumber"].ToString();
        txtImport_ExportLicenseNumber.Text = dt.Rows[0]["CompanyImportExportLicenseNumber"].ToString();
        txtCompanyFormFooter.Text = dt.Rows[0]["CompanyFormFooter"].ToString();
        txtCompanyReportFooter.Text = dt.Rows[0]["CompanyReportFooter"].ToString();
        ddlPayPeriodFrom.SelectedValue = dt.Rows[0]["PayPeriodFrom"].ToString();
        ddlPayPeriodTo.SelectedValue = dt.Rows[0]["PayPeriodTo"].ToString();
        if (dt.Rows[0]["FiscalYearFrom"].ToString() != "")
        {
            txtYearFrom.Text = Convert.ToDateTime(dt.Rows[0]["FiscalYearFrom"]).ToString("dd-MMM-yyyy");
        }
        if (dt.Rows[0]["FiscalYearTo"].ToString() != "")
        {
            txtYearTo.Text = Convert.ToDateTime(dt.Rows[0]["FiscalYearTo"]).ToString("dd-MMM-yyyy");
        }
        ddlisActive.SelectedValue = dt.Rows[0]["IS_ACTIVE"].ToString();
        hdnCompanyID.Value = dt.Rows[0]["CompanyID"].ToString();
        fillddlLocation(Int32.Parse(hdnCompanyID.Value));
        Session["LogoName"] = dt.Rows[0]["CompanyLogoPath"].ToString();
        fillrptContacts();
        fillrptBanks();
        fillrptLocations();
        fillLstOfdays();
    }
    private DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("logo", typeof(string));
        return dt;
    }
    private void fillLstOfdays()
    {
        DataTable dt = cc.SelectOffDays(Constants.IntNullValue, Int32.Parse(Session["EditCompanyID"].ToString()), Constants.IntNullValue);
        if (dt != null)
        {
            var items = lstOffDays.Items.Cast<ListItem>().ToList();
            foreach (var item in items)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (item.Value == dr["OffDayValue"].ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtYearFrom.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            txtYearTo.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            txtYearFrom.Attributes.Add("readonly", "readonly");
            txtYearTo.Attributes.Add("readonly", "readonly");
            fillPayPeriodFrom();
            fillPayPeriodto();
            if (Session["EditCompanyID"] != null)
            {
                EditCompany();
            }
        }
    }
    private void fillPayPeriodFrom()
    {
        for (int i = 1; i <= 31; i++)
        {
            ddlPayPeriodFrom.Items.Add(i.ToString());
        }
    }
    private void fillPayPeriodto()
    {
        for (int i = 31; i >= 1; i--)
        {
            ddlPayPeriodTo.Items.Add(i.ToString());
        }
    }
    private void fillddlLocation(int CompanyID)
    {
        DataTable dt = cc.SelectCompany_lOCATIONS(CompanyID, Constants.IntNullValue, Constants.IntNullValue);
        ddlLocation.Items.Clear();
        ddlLocation.Items.Add("--Select--");
        clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);
    }
    protected void tbUserAssignment_ActiveTabChanged(object sender, EventArgs e)
    {
    }
    private void ResetForm()
    {
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtBankAccountNo.Text = "";
        txtBankBranch.Text = "";
        txtBankCode.Text = "";
        txtBankName.Text = "";
        txtBapenkAccountType.Text = "";
        txtChamberRefNumber.Text = "";
        txtCity.Text = "";
        txtCompanyFormFooter.Text = "";
        txtCompanyName.Text = "";
        txtCompanyReportFooter.Text = "";
        txtCompanyTagLine.Text = "";
        txtCompanyUAN.Text = "";
        txtCompanyURL.Text = "";
        txtContactPerson.Text = "";
        txtCountry.Text = "";
        txtEmail.Text = "";
        txtFax.Text = "";
        txtGSTNumber.Text = "";
        txtImport_ExportLicenseNumber.Text = "";
        txtLocation.Text = "";
        txtNTNNumber.Text = "";
        txtPhone.Text = "";
        txtServiceTaxNumber.Text = "";
        txtUnitName.Text = "";
        hdnCompanyID.Value = "";
        hdnCompanyContactID.Value = "";
        hdnCompanyLocationID.Value = "";
        hdnCompanyBankID.Value = "";
        hdnUnitID.Value = "";
        ddlLocationIsActive.SelectedIndex = 0;
        ddlUnitIsActive.SelectedIndex = 0;
        if (Session["EditCompanyID"] != null)
        {
            Session.Remove("EditCompanyID");
        }
        if (Session["LogoName"] != null)
        {
            Session.Remove("LogoName");
        }
        DataTable dtCompanies = cc.SelectCompany_User(Constants.IntNullValue, Constants.IntNullValue);
        if (dtCompanies != null)
        {
            if (dtCompanies.Rows.Count > 0)
            {
                this.Session["dtCompanies"] = dtCompanies;
                this.Session["WorkingCompanyName"] = dtCompanies.Rows[0]["CompanyName"].ToString();
                this.Session["WorkingCompanyID"] = dtCompanies.Rows[0]["CompanyID"].ToString();
            }
        }
    }
    private void fillrptContacts()
    {
        DataTable dt = cc.SelectCompanyContact(Constants.IntNullValue, Int32.Parse(Session["EditCompanyID"].ToString()), false, true, Constants.IntNullValue);
        rptContents.DataSource = dt;
        rptContents.DataBind();
    }
    private void fillrptBanks()
    {
        DataTable dt = cc.SelectCompanyBankInfo(Constants.IntNullValue, Int32.Parse(Session["EditCompanyID"].ToString()), Constants.IntNullValue, true, false);
        rptBanks.DataSource = dt;
        rptBanks.DataBind();
    }
    private void fillrptLocations()
    {
        DataTable dt = cc.SelectCompany_lOCATIONS(Int32.Parse(Session["EditCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue);
        rptLocations.DataSource = dt;
        rptLocations.DataBind();
    }
    private void fillrptUnits()
    {
        DataTable dt = cc.SelectCompany_Units(Constants.IntNullValue, Int32.Parse(ddlLocation.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, true, false);
        rptUnits.DataSource = dt;
        rptUnits.DataBind();
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = null;
            if (FUCompanyLogo.HasFile)
            {
                string path = Server.MapPath("~/CompanyLogos");
                string fExtension = "";
                FileInfo oFileInfo = new FileInfo(FUCompanyLogo.PostedFile.FileName);
                fExtension = Path.GetExtension(FUCompanyLogo.FileName);
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "LOGO" + fExtension;
                string fullFileName = path + "\\" + fileName;
                string imagePath = "CompanyLogos/" + fileName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FUCompanyLogo.PostedFile.SaveAs(fullFileName);
            }
            if (hdnCompanyID.Value != null && hdnCompanyID.Value != "")// Update  Company Genral Info
            {
                cc.UpdateCOMPANY(Int32.Parse(hdnCompanyID.Value), Constants.IntNullValue, txtCompanyName.Text, txtCompanyTagLine.Text, fileName, txtServiceTaxNumber.Text, txtNTNNumber.Text, txtGSTNumber.Text, txtCompanyUAN.Text, txtCompanyURL.Text, txtChamberRefNumber.Text, txtImport_ExportLicenseNumber.Text, txtCompanyFormFooter.Text, txtCompanyReportFooter.Text, Int32.Parse(ddlPayPeriodFrom.SelectedValue), Int32.Parse(ddlPayPeriodTo.SelectedValue), false, Boolean.Parse(ddlisActive.SelectedValue), Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                    , Convert.ToDateTime(txtYearFrom.Text), Convert.ToDateTime(txtYearTo.Text));
                insertUppdateOffDays(Int32.Parse(hdnCompanyID.Value));
                if (hdnCompanyContactID.Value == "" || hdnCompanyContactID.Value == null)// InsertCompanyContactInfo
                {
                    if (txtPhone.Text != "" || txtFax.Text != "" || txtEmail.Text != "" || txtAddress1.Text != "" || txtAddress2.Text != "" || txtCity.Text != "" || txtCountry.Text != "")
                    {
                        cc.InsertCompanyContact(Int32.Parse(hdnCompanyID.Value), txtPhone.Text, txtFax.Text, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtCity.Text, txtCountry.Text, true, Int32.Parse(Session["UserID"].ToString()), DateTime.Now);
                    }
                }
                else //Update Company Contatc INfo
                {
                    if (txtPhone.Text != "" || txtFax.Text != "" || txtEmail.Text != "" || txtAddress1.Text != "" || txtAddress2.Text != "" || txtCity.Text != "" || txtCountry.Text != "")
                    {
                        cc.UpdateCompanyContact(Int32.Parse(hdnCompanyContactID.Value), Int32.Parse(hdnCompanyID.Value), txtPhone.Text, txtFax.Text, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtCity.Text, txtCountry.Text, false, true, Int32.Parse(Session["UserID"].ToString()));
                    }
                }
                if (hdnCompanyBankID.Value == "" || hdnCompanyBankID.Value == null)//Insert CompanyBankInfo
                {
                    if (txtBankName.Text != "" || txtBankCode.Text != "" || txtBankAccountNo.Text != "" || txtContactPerson.Text != "" || txtBankBranch.Text != "" || txtBapenkAccountType.Text != "")
                    {
                        cc.InsertCompanyBankInfo(Int32.Parse(hdnCompanyID.Value), txtBankAccountNo.Text, txtBankName.Text, txtBapenkAccountType.Text, txtBankCode.Text, txtBankBranch.Text, txtContactPerson.Text, Convert.ToInt32(Session["UserID"]), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                    }
                }
                else //Update  Company Bank Info
                {
                    if (txtBankName.Text != "" || txtBankCode.Text != "" || txtBankAccountNo.Text != "" || txtContactPerson.Text != "" || txtBankBranch.Text != "" || txtBapenkAccountType.Text != "")
                    {
                        cc.UpdateCompanyBankInfo(Int32.Parse(hdnCompanyBankID.Value), Int32.Parse(hdnCompanyID.Value), txtBankAccountNo.Text, txtBankName.Text, txtBapenkAccountType.Text, txtBankCode.Text, txtBankBranch.Text, txtContactPerson.Text, Convert.ToInt32(Session["UserID"]), true, false);
                    }

                }
                if (hdnCompanyLocationID.Value == "" || hdnCompanyLocationID.Value == null) // insert company Location
                {
                    if (txtLocation.Text != "")
                    {
                        cc.InsertCompany_Locations(Int32.Parse(hdnCompanyID.Value), txtLocation.Text, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), Convert.ToBoolean(ddlLocationIsActive.SelectedValue));
                    }
                }
                else // Update Company Location
                {
                    if (txtLocation.Text != "")
                    {
                        cc.UpdateCompany_Locations(Int32.Parse(hdnCompanyLocationID.Value), Int32.Parse(hdnCompanyID.Value), txtLocation.Text, Int32.Parse(Session["UserID"].ToString()), Convert.ToBoolean(ddlLocationIsActive.SelectedValue), false);
                    }
                }
                if (hdnUnitID.Value == "" || hdnUnitID.Value == null)//Insert New Unit Uder Location
                {
                    if (txtUnitName.Text != "" && ddlLocation.SelectedValue != null && ddlLocation.SelectedValue != "") //check Data for Insertion
                    {
                        cc.InsertCompany_Units(Int32.Parse(ddlLocation.SelectedValue), Int32.Parse(hdnCompanyID.Value), txtUnitName.Text, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), Convert.ToBoolean(ddlUnitIsActive.SelectedValue));
                    }
                }
                else // Update Unit Of Locatin
                {
                    if (txtUnitName.Text != "" && ddlLocation.SelectedValue != null && ddlLocation.SelectedValue != "") //check Data for Updation
                    {
                        cc.UpdateCompany_Units(Int32.Parse(hdnUnitID.Value), Int32.Parse(ddlLocation.SelectedValue), Int32.Parse(hdnCompanyID.Value), txtUnitName.Text, Int32.Parse(Session["UserID"].ToString()), Convert.ToBoolean(ddlUnitIsActive.SelectedValue), false);
                    }
                }
                //ResetForm();
                //Session["alert"] = "Company Updated Successfully..!";
                ResetForm2();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Company Updated Successfully...!');", true);
                return;
            }
            else
            {
                if (txtCompanyName.Text != null && txtCompanyName.Text != "")
                {
                    int CompanyID = cc.InsertCompany(1, txtCompanyName.Text, txtCompanyTagLine.Text, fileName, txtServiceTaxNumber.Text, txtNTNNumber.Text, txtGSTNumber.Text, txtCompanyUAN.Text, txtCompanyURL.Text, txtChamberRefNumber.Text, txtImport_ExportLicenseNumber.Text, txtCompanyFormFooter.Text, txtCompanyReportFooter.Text, Int32.Parse(ddlPayPeriodFrom.SelectedValue), Int32.Parse(ddlPayPeriodTo.SelectedValue), bool.Parse(ddlisActive.SelectedValue), Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                        , Convert.ToDateTime(txtYearFrom.Text), Convert.ToDateTime(txtYearTo.Text));
                    insertUppdateOffDays(CompanyID);
                    if (txtPhone.Text != "" || txtFax.Text != "" || txtEmail.Text != "" || txtAddress1.Text != "" || txtAddress2.Text != "" || txtCity.Text != "" || txtCountry.Text != "")
                    {
                        cc.InsertCompanyContact(CompanyID, txtPhone.Text, txtFax.Text, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtCity.Text, txtCountry.Text, true, Int32.Parse(Session["UserID"].ToString()), DateTime.Now);
                    }
                    if (txtBankName.Text != "" || txtBankCode.Text != "" || txtBankAccountNo.Text != "" || txtContactPerson.Text != "" || txtBankBranch.Text != "" || txtBapenkAccountType.Text != "")
                    {
                        cc.InsertCompanyBankInfo(CompanyID, txtBankAccountNo.Text, txtBankName.Text, txtBapenkAccountType.Text, txtBankCode.Text, txtBankBranch.Text, txtContactPerson.Text, Convert.ToInt32(Session["UserID"]), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                    }
                    if (txtLocation.Text != "")
                    {
                        cc.InsertCompany_Locations(CompanyID, txtLocation.Text, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), Convert.ToBoolean(ddlLocationIsActive.SelectedValue));
                    }
                    //Session["alert"] = "Company Saved Successfully Now you can assign it to users ...!";                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert(' " + "Company Saved Successfully Now you can assign it to users ...!');window.location.href = 'pr_frmCompany.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void ResetForm2()
    {
        fillrptContacts();
        fillrptBanks();
        fillrptLocations();
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        try
        {
            ResetForm();
            Response.Redirect("pr_frmCompany.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString());

        }
        catch (Exception ex)
        {

        }
    }
    protected void rptContents_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = cc.SelectCompanyContact(Int32.Parse(e.CommandArgument.ToString()), Constants.IntNullValue, false, true, Constants.IntNullValue);
        txtAddress1.Text = dt.Rows[0]["AddressL1"].ToString();
        txtPhone.Text = dt.Rows[0]["Phone"].ToString();
        txtFax.Text = dt.Rows[0]["Fax"].ToString();
        txtEmail.Text = dt.Rows[0]["Email"].ToString();
        txtAddress2.Text = dt.Rows[0]["AddressL2"].ToString();
        txtCity.Text = dt.Rows[0]["City"].ToString();
        txtCountry.Text = dt.Rows[0]["Country"].ToString();
        hdnCompanyContactID.Value = dt.Rows[0]["CompanyContactID"].ToString();
    }
    protected void rptBanks_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = cc.SelectCompanyBankInfo(Int32.Parse(e.CommandArgument.ToString()), Constants.IntNullValue, Constants.IntNullValue, true, false);
        txtBankAccountNo.Text = dt.Rows[0]["BankAccountNumber"].ToString();
        txtBankName.Text = dt.Rows[0]["BankName"].ToString();
        txtBapenkAccountType.Text = dt.Rows[0]["BankAccountType"].ToString();
        txtBankCode.Text = dt.Rows[0]["BankCode"].ToString();
        txtBankBranch.Text = dt.Rows[0]["BankBranch"].ToString();
        txtContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
        hdnCompanyBankID.Value = dt.Rows[0]["CompanyBankInfoID"].ToString();
    }
    protected void rptLocations_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = cc.SelectCompany_lOCATIONS(Constants.IntNullValue, Int32.Parse(e.CommandArgument.ToString()), Constants.IntNullValue);
        txtLocation.Text = dt.Rows[0]["LocationName"].ToString();
        hdnCompanyLocationID.Value = dt.Rows[0]["CompanyLocationID"].ToString();
        ddlLocationIsActive.SelectedValue = dt.Rows[0]["IS_ACTIVE"].ToString();
    }
    protected void rptUnits_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = cc.SelectCompany_Units(Constants.IntNullValue, Int32.Parse(ddlLocation.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, true, false);
        txtUnitName.Text = dt.Rows[0]["UnitName"].ToString();
        hdnUnitID.Value = dt.Rows[0]["UnitID"].ToString();
        ddlUnitIsActive.SelectedValue = dt.Rows[0]["IS_ACTIVE"].ToString();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedIndex != 0)
        {
            fillrptUnits();
        }

        else
        {
            rptUnits.DataSource = null;
            rptUnits.DataBind();
        }
    }
    private void insertUppdateOffDays(int CompanyID)
    {
        if (Session["EditCompanyID"] != null)
        {
            cc.DeleteOffDays(Constants.IntNullValue, CompanyID, Constants.IntNullValue);
            var selected = lstOffDays.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
            lstOffDays.Items.Clear();
            foreach (var item in selected)
            {
                cc.InsertOffDays(CompanyID, Int32.Parse(item.Value), item.Text);
            }
        }
        else
        {
            var selected = lstOffDays.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
            lstOffDays.Items.Clear();
            foreach (var item in selected)
            {
                cc.InsertOffDays(CompanyID, Int32.Parse(item.Value), item.Text);
            }
        }

    }
    private void InsertUpdateCompany()
    {
        try
        {

        }
        catch
        {
        }

    }
}