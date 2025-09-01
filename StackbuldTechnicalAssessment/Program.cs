using StackbuldTechnicalAssessment.Infrastructure;
using StackbuldTechnicalAssessment.Application;
using StackbuldTechnicalAssessment.Web.Extensions;
using System.Reflection;
using StackbuldTechnicalAssessment.Web.Filters.Swashbuckle;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddSwaggerDocs();
builder.Services.AddApplicationConfig(builder.Configuration);
builder.Services.AddInfrastructureConfig(builder.Configuration.GetConnectionString("DefaultConnectionString"));

//used to enable summary used in controller work in the swagegr Ui
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<SnakeCaseDictionaryFilter>();
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.CustomSchemaIds(type => type.FullName);

    c.SchemaFilter<SnakeCaseDictionaryFilter>();
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(
     options =>
     {
         options.SwaggerEndpoint("/swagger/v1/swagger.json", "StackBuldTechnicalAssessmentV1");
     });

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
