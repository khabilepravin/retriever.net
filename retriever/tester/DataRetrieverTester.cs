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

            Random r = new Random(DateTime.Now.Millisecond);
            int rInt = r.Next(0, int.MaxValue); //for ints            

            #region Test for sending object directly
            var obj = new
            {
                Id = rInt,
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };
            
            int rowsAffected = dataRequest.Hurl("[dbo].[usp_Test_Insert]", obj);
            Assert.IsTrue(rowsAffected == -1 || rowsAffected > 0);
            #endregion Test for sending object directly

            #region Test for sending json string
            var obj2 = new
            {
                Id = (rInt + 1),
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            int rowsAffected2 = dataRequest.Hurl("[dbo].[usp_Test_Insert]", JsonConvert.SerializeObject(obj2));
            Assert.IsTrue(rowsAffected2 == -1 || rowsAffected2 > 0);
            #endregion Test for sending json string

            #region Test for sending list of objects
            var objForList1 = new
            {
                Id = (rInt + 1),
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            var objForList2 = new
            {
                Id = (rInt + 1),
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };

            var objForList3 = new
            {
                Id = (rInt + 1),
                Name = string.Format("Test :{0}", DateTime.Now.ToLongDateString()),
                Description = string.Format("Description :{0}", DateTime.Now.ToLongDateString()),
                Details = string.Format("Details :{0}", DateTime.Now.ToLongDateString()),
                RecordedDate = DateTime.Now,
                IsActive = true
            };
            List<dynamic> objects = new List<dynamic>() { objForList1, objForList2, objForList3 };

            int rowsAffected3 = dataRequest.Hurl("[dbo].[usp_Test_Insert]", objects);
            Assert.IsTrue(rowsAffected3 == -1 || rowsAffected3 > 0);
            #endregion Test for sending list of objects
        }
    }
}
