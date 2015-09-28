using System.Collections.Generic;

namespace Retriever.Net
{
    interface IDataRequest
    {
        string ConnectionString { get; set; }
        string Fetch(string storedProcedureName, string jsonFetchParams);
        string Fetch(string storedProcedureName);        
        int Update(string storedProcedureName, string jsonData, TransactionMode transMode);
        int Update(string storedProcedureName, string jsonData);        
        int Update(string storedProcedureName, dynamic obj);
        int Update(string storedProcedureName, dynamic obj, TransactionMode transMode);
        int Update(string storedProcedureName, List<dynamic> objects, TransactionMode transMode);
        int Update(string storedProcedureName, List<dynamic> objects);
        
    }
}
