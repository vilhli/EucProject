var builder = DistributedApplication.CreateBuilder(args);

var webApi = builder.AddProject<Projects.Web_Api>("web-api");

builder.AddProject<Projects.WebUI>("webui")
	.WithExternalHttpEndpoints()
	.WithReference(webApi)
	.WaitFor(webApi);

builder.Build().Run();
