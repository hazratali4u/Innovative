using System;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmAdmision : Page
{
    static string _dTime;
    private static int _rowId;
    string _rgno;
    int _rgno2;
    string _year;
    string[] _length;


    readonly Student_Controller _mSCtrl = new Student_Controller();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           
            LoadDomicile();
            LoadProgram();
            LoadSession();
            LoadMaxId();
            CreateQualification();
            CreateSubling();

           Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            _dTime = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
       
        }
        
    }
 

    #region DataTable Creation

    public void CreateQualification()
    {
        DataTable dtQualfication = new DataTable();
       
        dtQualfication.Columns.Add("CERTIFICATE", typeof(string));
        dtQualfication.Columns.Add("SESSION", typeof(string));
        dtQualfication.Columns.Add("MARKS", typeof(string));
        dtQualfication.Columns.Add("CGPA", typeof(decimal));
        dtQualfication.Columns.Add("BOARD", typeof(string));

        this.Session.Add("dtQualfication", dtQualfication);
    }
    public void CreateSubling()
    {
        DataTable dtSubling = new DataTable();

        dtSubling.Columns.Add("NAME", typeof(string));
        dtSubling.Columns.Add("ROLLNO", typeof(string));
        dtSubling.Columns.Add("PROGRAM", typeof(string));
        dtSubling.Columns.Add("YEAR", typeof(string));

        this.Session.Add("dtSubling", dtSubling);
    }

    #endregion

    #region Load

    private void LoadMaxId()
    {
         _year =Convert.ToString(System.DateTime.Now.Year);
         _rgno = _mSCtrl.spSelectMAXStudent();
         if (DrpStudyPrograms.SelectedItem != null)
         {
             _length = DrpStudyPrograms.SelectedItem.Text.Split('-');


             if (_rgno == "")
             {
                 lblRegNo.Text = _year + '-' + _length[0] + '-' + "1";
             }
             else
             {
                 _rgno2 = Convert.ToInt32(_rgno) + 1;

                 lblRegNo.Text = Convert.ToString(_year + '-' + _length[0] + '-' + _rgno2);
             }
         }
    }
    public void LoadGird()
    {
        DataTable dtQualfication = (DataTable)this.Session["dtQualfication"];
        GrdPurchase.DataSource = dtQualfication;
        GrdPurchase.DataBind();
    }
    public void LoadGird2()
    {
        DataTable dtSubling = (DataTable)this.Session["dtSubling"];
        grdSubling.DataSource = dtSubling;
        grdSubling.DataBind();
    }
   
    private void LoadDomicile()
    {
        var dt = _mSCtrl.UspSelectDomicile(Constants.IntNullValue, null);
        if (dt.Rows.Count <= 0) return;
        clsWebFormUtil.FillDropDownList(DrpDomicile, dt, 0, 1,true);
    }

  
    private void LoadProgram()
    {
        DataTable dt = _mSCtrl.UspSelectProgram(Constants.IntNullValue, null,0);
        clsWebFormUtil.FillDropDownList(DrpStudyPrograms, dt, 0, 1,true);
    }
    private void LoadSession()
    {
        DataTable dt = _mSCtrl.UspSelectSession(null,null);
        clsWebFormUtil.FillDropDownList(DrpSession, dt, 0, 1,true);
    }

    private void LoadStudents()
    {
        DataTable dt = _mSCtrl.UspSelectStudent(ddSearchType.SelectedValue.ToString(), txtSeach.Text.ToString());
        this.Grid_users.DataSource = dt;
        this.Grid_users.DataBind();

    }

    #endregion

    #region Grid Operations

    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dtQualfication = (DataTable)this.Session["dtQualfication"];
       _rowId = e.NewEditIndex;
        txtCertificate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtSession.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
        txtMarks.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        txtCGPA.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtBoard.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
       
        btnAdd.Text = "Update";

      
    }
    protected void grdSubling_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dtSubling = (DataTable)this.Session["dtSubling"];
        _rowId = e.NewEditIndex;
        txtSubling_Name.Text = grdSubling.Rows[e.NewEditIndex].Cells[0].Text;
        txtSubling_RollNo.Text = grdSubling.Rows[e.NewEditIndex].Cells[1].Text;
        txtSubling_Program.Text = grdSubling.Rows[e.NewEditIndex].Cells[2].Text;
        txtSubling_Year.Text = grdSubling.Rows[e.NewEditIndex].Cells[3].Text;

        btn_Add2.Text = "Update";
    }
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtQualfication = (DataTable)this.Session["dtQualfication"];
        dtQualfication.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("dtQualification", dtQualfication);

        this.LoadGird();


    }
    protected void grdSubling_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtSubling = (DataTable)this.Session["dtSubling"];
        dtSubling.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("dtSubling", dtSubling);
        
        this.LoadGird2();

  
    }

    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hf_CustomerID.Value= Grid_users.Rows[e.NewEditIndex].Cells[0].Text;

        DrpStudyPrograms.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[1].Text;
        DrpDomicile.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[2].Text;
        DrpBloodGroup.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[3].Text;
        lblRegNo.Text = Grid_users.Rows[e.NewEditIndex].Cells[4].Text;
       
        txtStudentName.Text = Grid_users.Rows[e.NewEditIndex].Cells[5].Text;


        txtMobileNo.Text = Grid_users.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;", "");
        txtEmail.Text = Grid_users.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", "");
        txtCNIC.Text = Grid_users.Rows[e.NewEditIndex].Cells[9].Text.Replace("&nbsp;", "");
        txtFatherName.Text = Grid_users.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
        rdbGender.SelectedValue= Grid_users.Rows[e.NewEditIndex].Cells[12].Text;

        if (Convert.ToString(Grid_users.Rows[e.NewEditIndex].Cells[13].Text) == "True")
        {
            rdbM_Status.SelectedValue = "1";
        }
        else
        {
            rdbM_Status.SelectedValue = "0";
        }
      

        txtNationality.Text = Grid_users.Rows[e.NewEditIndex].Cells[14].Text.Replace("&nbsp;", "");
        txtReligion.Text = Grid_users.Rows[e.NewEditIndex].Cells[15].Text.Replace("&nbsp;", "");
        txtPermanent_Address.Text = Grid_users.Rows[e.NewEditIndex].Cells[16].Text.Replace("&nbsp;", "");
        txtPostal_Address.Text = Grid_users.Rows[e.NewEditIndex].Cells[17].Text.Replace("&nbsp;", "");
        txtBirthDate.SelectedDate = Convert.ToDateTime(Grid_users.Rows[e.NewEditIndex].Cells[18].Text);
        txtF_Email.Text = Grid_users.Rows[e.NewEditIndex].Cells[19].Text.Replace("&nbsp;", "");
        txtF_CNIC.Text = Grid_users.Rows[e.NewEditIndex].Cells[20].Text.Replace("&nbsp;", "");
        txtF_MobileNo.Text = Grid_users.Rows[e.NewEditIndex].Cells[21].Text.Replace("&nbsp;", "");
        txtFO_Address.Text = Grid_users.Rows[e.NewEditIndex].Cells[22].Text.Replace("&nbsp;", "");
        txtFO_No.Text = Grid_users.Rows[e.NewEditIndex].Cells[23].Text.Replace("&nbsp;", "");
        txtOccupation.Text = Grid_users.Rows[e.NewEditIndex].Cells[24].Text.Replace("&nbsp;", "");
        txtResidence.Text = Grid_users.Rows[e.NewEditIndex].Cells[25].Text.Replace("&nbsp;", "");
        hf_Image.Value= Grid_users.Rows[e.NewEditIndex].Cells[26].Text.Replace("&nbsp;", "");
        DrpSession.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[27].Text;

        if (hf_Image.Value != "")
        {
            Session.Add("Image", hf_Image.Value); 
        }
        DataTable dt = _mSCtrl.UspSelectStudent2(long.Parse(hf_CustomerID.Value), Constants.IntNullValue, Constants.IntNullValue, null,0);
        DataTable dt2 = _mSCtrl.UspSelectStudent2(long.Parse(hf_CustomerID.Value), Constants.IntNullValue, Constants.IntNullValue, null, 1);

        Session.Remove("dtQualfication");
        Session.Remove("dtSubling");

        CreateQualification();
        CreateSubling();

        DataTable dtQualfication = (DataTable)this.Session["dtQualfication"];
        DataTable dtSubling = (DataTable)this.Session["dtSubling"];

        foreach (DataRow drf in dt.Rows)
        {
                  DataRow DR2 = dtQualfication.NewRow();

                    DR2["CERTIFICATE"] = drf["CERTIFICATE"].ToString();
                    DR2["SESSION"] = drf["SESSION"].ToString();
                     DR2["MARKS"] = drf["MARKS"].ToString();
                    DR2["CGPA"] = drf["CGPA"].ToString(); 
                    DR2["BOARD"] = drf["BOARD"].ToString();

                    dtQualfication.Rows.Add(DR2);
        }
        GrdPurchase.DataSource = dtQualfication;
        GrdPurchase.DataBind();

        foreach (DataRow drf2 in dt2.Rows)
        {
            if (drf2["NAME"].ToString() != "")
            {
                DataRow DR2 = dtSubling.NewRow();

                DR2["NAME"] = drf2["NAME"].ToString();
                DR2["ROLLNO"] = drf2["ROLLNO"].ToString();
                DR2["PROGRAM"] = drf2["PROGRAM"].ToString();
                DR2["YEAR"] = drf2["YEAR"].ToString();

                dtSubling.Rows.Add(DR2);
            }
        }

         grdSubling.DataSource = dtSubling;
         grdSubling.DataBind();

        this.SetTableSorter();
    }
    #endregion

    #region Click Operations

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        DataTable dtQualfication = (DataTable)this.Session["dtQualfication"];

        if (btnAdd.Text == "Add")
        {

            DataRow dr = dtQualfication.NewRow();
            dr["CERTIFICATE"] = txtCertificate.Text;
            dr["SESSION"] = txtSession.Text;
            dr["MARKS"] = txtMarks.Text;
            dr["CGPA"] = txtCGPA.Text;
            dr["BOARD"] = txtBoard.Text;

            dtQualfication.Rows.Add(dr);
        }

        else
        {
            DataRow dr = dtQualfication.Rows[_rowId];

            dr["CERTIFICATE"] = txtCertificate.Text;
            dr["SESSION"] = txtSession.Text;
            dr["MARKS"] = txtMarks.Text;
            dr["CGPA"] = txtCGPA.Text;
            dr["BOARD"] = txtBoard.Text;
        }

        this.Session.Add("dtQualfication", dtQualfication);

        this.LoadGird();

        this.ClearAll();
        ScriptManager.GetCurrent(Page).SetFocus(txtCertificate);

    }
    protected void btn_Add2_Click(object sender, EventArgs e)
    {

        DataTable dtSubling = (DataTable)this.Session["dtSubling"];


        if (btn_Add2.Text == "Add")
        {

            DataRow dr = dtSubling.NewRow();

            dr["NAME"] = txtSubling_Name.Text;
            dr["ROLLNO"] = txtSubling_RollNo.Text;
            dr["PROGRAM"] = txtSubling_Program.Text;
            dr["YEAR"] = txtSubling_Year.Text;

            dtSubling.Rows.Add(dr);
        }

        else
        {
            DataRow dr = dtSubling.Rows[_rowId];

            dr["NAME"] = txtSubling_Name.Text;
            dr["ROLLNO"] = txtSubling_RollNo.Text;
            dr["PROGRAM"] = txtSubling_Program.Text;
            dr["YEAR"] = txtSubling_Year.Text;

        }

        this.Session.Add("dtSubling", dtSubling);

        this.LoadGird2();
        this.ClearAll2();

        ScriptManager.GetCurrent(Page).SetFocus(txtSubling_Name);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        DataTable dtQualfication = (DataTable)Session["dtQualfication"];
        DataTable dtSubling = (DataTable)Session["dtSubling"];
        string fileName="";

        if (dtQualfication.Rows.Count > 0)
        {
            if (fuStudent.HasFile)
            {
                fileName = lblRegNo.Text + " " + fuStudent.PostedFile.FileName;

                fuStudent.SaveAs(Server.MapPath("~/StudentImages/" + fileName));

            }
            else
            {
                if (Session["Image"] != null)
                {
                    fileName = lblRegNo.Text + " " + fuStudent.PostedFile.FileName;

                    fuStudent.SaveAs(Server.MapPath("~/StudentImages/" + fileName));
                }
            }
            if (hf_CustomerID.Value == "")
            {
                hf_CustomerID.Value = Convert.ToString(Constants.LongNullValue);
            }

            bool flag = _mSCtrl.InsertAdmission(long.Parse(hf_CustomerID.Value), int.Parse(DrpStudyPrograms.SelectedValue), int.Parse(DrpSession.SelectedValue), lblRegNo.Text, txtStudentName.Text, Convert.ToSByte(rdbGender.SelectedValue), txtReligion.Text, txtNationality.Text,
              int.Parse(DrpDomicile.SelectedValue), txtBirthDate.SelectedDate.Value, Convert.ToBoolean(rdbM_Status.SelectedValue), txtMobileNo.Text.ToString(),
              int.Parse(DrpBloodGroup.SelectedValue), txtCNIC.Text.ToString(), txtEmail.Text.ToString(), txtPermanent_Address.Text, txtPostal_Address.Text,
              txtFatherName.Text, txtOccupation.Text, txtF_Email.Text, txtFO_Address.Text, txtF_CNIC.Text, txtF_MobileNo.Text, txtFO_No.Text, txtResidence.Text,
              Convert.ToDateTime(_dTime), int.Parse(Session["USERID"].ToString()), Convert.ToString(fileName), dtQualfication, dtSubling);
            
            if (flag == true)
            {
                ClearMaster();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Some error occurred');");
            }
        }
        else
        {

        }
       
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
        this.LoadStudents();
        this.SetTableSorter();
    }
    
    #endregion

    #region Clear

    private void ClearMaster()
    {
        Session.Remove("dtQualfication");
        Session.Remove("dtSubling");
        Session.Remove("Image");
      
    
        LoadProgram();
        LoadSession();
        LoadDomicile();
        DrpBloodGroup.SelectedValue = "0";
        rdbGender.SelectedValue = "0";
        rdbM_Status.SelectedValue = "false";

        lblRegNo.Text = "";
        txtStudentName.Text = "";
        txtMobileNo.Text = "";
        txtEmail.Text = "";
        txtCNIC.Text = "";
        txtFatherName.Text = "";
        txtNationality.Text = "";
        txtReligion.Text = "";
        txtPermanent_Address.Text = "";
        txtPostal_Address.Text = "";
        txtBirthDate.Clear();
        txtF_Email.Text = "";
        txtF_CNIC.Text = "";
        txtF_MobileNo.Text = "";
        txtFO_Address.Text = "";
        txtFO_No.Text = "";
        txtOccupation.Text = "";
        txtResidence.Text = "";
        txtStudentName.Focus();

        grdSubling.DataSource = null;
        grdSubling.DataBind();
        
        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();

        LoadMaxId();

        CreateQualification();
        CreateSubling();

        ClearAll();
        ClearAll2();

        if (hf_CustomerID.Value != "")
        {
            Grid_users.DataSource = null;
            Grid_users.DataBind();
            hf_CustomerID.Value = "";
        }
      
    }
    private void ClearAll()
    {
        txtCertificate.Text = "";
        txtSession.Text = "";
        txtMarks.Text = "";
        txtCGPA.Text = "0";
        txtBoard.Text = "";
        btnAdd.Text = "Add";
    }
    private void ClearAll2()
    {
        txtSubling_Name.Text = "";
        txtSubling_RollNo.Text = "";
        txtSubling_Program.Text = "";
        txtSubling_Year.Text = "";
        btn_Add2.Text = "Add";
    }

    #endregion
   
    private void SetTableSorter()
    {
        if (Grid_users.Rows.Count > 1)
        {
            Grid_users.UseAccessibleHeader = true;
            Grid_users.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Session.Remove("dtQualfication");
        Session.Remove("dtSubling");
        Session.Remove("Image");

        hf_CustomerID.Value = "";
        LoadProgram();
        LoadDomicile();
        DrpBloodGroup.SelectedValue = "0";
        rdbGender.SelectedValue = "0";
        rdbM_Status.SelectedValue = "true";

        lblRegNo.Text = "";
        txtStudentName.Text = "";
        txtMobileNo.Text = "";
        txtEmail.Text = "";
        txtCNIC.Text = "";
        txtFatherName.Text = "";
        txtNationality.Text = "";
        txtReligion.Text = "";
        txtPermanent_Address.Text = "";
        txtPostal_Address.Text = "";
        txtBirthDate.Clear();
        txtF_Email.Text = "";
        txtF_CNIC.Text = "";
        txtF_MobileNo.Text = "";
        txtFO_Address.Text = "";
        txtFO_No.Text = "";
        txtOccupation.Text = "";
        txtResidence.Text = "";
        txtStudentName.Focus();

        grdSubling.DataSource = null;
        grdSubling.DataBind();

        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();

        CreateQualification();
        CreateSubling();
    }
    protected void DrpStudyPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {
        _year = Convert.ToString(System.DateTime.Now.Year);
        _rgno = _mSCtrl.spSelectMAXStudent();
        _length = DrpStudyPrograms.SelectedItem.Text.Split('-');

        if (_rgno == "")
        {
            lblRegNo.Text = _year + '-' + _length[0] + '-' + "1";
        }
        else
        {
            _rgno2 = Convert.ToInt32(_rgno)+1;


            lblRegNo.Text = Convert.ToString(_year + '-' + _length[0] + '-' + _rgno2);
        }
    }

  
}