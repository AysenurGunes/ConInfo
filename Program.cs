using ConInfo.DataAccess;
using ConInfo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ConInfoDbContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
	{
		builder
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IConInfo<Company>, ConInfoRepository<Company, ConInfoDbContext>>();
builder.Services.AddScoped<IConInfo<Employee>, ConInfoRepository<Employee, ConInfoDbContext>>();
builder.Services.AddScoped<IConInfo<EmployeeComInfo>, ConInfoRepository<EmployeeComInfo, ConInfoDbContext>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
//oluþturulan migration ile eðer db yoksa bu configurasyonlar ile db otomatik oluþturulur.
using (var scope = app.Services.CreateScope())
{

	var db = scope.ServiceProvider.GetRequiredService<ConInfoDbContext>();
	try
	{
		db.Database.Migrate();
		var services = scope.ServiceProvider;
		DataGenerator.Initialize(services);

	}
	catch (Exception ex)
	{
		Console.WriteLine(ex.ToString());
	}

	//var pendings = db.Database.GetPendingMigrations().ToList();

}
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
