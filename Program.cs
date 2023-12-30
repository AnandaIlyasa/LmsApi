using LmsApi.Utils;
using LmsApi.Config;
using LmsApi.Handler;
using LmsApi.IRepo;
using LmsApi.IService;
using LmsApi.Repo;
using LmsApi.Service;
using LmsApi.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace LmsApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidationHandler>();
            options.Filters.Add<ErrorHandler>();
            options.Filters.Add(new AuthorizeFilter());
        });

        builder.Services.AddDbContext<DBContextConfig>();

        builder.Services.AddScoped<IRoleRepo, RoleRepo>();
        builder.Services.AddScoped<IUserRepo, UserRepo>();
        builder.Services.AddScoped<IClassRepo, ClassRepo>();
        builder.Services.AddScoped<ISessionAttendanceRepo, SessionAttendanceRepo>();
        builder.Services.AddScoped<ISessionRepo, SessionRepo>();
        builder.Services.AddScoped<ISessionMaterialRepo, SessionMaterialRepo>();
        builder.Services.AddScoped<ISessionTaskRepo, SessionTaskRepo>();
        builder.Services.AddScoped<ISubmissionRepo, SubmissionRepo>();
        builder.Services.AddScoped<ISubmissionDetailQuestionRepo, SubmissionDetailQuestionRepo>();
        builder.Services.AddScoped<ISubmissionDetailFileRepo, SubmissionDetailFileRepo>();
        builder.Services.AddScoped<ILMSFileRepo, LMSFileRepo>();
        builder.Services.AddScoped<IStudentClassRepo, StudentClassRepo>();
        builder.Services.AddScoped<IForumRepo, ForumRepo>();
        builder.Services.AddScoped<IForumCommentRepo, ForumCommentRepo>();
        builder.Services.AddScoped<ILearningRepo, LearningRepo>();
        builder.Services.AddScoped<ISessionMaterialFileRepo, SessionMaterialFileRepo>();
        builder.Services.AddScoped<ITaskQuestionRepo, TaskQuestionRepo>();
        builder.Services.AddScoped<ITaskMultipleChoiceOptionRepo, TaskMultipleChoiceOptionRepo>();
        builder.Services.AddScoped<ITaskFileRepo, TaskFileRepo>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IClassService, ClassService>();
        builder.Services.AddScoped<ISessionService, SessionService>();
        builder.Services.AddScoped<ITaskSubmissionService, TaskSubmissionService>();
        builder.Services.AddScoped<IForumService, ForumService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IPrincipleService, PrincpleService>();

        var key = Encoding.ASCII.GetBytes(JwtUtil.KeyStr());
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                };

                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = 401;
                        var result = JsonSerializer.Serialize(new { message = "Invalid Authentication" });
                        context.Response.Headers.Append("Content-Type", "application/json");
                        await context.Response.WriteAsync(result);
                    }
                };
            });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // Configure the HTTP request pipeline.

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
