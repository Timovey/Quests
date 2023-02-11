using AuthService.DataContracts.Interfaces;
using CommonDatabase.QuestDatabase;
using CommonDatabase.QuestDatabase.Implements;
using CommonDatabase.QuestDatabase.Interfaces;
using CommonDatabase.QuestDatabase.MappingProfiles;
using CommonDatabase.QuestDatabase.Triggers;
using GenerateQuestsService.Core.BusinessLogic;
using GenerateQuestsService.DataContracts.DataContracts;
using GenerateQuestsService.DataContracts.Models.Stages;
using GenerateQuestsService.Main.DeserializationHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddDbContext<QuestContext>(options =>
    options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseTriggers(triggerOptions => {
        triggerOptions.AddTrigger<SetStageCountTrigger>();
    })
);

builder.Services.AddScoped<IGenerateQuestStorage, GenerateQuestStorage>();
builder.Services.AddScoped<GenerateQuestLogic>();


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.AllowInputFormatterExceptionMessages = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //options.JsonSerializerOptions.
    options.JsonSerializerOptions.Converters.Add(new StageJsonConverterHelper<Stage>());
});

//Auto mapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<GenerateQuestsMappingProfile>());



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var refitSettings = new RefitSettings
{
    ContentSerializer = new SystemTextJsonContentSerializer(
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }
    )
};

//!_! ------------------ Auth
var authAddress = new Uri(builder.Configuration["AuthSettings:BaseAddress"]);
builder.Services.AddRefitClient<IAuthApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = authAddress);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 1 == 1)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
