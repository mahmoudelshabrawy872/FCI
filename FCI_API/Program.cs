using FCI_API.Data;
using FCI_DataAccess.Repository;
using FCI_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper
builder.Services.AddAutoMapper(typeof(FCI_API.Helper.AutoMapperProfile));


#endregion


#region Cycle
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
#endregion

#region Repository

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


#endregion


#region Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        //options.Password.RequireDigit = false;
        //options.Password.RequiredLength = 4;
        //options.Password.RequireNonAlphanumeric = false;
        //options.Password.RequireUppercase = false;
        //options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header

            },
            new List<string>()
        }

    });
});
builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(z =>
{
    z.RequireHttpsMetadata = false;
    z.SaveToken = true;
    z.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("APISetting:secret"))),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

#region EntityFramwork
var CoonectioString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(CoonectioString));
#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseCors(policyName: "CorsPolicy");

app.Map("/", () => Results.Redirect("/swagger"));


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
