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

var builder = WebApplication.CreateBuilder(args);
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
// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("tracr-default", policy => policy.RequireAuthenticatedUser());
    options.AddPolicy("tracr-admin", policy => policy.Requirements.Add(new AdminRequirement()));
    options.AddPolicy("tracr-trainee", policy => policy.Requirements.Add(new TraineeRequirement()));
    options.AddPolicy("tracr-reviewer", policy => policy.Requirements.Add(new ReviewerRequirement()));
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}

app.UseSession();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
