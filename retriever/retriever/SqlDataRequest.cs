using Newtonsoft.Json;
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

        public string Fetch(string storedProcedureName, string jsonFetchParams)
        {
            string resultJson = string.Empty;
            using (SqlConnection dbConn = CreateNewConnection())
            {
                using (SqlCommand dbComm = new SqlCommand(storedProcedureName, dbConn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    if (!string.IsNullOrWhiteSpace(jsonFetchParams))
                    {
                        dbComm.Parameters.AddRange(jsonFetchParams.DeserializeJsonIntoSqlParameters());
                    }

                    dbConn.Open();
                    SqlDataReader dataReader = dbComm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                    resultJson = dataReader.SerializeToJSON();
                    dataReader.Close();
                }
            }

            return resultJson;
        }

        // Fetch overload which doesn't have any parameters
        public string Fetch(string storedProcedureName)
        {
            return Fetch(storedProcedureName, string.Empty);
        }
        
        public int Hurl(string storedProcedureName, string jsonData, TransactionMode transMode)
        {
            int numberOfRecordsAffected = 0;
            SqlTransaction transaction = null;

            using (SqlConnection dbConn = CreateNewConnection())
            {
                using (SqlCommand dbComm = new SqlCommand(storedProcedureName, dbConn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    if (transMode == TransactionMode.Transaction)
                    {
                        transaction = dbConn.BeginTransaction();
                    }

                    dbComm.Parameters.AddRange(jsonData.DeserializeJsonIntoSqlParameters());

                    dbConn.Open();
                    numberOfRecordsAffected = dbComm.ExecuteNonQuery();
                    if (transaction != null) { transaction.Commit(); }
                }
            }

            return numberOfRecordsAffected;
        }

        public int Hurl(string storedProcedureName, System.Collections.Generic.List<dynamic> objects, TransactionMode transMode)
        {
            // This has slightly different logic because we need to scope the transaction differently.
            int numberOfRecordsAffected = 0;
            SqlTransaction transaction = null;

            using (SqlConnection dbConn = CreateNewConnection())
            {
                using (SqlCommand dbComm = new SqlCommand(storedProcedureName, dbConn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    if (transMode == TransactionMode.Transaction)
                    {
                        transaction = dbConn.BeginTransaction();
                    }

                    foreach (dynamic obj in objects)
                    {
                        dbComm.Parameters.Clear();
                        string jsonString = JsonConvert.SerializeObject(obj);
                        dbComm.Parameters.AddRange(jsonString.DeserializeJsonIntoSqlParameters());

                        if (dbConn.State != System.Data.ConnectionState.Open) { dbConn.Open(); }
                        numberOfRecordsAffected += dbComm.ExecuteNonQuery();
                    }

                    if (transaction != null) { transaction.Commit(); }
                }
            }

            return numberOfRecordsAffected;
        }

        // All update overloads for flexibility of use
        public int Hurl(string storedProcedureName, string jsonData)
        {
            return Hurl(storedProcedureName, jsonData, TransactionMode.None);
        }

        public int Hurl(string storedProcedureName, dynamic obj) 
        {
            return Hurl(storedProcedureName, JsonConvert.SerializeObject(obj));
        }

        public int Hurl(string storedProcedureName, dynamic obj, TransactionMode transMode)
        {
            return Hurl(storedProcedureName, JsonConvert.SerializeObject(obj), transMode);
        }

        public int Hurl(string storedProcedureName, System.Collections.Generic.List<dynamic> objects)
        {
           return Hurl(storedProcedureName, objects, TransactionMode.None);
        }
                
        public string ConnectionString { get; set; }
    }
}
