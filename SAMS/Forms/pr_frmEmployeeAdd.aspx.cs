using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Models;
using SAMSCommon.Classes;

/// <summary>
/// From To Assign/UnAssign Location, Principals And VoucherTypes To Users
/// </summary>
public partial class pr_frmEmployeeAdd : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    /// 
    CompanyController cc = new CompanyController();
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    DistributorController DistCtl = new DistributorController();
    EmployeeController ec = new EmployeeController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetEmployeeForm();
            if (Session["EditEmployeeID"] != null)
            {
                EditEmployee();
                TabPanel4.Visible = true;                
                ScriptManager.GetCurrent(this).RegisterPostBackControl(btnUpload);
            }
            ModalPopupExtender.Show();
        }
    }
    protected void EditEmployee()
    {
        DataTable dt = ec.SelectEmployee(Int32.Parse(Session["EditEmployeeID"].ToString()), Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, false, Convert.ToInt32(Session["UserID"]));
        txtBankAccountNumber.Text = dt.Rows[0]["EmployeeBankAccountNumber"].ToString();
        txtBankAccountTitle.Text = dt.Rows[0]["EmployeeBankAccountTitle"].ToString();
        txtBankName.Text = dt.Rows[0]["EmployeeBankName"].ToString();
        txtBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();
        if (dt.Rows[0]["EmployeeDateOfBirth"].ToString() != "")
        {
            txtDateOfBirth.Text = Convert.ToDateTime(dt.Rows[0]["EmployeeDateOfBirth"]).ToString("dd-MMM-yyyy");
        }
        txtDrivingLicenseNumber.Text = dt.Rows[0]["EmployeeDrivingLicenseNumber"].ToString();
        if (dt.Rows[0]["EmployeeDrivingLicenseValidUpto"].ToString() != "")
        {
            txtDrivingLicenseValidUpto.Text = Convert.ToDateTime(dt.Rows[0]["EmployeeDrivingLicenseValidUpto"]).ToString("dd-MMM-yyyy");
        }
        txtEmergencyContactName.Text = dt.Rows[0]["EmployeeEmergencyContactName"].ToString();
        txtEmergencyContactNumber.Text = dt.Rows[0]["EmployeeEmergencyContactNumber"].ToString();
        txtEmployeeCNIC.Text = dt.Rows[0]["EmployeeCNIC"].ToString();
        if (dt.Rows[0]["EmployeeDurationFrom"].ToString() != "")
        {
            txtEmployeeDurationFrom.Text = Convert.ToDateTime(dt.Rows[0]["EmployeeDurationFrom"]).ToString("dd-MMM-yyyy");
        }
        if (dt.Rows[0]["EmployeeDurationTo"].ToString() != "")
        {
            txtEmployeeDurationTo.Text = Convert.ToDateTime(dt.Rows[0]["EmployeeDurationTo"]).ToString("dd-MMM-yyyy");
        }
        txtEmployeeName.Text = dt.Rows[0]["EmployeeFullName"].ToString();
        txtEmployeeNTN.Text = dt.Rows[0]["EmployeeNTN"].ToString();
        txtEmployeeTag.Text = dt.Rows[0]["EmployeeTag"].ToString();
        txtEOBInumber.Text = dt.Rows[0]["EmployeeEOBInumber"].ToString();
        txtHomeAddress.Text = dt.Rows[0]["EmployeeHomeAddress"].ToString();
        txtNationality.Text = dt.Rows[0]["EmployeeNationality"].ToString();
        txtPassportNumber.Text = dt.Rows[0]["EmployeePassportNumber"].ToString();
        if (dt.Rows[0]["EmployeeProbationFrom"].ToString() != "")
        {
            txtProbationFrom.Text = Convert.ToDateTime(dt.Rows[0]["EmployeeProbationFrom"]).ToString("dd-MMM-yyyy");
        }
        if (dt.Rows[0]["EmployeeProbationTo"].ToString() != "")
        {
            txtProbationTo.Text = Convert.ToDateTime(dt.Rows[0]["EmployeeProbationTo"]).ToString("dd-MMM-yyyy");
        }
        txtSocialSecurityNumber.Text = dt.Rows[0]["EmployeeSocialSecurityNumber"].ToString();
        txtVisaInformation.Text = dt.Rows[0]["EmployeeVisaInformation"].ToString();
        txtWorkAddress.Text = dt.Rows[0]["EmployeeWorkingAddress"].ToString();
        txtWorkEmail.Text = dt.Rows[0]["EmployeeWorkEmail"].ToString();
        txtWorkMobileNo.Text = dt.Rows[0]["EmployeeWorkMobile"].ToString();
        txtWorkPhone.Text = dt.Rows[0]["EmployeeWorkPhone"].ToString();
        ddlContractType.SelectedValue = dt.Rows[0]["EmployeeContractTypeID"].ToString();
        ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentID"].ToString();
        ddlDesignation.SelectedValue = dt.Rows[0]["DesignationID"].ToString();
        ddlEmployeeGender.SelectedValue = dt.Rows[0]["EmployeeGender"].ToString();
        ddlEmployeeRole.SelectedValue = dt.Rows[0]["EmployeeIsAManager"].ToString();
        ddlException.SelectedValue = dt.Rows[0]["ExceptionID"].ToString();
        ddlisActive.SelectedValue = dt.Rows[0]["IS_ACTIVE"].ToString();
        ddlLocation.SelectedValue = dt.Rows[0]["EmployeeLocationID"].ToString();
        ddlLocation_SelectedIndexChanged(null, null);
        ddlUnit.SelectedValue = dt.Rows[0]["EmployeeUnitID"].ToString();
        ddlMaritalStatus.SelectedValue = dt.Rows[0]["EmployeeMaritalStatus"].ToString();
        ddlOvertime.SelectedValue = dt.Rows[0]["IsOvertimeEnabled"].ToString();
        ddlPF.SelectedValue = dt.Rows[0]["IsPFEnabled"].ToString();
        ddlPF_SelectedIndexChanged(null,null);
        txtPFundOpening.Text = dt.Rows[0]["PFundOpening"].ToString();
        txtLoanOpening.Text = dt.Rows[0]["LoanOpening"].ToString();
        txtLeaseOpening.Text = dt.Rows[0]["LeasOpening"].ToString();
        txtLeaveOpening.Text = dt.Rows[0]["LeaveOpening"].ToString();
        ddlDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
        ddlSalaryBehaviour.SelectedValue = dt.Rows[0]["SalaryBehaviorID"].ToString();
        ddlShift.SelectedValue = dt.Rows[0]["ShiftID"].ToString();
        hdnEmployeeID.Value = dt.Rows[0]["EmployeeID"].ToString();
        if (dt.Rows[0]["EmployeePicture"].ToString() != "")
        {
            iItem.ImageUrl = "~/CompanyLogos/" + dt.Rows[0]["EmployeePicture"].ToString();
        }
        else
        {
            iItem.ImageUrl = "~/images/noimage.jpg";
        }
        int SalaryStructureID = 0;
        if (dt.Rows[0]["SalaryStructureID"] != null)
        {
            SalaryStructureID = Int32.Parse(dt.Rows[0]["SalaryStructureID"].ToString());
        }
        ddlSalaryTemplate.SelectedValue = dt.Rows[0]["TemplateID"].ToString();
        DataTable dt1 = ccc.SelectSalaryStructure(SalaryStructureID, null, Constants.IntNullValue, 2, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.IntNullValue);
        fillSalaryStructure(dt1);
        hdnSalaryStructureID.Value = SalaryStructureID.ToString();
        if (dt1.Rows[0]["BasicPay"] != null)
        {
            txtBasicSalary.Text = dt1.Rows[0]["BasicPay"].ToString();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            int SalaryStructureID = InsertUpdateSalaryStructure();
            if (Session["EditEmployeeID"] != null)
            {
                EmployeeModel Employee = GetEmployeeInfo();
                Employee.EmployeeID = long.Parse(Session["EditEmployeeID"].ToString());
                bool insert = ec.UpdateEmployee(Employee);
                ec.InsertEmployeeArchive(Employee);
                EmployeeTranscationalDetailModel EmployeeTranstionalDetail = GetEmployeeTranscationalDetailInfo(long.Parse(Session["EditEmployeeID"].ToString()));
                EmployeeTranstionalDetail.TemplateID = Int32.Parse(ddlSalaryTemplate.SelectedValue);
                ec.UpdateEmployeeTranscationalDetail(EmployeeTranstionalDetail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('Employee Update Successfully...!');", true);
            }
            else
            {
                EmployeeModel Employee = GetEmployeeInfo();
                long EmployeeID = ec.InsertEmployee(Employee);
                Employee.EmployeeID = EmployeeID;
                ec.InsertEmployeeArchive(Employee);
                EmployeeTranscationalDetailModel EmployeeTranstionalDetail = GetEmployeeTranscationalDetailInfo(EmployeeID);
                EmployeeTranstionalDetail.TemplateID = Int32.Parse(ddlSalaryTemplate.SelectedValue);
                EmployeeTranstionalDetail.SalaryStructureID = SalaryStructureID;
                long etdID = ec.InsertEmployeeTranscationalDetail(EmployeeTranstionalDetail);
                EmployeeTranstionalDetail.EmployeeTranscationalDetailID = etdID;
                ec.InsertEmployeeTranscationalArchiveDetail(EmployeeTranstionalDetail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert(' " + "Employee Saved Successfully...!');window.location.href = 'pr_frmEmployee.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "'", true);
            }
        }
        catch (Exception exp)
        {
            Session["alert"] = exp.Message;
        }
    }
    private EmployeeModel GetEmployeeInfo()
    {
        EmployeeModel Employee = new EmployeeModel();
        Employee.BloodGroup = txtBloodGroup.Text;
        Employee.CompanyID = Int32.Parse(Session["WorkingCompanyID"].ToString());
        Employee.Document_Date = DateTime.Parse(Session["CurrentWorkDate"].ToString());
        Employee.EmployeeBankAccountNumber = txtBankAccountNumber.Text;
        Employee.EmployeeBankAccountTitle = txtBankAccountTitle.Text;
        Employee.EmployeeBankName = txtBankName.Text;
        Employee.EmployeeCNIC = txtEmployeeCNIC.Text;
        if (txtDateOfBirth.Text == "")
        {
            Employee.EmployeeDateOfBirth = Constants.DateNullValue;
        }
        else
        {
            Employee.EmployeeDateOfBirth = DateTime.Parse(txtDateOfBirth.Text);
        }

        Employee.EmployeeDrivingLicenseNumber = txtDrivingLicenseNumber.Text;
        if (txtDrivingLicenseValidUpto.Text == "")
        {
            Employee.EmployeeDrivingLicenseValidUpto = Constants.DateNullValue;
        }
        else
        {
            Employee.EmployeeDrivingLicenseValidUpto = DateTime.Parse(txtDrivingLicenseValidUpto.Text);
        }
        if (txtEmployeeDurationFrom.Text == "")
        {
            Employee.EmployeeDurationFrom = Constants.DateNullValue;
        }
        else
        {
            Employee.EmployeeDurationFrom = DateTime.Parse(txtEmployeeDurationFrom.Text);
        }
        if (txtEmployeeDurationTo.Text == "")
        {
            Employee.EmployeeDurationTo = Constants.DateNullValue;
        }
        else
        {
            Employee.EmployeeDurationTo = DateTime.Parse(txtEmployeeDurationTo.Text);
        }
        Employee.EmployeeEmergencyContactName = txtEmergencyContactName.Text;
        Employee.EmployeeEmergencyContactNumber = txtEmergencyContactNumber.Text;
        Employee.EmployeeEOBInumber = txtEOBInumber.Text;
        Employee.EmployeeFullName = txtEmployeeName.Text;
        if (ddlEmployeeGender.SelectedIndex == 0)
        {
            Employee.EmployeeGender = Constants.IntNullValue;
        }
        else
        {
            Employee.EmployeeGender = Int32.Parse(ddlEmployeeGender.SelectedValue);
        }
        Employee.EmployeeHomeAddress = txtHomeAddress.Text;
        if (ddlEmployeeRole.SelectedIndex == 0)
        {
            Employee.EmployeeIsAManager = false;
        }
        else
        {
            Employee.EmployeeIsAManager = Boolean.Parse(ddlEmployeeRole.SelectedValue);
        }
        Employee.EmployeeMachineTag1 = null;
        Employee.EmployeeMachineTag2 = null;
        Employee.EmployeeMachineTag3 = null;
        if (ddlMaritalStatus.SelectedIndex == 0)
        {
            Employee.EmployeeMaritalStatus = false;
        }
        else
        {
            Employee.EmployeeMaritalStatus = Boolean.Parse(ddlMaritalStatus.SelectedValue);
        }
        Employee.EmployeeNationality = txtNationality.Text;
        Employee.EmployeeNTN = txtEmployeeNTN.Text;
        Employee.EmployeeOtherInformation = null;
        Employee.EmployeePassportNumber = txtPassportNumber.Text;
        if (txtProbationFrom.Text == "")
        {
            Employee.EmployeeProbationFrom = Constants.DateNullValue;
        }
        else
        {
            Employee.EmployeeProbationFrom = DateTime.Parse(txtProbationFrom.Text);
        }
        if (txtProbationTo.Text == "")
        {
            Employee.EmployeeProbationTo = Constants.DateNullValue;
        }
        else
        {
            Employee.EmployeeProbationTo = DateTime.Parse(txtProbationTo.Text);
        }
        Employee.IS_ACTIVE = bool.Parse(ddlisActive.SelectedValue);
        Employee.EmployeeSocialSecurityNumber = txtSocialSecurityNumber.Text;
        Employee.EmployeeTag = txtEmployeeTag.Text;
        Employee.EmployeeVisaInformation = txtVisaInformation.Text;
        Employee.EmployeeWorkEmail = txtWorkEmail.Text;
        Employee.EmployeeWorkingAddress = txtWorkAddress.Text;
        Employee.EmployeeWorkMobile = txtWorkMobileNo.Text;
        Employee.EmployeeWorkPhone = txtWorkPhone.Text;
        Employee.IS_APPROVED = false;
        Employee.IsMonitored = false;
        if (ddlOvertime.SelectedIndex == 0)
        {
            Employee.IsOvertimeEnabled = false;
        }
        else
        {
            Employee.IsOvertimeEnabled = Boolean.Parse(ddlOvertime.SelectedValue);
        }
        if (ddlPF.SelectedIndex == 0)
        {
            Employee.IsPFEnabled = false;
        }
        else
        {
            Employee.IsPFEnabled = Boolean.Parse(ddlPF.SelectedValue);
        }
        
        Employee.TimeZone = null;
        Employee.User_ID = Int32.Parse(Session["UserID"].ToString());
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
            Employee.EmployeePicture = fileName;
            iItem.ImageUrl = "~/CompanyLogos/" + fileName;
        }
        else
        {
            iItem.ImageUrl = "~/images/noimage.jpg";
        }   
        Employee.DISTRIBUTOR_ID = Convert.ToInt32(ddlDistributor.SelectedValue);        
        return Employee;
    }
    private EmployeeTranscationalDetailModel GetEmployeeTranscationalDetailInfo(long EmployeeID)
    {
        DataControl dc = new DataControl();
        EmployeeTranscationalDetailModel ETD = new EmployeeTranscationalDetailModel();
        ETD.CompanyID = Int32.Parse(Session["WorkingCompanyID"].ToString());
        if (ddlDepartment.SelectedIndex == 0)
        {
            ETD.DepartmentID = 0;
        }
        else
        {
            ETD.DepartmentID = Int32.Parse(ddlDepartment.SelectedValue);
        }
        if (ddlDesignation.SelectedIndex == 0)
        {
            ETD.DesignationID = 0;
        }
        else
        {
            ETD.DesignationID = Int32.Parse(ddlDesignation.SelectedValue);
        }
        if (ddlContractType.SelectedIndex == 0)
        {
            ETD.EmployeeContractTypeID = 0;
        }
        else
        {
            ETD.EmployeeContractTypeID = Int32.Parse(ddlContractType.SelectedValue);
        }
        ETD.EmployeeID = EmployeeID;
        if (ddlLocation.SelectedIndex == 0)
        {
            ETD.EmployeeLocationID = 0;
        }
        else
        {
            ETD.EmployeeLocationID = Int32.Parse(ddlLocation.SelectedValue);
        }
        ETD.EmployeeWorkingScheduleID = 0;
        if (ddlException.SelectedIndex == 0)
        {
            ETD.ExceptionID = 0;
        }
        else
        {
            ETD.ExceptionID = Int32.Parse(ddlException.SelectedValue);
        }
        if (ddlSalaryBehaviour.SelectedIndex == 0)
        {
            ETD.SalaryBehaviorID = 0;
        }
        else
        {
            ETD.SalaryBehaviorID = Int32.Parse(ddlSalaryBehaviour.SelectedValue);
        }
        ETD.SalaryStructureID = Constants.IntNullValue;
        if (ddlShift.SelectedIndex == 0)
        {
            ETD.ShiftID = 0;
        }
        else
        {
            ETD.ShiftID = Int32.Parse(ddlShift.SelectedValue);
        }
        ETD.TemplateID = Constants.IntNullValue;
        ETD.EmployeeUnitID = int.Parse(ddlUnit.SelectedValue);
        ETD.PFundOpening = Convert.ToDecimal(dc.chkNull_0(txtPFundOpening.Text));
        ETD.LoanOpening = Convert.ToDecimal(dc.chkNull_0(txtLoanOpening.Text));
        ETD.LeasOpening = Convert.ToDecimal(dc.chkNull_0(txtLeaseOpening.Text));
        ETD.LeaveOpening = Convert.ToInt32(dc.chkNull_0(txtLeaveOpening.Text));
        return ETD;
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        ResetEmployeeForm();
        resetAfterUpdate();
    }
    protected void FillddlDepartments()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("--Select--", "0"));
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);
    }
    protected void FillddlDesignations()
    {
        DataTable dt = ccc.SelectDesignation(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDesignation.Items.Clear();
        ddlDesignation.Items.Add(new ListItem("--Select--", "0"));
        clsWebFormUtil.FillDropDownList(ddlDesignation, dt, 0, 1, false);
    }
    private void FillddlLocation()
    {
        DataTable dt = cc.SelectCompany_lOCATIONS(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]));
        ddlLocation.Items.Clear();
        ddlLocation.Items.Add(new ListItem("--select--", "0"));
        clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);
    }
    private void FillddlUnit(int LocationID)
    {
        DataTable dt = cc.SelectCompany_Units(Constants.IntNullValue, LocationID, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, true, false);
        ddlUnit.Items.Clear();
        ddlUnit.Items.Add(new ListItem("--select--", "0"));
        clsWebFormUtil.FillDropDownList(ddlUnit, dt, 0, 3, false);
    }
    private void FillddlContractType()
    {
        DataTable dt = ec.SelectEmployee_Contract(Constants.IntNullValue, (Int32.Parse(Session["WorkingCompanyID"].ToString())));
        ddlContractType.Items.Clear();
        ddlContractType.Items.Add(new ListItem("--Select--", "0"));
        clsWebFormUtil.FillDropDownList(ddlContractType, dt, 0, 2, false);
    }
    private void FillddlException()
    {
        DataTable dt = ccc.SelectWorkHoursException(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlException.Items.Clear();
        ddlException.Items.Add(new ListItem("--Select--", "0"));
        clsWebFormUtil.FillDropDownList(ddlException, dt, 0, 3, false);
    }
    private void FillddlShift()
    {
        DataTable dt = ccc.SelectShift(Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        ddlShift.Items.Clear();
        ddlShift.Items.Add(new ListItem("--Select--", "0"));
        clsWebFormUtil.FillDropDownList(ddlShift, dt, 0, 1, false);
    }
    private void ResetEmployeeForm()
    {
        fillddlSalaryTemplate();
        FillddlDepartments();
        FillddlDesignations();
        FillddlLocation();
        FillddlUnit(0);
        FillddlContractType();
        FillddlException();
        FillddlShift();
        fillddlDistributor(Constants.IntNullValue, Constants.IntNullValue);
        txtBankAccountNumber.Text = "";
        txtBankAccountTitle.Text = "";
        txtBankName.Text = "";
        txtBloodGroup.Text = "";
        txtDateOfBirth.Text = "";
        txtDrivingLicenseNumber.Text = "";
        txtDrivingLicenseValidUpto.Text = "";
        txtEmergencyContactName.Text = "";
        txtEmergencyContactNumber.Text = "";
        txtEmployeeCNIC.Text = "";
        txtEmployeeDurationFrom.Text = "";
        txtEmployeeDurationTo.Text = "";
        txtEmployeeName.Text = "";
        txtEmployeeNTN.Text = "";
        txtEmployeeTag.Text = "";
        txtEOBInumber.Text = "";
        txtHomeAddress.Text = "";
        txtNationality.Text = "";
        txtPassportNumber.Text = "";
        txtProbationFrom.Text = "";
        txtProbationTo.Text = "";
        txtSocialSecurityNumber.Text = "";
        txtVisaInformation.Text = "";
        txtWorkAddress.Text = "";
        txtWorkEmail.Text = "";
        txtWorkMobileNo.Text = "";
        txtWorkPhone.Text = "";
        ddlContractType.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlDesignation.SelectedIndex = 0;
        ddlEmployeeGender.SelectedIndex = 0;
        ddlEmployeeRole.SelectedIndex = 0;
        ddlException.SelectedIndex = 0;
        ddlisActive.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        ddlMaritalStatus.SelectedIndex = 0;
        ddlOvertime.SelectedIndex = 0;
        ddlSalaryBehaviour.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        ddlDistributor.SelectedIndex = 0;
        txtDateOfBirth.Attributes.Add("readonly", "readonly");
        txtEmployeeDurationFrom.Attributes.Add("readonly", "readonly");
        txtEmployeeDurationTo.Attributes.Add("readonly", "readonly");
        txtProbationFrom.Attributes.Add("readonly", "readonly");
        txtProbationTo.Attributes.Add("readonly", "readonly");
        txtDrivingLicenseValidUpto.Attributes.Add("readonly", "readonly");
    }
    private void resetAfterUpdate()
    {
        if (Session["EditEmployeeID"] != null)
        {
            Session.Remove("EditEmployeeID");
        }
        if (Session["LogoName"] != null)
        {
            Session.Remove("LogoName");
        }
        hdnEmployeeID.Value = "";
        hdnSalaryStructureID.Value = "";
    }
    protected void fillddlSalaryTemplate()
    {
        DataTable dt = ccc.SelectSalaryStructureMaster(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), 1);
        ddlSalaryTemplate.Items.Clear();
        ListItem li = new ListItem();
        li.Text = "--Select--";
        li.Value = "0";
        li.Selected = true;
        ddlSalaryTemplate.Items.Add(li);
        clsWebFormUtil.FillDropDownList(ddlSalaryTemplate, dt, 0, 1, false);
    }
    protected void btnDiscardSalaryStructure_Click(object sender, EventArgs e)
    {
        if (Session["EditEmployeeID"] != null)
        {
            DataTable dt = ec.SelectEmployee(Int32.Parse(Session["EditEmployeeID"].ToString()), Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, false, Convert.ToInt32(Session["UserID"]));
            ddlSalaryTemplate.SelectedValue = dt.Rows[0]["TemplateID"].ToString();
            DataTable dt1 = ccc.SelectSalaryStructure(Int32.Parse(hdnSalaryStructureID.Value), null, Constants.IntNullValue, 2, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.IntNullValue);
            fillSalaryStructure(dt1);
        }
        else
        {
            ddlSalaryTemplate.SelectedIndex = 0;
            DataTable dt1 = ccc.SelectSalaryStructure(0, null, Constants.IntNullValue, 2, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.IntNullValue);
            fillSalaryStructure(dt1);
        }
    }
    protected void ddlSalaryTemplate_Change(object sender, EventArgs e)
    {
        DataTable dt = ccc.SelectSalaryStructure(Int32.Parse(ddlSalaryTemplate.SelectedValue), null, Constants.IntNullValue, Constants.IntNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.IntNullValue);
        fillSalaryStructure(dt);
        ModalPopupExtender.Show();
    }
    protected void btnSaveSalaryStructure_Click(object sender, EventArgs e)
    {
    }
    protected void btnSalaryStructure_Click(object sender, EventArgs e)
    {
    }
    protected DataTable CreateDeductionTable()
    {
        DataTable dtDeduction = new DataTable();
        dtDeduction.Columns.Add("RatioType", typeof(int));
        dtDeduction.Columns.Add("DeductionRatio", typeof(decimal));
        dtDeduction.Columns.Add("DeductionID", typeof(int));
        dtDeduction.Columns.Add("DeductionDescription", typeof(string));
        dtDeduction.Columns.Add("Comments", typeof(string));
        return dtDeduction;
    }
    protected DataTable CreateAllowanceTable()
    {
        DataTable dtAllowance = new DataTable();
        dtAllowance.Columns.Add("RatioType", typeof(int));
        dtAllowance.Columns.Add("AllowanceRatio", typeof(decimal));
        dtAllowance.Columns.Add("AllowanceID", typeof(int));
        dtAllowance.Columns.Add("AllowanceDescription", typeof(string));
        return dtAllowance;

    }
    protected void fillSalaryStructure(DataTable dt)
    {
        DataTable dtDeduction = CreateDeductionTable();
        DataTable dtAllowances = CreateAllowanceTable();
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["AllowanceID"].ToString() != null && dr["AllowanceID"].ToString() != "")
            {
                DataRow drAllowance = dtAllowances.NewRow();
                drAllowance["RatioType"] = dr["AmountTypeID"];
                drAllowance["AllowanceRatio"] = dr["Amount"];
                drAllowance["AllowanceID"] = dr["AllowanceID"];
                drAllowance["AllowanceDescription"] = dr["AllowanceDescription"];
                dtAllowances.Rows.Add(drAllowance);

            }
            if (dr["DeductionID"].ToString() != null && dr["DeductionID"].ToString() != "")
            {
                DataRow drDeduction = dtDeduction.NewRow();
                drDeduction["RatioType"] = dr["AmountTypeID"];
                drDeduction["DeductionRatio"] = dr["Amount"];
                drDeduction["DeductionID"] = dr["DeductionID"];
                drDeduction["Comments"] = dr["Comment"];
                drDeduction["DeductionDescription"] = dr["DeductionDescription"];
                dtDeduction.Rows.Add(drDeduction);
            }
        }
        rptAllAllowances.DataSource = dtAllowances;
        rptAllAllowances.DataBind();
        rptAllDeduction.DataSource = dtDeduction;
        rptAllDeduction.DataBind();
    }
    protected int InsertUpdateSalaryStructure()
    {
        try
        {
            decimal BasicSalary = 0;
            if (txtBasicSalary.Text != "")
            {
                BasicSalary = Decimal.Parse(txtBasicSalary.Text);
            }
            int SalaryStructureID = Constants.IntNullValue;
            if (hdnSalaryStructureID.Value == "" || hdnSalaryStructureID.Value == null)
            {
                string SalaryStructureName = txtEmployeeName.Text + "/" + ddlDepartment.SelectedItem.Text + "/" + ddlDesignation.SelectedItem.Text;
                SalaryStructureID = ccc.InsertSalaryStructureMaster(SalaryStructureName, Int32.Parse(Session["WorkingCompanyID"].ToString()), 2, BasicSalary, Constants.DecimalNullValue, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                ccc.InsertSalaryStructureArchiveMaster(SalaryStructureID, SalaryStructureName, Int32.Parse(Session["WorkingCompanyID"].ToString()), 2, BasicSalary, Constants.DecimalNullValue, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                InsertSalaryStructureDetail(SalaryStructureID);
            }
            else
            {
                string SalaryStructureName = txtEmployeeName.Text + "/" + ddlDepartment.SelectedItem.Text + "/" + ddlDesignation.SelectedItem.Text;
                ccc.UpdateSalaryStructureMaster(Convert.ToInt32(hdnSalaryStructureID.Value), SalaryStructureName, (Int32.Parse(Session["WorkingCompanyID"].ToString())), 2, BasicSalary, Constants.DecimalNullValue, false, true, Int32.Parse(Session["UserID"].ToString()));
                ccc.InsertSalaryStructureArchiveMaster(Convert.ToInt32(hdnSalaryStructureID.Value), SalaryStructureName, Int32.Parse(Session["WorkingCompanyID"].ToString()), 2, BasicSalary, Constants.DecimalNullValue, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                InsertSalaryStructureDetail(Convert.ToInt32(hdnSalaryStructureID.Value));
            }
            return SalaryStructureID;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void InsertSalaryStructureDetail(int SalaryStructureID)
    {
        try
        {
            ccc.DeleteSalaryStructureDetail(SalaryStructureID);
            foreach (RepeaterItem item in rptAllAllowances.Items)
            {
                TextBox txtAllowanceAmount = (TextBox)item.FindControl("txtAllowanceAmount");
                CheckBox chkAllowancePercentage = (CheckBox)item.FindControl("chkAllowancePercentage");
                HiddenField hdnAllAllownaceID = (HiddenField)item.FindControl("hdnAllAllownaceID");
                decimal AllowanceAmount = Decimal.Parse(txtAllowanceAmount.Text);
                int AllowanceID = Int32.Parse(hdnAllAllownaceID.Value);
                int amountTypeID = 2;
                if (chkAllowancePercentage.Checked == true)
                {
                    amountTypeID = 1;
                }
                int detailID = ccc.InsertSalaryStructureDetail(SalaryStructureID, AllowanceID, Constants.IntNullValue, AllowanceAmount, null, amountTypeID);
                ccc.InsertSalaryStructureArchiveDetail(detailID, SalaryStructureID, AllowanceID, Constants.IntNullValue, AllowanceAmount, null, amountTypeID);
            }
            foreach (RepeaterItem item in rptAllDeduction.Items)
            {
                TextBox txtDeductionAmount = (TextBox)item.FindControl("txtDeductionAmount");
                CheckBox chkDeductionPercentage = (CheckBox)item.FindControl("chkDeductionPercentage");
                HiddenField hdnAllDeductionID = (HiddenField)item.FindControl("hdnAllDeductionID");
                decimal DeductionAmount = Decimal.Parse(txtDeductionAmount.Text);
                int DeductionID = Int32.Parse(hdnAllDeductionID.Value);
                TextBox txtComments = (TextBox)item.FindControl("txtComments");
                string comments = txtComments.Text;
                int amountTypeID = 2;
                if (chkDeductionPercentage.Checked == true)
                {
                    amountTypeID = 1;
                }
                int detailID = ccc.InsertSalaryStructureDetail(SalaryStructureID, Constants.IntNullValue, DeductionID, DeductionAmount, comments, amountTypeID);
                ccc.InsertSalaryStructureArchiveDetail(detailID, SalaryStructureID, Constants.IntNullValue, DeductionID, DeductionAmount, comments, amountTypeID);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        int locationid = 0;
        if (ddlLocation.SelectedIndex != 0)
        {
            locationid = int.Parse(ddlLocation.SelectedValue);
        }
        FillddlUnit(locationid);
    }
    protected void ddlPF_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPF.SelectedValue == "True")
        {
            lblPFund.Visible = true;
            txtPFundOpening.Visible = true;
        }
        else
        {
            lblPFund.Visible = false;
            txtPFundOpening.Visible = false;
        }
    }
    private void fillddlDistributor(int DISTRIBUTOR_ID, int TOWN_ID)
    {
        DataTable dt = DistCtl.GetDistributor2(DISTRIBUTOR_ID, TOWN_ID);
        ddlDistributor.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", false);

    }
    #region Documents

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (txtSubject.Text.Trim().Length > 0)
        {
            if (fuNotification.HasFile)
            {
                string Doc1 = fuNotification.PostedFile.FileName;
                string Doc2 = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Doc1;
                fuNotification.SaveAs(Server.MapPath("~/EmployeeDocs/" + Doc2));

                //if (DController.InsertDocument(Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), Doc1, Doc2, txtSubject.Text, dtLocationIDs))
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('File uploaded successfully');", true);

                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No file selected');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please enter subject');", true);
        }
    }

    #endregion
}