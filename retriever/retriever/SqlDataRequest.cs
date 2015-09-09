using System.Configuration;
using System.Data.SqlClient;

namespace Retriever.Net
{
    public class SqlDataRequest : IDataRequest
    {
        /// <summary>
        /// This will assume "ConnectionString" is the config key for the connection string and will look for it.
        /// </summary>
        public SqlDataRequest() 
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ConnectionString"];

            if (settings != null)
            {
                settings.ConnectionString = settings.ConnectionString;
            }
        }
        
        /// <summary>
        /// Creates an instance of the DataRequest with specific connection string config key
        /// </summary>
        /// <param name="connectionStringConfigKey">Configuration key to get connection string from "ConnectionStrings" section</param>
        public SqlDataRequest(string connectionStringConfigKey) 
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringConfigKey].ConnectionString;
        }

        private SqlConnection CreateNewConnection()
        {
            return new SqlConnection(this.ConnectionString);
        }

        public string Fetch(string storedProcedureName)
        {
            using (SqlConnection dbConn = CreateNewConnection())
            {
                SqlCommand dbComm = new SqlCommand(storedProcedureName, dbConn) { CommandType = System.Data.CommandType.StoredProcedure };

                SqlDataReader dataReader = dbComm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }

            return string.Empty;
        }

        public string Fetch(string procName, string jsonFetchParams)
        {
            throw new System.NotImplementedException();
        }

        public string Update(string procName, string jsonData)
        {
            throw new System.NotImplementedException();
        }

        public string Update(string procName, string jsonData, TransactionMode transMode)
        {
            throw new System.NotImplementedException();
        }

        public string Update(string procName, System.Collections.Generic.List<dynamic> objects)
        {
            throw new System.NotImplementedException();
        }

        public string Update(string procName, System.Collections.Generic.List<dynamic> objects, TransactionMode transMode)
        {
            throw new System.NotImplementedException();
        }

        public string ConnectionString { get; set; }
    }
}
