using System;
using System.Data;
using SAMSDataAccessLayer.Classes;
using SAMSCommon.Classes;

namespace SAMSDatabaseLayer.Classes
{
    public class spInsertDOMICILE
    {
        #region Private Members

        private const string SpName = "spInsertDOMICILE";
        private IDbConnection _mConnection;
        private IDbTransaction _mTransaction;
        private int _mCityId;
        private int _mCityId2;
        private bool _mIsActive;
        private string _mCityName;
        #endregion
        #region Public Properties
        public int CityId
        {
            set
            {
                _mCityId = value;
            }
            get
            {
                return _mCityId;
            }
        }
        public int CityId2
        {
            get
            {
                return _mCityId2;
            }
        }
        public bool IsActive
        {
            set
            {
                _mIsActive = value;
            }
            get
            {
                return _mIsActive;
            }
        }
        public string CityName
        {
            set
            {
                _mCityName = value;
            }
            get
            {
                return _mCityName;
            }
        }


        public IDbConnection Connection
        {
            set
            {
                _mConnection = value;
            }
            get
            {
                return _mConnection;
            }
        }
        public IDbTransaction Transaction
        {
            set
            {
                _mTransaction = value;
            }
            get
            {
                return _mTransaction;
            }
        }
        #endregion
        #region Constructor
        public spInsertDOMICILE()
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
                cmd.CommandText = SpName;
                cmd.Connection = _mConnection;
                if (_mTransaction != null)
                {
                    cmd.Transaction = _mTransaction;
                }
                GetParameterCollection(ref cmd);
                cmd.ExecuteNonQuery();
                _mCityId2 = (int)((IDataParameter)(cmd.Parameters["@CITY_ID2"])).Value;
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
                command.CommandText = SpName;
                command.Connection = _mConnection;
                if (_mTransaction != null)
                {
                    command.Transaction = _mTransaction;
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
                command.CommandText = SpName;
                command.Connection = _mConnection;
                if (_mTransaction != null)
                {
                    command.Transaction = _mTransaction;
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
                command.CommandText = SpName;
                command.Connection = _mConnection;
                if (_mTransaction != null)
                {
                    command.Transaction = _mTransaction;
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
                _mCityId = Convert.ToInt32(dr["CITY_ID"]);
                _mCityId2 = Convert.ToInt32(dr["CITY_ID2"]);
                _mIsActive = Convert.ToBoolean(dr["IS_ACTIVE"]);
                _mCityName = Convert.ToString(dr["CITY_NAME"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CITY_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (_mCityId == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = _mCityId;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CITY_ID2";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_ACTIVE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = _mIsActive;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CITY_NAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (_mCityName == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = _mCityName;
            }
            pparams.Add(parameter);


        }
        #endregion
    }
}
