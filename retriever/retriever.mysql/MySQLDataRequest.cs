using Retriever.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retriever.MySQL
{
    public class MySQLDataRequest : IDataRequest
    {
        public string ConnectionString
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        
        public string Fetch(string storedProcedureName, dynamic paramsObject)
        {
            throw new NotImplementedException();
        }

        public string Fetch(string storedProcedureName, string jsonFetchParams)
        {
            throw new NotImplementedException();
        }

        public int Hurl(string storedProcedureName, List<dynamic> objects)
        {
            throw new NotImplementedException();
        }

        public int Hurl(string storedProcedureName, dynamic obj)
        {
            throw new NotImplementedException();
        }

        public int Hurl(string storedProcedureName, string jsonData)
        {
            throw new NotImplementedException();
        }

        public int Hurl(string storedProcedureName, List<dynamic> objects, TransactionMode transMode)
        {
            throw new NotImplementedException();
        }

        public int Hurl(string storedProcedureName, dynamic obj, TransactionMode transMode)
        {
            throw new NotImplementedException();
        }

        public int Hurl(string storedProcedureName, string jsonData, TransactionMode transMode)
        {
            throw new NotImplementedException();
        }

        public async Task<string> FetchAsync(string storedProcedureName, dynamic paramsObject)
        {
             throw new NotImplementedException();
        }

        public async Task<string> FetchAsync(string storedProcedureName, string jsonFetchParams)
        {
            throw new NotImplementedException();
        }

        public async Task<int> HurlAsync(string storedProcedureName, List<dynamic> objects)
        {
            throw new NotImplementedException();
        }

        public async Task<int> HurlAsync(string storedProcedureName, dynamic obj)
        {
            throw new NotImplementedException();
        }

        public async Task<int> HurlAsync(string storedProcedureName, string jsonData)
        {
            throw new NotImplementedException();
        }

        public async Task<int> HurlAsync(string storedProcedureName, List<dynamic> objects, TransactionMode transMode)
        {
            throw new NotImplementedException();
        }

        public async Task<int> HurlAsync(string storedProcedureName, dynamic obj, TransactionMode transMode)
        {
            throw new NotImplementedException();
        }

        public async Task<int> HurlAsync(string storedProcedureName, string jsonData, TransactionMode transMode)
        {
            throw new NotImplementedException();
        }
    }
}
