using System.Data.SqlClient;

namespace Retriever.Net
{
    public class SqlDataRequest : IDataRequest
    {
        /// <summary>
        /// This will assume "ConnectionString" is the config key for the connection string and will look for it.
        /// </summary>
        public SqlDataRequest() { }
        
        /// <summary>
        /// Creates an instance of the DataRequest with specific connection string config key
        /// </summary>
        /// <param name="connectionStringConfigKey">Configuration key to get connection string from "ConnectionStrings" section</param>
        public SqlDataRequest(string connectionStringConfigKey) { }

        /// <summary>
        /// Creates an instance with already initalized connection object
        /// </summary>
        /// <param name="conn">Initialized SqlConnection object</param>
        public SqlDataRequest(SqlConnection conn) { }



        public string Fetch(string procName)
        {
            throw new System.NotImplementedException();
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
    }
}
