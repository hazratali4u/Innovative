using System;
using System.Collections.Generic;
using System.Text;
using SAMSDataAccessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;

namespace SAMSDatabaseLayer.Classes
{
    public class spInsertQUALIFICATION
    {
        #region Private Members
        private string sp_Name = "spInsertQUALIFICATION";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private decimal m_CGPA;
        private long m_QUALIFICATION_ID;
        private long m_STUDENT_ID;
        private string m_CERTIFICATE;
        private string m_SESSION;
        private string m_MARKS;
        private string m_BOARD;
        #endregion
        #region Public Properties
        public decimal CGPA
        {
            set
            {
                m_CGPA = value;
            }
            get
            {
                return m_CGPA;
            }
        }
        public long QUALIFICATION_ID
        {
            set
            {
                m_QUALIFICATION_ID = value;
            }
            get
            {
                return m_QUALIFICATION_ID;
            }
        }
        public long STUDENT_ID
        {
            set
            {
                m_STUDENT_ID = value;
            }
            get
            {
                return m_STUDENT_ID;
            }
        }
        public string CERTIFICATE
        {
            set
            {
                m_CERTIFICATE = value;
            }
            get
            {
                return m_CERTIFICATE;
            }
        }
        public string SESSION
        {
            set
            {
                m_SESSION = value;
            }
            get
            {
                return m_SESSION;
            }
        }
        public string MARKS
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
        public string BOARD
        {
            set
            {
                m_BOARD = value;
            }
            get
            {
                return m_BOARD;
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
        public spInsertQUALIFICATION()
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
                m_CGPA = Convert.ToDecimal(dr["CGPA"]);
                m_QUALIFICATION_ID = Convert.ToInt64(dr["QUALIFICATION_ID"]);
                m_STUDENT_ID = Convert.ToInt64(dr["STUDENT_ID"]);
                m_CERTIFICATE = Convert.ToString(dr["CERTIFICATE"]);
                m_SESSION = Convert.ToString(dr["SESSION"]);
                m_MARKS = Convert.ToString(dr["MARKS"]);
                m_BOARD = Convert.ToString(dr["BOARD"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CGPA";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
            if (m_CGPA == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CGPA;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@QUALIFICATION_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_QUALIFICATION_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_QUALIFICATION_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@STUDENT_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_STUDENT_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_STUDENT_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CERTIFICATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_CERTIFICATE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CERTIFICATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SESSION";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_SESSION == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SESSION;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@MARKS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_MARKS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_MARKS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BOARD";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_BOARD == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_BOARD;
            }
            pparams.Add(parameter);


        }
        #endregion
    }
}
