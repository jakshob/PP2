﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Webservice.Models;

namespace Webservice
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(new DataService());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            MapperConfig();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

        //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World and RAWDATA!");
            //});
        }

        private void MapperConfig()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Question, QuestionModel>();
                cfg.CreateMap<Question, QuestionListModel>();
                cfg.CreateMap<Comment, CommentListModel>();
                //Måske skal nedenstående bruges på QA_User?
                //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
            });
        }

    }
}
