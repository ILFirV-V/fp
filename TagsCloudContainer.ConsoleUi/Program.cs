using Autofac;
using TagsCloudContainer.ConsoleUi;
using TagsCloudContainer.ConsoleUi.Runner.Interfaces;
using TagsCloudContainer.Core.Extensions;

var builder = new ContainerBuilder();
builder.RegisterModule(new ConsoleClientModule());

var app = builder.Build();
using var scope = app.BeginLifetimeScope();
var appRunner = scope.Resolve<ITagsCloudContainerUi>();

appRunner.Run(args)
    .Then(_ => Console.WriteLine("Программа отработала штатно"))
    .OnFail(err => Console.WriteLine(err.Message));