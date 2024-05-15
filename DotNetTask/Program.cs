using DotNetTask;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Data.Common;
using System.Text;

var _AppSpecificOrigins = "_AppSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:80");
builder.Services.AddHttpContextAccessor();
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _AppSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
builder.Services.Configure<FormOptions>(options =>
{
    // Set the limit to 10 MB
    options.MultipartBodyLengthLimit = 10240000;
});
builder.Services.AddScoped<IHomeRepo, HomeRepo>();
builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wSle7llXyAEgHZymzC1xjj28x8iqWoEYrLNv6vIO073WcOLyNMxMFTGrHHJPtb0BVUmeaT7JJ6md2tZI")),
        ClockSkew = TimeSpan.Zero
    };
});
//builder.Services.Configure<DbConnection>(configuration.GetSection("SqlDbConnection"));

var app = builder.Build();

//IHostApplicationLifetime lifetime = app.Lifetime;
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    if (app.Environment.IsProduction())
    {
        builder.WebHost.UseUrls(GlobalSetting.ServerUrl());
    }
    app.UseSwagger().UseSwaggerUI(options =>
    {
        options.DocExpansion(DocExpansion.None);
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetTask");
        options.EnableFilter("");
        options.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
}

if (app.Environment.IsProduction())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    builder.WebHost.UseUrls(GlobalSetting.ServerUrl());
}

//if (app.Environment.IsProduction())
//{
//    //app.UseHttpsRedirection();
//}
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 401)
    {
        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            Title = context.HttpContext.Response.Headers["WWW-Authenticate"][0].Contains("invalid_token") ? "Token Expired" : "401 - Unauthorized",
            StatusCode = 401,
            Status = false,
            Msg = "Access is denied due to invalid token, allowed only registered users."
        });
    }
    else if (context.HttpContext.Response.StatusCode == 403)
    {
        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            Title = "403 - Permission Denied",
            StatusCode = 403,
            Status = false,
            Msg = "You don't have permission to access this page."
        });
    }
});
DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
defaultFilesOptions.DefaultFileNames.Clear();
defaultFilesOptions.DefaultFileNames.Add("view/index.html");
app.UseDefaultFiles(defaultFilesOptions);
//app.UseStaticFiles();
app.UseCors(_AppSpecificOrigins);
app.UseAuthentication();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();