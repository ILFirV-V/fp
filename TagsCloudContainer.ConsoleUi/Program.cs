using Autofac;
using TagsCloudContainer.ConsoleUi;
using TagsCloudContainer.ConsoleUi.Runner.Interfaces;
using TagsCloudContainer.ConsoleUi.Tuners;
using TagsCloudContainer.ConsoleUi.Tuners.Interfaces;

var builder = new ContainerBuilder();
builder.RegisterType<Tuner>().As<ITuner>();
builder.RegisterModule(new ConsoleClientModule());

var app = builder.Build();
using var scope = app.BeginLifetimeScope();
var appRunner = scope.Resolve<ITagsCloudContainerUi>();
var appTuner = scope.Resolve<ITuner>();
appTuner.Tune(args);
appRunner.Run();