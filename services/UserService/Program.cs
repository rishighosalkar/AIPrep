using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UserService.Data;
using UserService.DTOs;
using UserService.Helpers;
using UserService.Middlewares;
using UserService.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService.Services.UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = bool.Parse(builder.Configuration["JwtSettings:ValidateIssueSignature"]),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:IssuerSigningKey"])),
        ValidateIssuer = bool.Parse(builder.Configuration["JwtSettings:ValidateIssuer"]),
        ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
        ValidateAudience = bool.Parse(builder.Configuration["JwtSettings:ValidateAudience"]),
        ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
        RequireExpirationTime = bool.Parse(builder.Configuration["JwtSettings:RequireExpirationTime"]),
        ValidateLifetime = bool.Parse(builder.Configuration["JwtSettings:ValidateLifeTime"]),
        ClockSkew = TimeSpan.FromMinutes(5)
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            Console.WriteLine("Token received: " + context.Token ?? "No token received");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine("Authentication challenge triggered.");
            return Task.CompletedTask;
        }
    };
});

//builder.Services.AddCors(policyBuilder =>
//    policyBuilder.AddDefaultPolicy(policy =>
//        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader())
//);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService API", Version = "v1" });

    // Add JWT Authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token below:\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsIn..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwagger();
//app.UseSwaggerUI();
app.UseHttpsRedirection();
//app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<ExceptionMiddleware>();


app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DbInitializer.Seed(db);
}
app.Run();

