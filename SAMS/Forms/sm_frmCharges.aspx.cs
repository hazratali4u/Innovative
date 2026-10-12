using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
public partial class Forms_sm_frmCharges : Page
{
    private int _rowId = Constants.IntNullValue;
    readonly Student_Controller _mSCtrl = new Student_Controller();

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadProgram();
            LoadSession();
            LoadStudents();
            LoadGrid();
        }
    }


    protected void grdStudentCharge_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


    }
    protected void grdStudentCharge_RowEditing(object sender, GridViewEditEventArgs e)
    {


        _rowId = Convert.ToInt32(grdStudentCharge.Rows[e.NewEditIndex].Cells[0].Text);

       
        DrpStudyPrograms.SelectedValue = grdStudentCharge.Rows[e.NewEditIndex].Cells[3].Text;
        DrpSession.SelectedValue = grdStudentCharge.Rows[e.NewEditIndex].Cells[2].Text;
        LoadStudents();

        DrpStudent.SelectedValue = grdStudentCharge.Rows[e.NewEditIndex].Cells[1].Text;
        txtAmount.Text = grdStudentCharge.Rows[e.NewEditIndex].Cells[7].Text;

        btnSaveCharges.Text = "Update";
    }

    protected void btnSaveCharges_Click(object sender, EventArgs e)
    {
       
        if(_mSCtrl.InsertStudentCharges(_rowId,int.Parse(rbChargestype.SelectedValue),int.Parse(DrpStudyPrograms.SelectedValue), int.Parse(DrpSession.SelectedValue), decimal.Parse(txtAmount.Text)
            , Convert.ToDateTime(Session["CurrentWorkDate"]), int.Parse(Session["USERID"].ToString()),
                   int.Parse(Session["USERID"].ToString())))
        {
          Clear();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "msg", "alert('Some error occurred');");

        }
      
    }

    private void Clear()
    {
        txtAmount.Text = "";
        LoadProgram();
        LoadSession();
        LoadStudents();
        LoadGrid();
        btnSaveCharges.Text = "Save";
    }

    private void LoadProgram()
    {
        DataTable dt = _mSCtrl.UspSelectProgram(Constants.IntNullValue, null, 0);
        clsWebFormUtil.FillDropDownList(DrpStudyPrograms, dt, 0, 1);
    }
    private void LoadSession()
    {
        DataTable dt = _mSCtrl.UspSelectSession(null, null);
        clsWebFormUtil.FillDropDownList(DrpSession, dt, 0, 1);
    }

    private void LoadGrid()
    {

        var dt = _mSCtrl.UspSelectStudentCharges(int.Parse(rbChargestype.SelectedValue));
        if (dt.Rows.Count <= 0) return;
        grdStudentCharge.DataSource = dt;
        grdStudentCharge.DataBind();

    }

    private void LoadStudents()
    {
        var dt = _mSCtrl.UspSelectStudent2(Constants.LongNullValue, int.Parse(DrpStudyPrograms.SelectedValue), int.Parse(DrpSession.SelectedValue), null, 2);
       
        clsWebFormUtil.FillDropDownList(DrpStudent,dt,0,1,true);
    }

    protected void DrpStudyPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudents();
    }
    protected void DrpSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudents();
    }

    protected void rbChargestype_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        LoadGrid();
    }
}