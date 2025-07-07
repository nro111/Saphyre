using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<Supabase.Client>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var url = config["Supabase:Url"];
    var key = config["Supabase:ApiKey"];

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
