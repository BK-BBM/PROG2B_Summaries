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
