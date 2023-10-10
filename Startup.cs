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

        // Este é o método ConfigureServices onde você configura os serviços da aplicação.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração de outros serviços...

            services.AddDbContext<ProdutoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers(); // Adiciona suporte para controllers

            // Configuração do MediatR
            services.AddMediatR(typeof(Startup));

            // Configuração de outros serviços...

            // Certifique-se de adicionar outras configurações e serviços necessários aqui
        }

        // Este é o método Configure onde você configura o pipeline de middleware HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configure o tratamento de erros para ambientes de produção, se necessário.
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Ativar o Strict Transport Security (HSTS)
            }

            // Configuração do middleware para roteamento de controllers
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
