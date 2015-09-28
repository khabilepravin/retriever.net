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

            string result = dataRequest.Fetch("[dbo].[usp_Test_Select]", JsonConvert.SerializeObject(new { Id = 1 }));

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void ExecuteUpdateTests()
        {
            SqlDataRequest dataRequest = new SqlDataRequest("ConnectionString");

            var obj = new
            {
                Id = 1,
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };
            
            dataRequest.Update("[dbo].[usp_Test_Insert]", obj);

            var obj2 = new
            {
                Id = 2,
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            dataRequest.Update("[dbo].[usp_Test_Insert]", JsonConvert.SerializeObject(obj2));
        }
    }
}
