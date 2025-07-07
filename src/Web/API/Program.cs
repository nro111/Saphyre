using Infrastructure;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("System.Net.Sockets.DisableIPv6", true);

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000);
});

builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<Supabase.Client>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var url = config["SUPABASE_URL"];
    var key = config["SUPABASE_API_KEY"];

    if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(key))
        throw new InvalidOperationException("Supabase URL or API key not configured.");

    var supabaseOptions = new Supabase.SupabaseOptions
    {
        AutoRefreshToken = true
    };

    var client = new Supabase.Client(url, key, supabaseOptions);
    return (Supabase.Client)client.InitializeAsync().GetAwaiter().GetResult();
});

builder.Services.AddUsersModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
