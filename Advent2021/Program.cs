using josephcarino.Advent2021;
using josephcarino.Advent2021.Helpers;
using josephcarino.Advent2021.Services;
using josephcarino.Advent2021.Services.Problems;
using josephcarino.Advent2021.Services.Problems.Implementation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Configuration, builder.Services);

//builder.WebHost.UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));

var app = builder.Build();

ConfigureMiddleware(app, app.Services);
ConfigureEndpoints(app, app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Run();

void ConfigureServices(ConfigurationManager configuration, IServiceCollection services)
{
    ProblemSettings problemSettings = new();
    configuration.Bind("ProblemSettings", problemSettings);
    services.AddSingleton<ProblemSettings>(problemSettings);

    services.AddControllersWithViews();

    Func<IList<object>, IEnumerable<IProblem?>> createProblems = (IList<object> args) => {
        args.Insert(0, problemSettings);
        object[] argsArray = args.ToArray();

        return Assembly.GetAssembly(typeof(Problem))!
        .GetTypes().Where(TheType => TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(typeof(Problem)))
        .Select(a => Activator.CreateInstance(a, argsArray) as IProblem);
    };

    services.AddSingleton(createProblems);
    services.AddSingleton<ProblemService>();
    services.AddSingleton<IFileHelper, FileHelper>();
    services.AddSingleton<IProblemFactory, ProblemFactory>();
}

void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services)
{
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
}

void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services)
{
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");
}
