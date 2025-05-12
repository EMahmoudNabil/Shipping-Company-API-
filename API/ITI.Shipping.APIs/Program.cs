using ITI.Shipping.APIs.Controllers;
using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Auth;
using ITI.Shipping.Core.Application.Abstraction.chat.DTO;
using ITI.Shipping.Core.Application.Abstraction.User;
using ITI.Shipping.Core.Application.Mapping;
using ITI.Shipping.Core.Application.Services;
using ITI.Shipping.Core.Application.Services.AuthServices;
using ITI.Shipping.Core.Application.Services.UserServices;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using ITI.Shipping.Infrastructure.Presistence.Repositories;
using ITI.Shipping.Infrastructure.Presistence.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
namespace ITI.Shipping.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.Configure<OpenAIConfiguration>(builder.Configuration.GetSection("OpenAI"));


            var app = builder.Build();

            #region Configure MiddleWare

            //if(app.Environment.IsDevelopment())
            //{

                app.UseSwagger();
                app.UseSwaggerUI();
            //}
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(txt);
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}