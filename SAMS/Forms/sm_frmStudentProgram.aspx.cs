using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;


public partial class Forms_frmStudentProgram : System.Web.UI.Page
{
    Student_Controller mS_Ctrl = new Student_Controller();
    DataTable dtCourses;
    DataTable dtAccountFee = new DataTable();
    static int ProgramId;
    static int SessionId;
    private static int RowId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
                LoadProgram();
                LoadProgram2();
                CreateCourse();
            
            //Session Tab
                LoadSession();
                LoadAssingned();
                LoadUnAssingned();
                LoadYear();

                btnSaveProgram.Attributes.Add("onclick", "return ValidateForm();");
               //Fee Tab
                LoadFeeSession();
                LoadAccountHead();
               
               
            
        }
    }

    #region Program Tab

    private void LoadProgram()
    {
        DataTable dt = mS_Ctrl.UspSelectProgram(Constants.IntNullValue, null, 1);
        GrdProgram.DataSource = dt;
        GrdProgram.DataBind();
       

    } 
    protected void GrdProgram_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ChbIsActive.Enabled = true;

        ProgramId = int.Parse(GrdProgram.Rows[e.NewEditIndex].Cells[0].Text);
        txtProgramCode.Text = GrdProgram.Rows[e.NewEditIndex].Cells[1].Text;
        txtProgramName.Text = GrdProgram.Rows[e.NewEditIndex].Cells[2].Text;
        if (GrdProgram.Rows[e.NewEditIndex].Cells[3].Text == "Active")
        {
            ChbIsActive.Checked = true;
            
        }
        else
        {
            ChbIsActive.Checked = false;
        }

    }
    protected void btnSaveProgram_Click(object sender, EventArgs e)
    {
        bool flag = false;
        if (ProgramId <= 0)
        {
            ProgramId = Constants.IntNullValue;
        }

        flag=mS_Ctrl.InsertStudyProgram(Convert.ToInt32(ProgramId), txtProgramName.Text, txtProgramCode.Text, Convert.ToDateTime(Session["currentworkdate"])
                                   , Convert.ToInt32(Session["userid"]), Convert.ToInt32(Session["userid"]), ChbIsActive.Checked);

        if (flag == true)
        {
            ClearProgram();
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Save Successfully');");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Some error occurred');");
        }
    }
    private void ClearProgram()
    {
        txtProgramName.Text = "";
        txtProgramCode.Text = "";
        ChbIsActive.Enabled = false;
        ChbIsActive.Checked = true;
        ProgramId = 0;
        LoadProgram();
        LoadProgram2();
        ScriptManager.GetCurrent(Page).SetFocus(txtProgramCode);

    }

    #endregion

    #region Course Tab

    #region DataTable Creation

    public void CreateCourse()
    {
        dtCourses = new DataTable();

        dtCourses.Columns.Add("PROFESSIONAL", typeof(string));
        dtCourses.Columns.Add("PROFESSIONAL_ID", typeof(int));
        dtCourses.Columns.Add("THEORY", typeof(string));
        dtCourses.Columns.Add("COURSENAME", typeof(string));
        dtCourses.Columns.Add("MARKS", typeof(decimal));

        this.Session.Add("dtCourses", dtCourses);
    }

    #endregion
  
    #region Load

    private void LoadProgram2()
    {
        DataTable dt = mS_Ctrl.UspSelectProgram(Constants.IntNullValue, null, 0);
        clsWebFormUtil.FillDropDownList(DrpStudyPrograms, dt, 0, 1,true);
    }
    public void LoadGird()
    {
        dtCourses = (DataTable)this.Session["dtCourses"];
        GrdCourse.DataSource = dtCourses;
        GrdCourse.DataBind();
    }
    #endregion

    #region Grid Operations

    protected void GrdCourse_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dtCourses = (DataTable)this.Session["dtCourses"];
        RowId = e.NewEditIndex;
        DrpProfessional.SelectedValue= GrdCourse.Rows[e.NewEditIndex].Cells[5].Text;
        txtTheory.Text = GrdCourse.Rows[e.NewEditIndex].Cells[0].Text;
        txtCourseName.Text = GrdCourse.Rows[e.NewEditIndex].Cells[1].Text;
        txtMarks.Text = GrdCourse.Rows[e.NewEditIndex].Cells[2].Text;
        
        btnAdd.Text = "Update";


    }
    protected void GrdCourse_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtCourses = (DataTable)this.Session["dtCourses"];
        dtCourses.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("dtCourses", dtCourses);

        this.LoadGird();

    }

    #endregion

    #region Click Operation

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        DataTable dtCourses = (DataTable)this.Session["dtCourses"];

        if (btnAdd.Text == "Add")
        {

            DataRow dr = dtCourses.NewRow();

            dr["PROFESSIONAL"] = DrpProfessional.SelectedItem.Text;
            dr["PROFESSIONAL_ID"] = DrpProfessional.SelectedItem.Value;
            dr["THEORY"] = txtTheory.Text;
            dr["COURSENAME"] = txtCourseName.Text;
            dr["MARKS"] = txtMarks.Text;
          

            dtCourses.Rows.Add(dr);
        }

        else
        {
            DataRow dr = dtCourses.Rows[RowId];

            dr["PROFESSIONAL"] = DrpProfessional.SelectedItem.Text;
            dr["PROFESSIONAL_ID"] = DrpProfessional.SelectedItem.Value;
            dr["THEORY"] = txtTheory.Text;
            dr["COURSENAME"] = txtCourseName.Text;
            dr["MARKS"] = txtMarks.Text;
          
        }

        this.Session.Add("dtCourses", dtCourses);

        this.LoadGird();

        ClearCourseDetail();
        ScriptManager.GetCurrent(Page).SetFocus(txtTheory);

    }
    protected void btnSaveCourse_Click(object sender, EventArgs e)
    {
         bool flag=false;
        DataTable dtCourses = (DataTable)Session["dtCourses"];

        if (dtCourses.Rows.Count > 0)
        {
            flag = mS_Ctrl.InsertCourses(Convert.ToInt32(DrpStudyPrograms.SelectedValue), Constants.LongNullValue, Convert.ToDateTime(Session["currentworkdate"])
                                      , Convert.ToInt32(Session["userid"]), Convert.ToInt32(Session["userid"]), dtCourses);

            ClearCourse();
            ClearCourseDetail();

            if (flag == true)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Save successfully');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Some error occurred');", true);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Plz enter atleast one Course');", true);
        }
    }
    #endregion

    private void ClearCourse()
    {
        DrpStudyPrograms.SelectedValue = "1";
        DrpProfessional.SelectedValue = "0";

        Session.Remove("dtCourses");

        GrdCourse.DataSource = null;
        GrdCourse.DataBind();
    }
    private void ClearCourseDetail()
    {
        txtCourseName.Text = "";
        txtMarks.Text = "";
        txtTheory.Text = "";
        btnAdd.Text = "Add";
    }
    #endregion

    #region Session Tab


    private void LoadSession()
    {
        DataTable dt = mS_Ctrl.UspSelectSession(null, null);

        GrdSession.DataSource = dt;
        GrdSession.DataBind();

        clsWebFormUtil.FillDropDownList(DrpSession, dt, 0, 1, true);
    }
    private void LoadUnAssingned()
    {
        if (DrpSession.Items.Count > 0)
        {

            DataTable dt = mS_Ctrl.SelectProgramAssignment(int.Parse(DrpSession.SelectedValue.ToString()), 0, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(lstUnAssignProgram, dt, 0, 1, true);
        }
    }
    private void LoadAssingned()
    {
        if (DrpSession.Items.Count > 0)
        {
            DataTable dt = mS_Ctrl.SelectProgramAssignment(int.Parse(DrpSession.SelectedValue.ToString()), 1, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(lstAssignProgram, dt, 0, 1, true);
        }
    }
    private void LoadYear()
    {
        int i, y = DateTime.Now.Year;

        for (i = 1990; i <= DateTime.Now.Year + 30; i++)
            DrpSessionFrom.Items.Add(Convert.ToString(i));

        for (y = 1990; y <= DateTime.Now.Year + 30; y++)
            DrpSessionTo.Items.Add(Convert.ToString(y));
    }

    protected void GrdSession_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string[] session;

        SessionId = int.Parse(GrdSession.Rows[e.NewEditIndex].Cells[0].Text);
        session = GrdSession.Rows[e.NewEditIndex].Cells[1].Text.Split('-');

        DrpSessionFrom.SelectedValue = session[0].ToString().Trim();
        DrpSessionTo.SelectedValue = session[1].ToString().Trim();
        
    }
    protected void DrpSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadAssingned();
        LoadUnAssingned();
    }

    protected void btnSaveSession_Click(object sender, EventArgs e)
    {
       
        bool flag = false;
        if (SessionId <= 0)
        {
            SessionId = Constants.IntNullValue;
        }

        flag = mS_Ctrl.InsertStudySession(Convert.ToInt32(SessionId), DrpSessionFrom.SelectedItem.Text, DrpSessionTo.SelectedItem.Text, Convert.ToDateTime(Session["currentworkdate"])
                                   , Convert.ToInt32(Session["userid"]), Convert.ToInt32(Session["userid"]), true);

        if (flag)
        {
            ClearSession();
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Save Successfully');",true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Some error occurred');", true);
        }
    }

    protected void btnAssignProgram_Click(object sender, EventArgs e)
    {
       
        for (int i = 0; i < lstUnAssignProgram.Items.Count; i++)
        {
            if (lstUnAssignProgram.Items[i].Selected == true && DrpSession.Items.Count > 0)
            {
                mS_Ctrl.InsertProgramAssignment(int.Parse(DrpSession.SelectedValue.ToString()),int.Parse(lstUnAssignProgram.Items[i].Value.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            }
        }
        this.LoadUnAssingned();
        this.LoadAssingned();
    }
    protected void btnUnAssignProgram_Click(object sender, EventArgs e)
    {
       
        for (int i = 0; i < lstAssignProgram.Items.Count; i++)
        {
            if (lstAssignProgram.Items[i].Selected == true && DrpSession.Items.Count > 0)
            {
                mS_Ctrl.DeleteProgramAssignment(int.Parse(DrpSession.SelectedValue.ToString()), int.Parse(lstAssignProgram.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            }
        }
        this.LoadUnAssingned();
        this.LoadAssingned();
    }

    private void ClearSession()
    {
        //  txtSessionFrom.Text = "";
        //   txtSessionTo.Text = "";
        SessionId = 0;
        LoadSession();
        DrpSessionFrom.SelectedIndex = 0;
        DrpSessionTo.SelectedIndex = 0;

        ScriptManager.GetCurrent(Page).SetFocus(DrpSessionFrom);

    }

    #endregion

    #region Fee

    private void CreateAccountFee()
    {
        dtAccountFee.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtAccountFee.Columns.Add("FEE_VALUE", typeof(decimal));
        dtAccountFee.Columns.Add("PROGRAM_ID", typeof(int));
        dtAccountFee.Columns.Add("SESSION_ID", typeof(int));
        dtAccountFee.Columns.Add("FEE_ID", typeof(int));
        
        this.Session.Add("dtAccountFee", dtAccountFee);
    }

    private void LoadFeeSession()
    {
        DataTable dt = mS_Ctrl.UspSelectSession(null, null);
        clsWebFormUtil.FillDropDownList(DrpFeeSession, dt, 0, 1, true);
       
        LoadFeeProgram();
       
    }
    private void LoadFeeProgram()
    {
        if (DrpFeeSession.Items.Count > 0)
        {
            DataTable f_dt = mS_Ctrl.SelectProgramAssignment(int.Parse(DrpFeeSession.SelectedValue.ToString()), 1, int.Parse(this.Session["CompanyId"].ToString()));
            
                clsWebFormUtil.FillDropDownList(DrpFeeProgram, f_dt, 0, 1, true);
        }
    }
    private void LoadAccountHead()
    {

        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.GetAssignAccountHead_SM(0, 230);
        grd_Accounts.DataSource = dt;
        grd_Accounts.DataBind();

    }
    protected void DrpFeeSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadFeeProgram();
    }

    protected void btnSaveFee_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow dr2 in grd_Accounts.Rows)
        {
            DataRow drFA = dtAccountFee.NewRow();

            drFA["ACCOUNT_HEAD_ID"] = dr2.Cells[0].Text;
            TextBox txtAccountFee = (TextBox)dr2.FindControl("txtAccountFee");

            drFA["FEE_VALUE"] = Convert.ToDecimal(txtAccountFee.Text);

            dtAccountFee.Rows.Add(drFA);
        }

        bool flag = false;

    
        flag = mS_Ctrl.InsertFeeStructure(int.Parse(DrpFeeProgram.SelectedValue), int.Parse(DrpFeeSession.SelectedValue),Constants.LongNullValue,
            Convert.ToDateTime(Session["CurrentWorkDate"]), int.Parse(Session["USERID"].ToString()),
                   int.Parse(Session["USERID"].ToString()), dtAccountFee,true);
        if (flag == true)
        {
           
            Session.Remove("dtAccountFee");
           
             ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Save successfully');",true);

        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Some error occurred');",true);

        }
    }

    #endregion


}