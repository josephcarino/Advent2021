using josephcarino.Advent2021;
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

    Func<IEnumerable<IProblem?>> createProblems = () => Assembly.GetAssembly(typeof(Problem))!
        .GetTypes().Where(TheType => TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(typeof(Problem)))
        .Select(a => Activator.CreateInstance(a, new object[] { problemSettings }) as IProblem);

    services.AddSingleton(createProblems);
    services.AddSingleton<ProblemService>();
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