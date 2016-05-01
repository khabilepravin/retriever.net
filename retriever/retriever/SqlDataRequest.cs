using Newtonsoft.Json;
using System;
using System.Data.SqlClient;

namespace Retriever.Net
{
    public class SqlDataRequest : IDataRequest
    {       
        public SqlDataRequest() 
        {            
        }
        
        /// <summary>
        /// Creates an instance of the DataRequest with specific connection string config key
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public SqlDataRequest(string connectionString) 
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {   
                this.ConnectionString = connectionString;             
            }
            else
            {
                throw new Exception("connectionString constructor parameter is null or empty");
            }
        }
              
        public string Fetch(string storedProcedureName, string jsonFetchParams)
        {
            string resultJson = string.Empty;
            using (SqlConnection dbConn = new SqlConnection(this.ConnectionString))
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
                }
            }

            return resultJson;
        }

        public string Fetch(string storedProcedureName)
        {
            return Fetch(storedProcedureName, string.Empty);
        }

        public string Fetch(string storedProcedureName, dynamic paramsObject)
        {
            return Fetch(storedProcedureName, JsonConvert.SerializeObject(paramsObject));
        }
        
        public int Hurl(string storedProcedureName, string jsonData, TransactionMode transMode)
        {
            int numberOfRecordsAffected = 0;
            SqlTransaction transaction = null;

            using (SqlConnection dbConn = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand dbComm = new SqlCommand(storedProcedureName, dbConn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    if (transMode == TransactionMode.On)
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

            using (SqlConnection dbConn = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand dbComm = new SqlCommand(storedProcedureName, dbConn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    if (transMode == TransactionMode.On)
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
            return Hurl(storedProcedureName, jsonData, TransactionMode.Off);
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
           return Hurl(storedProcedureName, objects, TransactionMode.Off);
        }
                
        public string ConnectionString { get; set; }
    }
}
