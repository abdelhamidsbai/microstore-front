using microstore_front.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// ========================================
// CONFIGURATION DES SERVICES - PATTERN REPOSITORY
// ========================================

// Déterminer si on utilise le Mock ou l'API réelle
var useMockService = builder.Environment.IsDevelopment()
    && string.IsNullOrEmpty(builder.Configuration["ProductApi:BaseUrl"]);

if (useMockService)
{
    // DÉVELOPPEMENT LOCAL : Mock Service sans Backend
    builder.Services.AddScoped<IProductService, MockProductService>();
}
else
{
    // PRODUCTION / DOCKER : API Service avec HttpClient vers le Backend réel
    var productApiBaseUrl = builder.Configuration["ProductApi:BaseUrl"]
        ?? throw new InvalidOperationException("ProductApi:BaseUrl non configurée");

    builder.Services.AddHttpClient<IProductService, ApiProductService>(client =>
    {
        client.BaseAddress = new Uri(productApiBaseUrl);
        client.Timeout = TimeSpan.FromSeconds(30);
    });
}

// Configuration via variables d'environnement Docker :
// docker run -e ProductApi__BaseUrl=http://localhost:5000 microstore-front:local

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
