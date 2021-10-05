using Locadora.Business;
using Locadora.Data;
using Locadora.Repository;
using Locadora.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Locadora
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
            services.AddControllers();
            services.AddDbContext<APILocadoraContext>(opts => opts.UseInMemoryDatabase("InMemoryDataBase"));
            services.AddSwaggerGen();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ILocacaoRepository, LocacaoRepository>();
            services.AddTransient<IFilmeRepository, FilmeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<APILocadoraContext>();
            AdicionarDados.AdicionarDadosAoInicializar(context);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Locadora")
            );
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
