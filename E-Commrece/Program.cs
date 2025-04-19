
using Domain.Contracts;
using E_Commrece.Extentions;
using E_Commrece.Factories;
using E_Commrece.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.Abstractions;

namespace E_Commrece
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            
            #region ConfigueServices
            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddPresentationServices();


            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Build
            var app = builder.Build();
            #endregion
            #region MiddleWares
             app.UseCustomMiddleWareExceptions();
            await app.SeedDbAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            
            #endregion



        }
       
    }
}
