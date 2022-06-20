# Description
This project has been created for my medium article about Couchbase Server.
Before running the project, make sure you have .NET 6 SDK and a running couchbase 
server instance on your local machine. After that, you should create a bucket 
on Couchbase UI called "products" and create a primary index for this bucket.

To create primary index, simply run the query below:
```
create primary index on products
```

Then you can run the project with;
```
dotnet run --project <.csproj path>
```