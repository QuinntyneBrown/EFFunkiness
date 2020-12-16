# EF Funkiness

An example of how you can run into a InvalidOperationException stating that "There is already an open DataReader associated with this Connection which must be closed first." using Entity Framework Core. 

## To Run the Api (Swagger)

Using the command line console

1. Navigate to the src\EFFUnkiness.Server folder
2. Execute `dotnet run`

## To Run the Integration tests

Using the command line console

1. Execute `dotnet test`


## Funkiness

The code below will normally throw an InvalidOperationException because while a the underlying DataReader the context is using is open, the Connection is in use exclusively by that DataReader. You cannot execute any commands for the Connection, including creating another DataReader, until the original DataReader is closed.

```csharp


var clients = new List<ClientDto>();

var query = from client in _context.Clients
            select client;

foreach (var client in query)
{
    var user = _context.Users.Single(x => x.UserId == client.CreatedByUserId);

    clients.Add(new ClientDto(client.ClientId, client.Name, new UserDto(user.UserId, user.Name)));
}

```

If you enabled automatic retries (see snippet below), you will not experience any issues.

```csharp

services.AddDbContext<EFFunkinessDbContext>(options =>
{
    options.UseSqlServer("your-connectionstring",
        builder => builder.EnableRetryOnFailure());
});

```
