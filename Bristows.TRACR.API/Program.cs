using Bristows.TRACR.BLL.Services;
using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.DAL.Factories;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Bristows.TRACR.Model.Contexts;
using Bristows.TRACR.DAL.Repositories;
using Bristows.TRACR.API.Authorization;
using Microsoft.EntityFrameworkCore.Design;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Bristows.TRACR.API.TESTDEV;
using Bristows.TRACR.API.TESTDEV.DependancyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Bristows.TRACR.API.AuthenticationTemplate;
using Bristows.TRACR.API.AuthenticationTemplate.Interfaces;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
builder.Logging.AddConsole();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "AllowSpecificOrigin",
                        builder =>
                        {
                            builder.AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()
                                    .SetIsOriginAllowed(hostName => true);
                        });
    });

builder.Services.AddScoped(typeof(IDbFactory<TRACRContext>), typeof(TRACRDbFactory));

builder.Services.AddScoped<IDesignTimeDbContextFactory<TRACRContext>, DesignTimeDbContextFactory>();

IDesignTimeDbContextFactory<TRACRContext>? designTimeDbContextFactory = builder.Services.BuildServiceProvider().GetRequiredService<IDesignTimeDbContextFactory<TRACRContext>>();
TRACRContext? designTimeDbContext = designTimeDbContextFactory?.CreateDbContext(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IDbFactory<TRACRContext>, TRACRDbFactory>();
builder.Services.AddScoped<UnitOfWork<TRACRContext>, TRACRWorkUnit>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDiaryService, DiaryService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IDiaryRepository, DiaryRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IDiaryTaskRepository, DiaryTaskRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();

builder.Services.AddTransient<IAuthProvider, AuthProvider>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<IAuthorizationHandler, AdminRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ReviewerRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, TraineeRequirementHandler>();

//    DI PRACTICE ----------------------------------
// builder.Services.AddTransient<ITransientCounterDependancy, TransientCounterDependancy>();
// builder.Services.AddScoped<IScopedCounterDependancy, ScopedCounterDependancy>();
// builder.Services.AddSingleton<ISingletonCounterDependancy, SingletonCounterDependancy>();
//    DI PRACTICE ----------------------------------


// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//     options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
// })
// .AddCookie(options =>
// {
//     options.Cookie.Name = "Bristows-TRACR";
//     options.Cookie.HttpOnly = true;
//     options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
//     options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Change this if using HTTPS
//     options.Cookie.IsEssential = true;
// });

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
// builder.Services.ConfigureOptions<JwtOptionsSetup>();
// builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
// builder.Services.ConfigureOptions<ServerOptionsSetup>();

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = NegotiateDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = NegotiateDefaults.AuthenticationScheme;
//     options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
// })
//   .AddNegotiate()

#region creating custom claim (DomainUsername) in ClaimsPrincipal
//***************************************************************
static ClaimsPrincipal GetClaimsPrincipal(IHttpContextAccessor httpContextAccessor)
{
    const string domainUsername = "BRISTOWS\\NRhima"; //replace with httpContextAccessor.HttpContext.User.Identity.Name in a domain environment
    List<Claim> claims = new() { new Claim("DomainUsername", domainUsername) };
    ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    return new ClaimsPrincipal(identity);
}
#endregion // Registering IHttpContextAccessor and configuring ClaimsPrincipal using custom function
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    return GetClaimsPrincipal(httpContextAccessor);
});
// Configuring authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("tracr-default", policy => policy.RequireAuthenticatedUser());
    options.AddPolicy("trainee//reviewer", policy => 
    {
        policy.Requirements.Add(new TraineeRequirement());
        policy.Requirements.Add(new ReviewerRequirement());
    });
    options.AddPolicy("admin//reviewer", policy => 
    {
        policy.Requirements.Add(new AdminRequirement());
        policy.Requirements.Add(new ReviewerRequirement());
    });
    options.AddPolicy("admin", policy => policy.Requirements.Add(new AdminRequirement()));
    options.AddPolicy("trainee", policy => policy.Requirements.Add(new TraineeRequirement()));
    options.AddPolicy("reviewer", policy => policy.Requirements.Add(new ReviewerRequirement()));
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) // Configuring the HTTP request pipeline.
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}

app.UseSession();
app.UseHttpsRedirection();
app.UseAuthentication();//---------//intercepts arriving requests, inspects the Authorization header and-
app.UseRouting();                 //-invokes the correct Authentication handler i.e. .AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer()-
app.UseAuthorization();          //-which will extract information (IsAuthenticated[bool] RoleClaimType, Claims, Tokens etc.) from within HTTPContext.User.Identity/Identities[n]-
app.MapControllers();           //-those local variables are of type: {HTTPContext: *.DefaultHttpContext}.{User: System.Security.Claims.ClaimsPrincipal}.{Identity: System.Security.Claims.ClaimsIdentity}-
app.Run();                     //-and the overall job of 'Authentication' is to populate the 'IsAuthenticated' and 'Claims' variables based on whatever information the specific authentication handler invoked is expecting, in this case our handler expects and reads from JSON Web Token.
