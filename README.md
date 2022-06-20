# Description
This project has been created for my Medium article about Couchbase Server. Before running the project, make sure you have .NET 6 SDK and a running Couchbase server instance on your local machine. After that, create a Bucket on Couchbase UI called "products" and create a primary index for this bucket.

### Note:
Credentials on appsettings.json must be changed with the ones you set up
your cluster.

To create a primary index, simply run the query below:
```
create primary index on products
```

Then you can run the project with;
```
dotnet run --project <.csproj path>
```