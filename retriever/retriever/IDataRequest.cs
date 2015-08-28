using System.Collections.Generic;

namespace Retriever.Net
{
    interface IDataRequest
    {
        string Fetch(string procName);
        string Fetch(string procName, string jsonFetchParams);
        string Update(string procName, string jsonData);
        string Update(List<dynamic> objects);
    }
}
