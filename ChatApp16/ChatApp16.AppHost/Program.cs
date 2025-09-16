var builder = DistributedApplication.CreateBuilder(args);

#region cpu
//var ollama = builder.AddOllama("ollama").WithLifetime(ContainerLifetime.Persistent)
//    .WithDataVolume();
//var chat = ollama.AddModel("chat", "llama3.2");
//var embeddings = ollama.AddModel("embeddings", "all-minilm");
#endregion

#region GPu
var ollama = builder.AddOllama("ollama")
    .WithImageTag("latest")
    .WithContainerRuntimeArgs("--gpus=all")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("ollama");
#endregion


var chat = ollama.AddModel("chat", "llama3.2");
var embeddings = ollama.AddModel("embeddings", "all-minilm");

var webApp = builder.AddProject<Projects.ChatApp16_Web>("aichatweb-app");
webApp
    .WithReference(chat)
    .WithReference(embeddings)
    .WaitFor(chat)
    .WaitFor(embeddings);

builder.Build().Run();
