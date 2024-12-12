using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc().AddRazorPagesOptions(o =>
            o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));
builder.Services.AddSession();
builder.Services.AddMemoryCache();

// Register DbAccess
builder.Services.AddSingleton<DbAccess>();

// Register repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<ShoppingCartRepository>();

// Register services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LoginStateService>();
builder.Services.AddScoped<UserCredentialsService>();
builder.Services.AddScoped<UserPermissionService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ShoppingCartService>();

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
app.UseSession();
app.MapRazorPages();

app.Run();