using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Retriever.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace tester
{
    [TestClass]
    public class DataRetrieverTester
    {
        private const string ConnectionString = @"Persist Security Info=False;Initial Catalog=Dev_Test;Data Source=EDIMS-DEVDB1-VM\SQL2008R2;Packet Size=4096;Enlist=false;User Id=EDIMAppAcct;Password=X13b18P!Rh;";
        [TestMethod]
        public void FetchTest()
        {             
            SqlDataRequest dataRequest = new SqlDataRequest(ConnectionString);

            string result = dataRequest.Fetch("[dbo].[usp_Test_Select]", new { Id = new Guid("69E24E7A-A24B-4241-BA5E-022E3EDBDA1D") });

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
    }
}
