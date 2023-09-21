using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApiOmTransaction.Context;
using WebApiOmTransaction.GenerateData;
using WebApiOmTransaction.Interfaces;
using WebApiOmTransaction.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddTransient<DatabaseInitializer>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthentication( options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer")!,
            ValidAudience = builder.Configuration.GetValue<string>("Jwt:Issuer")!,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")!))
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
builder.Services.AddScoped<IEntrepriseRepository, EntrepriseRepository>();
builder.Services.AddScoped<ISimCodeAgentRepository, SimCodeAgentRepository>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IStatistiqueRepository, StatistiqueRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OmOperationContext>(opt =>
{
    opt.UseMySql(builder.Configuration.GetValue<string>("connectionString")!, ServerVersion.AutoDetect(builder.Configuration.GetValue<string>("connectionString")!));
});
var app = builder.Build();
//app.Urls.Add("http://localhost:5000");
//Console.WriteLine("jwt : " + builder.Configuration.GetValue<string>("Jwt:Issuer"));

// void SeedData(IHost app)
// {
//     using (var scope = app.Services.CreateScope())
//     {
//         var services = scope.ServiceProvider;
//
//         var context = services.GetRequiredService<OmOperationContext>();
//
//         DatabaseInitializer.Seed(context);
//     }
// }
//
// SeedData(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();