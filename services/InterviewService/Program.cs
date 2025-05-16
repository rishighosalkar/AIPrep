using InterviewService.Commands.ScheduleInterview;
using InterviewService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<InterviewDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("InterviewDb")));

//MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ScheduleInterviewCommand).Assembly));

//Cors policy 
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();


