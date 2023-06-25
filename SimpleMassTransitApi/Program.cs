using HealthChecks.UI.Client;
using Infrastructure.Data;
using Infrastructure.Mediator.Handlers.Products;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Infrastructure.UnitOfWorks;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleMassTransitApi.Endpoints;
using SimpleMassTransitApi.GraphQL;
using SimpleMassTransitApi.GraphQL.Auth;
using SimpleMassTransitApi.GraphQL.Categories;
using SimpleMassTransitApi.GraphQL.Products;
using SimpleMassTransitApi.Health;
using SimpleMassTransitApi.Middlewares;
using SpanJson.AspNetCore.Formatter;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddSpanJson();
//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("MSSQL")
    .AddSqlServer(builder.Configuration.GetConnectionString("SimpleMassTransit")!)
    .AddRabbitMQ(rabbitConnectionString: "amqp://admin:admin@localhost:5672");

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetProductsHandler>());

builder.Services.AddDbContext<ApiDataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SimpleMassTransit"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductRepository, ProductRepository>()
    .AddScoped(provider => new Lazy<IProductRepository>(() => provider.GetRequiredService<IProductRepository>()));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>()
    .AddScoped(provider => new Lazy<ICategoryRepository>(() => provider.GetRequiredService<ICategoryRepository>()));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

//builder.Services.AddDbContext<CategorySagaDataContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleMassTransit"));
//});

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.FromSeconds(5),
    };
});

builder.Services.AddMassTransit(opts =>
{
    opts.SetKebabCaseEndpointNameFormatter();

    //opts.AddSagaStateMachine<CategoryStateMachine, CategoryState>()
    //                .EntityFrameworkRepository(r =>
    //                {
    //                    r.ExistingDbContext<CategorySagaDataContext>();
    //                    r.UseSqlServer();
    //                });

    opts.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("admin");
            h.Password("admin");
        });
        cfg.ConfigureEndpoints(context);
    });
});



builder.Services.AddMemoryCache();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddInMemorySubscriptions()
    .AddQueryType(q => q.Name("Query"))
    .AddType<CategoriesQueries>()
    .AddType<ProductsQueries>()
    .AddType<AuthQueries>()
    .AddMutationType(m => m.Name("Mutation"))
    .AddType<CategoriesMutations>()
    .AddType<ProductsMutations>()
    .AddType<AuthMutations>()
    .AddSubscriptionType(s => s.Name("Subscription"))
    .AddType<CategoriesSubscriptions>()
    .AddType<ProductsSubscriptions>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddErrorFilter<GraphQLExceptionHandler>()
    .UseAutomaticPersistedQueryPipeline()
    .AddInMemoryQueryStorage();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapCategoriesEndpoints();
app.MapProductsEndpoints();

app.MapControllers();

app.MapGraphQL();

app.Run();

public partial class Program { }
