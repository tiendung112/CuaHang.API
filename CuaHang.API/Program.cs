using CuaHang.API;
using CuaHang.API.MiddleWare;
using CuaHang.Common;
using CuaHang.Models.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Quản Lý Cửa hàng", Version = "v1" });
    option.CustomSchemaIds(type => type.ToString());
    option.AddSecurityDefinition(CommonContaint.JWT.SchemeName, new OpenApiSecurityScheme
    {
        Name = CommonContaint.JWT.Authorization,
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = CommonContaint.JWT.SchemeName,
        BearerFormat = CommonContaint.JWT.BearerFormat,
        Description = CommonContaint.JWT.Description
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = CommonContaint.JWT.SchemeName
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddDbContext<AppDBContext>(opt => opt
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AuthSecret").Value!))
    };
});
builder.Services.AddTransient<JwtMiddleWare>();
builder.Services.AddTransient<ExceptionHandlerMiddleWare>();

builder.Services.AddHttpContextAccessor();

Bootstrap.Register(builder.Services, builder.Configuration);


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials
app.UseAuthentication();

app.UseMiddleware<ExceptionHandlerMiddleWare>();
app.UseMiddleware<JwtMiddleWare>();

app.UseAuthorization();

app.MapControllers();

app.Run();
