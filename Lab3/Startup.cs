using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace Lab3
{
    public class BankDepositContext : DbContext
    {
        public BankDepositContext(DbContextOptions<BankDepositContext> options) : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Address> Addresses { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Passport> Passports { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Person> Persons { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Type> Types { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Visit> Visits { get; set; }
    }
    
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
            services.AddRazorPages();
            
            services.AddDbContext<BankDepositContext>(options => 
                options.UseMySql(Configuration.GetConnectionString("BankDeposit")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
