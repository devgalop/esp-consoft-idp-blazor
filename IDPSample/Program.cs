using Auth0.AspNetCore.Authentication;
using IDPSample.Components;
using IDPSample.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add Auth0 authentication
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration.GetValue<string>("Auth0:Domain") ?? throw new ConfigurationMissingException("Auth0:Domain");
    options.ClientId = builder.Configuration.GetValue<string>("Auth0:ClientId") ?? throw new ConfigurationMissingException("Auth0:ClientId");
    options.ClientSecret = builder.Configuration.GetValue<string>("Auth0:ClientSecret") ?? throw new ConfigurationMissingException("Auth0:ClientSecret");
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add cascading authentication state
builder.Services.AddCascadingAuthenticationState();

// Add Razor Pages for authentication endpoints
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapRazorPages();

await app.RunAsync();
