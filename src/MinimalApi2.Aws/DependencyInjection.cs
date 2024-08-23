using Amazon.S3;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalApi2.Aws.Abstractions;
using MinimalApi2.Aws.Concretes;
using MinimalApi2.Aws.Data;
using MinimalApi2.Aws.Entities.Identity;
using MinimalApi2.Aws.Helpers;
using MinimalApi2.Aws.Middlewares;
using MinimalApi2.Aws.Options;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace MinimalApi2.Aws
{
    public static class DependencyInjection
    {
        public static IServiceCollection ApiServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            CQRS_ServiceInject(services);

            Endpoint_ServiceRegistration(services);

            Swagger_ServiceInjection(services);

            Mediatr_ServiceRegistration(services);

            Database_ServiceRegistration(services);

            Identity_ServiceInjection(services);

            Jwt_ServiceRegistration(services);

            Authorization_ServiceInjection(services);

            Csrf_ServiceInjection(services);

            Service_Injection(services);

            //Seed_ServiceInjection(services);

            AmazonS3_ServiceInjection(services, configuration);

            AmazonLambda_ServiceInjection(services);

            return services;
        }



        public static WebApplication ApiApplicationRegistration(this WebApplication app)
        {
            Swagger_ApplicationInjection(app);

            CQRS_ApplicationInject(app);

            Endpoint_ApplicationInjection(app);

            Other_ApplicationRegistration(app);

            Csrf_ApplicationInjection(app);

            return app;
        }

        private static void Csrf_ServiceInjection(IServiceCollection services)
        {
            services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
        }

        private static void Csrf_ApplicationInjection(IApplicationBuilder app)
        {
            app.UseAntiforgery();
        }

        private static void Seed_ServiceInjection(IServiceCollection services)
        {
            var _sp = services.BuildServiceProvider();

            var _context = _sp.GetRequiredService<MinimalDbContext>();
            var _userManager = _sp.GetRequiredService<UserManager<User>>();

            MinimalSeedContext.SeedAsync(_context).GetAwaiter().GetResult();
        }

        private static void Other_ApplicationRegistration(WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();
        }

        private static void Endpoint_ServiceRegistration(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
        }

        private static void Database_ServiceRegistration(IServiceCollection services)
        {
            var sqlServerOptions = services.GetOptions<SqlServerOptions>("SqlServerOptions");

            if (int.Parse(sqlServerOptions.InMemory) != 1)
            {
                #region OLD SQLSERVER
                //services.AddDbContext<MinimalDbContext>(options =>
                //{
                //    options.UseSqlServer(
                //        sqlServerOptions.SqlConnection,
                //        sqlOptions =>
                //        {
                //            sqlOptions.EnableRetryOnFailure(
                //                maxRetryCount: int.Parse(sqlServerOptions.RetryCount),
                //                maxRetryDelay: TimeSpan.FromSeconds(int.Parse(sqlServerOptions.RetryDelay)),
                //                errorNumbersToAdd: null);
                //        });
                //});
                #endregion

                services.AddDbContext<MinimalDbContext>(options => options.UseNpgsql(sqlServerOptions.SqlConnection));
            }
            else
            {
                #region OLD SQLSERVER
                //services.AddDbContext<MinimalDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase(Constant.InMemoryDb.DatabaseName);
                //});
                #endregion
            }
        }

        private static void Authorization_ServiceInjection(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "User");
                });
            });
        }

        private static void Jwt_ServiceRegistration(IServiceCollection services)
        {
            var jwtOptions = services.GetOptions<JwtOptions>("JwtOptions");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = jwtOptions.Issuer,
                  ValidAudience = jwtOptions.Audience,
                  IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(jwtOptions.Secret)
                  ),
                  LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
              })
              .AddCookie("default");
        }

        private static void Identity_ServiceInjection(IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<MinimalDbContext>()
           .AddDefaultTokenProviders();
        }

        private static void Service_Injection(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUserServive, UserService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IImageService, ImageService>();

            //services.Configure<FormOptions>(options =>
            //{
            //    options.ValueCountLimit = 10_000; // Öğe sayısı sınırlamasını artırın
            //    options.ValueLengthLimit = 1024 * 1024 * 100; // Değer uzunluğu sınırlamasını artırın (örneğin, 100 MB)
            //    options.MultipartBodyLengthLimit = 1024 * 1024 * 100; // Multipart (dosya yükleme) sınırlamasını artırın (örneğin, 100 MB)
            //});
        }

        private static void AmazonS3_ServiceInjection(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());

            services.AddAWSService<IAmazonS3>();
        }

        private static void AmazonLambda_ServiceInjection(IServiceCollection services)
        {
            services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
        }

        private static void CQRS_ServiceInject(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyHeader()
                                       .AllowAnyMethod()
                                       .AllowAnyHeader()
                                       .SetIsOriginAllowed(_ => true)
                                       .AllowCredentials();
                    });
            });
        }

        private static void CQRS_ApplicationInject(WebApplication app)
        {
            app.UseCors("AllowOrigin");
        }

        private static void Swagger_ServiceInjection(IServiceCollection services)
        {
            services.AddSwaggerGen();
        }

        private static void Swagger_ApplicationInjection(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }

        private static void Mediatr_ServiceRegistration(IServiceCollection services)
        {
            services.AddMediatR(typeof(Program).Assembly);
        }

        private static void Middleware_ApplicationInjection(WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        private static void Endpoint_ApplicationInjection(WebApplication app)
        {
            Middleware_ApplicationInjection(app);

            var endpointDefinations = typeof(Program).Assembly.GetTypes()
              .Where(t => t.IsAssignableTo(typeof(IEndpointDefination)) && !t.IsAbstract && !t.IsInterface)
              .Select(Activator.CreateInstance)
              .Cast<IEndpointDefination>();

            foreach (var endpointDefination in endpointDefinations)
                endpointDefination.RegisterEndpoints(app);
        }



    }
}
