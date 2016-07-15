using Retriever.Core;
using System;
using System.Collections.Generic;

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

        public string Fetch(string storedProcedureName)
        {
            throw new NotImplementedException();
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
    }
}
