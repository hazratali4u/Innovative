using System;
using System.Collections.Generic;
using System.Text;
using SAMSDataAccessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;

namespace SAMSDatabaseLayer.Classes
{
    public class spInsertCOURSE_DETAIL
    {
        #region Private Members
        private string sp_Name = "spInsertCOURSE_DETAIL";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_PROFESSIONAL_ID;
        private decimal m_MARKS;
        private long m_COURSE_MASTER_ID;
        private string m_COURSE_NAME;
        private string m_THEORY;
        private string m_PROFESSIONAL;
        #endregion
        #region Public Properties
        public int PROFESSIONAL_ID
        {
            set
            {
                m_PROFESSIONAL_ID = value;
            }
            get
            {
                return m_PROFESSIONAL_ID;
            }
        }
        public decimal MARKS
        {
            set
            {
                m_MARKS = value;
            }
            get
            {
                return m_MARKS;
            }
        }
        public long COURSE_MASTER_ID
        {
            set
            {
                m_COURSE_MASTER_ID = value;
            }
            get
            {
                return m_COURSE_MASTER_ID;
            }
        }
        public string COURSE_NAME
        {
            set
            {
                m_COURSE_NAME = value;
            }
            get
            {
                return m_COURSE_NAME;
            }
        }
        public string THEORY
        {
            set
            {
                m_THEORY = value;
            }
            get
            {
                return m_THEORY;
            }
        }
        public string PROFESSIONAL
        {
            set
            {
                m_PROFESSIONAL = value;
            }
            get
            {
                return m_PROFESSIONAL;
            }
        }


        public IDbConnection Connection
        {
            set
            {
                m_connection = value;
            }
            get
            {
                return m_connection;
            }
        }
        public IDbTransaction Transaction
        {
            set
            {
                m_transaction = value;
            }
            get
            {
                return m_transaction;
            }
        }
        #endregion
        #region Constructor
        public spInsertCOURSE_DETAIL()
        {
        }
        #endregion
        #region public Methods
        public bool ExecuteQuery()
        {
            try
            {
                IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp_Name;
                cmd.Connection = m_connection;
                if (m_transaction != null)
                {
                    cmd.Transaction = m_transaction;
                }
                GetParameterCollection(ref cmd);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }
        public IDataReader ExecuteReader()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDataReader dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public DataTable ExecuteTable()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
                da.SelectCommand = command;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public string ExecuteScalar()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                object o;
                o = command.ExecuteScalar();


                return o.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public void FirstReader(IDataReader dr)
        {
            if (dr.Read())
            {
                m_PROFESSIONAL_ID = Convert.ToInt32(dr["PROFESSIONAL_ID"]);
                m_MARKS = Convert.ToDecimal(dr["MARKS"]);
                m_COURSE_MASTER_ID = Convert.ToInt64(dr["COURSE_MASTER_ID"]);
                m_COURSE_NAME = Convert.ToString(dr["COURSE_NAME"]);
                m_THEORY = Convert.ToString(dr["THEORY"]);
                m_PROFESSIONAL = Convert.ToString(dr["PROFESSIONAL"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PROFESSIONAL_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PROFESSIONAL_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PROFESSIONAL_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@MARKS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_MARKS == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_MARKS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@COURSE_MASTER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_COURSE_MASTER_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_COURSE_MASTER_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@COURSE_NAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_COURSE_NAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_COURSE_NAME;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@THEORY";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_THEORY == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_THEORY;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PROFESSIONAL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PROFESSIONAL == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PROFESSIONAL;
            }
            pparams.Add(parameter);


        }
        #endregion
    }
}
