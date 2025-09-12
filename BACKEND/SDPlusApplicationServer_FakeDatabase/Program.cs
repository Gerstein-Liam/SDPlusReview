using SDPlusApplicationServer_FakeDatabase.FakeDb;
using SDPlusApplicationServer_FakeDatabase.Middlewares;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<FakeDb>();
builder.Services.AddSingleton<FakeDbController>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
     options.JsonSerializerOptions.PropertyNameCaseInsensitive=false;
    //options.JsonSerializerOptions.Encoder.enc

}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<HTTPSnifferMiddleware>();

var app = builder.Build();
app.UseMiddleware<HTTPSnifferMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
