using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QuestingEngine.API.Mappers;
using QuestingEngine.Models;
using QuestingEngine.Repository;
using QuestingEngine.Service;
using QuestingEngine.Service.Commands;
using System.Reflection;

namespace QuestingEngine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatR(typeof(UpdatePlayerPointCommand).GetTypeInfo().Assembly);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuestingEngine", Version = "v1" });
            });
            services.Configure<MongoConfiguration>(Configuration.GetSection("MongoDb"));
            services
                // Register services
                .AddScoped<IQuestService, QuestService>()
                .AddScoped<IPlayerService, PlayerService>()
                .AddScoped<ISeedDataService, SeedDataService>()
                // Register repositories
                .AddScoped<ILevelBonusRateRepository, LevelBonusRateRepository>()
                .AddScoped<IBetRateRepository, BetRateRepository>()
                .AddScoped<IMilestoneRepository, MilestoneRepository>()
                .AddScoped<IQuestRepository, QuestRepository>()
                .AddScoped<IPlayerRepository, PlayerRepository>();

            // Seed data
            var provider = services.BuildServiceProvider();
            var seedDataService = provider.GetRequiredService<ISeedDataService>();
            seedDataService.InitializeData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuestingEngine v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
