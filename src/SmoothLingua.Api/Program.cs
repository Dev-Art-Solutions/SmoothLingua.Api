using SmoothLingua.Abstractions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddSingleton<IChatService, ChatService>();

var app = builder.Build();

var chatService = app.Services.GetService<IChatService>()!;

var chatApi = app.MapGroup("/chat");

chatApi.MapPost("/train/{id}", async (int id, Domain domain, CancellationToken cancellationToken) =>
{
    await chatService.Train(id, domain, cancellationToken);

    return Results.Ok();
});

chatApi.MapPost("/handle/{id}", (int id, Chat chat) =>
{
    return Results.Ok(chatService.Handle(id, chat));
});

app.Run();

[JsonSerializable(typeof(Domain))]
[JsonSerializable(typeof(Chat))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
