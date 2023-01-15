using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Text.Json;
using AuthService.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    //builder.UseDeveloperExceptionPage();
}

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "QuestCore", Version = "v1" });

    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


//builder.Services.AddScoped<ISmallEntitiesOperations, SmallEntitiesOperations>();

////!_! ------------------ Mapping Profiles
//builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AuthMappingProfile>());


builder.Services.AddCors(x => x.AddDefaultPolicy(xx => { xx.AllowAnyOrigin(); xx.AllowAnyHeader(); }));



////!_! ------------------ Auth
//var authAddress = new Uri(builder.Configuration["AuthSettings:BaseAddress"]);
//builder.Services.AddRefitClient<IAuthApi>(refitSettings)
//    .ConfigureHttpClient(c => c.BaseAddress = authAddress);

////!_! ------------------ SD
//var sdAddress = new Uri(builder.Configuration["SdSettings:BaseAddress"]);
//builder.Services.AddRefitClient<IContractClientApi>(refitSettings)
//    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);
//builder.Services.AddRefitClient<IStandApi>(refitSettings)
//    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);
//builder.Services.AddRefitClient<IContractApi>(refitSettings)
//    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);
//builder.Services.AddRefitClient<IContainerApi>(refitSettings)
//    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();