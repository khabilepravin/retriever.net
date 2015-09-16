using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Retriever.Net;
using Newtonsoft.Json;

namespace tester
{
    [TestClass]
    public class DataRetrieverTester
    {
        [TestMethod]
        public void ExecuteFetchTests()
        {
            SqlDataRequest dataRequest = new SqlDataRequest("ConnectionString");

            dataRequest.Fetch("[dbo].[usp_Test_Select]", JsonConvert.SerializeObject(new { Id = 1 }));
        }
    }
}
