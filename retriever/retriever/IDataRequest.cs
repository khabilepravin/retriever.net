using System.Collections.Generic;

namespace Retriever.Net
{
    interface IDataRequest
    {
        string ConnectionString { get; set; }
        string Fetch(string storedProcedureName, string jsonFetchParams);
        string Fetch(string storedProcedureName);
        string Fetch(string storedProcedureName, dynamic paramsObject);
        int Hurl(string storedProcedureName, string jsonData, TransactionMode transMode);
        int Hurl(string storedProcedureName, string jsonData);        
        int Hurl(string storedProcedureName, dynamic obj);
        int Hurl(string storedProcedureName, dynamic obj, TransactionMode transMode);
        int Hurl(string storedProcedureName, List<dynamic> objects, TransactionMode transMode);
        int Hurl(string storedProcedureName, List<dynamic> objects);
        
    }
}
