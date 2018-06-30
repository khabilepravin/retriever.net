using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retriever.Core
{
    public interface IDataRequest
    {
        string ConnectionString { get; set; }
        string Fetch(string storedProcedureName, string jsonFetchParams = null);
        string Fetch(string storedProcedureName, dynamic paramsObject);
        int Hurl(string storedProcedureName, string jsonData, TransactionMode transMode);
        int Hurl(string storedProcedureName, string jsonData);
        int Hurl(string storedProcedureName, dynamic obj);
        int Hurl(string storedProcedureName, dynamic obj, TransactionMode transMode);
        int Hurl(string storedProcedureName, List<dynamic> objects, TransactionMode transMode);
        int Hurl(string storedProcedureName, List<dynamic> objects);

        Task<string> FetchAsync(string storedProcedureName, string jsonFetchParams = null);
        Task<string> FetchAsync(string storedProcedureName, dynamic paramsObject);
        Task<int> HurlAsync(string storedProcedureName, string jsonData, TransactionMode transMode);
        Task<int> HurlAsync(string storedProcedureName, string jsonData);
        Task<int> HurlAsync(string storedProcedureName, dynamic obj);
        Task<int> HurlAsync(string storedProcedureName, dynamic obj, TransactionMode transMode);
        Task<int> HurlAsync(string storedProcedureName, List<dynamic> objects, TransactionMode transMode);
        Task<int> HurlAsync(string storedProcedureName, List<dynamic> objects);

    }
}
