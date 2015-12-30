# Retriever.net
Welcome to Retriever.net

In most .net projects some way of accessing database data is required, usually for all new projects many may choose to use ORM frameworks. But at times you just need an easy way to access data without much configuration. With many modern "Apps", all you need is JSON as input and output. 

This library makes it super easy to have JSON based dataAccess layer in minutes, currently only works with MS SQL Server database. 

This library depends on Newtonsoft.Json 

You can do retrieve directly to JSON with couple of lines of code like... 

    // Param is the key to the connectionstring config 
    SqlDataRequest dataRequest = new SqlDataRequest("ConnectionString");    
    string result = dataRequest.Fetch("[dbo].[usp_Test_Select]", "{ Id : 1 }"));  

Similarly insert/update is as simple as... 

    var obj = new
    {
     Id = 1,
     Name = "John Doe",
     Description = "Demo test object"
    };

    int rowsAffected = dataRequest.Update("[dbo].[usp_Test_Insert]", obj);

    // or you could directly send any JSON string like...
    int rowsAffected = dataRequest.Update("[dbo].[usp_Test_Insert]", "{ Id : 1, Name :'Test' }");

    // or a list of objects like...
    List<dynamic> objects = new List<dynamic>() { obj1, obj2, obj3 };
    int rowsAffected = dataRequest.Update("[dbo].[usp_Test_Insert]", objects);
