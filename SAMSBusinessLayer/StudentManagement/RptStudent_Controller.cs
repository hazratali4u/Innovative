using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    public class RptStudent_Controller
    {
        IDbTransaction mTransaction;
        IDbConnection mConnection;
       
        public DataSet rptSelectFEE(string p_STUDENT_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                rptSelectFee mProgram = new rptSelectFee();
                SAMSBusinessLayer.Reports.DsReport2 ds = new SAMSBusinessLayer.Reports.DsReport2();
                
                mProgram.Connection = mConnection;

                mProgram.STUDENT_ID = p_STUDENT_ID;

                DataTable dt = mProgram.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptSelectFee"].ImportRow(dr);
                }
                return ds;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
    }
}
