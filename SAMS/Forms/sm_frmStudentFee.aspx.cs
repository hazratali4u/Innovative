using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommon.Classes;

public partial class Forms_frmStudentFee : System.Web.UI.Page
{
    Student_Controller mS_Ctrl = new Student_Controller();
   
    DataTable dtFee = new DataTable();
    DataTable dtAccountFee = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            CreateFee();
            CreateAccountFee();

            if (Session["FEE_ID"] != null)
            {
                hf_FeeID.Value = Convert.ToString(Session["FEE_ID"]);
                LoadProgram();
                LoadSession();

                DataTable dt = mS_Ctrl.UspSelectFEE2(long.Parse(hf_FeeID.Value), Constants.IntNullValue, Constants.IntNullValue, null, 0);
                DrpStudyPrograms.SelectedValue = dt.Rows[0]["PROGRAM_ID"].ToString();
                DrpSession.SelectedValue = dt.Rows[0]["SESSION_ID"].ToString();
                txtPaymentDate.SelectedDate = Convert.ToDateTime(dt.Rows[0]["PAYMENT_DATE"]);
                txtFine.Text = dt.Rows[0]["FINE"].ToString();
                txtFineFrom.Text = dt.Rows[0]["FINE_TILL"].ToString();
                txtFineTill.Text = dt.Rows[0]["FINE_FROM"].ToString();

                LoadGrid();

                foreach (GridViewRow gr in grd_Student.Rows)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["STUDENT_ID"].ToString() == gr.Cells[1].Text)
                        {
                            CheckBox chbx = (CheckBox)gr.FindControl("chb_student");
                            chbx.Checked = true;
                        }
                    }
                }

                DataTable dt2 = mS_Ctrl.UspSelectFEE2(int.Parse(hf_FeeID.Value), Constants.IntNullValue, Constants.IntNullValue, null, 1);

                dtAccountFee = (DataTable)Session["dtAccountFee"];

                dtAccountFee = dt2;

                grd_Accounts.DataSource = dtAccountFee;
                grd_Accounts.DataBind();

                DrpSession.Enabled = false;
                DrpStudyPrograms.Enabled = false;


            }
            else
            {
                LoadProgram();
                LoadSession();
                LoadGrid();
                LoadAccountHead();
            }
        }
    }

    #region Load

    private void LoadProgram()
    {
        DataTable dt = mS_Ctrl.UspSelectProgram(Constants.IntNullValue, null, 0);
        clsWebFormUtil.FillDropDownList(DrpStudyPrograms, dt, 0, 1);
    }
    private void LoadSession()
    {
        DataTable dt = mS_Ctrl.UspSelectSession(null,null);
        clsWebFormUtil.FillDropDownList(DrpSession, dt, 0, 1);
    }

    private void LoadGrid()
    {
        if (DrpStudyPrograms.SelectedValue != null && DrpSession.SelectedValue != null && DrpStudyPrograms.SelectedValue != "" && DrpSession.SelectedValue != "")
        {
            DataTable dt = mS_Ctrl.UspSelectStudent2(Constants.LongNullValue, int.Parse(DrpStudyPrograms.SelectedValue), int.Parse(DrpSession.SelectedValue), null, 2);
            grd_Student.DataSource = dt;
            grd_Student.DataBind();
        }
    }
   
    private void LoadAccountHead()
    {
        if (DrpStudyPrograms.SelectedValue != null && DrpSession.SelectedValue != null && DrpStudyPrograms.SelectedValue != "" && DrpSession.SelectedValue != "")
        {
            DataTable dt2 = mS_Ctrl.UspSelectFEE2(Constants.LongNullValue, int.Parse(DrpStudyPrograms.SelectedValue), int.Parse(DrpSession.SelectedValue), null, 2);

            dtAccountFee = (DataTable)Session["dtAccountFee"];

            dtAccountFee = dt2;

            grd_Accounts.DataSource = dtAccountFee;
            grd_Accounts.DataBind();

            DrpSession.Enabled = false;
            DrpStudyPrograms.Enabled = false;

            //AccountHeadController mAccountController = new AccountHeadController();
            //DataTable dt = mAccountController.GetAssignAccountHead_SM(0, 230);
            //grd_Accounts.DataSource = dt;
            //grd_Accounts.DataBind();
        }

    }
    #endregion

    #region Chk OR Text/Change

    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        if (cbAll.Checked == true)
        {
            foreach (GridViewRow dr in grd_Student.Rows)
            {
                CheckBox chb_student = (CheckBox)dr.FindControl("chb_student");
                chb_student.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in grd_Student.Rows)
            {
                CheckBox chb_student = (CheckBox)dr.FindControl("chb_student");
                chb_student.Checked = false;
            }
        }
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtSearch.Text != "")
        {
            DataTable dt = mS_Ctrl.UspSelectStudent2(Constants.LongNullValue, int.Parse(DrpStudyPrograms.SelectedValue),
                                                     int.Parse(DrpSession.SelectedValue), txtSearch.Text.ToString(), 2);

            grd_Student.DataSource = dt;
            grd_Student.DataBind();
        }
        else
        {
            if (hf_FeeID.Value != "")
            {
                DataTable dt = mS_Ctrl.UspSelectFEE2(long.Parse(hf_FeeID.Value), Constants.IntNullValue, Constants.IntNullValue, null, 0);

                LoadGrid();

                foreach (GridViewRow gr in grd_Student.Rows)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["STUDENT_ID"].ToString() == gr.Cells[1].Text)
                        {
                            CheckBox chbx = (CheckBox)gr.FindControl("chb_student");
                            chbx.Checked = true;
                        }
                    }
                }
            }
            else
            {
                LoadGrid();

            }
        }

    }
    protected void DrpStudyPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void DrpSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }

    #endregion

    #region DataTable Creation

    private void CreateFee()
    {
        dtFee.Columns.Add("STUDENT_ID", typeof(long));
        dtFee.Columns.Add("REG_NO", typeof(string));

        this.Session.Add("dtFee", dtFee);
    }

    private void CreateAccountFee()
    {
        dtAccountFee.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtAccountFee.Columns.Add("FEE_VALUE", typeof(decimal));

        dtAccountFee.Columns.Add("PAYMENT_DATE", typeof(DateTime));
        dtAccountFee.Columns.Add("PROGRAM_ID", typeof(int));
        dtAccountFee.Columns.Add("SESSION_ID", typeof(int));
        dtAccountFee.Columns.Add("FEE_ID", typeof(int));
        dtAccountFee.Columns.Add("STUDENT_ID", typeof(long));
        dtAccountFee.Columns.Add("STUDENT_NAME", typeof(string));
        dtAccountFee.Columns.Add("REG_NO", typeof(string));

        this.Session.Add("dtAccountFee", dtAccountFee);
    }

    #endregion

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        bool flag = false;

        dtFee = (DataTable)Session["dtFee"];
        dtAccountFee = (DataTable)Session["dtAccountFee"];

        foreach (GridViewRow dr in grd_Student.Rows)
        {
            CheckBox chb_student = (CheckBox)dr.FindControl("chb_student");
            if (chb_student.Checked == true)
            {
                DataRow drF = dtFee.NewRow();
                drF["STUDENT_ID"] = dr.Cells[1].Text;
                drF["REG_NO"] = dr.Cells[2].Text;
                dtFee.Rows.Add(drF);
            }
        }
        foreach (GridViewRow dr2 in grd_Accounts.Rows)
        {
            DataRow drFA = dtAccountFee.NewRow();

            drFA["ACCOUNT_HEAD_ID"] = dr2.Cells[0].Text;
            TextBox txtAccountFee = (TextBox)dr2.FindControl("txtAccountFee");

            drFA["FEE_VALUE"] = Convert.ToDecimal(txtAccountFee.Text);

            dtAccountFee.Rows.Add(drFA);
        }
        if (hf_FeeID.Value == "")
        {
            hf_FeeID.Value = Convert.ToString(Constants.LongNullValue);
        }

        flag = mS_Ctrl.InsertFee(int.Parse(DrpStudyPrograms.SelectedValue), int.Parse(DrpSession.SelectedValue), decimal.Parse(txtFine.Text),
             decimal.Parse(txtFineTill.Text), decimal.Parse(txtFineFrom.Text), long.Parse(hf_FeeID.Value),
                   txtPaymentDate.SelectedDate.Value, Convert.ToDateTime(Session["CurrentWorkDate"]), int.Parse(Session["USERID"].ToString()),
                   int.Parse(Session["USERID"].ToString()), dtFee, dtAccountFee);
        if (flag == true)
        {
            Session.Remove("dtFee");
            Session.Remove("dtAccountFee");
            Session.Remove("FEE_ID");
            Response.Redirect("sm_frmStudentFeeView.aspx?LevelType="+ Request.QueryString["LevelType"].ToString()+  "&LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
         //   ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Save successfully');");
            
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Some error occurred');");
            
        }

     
    }

}