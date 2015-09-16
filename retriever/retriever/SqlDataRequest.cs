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
            return Fetch(storedProcedureName, string.Empty);
        }

        public string Fetch(string storedProcedureName, string jsonFetchParams)
        {
            string resultJson = string.Empty;
            using (SqlConnection dbConn = CreateNewConnection())
            {
                SqlCommand dbComm = new SqlCommand(storedProcedureName, dbConn) { CommandType = System.Data.CommandType.StoredProcedure };

                if (!string.IsNullOrWhiteSpace(jsonFetchParams))
                {
                    dbComm.Parameters.AddRange(jsonFetchParams.DeserializeJsonIntoSqlParameters());
                }

                dbConn.Open();
                SqlDataReader dataReader = dbComm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                resultJson = dataReader.SerializeToJSON();
                dataReader.Close();
            }

            return resultJson;
        }

        public int Update(string storedProcedureName, string jsonData)
        {
            int numberOfRecordsAffected = 0;
            using (SqlConnection dbConn = CreateNewConnection())
            {
                SqlCommand dbComm = new SqlCommand(storedProcedureName, dbConn) { CommandType = System.Data.CommandType.StoredProcedure };

                dbComm.Parameters.AddRange(jsonData.DeserializeJsonIntoSqlParameters());

                dbConn.Open();
                numberOfRecordsAffected = dbComm.ExecuteNonQuery();
            }

            return numberOfRecordsAffected;
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
