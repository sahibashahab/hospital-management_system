using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Database
    {
        public static string ConnectionString;
        private static System.Collections.Hashtable SqlparamCache = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
        private SqlConnection Connection = new SqlConnection();

        public void connect(string servername, string database, string serverusername, string serverpass)
        {
            ConnectionString = @"Data Source=" + servername + ";Initial Catalog=" + database + ";User ID=" + serverusername + ";Password=" + serverpass;

        }

        private SqlCommand DbCommand = new SqlCommand();
        private SqlDataAdapter DtAdapter = new SqlDataAdapter();
        private DataSet SqlDataSet = new DataSet();
        private DataTable SqlTable = new System.Data.DataTable();
        //private SqlDataReader sqlReader;


        public void UnLoadSpParameters()
        {
            DbCommand.Parameters.Clear();
        }

        public void LoadSpParameters(string SpName, params object[] ParaValues)
        {
            SqlParameter[] TheParameters = (SqlParameter[])SqlparamCache[SpName];
            DbCommand.Parameters.Clear();
            if (TheParameters == null)
            {
                DbCommand.CommandType = CommandType.StoredProcedure;
                DbCommand.CommandText = SpName;
                SqlCommandBuilder.DeriveParameters(DbCommand);
                TheParameters = new SqlParameter[DbCommand.Parameters.Count];

                DbCommand.Parameters.CopyTo(TheParameters, 0);
                SqlparamCache[SpName] = TheParameters;

            }
            else
            {
                short i;
                SqlParameter SqPr;
                DbCommand.CommandType = CommandType.StoredProcedure;
                DbCommand.CommandText = SpName;
                for (i = 0; i < TheParameters.Length; i++)
                {
                    SqPr = (SqlParameter)((System.ICloneable)(TheParameters[i])).Clone();
                    DbCommand.Parameters.Add(SqPr);
                }

            }
            MoveSqlParameters(ParaValues);

        }

        private void MoveSqlParameters(object[] Paras)
        {
            short ic;
            SqlParameter sqlPara;
            if (Paras.Length >= 0)
            {
                for (ic = 0; ic < Paras.Length; ic++)
                {
                    sqlPara = DbCommand.Parameters[ic + 1];
                    string s = sqlPara.ParameterName;
                    sqlPara.Value = Paras[ic];
                }
            }
        }

        public SqlParameter Parameters(int P)
        {
            return DbCommand.Parameters[P];
        }
        public void AddParameter(string parameterName, object value)
        {
            var parameter = DbCommand.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            DbCommand.Parameters.Add(parameter);
        }


        public bool OpenConnection()
        {
            try
            {
                if (Connection.State == ConnectionState.Open) return true;
                Connection = new SqlConnection();
                Connection.ConnectionString = ConnectionString;
                Connection.Open();
                if (Connection.State == ConnectionState.Open)
                {
                    DbCommand.Connection = Connection;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ee)
            {
                throw new System.Exception("Database:OpenConnection:" + ee.Message);
            }
        }

        public void CloseConnection()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
                DbCommand.Dispose();
                DbCommand = null;
                DtAdapter.Dispose();
                DtAdapter = null;
                SqlDataSet.Dispose();
                SqlDataSet = null;
                SqlTable.Dispose();
                SqlTable = null;
            }
        }

        public SqlDataReader GetDataReader()
        {
            return DbCommand.ExecuteReader();

        }
        public bool HasRows(SqlDataReader reader)
        {
            return reader != null && reader.HasRows;
        }

        public int ExecuteQuery()
        {
            return DbCommand.ExecuteNonQuery();
        }

        public object ExecuteValue()
        {
            return DbCommand.ExecuteScalar();
        }

        public object ExecuteValue(string SQLStatement)
        {
            DbCommand.CommandType = CommandType.Text;
            DbCommand.CommandText = SQLStatement;
            return DbCommand.ExecuteScalar();
        }


        public string ReturnValue(string _PName)
        {
            DbCommand.ExecuteNonQuery();
            return (string)DbCommand.Parameters[_PName].Value.ToString();

        }

        public DataTable GetDataTable()
        {
            DtAdapter.SelectCommand = DbCommand;
            DtAdapter.Fill(SqlTable);
            return SqlTable;
        }

        public SqlConnection ConnectionObject
        {
            get { return this.Connection; }
        }

    }

}

