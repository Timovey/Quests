using GenerateQuestsService.DataContracts.Interfaces;
using GenerateQuestsService.DataContracts.JsonHelpers;
using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProcessQuestService.Core.MappingProfiles;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using ProcessQuestService.Core.BusinessLogic;
using ProcessQuestService.Core.Helpers;
using ProcessQuestService.Core.HelperModels;

var builder = WebApplication.CreateBuilder(args);

//����������� ������ ��� ��������� ������ �� ���������
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});


//���������� Postgesql
//builder.Services.AddDbContext<QuestContext>(options =>
//    options
//    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
//);

//��������� ������������
var redisSettings = builder.Configuration.GetSection(nameof(RedisSetting)).Get<RedisSetting>();

// ���������� �����������
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = redisSettings.Host;
    options.InstanceName = redisSettings.InstanceName;
});


builder.Services.Configure<RedisSetting>(builder.Configuration.GetSection(nameof(RedisSetting)));

//������ � ������������� �� 

builder.Services.AddScoped<ProcessQuestLogic>();
builder.Services.AddScoped<ProcessQuestCacheHelper>();

//��������� ����������� � ������������� Json ����� ��� �������������� �������
//��������� ����������� ����������� ��������������
//� �������������� ������
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.AllowInputFormatterExceptionMessages = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(new StageJsonConverterHelper<Stage>());
    //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

//��������� Auto mapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ProcessQuestMappingProfile>());



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//����������� Refit
//������ ����� ��������� ��� ����� ��������
// "Value" = "value"
var refitSettings = new RefitSettings
{
    ContentSerializer = new SystemTextJsonContentSerializer(
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }
    )
};


//����������� Refit �������
//!_! ------------------ Generate Quest Service
var generateQuestAddress = new Uri(builder.Configuration["GenerateQuestSettings:BaseAddress"]);
builder.Services.AddRefitClient<IGenerateQuestsApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = generateQuestAddress);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 1 == 1)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(5)
};
app.UseWebSockets(webSocketOptions);

app.Run();
