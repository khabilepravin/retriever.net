using System.Collections.Generic;

namespace Retriever.Net
{
    interface IDataRequest
    {
        string ConnectionString { get; set; }
        string Fetch(string procName);
        string Fetch(string procName, string jsonFetchParams);
        string Update(string procName, string jsonData);
        string Update(string procName, string jsonData, TransactionMode transMode);
        string Update(string procName, List<dynamic> objects);
        string Update(string procname, List<dynamic> objects, TransactionMode transMode);
    }
}
