using EFFunkiness.Server.Data;
using EFFunkiness.Server.Queries;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace EFFunkiness.Server
{
    public static class Dependencies
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EF Funkiness",
                    Description = "Examining the behaviour of the enabling retries and the InvalidOperation Exception when executing two queries on the same context at the same time.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Quinntyne Browm",
                        Email = "quinntynebrown@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });

                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddMediatR(typeof(GetClients));

            services.AddTransient<IEFFunkinessDbContext, EFFunkinessDbContext>();

            services.AddDbContext<EFFunkinessDbContext>(options =>
            {
                options.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"],
                    (builder) => {

                        builder.MigrationsAssembly("EFFunkiness.Server");

                        if (Convert.ToBoolean(configuration["EnableRetryOnFailure"]))
                        {
                            builder.EnableRetryOnFailure();
                        }
                    })
                .UseLoggerFactory(EFFunkinessDbContext.ConsoleLoggerFactory)
                .EnableSensitiveDataLogging();
            });

            services.AddProblemDetails();

            services.AddControllers();
        }
    }
}
