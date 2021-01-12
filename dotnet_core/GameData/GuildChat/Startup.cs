using GuildChat.Hubs.v1;
using GuildChat.Models.v1;
using GuildChat.Services.v1;
using GuildChat.Business.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.SignalR;

namespace GuildChat
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
            services.Configure<GuildChatDatabaseSettings>(Configuration.GetSection(nameof(GuildChatDatabaseSettings)));

            services.AddSingleton<IGuildChatDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<GuildChatDatabaseSettings>>().Value);

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            services.AddSingleton<IMongoDB<Message>, MongoDB<Message>>();
            services.AddSingleton<IMessageBusiness, MessageBusiness>();
            services.AddSingleton<IChatBusiness, ChatBusiness>();

            services.AddSwaggerGen();

            services.AddSignalR();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }*/

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                }

                options.DocExpansion(DocExpansion.List);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });

        }
    }
}
