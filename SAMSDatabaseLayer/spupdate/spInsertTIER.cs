using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

namespace SAMSDatabaseLayer.Classes
{
	public class spInsertTIER
	{
		#region Private Members
		private string sp_Name = "spInsertTIER" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_USER_ID;
		private decimal m_BIKE_MAINTENANCE;
		private decimal m_COMMISSION;
		private decimal m_INCENTIVE1;
		private decimal m_INCENTIVE2;
		private decimal m_INCENTIVE3;
		private decimal m_INCENTIVE4;
		private string m_TIER_NAME;
		private string m_FUEL;
		#endregion
		#region Public Properties
		public int USER_ID
		{
			set
			{
				m_USER_ID = value ;
			}
			get
			{
				return m_USER_ID;
			}
		}
		public decimal BIKE_MAINTENANCE
		{
			set
			{
				m_BIKE_MAINTENANCE = value ;
			}
			get
			{
				return m_BIKE_MAINTENANCE;
			}
		}
		public decimal COMMISSION
		{
			set
			{
				m_COMMISSION = value ;
			}
			get
			{
				return m_COMMISSION;
			}
		}
		public decimal INCENTIVE1
		{
			set
			{
				m_INCENTIVE1 = value ;
			}
			get
			{
				return m_INCENTIVE1;
			}
		}
		public decimal INCENTIVE2
		{
			set
			{
				m_INCENTIVE2 = value ;
			}
			get
			{
				return m_INCENTIVE2;
			}
		}
		public decimal INCENTIVE3
		{
			set
			{
				m_INCENTIVE3 = value ;
			}
			get
			{
				return m_INCENTIVE3;
			}
		}
		public decimal INCENTIVE4
		{
			set
			{
				m_INCENTIVE4 = value ;
			}
			get
			{
				return m_INCENTIVE4;
			}
		}
		public  string TIER_NAME
		{
			set
			{
				m_TIER_NAME = value ;
			}
			get
			{
				return m_TIER_NAME;
			}
		}
		public  string FUEL
		{
			set
			{
				m_FUEL = value ;
			}
			get
			{
				return m_FUEL;
			}
		}


		public IDbConnection  Connection
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
		public IDbTransaction  Transaction
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
		public spInsertTIER()
		{
		}
		#endregion
		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch(Exception e)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
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
				if(m_transaction!=null)
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
			catch(Exception exp)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public void FirstReader(IDataReader dr)
		{
			if(dr.Read())
			{
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_BIKE_MAINTENANCE= Convert.ToDecimal(dr["BIKE_MAINTENANCE"]);
				m_COMMISSION= Convert.ToDecimal(dr["COMMISSION"]);
				m_INCENTIVE1= Convert.ToDecimal(dr["INCENTIVE1"]);
				m_INCENTIVE2= Convert.ToDecimal(dr["INCENTIVE2"]);
				m_INCENTIVE3= Convert.ToDecimal(dr["INCENTIVE3"]);
				m_INCENTIVE4= Convert.ToDecimal(dr["INCENTIVE4"]);
				m_TIER_NAME= Convert.ToString(dr["TIER_NAME"]);
				m_FUEL= Convert.ToString(dr["FUEL"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@USER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_USER_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_USER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BIKE_MAINTENANCE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_BIKE_MAINTENANCE==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BIKE_MAINTENANCE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@COMMISSION" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_COMMISSION==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMMISSION;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@INCENTIVE1" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_INCENTIVE1==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_INCENTIVE1;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@INCENTIVE2" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_INCENTIVE2==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_INCENTIVE2;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@INCENTIVE3" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_INCENTIVE3==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_INCENTIVE3;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@INCENTIVE4" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_INCENTIVE4==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_INCENTIVE4;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TIER_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_TIER_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TIER_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@FUEL" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_FUEL== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_FUEL;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
