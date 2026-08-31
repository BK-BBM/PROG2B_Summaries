# LU2 Theme 2 (WebApis)

## Remember the following.

Add migrations by using 
``` csharp
dotnet ef migrations add migration_name
```
<br>
Then finish off with

``` csharp
dotnet ef database update 
```
to apply the migration and create your database table on your SQL server <br>

## DTOs 

We use DTOs (Data Transfer Objects) to separate our app layers. This means, the client making requests won't have access to our TaskItem class (our Entity). 
The reason for doing this is to avoid DB structure from "leaking" to the client.

Think of DTOs as preventing against SQL injection. (If we exposed things like IDs, Roles etc) directly to the client, our WebApi can be compromised.

## Controller Method Signatures

The following method 
``` csharp
public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks()
```
**Task**         - shows that this is an asynchronous method (System.Threading.Tasks)<br>
**ActionResult** - this is an Action Method, but unlike our MVC action methods, this will give us the HTTP Status codes <br>
**IEnumerable** - to indicate that we want to get a collection as the result <br>
**TaskItemDto**  - the actual object sent or received as a JSON array


## Context for the video 

We added an MVC App that sends HTTP calls to our TasksWebApi app. <br>
I would argue that the most important block of code is what we started with in the TasksWebApp Program.cs file (where we register our client) 

``` csharp

builder.Services.AddHttpClient("TasksWebApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7185");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseCookies = true
});

```

_"TasksWebApi"_ is our "named" client and we add it our Dependency Injection container and we configure the client to work with cookies (this is because of our session management code where we also made use of cookies) <br>

We then duplicated our DTOs because we have two separate applications. Each of the apps needs its own C# type that's going to match the JSON shape (because our apps communicate over HTTP as JSON)
<br><br>


``` csharp

var response = await httpClient.PostAsJsonAsync("api/task/login", login);
var tasks = await response.Content.ReadFromJsonAsync<IEnumerable<TaskItemDto>>();
```
The **PostAsJsonAsyn()** method converts C# objects into JSON text (in the line above, the login dto object would be serialized as JSON text) [Read More](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-11.0-pp) <br>
The **ReadFromJsonAsync()** method reads JSON text from the api (the response) and converts it to a C# object (in the example above, the collection of TaskItemDto will be deserialized into C# objects) 
[Read More](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpcontentjsonextensions.readfromjsonasync?view=net-11.0-pp)
