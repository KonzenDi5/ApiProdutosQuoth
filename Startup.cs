using Microsoft.EntityFrameworkCore;
using MediatR;

namespace ProdutosQuoth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //  método ConfigureServices  configura os serviços da aplicação
        public void ConfigureServices(IServiceCollection services)
        {
    

            services.AddDbContext<ProdutoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers(); // Adiciona suporte para controllers

            // Configuração do MediatR
            services.AddMediatR(typeof(Startup));

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // tratamento de erros para ambientes de produção, se necessário.
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Ativar o Strict Transport Security (HSTS)
            }

            // middleware para roteamento de controllers
            app.UseRouting();

            // Middleware para forçar o uso de HTTPS
            app.UseHttpsRedirection();

            // Define as rotas dos controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
