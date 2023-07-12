using FCI_API.Data;
using FCI_DataAccess.Repository;
using FCI_DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper
builder.Services.AddAutoMapper(typeof(FCI_API.Helper.AutoMapper));


#endregion

#region Repository

builder.Services.AddScoped<IPostRepository, PostRepository>();


#endregion

#region EntityFramwork
var CoonectioString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(CoonectioString));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors(policyName: "CorsPolicy");

app.Map("/", () => Results.Redirect("/swagger"));

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
