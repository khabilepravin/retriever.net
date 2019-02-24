using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Retriever.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace tester
{
    [TestClass]
    public class DataRetrieverTester
    {
        // Change this connection string to your server connection string
        private const string ConnectionString = @"Persist Security Info=False;Initial Catalog=test;Data Source=DESKTOP-P58VK49;Packet Size=4096;Enlist=false;Integrated Security=true;";
        private Guid idGuid = new Guid("87D2DDFE-666D-470A-8984-2EDE473BACF1");
        [TestMethod]
        public void FetchWithoutParamsTest()
        {
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            string result = dataRequest.Fetch("[dbo].[usp_Test_Select]");

            var token = JToken.Parse(result);
            
            Assert.IsTrue(token is JArray);
        }

        [TestMethod]
        public void FetchByDynamicObjParamsTest()
        {             
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            string result = dataRequest.Fetch("[dbo].[usp_Test_Select]", new { Id = idGuid });

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void FetchByJSONStringParamsTest()
        {
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            string result = dataRequest.Fetch("[dbo].[usp_Test_Select]", JsonConvert.SerializeObject(new { Id = idGuid}));

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void SingleObjectHurlTest()
        {
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);
                      
            var obj = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };
            
            int rowsAffected = dataRequest.Hurl("[dbo].[usp_Test_Insert]", obj);
            Assert.IsTrue(rowsAffected > 0);
        }

        [TestMethod]
        public void MultiObjectHurlTest()
        {
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            var objForList1 = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            var objForList2 = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            var objForList3 = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };
            List<dynamic> objects = new List<dynamic>() { objForList1, objForList2, objForList3 };

            int rowsAffected3 = dataRequest.Hurl("[dbo].[usp_Test_Insert]", objects);
            Assert.IsTrue(rowsAffected3 > 0);            
        }

        [TestMethod]
        public void JsonStringHurlTest()
        {
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            var obj2 = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            int rowsAffected2 = dataRequest.Hurl("[dbo].[usp_Test_Insert]", JsonConvert.SerializeObject(obj2));
            Assert.IsTrue(rowsAffected2 > 0);
        }



        //Async tests 
        [TestMethod]
        public async Task FetchasyncTest()
        {
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            string result = await dataRequest.FetchAsync("[dbo].[usp_Test_Select]", new { Id = idGuid });

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task SingleObjectHurlAsyncTest()
        {
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            var obj = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test Async:{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description Async:{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details Async:{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            int rowsAffected = await dataRequest.HurlAsync("[dbo].[usp_Test_Insert]", obj);
            Assert.IsTrue(rowsAffected > 0);
        }

        [TestMethod]
        public async Task MultiObjectHurlAsyncTest()
        {
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            var objForList1 = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test Async:{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description Async:{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details Async:{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            var objForList2 = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test Async :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description Async :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details Async :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            var objForList3 = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test Async :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description Async :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details Async:{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };
            List<dynamic> objects = new List<dynamic>() { objForList1, objForList2, objForList3 };

            int rowsAffected3 = await dataRequest.HurlAsync("[dbo].[usp_Test_Insert]", objects);
            Assert.IsTrue(rowsAffected3 > 0);
        }

        [TestMethod]
        public async Task JsonStringHurlAsyncTest()
        {
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            var obj2 = new
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Test Async:{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description Async:{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details Async:{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            int rowsAffected2 = await dataRequest.HurlAsync("[dbo].[usp_Test_Insert]", JsonConvert.SerializeObject(obj2));
            Assert.IsTrue(rowsAffected2 > 0);
        }
    }
}
