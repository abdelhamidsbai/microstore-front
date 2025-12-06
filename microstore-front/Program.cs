using microstore_front.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// ========================================
// CONFIGURATION DES SERVICES - PATTERN REPOSITORY
// ========================================

// OPTION 1 (ACTUELLE) : Mock Service pour développement sans Backend
builder.Services.AddScoped<IProductService, MockProductService>();

// OPTION 2 (À ACTIVER PLUS TARD) : API Service avec HttpClient vers le Backend réel
// Décommentez ces lignes et commentez la ligne ci-dessus quand le Backend sera prêt :
/*
builder.Services.AddHttpClient<IProductService, ApiProductService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BackendUrl"] 
        ?? throw new InvalidOperationException("BackendUrl non configurée dans appsettings.json"));
    client.Timeout = TimeSpan.FromSeconds(30);
});
*/

// Note: Dans appsettings.json, ajoutez: "BackendUrl": "http://backend-api:8080"

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
